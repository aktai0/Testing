Imports RiotSharp

Public Class MainWindow
   Dim WithEvents MyMatches As New MatchCache
   Sub MyMatches_CountChanged() Handles MyMatches.CountChanged
      RefreshMatchListBox()
   End Sub

   Sub MyMatches_LoadChanged() Handles MyMatches.LoadChanged
      RefreshMatchListBox()
      TabPage2.Enabled = MyMatches.LoadedIndex = MyMatches.Count - 1
   End Sub

   Sub RefreshMatchListBox()
      ListBox1.Items.Clear()
      For Each i As Match In MyMatches
         ListBox1.Items.Add(i.ToString())
      Next
      If MyMatches.LoadedIndex < MyMatches.Count - 1 Then
         ListBox1.SelectedIndex = MyMatches.LoadedIndex + 1
      End If
      CacheCountLabel.Text = "Total Matches in Cache: " & MyMatches.LoadedIndex + 1 & "/" & MyMatches.Count
   End Sub

#Region "Time Stuff"
   #End Region

   Private Sub MainWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      TimeHelper.UpdateLastRoundedDateTime()

      MatchCache.LoadCacheFileInto(MyMatches)
      MyMatches.ForceCountChangedRefresh()
      LastBucketTimeTextBox.Text = MyMatches.LastURFAPICall().ToLocalTime

      APIHelper.API_INIT()
   End Sub

   Private Sub MainWindow_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
      MatchCache.StoreCacheFile(MyMatches)
   End Sub

#Region "API Stuff"
#End Region

   ' This sub does not touch UI (for BackgroundWorker)
   Private Sub LoadInUrfMatches(ByVal epochTime As Integer)
      Dim matchIDs As List(Of Integer) = APIHelper.API_GET_URF_MATCHES(epochTime)

      If matchIDs.Count = 0 Then
         Console.WriteLine("No matches returned by API-challenge")
         Return
      End If

      For Each i As Integer In matchIDs
         MyMatches.QuietAdd(New Match(i))
      Next

      MatchCache.StoreCacheFile(MyMatches)
   End Sub

   Private Sub LoadInLatestUrfMatches()
      TimeHelper.UpdateLastRoundedDateTime()

      If Not TimeHelper.CanUpdate(MyMatches.LastURFAPICall) Then
         Console.WriteLine("Not calling API. We already did it for this 5 minutes")
         Return
      Else
      End If

      CurrentStatusLabel.Text = "Retrieving Recent URF Matches..."
      Dim oldCount As Integer = MyMatches.Count

      LoadInUrfMatches(TimeHelper.PreviousRoundedEpochTime)
      MyMatches.LastURFAPICall = TimeHelper.PreviousRoundedEpochDateTime
      LastBucketTimeTextBox.Text = MyMatches.LastURFAPICall().ToLocalTime
      MyMatches.Trim()
      MyMatches.ForceCountChangedRefresh()

      CurrentStatusLabel.Text = (MyMatches.Count - oldCount) & " matches added to cache"
   End Sub

   ' Populate the cache with at least 100 games from the given time (used with BackgroundWorker)
   Private Sub PopulateLatestUrfMatches(ByVal givenTime As DateTime)
      Dim timeIndex As DateTime = givenTime

      CurrentStatusLabel.Text = "Populating with URF Matches..."
      Dim oldCount As Integer = MyMatches.Count

      Dim progressNum As Integer = 0
      While MyMatches.Count < MatchCache.CACHE_LIMIT
         Console.WriteLine("Loading games from: " & timeIndex.ToString)
         LoadInUrfMatches(TimeHelper.DateTimeToEpoch(timeIndex))
         timeIndex = timeIndex.Subtract(New TimeSpan(0, 5, 0))
         progressNum += 1
         CacheBackgroundWorker.ReportProgress(CSng(progressNum) / 12 * 100)
         Threading.Thread.Sleep(2000)
      End While

      CacheBackgroundWorker.ReportProgress(100)
   End Sub

   Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
      LoadInLatestUrfMatches()
   End Sub

   Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
      MyMatches.ForceLoadChangedRefresh()
   End Sub

   Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
      APIChallengeTimer.Enabled = True
      Button4.Enabled = True
      Button3.Enabled = False
      If TimeHelper.CanUpdate(MyMatches.LastURFAPICall) Then
         LoadInLatestUrfMatches()
      End If
   End Sub

   Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
      APIChallengeTimer.Enabled = False
      Button4.Enabled = False
      Button3.Enabled = True
   End Sub

   Private Sub APIChallengeTimer_Tick(sender As Object, e As EventArgs) Handles APIChallengeTimer.Tick
      If TimeHelper.CanUpdate(MyMatches.LastURFAPICall) Then
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
      SyncLock MyMatches
         matchCount = MyMatches.Count - 1
         startingIndex = If(MyMatches.LoadedIndex <> -1, MyMatches.LoadedIndex, 0)
      End SyncLock

      For index = startingIndex To matchCount
         If MyMatches(index).IsLoaded() Then
            Continue For
         End If

         Dim info = APIHelper.API_GET_MATCH_INFO(MyMatches(index).GetMatchID)
         MyMatches(index).SetMatchInfo(info)

         ' If there was an error, try it again (repeat the current loop)
         If Not MyMatches(index).IsLoaded() Then
            index -= 1
         End If

         UpdatedIndex = index
         MatchLoadingBackgroundWorker.ReportProgress(CSng(index - startingIndex) / (matchCount - startingIndex) * 100)
         MatchCache.StoreCacheFile(MyMatches)
         ' One API call every 2 seconds
         System.Threading.Thread.Sleep(2000)
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
      MyMatches.LoadedIndex = UpdatedIndex
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
      MyMatches.ForceCountChangedRefresh()
      If e.ProgressPercentage = 100 Then
         Button6.Enabled = True
         MyMatches.Trim()
         CurrentStatusLabel.Text = "Cache filled to " & MatchCache.CACHE_LIMIT & " matches"
      End If
   End Sub

   Private Sub Button7_Click(sender As Object, e As EventArgs)
      MyMatches.Trim()
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
      Dim totalGames As Integer = MyMatches.Count
      Dim totalBlueSideWins As Integer = 0
      Dim totalWinnerFirstWins As Integer = 0
      Dim a As New Dictionary(Of Integer, Tuple)
      For Each m As Match In MyMatches
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
End Class

