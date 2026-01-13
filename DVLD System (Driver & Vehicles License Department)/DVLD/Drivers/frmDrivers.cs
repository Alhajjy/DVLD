using System;
using System.Data;
using System.Windows.Forms;
using BusinessLayer;
using DVLD.Licenses;
using DVLD.People;

namespace DVLD.Drivers
{
    public partial class frmDrivers : Form
    {
        public frmDrivers()
        {
            InitializeComponent();
        }
        DataTable _DriversDT = new DataTable();
        private void _RefreshDriversTable()
        {
            _DriversDT = clsDriver.GetDrivers();
            dgvDrivers.DataSource = _DriversDT;
            lbRecords.Text = _DriversDT.Rows.Count.ToString();
        }
        private void _Filter()
        {
            _DriversDT.DefaultView.RowFilter = string.Empty;
            if (tbFilterValue.Visible)
            {
                if (tbFilterValue.Text == string.Empty)
                    return;
            }
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    _DriversDT.DefaultView.RowFilter = $"PersonID = '{tbFilterValue.Text}'";
                    return;
                case "Driver ID":
                    _DriversDT.DefaultView.RowFilter = $"DriverID = '{tbFilterValue.Text}'";
                    return;
                case "National No.":
                    _DriversDT.DefaultView.RowFilter = $"NationalNo LIKE '%{tbFilterValue.Text}%'";
                    return;
                case "Full Name":
                    _DriversDT.DefaultView.RowFilter = $"FullName LIKE '%{tbFilterValue.Text}%'";
                    return;
            }
            _DriversDT.DefaultView.RowFilter = string.Empty;
            tbFilterValue.Text = "";
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmDrivers_Load(object sender, EventArgs e)
        {
            _RefreshDriversTable();
            dgvDrivers.Columns[0].Width = 100;
            dgvDrivers.Columns[1].Width = 100;
            dgvDrivers.Columns[2].Width = 100;
            dgvDrivers.Columns[3].Width = 200;
            dgvDrivers.Columns[5].Width = 100;

            cbFilterBy.SelectedIndex = 0;
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dgvDrivers.CurrentRow.Cells[1].Value.ToString(), out int personID))
            {
                frmPersonDetails frm = new frmPersonDetails(personID);
                frm.ShowDialog();
            }
        }

        private void showDriverLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dgvDrivers.CurrentRow.Cells[1].Value.ToString(), out int personID))
            {
                frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(personID);
                frm.ShowDialog();
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 0)
            {
                tbFilterValue.Visible = false;
            }
            else
            {
                tbFilterValue.Visible = true;
            }
            _Filter();
        }

        private void tbFilterValue_TextChanged(object sender, EventArgs e)
        {
            _Filter();
        }
    }
}
