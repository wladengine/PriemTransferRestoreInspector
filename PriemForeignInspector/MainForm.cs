using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PriemForeignInspector
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Util.MainForm = this;
            Start();
        }

        void Start()
        {
            //Util.OpenPersonList();
        }

        protected override void OnClosed(EventArgs e)
        {
            Util.ClearTempFolder();
            base.OnClosed(e);
        }

        private void entryListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new OlympList().Show();
        }

        private void mainListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Util.OpenPersonList(); 
        }

        private void списокЗзаявленйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Util.OpenAbitList();
        }

        private void smiRestoreProtocols_Click(object sender, EventArgs e)
        {
            new ProtocolList().Show();
        }
    }
}