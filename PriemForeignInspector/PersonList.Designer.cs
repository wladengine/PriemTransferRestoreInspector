namespace PriemForeignInspector
{
    partial class PersonList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersonList));
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.tbFIO = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFaculty = new System.Windows.Forms.ComboBox();
            this.cbLicenseProgram = new System.Windows.Forms.ComboBox();
            this.cbObrazProgram = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.chbFaculty = new System.Windows.Forms.CheckBox();
            this.chbLicenseProgram = new System.Windows.Forms.CheckBox();
            this.chbObrazProgram = new System.Windows.Forms.CheckBox();
            this.chbShowHidedProfiles = new System.Windows.Forms.CheckBox();
            this.cbSemester = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbStudyLevel = new System.Windows.Forms.ComboBox();
            this.chbTransfer = new System.Windows.Forms.CheckBox();
            this.chbRestore = new System.Windows.Forms.CheckBox();
            this.chbChangeObrazProgram = new System.Windows.Forms.CheckBox();
            this.chbTransferForeign = new System.Windows.Forms.CheckBox();
            this.chbOnlyLastCampaign = new System.Windows.Forms.CheckBox();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.chbChangeStudyBasis = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Location = new System.Drawing.Point(12, 149);
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.Size = new System.Drawing.Size(825, 379);
            this.dgvList.TabIndex = 0;
            this.dgvList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellDoubleClick);
            this.dgvList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvList_CellFormatting);
            // 
            // tbFIO
            // 
            this.tbFIO.Location = new System.Drawing.Point(53, 96);
            this.tbFIO.Name = "tbFIO";
            this.tbFIO.Size = new System.Drawing.Size(313, 20);
            this.tbFIO.TabIndex = 1;
            this.tbFIO.TextChanged += new System.EventHandler(this.tbFIO_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "ФИО";
            // 
            // cbFaculty
            // 
            this.cbFaculty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFaculty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFaculty.Location = new System.Drawing.Point(565, 42);
            this.cbFaculty.Name = "cbFaculty";
            this.cbFaculty.Size = new System.Drawing.Size(249, 21);
            this.cbFaculty.TabIndex = 3;
            this.cbFaculty.SelectedIndexChanged += new System.EventHandler(this.cbFaculty_SelectedIndexChanged);
            // 
            // cbLicenseProgram
            // 
            this.cbLicenseProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLicenseProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLicenseProgram.FormattingEnabled = true;
            this.cbLicenseProgram.Location = new System.Drawing.Point(565, 69);
            this.cbLicenseProgram.Name = "cbLicenseProgram";
            this.cbLicenseProgram.Size = new System.Drawing.Size(249, 21);
            this.cbLicenseProgram.TabIndex = 4;
            this.cbLicenseProgram.SelectedIndexChanged += new System.EventHandler(this.cbLicenseProgram_SelectedIndexChanged);
            // 
            // cbObrazProgram
            // 
            this.cbObrazProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbObrazProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbObrazProgram.FormattingEnabled = true;
            this.cbObrazProgram.Location = new System.Drawing.Point(565, 96);
            this.cbObrazProgram.Name = "cbObrazProgram";
            this.cbObrazProgram.Size = new System.Drawing.Size(249, 21);
            this.cbObrazProgram.TabIndex = 5;
            this.cbObrazProgram.SelectedIndexChanged += new System.EventHandler(this.cbObrazProgram_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(496, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Факультет";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(484, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Направление";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Location = new System.Drawing.Point(461, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 28);
            this.label4.TabIndex = 8;
            this.label4.Text = "Образовательная программа";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(223, 547);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Всего:";
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(269, 547);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(0, 13);
            this.lblCount.TabIndex = 10;
            // 
            // chbFaculty
            // 
            this.chbFaculty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbFaculty.AutoSize = true;
            this.chbFaculty.Location = new System.Drawing.Point(820, 45);
            this.chbFaculty.Name = "chbFaculty";
            this.chbFaculty.Size = new System.Drawing.Size(15, 14);
            this.chbFaculty.TabIndex = 11;
            this.chbFaculty.UseVisualStyleBackColor = true;
            this.chbFaculty.CheckedChanged += new System.EventHandler(this.chbFaculty_CheckedChanged);
            // 
            // chbLicenseProgram
            // 
            this.chbLicenseProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbLicenseProgram.AutoSize = true;
            this.chbLicenseProgram.Enabled = false;
            this.chbLicenseProgram.Location = new System.Drawing.Point(820, 72);
            this.chbLicenseProgram.Name = "chbLicenseProgram";
            this.chbLicenseProgram.Size = new System.Drawing.Size(15, 14);
            this.chbLicenseProgram.TabIndex = 12;
            this.chbLicenseProgram.UseVisualStyleBackColor = true;
            this.chbLicenseProgram.CheckedChanged += new System.EventHandler(this.chbLicenseProgram_CheckedChanged);
            // 
            // chbObrazProgram
            // 
            this.chbObrazProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbObrazProgram.AutoSize = true;
            this.chbObrazProgram.Enabled = false;
            this.chbObrazProgram.Location = new System.Drawing.Point(820, 99);
            this.chbObrazProgram.Name = "chbObrazProgram";
            this.chbObrazProgram.Size = new System.Drawing.Size(15, 14);
            this.chbObrazProgram.TabIndex = 13;
            this.chbObrazProgram.UseVisualStyleBackColor = true;
            this.chbObrazProgram.CheckedChanged += new System.EventHandler(this.chbObrazProgram_CheckedChanged);
            // 
            // chbShowHidedProfiles
            // 
            this.chbShowHidedProfiles.AutoSize = true;
            this.chbShowHidedProfiles.Location = new System.Drawing.Point(53, 126);
            this.chbShowHidedProfiles.Name = "chbShowHidedProfiles";
            this.chbShowHidedProfiles.Size = new System.Drawing.Size(156, 17);
            this.chbShowHidedProfiles.TabIndex = 14;
            this.chbShowHidedProfiles.Text = "Прятать скрытые анкеты";
            this.chbShowHidedProfiles.UseVisualStyleBackColor = true;
            this.chbShowHidedProfiles.CheckedChanged += new System.EventHandler(this.chbShowHidedProfiles_CheckedChanged);
            // 
            // cbSemester
            // 
            this.cbSemester.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSemester.FormattingEnabled = true;
            this.cbSemester.Location = new System.Drawing.Point(565, 12);
            this.cbSemester.Name = "cbSemester";
            this.cbSemester.Size = new System.Drawing.Size(101, 21);
            this.cbSemester.TabIndex = 15;
            this.cbSemester.SelectedIndexChanged += new System.EventHandler(this.cbSemester_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(508, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Семестр";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(672, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Уровень";
            // 
            // cbStudyLevel
            // 
            this.cbStudyLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbStudyLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudyLevel.FormattingEnabled = true;
            this.cbStudyLevel.Location = new System.Drawing.Point(729, 12);
            this.cbStudyLevel.Name = "cbStudyLevel";
            this.cbStudyLevel.Size = new System.Drawing.Size(85, 21);
            this.cbStudyLevel.TabIndex = 17;
            this.cbStudyLevel.SelectedIndexChanged += new System.EventHandler(this.cbStudyLevel_SelectedIndexChanged);
            // 
            // chbTransfer
            // 
            this.chbTransfer.AutoSize = true;
            this.chbTransfer.Location = new System.Drawing.Point(23, 14);
            this.chbTransfer.Name = "chbTransfer";
            this.chbTransfer.Size = new System.Drawing.Size(174, 17);
            this.chbTransfer.TabIndex = 19;
            this.chbTransfer.Text = "Переводящиеся из др. вузов";
            this.chbTransfer.UseVisualStyleBackColor = true;
            this.chbTransfer.CheckedChanged += new System.EventHandler(this.chbTransfer_CheckedChanged);
            // 
            // chbRestore
            // 
            this.chbRestore.AutoSize = true;
            this.chbRestore.Location = new System.Drawing.Point(23, 60);
            this.chbRestore.Name = "chbRestore";
            this.chbRestore.Size = new System.Drawing.Size(145, 17);
            this.chbRestore.TabIndex = 20;
            this.chbRestore.Text = "Восстанавливающиеся";
            this.chbRestore.UseVisualStyleBackColor = true;
            this.chbRestore.CheckedChanged += new System.EventHandler(this.chbRestore_CheckedChanged);
            // 
            // chbChangeObrazProgram
            // 
            this.chbChangeObrazProgram.AutoSize = true;
            this.chbChangeObrazProgram.Location = new System.Drawing.Point(203, 16);
            this.chbChangeObrazProgram.Name = "chbChangeObrazProgram";
            this.chbChangeObrazProgram.Size = new System.Drawing.Size(201, 17);
            this.chbChangeObrazProgram.TabIndex = 21;
            this.chbChangeObrazProgram.Text = "Переводящиеся на др. программу";
            this.chbChangeObrazProgram.UseVisualStyleBackColor = true;
            this.chbChangeObrazProgram.CheckedChanged += new System.EventHandler(this.chbChangeObrazProgram_CheckedChanged);
            // 
            // chbTransferForeign
            // 
            this.chbTransferForeign.AutoSize = true;
            this.chbTransferForeign.Location = new System.Drawing.Point(23, 37);
            this.chbTransferForeign.Name = "chbTransferForeign";
            this.chbTransferForeign.Size = new System.Drawing.Size(171, 17);
            this.chbTransferForeign.TabIndex = 22;
            this.chbTransferForeign.Text = "Переводящиеся из др. гос-в";
            this.chbTransferForeign.UseVisualStyleBackColor = true;
            this.chbTransferForeign.CheckedChanged += new System.EventHandler(this.chbTransferForeign_CheckedChanged);
            // 
            // chbOnlyLastCampaign
            // 
            this.chbOnlyLastCampaign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbOnlyLastCampaign.AutoSize = true;
            this.chbOnlyLastCampaign.Checked = true;
            this.chbOnlyLastCampaign.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbOnlyLastCampaign.Location = new System.Drawing.Point(565, 126);
            this.chbOnlyLastCampaign.Name = "chbOnlyLastCampaign";
            this.chbOnlyLastCampaign.Size = new System.Drawing.Size(173, 17);
            this.chbOnlyLastCampaign.TabIndex = 23;
            this.chbOnlyLastCampaign.Text = "Только последняя кампания";
            this.chbOnlyLastCampaign.UseVisualStyleBackColor = true;
            this.chbOnlyLastCampaign.CheckedChanged += new System.EventHandler(this.chbOnlyLastCampaign_CheckedChanged);
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportToExcel.Location = new System.Drawing.Point(12, 534);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(75, 23);
            this.btnExportToExcel.TabIndex = 24;
            this.btnExportToExcel.Text = "B Excel";
            this.btnExportToExcel.UseVisualStyleBackColor = true;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // chbChangeStudyBasis
            // 
            this.chbChangeStudyBasis.AutoSize = true;
            this.chbChangeStudyBasis.Location = new System.Drawing.Point(203, 39);
            this.chbChangeStudyBasis.Name = "chbChangeStudyBasis";
            this.chbChangeStudyBasis.Size = new System.Drawing.Size(240, 17);
            this.chbChangeStudyBasis.TabIndex = 25;
            this.chbChangeStudyBasis.Text = "Перевод с платной основы на бюджетную";
            this.chbChangeStudyBasis.UseVisualStyleBackColor = true;
            this.chbChangeStudyBasis.CheckedChanged += new System.EventHandler(this.chbChangeStudyBasis_CheckedChanged);
            // 
            // PersonList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 569);
            this.Controls.Add(this.chbChangeStudyBasis);
            this.Controls.Add(this.btnExportToExcel);
            this.Controls.Add(this.chbOnlyLastCampaign);
            this.Controls.Add(this.chbTransferForeign);
            this.Controls.Add(this.chbChangeObrazProgram);
            this.Controls.Add(this.chbRestore);
            this.Controls.Add(this.chbTransfer);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbStudyLevel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbSemester);
            this.Controls.Add(this.chbShowHidedProfiles);
            this.Controls.Add(this.chbObrazProgram);
            this.Controls.Add(this.chbLicenseProgram);
            this.Controls.Add(this.chbFaculty);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbObrazProgram);
            this.Controls.Add(this.cbLicenseProgram);
            this.Controls.Add(this.cbFaculty);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbFIO);
            this.Controls.Add(this.dgvList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(865, 607);
            this.Name = "PersonList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Список абитуриентов, подавших заявления в Интернете";
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.TextBox tbFIO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFaculty;
        private System.Windows.Forms.ComboBox cbLicenseProgram;
        private System.Windows.Forms.ComboBox cbObrazProgram;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.CheckBox chbFaculty;
        private System.Windows.Forms.CheckBox chbLicenseProgram;
        private System.Windows.Forms.CheckBox chbObrazProgram;
        private System.Windows.Forms.CheckBox chbShowHidedProfiles;
        private System.Windows.Forms.ComboBox cbSemester;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbStudyLevel;
        private System.Windows.Forms.CheckBox chbTransfer;
        private System.Windows.Forms.CheckBox chbRestore;
        private System.Windows.Forms.CheckBox chbChangeObrazProgram;
        private System.Windows.Forms.CheckBox chbTransferForeign;
        private System.Windows.Forms.CheckBox chbOnlyLastCampaign;
        private System.Windows.Forms.Button btnExportToExcel;
        private System.Windows.Forms.CheckBox chbChangeStudyBasis;
    }
}