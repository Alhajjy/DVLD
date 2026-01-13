using System;
using System.Data;
using System.Windows.Forms;
using BusinessLayer;
using DVLD.Licenses.International_Licenses;
using DVLD.Licenses.Local_Licenses;

namespace DVLD.Licenses
{
    public partial class ctrlDriverLicenses : UserControl
    {
        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }
        DataTable _LocalLicenses_DT = new DataTable();
        DataTable _InternationalLicense_DT = new DataTable();
        private void _RefreshTables(int DriverID)
        {
            // international
            _InternationalLicense_DT = clsInternationalLicense.GetInternationalLicensesByDriverID(DriverID);
            dgvLocalLicensesHistory.DataSource = _InternationalLicense_DT;

            // local
            _LocalLicenses_DT = clsLicense.GetDriverLicenses(DriverID);
            dgvLocalLicensesHistory.DataSource = _LocalLicenses_DT;

            // records count
            lblLocalLicensesRecords.Text = _LocalLicenses_DT.Rows.Count.ToString();
            lblInternationalLicensesRecords.Text = _InternationalLicense_DT.Rows.Count.ToString();
        }
        public void LoadDriverLicenses(int DriverID)
        {
            if (clsDriver.IsDriverExistByDriverID(DriverID))
            {
                _RefreshTables(DriverID);
            }
            else
            {
                MessageBox.Show("DriverID doesnt exist!");
            }
        }
        public void LoadDriverLicensesByPersonID(int PersonID)
        {
            int driverID = -1;
            if (clsDriver.GetDriverIDByPersonID(ref driverID, PersonID))
            {
                LoadDriverLicenses(driverID);

                dgvLocalLicensesHistory.Columns[0].Width = 100;
                dgvLocalLicensesHistory.Columns[1].Width = 100;
                dgvLocalLicensesHistory.Columns[5].Width = 100;
            }
            else
            {
                MessageBox.Show("Person is not a driver");
            }
        }
        private void tcDriverLicenses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcDriverLicenses.SelectedIndex == 0)
            {
                dgvLocalLicensesHistory.DataSource = _LocalLicenses_DT;
                lblLocalLicensesRecords.Text = _LocalLicenses_DT.Rows.Count.ToString();
            }
            else
            {
                dgvInternationalLicensesHistory.DataSource = _InternationalLicense_DT;
                lblLocalLicensesRecords.Text = _InternationalLicense_DT.Rows.Count.ToString();
            }
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dgvLocalLicensesHistory.CurrentRow.Cells[0].Value.ToString(), out int LicenseID))
            {
                frmShowLicensesInfo frm = new frmShowLicensesInfo(LicenseID);
                frm.ShowDialog();
            }
        }

        private void InternationalLicenseHistorytoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dgvLocalLicensesHistory.CurrentRow.Cells[0].Value.ToString(), out int internationalLicenseID))
            {
                frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(internationalLicenseID);
                frm.ShowDialog();
            }
        }
    }
}
