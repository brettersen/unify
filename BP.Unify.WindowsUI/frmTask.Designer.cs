namespace BP.Unify.WindowsUI
{
    partial class frmTask
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Label3 = new System.Windows.Forms.Label();
            this.dgvExemption = new System.Windows.Forms.DataGridView();
            this.btnDemote = new System.Windows.Forms.Button();
            this.btnPromote = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAddFiles = new System.Windows.Forms.CheckBox();
            this.chkRemoveFiles = new System.Windows.Forms.CheckBox();
            this.chkReplaceFiles = new System.Windows.Forms.CheckBox();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.chkCompareFilesInDepth = new System.Windows.Forms.CheckBox();
            this.chkExcludeHiddenFiles = new System.Windows.Forms.CheckBox();
            this.chkExcludeSubdirectories = new System.Windows.Forms.CheckBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.btnTargetDirectory = new System.Windows.Forms.Button();
            this.btnSourceDirectory = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtTargetDirectory = new System.Windows.Forms.TextBox();
            this.txtSourceDirectory = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExemption)).BeginInit();
            this.GroupBox1.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(10, 202);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(61, 13);
            this.Label3.TabIndex = 24;
            this.Label3.Text = "Exemptions";
            // 
            // dgvExemption
            // 
            this.dgvExemption.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExemption.Location = new System.Drawing.Point(13, 250);
            this.dgvExemption.Name = "dgvExemption";
            this.dgvExemption.Size = new System.Drawing.Size(399, 135);
            this.dgvExemption.TabIndex = 34;
            // 
            // btnDemote
            // 
            this.btnDemote.Location = new System.Drawing.Point(337, 220);
            this.btnDemote.Name = "btnDemote";
            this.btnDemote.Size = new System.Drawing.Size(75, 23);
            this.btnDemote.TabIndex = 33;
            this.btnDemote.Text = "Demote";
            this.btnDemote.UseVisualStyleBackColor = true;
            // 
            // btnPromote
            // 
            this.btnPromote.Location = new System.Drawing.Point(256, 220);
            this.btnPromote.Name = "btnPromote";
            this.btnPromote.Size = new System.Drawing.Size(75, 23);
            this.btnPromote.TabIndex = 32;
            this.btnPromote.Text = "Promote";
            this.btnPromote.UseVisualStyleBackColor = true;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(175, 220);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 31;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(94, 220);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 30;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(13, 220);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 29;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.chkAddFiles);
            this.GroupBox1.Controls.Add(this.chkRemoveFiles);
            this.GroupBox1.Controls.Add(this.chkReplaceFiles);
            this.GroupBox1.Location = new System.Drawing.Point(13, 100);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(122, 94);
            this.GroupBox1.TabIndex = 25;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Operations";
            // 
            // chkAddFiles
            // 
            this.chkAddFiles.AutoSize = true;
            this.chkAddFiles.Checked = true;
            this.chkAddFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAddFiles.Location = new System.Drawing.Point(11, 22);
            this.chkAddFiles.Name = "chkAddFiles";
            this.chkAddFiles.Size = new System.Drawing.Size(66, 17);
            this.chkAddFiles.TabIndex = 5;
            this.chkAddFiles.Text = "Add files";
            this.chkAddFiles.UseVisualStyleBackColor = true;
            // 
            // chkRemoveFiles
            // 
            this.chkRemoveFiles.AutoSize = true;
            this.chkRemoveFiles.Checked = true;
            this.chkRemoveFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRemoveFiles.Location = new System.Drawing.Point(11, 68);
            this.chkRemoveFiles.Name = "chkRemoveFiles";
            this.chkRemoveFiles.Size = new System.Drawing.Size(87, 17);
            this.chkRemoveFiles.TabIndex = 7;
            this.chkRemoveFiles.Text = "Remove files";
            this.chkRemoveFiles.UseVisualStyleBackColor = true;
            // 
            // chkReplaceFiles
            // 
            this.chkReplaceFiles.AutoSize = true;
            this.chkReplaceFiles.Checked = true;
            this.chkReplaceFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReplaceFiles.Location = new System.Drawing.Point(11, 45);
            this.chkReplaceFiles.Name = "chkReplaceFiles";
            this.chkReplaceFiles.Size = new System.Drawing.Size(87, 17);
            this.chkReplaceFiles.TabIndex = 6;
            this.chkReplaceFiles.Text = "Replace files";
            this.chkReplaceFiles.UseVisualStyleBackColor = true;
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.chkCompareFilesInDepth);
            this.GroupBox2.Controls.Add(this.chkExcludeHiddenFiles);
            this.GroupBox2.Controls.Add(this.chkExcludeSubdirectories);
            this.GroupBox2.Location = new System.Drawing.Point(149, 100);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(263, 94);
            this.GroupBox2.TabIndex = 27;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Options";
            // 
            // chkCompareFilesInDepth
            // 
            this.chkCompareFilesInDepth.AutoSize = true;
            this.chkCompareFilesInDepth.Location = new System.Drawing.Point(11, 68);
            this.chkCompareFilesInDepth.Name = "chkCompareFilesInDepth";
            this.chkCompareFilesInDepth.Size = new System.Drawing.Size(130, 17);
            this.chkCompareFilesInDepth.TabIndex = 11;
            this.chkCompareFilesInDepth.Text = "Compare files in depth";
            this.chkCompareFilesInDepth.UseVisualStyleBackColor = true;
            // 
            // chkExcludeHiddenFiles
            // 
            this.chkExcludeHiddenFiles.AutoSize = true;
            this.chkExcludeHiddenFiles.Location = new System.Drawing.Point(11, 45);
            this.chkExcludeHiddenFiles.Name = "chkExcludeHiddenFiles";
            this.chkExcludeHiddenFiles.Size = new System.Drawing.Size(120, 17);
            this.chkExcludeHiddenFiles.TabIndex = 10;
            this.chkExcludeHiddenFiles.Text = "Exclude hidden files";
            this.chkExcludeHiddenFiles.UseVisualStyleBackColor = true;
            // 
            // chkExcludeSubdirectories
            // 
            this.chkExcludeSubdirectories.AutoSize = true;
            this.chkExcludeSubdirectories.Location = new System.Drawing.Point(11, 22);
            this.chkExcludeSubdirectories.Name = "chkExcludeSubdirectories";
            this.chkExcludeSubdirectories.Size = new System.Drawing.Size(132, 17);
            this.chkExcludeSubdirectories.TabIndex = 9;
            this.chkExcludeSubdirectories.Text = "Exclude subdirectories";
            this.chkExcludeSubdirectories.UseVisualStyleBackColor = true;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(10, 54);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(83, 13);
            this.Label2.TabIndex = 28;
            this.Label2.Text = "Target Directory";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(10, 10);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(86, 13);
            this.Label1.TabIndex = 26;
            this.Label1.Text = "Source Directory";
            // 
            // btnTargetDirectory
            // 
            this.btnTargetDirectory.Location = new System.Drawing.Point(388, 68);
            this.btnTargetDirectory.Name = "btnTargetDirectory";
            this.btnTargetDirectory.Size = new System.Drawing.Size(24, 23);
            this.btnTargetDirectory.TabIndex = 23;
            this.btnTargetDirectory.Text = "...";
            this.btnTargetDirectory.UseVisualStyleBackColor = true;
            // 
            // btnSourceDirectory
            // 
            this.btnSourceDirectory.Location = new System.Drawing.Point(388, 24);
            this.btnSourceDirectory.Name = "btnSourceDirectory";
            this.btnSourceDirectory.Size = new System.Drawing.Size(24, 23);
            this.btnSourceDirectory.TabIndex = 21;
            this.btnSourceDirectory.Text = "...";
            this.btnSourceDirectory.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(337, 399);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 36;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(256, 399);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 35;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // txtTargetDirectory
            // 
            this.txtTargetDirectory.Location = new System.Drawing.Point(13, 70);
            this.txtTargetDirectory.Name = "txtTargetDirectory";
            this.txtTargetDirectory.Size = new System.Drawing.Size(369, 20);
            this.txtTargetDirectory.TabIndex = 22;
            // 
            // txtSourceDirectory
            // 
            this.txtSourceDirectory.Location = new System.Drawing.Point(13, 26);
            this.txtSourceDirectory.Name = "txtSourceDirectory";
            this.txtSourceDirectory.Size = new System.Drawing.Size(369, 20);
            this.txtSourceDirectory.TabIndex = 20;
            // 
            // frmTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 433);
            this.ControlBox = false;
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.dgvExemption);
            this.Controls.Add(this.btnDemote);
            this.Controls.Add(this.btnPromote);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.btnTargetDirectory);
            this.Controls.Add(this.btnSourceDirectory);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtTargetDirectory);
            this.Controls.Add(this.txtSourceDirectory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTask";
            this.Text = "frmTask";
            ((System.ComponentModel.ISupportInitialize)(this.dgvExemption)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.DataGridView dgvExemption;
        internal System.Windows.Forms.Button btnDemote;
        internal System.Windows.Forms.Button btnPromote;
        internal System.Windows.Forms.Button btnRemove;
        internal System.Windows.Forms.Button btnEdit;
        internal System.Windows.Forms.Button btnAdd;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.CheckBox chkAddFiles;
        internal System.Windows.Forms.CheckBox chkRemoveFiles;
        internal System.Windows.Forms.CheckBox chkReplaceFiles;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.CheckBox chkCompareFilesInDepth;
        internal System.Windows.Forms.CheckBox chkExcludeHiddenFiles;
        internal System.Windows.Forms.CheckBox chkExcludeSubdirectories;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button btnTargetDirectory;
        internal System.Windows.Forms.Button btnSourceDirectory;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Button btnOK;
        internal System.Windows.Forms.TextBox txtTargetDirectory;
        internal System.Windows.Forms.TextBox txtSourceDirectory;
    }
}