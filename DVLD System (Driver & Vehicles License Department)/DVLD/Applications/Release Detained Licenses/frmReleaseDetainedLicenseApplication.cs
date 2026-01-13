using System;
using System.ComponentModel;
using System.Windows.Forms;
using BusinessLayer;
using DVLD.Licenses;
using DVLD.Licenses.Local_Licenses;
using Shared;

namespace DVLD.Applications.Release_Detained_Licenses
{
    public partial class frmReleaseDetainedLicenseApplication : Form
    {
        public frmReleaseDetainedLicenseApplication(int LicenseID = -1)
        {
            InitializeComponent();
            if (LicenseID > 0)
            {
                ctrlDrivingLicenseInfoWithFilter1.LoadLicenseInfo(LicenseID);
                Detaining = clsDetaining.FindByLicenseID(LicenseID);
                License = clsLicense.FindByLicenseID(LicenseID);
                _FillFieldsWithExpectedInfo();
                llShowLicenseHistory.Enabled = true;
                btnRelease.Enabled = true;
            }
        }
        clsDetaining Detaining = new clsDetaining();
        clsLicense License = new clsLicense();
        clsApplication ReleaseApp = new clsApplication();
        private void _FillFieldsWithExpectedInfo()
        {
            if (License != null)
            {
                double appFees = clsApplicationType.GetApplicationType((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationTypeFees;
                decimal FineFees = Detaining.FineFees;

                lblDetainID.Text = Detaining.DetainingID.ToString();
                lblLicenseID.Text = License.LicenseID.ToString();
                lblDetainDate.Text = Detaining.DetainDate.ToString();
                lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
                lblApplicationFees.Text = appFees.ToString();
                lblFineFees.Text = FineFees.ToString();
                lblTotalFees.Text = (appFees + (double)FineFees).ToString();
                if (ReleaseApp != null && ReleaseApp.ApplicationID != -1)
                    lblApplicationID.Text = ReleaseApp.ApplicationID.ToString();
                else
                    lblApplicationID.Text = "[????]";
            }
        }
        private void ctrlDrivingLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            ctrlDrivingLicenseInfoWithFilter1.LoadLicenseInfo(obj);
            Detaining = clsDetaining.FindByLicenseID(obj);
            License = clsLicense.FindByLicenseID(obj);
            _FillFieldsWithExpectedInfo();
            llShowLicenseHistory.Enabled = true;
            btnRelease.Enabled = true;
        }
        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (Detaining.ReleaseDetainedLicense(clsGlobal.CurrentUser.UserID))
            {
                _FillFieldsWithExpectedInfo();
                llShowLicenseInfo.Enabled = true;
                btnRelease.Enabled = false;
                ctrlDrivingLicenseInfoWithFilter1.Enabled = false;
                MessageBox.Show("License Released Successfully!");
            } else
            {
                MessageBox.Show($"Can`t Release the license!\n{clsShared.lastError}");
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
