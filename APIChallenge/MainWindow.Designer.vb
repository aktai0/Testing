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
      Me.ChampionImageList2 = New System.Windows.Forms.ImageList(Me.components)
      Me.ChampionImageList1 = New System.Windows.Forms.ImageList(Me.components)
      Me.ImageComboBox2 = New ImageComboBox.ImageComboBox()
      Me.ImageComboBox1 = New ImageComboBox.ImageComboBox()
      Me.SettingsButton = New System.Windows.Forms.Button()
      Me.MatchupFlowLayoutPanel = New System.Windows.Forms.FlowLayoutPanel()
      Me.SelectorPanel = New System.Windows.Forms.Panel()
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
      Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
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
      Me.SuspendLayout()
      '
      'ChampionImageList2
      '
      Me.ChampionImageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
      Me.ChampionImageList2.ImageSize = New System.Drawing.Size(60, 60)
      Me.ChampionImageList2.TransparentColor = System.Drawing.Color.Transparent
      '
      'ChampionImageList1
      '
      Me.ChampionImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
      Me.ChampionImageList1.ImageSize = New System.Drawing.Size(60, 60)
      Me.ChampionImageList1.TransparentColor = System.Drawing.Color.Transparent
      '
      'ImageComboBox2
      '
      Me.ImageComboBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
      Me.ImageComboBox2.DropDownHeight = 720
      Me.ImageComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
      Me.ImageComboBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
      Me.ImageComboBox2.ImageList = Me.ChampionImageList2
      Me.ImageComboBox2.Indent = 20
      Me.ImageComboBox2.IntegralHeight = False
      Me.ImageComboBox2.ItemHeight = 30
      Me.ImageComboBox2.Location = New System.Drawing.Point(315, 8)
      Me.ImageComboBox2.MaxDropDownItems = 12
      Me.ImageComboBox2.Name = "ImageComboBox2"
      Me.ImageComboBox2.Size = New System.Drawing.Size(300, 36)
      Me.ImageComboBox2.TabIndex = 19
      '
      'ImageComboBox1
      '
      Me.ImageComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
      Me.ImageComboBox1.DropDownHeight = 720
      Me.ImageComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
      Me.ImageComboBox1.DropDownWidth = 300
      Me.ImageComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
      Me.ImageComboBox1.ImageList = Me.ChampionImageList1
      Me.ImageComboBox1.Indent = 20
      Me.ImageComboBox1.IntegralHeight = False
      Me.ImageComboBox1.ItemHeight = 30
      Me.ImageComboBox1.Location = New System.Drawing.Point(9, 8)
      Me.ImageComboBox1.MaxDropDownItems = 12
      Me.ImageComboBox1.Name = "ImageComboBox1"
      Me.ImageComboBox1.Size = New System.Drawing.Size(300, 36)
      Me.ImageComboBox1.TabIndex = 18
      '
      'SettingsButton
      '
      Me.SettingsButton.Dock = System.Windows.Forms.DockStyle.Right
      Me.SettingsButton.Location = New System.Drawing.Point(843, 0)
      Me.SettingsButton.Name = "SettingsButton"
      Me.SettingsButton.Size = New System.Drawing.Size(97, 129)
      Me.SettingsButton.TabIndex = 20
      Me.SettingsButton.Text = "Settings"
      Me.SettingsButton.UseVisualStyleBackColor = True
      '
      'MatchupFlowLayoutPanel
      '
      Me.MatchupFlowLayoutPanel.AutoScroll = True
      Me.MatchupFlowLayoutPanel.AutoSize = True
      Me.MatchupFlowLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
      Me.MatchupFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
      Me.MatchupFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
      Me.MatchupFlowLayoutPanel.Location = New System.Drawing.Point(0, 56)
      Me.MatchupFlowLayoutPanel.Name = "MatchupFlowLayoutPanel"
      Me.MatchupFlowLayoutPanel.Padding = New System.Windows.Forms.Padding(32, 20, 0, 0)
      Me.MatchupFlowLayoutPanel.Size = New System.Drawing.Size(629, 658)
      Me.MatchupFlowLayoutPanel.TabIndex = 21
      Me.MatchupFlowLayoutPanel.WrapContents = False
      '
      'SelectorPanel
      '
      Me.SelectorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
      Me.SelectorPanel.Controls.Add(Me.ImageComboBox2)
      Me.SelectorPanel.Controls.Add(Me.ImageComboBox1)
      Me.SelectorPanel.Dock = System.Windows.Forms.DockStyle.Top
      Me.SelectorPanel.Location = New System.Drawing.Point(0, 0)
      Me.SelectorPanel.Name = "SelectorPanel"
      Me.SelectorPanel.Size = New System.Drawing.Size(629, 56)
      Me.SelectorPanel.TabIndex = 22
      '
      'LeftSidePanel
      '
      Me.LeftSidePanel.Controls.Add(Me.MatchupFlowLayoutPanel)
      Me.LeftSidePanel.Controls.Add(Me.SelectorPanel)
      Me.LeftSidePanel.Dock = System.Windows.Forms.DockStyle.Left
      Me.LeftSidePanel.Location = New System.Drawing.Point(0, 0)
      Me.LeftSidePanel.Name = "LeftSidePanel"
      Me.LeftSidePanel.Size = New System.Drawing.Size(629, 714)
      Me.LeftSidePanel.TabIndex = 0
      '
      'EnemyLabel
      '
      Me.EnemyLabel.AutoSize = True
      Me.EnemyLabel.Font = New System.Drawing.Font("Garamond", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.EnemyLabel.Location = New System.Drawing.Point(348, 29)
      Me.EnemyLabel.Name = "EnemyLabel"
      Me.EnemyLabel.Size = New System.Drawing.Size(84, 29)
      Me.EnemyLabel.TabIndex = 12
      Me.EnemyLabel.Text = "Testing"
      Me.EnemyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
      '
      'EnemyPictureBox
      '
      Me.EnemyPictureBox.Location = New System.Drawing.Point(282, 14)
      Me.EnemyPictureBox.Name = "EnemyPictureBox"
      Me.EnemyPictureBox.Size = New System.Drawing.Size(60, 60)
      Me.EnemyPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
      Me.EnemyPictureBox.TabIndex = 11
      Me.EnemyPictureBox.TabStop = False
      '
      'ChampionLabel
      '
      Me.ChampionLabel.Font = New System.Drawing.Font("Garamond", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.ChampionLabel.Location = New System.Drawing.Point(18, 29)
      Me.ChampionLabel.Name = "ChampionLabel"
      Me.ChampionLabel.Size = New System.Drawing.Size(155, 29)
      Me.ChampionLabel.TabIndex = 10
      Me.ChampionLabel.Text = "Testing"
      Me.ChampionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'ChampionPictureBox
      '
      Me.ChampionPictureBox.Location = New System.Drawing.Point(177, 14)
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
      Me.VSLabel.Location = New System.Drawing.Point(239, 31)
      Me.VSLabel.Name = "VSLabel"
      Me.VSLabel.Size = New System.Drawing.Size(40, 26)
      Me.VSLabel.TabIndex = 13
      Me.VSLabel.Text = "VS"
      '
      'WinRateLabel
      '
      Me.WinRateLabel.Font = New System.Drawing.Font("Garamond", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.WinRateLabel.Location = New System.Drawing.Point(89, 73)
      Me.WinRateLabel.Name = "WinRateLabel"
      Me.WinRateLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes
      Me.WinRateLabel.Size = New System.Drawing.Size(343, 60)
      Me.WinRateLabel.TabIndex = 14
      Me.WinRateLabel.Text = "Testing"
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
      Me.ChampVsChampWinRatePanel.Location = New System.Drawing.Point(335, 0)
      Me.ChampVsChampWinRatePanel.Name = "ChampVsChampWinRatePanel"
      Me.ChampVsChampWinRatePanel.Size = New System.Drawing.Size(605, 129)
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
      Me.ChampWinRatePanel.Size = New System.Drawing.Size(335, 129)
      Me.ChampWinRatePanel.TabIndex = 16
      '
      'ChampionPictureBoxInitial
      '
      Me.ChampionPictureBoxInitial.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.ChampionPictureBoxInitial.Location = New System.Drawing.Point(173, 13)
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
      Me.ChampionLabelInitial.Location = New System.Drawing.Point(-46, 28)
      Me.ChampionLabelInitial.Name = "ChampionLabelInitial"
      Me.ChampionLabelInitial.Size = New System.Drawing.Size(215, 29)
      Me.ChampionLabelInitial.TabIndex = 16
      Me.ChampionLabelInitial.Text = "Testing"
      Me.ChampionLabelInitial.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'WinRateLabelInitial
      '
      Me.WinRateLabelInitial.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.WinRateLabelInitial.Font = New System.Drawing.Font("Garamond", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.WinRateLabelInitial.Location = New System.Drawing.Point(5, 65)
      Me.WinRateLabelInitial.Name = "WinRateLabelInitial"
      Me.WinRateLabelInitial.RightToLeft = System.Windows.Forms.RightToLeft.Yes
      Me.WinRateLabelInitial.Size = New System.Drawing.Size(325, 60)
      Me.WinRateLabelInitial.TabIndex = 17
      Me.WinRateLabelInitial.Text = "Testing"
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
      Me.TopRightPanel.Controls.Add(Me.SettingsButton)
      Me.TopRightPanel.Controls.Add(Me.ChampVsChampWinRatePanel)
      Me.TopRightPanel.Controls.Add(Me.ChampWinRatePanel)
      Me.TopRightPanel.Dock = System.Windows.Forms.DockStyle.Top
      Me.TopRightPanel.Location = New System.Drawing.Point(629, 0)
      Me.TopRightPanel.Name = "TopRightPanel"
      Me.TopRightPanel.Size = New System.Drawing.Size(940, 129)
      Me.TopRightPanel.TabIndex = 18
      '
      'BottomRightPanel
      '
      Me.BottomRightPanel.Controls.Add(Me.LossRateFlowLayoutPanel)
      Me.BottomRightPanel.Controls.Add(Me.WinRateFlowLayoutPanel)
      Me.BottomRightPanel.Dock = System.Windows.Forms.DockStyle.Fill
      Me.BottomRightPanel.Location = New System.Drawing.Point(629, 129)
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
      'Timer1
      '
      Me.Timer1.Enabled = True
      '
      'MainWindow
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.ClientSize = New System.Drawing.Size(1569, 714)
      Me.Controls.Add(Me.BottomRightPanel)
      Me.Controls.Add(Me.TopRightPanel)
      Me.Controls.Add(Me.LeftSidePanel)
      Me.Name = "MainWindow"
      Me.Text = "Main Window"
      Me.SelectorPanel.ResumeLayout(False)
      Me.LeftSidePanel.ResumeLayout(False)
      Me.LeftSidePanel.PerformLayout()
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
      Me.ResumeLayout(False)

   End Sub
   Friend WithEvents ChampionImageList1 As System.Windows.Forms.ImageList
   Friend WithEvents ChampionImageList2 As System.Windows.Forms.ImageList
   Private WithEvents ImageComboBox2 As ImageComboBox.ImageComboBox
   Private WithEvents ImageComboBox1 As ImageComboBox.ImageComboBox
   Friend WithEvents SettingsButton As System.Windows.Forms.Button
   Friend WithEvents MatchupFlowLayoutPanel As System.Windows.Forms.FlowLayoutPanel
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
   Friend WithEvents Timer1 As System.Windows.Forms.Timer

End Class
