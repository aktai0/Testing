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
      Me.TabPage2 = New System.Windows.Forms.TabPage()
      Me.Button9 = New System.Windows.Forms.Button()
      Me.ComboBox1 = New System.Windows.Forms.ComboBox()
      Me.ListBox2 = New System.Windows.Forms.ListBox()
      Me.Button7 = New System.Windows.Forms.Button()
      Me.TabPage1 = New System.Windows.Forms.TabPage()
      Me.PictureBox1 = New System.Windows.Forms.PictureBox()
      Me.TextBox1 = New System.Windows.Forms.TextBox()
      Me.Button8 = New System.Windows.Forms.Button()
      Me.Button6 = New System.Windows.Forms.Button()
      Me.Button1 = New System.Windows.Forms.Button()
      Me.Button5 = New System.Windows.Forms.Button()
      Me.LastBucketTimeTextBox = New System.Windows.Forms.TextBox()
      Me.ListBox1 = New System.Windows.Forms.ListBox()
      Me.Label1 = New System.Windows.Forms.Label()
      Me.Button2 = New System.Windows.Forms.Button()
      Me.Button4 = New System.Windows.Forms.Button()
      Me.Button3 = New System.Windows.Forms.Button()
      Me.TabControl1 = New System.Windows.Forms.TabControl()
      Me.StatusStrip1.SuspendLayout()
      Me.TabPage2.SuspendLayout()
      Me.TabPage1.SuspendLayout()
      CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.TabControl1.SuspendLayout()
      Me.SuspendLayout()
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
      'TabPage2
      '
      Me.TabPage2.Controls.Add(Me.Button9)
      Me.TabPage2.Controls.Add(Me.ComboBox1)
      Me.TabPage2.Controls.Add(Me.ListBox2)
      Me.TabPage2.Controls.Add(Me.Button7)
      Me.TabPage2.Location = New System.Drawing.Point(4, 25)
      Me.TabPage2.Name = "TabPage2"
      Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
      Me.TabPage2.Size = New System.Drawing.Size(892, 333)
      Me.TabPage2.TabIndex = 1
      Me.TabPage2.Text = "Basic Info"
      Me.TabPage2.UseVisualStyleBackColor = True
      '
      'Button9
      '
      Me.Button9.Location = New System.Drawing.Point(9, 37)
      Me.Button9.Name = "Button9"
      Me.Button9.Size = New System.Drawing.Size(75, 23)
      Me.Button9.TabIndex = 4
      Me.Button9.Text = "Button9"
      Me.Button9.UseVisualStyleBackColor = True
      '
      'ComboBox1
      '
      Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
      Me.ComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
      Me.ComboBox1.FormattingEnabled = True
      Me.ComboBox1.Location = New System.Drawing.Point(8, 6)
      Me.ComboBox1.Name = "ComboBox1"
      Me.ComboBox1.Size = New System.Drawing.Size(121, 24)
      Me.ComboBox1.TabIndex = 3
      '
      'ListBox2
      '
      Me.ListBox2.FormattingEnabled = True
      Me.ListBox2.ItemHeight = 16
      Me.ListBox2.Location = New System.Drawing.Point(352, 3)
      Me.ListBox2.Name = "ListBox2"
      Me.ListBox2.Size = New System.Drawing.Size(537, 308)
      Me.ListBox2.TabIndex = 2
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
      'TabPage1
      '
      Me.TabPage1.Controls.Add(Me.PictureBox1)
      Me.TabPage1.Controls.Add(Me.TextBox1)
      Me.TabPage1.Controls.Add(Me.Button8)
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
      'PictureBox1
      '
      Me.PictureBox1.Location = New System.Drawing.Point(160, 189)
      Me.PictureBox1.Name = "PictureBox1"
      Me.PictureBox1.Size = New System.Drawing.Size(156, 108)
      Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
      Me.PictureBox1.TabIndex = 23
      Me.PictureBox1.TabStop = False
      '
      'TextBox1
      '
      Me.TextBox1.Location = New System.Drawing.Point(333, 228)
      Me.TextBox1.Name = "TextBox1"
      Me.TextBox1.Size = New System.Drawing.Size(100, 22)
      Me.TextBox1.TabIndex = 22
      Me.TextBox1.Text = "0"
      '
      'Button8
      '
      Me.Button8.Location = New System.Drawing.Point(334, 256)
      Me.Button8.Name = "Button8"
      Me.Button8.Size = New System.Drawing.Size(75, 23)
      Me.Button8.TabIndex = 21
      Me.Button8.Text = "Button8"
      Me.Button8.UseVisualStyleBackColor = True
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
      'Button1
      '
      Me.Button1.Location = New System.Drawing.Point(380, 160)
      Me.Button1.Name = "Button1"
      Me.Button1.Size = New System.Drawing.Size(75, 23)
      Me.Button1.TabIndex = 10
      Me.Button1.Text = "Button1"
      Me.Button1.UseVisualStyleBackColor = True
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
      'LastBucketTimeTextBox
      '
      Me.LastBucketTimeTextBox.Location = New System.Drawing.Point(141, 6)
      Me.LastBucketTimeTextBox.Name = "LastBucketTimeTextBox"
      Me.LastBucketTimeTextBox.ReadOnly = True
      Me.LastBucketTimeTextBox.Size = New System.Drawing.Size(268, 22)
      Me.LastBucketTimeTextBox.TabIndex = 0
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
      'Label1
      '
      Me.Label1.AutoSize = True
      Me.Label1.Location = New System.Drawing.Point(0, 9)
      Me.Label1.Name = "Label1"
      Me.Label1.Size = New System.Drawing.Size(125, 17)
      Me.Label1.TabIndex = 2
      Me.Label1.Text = "Last Bucket Time: "
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
      'Button3
      '
      Me.Button3.Location = New System.Drawing.Point(2, 34)
      Me.Button3.Name = "Button3"
      Me.Button3.Size = New System.Drawing.Size(68, 23)
      Me.Button3.TabIndex = 13
      Me.Button3.Text = "Start"
      Me.Button3.UseVisualStyleBackColor = True
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
      Me.TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
      Me.TabControl1.TabIndex = 21
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
      Me.TabPage2.ResumeLayout(False)
      Me.TabPage1.ResumeLayout(False)
      Me.TabPage1.PerformLayout()
      CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
      Me.TabControl1.ResumeLayout(False)
      Me.ResumeLayout(False)
      Me.PerformLayout()

   End Sub
   Friend WithEvents APIChallengeTimer As System.Windows.Forms.Timer
   Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
   Friend WithEvents CacheCountLabel As System.Windows.Forms.ToolStripStatusLabel
   Friend WithEvents _FillerLabel As System.Windows.Forms.ToolStripStatusLabel
   Friend WithEvents CurrentStatusLabel As System.Windows.Forms.ToolStripStatusLabel
   Friend WithEvents EpochTimer As System.Windows.Forms.Timer
   Friend WithEvents MatchLoadingBackgroundWorker As System.ComponentModel.BackgroundWorker
   Friend WithEvents ProgressBarLabel As System.Windows.Forms.ToolStripStatusLabel
   Friend WithEvents StatusProgressBar As System.Windows.Forms.ToolStripProgressBar
   Friend WithEvents CacheBackgroundWorker As System.ComponentModel.BackgroundWorker
   Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
   Friend WithEvents ListBox2 As System.Windows.Forms.ListBox
   Friend WithEvents Button7 As System.Windows.Forms.Button
   Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
   Friend WithEvents Button6 As System.Windows.Forms.Button
   Friend WithEvents Button1 As System.Windows.Forms.Button
   Friend WithEvents Button5 As System.Windows.Forms.Button
   Friend WithEvents LastBucketTimeTextBox As System.Windows.Forms.TextBox
   Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
   Friend WithEvents Label1 As System.Windows.Forms.Label
   Friend WithEvents Button2 As System.Windows.Forms.Button
   Friend WithEvents Button4 As System.Windows.Forms.Button
   Friend WithEvents Button3 As System.Windows.Forms.Button
   Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
   Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
   Friend WithEvents Button8 As System.Windows.Forms.Button
   Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
   Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
   Friend WithEvents Button9 As System.Windows.Forms.Button

End Class
