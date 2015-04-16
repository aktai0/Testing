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
      Me.Button1 = New System.Windows.Forms.Button()
      Me.Button2 = New System.Windows.Forms.Button()
      Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
      Me.StatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
      Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
      Me.MatchIDsLabel = New System.Windows.Forms.Label()
      Me.LoadedMatchesLabel = New System.Windows.Forms.Label()
      Me.MatchLoaderBackgroundWorker = New System.ComponentModel.BackgroundWorker()
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
      'Button1
      '
      Me.Button1.Location = New System.Drawing.Point(15, 29)
      Me.Button1.Name = "Button1"
      Me.Button1.Size = New System.Drawing.Size(75, 23)
      Me.Button1.TabIndex = 1
      Me.Button1.Text = "Start"
      Me.Button1.UseVisualStyleBackColor = True
      '
      'Button2
      '
      Me.Button2.Enabled = False
      Me.Button2.Location = New System.Drawing.Point(96, 29)
      Me.Button2.Name = "Button2"
      Me.Button2.Size = New System.Drawing.Size(75, 23)
      Me.Button2.TabIndex = 2
      Me.Button2.Text = "Stop"
      Me.Button2.UseVisualStyleBackColor = True
      '
      'StatusStrip1
      '
      Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusLabel, Me.ToolStripStatusLabel3})
      Me.StatusStrip1.Location = New System.Drawing.Point(0, 238)
      Me.StatusStrip1.Name = "StatusStrip1"
      Me.StatusStrip1.Size = New System.Drawing.Size(782, 25)
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
      Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(706, 20)
      Me.ToolStripStatusLabel3.Spring = True
      '
      'MatchIDsLabel
      '
      Me.MatchIDsLabel.AutoSize = True
      Me.MatchIDsLabel.Location = New System.Drawing.Point(12, 62)
      Me.MatchIDsLabel.Name = "MatchIDsLabel"
      Me.MatchIDsLabel.Size = New System.Drawing.Size(148, 17)
      Me.MatchIDsLabel.TabIndex = 4
      Me.MatchIDsLabel.Text = "Number of Match IDs: "
      '
      'LoadedMatchesLabel
      '
      Me.LoadedMatchesLabel.AutoSize = True
      Me.LoadedMatchesLabel.Location = New System.Drawing.Point(12, 79)
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
      'MatchLoadingWindow
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.ClientSize = New System.Drawing.Size(782, 263)
      Me.Controls.Add(Me.LoadedMatchesLabel)
      Me.Controls.Add(Me.MatchIDsLabel)
      Me.Controls.Add(Me.StatusStrip1)
      Me.Controls.Add(Me.Button2)
      Me.Controls.Add(Me.Button1)
      Me.Controls.Add(Me.URFTimeLabel)
      Me.Name = "MatchLoadingWindow"
      Me.Text = "MatchLoadingWindow"
      Me.StatusStrip1.ResumeLayout(False)
      Me.StatusStrip1.PerformLayout()
      Me.ResumeLayout(False)
      Me.PerformLayout()

   End Sub
   Friend WithEvents URFTimeLabel As System.Windows.Forms.Label
   Friend WithEvents Button1 As System.Windows.Forms.Button
   Friend WithEvents Button2 As System.Windows.Forms.Button
   Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
   Friend WithEvents StatusLabel As System.Windows.Forms.ToolStripStatusLabel
   Friend WithEvents MatchIDsLabel As System.Windows.Forms.Label
   Friend WithEvents LoadedMatchesLabel As System.Windows.Forms.Label
   Friend WithEvents ToolStripStatusLabel3 As System.Windows.Forms.ToolStripStatusLabel
   Friend WithEvents MatchLoaderBackgroundWorker As System.ComponentModel.BackgroundWorker
End Class
