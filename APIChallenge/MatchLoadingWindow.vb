Public Class MatchLoadingWindow
   Private Const SLEEP_DELAY As Integer = 200
   Private Const NUM_MATCHIDS_TO_FETCH As Integer = 2000
   Private Const THREAD_COUNT As Integer = 32

   Private Sub SleepBreak(ByVal milliseconds As Integer)
      Dim i = 0
      While i < milliseconds And Not MatchLoaderBackgroundWorker.CancellationPending
         Threading.Thread.Sleep(SLEEP_DELAY)
         i += SLEEP_DELAY
      End While
   End Sub

   Private Sub GetMatchIDs(ByVal state As Object)
      If MatchIDCache.HasMatchIDsAvailable Then
         MatchIDCache.LoadMatchIDs()
      End If
   End Sub

   Private Sub DoWork_Fast(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles MatchLoaderBackgroundWorker.DoWork
      While True
         While MatchIDCache.NumMatchesPendingLoad < NUM_MATCHIDS_TO_FETCH
            MatchLoaderBackgroundWorker.ReportProgress(0, ProgressState.GoingToUpdateMatchIDs)
            GetMatchIDs(Nothing)
            MatchLoaderBackgroundWorker.ReportProgress(0, ProgressState.UpdatedMatchIDs)
            Threading.Thread.Sleep(SLEEP_DELAY)

            If MatchLoaderBackgroundWorker.CancellationPending Or MatchIDCache.ErrorPending Then
               e.Result = MatchIDCache.ErrorPending
               Return
            End If
         End While

         While MatchIDCache.HasMatchesPendingLoad
            MatchLoaderBackgroundWorker.ReportProgress(0, ProgressState.GoingToLoadMatch)
            Dim curTime = DateTime.Now
            MatchIDCache.LoadMatchesAsync(Math.Min(MatchIDCache.NumMatchesPendingLoad, THREAD_COUNT))
            Dim difference = DateTime.Now.Subtract(curTime)
            MatchLoaderBackgroundWorker.ReportProgress(0, ProgressState.LoadedMatch)

            If MatchLoaderBackgroundWorker.CancellationPending Or MatchIDCache.ErrorPending Then
               e.Result = MatchIDCache.ErrorPending
               Return
            End If
         End While
      End While
   End Sub

   Private Sub UpdateLabels()
      TotalMatchIDsLabel.Text = "" & MatchIDCache.TotalMatchIDs
      DateLabel.Text = MatchIDCache.NextURFBucketTimeToLoad.ToString
      UnloadedMatchesLabel.Text = "" & MatchIDCache.NumMatchesPendingLoad
      LoadedMatchesLabel.Text = "" & MatchIDCache.TotalMatchesLoaded
   End Sub


   Private Sub DoWork_Slow(sender As Object, e As System.ComponentModel.DoWorkEventArgs)
      Dim firstRun As Boolean = True
      While True
         Dim i = 0
         ' Load in all the matches we have
         While MatchIDCache.HasMatchesPendingLoad
            firstRun = False
            MatchLoaderBackgroundWorker.ReportProgress(0, ProgressState.GoingToLoadMatch)
            MatchIDCache.LoadFirstMatchIntoMatchups()
            MatchLoaderBackgroundWorker.ReportProgress(0, ProgressState.LoadedMatch)

            If MatchLoaderBackgroundWorker.CancellationPending Or MatchIDCache.ErrorPending Then
               e.Result = MatchIDCache.ErrorPending
               Return
            End If

            i += 1
            If i = APIHelper.SLOW_API_LIMIT Then
               i = 0
               MatchLoaderBackgroundWorker.ReportProgress(0, ProgressState.WaitingForAPI)
               SleepBreak(APIHelper.API_FULL_DELAY)

               If MatchLoaderBackgroundWorker.CancellationPending Then
                  e.Result = MatchIDCache.ErrorPending
                  Return
               End If
            Else
               Threading.Thread.Sleep(SLEEP_DELAY)
            End If
         End While

         If MatchLoaderBackgroundWorker.CancellationPending Then
            e.Result = MatchIDCache.ErrorPending
            Return
         End If

         If Not firstRun Then
            MatchLoaderBackgroundWorker.ReportProgress(0, ProgressState.WaitingForAPI)
            SleepBreak(APIHelper.API_FULL_DELAY)
         End If

         If MatchLoaderBackgroundWorker.CancellationPending Then
            e.Result = MatchIDCache.ErrorPending
            Return
         End If

         ' Load in 10 match ID buckets
         For i = 1 To APIHelper.SLOW_API_LIMIT
            If MatchIDCache.HasMatchIDsAvailable Then
               MatchLoaderBackgroundWorker.ReportProgress(0, ProgressState.GoingToUpdateMatchIDs)
               MatchIDCache.LoadMatchIDs()
               MatchLoaderBackgroundWorker.ReportProgress(0, ProgressState.UpdatedMatchIDs)

               If MatchLoaderBackgroundWorker.CancellationPending Or MatchIDCache.ErrorPending Then
                  e.Result = MatchIDCache.ErrorPending
                  Return
               End If

               Threading.Thread.Sleep(200)

               If MatchLoaderBackgroundWorker.CancellationPending Then
                  e.Result = MatchIDCache.ErrorPending
                  Return
               End If
            Else
               ' If we reach the last epoch time and we have no more Match IDs, then we end
               If Not MatchIDCache.HasMatchesPendingLoad Then
                  MatchLoaderBackgroundWorker.ReportProgress(0, ProgressState.NoMoreMatchIDs)
                  e.Result = False
                  Return
               End If
            End If
         Next

         MatchLoaderBackgroundWorker.ReportProgress(0, ProgressState.WaitingForAPI)
         SleepBreak(APIHelper.API_FULL_DELAY)

         If MatchLoaderBackgroundWorker.CancellationPending Then
            e.Result = MatchIDCache.ErrorPending
            Return
         End If
      End While
   End Sub

   Private Enum ProgressState
      GoingToUpdateMatchIDs
      UpdatedMatchIDs
      GoingToLoadMatch
      LoadedMatch
      WaitingForAPI
      NoMoreMatchIDs
   End Enum

   Private Loaded As Boolean = False
   Private WithEvents MatchIDCache As MatchIDCache
   Private Sub MatchLoadingWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      MatchIDCache = RetrieveCache(Of MatchIDCache)()

      TotalMatchIDsLabel.Text = "" & MatchIDCache.TotalMatchIDs
      DateLabel.Text = MatchIDCache.NextURFBucketTimeToLoad.ToString
      UnloadedMatchesLabel.Text = "" & MatchIDCache.NumMatchesPendingLoad
      LoadedMatchesLabel.Text = "" & MatchIDCache.TotalMatchesLoaded

      ' Default fast load on
      MatchIDCache.LoadFast = True

      Loaded = True
   End Sub

   Private Sub MatchLoaderBackgroundWorker_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles MatchLoaderBackgroundWorker.ProgressChanged
      If TypeOf (e.UserState) Is String Then
         Console.WriteLine(CStr(e.UserState))
         Return
      End If
      Select Case CType(e.UserState, ProgressState)
         Case ProgressState.GoingToUpdateMatchIDs
            StatusLabel.Text = "Loading in new URF match IDs using the API..."
         Case ProgressState.UpdatedMatchIDs
            StatusLabel.Text = "Loaded in new URF match IDs."
            TotalMatchIDsLabel.Text = "" & MatchIDCache.TotalMatchIDs
            UnloadedMatchesLabel.Text = "" & MatchIDCache.NumMatchesPendingLoad
            DateLabel.Text = MatchIDCache.NextURFBucketTimeToLoad.ToString
         Case ProgressState.GoingToLoadMatch
            StatusLabel.Text = "Loading a match using the API..."
         Case ProgressState.LoadedMatch
            StatusLabel.Text = "Loaded in a new match."
            UnloadedMatchesLabel.Text = "" & MatchIDCache.NumMatchesPendingLoad
            LoadedMatchesLabel.Text = "" & MatchIDCache.TotalMatchesLoaded
         Case ProgressState.WaitingForAPI
            StatusLabel.Text = "Waiting for API rate limit..."
            MainWindow.RefreshPanels()
         Case ProgressState.NoMoreMatchIDs
            StatusLabel.Text = "Reached end of URF buckets."
            TotalMatchIDsLabel.Text = "" & MatchIDCache.TotalMatchIDs
            UnloadedMatchesLabel.Text = "" & MatchIDCache.NumMatchesPendingLoad
            LoadedMatchesLabel.Text = "" & MatchIDCache.TotalMatchesLoaded
      End Select
   End Sub

   Private Sub StartButton_Click(sender As Object, e As EventArgs) Handles StartButton.Click
      MatchLoaderBackgroundWorker.RunWorkerAsync()
      SlowRadioButton.Enabled = False
      FastRadioButton.Enabled = False
      StartButton.Enabled = False
      StopButton.Enabled = True
   End Sub

   Private Sub StopButton_Click(sender As Object, e As EventArgs) Handles StopButton.Click
      If MatchLoaderBackgroundWorker.IsBusy Then
         MatchLoaderBackgroundWorker.CancelAsync()
         StopButton.Enabled = False
      End If
   End Sub

   Private Sub MatchLoaderBackgroundWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles MatchLoaderBackgroundWorker.RunWorkerCompleted
      If e.Result IsNot Nothing AndAlso TypeOf (e.Result) Is Boolean Then
         If CBool(e.Result) Then
            StatusLabel.Text = "Error with the API server. Stopping match loading for now."
         Else
            StatusLabel.Text = "Standby"
         End If
         StartButton.Enabled = True
         StopButton.Enabled = False

         SlowRadioButton.Enabled = True
         FastRadioButton.Enabled = True

         StoreAllCaches()
      End If
   End Sub

   Private Sub SlowRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles SlowRadioButton.CheckedChanged, FastRadioButton.CheckedChanged
      If Loaded Then
         MatchIDCache.LoadFast = FastRadioButton.Checked
         If FastRadioButton.Checked Then
            RemoveHandler MatchLoaderBackgroundWorker.DoWork, AddressOf DoWork_Slow
            AddHandler MatchLoaderBackgroundWorker.DoWork, AddressOf DoWork_Fast
         Else
            RemoveHandler MatchLoaderBackgroundWorker.DoWork, AddressOf DoWork_Fast
            AddHandler MatchLoaderBackgroundWorker.DoWork, AddressOf DoWork_Slow
         End If
      End If
   End Sub

   Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
      UpdateLabels()
   End Sub
End Class