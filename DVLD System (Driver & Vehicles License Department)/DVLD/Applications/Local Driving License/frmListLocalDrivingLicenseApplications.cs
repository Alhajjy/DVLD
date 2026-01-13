using System;
using System.Data;
using System.Windows.Forms;
using BusinessLayer;
using DVLD.Licenses;
using DVLD.Licenses.Local_Licenses;
using DVLD.Tests;
using Shared;

namespace DVLD.Applications.Local_Driving_License
{
    public partial class frmListLocalDrivingLicenseApplications : Form
    {
        public frmListLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }
        DataTable _LDLApplications_DT = new DataTable();
        clsLocalDrivingLicenseApp _Temp_LDLApp = new clsLocalDrivingLicenseApp();
        private void _RefrashTable()
        {
            _LDLApplications_DT = clsLocalDrivingLicenseApp.GetLDLAppsView();
            dgvLocalDrivingLicenseApplications.DataSource = _LDLApplications_DT;
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();

            if (dgvLocalDrivingLicenseApplications.Rows.Count > 0)
            {
                dgvLocalDrivingLicenseApplications.Columns[0].Width = 100;
                dgvLocalDrivingLicenseApplications.Columns[1].Width = 150;
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 100;
                dgvLocalDrivingLicenseApplications.Columns[3].Width = 300;
                dgvLocalDrivingLicenseApplications.Columns[4].Width = 80;
                dgvLocalDrivingLicenseApplications.Columns[5].Width = 100;
                dgvLocalDrivingLicenseApplications.Columns[6].Width = 100;
            }
        }
        private void _Filter(object sender, EventArgs e)
        {
            _LDLApplications_DT.DefaultView.RowFilter = string.Empty;
            if (tbFilterValue.Visible)
            {
                if (tbFilterValue.Text == string.Empty)
                    return;
            }
            _LDLApplications_DT.DefaultView.RowFilter = string.Empty;
            switch (cbFilterBy.Text)
            {
                case "None":
                    _LDLApplications_DT.DefaultView.RowFilter = string.Empty;
                    return;
                case "L.D.L.AppID":
                    if (int.TryParse(tbFilterValue.Text, out int result))
                        _LDLApplications_DT.DefaultView.RowFilter = $"LocalLicenseAppID = '{result}'";
                    return;
                case "National No.":
                    _LDLApplications_DT.DefaultView.RowFilter = $"NationalNo LIKE '%{tbFilterValue.Text}%'";
                    return;
                case "Full Name":
                    _LDLApplications_DT.DefaultView.RowFilter = $"FullName LIKE '%{tbFilterValue.Text}%'";
                    return;
                case "Status":
                    _LDLApplications_DT.DefaultView.RowFilter = $"Status LIKE '%{tbFilterValue.Text}%'";
                    return;
            }
            _LDLApplications_DT.DefaultView.RowFilter = string.Empty;
        }
        private void _HandleTestsInMenu(int LDLAppID, int passedTests, bool isConverted)
        {
            var main = cmsApplications.Items["ScheduleTestsMenue"] as ToolStripMenuItem;

            if (passedTests >= 0 && passedTests < 3 && clsLocalDrivingLicenseApp.IsAppCanBeCancelled(LDLAppID))
            {
                main.Enabled = true;
                switch (passedTests)
                {
                    case 0:
                        main.DropDownItems["scheduleVisionTestToolStripMenuItem"].Enabled = true;
                        main.DropDownItems["scheduleWrittenTestToolStripMenuItem"].Enabled = false;
                        main.DropDownItems["scheduleStreetTestToolStripMenuItem"].Enabled = false;
                        break;
                    case 1:
                        main.DropDownItems["scheduleVisionTestToolStripMenuItem"].Enabled = false;
                        main.DropDownItems["scheduleWrittenTestToolStripMenuItem"].Enabled = true;
                        main.DropDownItems["scheduleStreetTestToolStripMenuItem"].Enabled = false;
                        break;
                    case 2:
                        main.DropDownItems["scheduleVisionTestToolStripMenuItem"].Enabled = false;
                        main.DropDownItems["scheduleWrittenTestToolStripMenuItem"].Enabled = false;
                        main.DropDownItems["scheduleStreetTestToolStripMenuItem"].Enabled = true;
                        break;
                    default:
                        main.Enabled = false;
                        main.DropDownItems["scheduleVisionTestToolStripMenuItem"].Enabled = false;
                        main.DropDownItems["scheduleWrittenTestToolStripMenuItem"].Enabled = false;
                        main.DropDownItems["scheduleStreetTestToolStripMenuItem"].Enabled = false;
                        break;
                }
            }
            else
            {
                main.Enabled = false;
                main.DropDownItems["scheduleVisionTestToolStripMenuItem"].Enabled = false;
                main.DropDownItems["scheduleWrittenTestToolStripMenuItem"].Enabled = false;
                main.DropDownItems["scheduleStreetTestToolStripMenuItem"].Enabled = false;
            }
        }
        private void _PrepareContextMentStrip()
        {
            bool isConverted = int.TryParse(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value.ToString(), out int LDLAppID);
            int passedTests = clsTestAppointment.GetPassedTestsNumber(LDLAppID);
            _Temp_LDLApp = clsLocalDrivingLicenseApp.FindLocalLicenseAppWithID(LDLAppID);
            // conditions => enable, disable
            if (clsLocalDrivingLicenseApp.IsAppHaveAtLeastOneTestAppointment(LDLAppID))
            {
                cmsApplications.Items["DeleteApplicationToolStripMenuItem"].Enabled = false;
                cmsApplications.Items["editToolStripMenuItem"].Enabled = false;
            }
            else
            {
                cmsApplications.Items["DeleteApplicationToolStripMenuItem"].Enabled = true;
                cmsApplications.Items["editToolStripMenuItem"].Enabled = true;
            }
            if (clsLocalDrivingLicenseApp.IsAppCanBeCancelled(LDLAppID))
            {
                cmsApplications.Items["CancelApplicaitonToolStripMenuItem"].Enabled = true;
            }
            else
            {
                cmsApplications.Items["CancelApplicaitonToolStripMenuItem"].Enabled = false;
            }

            _HandleTestsInMenu(LDLAppID, passedTests, isConverted);

            // issue license
            if ((passedTests == 3) && !clsLicense.IsApplicationIssuedTheLicense(_Temp_LDLApp.ApplicationID) && !clsLocalDrivingLicenseApp.IsAppCancelled(LDLAppID))
            {
                cmsApplications.Items["issueDrivingLicenseFirstTimeToolStripMenuItem"].Enabled = true;
                cmsApplications.Items["showLicenseToolStripMenuItem"].Enabled = false;
            }
            else
            {
                cmsApplications.Items["issueDrivingLicenseFirstTimeToolStripMenuItem"].Enabled = false;
                cmsApplications.Items["showLicenseToolStripMenuItem"].Enabled = true;
            }
            // show license
            if (!clsLicense.IsApplicationIssuedTheLicense(_Temp_LDLApp.ApplicationID))
            {
                cmsApplications.Items["showLicenseToolStripMenuItem"].Enabled = false;
            }
            else
            {
                cmsApplications.Items["showLicenseToolStripMenuItem"].Enabled = true;
            }

            //cmsApplications.Items["showPersonLicenseHistoryToolStripMenuItem"].Enabled = false;
        }
        private void _onChangeDetected(object sender, bool isChangingDetected)
        {
            if (isChangingDetected)
                _RefrashTable();
        }
        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication();
            frm.RefreshTableEvent += _onChangeDetected;
            frm.ShowDialog();
        }
        private void frmListLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            tbFilterValue.TextChanged += _Filter;
            _RefrashTable();
            cbFilterBy.SelectedIndex = 0;
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 0)
            {
                tbFilterValue.Text = "";
                tbFilterValue.Visible = false;
            }
            else
            {
                tbFilterValue.Visible = true;
            }
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value.ToString(), out int LDLAppID))
            {
                frmLocalDrivingLicenseApplicationInfo frm = new frmLocalDrivingLicenseApplicationInfo(LDLAppID);
                frm.ShowDialog();
            }
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int.TryParse(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value.ToString(), out int result);
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication(result);
            frm.RefreshTableEvent += _onChangeDetected;
            frm.ShowDialog();
        }
        private void DeleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string LDLAppID = dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value.ToString();
            int.TryParse(LDLAppID, out int resultID);
            DialogResult result = MessageBox.Show(
            $"Are You Sure You Want to Delete Application with '{LDLAppID}' ID ?",
            "Validation",
            MessageBoxButtons.OKCancel,
            MessageBoxIcon.Exclamation,
            MessageBoxDefaultButton.Button2
                        );
            if (result == DialogResult.OK)
            {
                if (!clsLocalDrivingLicenseApp.DeleteLocalDrivingLicenseApp(resultID))
                {
                    MessageBox.Show($"Deleting Failed.\n\n{clsShared.lastError}");
                    return;
                }
                _RefrashTable();
            }
        }
        private void CancelApplicaitonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value.ToString(), out int LDLAppID))
            {
                clsLocalDrivingLicenseApp LDLApp = clsLocalDrivingLicenseApp.FindLocalLicenseAppWithID(LDLAppID);
                if (LDLApp != null)
                {
                    DialogResult result = MessageBox.Show(
                        $"Are You Sure You Want to Cancel Application with '{LDLAppID}' ID ?",
                        "Validation",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.OK)
                    {
                        if (LDLApp.Cancel())
                        {
                            MessageBox.Show("Application Cancelled Successfully!");
                            _RefrashTable();
                        }
                        else
                        {
                            MessageBox.Show("Application Can`t Cancelled!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Not Valid ID, Couldn`t open the form!");
                }
            }
        }
        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value.ToString(), out int LDLAppID))
            {
                frmListTestAppointments frm = new frmListTestAppointments(LDLAppID, clsTestType.enTestTypes.VisionTest);
                frm.ShowDialog();
                _RefrashTable();
            }
        }
        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value.ToString(), out int LDLAppID))
            {
                frmListTestAppointments frm = new frmListTestAppointments(LDLAppID, clsTestType.enTestTypes.WrittenTest);
                frm.ShowDialog();
                _RefrashTable();
            }
        }
        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value.ToString(), out int LDLAppID))
            {
                frmListTestAppointments frm = new frmListTestAppointments(LDLAppID, clsTestType.enTestTypes.StreetTest);
                frm.ShowDialog();
                _RefrashTable();
            }
        }
        private void dgvLocalDrivingLicenseApplications_RowContextMenuStripNeeded(object sender, DataGridViewRowContextMenuStripNeededEventArgs e)
        {
            _PrepareContextMentStrip();
        }
        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value.ToString(), out int LDLAppID))
            {
                frmIssueDriverLicenseFirstTime frm = new frmIssueDriverLicenseFirstTime(LDLAppID);
                frm.ShowDialog();
                _RefrashTable();
            }
        }
        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value.ToString(), out int LDLAppID))
            {
                int LicenseID = -1;
                if (clsLicense.GetLicenseIDByLocalDrivingLicenseAppID(ref LicenseID, LDLAppID))
                {
                    frmShowLicensesInfo frm = new frmShowLicensesInfo(LicenseID);
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Can`t find License with id " + LicenseID.ToString());
                }
            }
            else
            {
                MessageBox.Show("Local driving license app id makes a problem");
            }
        }
        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_Temp_LDLApp.PersonID > 0)
            {
                frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(_Temp_LDLApp.PersonID);
                frm.ShowDialog();
            }
        }
    }
}