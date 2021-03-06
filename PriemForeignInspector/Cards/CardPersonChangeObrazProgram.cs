﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PriemForeignInspector
{
    public partial class CardPersonChangeObrazProgram : CardPerson
    {
        public CardPersonChangeObrazProgram()
            :base()
        {
            InitializeComponent();
        }
        public CardPersonChangeObrazProgram(Guid id)
            : base(id)
        {
        }
        protected override void FillText()
        {
            this.Text = "Смена образовательной программы";
        }

        protected override void InitFields()
        {
            HasCurrentEdication = true;
            HasDisorderInfo = false;
            HasReason = false;

            ChangeVisiblegbCurrentEducation(true);
            ChangeVisiblegbDisorderInfo(false);
            ChangeVisiblegbReason(false);
            ChangeVisibleAccreditationInfo(false);
        }
    }
}
