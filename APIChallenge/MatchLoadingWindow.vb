Public Class MatchLoadingWindow
   Private Const SLOW_DELAY As Integer = 200
   Private Const FAST_DELAY As Integer = 20


   Private Sub SleepBreak(ByVal milliseconds As Integer)
      Dim i = 0
      While i < milliseconds And Not MatchLoaderBackgroundWorker.CancellationPending
         Threading.Thread.Sleep(SLOW_DELAY)
         i += SLOW_DELAY
      End While
   End Sub

   Private Sub MatchLoaderBackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles MatchLoaderBackgroundWorker.DoWork
      Dim firstRun As Boolean = True
      While True
         Dim i = 0
         ' Load in all the matches we have
         While MatchIDCache.MatchesPendingLoad
            firstRun = False
            MatchLoaderBackgroundWorker.ReportProgress(0, ProgressState.GoingToLoadMatch)
            MatchIDCache.LoadFirstMatchIntoMatchups()
            MatchLoaderBackgroundWorker.ReportProgress(0, ProgressState.LoadedMatch)

            If MatchLoaderBackgroundWorker.CancellationPending Or MatchIDCache.ErrorPending Then
               e.Result = MatchIDCache.ErrorPending
               Return
            End If

            i += 1
            If i = If(FastRadioButton.Checked, APIHelper.FAST_API_LIMIT, APIHelper.SLOW_API_LIMIT) Then
               i = 0
               MatchLoaderBackgroundWorker.ReportProgress(0, ProgressState.WaitingForAPI)
               SleepBreak(APIHelper.API_FULL_DELAY)

               If MatchLoaderBackgroundWorker.CancellationPending Then
                  e.Result = MatchIDCache.ErrorPending
                  Return
               End If
            Else
               Threading.Thread.Sleep(If(FastRadioButton.Checked, FAST_DELAY, SLOW_DELAY))
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
         For i = 1 To APIHelper.SLOW_API_LIMIT * If(FastRadioButton.Checked, 5, 1)
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
               If Not MatchIDCache.MatchesPendingLoad Then
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

      TotalMatchIDsLabel.Text = "" & MatchIDCache.MatchIDCount
      DateLabel.Text = MatchIDCache.NextURFBucketTimeToLoad.ToString
      UnloadedMatchesLabel.Text = "" & MatchIDCache.NumMatchesPendingLoad
      LoadedMatchesLabel.Text = "" & MatchIDCache.TotalMatchesLoaded

      ' Default fast load on
      MatchIDCache.LoadFast = True

      Loaded = True
   End Sub

   Private Sub MatchLoaderBackgroundWorker_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles MatchLoaderBackgroundWorker.ProgressChanged
      Select Case CType(e.UserState, ProgressState)
         Case ProgressState.GoingToUpdateMatchIDs
            StatusLabel.Text = "Loading in new URF match IDs using the API..."
         Case ProgressState.UpdatedMatchIDs
            StatusLabel.Text = "Loaded in new URF match IDs."
            TotalMatchIDsLabel.Text = "" & MatchIDCache.MatchIDCount
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
            TotalMatchIDsLabel.Text = "" & MatchIDCache.MatchIDCount
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
      End If
   End Sub
End Class