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
    public partial class CardPersonTransfer : CardPerson
    {
        public CardPersonTransfer()
            : base()
        {
        }
        public CardPersonTransfer(Guid id)
            :base(id)
        {
            InitializeComponent();
            _AbitType = 2;
            HasCurrentEdication = true;
            HasDisorderInfo = false;

            FillText(_AbitType);
            ChangeVisibleCurrentObrazProgramProfile(true);
            ChangeVisiblegbDisorderInfo(false);
            ChangeVisibleCurrentObrazProgramProfile(false);
            ChangeVisiblegbReason(false);
        }



    }
}
