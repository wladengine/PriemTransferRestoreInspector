namespace PriemForeignInspector
{
    partial class OlympList
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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btnCreateImport = new System.Windows.Forms.Button();
            this.btnAddProfiles = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(12, 85);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(700, 327);
            this.dgv.TabIndex = 0;
            // 
            // btnCreateImport
            // 
            this.btnCreateImport.Location = new System.Drawing.Point(243, 442);
            this.btnCreateImport.Name = "btnCreateImport";
            this.btnCreateImport.Size = new System.Drawing.Size(104, 23);
            this.btnCreateImport.TabIndex = 1;
            this.btnCreateImport.Text = "Создать блоки";
            this.btnCreateImport.UseVisualStyleBackColor = true;
            this.btnCreateImport.Click += new System.EventHandler(this.btnCreateImport_Click);
            // 
            // btnAddProfiles
            // 
            this.btnAddProfiles.Location = new System.Drawing.Point(608, 418);
            this.btnAddProfiles.Name = "btnAddProfiles";
            this.btnAddProfiles.Size = new System.Drawing.Size(104, 23);
            this.btnAddProfiles.TabIndex = 2;
            this.btnAddProfiles.Text = "Создать профили";
            this.btnAddProfiles.UseVisualStyleBackColor = true;
            this.btnAddProfiles.Click += new System.EventHandler(this.btnAddProfiles_Click);
            // 
            // OlympList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 477);
            this.Controls.Add(this.btnAddProfiles);
            this.Controls.Add(this.btnCreateImport);
            this.Controls.Add(this.dgv);
            this.Name = "OlympList";
            this.Text = "Список олимпиад";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnCreateImport;
        private System.Windows.Forms.Button btnAddProfiles;
    }
}