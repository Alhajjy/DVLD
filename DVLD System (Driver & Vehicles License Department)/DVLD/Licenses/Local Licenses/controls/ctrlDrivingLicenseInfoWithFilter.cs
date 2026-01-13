using System;
using System.Windows.Forms;
using BusinessLayer;

namespace DVLD.Licenses.Local_Licenses
{
    public partial class ctrlDrivingLicenseInfoWithFilter : UserControl
    {
        public ctrlDrivingLicenseInfoWithFilter()
        {
            InitializeComponent();
        }
        public event Action<int> OnLicenseSelected;
        protected virtual void LicenseSelected(int licneseID)
        {
            Action<int> handler = OnLicenseSelected;
            if (handler != null)
            {
                handler(licneseID);
            }
        }

        public void LoadLicenseInfo(int LicenseID)
        {
            txtLicenseID.Text = LicenseID.ToString();
            gbFilters.Enabled = false;
            driverLicenseInfo1.LoadDriverLicenseInfo(LicenseID);
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtLicenseID.Text, out int LicenseID))
            {
                LoadLicenseInfo(LicenseID);
                if (OnLicenseSelected != null)
                {
                    OnLicenseSelected(LicenseID);
                }
            }
            else
                MessageBox.Show("Enter a valid id Number");
        }
    }
}
