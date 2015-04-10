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
      Me.LastBucketTimeTextBox = New System.Windows.Forms.TextBox()
      Me.Label1 = New System.Windows.Forms.Label()
      Me.Button1 = New System.Windows.Forms.Button()
      Me.Button2 = New System.Windows.Forms.Button()
      Me.Button3 = New System.Windows.Forms.Button()
      Me.Button4 = New System.Windows.Forms.Button()
      Me.APIChallengeTimer = New System.Windows.Forms.Timer(Me.components)
      Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
      Me.CurrentStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
      Me._FillerLabel = New System.Windows.Forms.ToolStripStatusLabel()
      Me.ProgressBarLabel = New System.Windows.Forms.ToolStripStatusLabel()
      Me.StatusProgressBar = New System.Windows.Forms.ToolStripProgressBar()
      Me.CacheCountLabel = New System.Windows.Forms.ToolStripStatusLabel()
      Me.ListBox1 = New System.Windows.Forms.ListBox()
      Me.EpochTimer = New System.Windows.Forms.Timer(Me.components)
      Me.Button5 = New System.Windows.Forms.Button()
      Me.MatchLoadingBackgroundWorker = New System.ComponentModel.BackgroundWorker()
      Me.Button6 = New System.Windows.Forms.Button()
      Me.CacheBackgroundWorker = New System.ComponentModel.BackgroundWorker()
      Me.TabControl1 = New System.Windows.Forms.TabControl()
      Me.TabPage1 = New System.Windows.Forms.TabPage()
      Me.TabPage2 = New System.Windows.Forms.TabPage()
      Me.TextBox1 = New System.Windows.Forms.TextBox()
      Me.Button7 = New System.Windows.Forms.Button()
      Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
      Me.StatusStrip1.SuspendLayout()
      Me.TabControl1.SuspendLayout()
      Me.TabPage1.SuspendLayout()
      Me.TabPage2.SuspendLayout()
      Me.SuspendLayout()
      '
      'LastBucketTimeTextBox
      '
      Me.LastBucketTimeTextBox.Location = New System.Drawing.Point(141, 6)
      Me.LastBucketTimeTextBox.Name = "LastBucketTimeTextBox"
      Me.LastBucketTimeTextBox.ReadOnly = True
      Me.LastBucketTimeTextBox.Size = New System.Drawing.Size(268, 22)
      Me.LastBucketTimeTextBox.TabIndex = 0
      '
      'Label1
      '
      Me.Label1.AutoSize = True
      Me.Label1.Location = New System.Drawing.Point(0, 9)
      Me.Label1.Name = "Label1"
      Me.Label1.Size = New System.Drawing.Size(125, 17)
      Me.Label1.TabIndex = 2
      Me.Label1.Text = "Last Bucket Time: "
      '
      'Button1
      '
      Me.Button1.Location = New System.Drawing.Point(380, 160)
      Me.Button1.Name = "Button1"
      Me.Button1.Size = New System.Drawing.Size(75, 23)
      Me.Button1.TabIndex = 10
      Me.Button1.Text = "Button1"
      Me.Button1.UseVisualStyleBackColor = True
      '
      'Button2
      '
      Me.Button2.Location = New System.Drawing.Point(461, 160)
      Me.Button2.Name = "Button2"
      Me.Button2.Size = New System.Drawing.Size(75, 23)
      Me.Button2.TabIndex = 11
      Me.Button2.Text = "Button2"
      Me.Button2.UseVisualStyleBackColor = True
      '
      'Button3
      '
      Me.Button3.Location = New System.Drawing.Point(2, 34)
      Me.Button3.Name = "Button3"
      Me.Button3.Size = New System.Drawing.Size(68, 23)
      Me.Button3.TabIndex = 13
      Me.Button3.Text = "Start"
      Me.Button3.UseVisualStyleBackColor = True
      '
      'Button4
      '
      Me.Button4.Enabled = False
      Me.Button4.Location = New System.Drawing.Point(76, 34)
      Me.Button4.Name = "Button4"
      Me.Button4.Size = New System.Drawing.Size(68, 23)
      Me.Button4.TabIndex = 14
      Me.Button4.Text = "Stop"
      Me.Button4.UseVisualStyleBackColor = True
      '
      'APIChallengeTimer
      '
      Me.APIChallengeTimer.Interval = 30000
      '
      'StatusStrip1
      '
      Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CurrentStatusLabel, Me._FillerLabel, Me.ProgressBarLabel, Me.StatusProgressBar, Me.CacheCountLabel})
      Me.StatusStrip1.Location = New System.Drawing.Point(0, 337)
      Me.StatusStrip1.Name = "StatusStrip1"
      Me.StatusStrip1.Size = New System.Drawing.Size(900, 25)
      Me.StatusStrip1.TabIndex = 17
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
      Me._FillerLabel.Size = New System.Drawing.Size(547, 20)
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
      'ListBox1
      '
      Me.ListBox1.FormattingEnabled = True
      Me.ListBox1.ItemHeight = 16
      Me.ListBox1.Location = New System.Drawing.Point(542, 6)
      Me.ListBox1.Name = "ListBox1"
      Me.ListBox1.Size = New System.Drawing.Size(347, 244)
      Me.ListBox1.TabIndex = 18
      '
      'EpochTimer
      '
      Me.EpochTimer.Enabled = True
      Me.EpochTimer.Interval = 50
      '
      'Button5
      '
      Me.Button5.Location = New System.Drawing.Point(3, 95)
      Me.Button5.Name = "Button5"
      Me.Button5.Size = New System.Drawing.Size(163, 23)
      Me.Button5.TabIndex = 19
      Me.Button5.Text = "Start Loading Games"
      Me.Button5.UseVisualStyleBackColor = True
      '
      'MatchLoadingBackgroundWorker
      '
      Me.MatchLoadingBackgroundWorker.WorkerReportsProgress = True
      '
      'Button6
      '
      Me.Button6.Location = New System.Drawing.Point(3, 124)
      Me.Button6.Name = "Button6"
      Me.Button6.Size = New System.Drawing.Size(163, 23)
      Me.Button6.TabIndex = 20
      Me.Button6.Text = "Populate Game Cache"
      Me.Button6.UseVisualStyleBackColor = True
      '
      'CacheBackgroundWorker
      '
      Me.CacheBackgroundWorker.WorkerReportsProgress = True
      '
      'TabControl1
      '
      Me.TabControl1.Controls.Add(Me.TabPage1)
      Me.TabControl1.Controls.Add(Me.TabPage2)
      Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
      Me.TabControl1.Location = New System.Drawing.Point(0, 0)
      Me.TabControl1.Name = "TabControl1"
      Me.TabControl1.SelectedIndex = 0
      Me.TabControl1.Size = New System.Drawing.Size(900, 362)
      Me.TabControl1.TabIndex = 21
      '
      'TabPage1
      '
      Me.TabPage1.Controls.Add(Me.Button6)
      Me.TabPage1.Controls.Add(Me.Button1)
      Me.TabPage1.Controls.Add(Me.Button5)
      Me.TabPage1.Controls.Add(Me.LastBucketTimeTextBox)
      Me.TabPage1.Controls.Add(Me.ListBox1)
      Me.TabPage1.Controls.Add(Me.Label1)
      Me.TabPage1.Controls.Add(Me.Button2)
      Me.TabPage1.Controls.Add(Me.Button4)
      Me.TabPage1.Controls.Add(Me.Button3)
      Me.TabPage1.Location = New System.Drawing.Point(4, 25)
      Me.TabPage1.Name = "TabPage1"
      Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
      Me.TabPage1.Size = New System.Drawing.Size(892, 333)
      Me.TabPage1.TabIndex = 0
      Me.TabPage1.Text = "Match Data"
      Me.TabPage1.UseVisualStyleBackColor = True
      '
      'TabPage2
      '
      Me.TabPage2.Controls.Add(Me.Button7)
      Me.TabPage2.Controls.Add(Me.TextBox1)
      Me.TabPage2.Location = New System.Drawing.Point(4, 25)
      Me.TabPage2.Name = "TabPage2"
      Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
      Me.TabPage2.Size = New System.Drawing.Size(892, 333)
      Me.TabPage2.TabIndex = 1
      Me.TabPage2.Text = "Basic Info"
      Me.TabPage2.UseVisualStyleBackColor = True
      '
      'TextBox1
      '
      Me.TextBox1.Location = New System.Drawing.Point(352, 0)
      Me.TextBox1.Multiline = True
      Me.TextBox1.Name = "TextBox1"
      Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
      Me.TextBox1.Size = New System.Drawing.Size(537, 309)
      Me.TextBox1.TabIndex = 0
      '
      'Button7
      '
      Me.Button7.Location = New System.Drawing.Point(271, 6)
      Me.Button7.Name = "Button7"
      Me.Button7.Size = New System.Drawing.Size(75, 23)
      Me.Button7.TabIndex = 1
      Me.Button7.Text = "Button7"
      Me.Button7.UseVisualStyleBackColor = True
      '
      'Timer1
      '
      Me.Timer1.Enabled = True
      '
      'MainWindow
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.ClientSize = New System.Drawing.Size(900, 362)
      Me.Controls.Add(Me.StatusStrip1)
      Me.Controls.Add(Me.TabControl1)
      Me.Name = "MainWindow"
      Me.Text = "Main Window"
      Me.StatusStrip1.ResumeLayout(False)
      Me.StatusStrip1.PerformLayout()
      Me.TabControl1.ResumeLayout(False)
      Me.TabPage1.ResumeLayout(False)
      Me.TabPage1.PerformLayout()
      Me.TabPage2.ResumeLayout(False)
      Me.TabPage2.PerformLayout()
      Me.ResumeLayout(False)
      Me.PerformLayout()

   End Sub
   Friend WithEvents LastBucketTimeTextBox As System.Windows.Forms.TextBox
   Friend WithEvents Label1 As System.Windows.Forms.Label
   Friend WithEvents Button1 As System.Windows.Forms.Button
   Friend WithEvents Button2 As System.Windows.Forms.Button
   Friend WithEvents Button3 As System.Windows.Forms.Button
   Friend WithEvents Button4 As System.Windows.Forms.Button
   Friend WithEvents APIChallengeTimer As System.Windows.Forms.Timer
   Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
   Friend WithEvents CacheCountLabel As System.Windows.Forms.ToolStripStatusLabel
   Friend WithEvents _FillerLabel As System.Windows.Forms.ToolStripStatusLabel
   Friend WithEvents CurrentStatusLabel As System.Windows.Forms.ToolStripStatusLabel
   Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
   Friend WithEvents EpochTimer As System.Windows.Forms.Timer
   Friend WithEvents Button5 As System.Windows.Forms.Button
   Friend WithEvents MatchLoadingBackgroundWorker As System.ComponentModel.BackgroundWorker
   Friend WithEvents ProgressBarLabel As System.Windows.Forms.ToolStripStatusLabel
   Friend WithEvents StatusProgressBar As System.Windows.Forms.ToolStripProgressBar
   Friend WithEvents Button6 As System.Windows.Forms.Button
   Friend WithEvents CacheBackgroundWorker As System.ComponentModel.BackgroundWorker
   Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
   Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
   Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
   Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
   Friend WithEvents Button7 As System.Windows.Forms.Button
   Friend WithEvents Timer1 As System.Windows.Forms.Timer

End Class
