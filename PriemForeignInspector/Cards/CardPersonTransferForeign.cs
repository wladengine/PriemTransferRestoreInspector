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
    public partial class CardPersonTransferForeign : CardPerson
    {
        public CardPersonTransferForeign():
            base()
        {
            InitializeComponent();
        }
        public CardPersonTransferForeign(Guid id)
            :base(id)
        {
        }
        protected override void FillText()
        {
            this.Text = "Перевода в СПБГУ из иностранного ВУЗа";
        }
        protected override void InitFields()
        {
            HasCurrentEdication = true;
            HasDisorderInfo = false;
            HasReason = false;
            ChangeVisibleCurrentObrazProgramProfile(true);
            ChangeVisiblegbDisorderInfo(false);
            ChangeVisibleCurrentObrazProgramProfile(false);
            ChangeVisiblegbReason(false);
        }
    }
}
