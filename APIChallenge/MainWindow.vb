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

   Private Enum SortMode
      WinRate
      LossRate
      Popularity
   End Enum

   Private Function GetWinRatesForAllChampions(Optional ByVal sortMode As SortMode = SortMode.WinRate) As IEnumerable(Of ChampHistory)
      Dim champHistoryList As New List(Of ChampHistory)
      For Each champ In APIHelper.Champions.Keys
         champHistoryList.Add(GetWinRateOfChamp(champ))
      Next

      Dim query As IEnumerable(Of ChampHistory)
      If sortMode = MainWindow.SortMode.WinRate Then
         query = From c In champHistoryList, names In APIHelper.Champions.Values
                     Where c.champID = names.Id
                     Order By c.GetWinRate() Descending, c.gamesPlayed Descending, c.champName Ascending
                     Select c
      ElseIf sortMode = MainWindow.SortMode.LossRate Then
         query = From c In champHistoryList, names In APIHelper.Champions.Values
                     Where c.champID = names.Id
                     Order By c.GetWinRate() Ascending, c.gamesPlayed Descending, c.champName Ascending
                     Select c
      Else
         query = From c In champHistoryList, names In APIHelper.Champions.Values
                     Where c.champID = names.Id
                     Order By c.gamesPlayed Descending, c.GetWinRate Descending, c.champName Ascending
                     Select c
      End If
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
         _WinRate = CSng(gamesWon) / If(gamesPlayed > 0, gamesPlayed, 1)
      End Sub

      Public Overrides Function ToString() As String
         Return "Champion " & champName & " won " & gamesWon & "/" & gamesPlayed & " (" & GetWinRate() & ")"
      End Function
   End Class

   Public Function GetWinRateOfChamp(ByVal champID As Integer) As ChampHistory
      If DataCache.GetMatchupDataFor(APIHelper.GetChampName(champID)) Is Nothing Then
         Return New ChampHistory(champID, 0, 0)
      End If
      ' Count how many matches the champion won in
      Dim winNum = (From matches In DataCache.GetMatchupDataFor(APIHelper.GetChampName(champID))
                    Where matches.WonLane
                    Select matches).Count()
      ' Count how many matches the champion was in
      Dim totalGames = DataCache.GetMatchupDataFor(APIHelper.GetChampName(champID)).Count
      Return New ChampHistory(champID, winNum, totalGames)
   End Function

   Private Sub MainWindow_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
      '    Console.WriteLine(Me.Size)
   End Sub

   Private Sub DisplayGeneralRates()
      Dim allWinRates = GetWinRatesForAllChampions(SortMode.WinRate)

      WinRateFlowLayoutPanel.Controls.Clear()
      TopWinRateLabel.Parent = WinRateFlowLayoutPanel
      For i = 0 To Math.Min(9, allWinRates.Count - 1)
         Dim wR As New WinRateMatchup(allWinRates(i).champID, 0, allWinRates(i).gamesWon, allWinRates(i).gamesPlayed)
         WinRateFlowLayoutPanel.Controls.Add(New WinRateUserControl(wR))
      Next

      Dim lossRates = GetWinRatesForAllChampions(SortMode.LossRate)

      LossRateFlowLayoutPanel.Controls.Clear()
      LowestWinRateLabel.Parent = LossRateFlowLayoutPanel
      For i = 0 To Math.Min(9, lossRates.Count - 1)
         Dim lR As New WinRateMatchup(lossRates(i).champID, 0, lossRates(i).gamesWon, lossRates(i).gamesPlayed)
         LossRateFlowLayoutPanel.Controls.Add(New WinRateUserControl(lR))
      Next

      Dim popular = GetWinRatesForAllChampions(SortMode.Popularity)

      MatchupFlowLayoutPanel.Controls.Clear()
      PopularChampionsLabel.Parent = MatchupFlowLayoutPanel
      For i = 0 To Math.Min(9, lossRates.Count - 1)
         Dim lR As New WinRateMatchup(popular(i).champID, 0, popular(i).gamesWon, popular(i).gamesPlayed)
         MatchupFlowLayoutPanel.Controls.Add(New WinRateUserControl(lR))
      Next
   End Sub

   Private Sub ImageComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FirstImageComboBox.SelectedIndexChanged
      If FirstImageComboBox.Text = "" Or FirstImageComboBox.Text = " " Then
         DisplayGeneralRates()
         Return
      End If

      Dim CurrentMatchups = DataCache.GetMatchupDataFor(FirstImageComboBox.Text)
      If CurrentMatchups Is Nothing Then
         ClearChampionData()
         Return
      End If

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

      Dim lostRates = WinRateMatchup.GetWinRateDataFor(APIHelper.GetChampID(FirstImageComboBox.Text), CurrentMatchups, False)

      LossRateFlowLayoutPanel.Controls.Clear()
      LowestWinRateLabel.Parent = LossRateFlowLayoutPanel
      For i = 0 To Math.Min(9, lostRates.Count - 1)
         LossRateFlowLayoutPanel.Controls.Add(New WinRateUserControl(lostRates(i)))
      Next

      ClearMatchupData()

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

   Private Sub ImageComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SecondImageComboBox.SelectedIndexChanged
      If SecondImageComboBox.Text = "" Or SecondImageComboBox.Text = " " Then
         ClearMatchupData(False)
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

      MatchupFlowLayoutPanel.Controls.Clear()
      For Each m In q
         Dim matchupUC As New MatchupUserControl(m)
         MatchupFlowLayoutPanel.Controls.Add(matchupUC)
         matchupUC.Location = New Point(12, 100 + MatchupFlowLayoutPanel.Controls.Count * (150 + 10))
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

   Private Sub ClearMatchupData(Optional ByVal clearSecondList As Boolean = True)
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

      MatchupFlowLayoutPanel.Controls.Clear()
   End Sub

   ' For champions who haven't loaded in a match yet
   Private Sub ClearChampionData()
      ClearMatchupData()

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
      ClearChampionData()
      FirstImageComboBox.SelectedIndex = 0
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