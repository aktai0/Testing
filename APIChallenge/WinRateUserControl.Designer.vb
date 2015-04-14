<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WinRateUserControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
      Me.WinRateLabel = New System.Windows.Forms.Label()
      Me.VSLabel = New System.Windows.Forms.Label()
      Me.EnemyLabel = New System.Windows.Forms.Label()
      Me.ChampionPictureBox = New System.Windows.Forms.PictureBox()
      Me.EnemyPictureBox = New System.Windows.Forms.PictureBox()
      Me.ChampionLabel = New System.Windows.Forms.Label()
      CType(Me.ChampionPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(Me.EnemyPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.SuspendLayout()
      '
      'WinRateLabel
      '
      Me.WinRateLabel.Font = New System.Drawing.Font("Garamond", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.WinRateLabel.Location = New System.Drawing.Point(51, 74)
      Me.WinRateLabel.Name = "WinRateLabel"
      Me.WinRateLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes
      Me.WinRateLabel.Size = New System.Drawing.Size(343, 39)
      Me.WinRateLabel.TabIndex = 20
      Me.WinRateLabel.Text = "Testing"
      Me.WinRateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
      '
      'VSLabel
      '
      Me.VSLabel.AutoSize = True
      Me.VSLabel.Font = New System.Drawing.Font("Algerian", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.VSLabel.Location = New System.Drawing.Point(199, 28)
      Me.VSLabel.Name = "VSLabel"
      Me.VSLabel.Size = New System.Drawing.Size(40, 26)
      Me.VSLabel.TabIndex = 19
      Me.VSLabel.Text = "VS"
      '
      'EnemyLabel
      '
      Me.EnemyLabel.AutoSize = True
      Me.EnemyLabel.Font = New System.Drawing.Font("Garamond", 13.0!)
      Me.EnemyLabel.Location = New System.Drawing.Point(308, 26)
      Me.EnemyLabel.Name = "EnemyLabel"
      Me.EnemyLabel.Size = New System.Drawing.Size(129, 25)
      Me.EnemyLabel.TabIndex = 18
      Me.EnemyLabel.Text = "Heimerdinger"
      Me.EnemyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
      '
      'ChampionPictureBox
      '
      Me.ChampionPictureBox.Location = New System.Drawing.Point(137, 11)
      Me.ChampionPictureBox.Name = "ChampionPictureBox"
      Me.ChampionPictureBox.Size = New System.Drawing.Size(60, 60)
      Me.ChampionPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
      Me.ChampionPictureBox.TabIndex = 15
      Me.ChampionPictureBox.TabStop = False
      '
      'EnemyPictureBox
      '
      Me.EnemyPictureBox.Location = New System.Drawing.Point(242, 11)
      Me.EnemyPictureBox.Name = "EnemyPictureBox"
      Me.EnemyPictureBox.Size = New System.Drawing.Size(60, 60)
      Me.EnemyPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
      Me.EnemyPictureBox.TabIndex = 17
      Me.EnemyPictureBox.TabStop = False
      '
      'ChampionLabel
      '
      Me.ChampionLabel.Font = New System.Drawing.Font("Garamond", 13.0!)
      Me.ChampionLabel.Location = New System.Drawing.Point(-22, 26)
      Me.ChampionLabel.Name = "ChampionLabel"
      Me.ChampionLabel.Size = New System.Drawing.Size(155, 29)
      Me.ChampionLabel.TabIndex = 16
      Me.ChampionLabel.Text = "Heimerdinger"
      Me.ChampionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'WinRateUserControl
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.Controls.Add(Me.WinRateLabel)
      Me.Controls.Add(Me.VSLabel)
      Me.Controls.Add(Me.EnemyLabel)
      Me.Controls.Add(Me.ChampionPictureBox)
      Me.Controls.Add(Me.EnemyPictureBox)
      Me.Controls.Add(Me.ChampionLabel)
      Me.Name = "WinRateUserControl"
      Me.Size = New System.Drawing.Size(443, 120)
      CType(Me.ChampionPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
      CType(Me.EnemyPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
      Me.ResumeLayout(False)
      Me.PerformLayout()

   End Sub
   Friend WithEvents WinRateLabel As System.Windows.Forms.Label
   Friend WithEvents VSLabel As System.Windows.Forms.Label
   Friend WithEvents EnemyLabel As System.Windows.Forms.Label
   Friend WithEvents ChampionPictureBox As System.Windows.Forms.PictureBox
   Friend WithEvents EnemyPictureBox As System.Windows.Forms.PictureBox
   Friend WithEvents ChampionLabel As System.Windows.Forms.Label

End Class
