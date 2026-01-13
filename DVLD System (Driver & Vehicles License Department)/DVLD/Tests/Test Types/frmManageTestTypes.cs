using System;
using System.Data;
using System.Windows.Forms;
using BusinessLayer;

namespace DVLD.Tests.Test_Types
{
    public partial class frmManageTestTypes : Form
    {
        public frmManageTestTypes()
        {
            InitializeComponent();
        }
        DataTable DT { get; set; }
        private void _RefreshTable()
        {
            DT = clsTestType.AllTestTypes();
            dgvTestTypes.DataSource = DT;
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            _RefreshTable();
            dgvTestTypes.Columns[2].FillWeight = 300;
        }

        private void tsmiEditTestType_Click(object sender, EventArgs e)
        {
            if (dgvTestTypes.CurrentCell == null && !dgvTestTypes.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Please select a type first.");
                return;
            }
            int.TryParse(dgvTestTypes.CurrentRow.Cells[0].Value.ToString(), out int result);
            frmEditTestType frm = new frmEditTestType(result);
            frm.ShowDialog();
            _RefreshTable();
        }
    }
}
