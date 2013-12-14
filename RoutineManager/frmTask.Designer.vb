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
        Me.txtDestinationDirectory = New System.Windows.Forms.TextBox()
        Me.chkAddFiles = New System.Windows.Forms.CheckBox()
        Me.chkRemoveFiles = New System.Windows.Forms.CheckBox()
        Me.chkReplaceFiles = New System.Windows.Forms.CheckBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnManage = New System.Windows.Forms.Button()
        Me.btnSourceDirectory = New System.Windows.Forms.Button()
        Me.btnDestinationDirectory = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.dgvExemption = New System.Windows.Forms.DataGridView()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkSearchRecursively = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkExcludeHiddenFiles = New System.Windows.Forms.CheckBox()
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
        'txtDestinationDirectory
        '
        Me.txtDestinationDirectory.Location = New System.Drawing.Point(12, 69)
        Me.txtDestinationDirectory.Name = "txtDestinationDirectory"
        Me.txtDestinationDirectory.Size = New System.Drawing.Size(390, 20)
        Me.txtDestinationDirectory.TabIndex = 1
        '
        'chkAddFiles
        '
        Me.chkAddFiles.AutoSize = True
        Me.chkAddFiles.Location = New System.Drawing.Point(9, 20)
        Me.chkAddFiles.Name = "chkAddFiles"
        Me.chkAddFiles.Size = New System.Drawing.Size(45, 17)
        Me.chkAddFiles.TabIndex = 2
        Me.chkAddFiles.Text = "Add"
        Me.chkAddFiles.UseVisualStyleBackColor = True
        '
        'chkRemoveFiles
        '
        Me.chkRemoveFiles.AutoSize = True
        Me.chkRemoveFiles.Location = New System.Drawing.Point(124, 20)
        Me.chkRemoveFiles.Name = "chkRemoveFiles"
        Me.chkRemoveFiles.Size = New System.Drawing.Size(66, 17)
        Me.chkRemoveFiles.TabIndex = 1
        Me.chkRemoveFiles.Text = "Remove"
        Me.chkRemoveFiles.UseVisualStyleBackColor = True
        '
        'chkReplaceFiles
        '
        Me.chkReplaceFiles.AutoSize = True
        Me.chkReplaceFiles.Location = New System.Drawing.Point(56, 20)
        Me.chkReplaceFiles.Name = "chkReplaceFiles"
        Me.chkReplaceFiles.Size = New System.Drawing.Size(66, 17)
        Me.chkReplaceFiles.TabIndex = 0
        Me.chkReplaceFiles.Text = "Replace"
        Me.chkReplaceFiles.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(357, 213)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(357, 242)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnManage
        '
        Me.btnManage.Location = New System.Drawing.Point(357, 169)
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
        'btnDestinationDirectory
        '
        Me.btnDestinationDirectory.Location = New System.Drawing.Point(408, 67)
        Me.btnDestinationDirectory.Name = "btnDestinationDirectory"
        Me.btnDestinationDirectory.Size = New System.Drawing.Size(24, 23)
        Me.btnDestinationDirectory.TabIndex = 7
        Me.btnDestinationDirectory.Text = "..."
        Me.btnDestinationDirectory.UseVisualStyleBackColor = True
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
        Me.Label2.Size = New System.Drawing.Size(105, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Destination Directory"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkRemoveFiles)
        Me.GroupBox2.Controls.Add(Me.chkReplaceFiles)
        Me.GroupBox2.Controls.Add(Me.chkAddFiles)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 100)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(202, 45)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "File Operations"
        '
        'dgvExemption
        '
        Me.dgvExemption.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvExemption.Location = New System.Drawing.Point(12, 170)
        Me.dgvExemption.Name = "dgvExemption"
        Me.dgvExemption.Size = New System.Drawing.Size(332, 95)
        Me.dgvExemption.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 154)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Exemptions"
        '
        'chkSearchRecursively
        '
        Me.chkSearchRecursively.AutoSize = True
        Me.chkSearchRecursively.Location = New System.Drawing.Point(9, 20)
        Me.chkSearchRecursively.Name = "chkSearchRecursively"
        Me.chkSearchRecursively.Size = New System.Drawing.Size(74, 17)
        Me.chkSearchRecursively.TabIndex = 14
        Me.chkSearchRecursively.Text = "Recursive"
        Me.chkSearchRecursively.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkExcludeHiddenFiles)
        Me.GroupBox1.Controls.Add(Me.chkSearchRecursively)
        Me.GroupBox1.Location = New System.Drawing.Point(225, 100)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(207, 45)
        Me.GroupBox1.TabIndex = 15
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Sync Options"
        '
        'chkExcludeHiddenFiles
        '
        Me.chkExcludeHiddenFiles.AutoSize = True
        Me.chkExcludeHiddenFiles.Location = New System.Drawing.Point(85, 20)
        Me.chkExcludeHiddenFiles.Name = "chkExcludeHiddenFiles"
        Me.chkExcludeHiddenFiles.Size = New System.Drawing.Size(120, 17)
        Me.chkExcludeHiddenFiles.TabIndex = 15
        Me.chkExcludeHiddenFiles.Text = "Exclude hidden files"
        Me.chkExcludeHiddenFiles.UseVisualStyleBackColor = True
        '
        'frmTask
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(444, 277)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dgvExemption)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnManage)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnDestinationDirectory)
        Me.Controls.Add(Me.btnSourceDirectory)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtDestinationDirectory)
        Me.Controls.Add(Me.txtSourceDirectory)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTask"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
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
    Friend WithEvents txtDestinationDirectory As System.Windows.Forms.TextBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnManage As System.Windows.Forms.Button
    Friend WithEvents btnSourceDirectory As System.Windows.Forms.Button
    Friend WithEvents btnDestinationDirectory As System.Windows.Forms.Button
    Friend WithEvents chkAddFiles As System.Windows.Forms.CheckBox
    Friend WithEvents chkRemoveFiles As System.Windows.Forms.CheckBox
    Friend WithEvents chkReplaceFiles As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvExemption As System.Windows.Forms.DataGridView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkSearchRecursively As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkExcludeHiddenFiles As System.Windows.Forms.CheckBox
End Class
