<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MatchLoadingWindow
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
      Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MatchLoadingWindow))
      Me.URFTimeLabel = New System.Windows.Forms.Label()
      Me.StartButton = New System.Windows.Forms.Button()
      Me.StopButton = New System.Windows.Forms.Button()
      Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
      Me.StatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
      Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
      Me.__TotalMatchIDsLabel = New System.Windows.Forms.Label()
      Me.__UnloadedMatchesLabel = New System.Windows.Forms.Label()
      Me.MatchLoaderBackgroundWorker = New System.ComponentModel.BackgroundWorker()
      Me.DateLabel = New System.Windows.Forms.Label()
      Me.__LoadedMatchesLabel = New System.Windows.Forms.Label()
      Me.TotalMatchIDsLabel = New System.Windows.Forms.Label()
      Me.UnloadedMatchesLabel = New System.Windows.Forms.Label()
      Me.LoadedMatchesLabel = New System.Windows.Forms.Label()
      Me.SlowRadioButton = New System.Windows.Forms.RadioButton()
      Me.FastRadioButton = New System.Windows.Forms.RadioButton()
      Me.StatusStrip1.SuspendLayout()
      Me.SuspendLayout()
      '
      'URFTimeLabel
      '
      Me.URFTimeLabel.AutoSize = True
      Me.URFTimeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.URFTimeLabel.Location = New System.Drawing.Point(12, 9)
      Me.URFTimeLabel.Name = "URFTimeLabel"
      Me.URFTimeLabel.Size = New System.Drawing.Size(246, 17)
      Me.URFTimeLabel.TabIndex = 0
      Me.URFTimeLabel.Text = "Last URF Match Bucket Loaded: "
      '
      'StartButton
      '
      Me.StartButton.Location = New System.Drawing.Point(32, 139)
      Me.StartButton.Name = "StartButton"
      Me.StartButton.Size = New System.Drawing.Size(105, 23)
      Me.StartButton.TabIndex = 1
      Me.StartButton.Text = "Start"
      Me.StartButton.UseVisualStyleBackColor = True
      '
      'StopButton
      '
      Me.StopButton.Enabled = False
      Me.StopButton.Location = New System.Drawing.Point(143, 139)
      Me.StopButton.Name = "StopButton"
      Me.StopButton.Size = New System.Drawing.Size(105, 23)
      Me.StopButton.TabIndex = 2
      Me.StopButton.Text = "Stop"
      Me.StopButton.UseVisualStyleBackColor = True
      '
      'StatusStrip1
      '
      Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusLabel, Me.ToolStripStatusLabel3})
      Me.StatusStrip1.Location = New System.Drawing.Point(0, 171)
      Me.StatusStrip1.Name = "StatusStrip1"
      Me.StatusStrip1.Size = New System.Drawing.Size(275, 25)
      Me.StatusStrip1.TabIndex = 3
      Me.StatusStrip1.Text = "StatusStrip1"
      '
      'StatusLabel
      '
      Me.StatusLabel.Name = "StatusLabel"
      Me.StatusLabel.Size = New System.Drawing.Size(61, 20)
      Me.StatusLabel.Text = "Waiting"
      '
      'ToolStripStatusLabel3
      '
      Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
      Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(199, 20)
      Me.ToolStripStatusLabel3.Spring = True
      '
      '__TotalMatchIDsLabel
      '
      Me.__TotalMatchIDsLabel.AutoSize = True
      Me.__TotalMatchIDsLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.__TotalMatchIDsLabel.Location = New System.Drawing.Point(12, 58)
      Me.__TotalMatchIDsLabel.Name = "__TotalMatchIDsLabel"
      Me.__TotalMatchIDsLabel.Size = New System.Drawing.Size(131, 17)
      Me.__TotalMatchIDsLabel.TabIndex = 4
      Me.__TotalMatchIDsLabel.Text = "Total Match IDs: "
      '
      '__UnloadedMatchesLabel
      '
      Me.__UnloadedMatchesLabel.AutoSize = True
      Me.__UnloadedMatchesLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.__UnloadedMatchesLabel.Location = New System.Drawing.Point(12, 75)
      Me.__UnloadedMatchesLabel.Name = "__UnloadedMatchesLabel"
      Me.__UnloadedMatchesLabel.Size = New System.Drawing.Size(152, 17)
      Me.__UnloadedMatchesLabel.TabIndex = 5
      Me.__UnloadedMatchesLabel.Text = "Unloaded Matches: "
      '
      'MatchLoaderBackgroundWorker
      '
      Me.MatchLoaderBackgroundWorker.WorkerReportsProgress = True
      Me.MatchLoaderBackgroundWorker.WorkerSupportsCancellation = True
      '
      'DateLabel
      '
      Me.DateLabel.Location = New System.Drawing.Point(12, 33)
      Me.DateLabel.Name = "DateLabel"
      Me.DateLabel.Size = New System.Drawing.Size(246, 17)
      Me.DateLabel.TabIndex = 6
      Me.DateLabel.Text = "Date"
      Me.DateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
      '
      '__LoadedMatchesLabel
      '
      Me.__LoadedMatchesLabel.AutoSize = True
      Me.__LoadedMatchesLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.__LoadedMatchesLabel.Location = New System.Drawing.Point(12, 92)
      Me.__LoadedMatchesLabel.Name = "__LoadedMatchesLabel"
      Me.__LoadedMatchesLabel.Size = New System.Drawing.Size(137, 17)
      Me.__LoadedMatchesLabel.TabIndex = 7
      Me.__LoadedMatchesLabel.Text = "Loaded Matches: "
      '
      'TotalMatchIDsLabel
      '
      Me.TotalMatchIDsLabel.Location = New System.Drawing.Point(161, 58)
      Me.TotalMatchIDsLabel.Name = "TotalMatchIDsLabel"
      Me.TotalMatchIDsLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes
      Me.TotalMatchIDsLabel.Size = New System.Drawing.Size(100, 20)
      Me.TotalMatchIDsLabel.TabIndex = 8
      Me.TotalMatchIDsLabel.Text = "Label1"
      '
      'UnloadedMatchesLabel
      '
      Me.UnloadedMatchesLabel.Location = New System.Drawing.Point(161, 75)
      Me.UnloadedMatchesLabel.Name = "UnloadedMatchesLabel"
      Me.UnloadedMatchesLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes
      Me.UnloadedMatchesLabel.Size = New System.Drawing.Size(100, 20)
      Me.UnloadedMatchesLabel.TabIndex = 9
      Me.UnloadedMatchesLabel.Text = "Label2"
      '
      'LoadedMatchesLabel
      '
      Me.LoadedMatchesLabel.Location = New System.Drawing.Point(161, 92)
      Me.LoadedMatchesLabel.Name = "LoadedMatchesLabel"
      Me.LoadedMatchesLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes
      Me.LoadedMatchesLabel.Size = New System.Drawing.Size(100, 20)
      Me.LoadedMatchesLabel.TabIndex = 10
      Me.LoadedMatchesLabel.Text = "Label3"
      '
      'SlowRadioButton
      '
      Me.SlowRadioButton.AutoSize = True
      Me.SlowRadioButton.Location = New System.Drawing.Point(54, 112)
      Me.SlowRadioButton.Name = "SlowRadioButton"
      Me.SlowRadioButton.Size = New System.Drawing.Size(83, 21)
      Me.SlowRadioButton.TabIndex = 11
      Me.SlowRadioButton.Text = "Slow API"
      Me.SlowRadioButton.UseVisualStyleBackColor = True
      '
      'FastRadioButton
      '
      Me.FastRadioButton.AutoSize = True
      Me.FastRadioButton.Checked = True
      Me.FastRadioButton.Location = New System.Drawing.Point(143, 112)
      Me.FastRadioButton.Name = "FastRadioButton"
      Me.FastRadioButton.Size = New System.Drawing.Size(81, 21)
      Me.FastRadioButton.TabIndex = 12
      Me.FastRadioButton.TabStop = True
      Me.FastRadioButton.Text = "Fast API"
      Me.FastRadioButton.UseVisualStyleBackColor = True
      '
      'MatchLoadingWindow
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.ClientSize = New System.Drawing.Size(275, 196)
      Me.Controls.Add(Me.FastRadioButton)
      Me.Controls.Add(Me.SlowRadioButton)
      Me.Controls.Add(Me.LoadedMatchesLabel)
      Me.Controls.Add(Me.UnloadedMatchesLabel)
      Me.Controls.Add(Me.TotalMatchIDsLabel)
      Me.Controls.Add(Me.__LoadedMatchesLabel)
      Me.Controls.Add(Me.DateLabel)
      Me.Controls.Add(Me.__UnloadedMatchesLabel)
      Me.Controls.Add(Me.__TotalMatchIDsLabel)
      Me.Controls.Add(Me.StatusStrip1)
      Me.Controls.Add(Me.StopButton)
      Me.Controls.Add(Me.StartButton)
      Me.Controls.Add(Me.URFTimeLabel)
      Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
      Me.Name = "MatchLoadingWindow"
      Me.Text = "Match Loading"
      Me.StatusStrip1.ResumeLayout(False)
      Me.StatusStrip1.PerformLayout()
      Me.ResumeLayout(False)
      Me.PerformLayout()

   End Sub
   Friend WithEvents URFTimeLabel As System.Windows.Forms.Label
   Friend WithEvents StartButton As System.Windows.Forms.Button
   Friend WithEvents StopButton As System.Windows.Forms.Button
   Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
   Friend WithEvents StatusLabel As System.Windows.Forms.ToolStripStatusLabel
   Friend WithEvents __TotalMatchIDsLabel As System.Windows.Forms.Label
   Friend WithEvents __UnloadedMatchesLabel As System.Windows.Forms.Label
   Friend WithEvents ToolStripStatusLabel3 As System.Windows.Forms.ToolStripStatusLabel
   Friend WithEvents MatchLoaderBackgroundWorker As System.ComponentModel.BackgroundWorker
   Friend WithEvents DateLabel As System.Windows.Forms.Label
   Friend WithEvents __LoadedMatchesLabel As System.Windows.Forms.Label
   Friend WithEvents TotalMatchIDsLabel As System.Windows.Forms.Label
   Friend WithEvents UnloadedMatchesLabel As System.Windows.Forms.Label
   Friend WithEvents LoadedMatchesLabel As System.Windows.Forms.Label
   Friend WithEvents SlowRadioButton As System.Windows.Forms.RadioButton
   Friend WithEvents FastRadioButton As System.Windows.Forms.RadioButton
End Class
