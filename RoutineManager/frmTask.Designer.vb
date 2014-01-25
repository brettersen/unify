<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTask
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
        Me.txtSourceDirectory = New System.Windows.Forms.TextBox()
        Me.txtTargetDirectory = New System.Windows.Forms.TextBox()
        Me.chkAddFiles = New System.Windows.Forms.CheckBox()
        Me.chkRemoveFiles = New System.Windows.Forms.CheckBox()
        Me.chkReplaceFiles = New System.Windows.Forms.CheckBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnManage = New System.Windows.Forms.Button()
        Me.btnSourceDirectory = New System.Windows.Forms.Button()
        Me.btnTargetDirectory = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkExcludeHiddenFiles = New System.Windows.Forms.CheckBox()
        Me.chkIncludeSubdirectories = New System.Windows.Forms.CheckBox()
        Me.dgvExemption = New System.Windows.Forms.DataGridView()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgvExemption, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtSourceDirectory
        '
        Me.txtSourceDirectory.Location = New System.Drawing.Point(12, 25)
        Me.txtSourceDirectory.Name = "txtSourceDirectory"
        Me.txtSourceDirectory.Size = New System.Drawing.Size(390, 20)
        Me.txtSourceDirectory.TabIndex = 0
        '
        'txtTargetDirectory
        '
        Me.txtTargetDirectory.Location = New System.Drawing.Point(12, 69)
        Me.txtTargetDirectory.Name = "txtTargetDirectory"
        Me.txtTargetDirectory.Size = New System.Drawing.Size(390, 20)
        Me.txtTargetDirectory.TabIndex = 1
        '
        'chkAddFiles
        '
        Me.chkAddFiles.AutoSize = True
        Me.chkAddFiles.Checked = True
        Me.chkAddFiles.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAddFiles.Location = New System.Drawing.Point(11, 22)
        Me.chkAddFiles.Name = "chkAddFiles"
        Me.chkAddFiles.Size = New System.Drawing.Size(66, 17)
        Me.chkAddFiles.TabIndex = 2
        Me.chkAddFiles.Text = "Add files"
        Me.chkAddFiles.UseVisualStyleBackColor = True
        '
        'chkRemoveFiles
        '
        Me.chkRemoveFiles.AutoSize = True
        Me.chkRemoveFiles.Checked = True
        Me.chkRemoveFiles.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRemoveFiles.Location = New System.Drawing.Point(11, 45)
        Me.chkRemoveFiles.Name = "chkRemoveFiles"
        Me.chkRemoveFiles.Size = New System.Drawing.Size(87, 17)
        Me.chkRemoveFiles.TabIndex = 1
        Me.chkRemoveFiles.Text = "Remove files"
        Me.chkRemoveFiles.UseVisualStyleBackColor = True
        '
        'chkReplaceFiles
        '
        Me.chkReplaceFiles.AutoSize = True
        Me.chkReplaceFiles.Checked = True
        Me.chkReplaceFiles.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkReplaceFiles.Location = New System.Drawing.Point(11, 68)
        Me.chkReplaceFiles.Name = "chkReplaceFiles"
        Me.chkReplaceFiles.Size = New System.Drawing.Size(87, 17)
        Me.chkReplaceFiles.TabIndex = 0
        Me.chkReplaceFiles.Text = "Replace files"
        Me.chkReplaceFiles.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(357, 297)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(357, 326)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnManage
        '
        Me.btnManage.Location = New System.Drawing.Point(357, 216)
        Me.btnManage.Name = "btnManage"
        Me.btnManage.Size = New System.Drawing.Size(75, 23)
        Me.btnManage.TabIndex = 5
        Me.btnManage.Text = "Manage..."
        Me.btnManage.UseVisualStyleBackColor = True
        '
        'btnSourceDirectory
        '
        Me.btnSourceDirectory.Location = New System.Drawing.Point(408, 23)
        Me.btnSourceDirectory.Name = "btnSourceDirectory"
        Me.btnSourceDirectory.Size = New System.Drawing.Size(24, 23)
        Me.btnSourceDirectory.TabIndex = 6
        Me.btnSourceDirectory.Text = "..."
        Me.btnSourceDirectory.UseVisualStyleBackColor = True
        '
        'btnTargetDirectory
        '
        Me.btnTargetDirectory.Location = New System.Drawing.Point(408, 67)
        Me.btnTargetDirectory.Name = "btnTargetDirectory"
        Me.btnTargetDirectory.Size = New System.Drawing.Size(24, 23)
        Me.btnTargetDirectory.TabIndex = 7
        Me.btnTargetDirectory.Text = "..."
        Me.btnTargetDirectory.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Source Directory"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Target Directory"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkExcludeHiddenFiles)
        Me.GroupBox2.Controls.Add(Me.chkIncludeSubdirectories)
        Me.GroupBox2.Location = New System.Drawing.Point(148, 99)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(284, 94)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Options"
        '
        'chkExcludeHiddenFiles
        '
        Me.chkExcludeHiddenFiles.AutoSize = True
        Me.chkExcludeHiddenFiles.Location = New System.Drawing.Point(11, 45)
        Me.chkExcludeHiddenFiles.Name = "chkExcludeHiddenFiles"
        Me.chkExcludeHiddenFiles.Size = New System.Drawing.Size(120, 17)
        Me.chkExcludeHiddenFiles.TabIndex = 15
        Me.chkExcludeHiddenFiles.Text = "Exclude hidden files"
        Me.chkExcludeHiddenFiles.UseVisualStyleBackColor = True
        '
        'chkIncludeSubdirectories
        '
        Me.chkIncludeSubdirectories.AutoSize = True
        Me.chkIncludeSubdirectories.Location = New System.Drawing.Point(11, 22)
        Me.chkIncludeSubdirectories.Name = "chkIncludeSubdirectories"
        Me.chkIncludeSubdirectories.Size = New System.Drawing.Size(129, 17)
        Me.chkIncludeSubdirectories.TabIndex = 14
        Me.chkIncludeSubdirectories.Text = "Include subdirectories"
        Me.chkIncludeSubdirectories.UseVisualStyleBackColor = True
        '
        'dgvExemption
        '
        Me.dgvExemption.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvExemption.Location = New System.Drawing.Point(12, 216)
        Me.dgvExemption.Name = "dgvExemption"
        Me.dgvExemption.Size = New System.Drawing.Size(333, 133)
        Me.dgvExemption.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 200)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Exemptions"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkAddFiles)
        Me.GroupBox1.Controls.Add(Me.chkRemoveFiles)
        Me.GroupBox1.Controls.Add(Me.chkReplaceFiles)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 99)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(122, 94)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Operations"
        '
        'frmTask
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(444, 361)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dgvExemption)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnManage)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnTargetDirectory)
        Me.Controls.Add(Me.btnSourceDirectory)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtTargetDirectory)
        Me.Controls.Add(Me.txtSourceDirectory)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTask"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmTask"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.dgvExemption, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtSourceDirectory As System.Windows.Forms.TextBox
    Friend WithEvents txtTargetDirectory As System.Windows.Forms.TextBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnManage As System.Windows.Forms.Button
    Friend WithEvents btnSourceDirectory As System.Windows.Forms.Button
    Friend WithEvents btnTargetDirectory As System.Windows.Forms.Button
    Friend WithEvents chkAddFiles As System.Windows.Forms.CheckBox
    Friend WithEvents chkRemoveFiles As System.Windows.Forms.CheckBox
    Friend WithEvents chkReplaceFiles As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvExemption As System.Windows.Forms.DataGridView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkIncludeSubdirectories As System.Windows.Forms.CheckBox
    Friend WithEvents chkExcludeHiddenFiles As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
