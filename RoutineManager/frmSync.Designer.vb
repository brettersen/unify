<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSync
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
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.rtbConsole = New System.Windows.Forms.RichTextBox()
        Me.pbrOperation = New System.Windows.Forms.ProgressBar()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(647, 426)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'rtbConsole
        '
        Me.rtbConsole.BackColor = System.Drawing.SystemColors.Window
        Me.rtbConsole.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbConsole.Location = New System.Drawing.Point(12, 12)
        Me.rtbConsole.Name = "rtbConsole"
        Me.rtbConsole.Size = New System.Drawing.Size(710, 363)
        Me.rtbConsole.TabIndex = 3
        Me.rtbConsole.Text = ""
        '
        'pbrOperation
        '
        Me.pbrOperation.Location = New System.Drawing.Point(12, 389)
        Me.pbrOperation.Name = "pbrOperation"
        Me.pbrOperation.Size = New System.Drawing.Size(710, 23)
        Me.pbrOperation.TabIndex = 4
        '
        'frmSync
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(734, 461)
        Me.ControlBox = False
        Me.Controls.Add(Me.pbrOperation)
        Me.Controls.Add(Me.rtbConsole)
        Me.Controls.Add(Me.btnCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSync"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmSync"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents rtbConsole As System.Windows.Forms.RichTextBox
    Friend WithEvents pbrOperation As System.Windows.Forms.ProgressBar
End Class
