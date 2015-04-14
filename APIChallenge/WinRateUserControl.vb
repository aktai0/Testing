Public Class WinRateUserControl

   Private WinRateMatchup As WinRateMatchup

   Sub New(ByVal givenWinRateMatchup As WinRateMatchup)
      InitializeComponent()

      WinRateMatchup = givenWinRateMatchup

      ChampionLabel.Text = APIHelper.GetChampName(WinRateMatchup.ChampionID)
      ChampionPictureBox.Image = CacheManager.RetrieveCache(Of StaticCache).Images(WinRateMatchup.ChampionID)

      EnemyLabel.Text = APIHelper.GetChampName(WinRateMatchup.EnemyChampionID)
      EnemyPictureBox.Image = CacheManager.RetrieveCache(Of StaticCache).Images(WinRateMatchup.EnemyChampionID)

      WinRateLabel.Text = "Win Rate: " & String.Format("{0:0.00}", WinRateMatchup.WinRate * 100) & " (from " & WinRateMatchup.TotalGames & " game" & If(WinRateMatchup.TotalGames > 1, "s", "") & ")"
   End Sub

End Class
