Imports RiotSharp

Public Class MainWindow
   Dim WithEvents MatchCache As MatchCache
   Dim StaticCache As StaticCache
   Dim DataCache As DataCache

   Private Sub MainWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      CacheManager.Init()

      TimeHelper.UpdateLastRoundedDateTime()

      CacheManager.LoadAllCaches()
      APIHelper.API_INIT()

      MatchCache = CType(CacheManager.CacheList("Matches"), MatchCache)
      StaticCache = CType(CacheManager.CacheList("Static"), StaticCache)
      DataCache = CType(CacheManager.CacheList("Data"), DataCache)

      ImageComboBox1.Items.Clear()
      ChampionImageList1.Images.Clear()
      For Each c In APIHelper.Champions.Values
         ChampionImageList1.Images.Add(StaticCache.Images(c.Id))
         Dim comboBoxItem As New ImageComboBox.ImageComboBoxItem(ChampionImageList1.Images.Count - 1, c.Name, New Font("Microsoft Sans Serif", 18.0), 0)
         ImageComboBox1.Items.Add(comboBoxItem)
      Next
      ImageComboBox1.SelectedIndex = 23

      'Dim matchupUC As New MatchupUserControl(New Matchup(0, 17, 60, 33, 0, MatchEndpoint.Lane.Top, True, 100))
      'matchupUC.Parent = Me
      'matchupUC.Location = New Point(12, 150)
   End Sub

   Private Sub MainWindow_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
      CacheManager.StoreAllCaches()
   End Sub

   Public Function GetWinRatesForAllChampions() As IEnumerable(Of ChampHistory)
      Dim champHistoryList As New List(Of ChampHistory)
      For Each champ In APIHelper.Champions.Keys
         champHistoryList.Add(GetWinRateOfChamp(champ))
      Next

      Dim query = From c In champHistoryList, names In APIHelper.Champions.Values
                  Where c.champID = names.Id
                  Order By c.GetWinRate() Descending
                  Select c
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
         _WinRate = CSng(gamesWon) / gamesPlayed
      End Sub

      Public Overrides Function ToString() As String
         Return "Champion " & champName & " won " & gamesWon & "/" & gamesPlayed & " (" & GetWinRate() & ")"
      End Function
   End Class

   Public Function GetWinRateOfChamp(ByVal champID As Integer) As ChampHistory
      ' Count how many matches the champion won in
      Dim winNum = (From matches In MatchCache.MatchList
                    Where matches.GetMatchInfo.ChampWon(CInt(champID))
                    Select matches.GetMatchID).Count()
      ' Count how many matches the champion was in
      Dim totalGames = (From champs In MatchCache.MatchList
                        Where champs.GetMatchInfo.Participants.ContainsChamp(CInt(champID))
                        Select champs.GetMatchID).Count()
      Dim a = MatchCache.MatchList(0).GetMatchInfo.Participants(0).Timeline.Lane
      Return New ChampHistory(champID, winNum, totalGames)
   End Function

   Private Sub MainWindow_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
      '    Console.WriteLine(Me.Size)
   End Sub

   Dim CurrentMatchups As List(Of Matchup)


   Private Sub ImageComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ImageComboBox1.SelectedIndexChanged
      CurrentMatchups = Matchup.GetMatchupDataFor(ImageComboBox1.Text)
      Dim names = (From m In CurrentMatchups
                   Order By APIHelper.GetChampName(m.EnemyChampionID)
                   Select APIHelper.GetChampName(m.EnemyChampionID)).Distinct()

      Dim winRates = WinRateMatchup.GetWinRateDataFor(APIHelper.GetChampID(ImageComboBox1.Text), CurrentMatchups)

      WinRateFlowLayoutPanel.Controls.Clear()
      TopWinRateLabel.Parent = WinRateFlowLayoutPanel
      For i = 0 To Math.Min(9, winRates.Count - 1)
         WinRateFlowLayoutPanel.Controls.Add(New WinRateUserControl(winRates(i)))
      Next

      Dim lostRates = WinRateMatchup.GetWinRateDataFor(APIHelper.GetChampID(ImageComboBox1.Text), CurrentMatchups, False)

      LossRateFlowLayoutPanel.Controls.Clear()
      LowestWinRateLabel.Parent = LossRateFlowLayoutPanel
      For i = 0 To Math.Min(9, lostRates.Count - 1)
         LossRateFlowLayoutPanel.Controls.Add(New WinRateUserControl(lostRates(i)))
      Next

      'For Each m In CurrentMatchups
      '   Console.WriteLine(m.ToString)
      'Next

      ImageComboBox2.Items.Clear()
      ChampionImageList2.Images.Clear()
      For Each c In names
         ChampionImageList2.Images.Add(StaticCache.Images(APIHelper.GetChampID(c)))
         Dim comboBoxItem As New ImageComboBox.ImageComboBoxItem(ChampionImageList2.Images.Count - 1, c, New Font("Microsoft Sans Serif", 18.0), 0)
         ImageComboBox2.Items.Add(comboBoxItem)
      Next
      ImageComboBox2.SelectedIndex = -1

      ChampionLabelInitial.Text = ImageComboBox1.Text
      ChampionPictureBoxInitial.Image = StaticCache.Images(APIHelper.GetChampID(ImageComboBox1.Text))

      Dim wins = Aggregate m In CurrentMatchups
                 Where m.WonLane
                 Into Count()

      Dim games = CurrentMatchups.Count

      WinRateLabelInitial.Text = "Overall Win Rate: " & String.Format("{0:0.00}%", CSng(wins) * 100 / games)


      WinRateLabel.Text = ""

      ChampionLabel.Text = ""
      EnemyLabel.Text = ""
      ChampionPictureBox.Image = Nothing
      EnemyPictureBox.Image = Nothing
      VSLabel.Visible = False

      Console.WriteLine("Ready")
   End Sub

   Private MatchupUCList As New List(Of MatchupUserControl)

   Private Sub ImageComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ImageComboBox2.SelectedIndexChanged
      If ImageComboBox2.Text = Nothing Then
         Return
      End If

      Dim enemyChampID As Integer = APIHelper.GetChampID(CStr(ImageComboBox2.Text))
      Dim q = From m In CurrentMatchups
              Where m.ChampionID = APIHelper.GetChampID(ImageComboBox1.Text) AndAlso m.EnemyChampionID = enemyChampID
              Select m
      For Each m In q
         Console.WriteLine(m.ToString)
      Next

      Dim c = Aggregate m In q
              Where m.WonLane
              Into Count()

      Dim c2 = Aggregate m In q
              Into Count()

      For Each mUC In MatchupUCList
         mUC.Parent = Nothing
      Next
      MatchupUCList.Clear()

      For Each m In q
         Dim matchupUC As New MatchupUserControl(m)
         matchupUC.Parent = MatchupFlowLayoutPanel
         matchupUC.Location = New Point(12, 100 + MatchupUCList.Count * (150 + 10))

         MatchupUCList.Add(matchupUC)
      Next

      WinRateLabel.Text = "Win Rate: " & String.Format("{0:0.00}%", CSng(c) * 100 / c2)

      ChampionLabel.Text = ImageComboBox1.Text
      EnemyLabel.Text = ImageComboBox2.Text
      ChampionPictureBox.Image = StaticCache.Images(APIHelper.GetChampID(ImageComboBox1.Text))
      EnemyPictureBox.Image = StaticCache.Images(APIHelper.GetChampID(ImageComboBox2.Text))
      VSLabel.Visible = True
   End Sub

   Private Sub SettingsButton_Click(sender As Object, e As EventArgs) Handles SettingsButton.Click
      CacheSettingsWindow.Show()
   End Sub

   'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
   '   Dim uEncode As New System.Text.UnicodeEncoding
   '   Dim ClearString As String = "asdf"
   '   Dim bytClearString() As Byte = uEncode.GetBytes(ClearString)
   '   Dim sha As New  _
   '   System.Security.Cryptography.SHA256Managed()
   '   Dim hash() As Byte = sha.ComputeHash(bytClearString)
   '   Console.WriteLine(Convert.ToBase64String(hash))
   'End Sub
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