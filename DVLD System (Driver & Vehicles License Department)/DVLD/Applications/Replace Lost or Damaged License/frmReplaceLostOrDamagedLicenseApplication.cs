using System;
using System.Windows.Forms;
using BusinessLayer;
using DVLD.Licenses;
using DVLD.Licenses.Local_Licenses;
using Shared;

namespace DVLD.Applications.Replace_Lost_or_Damaged_License
{
    public partial class frmReplaceLostOrDamagedLicenseApplication : Form
    {
        public frmReplaceLostOrDamagedLicenseApplication()
        {
            InitializeComponent();
        }
        clsLicense _ExistingLicense = new clsLicense();
        clsLicense _NewLicense = new clsLicense();
        private clsLicense.enIssueReason? _DamagedOrLostConf()
        {
            if (rbDamagedLicense.Checked == true)
            {
                lblApplicationFees.Text = clsApplicationType.GetApplicationType((int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense).ApplicationTypeFees.ToString();
                return clsLicense.enIssueReason.DamagedReplacement;
            }
            else if (rbLostLicense.Checked == true)
            {
                lblApplicationFees.Text = clsApplicationType.GetApplicationType((int)clsApplication.enApplicationType.ReplaceLostDrivingLicense).ApplicationTypeFees.ToString();
                return clsLicense.enIssueReason.LostReplacement;
            }
            lblApplicationFees.Text = "[???]";
            return null;
        }
        private void _FillNewLicenseWithInfo()
        {
            _NewLicense = _ExistingLicense;
        }
        private void _FillFieldsWithExpectedInfo()
        {
            lblApplicationDate.Text = DateTime.Now.ToString();
            lblApplicationFees.Text = clsApplicationType.GetApplicationType((int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense).ApplicationTypeFees.ToString();
            lblApplicationDate.Text = DateTime.Now.ToString();
            lblOldLicenseID.Text = _ExistingLicense.LicenseID.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }
        private void _FillFieldsWithNewLicenseInfo()
        {
            lblApplicationID.Text = _NewLicense.ApplicationID.ToString();
            lblRreplacedLicenseID.Text = _NewLicense.LicenseID.ToString();
            lblApplicationDate.Text = _NewLicense.ApplicationID.ToString();
            lblApplicationFees.Text = _NewLicense.ApplicationInfo.PaidFees.ToString();
            lblApplicationDate.Text = _NewLicense.ApplicationInfo.ApplicationDate.ToString();
            lblCreatedByUser.Text = clsUser.GetUserByID(_NewLicense.CreatedByUserID).UserName;
        }
        private void frmReplaceLostOrDamagedLicenseApplication_Load(object sender, EventArgs e)
        {

        }
        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            _DamagedOrLostConf();
        }
        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            _DamagedOrLostConf();
        }
        private void ctrlDrivingLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _ExistingLicense = clsLicense.FindByLicenseID(obj);
            _FillNewLicenseWithInfo();
            _FillFieldsWithExpectedInfo();
            llShowLicenseHistory.Enabled = true;
            btnIssueReplacement.Enabled = true;
        }
        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(_ExistingLicense.DriverInfo.PersonID);
            frm.ShowDialog();
        }
        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensesInfo frm = new frmShowLicensesInfo(_NewLicense.LicenseID);
            frm.ShowDialog();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            clsLicense.enIssueReason? issueReason = _DamagedOrLostConf();
            if (issueReason == null)
            {
                MessageBox.Show("Choose if Damaged or Lost!");
                return;
            }

            if (_NewLicense.ReplaceLostOrDamagedLicense(clsGlobal.CurrentUser.UserID, (clsLicense.enIssueReason)issueReason))
            {
                _NewLicense = clsLicense.FindByLicenseID(_NewLicense.LicenseID);
                _FillFieldsWithNewLicenseInfo();
                llShowLicenseInfo.Enabled = true;
                btnIssueReplacement.Enabled = false;
                MessageBox.Show("Replacement done successfully..");
            }
            else
            {
                MessageBox.Show($"somthing went wrong:\n{clsShared.lastError}");
            }
        }
    }
}
