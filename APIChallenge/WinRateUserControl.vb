Public Class WinRateUserControl

   Private WinRateMatchup As WinRateMatchup

   Sub New(ByVal givenWinRateMatchup As WinRateMatchup)
      InitializeComponent()

      WinRateMatchup = givenWinRateMatchup

      ChampionLabel.Text = APIHelper.GetChampName(WinRateMatchup.ChampionID)
      ChampionPictureBox.Image = RetrieveCache(Of StaticCache).Images(WinRateMatchup.ChampionID)

      If WinRateMatchup.EnemyChampionID > 0 Then
         EnemyLabel.Text = APIHelper.GetChampName(WinRateMatchup.EnemyChampionID)
         EnemyPictureBox.Image = RetrieveCache(Of StaticCache).Images(WinRateMatchup.EnemyChampionID)
         VSLabel.Visible = True
      Else
         EnemyLabel.Text = Nothing
         EnemyPictureBox.Image = Nothing
         VSLabel.Visible = False
      End If

      WinRateLabel.Text = "Win Rate: " & String.Format("{0:0.00}%", WinRateMatchup.WinRate * 100) & " (from " & WinRateMatchup.TotalGames & " game" & If(WinRateMatchup.TotalGames > 1, "s", "") & ")"
   End Sub

End Class
