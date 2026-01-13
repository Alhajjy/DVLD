using System;
using System.ComponentModel;
using System.Data;
using System.Security.Policy;
using System.Windows.Forms;
using BusinessLayer;
using DVLD.Licenses;
using DVLD.Licenses.International_Licenses;
using DVLD.People;
using Shared;

namespace DVLD.Applications.International_License
{
    public partial class frmListInternationalLicenseApplications : Form
    {
        public frmListInternationalLicenseApplications()
        {
            InitializeComponent();
            cbFilterBy.SelectedIndex = 0;
        }
        DataTable InternationalLicenses_DT = new DataTable();
        private void _RefreshTable()
        {
            InternationalLicenses_DT = clsInternationalLicense.GetInternationalLicenses();
            dgvInternationalLicenses.DataSource = InternationalLicenses_DT;
            lblInternationalLicensesRecords.Text = InternationalLicenses_DT.Rows.Count.ToString();
        }
        private void _Filter()
        {
            InternationalLicenses_DT.DefaultView.RowFilter = string.Empty;
            if (txtFilterValue.Visible)
            {
                if (txtFilterValue.Text == string.Empty || !int.TryParse(txtFilterValue.Text, out int result))
                    return;
            }
            switch (cbFilterBy.Text)
            {
                case "International License ID":
                    InternationalLicenses_DT.DefaultView.RowFilter = $"Int.LicenseID = '{txtFilterValue.Text}'";
                    return;
                case "Application ID":
                    InternationalLicenses_DT.DefaultView.RowFilter = $"ApplicationID = '{txtFilterValue.Text}'";
                    return;
                case "Driver ID":
                    InternationalLicenses_DT.DefaultView.RowFilter = $"DriverID = '{txtFilterValue.Text}'";
                    return;
                case "Local License ID":
                    InternationalLicenses_DT.DefaultView.RowFilter = $"LocalLicenseID = '{txtFilterValue.Text}'";
                    return;
                case "Is Active":
                    switch (cbIsActive.SelectedIndex)
                    {
                        case 1:
                            InternationalLicenses_DT.DefaultView.RowFilter = $"IsActive = 1";
                            return;
                        case 2:
                            InternationalLicenses_DT.DefaultView.RowFilter = $"IsActive = 0";
                            return;
                    }
                    return;
                default:
                    txtFilterValue.Text = "";
                    txtFilterValue.Visible = false;
                    cbIsActive.SelectedIndex = 0;
                    cbIsActive.Visible = false;
                    break;
            }
            InternationalLicenses_DT.DefaultView.RowFilter = string.Empty;
        }
        private void btnNewApplication_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
            _RefreshTable();
        }
        private void frmListInternationalLicenseApplications_Load(object sender, EventArgs e)
        {
            _RefreshTable();
        }
        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dgvInternationalLicenses.CurrentRow.Cells[1].Value.ToString(), out int applicationID))
            {
                int personID = -1;
                if (clsApplication.GetApplicantIDByAppID(applicationID, ref personID))
                {
                    frmPersonDetails frm = new frmPersonDetails(personID);
                    frm.ShowDialog();
                    return;
                }
            }
            MessageBox.Show($"Some thing went wrong!\n{clsShared.lastError}");
        }
        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dgvInternationalLicenses.CurrentRow.Cells[0].Value.ToString(), out int internationalLicenseID))
            {
                frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(internationalLicenseID);
                frm.ShowDialog();
            }
            else
                MessageBox.Show($"Some thing went wrong!\n{clsShared.lastError}");
        }
        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dgvInternationalLicenses.CurrentRow.Cells[1].Value.ToString(), out int applicationID))
            {
                int personID = -1;
                if (clsApplication.GetApplicantIDByAppID(applicationID, ref personID))
                {
                    frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(personID);
                    frm.ShowDialog();
                    return;
                }
            }
            MessageBox.Show($"Some thing went wrong!\n{clsShared.lastError}");
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbFilterBy.SelectedIndex)
            {
                case 0:
                    cbIsActive.SelectedIndex = 0;
                    cbIsActive.Visible = false;
                    txtFilterValue.Text = "";
                    txtFilterValue.Visible = false;
                    break;
                case 5:
                    cbIsActive.Visible = true;
                    cbIsActive.SelectedIndex = 0;
                    txtFilterValue.Text = "";
                    txtFilterValue.Visible = false;
                    break;
                default:
                    cbIsActive.SelectedIndex = 0;
                    cbIsActive.Visible = false;
                    txtFilterValue.Text = "";
                    txtFilterValue.Visible = true;
                    break;
            }
            _Filter();
        }
        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Filter();
        }
        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            _Filter();
        }
    }
}