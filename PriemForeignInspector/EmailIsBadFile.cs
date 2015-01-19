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
    public partial class EmailIsBadFile : Form
    {
        private string _Body;
        private string _emailAddress;
        private string _emailFrom;
        private Guid _FileId;

        UpdateHandler _parentUpdate;

        public EmailIsBadFile(string messageBody, string email, string emailFrom, Guid fileId, UpdateHandler handler)
        {
            InitializeComponent();
            _Body = messageBody;
            _emailAddress = email;
            _emailFrom = emailFrom;
            _FileId = fileId;
            _parentUpdate = handler;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string query = "UPDATE ApplicationFile SET IsApproved='False' WHERE Id=@Id";
            int res = Util.BDC.ExecuteQuery(query, new Dictionary<string, object>() { { "@Id", _FileId } });
            if (res == 0)
            {
                query = "UPDATE PersonFile SET IsApproved='False' WHERE Id=@Id";
                res = Util.BDC.ExecuteQuery(query, new Dictionary<string, object>() { { "@Id", _FileId } });
            }
            if (res == 0)
            {
                MessageBox.Show("Не удалось найти файл");
                return;
            }

            query = "SELECT PersonId FROM AllFiles WHERE Id=@Id";
            Guid? pid = (Guid?)Util.BDC.GetValue(query, new Dictionary<string, object>() { { "@Id", _FileId } });
            if (pid.HasValue)
            {
                query = "SELECT FileName FROM AllFiles WHERE Id=@Id";
                string fname = Util.BDC.GetValue(query, new Dictionary<string, object>() { { "@Id", _FileId } }).ToString();

                query = "INSERT INTO ForeignPersonHistory (PersonId, Action, NewValue, Time, Owner) VALUES (@PersonId, @Action, @NewValue, @Time, @Owner)";
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("@PersonId", pid.Value);
                dic.Add("@Action", "Файл не одобрен");
                dic.Add("@NewValue", fname + "\nПричина отказа:\n" + (string.IsNullOrEmpty(tbReasonBad.Text) ? "не указана" : tbReasonBad.Text));
                dic.Add("@Time", DateTime.Now);
                dic.Add("@Owner", System.Environment.UserName);

                Util.BDC.ExecuteQuery(query, dic);
            }

            _Body += string.Format(@"
Причина отказа: 
{0}

С уважением,
Приёмная комиссия СПбГУ", string.IsNullOrEmpty(tbReasonBad.Text) ? "не указана" : tbReasonBad.Text);

            Util.Email(_emailAddress, _Body, "Admissions Committee SPbSU", _emailFrom);
            _parentUpdate.Invoke();
            this.Close();
        }
    }
}
