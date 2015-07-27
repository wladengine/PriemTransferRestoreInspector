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
        public CardPersonRestore(Guid id)
            :base(id)
        {
            //InitializeComponent(); 
            _AbitType = 3;
            HasCurrentEdication = true;
            HasDisorderInfo = false;

            FillText(_AbitType);


            ChangeVisiblegbCurrentEducation(false);
            ChangeVisiblegbDisorderInfo(true);
            ChangeVisiblegbReason(false);
        }
    }
}
