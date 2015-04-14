Imports RiotSharp

Public Class MainWindow
   Dim WithEvents MatchCache As MatchCache
   Dim StaticCache As StaticCache
   Sub MyMatches_CountChanged()
      RefreshMatchListBox()
   End Sub

   Sub MyMatches_LoadChanged()
      RefreshMatchListBox()
      TabPage2.Enabled = MatchCache.MatchList.LoadedIndex = MatchCache.MatchList.Count - 1
   End Sub

   Private Sub MainWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      CacheManager.Init()

      TimeHelper.UpdateLastRoundedDateTime()

      CacheManager.LoadAllCaches()
      APIHelper.API_INIT()

      MatchCache = CType(CacheManager.CacheList("Matches"), MatchCache)
      StaticCache = CType(CacheManager.CacheList("Static"), StaticCache)

      ' Handlers need to be added after the initial load, because the object is replaced.
      AddHandler MatchCache.MatchList.CountChanged, AddressOf Me.MyMatches_CountChanged
      AddHandler MatchCache.MatchList.LoadChanged, AddressOf Me.MyMatches_LoadChanged

      MatchCache.MatchList.ForceCountChangedRefresh()
      LastBucketTimeTextBox.Text = MatchCache.MatchList.LastURFAPICall().ToLocalTime.ToString

      ImageComboBox1.Items.Clear()
      ChampionImageList1.Images.Clear()
      For Each c In APIHelper.Champions.Values
         ChampionImageList1.Images.Add(StaticCache.Images(c.Id))
         Dim comboBoxItem As New ImageComboBox.ImageComboBoxItem(ChampionImageList1.Images.Count - 1, c.Name, New Font("Microsoft Sans Serif", 18.0), 0)
         ImageComboBox1.Items.Add(comboBoxItem)
      Next
      ImageComboBox1.SelectedIndex = 0
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
      Dim matchIDs As List(Of Integer) = APIHelper.API_GET_URF_MATCHES(epochTime.ToString)

      If matchIDs.Count = 0 Then
         Console.WriteLine("No matches returned by API-challenge")
         Return
      End If

      For Each i As Integer In matchIDs
         MatchCache.MatchList.QuietAdd(New Match(i))
      Next

      SyncLock MatchCache
         CacheManager.StoreCacheFile(MatchCache.CACHE_FILE_NAME, CType(MatchCache, EasyCache))
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
      LastBucketTimeTextBox.Text = MatchCache.MatchList.LastURFAPICall().ToLocalTime.ToString
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
         CacheBackgroundWorker.ReportProgress(CInt(100 - CSng(MatchCache.CACHE_LIMIT - Math.Min(MatchCache.CACHE_LIMIT, MatchCache.MatchList.Count)) / MatchCache.CACHE_LIMIT * 100))
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
      StaticCache.RebuildCache()
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
         MatchLoadingBackgroundWorker.ReportProgress(CInt(Math.Min(CSng(index - startingIndex) / (matchCount - startingIndex) * 100, 100)))
         SyncLock MatchCache
            CacheManager.StoreCacheFile(MatchCache.CACHE_FILE_NAME, CType(MatchCache, EasyCache))
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

   Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
      PictureBox1.Image = StaticCache.Images(CInt(TextBox1.Text))

      Dim totalGames = (From champs In MatchCache.MatchList
                        Where champs.GetMatchInfo.Participants.ContainsChamp(CInt(TextBox1.Text))
                        Select champs.GetMatchID)
      For Each item In totalGames
         Console.WriteLine(item)
      Next
      Console.WriteLine("---")

      'Console.WriteLine(GetWinRateOfChamp(CInt(TextBox1.Text)).ToString)
      'Dim i As Integer = 0
      'Dim results = GetWinRatesForAllChampions()
      'For Each item In results
      '   i += 1
      '   If i >= 10 Then
      '      Return
      '   End If
      '   Console.WriteLine(item.ToString())
      'Next
   End Sub

   Public Function GetWinRatesForAllChampions() As IEnumerable(Of ChampHistory)
      Dim champHistoryList As New List(Of ChampHistory)
      For Each champ In APIHelper.Champions.Keys
         champHistoryList.Add(GetWinRateOfChamp(champ))
      Next

      Dim query = From c In champHistoryList, names In APIHelper.Champions.Values
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
         champName = APIHelper.Champions(champID).Name
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

   Class Matchup
      Public MatchID As Integer

      Public ChampionID As Integer
      Public AllyChampionID As Integer
      Public EnemyChampionID As Integer
      Public EnemyChampionID2 As Integer

      Public Team As Integer
      Public Lane As MatchEndpoint.Lane
      Public WonLane As Boolean

      Public Sub New(ByVal match As Integer, ByVal cID As Integer, ByVal acID As Integer, ByVal eID As Integer, ByVal eID2 As Integer, ByVal ln As MatchEndpoint.Lane, ByVal won As Boolean, ByVal tm As Integer)
         With Me
            .MatchID = match
            .ChampionID = cID
            .AllyChampionID = acID
            .EnemyChampionID = eID
            .EnemyChampionID2 = eID2
            .Lane = ln
            .WonLane = won
            .Team = tm
         End With
      End Sub

      Private Sub OrganizeEnemies()
         If EnemyChampionID > EnemyChampionID2 AndAlso EnemyChampionID2 <> 0 Then
            Dim t As Integer = EnemyChampionID
            EnemyChampionID = EnemyChampionID2
            EnemyChampionID2 = t
         End If
      End Sub

      Public Overrides Function ToString() As String
         Dim allyStr As String = ""
         If AllyChampionID <> 0 Then
            allyStr = " and " & APIHelper.GetChampName(AllyChampionID)
         End If

         Dim enemy2Str As String = ""
         If EnemyChampionID2 <> 0 Then
            enemy2Str = " and " & APIHelper.GetChampName(EnemyChampionID2)
         End If

         Return APIHelper.GetChampName(ChampionID) & allyStr & " vs " & APIHelper.GetChampName(EnemyChampionID) & enemy2Str & " in " & Lane.ToString & ": " & If(WonLane, "Won!", "Lost!") & " (Match: " & MatchID & ")"
      End Function

      ' Returns a copy of the Matchup with enemies reversed if there's a match, and also sets
      '  the enemy2 in this matchup
      Public Function SetEnemy2IfSameMatch(ByVal givenMatchID As Integer, ByVal e2 As Integer, ByVal tm As Integer) As Matchup
         If (givenMatchID = Me.MatchID AndAlso Me.Team = tm) Then
            EnemyChampionID2 = e2
            OrganizeEnemies()
            Return New Matchup(Me.MatchID, Me.ChampionID, Me.AllyChampionID, EnemyChampionID2, Me.EnemyChampionID, Me.Lane, Me.WonLane, Me.Team)
         End If
         Return Nothing
      End Function

      'Public Overrides Function Equals(obj As Object) As Boolean
      '   If obj Is Nothing Then
      '      Return False
      '   End If
      '   If TypeOf (obj) Is Integer Then
      '      Return Me.MatchID = CInt(obj)
      '   End If
      '   If TypeOf (obj) Is Matchup Then
      '      Return Me.MatchID = CType(obj, Matchup).MatchID
      '   End If
      '   Return False
      'End Function
   End Class

   ' Riot's Lane Data is Shitty. Abort Mission!
   ' Compute comprehensive matchup data
   Public Function GetMatchupDataFor(ByVal champName As String) As List(Of Matchup)
      Dim MatchupList As New List(Of Matchup)

      Dim champID As Integer = APIHelper.GetChampID(champName)
      Dim now As DateTime = DateTime.Now
      Dim q = From match In MatchCache.MatchList, p In match.GetMatchInfo.Participants,
               match2 In MatchCache.MatchList, p2 In match2.GetMatchInfo.Participants
               Where match.GetMatchID = match2.GetMatchID AndAlso p.ParticipantId <> p2.ParticipantId AndAlso p.Timeline.Lane = p2.Timeline.Lane AndAlso p.ChampionId = champID AndAlso p.TeamId <> p2.TeamId
               Select match.GetMatchID, p.TeamId, BlueWon = match.GetMatchInfo().Teams(0).Winner, BlueTeamID = match.GetMatchInfo().Teams(0).TeamId, OtherChamp = p2.ChampionId, p.Timeline.Lane
      For Each item In q
         Dim wonGame As Boolean = False
         If item.TeamId = item.BlueTeamID Then
            If item.BlueWon Then
               wonGame = True
            Else
            End If
         ElseIf Not item.BlueWon Then
            wonGame = True
         End If

         'If item.GetMatchID = 1791704542 Then
         '   Console.WriteLine("Here")
         'End If

         Dim setSecondEnemy As Boolean = False
         Dim result As Matchup = Nothing
         For Each m In MatchupList
            result = m.SetEnemy2IfSameMatch(item.GetMatchID, item.OtherChamp, item.TeamId)
            If result IsNot Nothing Then
               setSecondEnemy = True
               Exit For
            End If
         Next
         If setSecondEnemy Then
            MatchupList.Add(result)
            Continue For
         End If

         Dim q2 = From match In MatchCache.MatchList, p In match.GetMatchInfo.Participants
                  Where match.GetMatchID = item.GetMatchID AndAlso p.ChampionId <> champID AndAlso p.TeamId = item.TeamId AndAlso p.Timeline.Lane = item.Lane
                  Select p.ChampionId
         Dim allyChamp = 0
         If q2.Count > 0 Then
            allyChamp = q2(0)
         End If

         MatchupList.Add(New Matchup(item.GetMatchID, champID, allyChamp, item.OtherChamp, 0, item.Lane, wonGame, item.TeamId))
      Next
      Console.WriteLine("That took: " & DateTime.Now.Subtract(now).ToString & " to complete")
      Return MatchupList
   End Function

   Private Sub MainWindow_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
      'TabControl1.ItemSize = New Point(Me.Width / (TabControl1.TabCount) - (7 - TabControl1.TabCount), TabControl1.ItemSize.Height)
   End Sub

   Dim CurrentMatchups As List(Of Matchup)

   Private Sub ImageComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ImageComboBox1.SelectedIndexChanged
      CurrentMatchups = GetMatchupDataFor(ImageComboBox1.Text)
      Dim names = (From m In CurrentMatchups
                   Order By APIHelper.GetChampName(m.EnemyChampionID)
                   Select APIHelper.GetChampName(m.EnemyChampionID)).Distinct()

      'For Each m In CurrentMatchups
      '   Console.WriteLine(m.ToString)
      'Next

      ImageComboBox2.Items.Clear()
      ChampionImageList2.Images.Clear()
      For Each c In names
         ChampionImageList2.Images.Add(StaticCache.Images(APIHelper.GetChampID(c)))
         Dim comboBoxItem As New ImageComboBox.ImageComboBoxItem(ChampionImageList2.Images.Count - 1, c, New Font("Microsoft Sans Serif", 18.0), 0)
         ImageComboBox2.Items.Add(comboBoxItem)
      Next
      ImageComboBox2.SelectedIndex = -1

      Console.WriteLine("Ready")
   End Sub

   Private Sub ImageComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ImageComboBox2.SelectedIndexChanged
      If ImageComboBox2.Text = Nothing Then
         Return
      End If

      Dim enemyChampID As Integer = APIHelper.GetChampID(CStr(ImageComboBox2.Text))
      Dim q = From m In CurrentMatchups
              Where m.ChampionID = APIHelper.GetChampID(ImageComboBox1.Text) AndAlso m.EnemyChampionID = enemyChampID
              Select m
      For Each m In q
         Console.WriteLine(m.ToString)
      Next

      Dim c = Aggregate m In q
              Where m.WonLane
              Into Count()

      Dim c2 = Aggregate m In q
              Into Count()

      Console.WriteLine("Total Won: " & c)
      Console.WriteLine("Total Games: " & c2)
   End Sub
End Class

Module LINQExtension
   <System.Runtime.CompilerServices.Extension()>
   Function Average(query As IEnumerable(Of TimeSpan)) As TimeSpan
      Dim totalTime As New TimeSpan(0, 0, 0)

      For Each t As TimeSpan In query
         totalTime += t
      Next

      Return New TimeSpan(CLng(CSng(totalTime.Ticks) / query.Count))
   End Function

   Function Count(query As IEnumerable(Of MainWindow.Matchup)) As Integer
      Return query.Count
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

