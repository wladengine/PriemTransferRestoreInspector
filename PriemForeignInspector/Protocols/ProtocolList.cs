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
                var src = context.AbiturientType.Where(x => x.AppSecondTypeId != null && x.AppSecondTypeId > 1)
                    .Select(x => new { x.AppSecondTypeId, x.Description })
                    .ToList()
                    .Select(x => new KeyValuePair<string, string>(x.AppSecondTypeId.ToString(), x.Description))
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
                IPrintProtocolProvider prot = new RestorePrintProtocolProvider();
                DataTable tbl = prot.GetProtocolData(LicenseProgramId.Value, StudyFormId, StudyBasisId);
                dgv.DataSource = tbl;
                foreach (DataGridViewColumn col in dgv.Columns)
                    col.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (LicenseProgramId.HasValue)
            {
                IPrintProtocolProvider prot = new RestorePrintProtocolProvider();
                prot.PrintProtocol(LicenseProgramId.Value, StudyFormId, StudyBasisId);
            }
        }
    }
}
