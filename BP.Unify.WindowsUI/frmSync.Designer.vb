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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblTotalSucceeded = New System.Windows.Forms.Label()
        Me.lblTotalFailed = New System.Windows.Forms.Label()
        Me.lblTotalIgnored = New System.Windows.Forms.Label()
        Me.lblRoutineProgress = New System.Windows.Forms.Label()
        Me.lblTaskProgress = New System.Windows.Forms.Label()
        Me.lblFileProgress = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(647, 381)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'rtbConsole
        '
        Me.rtbConsole.BackColor = System.Drawing.SystemColors.Window
        Me.rtbConsole.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbConsole.Location = New System.Drawing.Point(12, 12)
        Me.rtbConsole.Name = "rtbConsole"
        Me.rtbConsole.ReadOnly = True
        Me.rtbConsole.Size = New System.Drawing.Size(710, 326)
        Me.rtbConsole.TabIndex = 0
        Me.rtbConsole.Text = ""
        Me.rtbConsole.WordWrap = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label1.Location = New System.Drawing.Point(21, 345)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Routine Progress:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label2.Location = New System.Drawing.Point(165, 345)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Task Progress:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label3.Location = New System.Drawing.Point(290, 345)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "File Progress:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label4.Location = New System.Drawing.Point(593, 345)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Succeeded:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label5.Location = New System.Drawing.Point(411, 345)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Failed:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label6.Location = New System.Drawing.Point(497, 345)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(51, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Ignored:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalSucceeded
        '
        Me.lblTotalSucceeded.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotalSucceeded.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalSucceeded.Location = New System.Drawing.Point(656, 343)
        Me.lblTotalSucceeded.Name = "lblTotalSucceeded"
        Me.lblTotalSucceeded.Size = New System.Drawing.Size(56, 17)
        Me.lblTotalSucceeded.TabIndex = 13
        Me.lblTotalSucceeded.Text = "0"
        Me.lblTotalSucceeded.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalFailed
        '
        Me.lblTotalFailed.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotalFailed.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalFailed.Location = New System.Drawing.Point(450, 343)
        Me.lblTotalFailed.Name = "lblTotalFailed"
        Me.lblTotalFailed.Size = New System.Drawing.Size(41, 17)
        Me.lblTotalFailed.TabIndex = 14
        Me.lblTotalFailed.Text = "0"
        Me.lblTotalFailed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalIgnored
        '
        Me.lblTotalIgnored.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotalIgnored.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalIgnored.Location = New System.Drawing.Point(546, 343)
        Me.lblTotalIgnored.Name = "lblTotalIgnored"
        Me.lblTotalIgnored.Size = New System.Drawing.Size(41, 17)
        Me.lblTotalIgnored.TabIndex = 15
        Me.lblTotalIgnored.Text = "0"
        Me.lblTotalIgnored.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRoutineProgress
        '
        Me.lblRoutineProgress.BackColor = System.Drawing.SystemColors.Control
        Me.lblRoutineProgress.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoutineProgress.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRoutineProgress.Location = New System.Drawing.Point(118, 343)
        Me.lblRoutineProgress.Name = "lblRoutineProgress"
        Me.lblRoutineProgress.Size = New System.Drawing.Size(41, 17)
        Me.lblRoutineProgress.TabIndex = 16
        Me.lblRoutineProgress.Text = "0%"
        Me.lblRoutineProgress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTaskProgress
        '
        Me.lblTaskProgress.BackColor = System.Drawing.SystemColors.Control
        Me.lblTaskProgress.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaskProgress.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTaskProgress.Location = New System.Drawing.Point(243, 343)
        Me.lblTaskProgress.Name = "lblTaskProgress"
        Me.lblTaskProgress.Size = New System.Drawing.Size(41, 17)
        Me.lblTaskProgress.TabIndex = 17
        Me.lblTaskProgress.Text = "0%"
        Me.lblTaskProgress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFileProgress
        '
        Me.lblFileProgress.BackColor = System.Drawing.SystemColors.Control
        Me.lblFileProgress.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFileProgress.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFileProgress.Location = New System.Drawing.Point(364, 343)
        Me.lblFileProgress.Name = "lblFileProgress"
        Me.lblFileProgress.Size = New System.Drawing.Size(41, 17)
        Me.lblFileProgress.TabIndex = 19
        Me.lblFileProgress.Text = "0%"
        Me.lblFileProgress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label7.Location = New System.Drawing.Point(12, 367)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(710, 1)
        Me.Label7.TabIndex = 20
        '
        'frmSync
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(734, 416)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblFileProgress)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.rtbConsole)
        Me.Controls.Add(Me.lblTaskProgress)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblTotalSucceeded)
        Me.Controls.Add(Me.lblRoutineProgress)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblTotalFailed)
        Me.Controls.Add(Me.lblTotalIgnored)
        Me.Controls.Add(Me.Label5)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSync"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmSync"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents rtbConsole As System.Windows.Forms.RichTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblTotalSucceeded As System.Windows.Forms.Label
    Friend WithEvents lblTotalFailed As System.Windows.Forms.Label
    Friend WithEvents lblTotalIgnored As System.Windows.Forms.Label
    Friend WithEvents lblRoutineProgress As System.Windows.Forms.Label
    Friend WithEvents lblTaskProgress As System.Windows.Forms.Label
    Friend WithEvents lblFileProgress As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
End Class
