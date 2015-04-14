Imports RiotSharp

Public Class CacheSettingsWindow
   Dim WithEvents MatchCache As MatchCache
   Dim StaticCache As StaticCache
   Sub MyMatches_CountChanged()
      RefreshMatchListBox()
   End Sub

   Sub MyMatches_LoadChanged()
      RefreshMatchListBox()
   End Sub

   Private Sub CacheSettingsWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      MatchCache = CType(CacheManager.CacheList("Matches"), MatchCache)
      StaticCache = CType(CacheManager.CacheList("Static"), StaticCache)

      ' Handlers need to be added after the initial load, because the object is replaced.
      AddHandler MatchCache.MatchList.CountChanged, AddressOf Me.MyMatches_CountChanged
      AddHandler MatchCache.MatchList.LoadChanged, AddressOf Me.MyMatches_LoadChanged

      MatchCache.MatchList.ForceCountChangedRefresh()
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

   Private Sub CacheSettingsWindow_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
      e.Cancel = True
      Me.Hide()
   End Sub
End Class

