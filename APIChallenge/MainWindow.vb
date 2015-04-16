Imports RiotSharp

Public Class MainWindow
   Dim StaticCache As StaticCache
   Dim DataCache As DataCache

   Private Sub MainWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      CacheManager.Init()

      TimeHelper.UpdateLastRoundedDateTime()

      CacheManager.LoadAllCaches()
      APIHelper.API_INIT()

      StaticCache = RetrieveCache(Of StaticCache)()
      DataCache = RetrieveCache(Of DataCache)()

      'Dim matchupUC As New MatchupUserControl(New Matchup(0, 17, 60, 33, 0, MatchEndpoint.Lane.Top, True, 100))
      'matchupUC.Parent = Me
      'matchupUC.Location = New Point(12, 150)

      FirstImageComboBox.Items.Clear()
      _ChampionImageList1.Images.Clear()
      Dim temp = New ImageComboBox.ImageComboBoxItem("", 0)
      temp.Text = " "
      FirstImageComboBox.Items.Add(temp)
      For Each c In APIHelper.Champions.Values
         _ChampionImageList1.Images.Add(StaticCache.Images(c.Id))
         Dim comboBoxItem As New ImageComboBox.ImageComboBoxItem(_ChampionImageList1.Images.Count - 1, c.Name, New Font("Microsoft Sans Serif", 18.0), 0)
         FirstImageComboBox.Items.Add(comboBoxItem)
      Next
      FirstImageComboBox.SelectedIndex = 0
   End Sub

   Private Sub MainWindow_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
      CacheManager.StoreAllCaches()
   End Sub

   Private Sub DisplayGeneralRates()
      ' Display Best Champions
      Dim allWinRates = Matchup.GetWinRatesForAllChampions(CheckBox1.Checked, ListSortMode.WinRate)

      WinRateFlowLayoutPanel.Controls.Clear()
      TopWinRateLabel.Parent = WinRateFlowLayoutPanel
      For i = 0 To Math.Min(9, allWinRates.Count - 1)
         Dim wR As New WinRateMatchup(allWinRates(i).champID, 0, allWinRates(i).gamesWon, allWinRates(i).gamesPlayed)
         WinRateFlowLayoutPanel.Controls.Add(New WinRateUserControl(wR))
      Next

      ' Display Worst Champions
      Dim lossRates = Matchup.GetWinRatesForAllChampions(CheckBox1.Checked, ListSortMode.LossRate)

      LossRateFlowLayoutPanel.Controls.Clear()
      LowestWinRateLabel.Parent = LossRateFlowLayoutPanel
      For i = 0 To Math.Min(9, lossRates.Count - 1)
         Dim lR As New WinRateMatchup(lossRates(i).champID, 0, lossRates(i).gamesWon, lossRates(i).gamesPlayed)
         LossRateFlowLayoutPanel.Controls.Add(New WinRateUserControl(lR))
      Next

      ' Display Most Popular Champions
      Dim popular = Matchup.GetWinRatesForAllChampions(CheckBox1.Checked, ListSortMode.Popularity)

      PopularityFlowLayoutPanel.Controls.Clear()
      PopularChampionsLabel.Parent = PopularityFlowLayoutPanel
      For i = 0 To Math.Min(9, lossRates.Count - 1)
         Dim lR As New WinRateMatchup(popular(i).champID, 0, popular(i).gamesWon, popular(i).gamesPlayed)
         PopularityFlowLayoutPanel.Controls.Add(New WinRateUserControl(lR))
      Next
   End Sub

   Private Sub DisplayChampionRates(ByVal champName As String)
      Dim CurrentMatchups = DataCache.GetMatchupDataFor(FirstImageComboBox.Text)
      ' If we don't have any data for that champion, clear everything.
      If CurrentMatchups Is Nothing Then
         ClearChampionDataPanels()
         Return
      End If

      ' Display Most Favorable Matchups
      Dim names = (From m In CurrentMatchups
                   Where m.EnemyChampionID <> 0
                   Order By APIHelper.GetChampName(m.EnemyChampionID)
                   Select APIHelper.GetChampName(m.EnemyChampionID)).Distinct()

      Dim winRates = WinRateMatchup.GetWinRateDataFor(APIHelper.GetChampID(FirstImageComboBox.Text), CurrentMatchups)

      WinRateFlowLayoutPanel.Controls.Clear()
      TopWinRateLabel.Parent = WinRateFlowLayoutPanel
      For i = 0 To Math.Min(9, winRates.Count - 1)
         WinRateFlowLayoutPanel.Controls.Add(New WinRateUserControl(winRates(i)))
      Next

      ' Display Least Favorable Matchups
      Dim lostRates = WinRateMatchup.GetWinRateDataFor(APIHelper.GetChampID(FirstImageComboBox.Text), CurrentMatchups, False)

      LossRateFlowLayoutPanel.Controls.Clear()
      LowestWinRateLabel.Parent = LossRateFlowLayoutPanel
      For i = 0 To Math.Min(9, lostRates.Count - 1)
         LossRateFlowLayoutPanel.Controls.Add(New WinRateUserControl(lostRates(i)))
      Next

      ' Display Most Popular Matchups
      ClearMatchupDataPanels()

      Dim temp = New ImageComboBox.ImageComboBoxItem("", 0)
      temp.Text = " "
      SecondImageComboBox.Items.Add(temp)
      For Each c In names
         _ChampionImageList2.Images.Add(StaticCache.Images(APIHelper.GetChampID(c)))
         Dim comboBoxItem As New ImageComboBox.ImageComboBoxItem(_ChampionImageList2.Images.Count - 1, c, New Font("Microsoft Sans Serif", 18.0), 0)
         SecondImageComboBox.Items.Add(comboBoxItem)
      Next

      ChampionLabelInitial.Text = FirstImageComboBox.Text
      ChampionPictureBoxInitial.Image = StaticCache.Images(APIHelper.GetChampID(FirstImageComboBox.Text))

      Dim wins = Aggregate m In CurrentMatchups
                 Where m.WonLane
                 Into Count()

      Dim games = CurrentMatchups.Count

      WinRateLabelInitial.Text = "Overall Win Rate: " & String.Format("{0:0.00}%", CSng(wins) * 100 / games) & " (from " & games & " games)"
   End Sub

   Private Sub ImageComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FirstImageComboBox.SelectedIndexChanged
      If FirstImageComboBox.Text = "" Or FirstImageComboBox.Text = " " Then
         DisplayGeneralRates()
         Return
      End If

      DisplayChampionRates(FirstImageComboBox.Text)
   End Sub

   Private Sub ImageComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SecondImageComboBox.SelectedIndexChanged
      If SecondImageComboBox.Text = "" Or SecondImageComboBox.Text = " " Then
         ClearMatchupDataPanels(False)
         Return
      End If
      If SecondImageComboBox.Text = Nothing Then
         Return
      End If
      Dim CurrentMatchups = DataCache.GetMatchupDataFor(FirstImageComboBox.Text)

      Dim enemyChampID As Integer = APIHelper.GetChampID(CStr(SecondImageComboBox.Text))
      Dim q = From m In CurrentMatchups
              Where m.ChampionID = APIHelper.GetChampID(FirstImageComboBox.Text) AndAlso m.EnemyChampionID = enemyChampID
              Select m
      For Each m In q
         Console.WriteLine(m.ToString)
      Next

      Dim c = Aggregate m In q
              Where m.WonLane
              Into Count()

      Dim c2 = Aggregate m In q
              Into Count()

      PopularityFlowLayoutPanel.Controls.Clear()
      For Each m In q
         Dim matchupUC As New MatchupUserControl(m)
         PopularityFlowLayoutPanel.Controls.Add(matchupUC)
         matchupUC.Location = New Point(12, 100 + PopularityFlowLayoutPanel.Controls.Count * (150 + 10))
      Next

      WinRateLabel.Text = "Win Rate: " & String.Format("{0:0.00}%", CSng(c) * 100 / c2) & " (from " & c2 & " games)"

      ChampionLabel.Text = FirstImageComboBox.Text
      EnemyLabel.Text = SecondImageComboBox.Text
      ChampionPictureBox.Image = StaticCache.Images(APIHelper.GetChampID(FirstImageComboBox.Text))
      EnemyPictureBox.Image = StaticCache.Images(APIHelper.GetChampID(SecondImageComboBox.Text))
      VSLabel.Visible = True
   End Sub

   Private Sub ShowMatchesButton_Click(sender As Object, e As EventArgs) Handles ShowMatchesButton.Click
      MatchLoadingWindow.Show()
   End Sub

   Private Sub ClearMatchupDataPanels(Optional ByVal clearSecondList As Boolean = True)
      WinRateLabel.Text = ""
      ChampionLabel.Text = ""
      EnemyLabel.Text = ""

      ChampionPictureBox.Image = Nothing
      EnemyPictureBox.Image = Nothing
      VSLabel.Visible = False

      If clearSecondList Then
         SecondImageComboBox.Items.Clear()
         _ChampionImageList2.Images.Clear()
      Else

      End If

      PopularityFlowLayoutPanel.Controls.Clear()
   End Sub

   ' For champions who haven't loaded in a match yet
   Private Sub ClearChampionDataPanels()
      ClearMatchupDataPanels()

      ChampionLabelInitial.Text = Nothing
      WinRateLabelInitial.Text = Nothing
      ChampionPictureBoxInitial.Image = Nothing

      WinRateFlowLayoutPanel.Controls.Clear()
      LossRateFlowLayoutPanel.Controls.Clear()
   End Sub

   Private Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
      If FirstImageComboBox.SelectedIndex = 0 Then
         DisplayGeneralRates()
         Return
      End If
      ClearChampionDataPanels()
      FirstImageComboBox.SelectedIndex = 0
   End Sub

   Private Sub RefreshPanels()
      If FirstImageComboBox.SelectedIndex = 0 Then
         DisplayGeneralRates()
         Return
      End If

      ImageComboBox1_SelectedIndexChanged(Nothing, Nothing)
      ImageComboBox2_SelectedIndexChanged(Nothing, Nothing)
   End Sub

   Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
      RefreshPanels()
   End Sub
End Class

Module LINQExtension
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