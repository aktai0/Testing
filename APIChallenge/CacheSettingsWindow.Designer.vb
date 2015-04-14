<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CacheSettingsWindow
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
      Me.APIChallengeTimer = New System.Windows.Forms.Timer(Me.components)
      Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
      Me.CurrentStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
      Me._FillerLabel = New System.Windows.Forms.ToolStripStatusLabel()
      Me.ProgressBarLabel = New System.Windows.Forms.ToolStripStatusLabel()
      Me.StatusProgressBar = New System.Windows.Forms.ToolStripProgressBar()
      Me.CacheCountLabel = New System.Windows.Forms.ToolStripStatusLabel()
      Me.EpochTimer = New System.Windows.Forms.Timer(Me.components)
      Me.MatchLoadingBackgroundWorker = New System.ComponentModel.BackgroundWorker()
      Me.CacheBackgroundWorker = New System.ComponentModel.BackgroundWorker()
      Me.ChampionImageList1 = New System.Windows.Forms.ImageList(Me.components)
      Me.ChampionImageList2 = New System.Windows.Forms.ImageList(Me.components)
      Me.Button6 = New System.Windows.Forms.Button()
      Me.Button5 = New System.Windows.Forms.Button()
      Me.ListBox1 = New System.Windows.Forms.ListBox()
      Me.StatusStrip1.SuspendLayout()
      Me.SuspendLayout()
      '
      'APIChallengeTimer
      '
      Me.APIChallengeTimer.Interval = 30000
      '
      'StatusStrip1
      '
      Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CurrentStatusLabel, Me._FillerLabel, Me.ProgressBarLabel, Me.StatusProgressBar, Me.CacheCountLabel})
      Me.StatusStrip1.Location = New System.Drawing.Point(0, 438)
      Me.StatusStrip1.Name = "StatusStrip1"
      Me.StatusStrip1.Size = New System.Drawing.Size(1084, 25)
      Me.StatusStrip1.TabIndex = 22
      Me.StatusStrip1.Text = "StatusStrip1"
      '
      'CurrentStatusLabel
      '
      Me.CurrentStatusLabel.Name = "CurrentStatusLabel"
      Me.CurrentStatusLabel.Size = New System.Drawing.Size(59, 20)
      Me.CurrentStatusLabel.Text = "Loaded"
      '
      '_FillerLabel
      '
      Me._FillerLabel.Name = "_FillerLabel"
      Me._FillerLabel.Size = New System.Drawing.Size(731, 20)
      Me._FillerLabel.Spring = True
      '
      'ProgressBarLabel
      '
      Me.ProgressBarLabel.Name = "ProgressBarLabel"
      Me.ProgressBarLabel.Size = New System.Drawing.Size(0, 20)
      '
      'StatusProgressBar
      '
      Me.StatusProgressBar.Name = "StatusProgressBar"
      Me.StatusProgressBar.Size = New System.Drawing.Size(100, 19)
      '
      'CacheCountLabel
      '
      Me.CacheCountLabel.Name = "CacheCountLabel"
      Me.CacheCountLabel.Size = New System.Drawing.Size(177, 20)
      Me.CacheCountLabel.Text = "Total Matches In Cache: 0"
      '
      'EpochTimer
      '
      Me.EpochTimer.Enabled = True
      Me.EpochTimer.Interval = 50
      '
      'MatchLoadingBackgroundWorker
      '
      Me.MatchLoadingBackgroundWorker.WorkerReportsProgress = True
      '
      'CacheBackgroundWorker
      '
      Me.CacheBackgroundWorker.WorkerReportsProgress = True
      Me.CacheBackgroundWorker.WorkerSupportsCancellation = True
      '
      'ChampionImageList1
      '
      Me.ChampionImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
      Me.ChampionImageList1.ImageSize = New System.Drawing.Size(48, 48)
      Me.ChampionImageList1.TransparentColor = System.Drawing.Color.Transparent
      '
      'ChampionImageList2
      '
      Me.ChampionImageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
      Me.ChampionImageList2.ImageSize = New System.Drawing.Size(16, 16)
      Me.ChampionImageList2.TransparentColor = System.Drawing.Color.Transparent
      '
      'Button6
      '
      Me.Button6.Location = New System.Drawing.Point(104, 227)
      Me.Button6.Name = "Button6"
      Me.Button6.Size = New System.Drawing.Size(323, 59)
      Me.Button6.TabIndex = 25
      Me.Button6.Text = "Populate Game Cache With URF API"
      Me.Button6.UseVisualStyleBackColor = True
      '
      'Button5
      '
      Me.Button5.Location = New System.Drawing.Point(99, 198)
      Me.Button5.Name = "Button5"
      Me.Button5.Size = New System.Drawing.Size(163, 23)
      Me.Button5.TabIndex = 24
      Me.Button5.Text = "Start Loading Games"
      Me.Button5.UseVisualStyleBackColor = True
      '
      'ListBox1
      '
      Me.ListBox1.FormattingEnabled = True
      Me.ListBox1.ItemHeight = 16
      Me.ListBox1.Location = New System.Drawing.Point(638, 109)
      Me.ListBox1.Name = "ListBox1"
      Me.ListBox1.Size = New System.Drawing.Size(347, 244)
      Me.ListBox1.TabIndex = 23
      '
      'CacheSettingsWindow
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.ClientSize = New System.Drawing.Size(1084, 463)
      Me.Controls.Add(Me.Button6)
      Me.Controls.Add(Me.Button5)
      Me.Controls.Add(Me.ListBox1)
      Me.Controls.Add(Me.StatusStrip1)
      Me.Name = "CacheSettingsWindow"
      Me.Text = "FrontEndWindow"
      Me.StatusStrip1.ResumeLayout(False)
      Me.StatusStrip1.PerformLayout()
      Me.ResumeLayout(False)
      Me.PerformLayout()

   End Sub
   Friend WithEvents APIChallengeTimer As System.Windows.Forms.Timer
   Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
   Friend WithEvents CurrentStatusLabel As System.Windows.Forms.ToolStripStatusLabel
   Friend WithEvents _FillerLabel As System.Windows.Forms.ToolStripStatusLabel
   Friend WithEvents ProgressBarLabel As System.Windows.Forms.ToolStripStatusLabel
   Friend WithEvents StatusProgressBar As System.Windows.Forms.ToolStripProgressBar
   Friend WithEvents CacheCountLabel As System.Windows.Forms.ToolStripStatusLabel
   Friend WithEvents EpochTimer As System.Windows.Forms.Timer
   Friend WithEvents MatchLoadingBackgroundWorker As System.ComponentModel.BackgroundWorker
   Friend WithEvents CacheBackgroundWorker As System.ComponentModel.BackgroundWorker
   Friend WithEvents ChampionImageList1 As System.Windows.Forms.ImageList
   Friend WithEvents ChampionImageList2 As System.Windows.Forms.ImageList
   Friend WithEvents Button6 As System.Windows.Forms.Button
   Friend WithEvents Button5 As System.Windows.Forms.Button
   Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
End Class
