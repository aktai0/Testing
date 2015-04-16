Public Class MatchLoadingWindow

   Private Sub SleepBreak(ByVal milliseconds As Integer)
      Dim i = 0
      While i < milliseconds And Not MatchLoaderBackgroundWorker.CancellationPending
         Threading.Thread.Sleep(200)
         i += 200
      End While
   End Sub

   Private Sub MatchLoaderBackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles MatchLoaderBackgroundWorker.DoWork
      While True
         Dim i = 0
         ' Load in all the matches we have
         While MatchIDCache.MatchesPendingLoad

            MatchLoaderBackgroundWorker.ReportProgress(0, ProgressState.GoingToLoadMatch)
            MatchIDCache.LoadFirstMatchIntoMatchups()
            MatchLoaderBackgroundWorker.ReportProgress(0, ProgressState.LoadedMatch)

            If MatchLoaderBackgroundWorker.CancellationPending Or MatchIDCache.ErrorPending Then
               e.Result = MatchIDCache.ErrorPending
               Return
            End If

            i += 1
            If i = 10 Then
               i = 0
               MatchLoaderBackgroundWorker.ReportProgress(0, ProgressState.WaitingForAPI)
               SleepBreak(APIHelper.API_FULL_DELAY)

               If MatchLoaderBackgroundWorker.CancellationPending Then
                  e.Result = MatchIDCache.ErrorPending
                  Return
               End If
            Else
               Threading.Thread.Sleep(100)
            End If
         End While

         If MatchLoaderBackgroundWorker.CancellationPending Then
            e.Result = MatchIDCache.ErrorPending
            Return
         End If

         MatchLoaderBackgroundWorker.ReportProgress(0, ProgressState.WaitingForAPI)
         SleepBreak(APIHelper.API_FULL_DELAY)

         If MatchLoaderBackgroundWorker.CancellationPending Then
            e.Result = MatchIDCache.ErrorPending
            Return
         End If

         For i = 0 To 9
            Console.WriteLine("Loading match IDs")
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
         Next

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
   End Enum

   Private WithEvents MatchIDCache As MatchIDCache
   Private Sub MatchLoadingWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      MatchIDCache = RetrieveCache(Of MatchIDCache)()

      MatchIDsLabel.Text = "Number of Match IDs: " & MatchIDCache.NumMatchesPendingLoad & "/" & MatchIDCache.MatchIDCount
      DateLabel.Text = MatchIDCache.NextURFBucketTimeToLoad.ToString
      LoadedMatchesLabel.Text = "Number of Loaded Matches: " & MatchIDCache.TotalMatchesLoaded
   End Sub

   Private Sub MatchLoaderBackgroundWorker_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles MatchLoaderBackgroundWorker.ProgressChanged
      Select Case CType(e.UserState, ProgressState)
         Case ProgressState.GoingToUpdateMatchIDs
            StatusLabel.Text = "Loading in new URF match IDs using the API..."
         Case ProgressState.UpdatedMatchIDs
            StatusLabel.Text = "Loaded in new URF match IDs."
            MatchIDsLabel.Text = "Number of Match IDs: " & MatchIDCache.TotalMatchesLoaded & "/" & MatchIDCache.MatchIDCount
            DateLabel.Text = MatchIDCache.NextURFBucketTimeToLoad.ToString
         Case ProgressState.GoingToLoadMatch
            StatusLabel.Text = "Loading a match using the API..."
         Case ProgressState.LoadedMatch
            StatusLabel.Text = "Loaded in a new match."
            LoadedMatchesLabel.Text = "Number of Loaded Matches: " & MatchIDCache.TotalMatchesLoaded
            MatchIDsLabel.Text = "Number of Match IDs: " & MatchIDCache.TotalMatchesLoaded & "/" & MatchIDCache.MatchIDCount
         Case ProgressState.WaitingForAPI
            StatusLabel.Text = "Waiting for API rate limit..."
      End Select
   End Sub

   Private Sub MatchIDsChanged()
      MatchIDsLabel.Text = "Number of Match IDs: " & MatchIDCache.TotalMatchesLoaded & "/" & MatchIDCache.MatchIDCount
      DateLabel.Text = MatchIDCache.NextURFBucketTimeToLoad.ToString
   End Sub

   Private Sub NewMatchesLoaded()
      LoadedMatchesLabel.Text = "Number of Loaded Matches: " & MatchIDCache.TotalMatchesLoaded
   End Sub

   Private Sub Button1_Click(sender As Object, e As EventArgs) Handles StartButton.Click
      MatchLoaderBackgroundWorker.RunWorkerAsync()
      StartButton.Enabled = False
      StopButton.Enabled = True
   End Sub

   Private Sub Button2_Click(sender As Object, e As EventArgs) Handles StopButton.Click
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

         StoreAllCaches()
      End If
   End Sub
End Class