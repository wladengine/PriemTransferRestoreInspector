namespace PriemForeignInspector
{
    partial class AbiturientCard
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbSemester = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cbStudyBasis = new System.Windows.Forms.ComboBox();
            this.cbStudyForm = new System.Windows.Forms.ComboBox();
            this.cbStudyLevel = new System.Windows.Forms.ComboBox();
            this.cbFaculty = new System.Windows.Forms.ComboBox();
            this.cbProfile = new System.Windows.Forms.ComboBox();
            this.cbObrazProgram = new System.Windows.Forms.ComboBox();
            this.cbLicenseProgram = new System.Windows.Forms.ComboBox();
            this.btnPersonCard = new System.Windows.Forms.Button();
            this.btnEmail = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnApproveApplication = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbAppStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btnIsBad = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvFiles = new System.Windows.Forms.DataGridView();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnApproveFile = new System.Windows.Forms.Button();
            this.lblFIO = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbSemester);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.cbStudyBasis);
            this.groupBox2.Controls.Add(this.cbStudyForm);
            this.groupBox2.Controls.Add(this.cbStudyLevel);
            this.groupBox2.Controls.Add(this.cbFaculty);
            this.groupBox2.Controls.Add(this.cbProfile);
            this.groupBox2.Controls.Add(this.cbObrazProgram);
            this.groupBox2.Controls.Add(this.cbLicenseProgram);
            this.groupBox2.Controls.Add(this.btnPersonCard);
            this.groupBox2.Controls.Add(this.btnEmail);
            this.groupBox2.Controls.Add(this.btnPrint);
            this.groupBox2.Controls.Add(this.btnApproveApplication);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tbAppStatus);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Location = new System.Drawing.Point(21, 62);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(404, 393);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Данные заявления";
            // 
            // cbSemester
            // 
            this.cbSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSemester.FormattingEnabled = true;
            this.cbSemester.Location = new System.Drawing.Point(142, 46);
            this.cbSemester.Name = "cbSemester";
            this.cbSemester.Size = new System.Drawing.Size(187, 21);
            this.cbSemester.TabIndex = 43;
            this.cbSemester.SelectedIndexChanged += new System.EventHandler(this.cbSemester_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(85, 49);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 13);
            this.label15.TabIndex = 42;
            this.label15.Text = "Семестр";
            // 
            // cbStudyBasis
            // 
            this.cbStudyBasis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudyBasis.FormattingEnabled = true;
            this.cbStudyBasis.Location = new System.Drawing.Point(142, 124);
            this.cbStudyBasis.Name = "cbStudyBasis";
            this.cbStudyBasis.Size = new System.Drawing.Size(187, 21);
            this.cbStudyBasis.TabIndex = 40;
            this.cbStudyBasis.SelectedIndexChanged += new System.EventHandler(this.cbStudyBasis_SelectedIndexChanged);
            // 
            // cbStudyForm
            // 
            this.cbStudyForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudyForm.FormattingEnabled = true;
            this.cbStudyForm.Location = new System.Drawing.Point(142, 97);
            this.cbStudyForm.Name = "cbStudyForm";
            this.cbStudyForm.Size = new System.Drawing.Size(187, 21);
            this.cbStudyForm.TabIndex = 39;
            this.cbStudyForm.SelectedIndexChanged += new System.EventHandler(this.cbStudyForm_SelectedIndexChanged);
            // 
            // cbStudyLevel
            // 
            this.cbStudyLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudyLevel.FormattingEnabled = true;
            this.cbStudyLevel.Location = new System.Drawing.Point(142, 19);
            this.cbStudyLevel.Name = "cbStudyLevel";
            this.cbStudyLevel.Size = new System.Drawing.Size(187, 21);
            this.cbStudyLevel.TabIndex = 38;
            this.cbStudyLevel.SelectedIndexChanged += new System.EventHandler(this.cbStudyLevel_SelectedIndexChanged);
            // 
            // cbFaculty
            // 
            this.cbFaculty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFaculty.FormattingEnabled = true;
            this.cbFaculty.Location = new System.Drawing.Point(142, 240);
            this.cbFaculty.Name = "cbFaculty";
            this.cbFaculty.Size = new System.Drawing.Size(244, 21);
            this.cbFaculty.TabIndex = 37;
            this.cbFaculty.SelectedIndexChanged += new System.EventHandler(this.cbFaculty_SelectedIndexChanged);
            // 
            // cbProfile
            // 
            this.cbProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProfile.FormattingEnabled = true;
            this.cbProfile.Location = new System.Drawing.Point(142, 213);
            this.cbProfile.Name = "cbProfile";
            this.cbProfile.Size = new System.Drawing.Size(244, 21);
            this.cbProfile.TabIndex = 36;
            this.cbProfile.SelectedIndexChanged += new System.EventHandler(this.cbProfile_SelectedIndexChanged);
            // 
            // cbObrazProgram
            // 
            this.cbObrazProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbObrazProgram.FormattingEnabled = true;
            this.cbObrazProgram.Location = new System.Drawing.Point(142, 186);
            this.cbObrazProgram.Name = "cbObrazProgram";
            this.cbObrazProgram.Size = new System.Drawing.Size(244, 21);
            this.cbObrazProgram.TabIndex = 35;
            this.cbObrazProgram.SelectedIndexChanged += new System.EventHandler(this.cbObrazProgram_SelectedIndexChanged);
            // 
            // cbLicenseProgram
            // 
            this.cbLicenseProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLicenseProgram.FormattingEnabled = true;
            this.cbLicenseProgram.Location = new System.Drawing.Point(142, 159);
            this.cbLicenseProgram.Name = "cbLicenseProgram";
            this.cbLicenseProgram.Size = new System.Drawing.Size(244, 21);
            this.cbLicenseProgram.TabIndex = 34;
            this.cbLicenseProgram.SelectedIndexChanged += new System.EventHandler(this.cbLicenseProgram_SelectedIndexChanged);
            // 
            // btnPersonCard
            // 
            this.btnPersonCard.Location = new System.Drawing.Point(94, 337);
            this.btnPersonCard.Name = "btnPersonCard";
            this.btnPersonCard.Size = new System.Drawing.Size(81, 40);
            this.btnPersonCard.TabIndex = 33;
            this.btnPersonCard.Text = "Карточка абитуриента";
            this.btnPersonCard.UseVisualStyleBackColor = true;
            this.btnPersonCard.Click += new System.EventHandler(this.btnPersonCard_Click);
            // 
            // btnEmail
            // 
            this.btnEmail.Location = new System.Drawing.Point(236, 337);
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Size = new System.Drawing.Size(75, 40);
            this.btnEmail.TabIndex = 32;
            this.btnEmail.Text = "Написать письмо";
            this.btnEmail.UseVisualStyleBackColor = true;
            this.btnEmail.Click += new System.EventHandler(this.btnEmail_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(7, 337);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(81, 40);
            this.btnPrint.TabIndex = 31;
            this.btnPrint.Text = "Распечатать заявление";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnApproveApplication
            // 
            this.btnApproveApplication.Location = new System.Drawing.Point(317, 337);
            this.btnApproveApplication.Name = "btnApproveApplication";
            this.btnApproveApplication.Size = new System.Drawing.Size(81, 40);
            this.btnApproveApplication.TabIndex = 30;
            this.btnApproveApplication.Text = "Одобрить заявление";
            this.btnApproveApplication.UseVisualStyleBackColor = true;
            this.btnApproveApplication.Click += new System.EventHandler(this.btnApproveApplication_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 299);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Статус заявления";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 162);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Направление";
            // 
            // tbAppStatus
            // 
            this.tbAppStatus.AutoSize = true;
            this.tbAppStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbAppStatus.Location = new System.Drawing.Point(139, 297);
            this.tbAppStatus.Name = "tbAppStatus";
            this.tbAppStatus.Size = new System.Drawing.Size(142, 16);
            this.tbAppStatus.TabIndex = 28;
            this.tbAppStatus.Text = "Подано/Отозвано";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(83, 216);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Профиль";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(73, 243);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Факультет";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Форма обучения";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Основа обучения";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(120, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "Уровень образования";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(32, 182);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(104, 26);
            this.label12.TabIndex = 21;
            this.label12.Text = "Образовательная программа";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.btnIsBad);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dgvFiles);
            this.groupBox1.Controls.Add(this.btnOpenFile);
            this.groupBox1.Controls.Add(this.btnApproveFile);
            this.groupBox1.Location = new System.Drawing.Point(431, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(562, 393);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Портфолио";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(493, 234);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 27);
            this.label13.TabIndex = 19;
            this.label13.Text = "Файл не одобрен";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Tomato;
            this.label14.Location = new System.Drawing.Point(474, 234);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(13, 13);
            this.label14.TabIndex = 18;
            this.label14.Text = "  ";
            // 
            // btnIsBad
            // 
            this.btnIsBad.Location = new System.Drawing.Point(467, 112);
            this.btnIsBad.Name = "btnIsBad";
            this.btnIsBad.Size = new System.Drawing.Size(81, 40);
            this.btnIsBad.TabIndex = 17;
            this.btnIsBad.Text = "Статус \"Плохой\"";
            this.btnIsBad.UseVisualStyleBackColor = true;
            this.btnIsBad.Click += new System.EventHandler(this.btnIsBad_Click);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(493, 206);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 27);
            this.label10.TabIndex = 16;
            this.label10.Text = "Файл проверен";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(493, 176);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 30);
            this.label9.TabIndex = 15;
            this.label9.Text = "Файл не проверен";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Green;
            this.label8.Location = new System.Drawing.Point(474, 206);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "  ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.LightYellow;
            this.label5.Location = new System.Drawing.Point(474, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "  ";
            // 
            // dgvFiles
            // 
            this.dgvFiles.AllowUserToAddRows = false;
            this.dgvFiles.AllowUserToDeleteRows = false;
            this.dgvFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiles.Location = new System.Drawing.Point(11, 19);
            this.dgvFiles.Name = "dgvFiles";
            this.dgvFiles.ReadOnly = true;
            this.dgvFiles.RowHeadersVisible = false;
            this.dgvFiles.Size = new System.Drawing.Size(441, 358);
            this.dgvFiles.TabIndex = 12;
            this.dgvFiles.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFiles_CellDoubleClick);
            this.dgvFiles.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvFiles_CellFormatting);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(467, 20);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(81, 40);
            this.btnOpenFile.TabIndex = 10;
            this.btnOpenFile.Text = "Открыть файл";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnApproveFile
            // 
            this.btnApproveFile.Location = new System.Drawing.Point(467, 66);
            this.btnApproveFile.Name = "btnApproveFile";
            this.btnApproveFile.Size = new System.Drawing.Size(81, 40);
            this.btnApproveFile.TabIndex = 11;
            this.btnApproveFile.Text = "Статус \"Проверен\"";
            this.btnApproveFile.UseVisualStyleBackColor = true;
            this.btnApproveFile.Click += new System.EventHandler(this.btnApproveFile_Click);
            // 
            // lblFIO
            // 
            this.lblFIO.AutoSize = true;
            this.lblFIO.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFIO.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblFIO.Location = new System.Drawing.Point(31, 18);
            this.lblFIO.Name = "lblFIO";
            this.lblFIO.Size = new System.Drawing.Size(54, 24);
            this.lblFIO.TabIndex = 31;
            this.lblFIO.Text = "ФИО";
            this.lblFIO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(21, 461);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 34;
            this.btnSave.Text = "Изменить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // AbiturientCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 491);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblFIO);
            this.MaximizeBox = false;
            this.Name = "AbiturientCard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AbiturientCard";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnPersonCard;
        private System.Windows.Forms.Button btnEmail;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnApproveApplication;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label tbAppStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnIsBad;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvFiles;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Button btnApproveFile;
        private System.Windows.Forms.Label lblFIO;
        private System.Windows.Forms.ComboBox cbStudyBasis;
        private System.Windows.Forms.ComboBox cbStudyForm;
        private System.Windows.Forms.ComboBox cbStudyLevel;
        private System.Windows.Forms.ComboBox cbFaculty;
        private System.Windows.Forms.ComboBox cbProfile;
        private System.Windows.Forms.ComboBox cbObrazProgram;
        private System.Windows.Forms.ComboBox cbLicenseProgram;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cbSemester;
        private System.Windows.Forms.Label label15;

    }
}