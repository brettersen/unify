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
        Me.btnSourceDirectory = New System.Windows.Forms.Button()
        Me.btnTargetDirectory = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkCompareFilesInDepth = New System.Windows.Forms.CheckBox()
        Me.chkExcludeHiddenFiles = New System.Windows.Forms.CheckBox()
        Me.chkExcludeSubdirectories = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnDemote = New System.Windows.Forms.Button()
        Me.btnPromote = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.dgvExemption = New System.Windows.Forms.DataGridView()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.msMain = New System.Windows.Forms.MenuStrip()
        Me.tsmiFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiOpen = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiClose = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiSaveAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvExemption, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.msMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtSourceDirectory
        '
        Me.txtSourceDirectory.Location = New System.Drawing.Point(12, 48)
        Me.txtSourceDirectory.Name = "txtSourceDirectory"
        Me.txtSourceDirectory.Size = New System.Drawing.Size(370, 20)
        Me.txtSourceDirectory.TabIndex = 0
        '
        'txtTargetDirectory
        '
        Me.txtTargetDirectory.Location = New System.Drawing.Point(12, 92)
        Me.txtTargetDirectory.Name = "txtTargetDirectory"
        Me.txtTargetDirectory.Size = New System.Drawing.Size(370, 20)
        Me.txtTargetDirectory.TabIndex = 2
        '
        'chkAddFiles
        '
        Me.chkAddFiles.AutoSize = True
        Me.chkAddFiles.Checked = True
        Me.chkAddFiles.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAddFiles.Location = New System.Drawing.Point(11, 22)
        Me.chkAddFiles.Name = "chkAddFiles"
        Me.chkAddFiles.Size = New System.Drawing.Size(66, 17)
        Me.chkAddFiles.TabIndex = 5
        Me.chkAddFiles.Text = "Add files"
        Me.chkAddFiles.UseVisualStyleBackColor = True
        '
        'chkRemoveFiles
        '
        Me.chkRemoveFiles.AutoSize = True
        Me.chkRemoveFiles.Checked = True
        Me.chkRemoveFiles.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRemoveFiles.Location = New System.Drawing.Point(11, 68)
        Me.chkRemoveFiles.Name = "chkRemoveFiles"
        Me.chkRemoveFiles.Size = New System.Drawing.Size(87, 17)
        Me.chkRemoveFiles.TabIndex = 7
        Me.chkRemoveFiles.Text = "Remove files"
        Me.chkRemoveFiles.UseVisualStyleBackColor = True
        '
        'chkReplaceFiles
        '
        Me.chkReplaceFiles.AutoSize = True
        Me.chkReplaceFiles.Checked = True
        Me.chkReplaceFiles.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkReplaceFiles.Location = New System.Drawing.Point(11, 45)
        Me.chkReplaceFiles.Name = "chkReplaceFiles"
        Me.chkReplaceFiles.Size = New System.Drawing.Size(87, 17)
        Me.chkReplaceFiles.TabIndex = 6
        Me.chkReplaceFiles.Text = "Replace files"
        Me.chkReplaceFiles.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(255, 438)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 18
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(336, 438)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 19
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSourceDirectory
        '
        Me.btnSourceDirectory.Location = New System.Drawing.Point(388, 46)
        Me.btnSourceDirectory.Name = "btnSourceDirectory"
        Me.btnSourceDirectory.Size = New System.Drawing.Size(24, 23)
        Me.btnSourceDirectory.TabIndex = 1
        Me.btnSourceDirectory.Text = "..."
        Me.btnSourceDirectory.UseVisualStyleBackColor = True
        '
        'btnTargetDirectory
        '
        Me.btnTargetDirectory.Location = New System.Drawing.Point(388, 90)
        Me.btnTargetDirectory.Name = "btnTargetDirectory"
        Me.btnTargetDirectory.Size = New System.Drawing.Size(24, 23)
        Me.btnTargetDirectory.TabIndex = 3
        Me.btnTargetDirectory.Text = "..."
        Me.btnTargetDirectory.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Source Directory"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 76)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Target Directory"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkCompareFilesInDepth)
        Me.GroupBox2.Controls.Add(Me.chkExcludeHiddenFiles)
        Me.GroupBox2.Controls.Add(Me.chkExcludeSubdirectories)
        Me.GroupBox2.Location = New System.Drawing.Point(148, 122)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(264, 94)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Options"
        '
        'chkCompareFilesInDepth
        '
        Me.chkCompareFilesInDepth.AutoSize = True
        Me.chkCompareFilesInDepth.Location = New System.Drawing.Point(11, 68)
        Me.chkCompareFilesInDepth.Name = "chkCompareFilesInDepth"
        Me.chkCompareFilesInDepth.Size = New System.Drawing.Size(130, 17)
        Me.chkCompareFilesInDepth.TabIndex = 11
        Me.chkCompareFilesInDepth.Text = "Compare files in depth"
        Me.chkCompareFilesInDepth.UseVisualStyleBackColor = True
        '
        'chkExcludeHiddenFiles
        '
        Me.chkExcludeHiddenFiles.AutoSize = True
        Me.chkExcludeHiddenFiles.Location = New System.Drawing.Point(11, 45)
        Me.chkExcludeHiddenFiles.Name = "chkExcludeHiddenFiles"
        Me.chkExcludeHiddenFiles.Size = New System.Drawing.Size(120, 17)
        Me.chkExcludeHiddenFiles.TabIndex = 10
        Me.chkExcludeHiddenFiles.Text = "Exclude hidden files"
        Me.chkExcludeHiddenFiles.UseVisualStyleBackColor = True
        '
        'chkExcludeSubdirectories
        '
        Me.chkExcludeSubdirectories.AutoSize = True
        Me.chkExcludeSubdirectories.Location = New System.Drawing.Point(11, 22)
        Me.chkExcludeSubdirectories.Name = "chkExcludeSubdirectories"
        Me.chkExcludeSubdirectories.Size = New System.Drawing.Size(132, 17)
        Me.chkExcludeSubdirectories.TabIndex = 9
        Me.chkExcludeSubdirectories.Text = "Exclude subdirectories"
        Me.chkExcludeSubdirectories.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkAddFiles)
        Me.GroupBox1.Controls.Add(Me.chkRemoveFiles)
        Me.GroupBox1.Controls.Add(Me.chkReplaceFiles)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 122)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(122, 94)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Operations"
        '
        'btnDemote
        '
        Me.btnDemote.Location = New System.Drawing.Point(337, 390)
        Me.btnDemote.Name = "btnDemote"
        Me.btnDemote.Size = New System.Drawing.Size(75, 23)
        Me.btnDemote.TabIndex = 16
        Me.btnDemote.Text = "Demote"
        Me.btnDemote.UseVisualStyleBackColor = True
        '
        'btnPromote
        '
        Me.btnPromote.Location = New System.Drawing.Point(255, 390)
        Me.btnPromote.Name = "btnPromote"
        Me.btnPromote.Size = New System.Drawing.Size(75, 23)
        Me.btnPromote.TabIndex = 15
        Me.btnPromote.Text = "Promote"
        Me.btnPromote.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(174, 390)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(75, 23)
        Me.btnRemove.TabIndex = 14
        Me.btnRemove.Text = "Remove"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(93, 390)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(75, 23)
        Me.btnEdit.TabIndex = 13
        Me.btnEdit.Text = "Edit"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(12, 390)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 12
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'dgvExemption
        '
        Me.dgvExemption.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvExemption.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvExemption.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvExemption.Location = New System.Drawing.Point(12, 243)
        Me.dgvExemption.Name = "dgvExemption"
        Me.dgvExemption.Size = New System.Drawing.Size(400, 135)
        Me.dgvExemption.TabIndex = 17
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 227)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Exemptions"
        '
        'msMain
        '
        Me.msMain.BackColor = System.Drawing.SystemColors.Control
        Me.msMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiFile, Me.HelpToolStripMenuItem})
        Me.msMain.Location = New System.Drawing.Point(0, 0)
        Me.msMain.Name = "msMain"
        Me.msMain.Size = New System.Drawing.Size(424, 24)
        Me.msMain.TabIndex = 21
        Me.msMain.Text = "MenuStrip1"
        '
        'tsmiFile
        '
        Me.tsmiFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiNew, Me.ToolStripSeparator4, Me.tsmiOpen, Me.ToolStripSeparator3, Me.tsmiClose, Me.ToolStripSeparator2, Me.tsmiSave, Me.tsmiSaveAs, Me.ToolStripSeparator1, Me.tsmiExit})
        Me.tsmiFile.Name = "tsmiFile"
        Me.tsmiFile.Size = New System.Drawing.Size(37, 20)
        Me.tsmiFile.Text = "&File"
        '
        'tsmiNew
        '
        Me.tsmiNew.Name = "tsmiNew"
        Me.tsmiNew.Size = New System.Drawing.Size(152, 22)
        Me.tsmiNew.Text = "&New"
        '
        'tsmiOpen
        '
        Me.tsmiOpen.Name = "tsmiOpen"
        Me.tsmiOpen.Size = New System.Drawing.Size(152, 22)
        Me.tsmiOpen.Text = "&Open..."
        '
        'tsmiClose
        '
        Me.tsmiClose.Name = "tsmiClose"
        Me.tsmiClose.Size = New System.Drawing.Size(152, 22)
        Me.tsmiClose.Text = "&Close"
        '
        'tsmiSave
        '
        Me.tsmiSave.Name = "tsmiSave"
        Me.tsmiSave.Size = New System.Drawing.Size(152, 22)
        Me.tsmiSave.Text = "&Save"
        '
        'tsmiSaveAs
        '
        Me.tsmiSaveAs.Name = "tsmiSaveAs"
        Me.tsmiSaveAs.Size = New System.Drawing.Size(152, 22)
        Me.tsmiSaveAs.Text = "Save &As..."
        '
        'tsmiExit
        '
        Me.tsmiExit.Name = "tsmiExit"
        Me.tsmiExit.Size = New System.Drawing.Size(152, 22)
        Me.tsmiExit.Text = "E&xit"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(149, 6)
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(149, 6)
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(149, 6)
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(149, 6)
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "&Help"
        '
        'frmTask
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(424, 473)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dgvExemption)
        Me.Controls.Add(Me.btnDemote)
        Me.Controls.Add(Me.btnPromote)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnTargetDirectory)
        Me.Controls.Add(Me.btnSourceDirectory)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtTargetDirectory)
        Me.Controls.Add(Me.txtSourceDirectory)
        Me.Controls.Add(Me.msMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MainMenuStrip = Me.msMain
        Me.MaximizeBox = False
        Me.Name = "frmTask"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmTask"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvExemption, System.ComponentModel.ISupportInitialize).EndInit()
        Me.msMain.ResumeLayout(False)
        Me.msMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtSourceDirectory As System.Windows.Forms.TextBox
    Friend WithEvents txtTargetDirectory As System.Windows.Forms.TextBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSourceDirectory As System.Windows.Forms.Button
    Friend WithEvents btnTargetDirectory As System.Windows.Forms.Button
    Friend WithEvents chkAddFiles As System.Windows.Forms.CheckBox
    Friend WithEvents chkRemoveFiles As System.Windows.Forms.CheckBox
    Friend WithEvents chkReplaceFiles As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkExcludeSubdirectories As System.Windows.Forms.CheckBox
    Friend WithEvents chkExcludeHiddenFiles As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkCompareFilesInDepth As System.Windows.Forms.CheckBox
    Friend WithEvents btnDemote As System.Windows.Forms.Button
    Friend WithEvents btnPromote As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents dgvExemption As System.Windows.Forms.DataGridView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents msMain As System.Windows.Forms.MenuStrip
    Friend WithEvents tsmiFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmiOpen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmiClose As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmiSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiSaveAs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmiExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
