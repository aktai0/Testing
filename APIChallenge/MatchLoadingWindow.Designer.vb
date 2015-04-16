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
      Me.URFTimeLabel = New System.Windows.Forms.Label()
      Me.StartButton = New System.Windows.Forms.Button()
      Me.StopButton = New System.Windows.Forms.Button()
      Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
      Me.StatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
      Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
      Me.MatchIDsLabel = New System.Windows.Forms.Label()
      Me.LoadedMatchesLabel = New System.Windows.Forms.Label()
      Me.MatchLoaderBackgroundWorker = New System.ComponentModel.BackgroundWorker()
      Me.DateLabel = New System.Windows.Forms.Label()
      Me.StatusStrip1.SuspendLayout()
      Me.SuspendLayout()
      '
      'URFTimeLabel
      '
      Me.URFTimeLabel.AutoSize = True
      Me.URFTimeLabel.Location = New System.Drawing.Point(12, 9)
      Me.URFTimeLabel.Name = "URFTimeLabel"
      Me.URFTimeLabel.Size = New System.Drawing.Size(216, 17)
      Me.URFTimeLabel.TabIndex = 0
      Me.URFTimeLabel.Text = "Last URF Match Bucket Loaded: "
      '
      'StartButton
      '
      Me.StartButton.Location = New System.Drawing.Point(15, 96)
      Me.StartButton.Name = "StartButton"
      Me.StartButton.Size = New System.Drawing.Size(75, 23)
      Me.StartButton.TabIndex = 1
      Me.StartButton.Text = "Start"
      Me.StartButton.UseVisualStyleBackColor = True
      '
      'StopButton
      '
      Me.StopButton.Enabled = False
      Me.StopButton.Location = New System.Drawing.Point(96, 96)
      Me.StopButton.Name = "StopButton"
      Me.StopButton.Size = New System.Drawing.Size(75, 23)
      Me.StopButton.TabIndex = 2
      Me.StopButton.Text = "Stop"
      Me.StopButton.UseVisualStyleBackColor = True
      '
      'StatusStrip1
      '
      Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusLabel, Me.ToolStripStatusLabel3})
      Me.StatusStrip1.Location = New System.Drawing.Point(0, 122)
      Me.StatusStrip1.Name = "StatusStrip1"
      Me.StatusStrip1.Size = New System.Drawing.Size(333, 25)
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
      Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(257, 20)
      Me.ToolStripStatusLabel3.Spring = True
      '
      'MatchIDsLabel
      '
      Me.MatchIDsLabel.AutoSize = True
      Me.MatchIDsLabel.Location = New System.Drawing.Point(12, 58)
      Me.MatchIDsLabel.Name = "MatchIDsLabel"
      Me.MatchIDsLabel.Size = New System.Drawing.Size(148, 17)
      Me.MatchIDsLabel.TabIndex = 4
      Me.MatchIDsLabel.Text = "Number of Match IDs: "
      '
      'LoadedMatchesLabel
      '
      Me.LoadedMatchesLabel.AutoSize = True
      Me.LoadedMatchesLabel.Location = New System.Drawing.Point(12, 75)
      Me.LoadedMatchesLabel.Name = "LoadedMatchesLabel"
      Me.LoadedMatchesLabel.Size = New System.Drawing.Size(191, 17)
      Me.LoadedMatchesLabel.TabIndex = 5
      Me.LoadedMatchesLabel.Text = "Number of Loaded Matches: "
      '
      'MatchLoaderBackgroundWorker
      '
      Me.MatchLoaderBackgroundWorker.WorkerReportsProgress = True
      Me.MatchLoaderBackgroundWorker.WorkerSupportsCancellation = True
      '
      'DateLabel
      '
      Me.DateLabel.AutoSize = True
      Me.DateLabel.Location = New System.Drawing.Point(12, 33)
      Me.DateLabel.Name = "DateLabel"
      Me.DateLabel.Size = New System.Drawing.Size(38, 17)
      Me.DateLabel.TabIndex = 6
      Me.DateLabel.Text = "Date"
      '
      'MatchLoadingWindow
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.ClientSize = New System.Drawing.Size(333, 147)
      Me.Controls.Add(Me.DateLabel)
      Me.Controls.Add(Me.LoadedMatchesLabel)
      Me.Controls.Add(Me.MatchIDsLabel)
      Me.Controls.Add(Me.StatusStrip1)
      Me.Controls.Add(Me.StopButton)
      Me.Controls.Add(Me.StartButton)
      Me.Controls.Add(Me.URFTimeLabel)
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
   Friend WithEvents MatchIDsLabel As System.Windows.Forms.Label
   Friend WithEvents LoadedMatchesLabel As System.Windows.Forms.Label
   Friend WithEvents ToolStripStatusLabel3 As System.Windows.Forms.ToolStripStatusLabel
   Friend WithEvents MatchLoaderBackgroundWorker As System.ComponentModel.BackgroundWorker
   Friend WithEvents DateLabel As System.Windows.Forms.Label
End Class
