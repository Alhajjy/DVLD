using System;
using System.Windows.Forms;
using BusinessLayer;

namespace DVLD.Licenses.Local_Licenses
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }
        clsLicense License = new clsLicense();
        private void _HandleImage()
        {
            if (string.IsNullOrEmpty(License.DriverInfo.PersonInfo.ImagePath))
                pbPersonImage.Image = (License.DriverInfo.PersonInfo.Gender) ? Properties.Resources.male_user_128 : Properties.Resources.female_128;
            else
                pbPersonImage.ImageLocation = License.DriverInfo.PersonInfo.ImagePath;
        }
        private void _FillFieldsWithInfo()
        {
            if (License != null)
            {
                lblClass.Text = License.LicenseClassInfo.ClassName;
                lblFullName.Text = License.DriverInfo.PersonInfo.FullName;
                lblLicenseID.Text = License.LicenseID.ToString();
                lblNationalNo.Text = License.DriverInfo.PersonInfo.NationalNo;
                lblGendor.Text = License.DriverInfo.PersonInfo.Gender ? "Male" : "Female";
                lblIssueDate.Text = License.IssueDate.ToString("yyyy-MM-dd");
                lblIssueReason.Text = clsLicense.GetIssueReasonAsString((clsLicense.enIssueReason)License.IssueReason);
                if (!string.IsNullOrEmpty(License.Notes))
                    lblNotes.Text = License.Notes;
                else
                    lblNotes.Text = "---";
                lblIsActive.Text = License.IsActive ? "Yes" : "No";
                lblDateOfBirth.Text = License.DriverInfo.PersonInfo.BirthDate.ToString("yyyy-MM-dd");
                lblDriverID.Text = License.DriverID.ToString();
                lblExpirationDate.Text = License.ExpirationDate.ToString("yyyy-MM-dd");
                if (clsDetaining.IsLicenseDetained(License.LicenseID))
                    lblIsDetained.Text = "Yes";
                else
                    lblIsDetained.Text = "No";
                _HandleImage();
            }
            else
            {
                MessageBox.Show("License not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void LoadDriverLicenseInfo(int LicenseID)
        {
            License = clsLicense.FindByLicenseID(LicenseID);
            _FillFieldsWithInfo();
        }
    }
}
