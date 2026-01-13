using System;
using System.Windows.Forms;
using BusinessLayer;
using Shared;

namespace DVLD.Licenses.Local_Licenses
{
    public partial class frmIssueDriverLicenseFirstTime : Form
    {
        public frmIssueDriverLicenseFirstTime(int LocalDrivingLicenseID)
        {
            InitializeComponent();
            ctrlDrivingLicneseApplicationInfo1.LoadLocalAppInfo(LocalDrivingLicenseID);
        }
        clsLicense License = new clsLicense();
        private void _CollectDataToObject()
        {
            License.ApplicationID = ctrlDrivingLicneseApplicationInfo1.LocalDrivingLicenseApp.ApplicationID;
            License.LicenseClassID = ctrlDrivingLicneseApplicationInfo1.LocalDrivingLicenseApp.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(ctrlDrivingLicneseApplicationInfo1.LocalDrivingLicenseApp.LicenseClassInfo.DefaultValidityLength);
            License.Notes = tbNotes.Text;
            License.PaidFees = ctrlDrivingLicneseApplicationInfo1.LocalDrivingLicenseApp.LicenseClassInfo.ClassFees;
            License.IsActive = true;
            License.IssueReason = (byte)clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = clsGlobal.CurrentUser.UserID;
        }
        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            _CollectDataToObject();
            if (License.Save())
            {
                MessageBox.Show("Driver License Issued Successfully!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Error Issuing Driver License!");
                MessageBox.Show(clsShared.lastError);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
