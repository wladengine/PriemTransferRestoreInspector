﻿namespace PriemForeignInspector
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.listsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокЗзаявленйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smiDics = new System.Windows.Forms.ToolStripMenuItem();
            this.entryListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smiProtocols = new System.Windows.Forms.ToolStripMenuItem();
            this.smiRestoreProtocols = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listsToolStripMenuItem,
            this.smiDics,
            this.smiProtocols});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(738, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // listsToolStripMenuItem
            // 
            this.listsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainListToolStripMenuItem,
            this.списокЗзаявленйToolStripMenuItem});
            this.listsToolStripMenuItem.Name = "listsToolStripMenuItem";
            this.listsToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.listsToolStripMenuItem.Text = "Списки";
            // 
            // mainListToolStripMenuItem
            // 
            this.mainListToolStripMenuItem.Name = "mainListToolStripMenuItem";
            this.mainListToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.mainListToolStripMenuItem.Text = "Список абитуриентов";
            this.mainListToolStripMenuItem.Click += new System.EventHandler(this.mainListToolStripMenuItem_Click);
            // 
            // списокЗзаявленйToolStripMenuItem
            // 
            this.списокЗзаявленйToolStripMenuItem.Name = "списокЗзаявленйToolStripMenuItem";
            this.списокЗзаявленйToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.списокЗзаявленйToolStripMenuItem.Text = "Список заявлений";
            this.списокЗзаявленйToolStripMenuItem.Click += new System.EventHandler(this.списокЗзаявленйToolStripMenuItem_Click);
            // 
            // smiDics
            // 
            this.smiDics.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.entryListToolStripMenuItem});
            this.smiDics.Name = "smiDics";
            this.smiDics.Size = new System.Drawing.Size(41, 20);
            this.smiDics.Text = "Dics";
            // 
            // entryListToolStripMenuItem
            // 
            this.entryListToolStripMenuItem.Name = "entryListToolStripMenuItem";
            this.entryListToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.entryListToolStripMenuItem.Text = "EntryList";
            this.entryListToolStripMenuItem.Click += new System.EventHandler(this.entryListToolStripMenuItem_Click);
            // 
            // smiProtocols
            // 
            this.smiProtocols.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiRestoreProtocols});
            this.smiProtocols.Name = "smiProtocols";
            this.smiProtocols.Size = new System.Drawing.Size(83, 20);
            this.smiProtocols.Text = "Протоколы";
            // 
            // smiRestoreProtocols
            // 
            this.smiRestoreProtocols.Name = "smiRestoreProtocols";
            this.smiRestoreProtocols.Size = new System.Drawing.Size(182, 22);
            this.smiRestoreProtocols.Text = "Печать протоколов";
            this.smiRestoreProtocols.Click += new System.EventHandler(this.smiRestoreProtocols_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(738, 433);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Переводы и восстановления в СПбГУ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem listsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mainListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smiDics;
        private System.Windows.Forms.ToolStripMenuItem entryListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списокЗзаявленйToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smiProtocols;
        private System.Windows.Forms.ToolStripMenuItem smiRestoreProtocols;
    }
}

