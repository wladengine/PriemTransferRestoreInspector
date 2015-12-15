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
    public partial class EmailForm : Form
    {
        private Guid _personId;
        public EmailForm(string startEmail, string emailTo, Guid PersonId)
        {
            InitializeComponent();
            _personId = PersonId;
            this.CenterToParent();
            this.Icon = PriemForeignInspector.Properties.Resources.Mail_icon;

            string appendix = "";
            //Util.BDC.GetValue(query, new Dictionary<string, object>() { { "@Email", startEmail } }).ToString();

            tbText.Text += "\n" + appendix;

            if (cbEmailFrom.Items.Count == 1 || cbEmailFrom.Items.Count == 0)
                cbEmailFrom.Enabled = false;
            else
                cbEmailFrom.Enabled = true;

            cbEmailFrom.Text = startEmail;
            tbEmailTo.Text = emailTo;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO [PersonHistory] (PersonId, Action, NewValue) VALUES (@PersonId, @Action, @NewValue)";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@PersonId", _personId);
            dic.Add("@Action", "Email");
            dic.Add("@NewValue", tbText.Text);
            try 
            {
                Util.BDC.ExecuteQuery(query, dic);
            }
            catch { }
            Util.Email(tbEmailTo.Text, tbText.Text, tbTheme.Text, cbEmailFrom.Text);
            this.Close();
        }
    }
}
