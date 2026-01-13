using System;
using System.Windows.Forms;
using BusinessLayer;
using DVLD.Licenses;
using DVLD.Licenses.Local_Licenses;
using Shared;

namespace DVLD.Applications.International_License
{
    public partial class frmNewInternationalLicenseApplication : Form
    {
        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
        }
        clsLicense License = new clsLicense();
        clsInternationalLicense InternationalLicense = new clsInternationalLicense();
        private void _FillFieldsWithExpectedInfo()
        {
            if (License != null)
            {
                lblInternationalLicenseID.Text = "[???]";
                lblApplicationID.Text = "[???]";
                lblApplicationDate.Text = DateTime.Now.ToString();
                lblLocalLicenseID.Text = License.LicenseID.ToString();
                lblIssueDate.Text = License.IssueDate.ToString();
                lblExpirationDate.Text = License.ExpirationDate.ToString();
                lblFees.Text = clsApplicationType.GetApplicationType((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationTypeFees.ToString();
                lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            }
        }
        private void _FillOtherFieldsWithInternationalLicensenfo()
        {
            if (InternationalLicense != null)
            {
                lblInternationalLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
                lblApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            }
        }

        private void ctrlDrivingLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            if (obj != -1)
            {
                License = clsLicense.FindByLicenseID(obj);
                _FillFieldsWithExpectedInfo();
                if (License != null)
                {
                    llShowLicenseHistory.Enabled = true;
                    btnIssueLicense.Enabled = true;
                }
            }
        }
        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(License.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensesInfo frm = new frmShowLicensesInfo(License.LicenseID);
            frm.ShowDialog();
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            if (License != null)
            {
                int internationalLicenseID = -1;
                if (License.IssueInternationalDrivingLicense(ref internationalLicenseID, clsGlobal.CurrentUser.UserID))
                {
                    MessageBox.Show($"International Driving License issued successfully.\nID: {internationalLicenseID}");
                    btnIssueLicense.Enabled = false;
                    llShowLicenseInfo.Enabled = true;
                    ctrlDrivingLicenseInfoWithFilter1.Enabled = false;
                    InternationalLicense = clsInternationalLicense.FindByInternationalLicenseID(internationalLicenseID);
                    _FillOtherFieldsWithInternationalLicensenfo();
                }
                else
                {
                    MessageBox.Show($"Failed to issue International Driving License.\n{clsShared.lastError}");
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
