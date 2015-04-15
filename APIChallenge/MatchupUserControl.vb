Imports System.ComponentModel

Public Class MatchupUserControl
   Private Matchup As Matchup

   Public Property BorderColor As Color = Color.Red

   Sub New(ByVal givenMatchup As Matchup)
      InitializeComponent()
      Dim initialRightA As Integer = ChampionLabel.Right
      Dim initialRightB As Integer = AllyLabel.Right

      Me.Matchup = givenMatchup

      ChampionLabel.Text = APIHelper.GetChampName(Matchup.ChampionID)
      ChampPictureBox.Image = RetrieveCache(Of StaticCache).Images(Matchup.ChampionID)
      'Readjust for short names
      ChampionLabel.Left = ChampionLabel.Left + (initialRightA - ChampionLabel.Right)

      EnemyLabel.Text = APIHelper.GetChampName(Matchup.EnemyChampionID)
      EnemyPictureBox.Image = RetrieveCache(Of StaticCache).Images(Matchup.EnemyChampionID)

      If Matchup.AllyChampionID > 0 Then
         AllyLabel.Text = APIHelper.GetChampName(Matchup.AllyChampionID)
         AllyPictureBox.Image = RetrieveCache(Of StaticCache).Images(Matchup.AllyChampionID)
         'Readjust for short names
         AllyLabel.Left = AllyLabel.Left + (initialRightB - AllyLabel.Right)
      Else
         AllyLabel.Text = ""
         ChampionLabel.Top += 32
         ChampPictureBox.Top += 32
      End If

      If Matchup.EnemyChampionID2 > 0 Then
         EnemyLabel2.Text = APIHelper.GetChampName(Matchup.EnemyChampionID2)
         EnemyPictureBox2.Image = CacheManager.RetrieveCache(Of StaticCache).Images(Matchup.EnemyChampionID2)
      Else
         EnemyLabel2.Text = ""
         EnemyLabel.Top += 32
         EnemyPictureBox.Top += 32
      End If

      ' Fix uneven sides
      If Matchup.AllyChampionID > 0 AndAlso Matchup.EnemyChampionID2 = 0 Then
         AllyLabel.Left += (ChampionLabel.Right - AllyLabel.Right)
         AllyPictureBox.Left = ChampPictureBox.Left
      ElseIf Matchup.AllyChampionID = 0 AndAlso Matchup.EnemyChampionID2 > 0 Then
         EnemyLabel2.Left = EnemyLabel.Left
         EnemyPictureBox2.Left = EnemyPictureBox.Left
      End If

      If Matchup.WonLane Then
         Me.BorderColor = Color.Green
      End If

      Label2.Text = Matchup.Lane.ToString
   End Sub

   Protected Overrides Sub OnPaint(e As PaintEventArgs)
      MyBase.OnPaint(e)

      ControlPaint.DrawBorder(e.Graphics, Me.ClientRectangle, BorderColor, ButtonBorderStyle.Solid)
   End Sub
End Class
