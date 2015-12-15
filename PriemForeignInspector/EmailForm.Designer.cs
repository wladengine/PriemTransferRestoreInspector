namespace PriemForeignInspector
{
    partial class EmailForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.cbEmailFrom = new System.Windows.Forms.ComboBox();
            this.tbEmailTo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTheme = new System.Windows.Forms.TextBox();
            this.tbText = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Отправитель";
            // 
            // cbEmailFrom
            // 
            this.cbEmailFrom.Enabled = false;
            this.cbEmailFrom.FormattingEnabled = true;
            this.cbEmailFrom.Location = new System.Drawing.Point(88, 14);
            this.cbEmailFrom.Name = "cbEmailFrom";
            this.cbEmailFrom.Size = new System.Drawing.Size(415, 21);
            this.cbEmailFrom.TabIndex = 14;
            // 
            // tbEmailTo
            // 
            this.tbEmailTo.Enabled = false;
            this.tbEmailTo.Location = new System.Drawing.Point(88, 41);
            this.tbEmailTo.Name = "tbEmailTo";
            this.tbEmailTo.Size = new System.Drawing.Size(415, 20);
            this.tbEmailTo.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Email";
            // 
            // tbTheme
            // 
            this.tbTheme.Location = new System.Drawing.Point(88, 67);
            this.tbTheme.Name = "tbTheme";
            this.tbTheme.Size = new System.Drawing.Size(415, 20);
            this.tbTheme.TabIndex = 11;
            // 
            // tbText
            // 
            this.tbText.Location = new System.Drawing.Point(12, 113);
            this.tbText.Multiline = true;
            this.tbText.Name = "tbText";
            this.tbText.Size = new System.Drawing.Size(496, 292);
            this.tbText.TabIndex = 10;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(428, 411);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 9;
            this.btnSend.Text = "Отправить";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Тема";
            // 
            // EmailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 448);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbEmailFrom);
            this.Controls.Add(this.tbEmailTo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbTheme);
            this.Controls.Add(this.tbText);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.label1);
            this.Name = "EmailForm";
            this.Text = "Отправка письма";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbEmailFrom;
        private System.Windows.Forms.TextBox tbEmailTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbTheme;
        private System.Windows.Forms.TextBox tbText;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label1;
    }
}