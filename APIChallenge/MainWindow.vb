Imports RiotSharp

Public Class MainWindow
   Dim WithEvents MatchCache As MatchCache
   Dim ImageCache As ImageCache
   Sub MyMatches_CountChanged()
      RefreshMatchListBox()
   End Sub

   Sub MyMatches_LoadChanged()
      RefreshMatchListBox()
      TabPage2.Enabled = MatchCache.MatchList.LoadedIndex = MatchCache.MatchList.Count - 1
   End Sub

   Private Sub MainWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      APIHelper.API_INIT()
      TimeHelper.UpdateLastRoundedDateTime()

      CacheManager.Init()
      CacheManager.LoadAllCaches()

      MatchCache = CacheManager.CacheList("Matches")
      ImageCache = CacheManager.CacheList("Images")

      ' Handlers need to be added after the initial load, because the object is replaced.
      AddHandler MatchCache.MatchList.CountChanged, AddressOf Me.MyMatches_CountChanged
      AddHandler MatchCache.MatchList.LoadChanged, AddressOf Me.MyMatches_LoadChanged

      MatchCache.MatchList.ForceCountChangedRefresh()
      LastBucketTimeTextBox.Text = MatchCache.MatchList.LastURFAPICall().ToLocalTime
   End Sub

   Sub RefreshMatchListBox()
      ListBox1.Items.Clear()
      For Each i As Match In MatchCache.MatchList
         ListBox1.Items.Add(i.ToString())
      Next
      If MatchCache.MatchList.LoadedIndex < MatchCache.MatchList.Count - 1 Then
         ListBox1.SelectedIndex = MatchCache.MatchList.LoadedIndex + 1
      End If
      CacheCountLabel.Text = "Total Matches in Cache: " & MatchCache.MatchList.LoadedIndex + 1 & "/" & MatchCache.MatchList.Count
   End Sub

   Private Sub MainWindow_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
      CacheManager.StoreAllCaches()
   End Sub

   ' This sub does not touch UI (for BackgroundWorker)
   Private Sub LoadInUrfMatches(ByVal epochTime As Integer)
      Dim matchIDs As List(Of Integer) = APIHelper.API_GET_URF_MATCHES(epochTime)

      If matchIDs.Count = 0 Then
         Console.WriteLine("No matches returned by API-challenge")
         Return
      End If

      For Each i As Integer In matchIDs
         MatchCache.MatchList.QuietAdd(New Match(i))
      Next

      SyncLock MatchCache
         CacheManager.StoreCacheFile(MatchCache.CACHE_FILE_NAME, MatchCache)
      End SyncLock
   End Sub

   Private Sub LoadInLatestUrfMatches()
      TimeHelper.UpdateLastRoundedDateTime()

      If Not TimeHelper.CanUpdate(MatchCache.MatchList.LastURFAPICall) Then
         Console.WriteLine("Not calling API. We already did it for this 5 minutes")
         Return
      Else
      End If

      CurrentStatusLabel.Text = "Retrieving Recent URF Matches..."
      Dim oldCount As Integer = MatchCache.MatchList.Count

      LoadInUrfMatches(TimeHelper.PreviousRoundedEpochTime)
      MatchCache.MatchList.LastURFAPICall = TimeHelper.PreviousRoundedEpochDateTime
      LastBucketTimeTextBox.Text = MatchCache.MatchList.LastURFAPICall().ToLocalTime
      MatchCache.Trim()
      MatchCache.MatchList.ForceCountChangedRefresh()

      CurrentStatusLabel.Text = (MatchCache.MatchList.Count - oldCount) & " matches added to cache"
   End Sub

   ' Populate the cache with at least 100 games from the given time (used with BackgroundWorker)
   Private Sub PopulateLatestUrfMatches(ByVal givenTime As DateTime)
      Dim timeIndex As DateTime = givenTime

      CurrentStatusLabel.Text = "Populating with URF Matches..."
      Dim oldCount As Integer = MatchCache.MatchList.Count

      Dim progressNum As Integer = 0
      While MatchCache.MatchList.Count < MatchCache.CACHE_LIMIT
         Console.WriteLine("Loading games from: " & timeIndex.ToString)
         LoadInUrfMatches(TimeHelper.DateTimeToEpoch(timeIndex))
         timeIndex = timeIndex.Subtract(New TimeSpan(0, 5, 0))
         progressNum += 1
         CacheBackgroundWorker.ReportProgress(100 - CSng(MatchCache.CACHE_LIMIT - Math.Min(MatchCache.CACHE_LIMIT, MatchCache.MatchList.Count)) / MatchCache.CACHE_LIMIT * 100)
         If CacheBackgroundWorker.CancellationPending Then
            Exit While
         End If
         Threading.Thread.Sleep(APIHelper.API_DELAY)
      End While

      CacheBackgroundWorker.ReportProgress(100)
   End Sub

   Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
      LoadInLatestUrfMatches()
   End Sub

   Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
      ImageCache.GetImagesAsync()
   End Sub

   Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
      APIChallengeTimer.Enabled = True
      Button4.Enabled = True
      Button3.Enabled = False
      If TimeHelper.CanUpdate(MatchCache.MatchList.LastURFAPICall) Then
         LoadInLatestUrfMatches()
      End If
   End Sub

   Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
      APIChallengeTimer.Enabled = False
      Button4.Enabled = False
      Button3.Enabled = True
   End Sub

   Private Sub APIChallengeTimer_Tick(sender As Object, e As EventArgs) Handles APIChallengeTimer.Tick
      If TimeHelper.CanUpdate(MatchCache.MatchList.LastURFAPICall) Then
         LoadInLatestUrfMatches()
      End If
   End Sub

   Private Sub EpochTimer_Tick(sender As Object, e As EventArgs) Handles EpochTimer.Tick
      TimeHelper.UpdateLastRoundedDateTime()
   End Sub

   Private UpdatedIndex As Integer
   Private Sub MatchLoadingBackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles MatchLoadingBackgroundWorker.DoWork
      ' Since the list will have entries ADDED while this worker uses the list asynchronously, we get to
      '  work to the end of the list as we know it.
      Dim matchCount = 0, startingIndex = 0
      SyncLock MatchCache
         matchCount = MatchCache.MatchList.Count - 1
         startingIndex = If(MatchCache.MatchList.LoadedIndex <> -1, MatchCache.MatchList.LoadedIndex, 0)
      End SyncLock

      For index = startingIndex To matchCount
         If MatchCache.MatchList(index).IsLoaded() Then
            Continue For
         End If

         Dim info = APIHelper.API_GET_MATCH_INFO(MatchCache.MatchList(index).GetMatchID)
         MatchCache.MatchList(index).SetMatchInfo(info)

         ' If there was an error, try it again (repeat the current loop)
         If Not MatchCache.MatchList(index).IsLoaded() Then
            index -= 1
         End If

         UpdatedIndex = index
         MatchLoadingBackgroundWorker.ReportProgress(Math.Min(CSng(index - startingIndex) / (matchCount - startingIndex) * 100, 100))
         SyncLock MatchCache
            CacheManager.StoreCacheFile(MatchCache.CACHE_FILE_NAME, MatchCache)
         End SyncLock

         If MatchLoadingBackgroundWorker.CancellationPending Then
            matchCount = index
            Exit For
         End If

         ' One API call every X seconds
         System.Threading.Thread.Sleep(APIHelper.API_DELAY)
      Next

      UpdatedIndex = matchCount
      MatchLoadingBackgroundWorker.ReportProgress(100)
   End Sub

   Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
      If Not MatchLoadingBackgroundWorker.IsBusy Then
         Button5.Enabled = False
         ProgressBarLabel.Text = "Getting Match Info:"
         MatchLoadingBackgroundWorker.RunWorkerAsync()
      End If
   End Sub

   Private Sub MatchLoadingBackgroundWorker_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles MatchLoadingBackgroundWorker.ProgressChanged
      StatusProgressBar.Value = e.ProgressPercentage
      MatchCache.MatchList.LoadedIndex = UpdatedIndex
      If e.ProgressPercentage = 100 Then
         Button5.Enabled = True
      End If
   End Sub

   Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
      If Not CacheBackgroundWorker.IsBusy Then
         ProgressBarLabel.Text = "Populating Cache with Games:"
         Button6.Enabled = False
         CacheBackgroundWorker.RunWorkerAsync()
      End If
   End Sub

   Private Sub CacheBackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles CacheBackgroundWorker.DoWork
      TimeHelper.UpdateLastRoundedDateTime()
      PopulateLatestUrfMatches(TimeHelper.PreviousRoundedEpochDateTime)
   End Sub

   Private Sub CacheBackgroundWorker_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles CacheBackgroundWorker.ProgressChanged
      StatusProgressBar.Value = e.ProgressPercentage
      MatchCache.MatchList.ForceCountChangedRefresh()
      If e.ProgressPercentage = 100 Then
         Button6.Enabled = True
         MatchCache.Trim()
         CurrentStatusLabel.Text = "Cache filled to " & MatchCache.CACHE_LIMIT & " matches"
      End If
   End Sub

   Private Sub Button7_Click(sender As Object, e As EventArgs)
      MatchCache.Trim()
   End Sub

   Private Class Tuple
      Public a As Integer
      Public b As Integer

      Sub New(ByVal x As Integer, ByVal y As Integer)
         a = x
         b = y
      End Sub
   End Class

   Private Sub Button7_Click_1(sender As Object, e As EventArgs) Handles Button7.Click
      Dim totalGames As Integer = MatchCache.MatchList.Count
      Dim totalBlueSideWins As Integer = 0
      Dim totalWinnerFirstWins As Integer = 0
      Dim a As New Dictionary(Of Integer, Tuple)
      For Each m As Match In MatchCache.MatchList
         Dim matchInfo = m.GetMatchInfo()
         Dim winningTeamID = If(matchInfo.Teams(0).Winner, matchInfo.Teams(0).TeamId, matchInfo.Teams(1).TeamId)

         For Each participant As MatchEndpoint.Participant In matchInfo.Participants
            If Not a.ContainsKey(participant.ChampionId) Then
               a.Add(participant.ChampionId, New Tuple(0, 0))
            End If

            If participant.TeamId = winningTeamID Then
               a(participant.ChampionId).a += 1
               a(participant.ChampionId).b += 1
            Else
               a(participant.ChampionId).b += 1
            End If

            'If (m.GetMatchInfo.Teams(0).FirstBlood AndAlso m.GetMatchInfo.Teams(0).Winner) Or _
            '   (m.GetMatchInfo.Teams(1).FirstBlood AndAlso m.GetMatchInfo.Teams(1).Winner) Then
            '   totalWinnerFirstWins += 1
            'End If
         Next
      Next

      ListBox2.Items.Clear()
      For Each item In a
         ListBox2.Items.Add(APIHelper.ChampionDict(item.Key).Name & ": " & item.Value.a & "/" & item.Value.b)
      Next
   End Sub

   Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
      'Dim query = From champs In APIHelper.ChampionDict.Values
      '            Where champs.Name = TextBox1.Text
      '            Select champs.Id
      PictureBox1.Image = ImageCache.Images(CInt(TextBox1.Text))

      'Console.WriteLine(GetWinRateOfChamp(CInt(TextBox1.Text)).ToString)
      Dim i As Integer = 0
      Dim results = GetWinRatesForAllChampions()
      For Each item In results
         i += 1
         If i >= 10 Then
            Return
         End If
         Console.WriteLine(item.ToString())
      Next
   End Sub

   Public Function GetWinRatesForAllChampions() As IEnumerable(Of ChampHistory)
      Dim champHistoryList As New List(Of ChampHistory)
      For Each champ In APIHelper.ChampionDict.Keys
         champHistoryList.Add(GetWinRateOfChamp(champ))
      Next

      Dim query = From c In champHistoryList, names In APIHelper.ChampionDict.Values
                  Where c.champID = names.Id
                  Order By c.GetWinRate() Descending
                  Select c
      Return query
   End Function

   Class ChampHistory
      Public champID As Integer
      Public gamesWon As Integer
      Public gamesPlayed As Integer
      Public champName As String

      Private _WinRate As Single

      Public Function GetWinRate() As Single
         Return _WinRate
      End Function

      Sub New(ByVal ID As Integer, ByVal won As Integer, ByVal played As Integer)
         With Me
            .champID = ID
            .gamesWon = won
            .gamesPlayed = played
         End With
         champName = APIHelper.ChampionDict(champID).Name
         _WinRate = CSng(gamesWon) / gamesPlayed
      End Sub

      Public Overrides Function ToString() As String
         Return "Champion " & champName & " won " & gamesWon & "/" & gamesPlayed & " (" & GetWinRate() & ")"
      End Function
   End Class

   Public Function GetWinRateOfChamp(ByVal champID As Integer) As ChampHistory
      ' Count how many matches the champion won in
      Dim winNum = (From matches In MatchCache.MatchList
                    Where matches.GetMatchInfo.ChampWon(CInt(champID))
                    Select matches.GetMatchID).Count()
      ' Count how many matches the champion was in
      Dim totalGames = (From champs In MatchCache.MatchList
                        Where champs.GetMatchInfo.Participants.ContainsChamp(CInt(champID))
                        Select champs.GetMatchID).Count()
      Dim a = MatchCache.MatchList(0).GetMatchInfo.Participants(0).Timeline.Lane
      Return New ChampHistory(champID, winNum, totalGames)
   End Function

   Class Against
      Inherits Tuple(Of Integer, Integer)

      Sub New(ByVal a As Integer, ByVal b As Integer)
         MyBase.New(Math.Min(a, b), Math.Max(a, b))
      End Sub

      Public Overrides Function Equals(obj As Object) As Boolean
         If obj Is Nothing Then
            Return False
         End If

         If Not TypeOf (obj) Is Against Then
            Return False
         End If

         Return MyBase.Item1 = CType(obj, Against).Item1 AndAlso MyBase.Item2 = CType(obj, Against).Item2
      End Function
   End Class

   Class Matchup
      Inherits Tuple(Of Against, MatchEndpoint.Lane) ' Matchup of two champs in a specific lane

      Public gamesFirstChampWon As Integer
      Public totalGames As Integer = 0

      Private _WinRate As Single
      Public Function GetWinRate() As Single
         Return _WinRate
      End Function

      Private Sub UpdateWinRate()
         _WinRate = CSng(gamesFirstChampWon) / totalGames
      End Sub

      Sub New(ByVal a As Against, ByVal b As MatchEndpoint.Lane)
         MyBase.New(a, b)
      End Sub

      Sub New(ByVal a As Integer, ByVal b As Integer, ByVal c As MatchEndpoint.Lane)
         MyBase.New(New Against(a, b), c)
      End Sub

      Public Overrides Function Equals(obj As Object) As Boolean
         If obj Is Nothing Then
            Return False
         End If

         If Not TypeOf (obj) Is Matchup Then
            Return False
         End If

         ' Lane is an integer, so shallow compare
         Return Me.Item1.Equals(CType(obj, Matchup).Item1) AndAlso Me.Item2 = CType(obj, Matchup).Item2
      End Function
   End Class

   ' A list class that adds 1 to totalGames if it exists, or else adds it to the list
   Class CountingList
      Inherits List(Of Matchup)

      Public Shadows Sub Add(ByVal a As Matchup, ByVal whoWon As Integer)
         If Not Me.Contains(a) Then
            MyBase.Add(a)
         End If

         Dim q = From item In Me
                 Where item.Equals(a)
                 Select item

         q(0).totalGames += 1
         q(0).gamesFirstChampWon += -whoWon + 2 ' 1 -> 1, 2 -> 0
      End Sub
   End Class

   ' Compute comprehensive matchup data
   Public Function GetMatchupData()

   End Function
End Class

Module LINQExtension
   <System.Runtime.CompilerServices.Extension()>
   Function Average(query As IEnumerable(Of TimeSpan))
      Dim totalTime As New TimeSpan(0, 0, 0)

      For Each t As TimeSpan In query
         totalTime += t
      Next

      Return New TimeSpan(totalTime.Ticks / query.Count)
   End Function

   <System.Runtime.CompilerServices.Extension()>
   Function ContainsChamp(query As List(Of MatchEndpoint.Participant), champID As Integer) As Boolean
      For Each p As MatchEndpoint.Participant In query
         If p.ChampionId = champID Then
            Return True
         End If
      Next
      Return False
   End Function

   <System.Runtime.CompilerServices.Extension()>
   Function ChampWon(query As MatchEndpoint.MatchDetail, champID As Integer) As Boolean
      Dim WinningTeamID = If(query.Teams(0).Winner, query.Teams(0).TeamId, query.Teams(1).TeamId)

      For Each p As MatchEndpoint.Participant In query.Participants
         If p.ChampionId = champID AndAlso p.TeamId = WinningTeamID Then
            Return True
         End If
      Next
      Return False
   End Function
End Module

