using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PriemForeignInspector.EDM;
using EducServLib;

namespace PriemForeignInspector
{
    public partial class ProtocolList : Form
    {
        public int? LicenseProgramId
        {
            get { return ComboServ.GetComboIdInt(cbLicenseProgram); }
        }
        public int? StudyFormId
        {
            get { return ComboServ.GetComboIdInt(cbStudyForm); }
        }
        public int? StudyBasisId
        {
            get { return ComboServ.GetComboIdInt(cbStudyBasis); }
        }
        public int? AbiturientTypeId
        {
            get { return ComboServ.GetComboIdInt(cbAbiturientType); }
        }

        public ProtocolList()
        {
            InitializeComponent();
            InitControls();
        }

        private void InitControls()
        {
            this.MdiParent = Util.MainForm;
            ExtraInit();
        }

        private void ExtraInit()
        {
            FillComboType();
            FillComboLicenseProgram();
            FillComboStudyForm();
            FillComboStudyBasis();
        }

        private void FillComboType()
        {
            using (OnlinePriem2012Entities context = new OnlinePriem2012Entities())
            {
                var src = context.ApplicationSecondType
                    .Select(x => new { x.Id, x.DisplayName })
                    .ToList()
                    .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.DisplayName))
                    .ToList();

                ComboServ.FillCombo(cbAbiturientType, src, false, false);
            }
        }
        private void FillComboLicenseProgram()
        {
            using (OnlinePriem2012Entities context = new OnlinePriem2012Entities())
            {
                var src = context.Entry.Where(x => x.SemesterId > 1 && x.IsUsedForPriem)
                    .Select(x => new { x.LicenseProgramId, x.LicenseProgramCode, x.LicenseProgramName })
                    .Distinct()
                    .ToList()
                    .Select(x => new KeyValuePair<string, string>(x.LicenseProgramId.ToString(), x.LicenseProgramCode + " " + x.LicenseProgramName))
                    .ToList();

                ComboServ.FillCombo(cbLicenseProgram, src, false, false);
            }
        }
        private void FillComboStudyForm()
        {
            using (OnlinePriem2012Entities context = new OnlinePriem2012Entities())
            {
                var src = context.Entry.Where(x => x.SemesterId > 1 && x.IsUsedForPriem)
                    .Select(x => new { x.StudyFormId, x.StudyFormName })
                    .Distinct()
                    .ToList()
                    .Select(x => new KeyValuePair<string, string>(x.StudyFormId.ToString(), x.StudyFormName))
                    .ToList();

                ComboServ.FillCombo(cbStudyForm, src, false, true);
            }
        }
        private void FillComboStudyBasis()
        {
            using (OnlinePriem2012Entities context = new OnlinePriem2012Entities())
            {
                var src = context.Entry.Where(x => x.SemesterId > 1 && x.IsUsedForPriem)
                    .Select(x => new { x.StudyBasisId, x.StudyBasisName })
                    .Distinct()
                    .ToList()
                    .Select(x => new KeyValuePair<string, string>(x.StudyBasisId.ToString(), x.StudyBasisName))
                    .ToList();

                ComboServ.FillCombo(cbStudyBasis, src, false, true);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            if (LicenseProgramId.HasValue)
            {
                IPrintProtocolProvider prot = GetProtocolProvider();

                DataTable tbl = prot.GetProtocolData(LicenseProgramId.Value, StudyFormId, StudyBasisId);
                dgv.DataSource = tbl;
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    col.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    col.HeaderText = tbl.Columns[col.Name].Caption;
                }

                dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (LicenseProgramId.HasValue)
            {
                IPrintProtocolProvider prot = GetProtocolProvider();
                try
                {
                    prot.PrintProtocol(LicenseProgramId.Value, StudyFormId, StudyBasisId);
                }
                catch (System.IO.IOException ex)
                {
                    WinFormsServ.Error("Ошибка при чтении файла", ex);
                }
                catch (Exception ex)
                {
                    WinFormsServ.Error(ex);
                }
            }
        }

        private IPrintProtocolProvider GetProtocolProvider()
        {
            IPrintProtocolProvider prot;
            switch (AbiturientTypeId)
            {
                case 2: { prot = new TransferPrintProtocolProvider(); break; }
                case 3: { prot = new RestorePrintProtocolProvider(); break; }
                case 4: { prot = new ChangeStudyFormPrintProtocolProvider(); break; }
                case 5: { prot = new ChangeStudyBasisPrintProtocolProvider(); break; }
                case 6: { prot = new ChangeObrazProgramPrintProtocolProvider(); break; }
                default: { prot = new RestorePrintProtocolProvider(); break; }
            }

            return prot;
        }
    }
}
