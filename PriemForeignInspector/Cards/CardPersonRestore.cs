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
    public partial class CardPersonRestore : CardPerson
    {
        public CardPersonRestore()
            : base()
        {
        }
        public CardPersonRestore(Guid id)
            :base(id)
        {
        }
        protected override void FillText()
        {
            this.Text = "Восстановление в СПбГУ";
        }
        protected override void InitFields()
        {
            HasCurrentEdication = false;
            HasDisorderInfo = true;
            HasReason = false;

            ChangeVisiblegbCurrentEducation(false);
            ChangeVisiblegbDisorderInfo(true);
            ChangeVisiblegbReason(false);
        }
    }
}
