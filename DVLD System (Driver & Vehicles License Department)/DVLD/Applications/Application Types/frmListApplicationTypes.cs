using System;
using System.Data;
using System.Windows.Forms;
using DVLD.People;

namespace DVLD.Applications.Application_Types
{
    public partial class frmListApplicationTypes : Form
    {
        public frmListApplicationTypes()
        {
            InitializeComponent();
        }
        DataTable DT { get; set; }
        private void _RefreshTable()
        {
            DT = BusinessLayer.clsApplicationType.AllApplicationTypes();
            dgvApplicationTypes.DataSource = DT;
            lblRecordsCount.Text = DT.Rows.Count.ToString();
        }

        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
            _RefreshTable();
            dgvApplicationTypes.Columns[1].FillWeight = 200;
        }

        private void tsmiEditAppType_Click(object sender, EventArgs e)
        {
            if (dgvApplicationTypes.CurrentCell == null && !dgvApplicationTypes.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Please select a type first.");
                return;
            }
            int.TryParse(dgvApplicationTypes.CurrentRow.Cells[0].Value.ToString(), out int result);
            frmEditApplicationType frm = new frmEditApplicationType(result);
            frm.ShowDialog();
            _RefreshTable();
        }
    }
}
