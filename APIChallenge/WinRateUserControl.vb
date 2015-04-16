Public Class WinRateUserControl

   Private WinRateMatchup As WinRateMatchup

   Sub New(ByVal givenWinRateMatchup As WinRateMatchup, Optional ByVal ShowPercentPlayed As Boolean = False)
      InitializeComponent()

      WinRateMatchup = givenWinRateMatchup

      ChampionLabel.Text = APIHelper.GetChampName(WinRateMatchup.ChampionID)

      If WinRateMatchup.EnemyChampionID > 0 Then
         ChampionPictureBox.Image = RetrieveCache(Of StaticCache).Images(WinRateMatchup.ChampionID)
         EnemyLabel.Text = APIHelper.GetChampName(WinRateMatchup.EnemyChampionID)
         EnemyPictureBox.Image = RetrieveCache(Of StaticCache).Images(WinRateMatchup.EnemyChampionID)
         VSLabel.Visible = True
      Else
         EnemyPictureBox.Image = RetrieveCache(Of StaticCache).Images(WinRateMatchup.ChampionID)
         EnemyLabel.Text = Nothing
         ChampionPictureBox.Image = Nothing
         VSLabel.Visible = False

         EnemyPictureBox.Left -= 25
         ChampionLabel.Left = 59
      End If

      WinRateLabel.Text = "Win Rate: " & String.Format("{0:0.00}%", WinRateMatchup.WinRate * 100) & " (from " & WinRateMatchup.TotalGames & " game" & If(WinRateMatchup.TotalGames > 1, "s", "") & ")"
      If WinRateMatchup.EnemyChampionID = 0 Then
         PlayRateLabel.Text = "Played in " & String.Format("{0:0.00}%", WinRateMatchup.TotalGames * 100 / RetrieveCache(Of MatchIDCache).TotalMatchesLoaded) & " (" & WinRateMatchup.TotalGames & "/" & RetrieveCache(Of MatchIDCache).TotalMatchesLoaded & ") matches"
      Else
         PlayRateLabel.Text = "Played in " & String.Format("{0:0.00}%", WinRateMatchup.TotalGames * 100 / RetrieveCache(Of DataCache).GetMatchupDataFor(APIHelper.GetChampName(WinRateMatchup.ChampionID)).Count) & " (" & WinRateMatchup.TotalGames & "/" & RetrieveCache(Of DataCache).GetMatchupDataFor(APIHelper.GetChampName(WinRateMatchup.ChampionID)).Count & ") matchups"
      End If
   End Sub

End Class
