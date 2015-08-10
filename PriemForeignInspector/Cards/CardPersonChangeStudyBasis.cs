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
    public partial class CardPersonChangeStudyBasis : CardPerson
    {
        
        public CardPersonChangeStudyBasis()
            : base()
        {
            InitializeComponent();
        }
        public CardPersonChangeStudyBasis(Guid id)
            : base(id)
        {
        }
        protected override void FillText()
        {
            this.Text = "Смена основы обучения";
        }
        protected override void InitFields()
        {
            HasCurrentEdication = true;
            HasDisorderInfo = false;
            HasReason = true;

            ChangeVisiblegbCurrentEducation(true);
            ChangeVisiblegbDisorderInfo(false);
            ChangeVisiblegbReason(true);
            ChangeVisibleAccreditationInfo(false);
        }
    }
}
