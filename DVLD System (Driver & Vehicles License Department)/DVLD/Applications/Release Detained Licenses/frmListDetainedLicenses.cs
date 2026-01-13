using System;
using System.Data;
using System.Windows.Forms;
using BusinessLayer;
using DVLD.Licenses;
using DVLD.Licenses.Detain_License;
using DVLD.Licenses.Local_Licenses;
using DVLD.People;

namespace DVLD.Applications.Release_Detained_Licenses
{
    public partial class frmListDetainedLicenses : Form
    {
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }
        DataTable Detainings_DT = new DataTable();
        private void _RefreshTable()
        {
            Detainings_DT = clsDetaining.GetAllDetainings();
            dgvDetainedLicenses.DataSource = Detainings_DT;
            lblTotalRecords.Text = Detainings_DT.Rows.Count.ToString();
        }
        private void _Filter()
        {
            Detainings_DT.DefaultView.RowFilter = string.Empty;
            if (txtFilterValue.Visible)
            {
                if (txtFilterValue.Text == string.Empty)
                    return;
            }
            switch (cbFilterBy.Text)
            {
                case "Detain ID":
                    Detainings_DT.DefaultView.RowFilter = $"D.ID = '{txtFilterValue.Text}'";
                    return;
                case "National No.":
                    Detainings_DT.DefaultView.RowFilter = $"N.No LIKE '%{txtFilterValue.Text}%'";
                    return;
                case "Full Name":
                    Detainings_DT.DefaultView.RowFilter = $"FullName LIKE '%{txtFilterValue.Text}%'";
                    return;
                case "Release Application ID":
                    Detainings_DT.DefaultView.RowFilter = $"ReleaseApp.ID = '{txtFilterValue.Text}'";
                    return;
                case "Is Released":
                    switch (cbIsReleased.SelectedIndex)
                    {
                        case 1:
                            Detainings_DT.DefaultView.RowFilter = $"IsReleased = 1";
                            return;
                        case 2:
                            Detainings_DT.DefaultView.RowFilter = $"IsReleased = 0";
                            return;
                    }
                    return;
                default:
                    txtFilterValue.Text = "";
                    txtFilterValue.Visible = false;
                    cbIsReleased.SelectedIndex = 0;
                    cbIsReleased.Visible = false;
                    break;
            }
            Detainings_DT.DefaultView.RowFilter = string.Empty;
        }
        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
            _RefreshTable();
        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
            _RefreshTable();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            _RefreshTable();
            cbFilterBy.SelectedIndex = 0;
            if (dgvDetainedLicenses.Rows.Count > 0)
            {
                dgvDetainedLicenses.Columns[0].Width = 80;
                dgvDetainedLicenses.Columns[1].Width = 100;
                dgvDetainedLicenses.Columns[2].Width = 100;
                dgvDetainedLicenses.Columns[3].Width = 100;
                dgvDetainedLicenses.Columns[4].Width = 100;
                dgvDetainedLicenses.Columns[5].Width = 100;
                dgvDetainedLicenses.Columns[6].Width = 100;
                dgvDetainedLicenses.Columns[8].Width = 100;
            }
        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dgvDetainedLicenses.CurrentRow.Cells[8].Value.ToString(), out int appID))
            {
                int personID = -1;
                if (clsApplication.GetApplicantIDByAppID(appID, ref personID))
                {
                    frmPersonDetails frm = new frmPersonDetails(personID);
                    frm.ShowDialog();
                }
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dgvDetainedLicenses.CurrentRow.Cells[1].Value.ToString(), out int LicenseID))
            {
                frmShowLicensesInfo frm = new frmShowLicensesInfo(LicenseID);
                frm.ShowDialog();
            }
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dgvDetainedLicenses.CurrentRow.Cells[1].Value.ToString(), out int LicenseID))
            {
                int personID = -1;
                if (clsLicense.GetPersonIDByLicenseID(ref personID, LicenseID))
                {
                    frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(personID);
                    frm.ShowDialog();
                }
            }
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dgvDetainedLicenses.CurrentRow.Cells[1].Value.ToString(), out int LicenseID))
            {
                frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication(LicenseID);
                frm.ShowDialog();
                _RefreshTable();
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbFilterBy.SelectedIndex)
            {
                case 0:
                    txtFilterValue.Text = "";
                    txtFilterValue.Visible = false;
                    cbIsReleased.Visible = false;
                    break;
                case 2:
                    cbIsReleased.Visible = true;
                    cbIsReleased.SelectedIndex = 0;
                    txtFilterValue.Visible = false;
                    break;
                default:
                    cbIsReleased.Visible = false;
                    txtFilterValue.Visible = true;
                    txtFilterValue.Text = "";
                    break;
            }
            _Filter();
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Filter();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            _Filter();
        }
    }
}
