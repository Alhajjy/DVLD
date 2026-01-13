using System;
using System.Windows.Forms;
using BusinessLayer;

namespace DVLD.Licenses.International_Licenses.Controls
{
    public partial class ctrlDriverInternationalLicenseInfo : UserControl
    {
        public ctrlDriverInternationalLicenseInfo()
        {
            InitializeComponent();
        }
        clsInternationalLicense InternationalLicense = new clsInternationalLicense();

        public void LoadInternationalLicenseInfo(int InternationalLicenseID)
        {
            InternationalLicense = clsInternationalLicense.FindByInternationalLicenseID(InternationalLicenseID);
            if (InternationalLicense != null)
            {
                lblFullName.Text = InternationalLicense.LocalLicenseInfo.DriverInfo.PersonInfo.FullName;
                lblInternationalLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
                lblApplicationID.Text = InternationalLicense.ApplicationID.ToString();
                lblLocalLicenseID.Text = InternationalLicense.LocalLicenseID.ToString();
                lblIsActive.Text = InternationalLicense.IsActive ? "Yes" : "No";
                lblNationalNo.Text = InternationalLicense.LocalLicenseInfo.DriverInfo.PersonInfo.NationalNo;
                lblDateOfBirth.Text = InternationalLicense.LocalLicenseInfo.DriverInfo.PersonInfo.BirthDate.ToString();
                lblGendor.Text = InternationalLicense.LocalLicenseInfo.DriverInfo.PersonInfo.Gender ? "Male" : "Female";
                lblDriverID.Text = InternationalLicense.DriverID.ToString();
                lblIssueDate.Text = InternationalLicense.IssueDate.ToString("yyyy-MM-dd");
                lblExpirationDate.Text = InternationalLicense.ExpirationDate.ToString("yyyy-MM-dd");
                pbPersonImage.ImageLocation = InternationalLicense.LocalLicenseInfo.DriverInfo.PersonInfo.ImagePath;
            }
            else
            {
                MessageBox.Show("No DATA!");
            }
        }
    }
}
