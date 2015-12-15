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
    public partial class HistoryCard : Form
    {
        private Guid PersonId;
        public HistoryCard(Guid persId)
        {
            InitializeComponent();
            PersonId = persId;
            FillGrid();
        }

        private void FillGrid()
        {
            string query = "SELECT Action AS Действие, OldValue AS СтароеЗначение, NewValue AS НовоеЗначение, convert(nvarchar, Time, 104) AS Время, Owner AS Автор FROM PersonHistory WHERE PersonId=@PersonId ORDER BY Time";
            DataTable tbl = Util.BDC.GetDataTable(query, new Dictionary<string, object>() { { "@PersonId", PersonId } });
            dgv.DataSource = tbl;
            
            dgv.Columns["Действие"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns["Время"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns["Автор"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns["НовоеЗначение"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
