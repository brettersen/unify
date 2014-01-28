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
        Me.pbrFile = New System.Windows.Forms.ProgressBar()
        Me.pbrRoutine = New System.Windows.Forms.ProgressBar()
        Me.pbrTask = New System.Windows.Forms.ProgressBar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblTotalSucceeded = New System.Windows.Forms.Label()
        Me.lblTotalFailed = New System.Windows.Forms.Label()
        Me.lblTotalIgnored = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(647, 460)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'rtbConsole
        '
        Me.rtbConsole.BackColor = System.Drawing.SystemColors.Window
        Me.rtbConsole.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbConsole.Location = New System.Drawing.Point(12, 12)
        Me.rtbConsole.Name = "rtbConsole"
        Me.rtbConsole.ReadOnly = True
        Me.rtbConsole.Size = New System.Drawing.Size(710, 326)
        Me.rtbConsole.TabIndex = 0
        Me.rtbConsole.Text = ""
        Me.rtbConsole.WordWrap = False
        '
        'pbrFile
        '
        Me.pbrFile.Location = New System.Drawing.Point(120, 405)
        Me.pbrFile.Name = "pbrFile"
        Me.pbrFile.Size = New System.Drawing.Size(602, 17)
        Me.pbrFile.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.pbrFile.TabIndex = 4
        '
        'pbrRoutine
        '
        Me.pbrRoutine.Location = New System.Drawing.Point(120, 351)
        Me.pbrRoutine.Name = "pbrRoutine"
        Me.pbrRoutine.Size = New System.Drawing.Size(602, 17)
        Me.pbrRoutine.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.pbrRoutine.TabIndex = 5
        '
        'pbrTask
        '
        Me.pbrTask.Location = New System.Drawing.Point(120, 378)
        Me.pbrTask.Name = "pbrTask"
        Me.pbrTask.Size = New System.Drawing.Size(602, 17)
        Me.pbrTask.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.pbrTask.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Window
        Me.Label1.Location = New System.Drawing.Point(12, 351)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 17)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Routine Progress"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.Window
        Me.Label2.Location = New System.Drawing.Point(12, 378)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 17)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Task Progress"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.Window
        Me.Label3.Location = New System.Drawing.Point(12, 405)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(102, 17)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "File Progress"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.Window
        Me.Label4.Location = New System.Drawing.Point(212, 432)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(106, 17)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Succeeded"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.Window
        Me.Label5.Location = New System.Drawing.Point(414, 432)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(106, 17)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Failed"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.Window
        Me.Label6.Location = New System.Drawing.Point(616, 432)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(106, 17)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Ignored"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalSucceeded
        '
        Me.lblTotalSucceeded.BackColor = System.Drawing.SystemColors.Window
        Me.lblTotalSucceeded.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotalSucceeded.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalSucceeded.Location = New System.Drawing.Point(120, 432)
        Me.lblTotalSucceeded.Name = "lblTotalSucceeded"
        Me.lblTotalSucceeded.Size = New System.Drawing.Size(86, 17)
        Me.lblTotalSucceeded.TabIndex = 13
        Me.lblTotalSucceeded.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalFailed
        '
        Me.lblTotalFailed.BackColor = System.Drawing.SystemColors.Window
        Me.lblTotalFailed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotalFailed.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalFailed.Location = New System.Drawing.Point(324, 432)
        Me.lblTotalFailed.Name = "lblTotalFailed"
        Me.lblTotalFailed.Size = New System.Drawing.Size(84, 17)
        Me.lblTotalFailed.TabIndex = 14
        Me.lblTotalFailed.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalIgnored
        '
        Me.lblTotalIgnored.BackColor = System.Drawing.SystemColors.Window
        Me.lblTotalIgnored.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotalIgnored.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalIgnored.Location = New System.Drawing.Point(526, 432)
        Me.lblTotalIgnored.Name = "lblTotalIgnored"
        Me.lblTotalIgnored.Size = New System.Drawing.Size(84, 17)
        Me.lblTotalIgnored.TabIndex = 15
        Me.lblTotalIgnored.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.Window
        Me.Label10.Location = New System.Drawing.Point(12, 432)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(102, 17)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "File Count"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmSync
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(734, 495)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.lblTotalIgnored)
        Me.Controls.Add(Me.lblTotalFailed)
        Me.Controls.Add(Me.lblTotalSucceeded)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.pbrTask)
        Me.Controls.Add(Me.pbrRoutine)
        Me.Controls.Add(Me.pbrFile)
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
    Friend WithEvents pbrFile As System.Windows.Forms.ProgressBar
    Friend WithEvents pbrRoutine As System.Windows.Forms.ProgressBar
    Friend WithEvents pbrTask As System.Windows.Forms.ProgressBar
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblTotalSucceeded As System.Windows.Forms.Label
    Friend WithEvents lblTotalFailed As System.Windows.Forms.Label
    Friend WithEvents lblTotalIgnored As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
End Class
