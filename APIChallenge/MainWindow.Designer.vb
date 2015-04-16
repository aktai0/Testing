<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainWindow
   Inherits System.Windows.Forms.Form

   'Form overrides dispose to clean up the component list.
   <System.Diagnostics.DebuggerNonUserCode()> _
   Protected Overrides Sub Dispose(ByVal disposing As Boolean)
      Try
         If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
         End If
      Finally
         MyBase.Dispose(disposing)
      End Try
   End Sub

   'Required by the Windows Form Designer
   Private components As System.ComponentModel.IContainer

   'NOTE: The following procedure is required by the Windows Form Designer
   'It can be modified using the Windows Form Designer.  
   'Do not modify it using the code editor.
   <System.Diagnostics.DebuggerStepThrough()> _
   Private Sub InitializeComponent()
      Me.components = New System.ComponentModel.Container()
      Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainWindow))
      Me._ChampionImageList2 = New System.Windows.Forms.ImageList(Me.components)
      Me._ChampionImageList1 = New System.Windows.Forms.ImageList(Me.components)
      Me.SecondImageComboBox = New ImageComboBox.ImageComboBox()
      Me.FirstImageComboBox = New ImageComboBox.ImageComboBox()
      Me.PopularChampionsLabel = New System.Windows.Forms.Label()
      Me.AllMatchupsLabel = New System.Windows.Forms.Label()
      Me.SelectorPanel = New System.Windows.Forms.Panel()
      Me.ClearButton = New System.Windows.Forms.Button()
      Me.ShowMatchesButton = New System.Windows.Forms.Button()
      Me.LeftSidePanel = New System.Windows.Forms.Panel()
      Me.EnemyLabel = New System.Windows.Forms.Label()
      Me.EnemyPictureBox = New System.Windows.Forms.PictureBox()
      Me.ChampionLabel = New System.Windows.Forms.Label()
      Me.ChampionPictureBox = New System.Windows.Forms.PictureBox()
      Me.VSLabel = New System.Windows.Forms.Label()
      Me.WinRateLabel = New System.Windows.Forms.Label()
      Me.ChampVsChampWinRatePanel = New System.Windows.Forms.Panel()
      Me.ChampWinRatePanel = New System.Windows.Forms.Panel()
      Me.ChampionPictureBoxInitial = New System.Windows.Forms.PictureBox()
      Me.ChampionLabelInitial = New System.Windows.Forms.Label()
      Me.WinRateLabelInitial = New System.Windows.Forms.Label()
      Me.WinRateFlowLayoutPanel = New System.Windows.Forms.FlowLayoutPanel()
      Me.TopWinRateLabel = New System.Windows.Forms.Label()
      Me.TopRightPanel = New System.Windows.Forms.Panel()
      Me.BottomRightPanel = New System.Windows.Forms.Panel()
      Me.LossRateFlowLayoutPanel = New System.Windows.Forms.FlowLayoutPanel()
      Me.LowestWinRateLabel = New System.Windows.Forms.Label()
      Me.PopularityFlowLayoutPanel = New System.Windows.Forms.FlowLayoutPanel()
      Me.SelectorPanel.SuspendLayout()
      Me.LeftSidePanel.SuspendLayout()
      CType(Me.EnemyPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(Me.ChampionPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.ChampVsChampWinRatePanel.SuspendLayout()
      Me.ChampWinRatePanel.SuspendLayout()
      CType(Me.ChampionPictureBoxInitial, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.WinRateFlowLayoutPanel.SuspendLayout()
      Me.TopRightPanel.SuspendLayout()
      Me.BottomRightPanel.SuspendLayout()
      Me.LossRateFlowLayoutPanel.SuspendLayout()
      Me.PopularityFlowLayoutPanel.SuspendLayout()
      Me.SuspendLayout()
      '
      '_ChampionImageList2
      '
      Me._ChampionImageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
      Me._ChampionImageList2.ImageSize = New System.Drawing.Size(60, 60)
      Me._ChampionImageList2.TransparentColor = System.Drawing.Color.Transparent
      '
      '_ChampionImageList1
      '
      Me._ChampionImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
      Me._ChampionImageList1.ImageSize = New System.Drawing.Size(60, 60)
      Me._ChampionImageList1.TransparentColor = System.Drawing.Color.Transparent
      '
      'SecondImageComboBox
      '
      Me.SecondImageComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
      Me.SecondImageComboBox.DropDownHeight = 720
      Me.SecondImageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
      Me.SecondImageComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
      Me.SecondImageComboBox.ImageList = Me._ChampionImageList2
      Me.SecondImageComboBox.Indent = 20
      Me.SecondImageComboBox.IntegralHeight = False
      Me.SecondImageComboBox.ItemHeight = 30
      Me.SecondImageComboBox.Location = New System.Drawing.Point(9, 87)
      Me.SecondImageComboBox.MaxDropDownItems = 12
      Me.SecondImageComboBox.Name = "SecondImageComboBox"
      Me.SecondImageComboBox.Size = New System.Drawing.Size(300, 36)
      Me.SecondImageComboBox.TabIndex = 3
      '
      'FirstImageComboBox
      '
      Me.FirstImageComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
      Me.FirstImageComboBox.DropDownHeight = 720
      Me.FirstImageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
      Me.FirstImageComboBox.DropDownWidth = 300
      Me.FirstImageComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
      Me.FirstImageComboBox.ImageList = Me._ChampionImageList1
      Me.FirstImageComboBox.Indent = 20
      Me.FirstImageComboBox.IntegralHeight = False
      Me.FirstImageComboBox.ItemHeight = 30
      Me.FirstImageComboBox.Location = New System.Drawing.Point(9, 45)
      Me.FirstImageComboBox.MaxDropDownItems = 12
      Me.FirstImageComboBox.Name = "FirstImageComboBox"
      Me.FirstImageComboBox.Size = New System.Drawing.Size(300, 36)
      Me.FirstImageComboBox.TabIndex = 2
      '
      'PopularChampionsLabel
      '
      Me.PopularChampionsLabel.Dock = System.Windows.Forms.DockStyle.Top
      Me.PopularChampionsLabel.Font = New System.Drawing.Font("Garamond", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.PopularChampionsLabel.Location = New System.Drawing.Point(3, 45)
      Me.PopularChampionsLabel.Name = "PopularChampionsLabel"
      Me.PopularChampionsLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes
      Me.PopularChampionsLabel.Size = New System.Drawing.Size(0, 45)
      Me.PopularChampionsLabel.TabIndex = 19
      Me.PopularChampionsLabel.Text = "Most Popular Champions"
      Me.PopularChampionsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
      '
      'AllMatchupsLabel
      '
      Me.AllMatchupsLabel.Dock = System.Windows.Forms.DockStyle.Top
      Me.AllMatchupsLabel.Font = New System.Drawing.Font("Garamond", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.AllMatchupsLabel.Location = New System.Drawing.Point(3, 0)
      Me.AllMatchupsLabel.Name = "AllMatchupsLabel"
      Me.AllMatchupsLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes
      Me.AllMatchupsLabel.Size = New System.Drawing.Size(0, 45)
      Me.AllMatchupsLabel.TabIndex = 20
      Me.AllMatchupsLabel.Text = "All Matchups"
      Me.AllMatchupsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
      '
      'SelectorPanel
      '
      Me.SelectorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
      Me.SelectorPanel.Controls.Add(Me.ClearButton)
      Me.SelectorPanel.Controls.Add(Me.ShowMatchesButton)
      Me.SelectorPanel.Controls.Add(Me.SecondImageComboBox)
      Me.SelectorPanel.Controls.Add(Me.FirstImageComboBox)
      Me.SelectorPanel.Dock = System.Windows.Forms.DockStyle.Top
      Me.SelectorPanel.Location = New System.Drawing.Point(0, 0)
      Me.SelectorPanel.Name = "SelectorPanel"
      Me.SelectorPanel.Size = New System.Drawing.Size(470, 129)
      Me.SelectorPanel.TabIndex = 22
      '
      'ClearButton
      '
      Me.ClearButton.Location = New System.Drawing.Point(315, 46)
      Me.ClearButton.Name = "ClearButton"
      Me.ClearButton.Size = New System.Drawing.Size(143, 35)
      Me.ClearButton.TabIndex = 4
      Me.ClearButton.Text = "Clear"
      Me.ClearButton.UseVisualStyleBackColor = True
      '
      'ShowMatchesButton
      '
      Me.ShowMatchesButton.Location = New System.Drawing.Point(9, 4)
      Me.ShowMatchesButton.Name = "ShowMatchesButton"
      Me.ShowMatchesButton.Size = New System.Drawing.Size(449, 35)
      Me.ShowMatchesButton.TabIndex = 1
      Me.ShowMatchesButton.Text = "Load Matches"
      Me.ShowMatchesButton.UseVisualStyleBackColor = True
      '
      'LeftSidePanel
      '
      Me.LeftSidePanel.Controls.Add(Me.PopularityFlowLayoutPanel)
      Me.LeftSidePanel.Controls.Add(Me.SelectorPanel)
      Me.LeftSidePanel.Dock = System.Windows.Forms.DockStyle.Left
      Me.LeftSidePanel.Location = New System.Drawing.Point(0, 0)
      Me.LeftSidePanel.Name = "LeftSidePanel"
      Me.LeftSidePanel.Size = New System.Drawing.Size(470, 714)
      Me.LeftSidePanel.TabIndex = 0
      '
      'EnemyLabel
      '
      Me.EnemyLabel.AutoSize = True
      Me.EnemyLabel.Font = New System.Drawing.Font("Garamond", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.EnemyLabel.Location = New System.Drawing.Point(324, 29)
      Me.EnemyLabel.Name = "EnemyLabel"
      Me.EnemyLabel.Size = New System.Drawing.Size(0, 29)
      Me.EnemyLabel.TabIndex = 12
      Me.EnemyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
      '
      'EnemyPictureBox
      '
      Me.EnemyPictureBox.Location = New System.Drawing.Point(258, 14)
      Me.EnemyPictureBox.Name = "EnemyPictureBox"
      Me.EnemyPictureBox.Size = New System.Drawing.Size(60, 60)
      Me.EnemyPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
      Me.EnemyPictureBox.TabIndex = 11
      Me.EnemyPictureBox.TabStop = False
      '
      'ChampionLabel
      '
      Me.ChampionLabel.Font = New System.Drawing.Font("Garamond", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.ChampionLabel.Location = New System.Drawing.Point(5, 29)
      Me.ChampionLabel.Name = "ChampionLabel"
      Me.ChampionLabel.Size = New System.Drawing.Size(144, 29)
      Me.ChampionLabel.TabIndex = 10
      Me.ChampionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'ChampionPictureBox
      '
      Me.ChampionPictureBox.Location = New System.Drawing.Point(153, 14)
      Me.ChampionPictureBox.Name = "ChampionPictureBox"
      Me.ChampionPictureBox.Size = New System.Drawing.Size(60, 60)
      Me.ChampionPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
      Me.ChampionPictureBox.TabIndex = 9
      Me.ChampionPictureBox.TabStop = False
      '
      'VSLabel
      '
      Me.VSLabel.AutoSize = True
      Me.VSLabel.Font = New System.Drawing.Font("Algerian", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.VSLabel.Location = New System.Drawing.Point(215, 31)
      Me.VSLabel.Name = "VSLabel"
      Me.VSLabel.Size = New System.Drawing.Size(40, 26)
      Me.VSLabel.TabIndex = 13
      Me.VSLabel.Text = "VS"
      Me.VSLabel.Visible = False
      '
      'WinRateLabel
      '
      Me.WinRateLabel.Font = New System.Drawing.Font("Garamond", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.WinRateLabel.Location = New System.Drawing.Point(10, 77)
      Me.WinRateLabel.Name = "WinRateLabel"
      Me.WinRateLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes
      Me.WinRateLabel.Size = New System.Drawing.Size(447, 52)
      Me.WinRateLabel.TabIndex = 14
      Me.WinRateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
      '
      'ChampVsChampWinRatePanel
      '
      Me.ChampVsChampWinRatePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
      Me.ChampVsChampWinRatePanel.Controls.Add(Me.WinRateLabel)
      Me.ChampVsChampWinRatePanel.Controls.Add(Me.VSLabel)
      Me.ChampVsChampWinRatePanel.Controls.Add(Me.EnemyLabel)
      Me.ChampVsChampWinRatePanel.Controls.Add(Me.ChampionPictureBox)
      Me.ChampVsChampWinRatePanel.Controls.Add(Me.EnemyPictureBox)
      Me.ChampVsChampWinRatePanel.Controls.Add(Me.ChampionLabel)
      Me.ChampVsChampWinRatePanel.Dock = System.Windows.Forms.DockStyle.Fill
      Me.ChampVsChampWinRatePanel.Location = New System.Drawing.Point(470, 0)
      Me.ChampVsChampWinRatePanel.Name = "ChampVsChampWinRatePanel"
      Me.ChampVsChampWinRatePanel.Size = New System.Drawing.Size(470, 129)
      Me.ChampVsChampWinRatePanel.TabIndex = 15
      '
      'ChampWinRatePanel
      '
      Me.ChampWinRatePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
      Me.ChampWinRatePanel.Controls.Add(Me.ChampionPictureBoxInitial)
      Me.ChampWinRatePanel.Controls.Add(Me.ChampionLabelInitial)
      Me.ChampWinRatePanel.Controls.Add(Me.WinRateLabelInitial)
      Me.ChampWinRatePanel.Dock = System.Windows.Forms.DockStyle.Left
      Me.ChampWinRatePanel.Location = New System.Drawing.Point(0, 0)
      Me.ChampWinRatePanel.Name = "ChampWinRatePanel"
      Me.ChampWinRatePanel.Size = New System.Drawing.Size(470, 129)
      Me.ChampWinRatePanel.TabIndex = 16
      '
      'ChampionPictureBoxInitial
      '
      Me.ChampionPictureBoxInitial.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.ChampionPictureBoxInitial.Location = New System.Drawing.Point(243, 13)
      Me.ChampionPictureBoxInitial.Name = "ChampionPictureBoxInitial"
      Me.ChampionPictureBoxInitial.Size = New System.Drawing.Size(60, 60)
      Me.ChampionPictureBoxInitial.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
      Me.ChampionPictureBoxInitial.TabIndex = 15
      Me.ChampionPictureBoxInitial.TabStop = False
      '
      'ChampionLabelInitial
      '
      Me.ChampionLabelInitial.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.ChampionLabelInitial.Font = New System.Drawing.Font("Garamond", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.ChampionLabelInitial.Location = New System.Drawing.Point(24, 28)
      Me.ChampionLabelInitial.Name = "ChampionLabelInitial"
      Me.ChampionLabelInitial.Size = New System.Drawing.Size(215, 29)
      Me.ChampionLabelInitial.TabIndex = 16
      Me.ChampionLabelInitial.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'WinRateLabelInitial
      '
      Me.WinRateLabelInitial.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.WinRateLabelInitial.Font = New System.Drawing.Font("Garamond", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.WinRateLabelInitial.Location = New System.Drawing.Point(18, 76)
      Me.WinRateLabelInitial.Name = "WinRateLabelInitial"
      Me.WinRateLabelInitial.RightToLeft = System.Windows.Forms.RightToLeft.Yes
      Me.WinRateLabelInitial.Size = New System.Drawing.Size(432, 49)
      Me.WinRateLabelInitial.TabIndex = 17
      Me.WinRateLabelInitial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
      '
      'WinRateFlowLayoutPanel
      '
      Me.WinRateFlowLayoutPanel.AutoScroll = True
      Me.WinRateFlowLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
      Me.WinRateFlowLayoutPanel.Controls.Add(Me.TopWinRateLabel)
      Me.WinRateFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Left
      Me.WinRateFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
      Me.WinRateFlowLayoutPanel.Location = New System.Drawing.Point(0, 0)
      Me.WinRateFlowLayoutPanel.Name = "WinRateFlowLayoutPanel"
      Me.WinRateFlowLayoutPanel.Size = New System.Drawing.Size(470, 585)
      Me.WinRateFlowLayoutPanel.TabIndex = 17
      Me.WinRateFlowLayoutPanel.WrapContents = False
      '
      'TopWinRateLabel
      '
      Me.TopWinRateLabel.Dock = System.Windows.Forms.DockStyle.Top
      Me.TopWinRateLabel.Font = New System.Drawing.Font("Garamond", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.TopWinRateLabel.Location = New System.Drawing.Point(3, 0)
      Me.TopWinRateLabel.Name = "TopWinRateLabel"
      Me.TopWinRateLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes
      Me.TopWinRateLabel.Size = New System.Drawing.Size(0, 45)
      Me.TopWinRateLabel.TabIndex = 18
      Me.TopWinRateLabel.Text = "Top Win Rates"
      Me.TopWinRateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
      '
      'TopRightPanel
      '
      Me.TopRightPanel.Controls.Add(Me.ChampVsChampWinRatePanel)
      Me.TopRightPanel.Controls.Add(Me.ChampWinRatePanel)
      Me.TopRightPanel.Dock = System.Windows.Forms.DockStyle.Top
      Me.TopRightPanel.Location = New System.Drawing.Point(470, 0)
      Me.TopRightPanel.Name = "TopRightPanel"
      Me.TopRightPanel.Size = New System.Drawing.Size(940, 129)
      Me.TopRightPanel.TabIndex = 18
      '
      'BottomRightPanel
      '
      Me.BottomRightPanel.Controls.Add(Me.LossRateFlowLayoutPanel)
      Me.BottomRightPanel.Controls.Add(Me.WinRateFlowLayoutPanel)
      Me.BottomRightPanel.Dock = System.Windows.Forms.DockStyle.Fill
      Me.BottomRightPanel.Location = New System.Drawing.Point(470, 129)
      Me.BottomRightPanel.Name = "BottomRightPanel"
      Me.BottomRightPanel.Size = New System.Drawing.Size(940, 585)
      Me.BottomRightPanel.TabIndex = 19
      '
      'LossRateFlowLayoutPanel
      '
      Me.LossRateFlowLayoutPanel.AutoScroll = True
      Me.LossRateFlowLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
      Me.LossRateFlowLayoutPanel.Controls.Add(Me.LowestWinRateLabel)
      Me.LossRateFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
      Me.LossRateFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
      Me.LossRateFlowLayoutPanel.Location = New System.Drawing.Point(470, 0)
      Me.LossRateFlowLayoutPanel.Name = "LossRateFlowLayoutPanel"
      Me.LossRateFlowLayoutPanel.Size = New System.Drawing.Size(470, 585)
      Me.LossRateFlowLayoutPanel.TabIndex = 19
      Me.LossRateFlowLayoutPanel.WrapContents = False
      '
      'LowestWinRateLabel
      '
      Me.LowestWinRateLabel.Dock = System.Windows.Forms.DockStyle.Top
      Me.LowestWinRateLabel.Font = New System.Drawing.Font("Garamond", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.LowestWinRateLabel.Location = New System.Drawing.Point(3, 0)
      Me.LowestWinRateLabel.Name = "LowestWinRateLabel"
      Me.LowestWinRateLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes
      Me.LowestWinRateLabel.Size = New System.Drawing.Size(0, 45)
      Me.LowestWinRateLabel.TabIndex = 18
      Me.LowestWinRateLabel.Text = "Lowest Win Rates"
      Me.LowestWinRateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
      '
      'PopularityFlowLayoutPanel
      '
      Me.PopularityFlowLayoutPanel.AutoScroll = True
      Me.PopularityFlowLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
      Me.PopularityFlowLayoutPanel.Controls.Add(Me.AllMatchupsLabel)
      Me.PopularityFlowLayoutPanel.Controls.Add(Me.PopularChampionsLabel)
      Me.PopularityFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Left
      Me.PopularityFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
      Me.PopularityFlowLayoutPanel.Location = New System.Drawing.Point(0, 129)
      Me.PopularityFlowLayoutPanel.Name = "PopularityFlowLayoutPanel"
      Me.PopularityFlowLayoutPanel.Size = New System.Drawing.Size(470, 585)
      Me.PopularityFlowLayoutPanel.TabIndex = 21
      Me.PopularityFlowLayoutPanel.WrapContents = False
      '
      'MainWindow
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.ClientSize = New System.Drawing.Size(1410, 714)
      Me.Controls.Add(Me.BottomRightPanel)
      Me.Controls.Add(Me.TopRightPanel)
      Me.Controls.Add(Me.LeftSidePanel)
      Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
      Me.Name = "MainWindow"
      Me.Text = "Main Window"
      Me.SelectorPanel.ResumeLayout(False)
      Me.LeftSidePanel.ResumeLayout(False)
      CType(Me.EnemyPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
      CType(Me.ChampionPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
      Me.ChampVsChampWinRatePanel.ResumeLayout(False)
      Me.ChampVsChampWinRatePanel.PerformLayout()
      Me.ChampWinRatePanel.ResumeLayout(False)
      CType(Me.ChampionPictureBoxInitial, System.ComponentModel.ISupportInitialize).EndInit()
      Me.WinRateFlowLayoutPanel.ResumeLayout(False)
      Me.TopRightPanel.ResumeLayout(False)
      Me.BottomRightPanel.ResumeLayout(False)
      Me.LossRateFlowLayoutPanel.ResumeLayout(False)
      Me.PopularityFlowLayoutPanel.ResumeLayout(False)
      Me.ResumeLayout(False)

   End Sub
   Friend WithEvents _ChampionImageList1 As System.Windows.Forms.ImageList
   Friend WithEvents _ChampionImageList2 As System.Windows.Forms.ImageList
   Private WithEvents SecondImageComboBox As ImageComboBox.ImageComboBox
   Private WithEvents FirstImageComboBox As ImageComboBox.ImageComboBox
   Friend WithEvents SelectorPanel As System.Windows.Forms.Panel
   Friend WithEvents LeftSidePanel As System.Windows.Forms.Panel
   Friend WithEvents EnemyLabel As System.Windows.Forms.Label
   Friend WithEvents EnemyPictureBox As System.Windows.Forms.PictureBox
   Friend WithEvents ChampionLabel As System.Windows.Forms.Label
   Friend WithEvents ChampionPictureBox As System.Windows.Forms.PictureBox
   Friend WithEvents VSLabel As System.Windows.Forms.Label
   Friend WithEvents WinRateLabel As System.Windows.Forms.Label
   Friend WithEvents ChampVsChampWinRatePanel As System.Windows.Forms.Panel
   Friend WithEvents ChampWinRatePanel As System.Windows.Forms.Panel
   Friend WithEvents WinRateLabelInitial As System.Windows.Forms.Label
   Friend WithEvents ChampionPictureBoxInitial As System.Windows.Forms.PictureBox
   Friend WithEvents ChampionLabelInitial As System.Windows.Forms.Label
   Friend WithEvents WinRateFlowLayoutPanel As System.Windows.Forms.FlowLayoutPanel
   Friend WithEvents TopWinRateLabel As System.Windows.Forms.Label
   Friend WithEvents TopRightPanel As System.Windows.Forms.Panel
   Friend WithEvents BottomRightPanel As System.Windows.Forms.Panel
   Friend WithEvents LossRateFlowLayoutPanel As System.Windows.Forms.FlowLayoutPanel
   Friend WithEvents LowestWinRateLabel As System.Windows.Forms.Label
   Friend WithEvents ShowMatchesButton As System.Windows.Forms.Button
   Friend WithEvents ClearButton As System.Windows.Forms.Button
   Friend WithEvents PopularChampionsLabel As System.Windows.Forms.Label
   Friend WithEvents AllMatchupsLabel As System.Windows.Forms.Label
   Friend WithEvents PopularityFlowLayoutPanel As System.Windows.Forms.FlowLayoutPanel

End Class
