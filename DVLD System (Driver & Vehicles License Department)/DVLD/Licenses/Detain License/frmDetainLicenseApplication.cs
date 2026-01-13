using System;
using System.Windows.Forms;
using BusinessLayer;
using DVLD.Licenses.Local_Licenses;
using Shared;

namespace DVLD.Licenses.Detain_License
{
    public partial class frmDetainLicenseApplication : Form
    {
        public frmDetainLicenseApplication()
        {
            InitializeComponent();
        }
        clsLicense License = new clsLicense();
        clsDetaining Detaining = new clsDetaining();

        private void _FillFieldsWithInfo(bool isDetainingObjectFilled = false)
        {
            if (isDetainingObjectFilled)
            {
                lblDetainID.Text = Detaining.DetainingID.ToString();
                lblLicenseID.Text = Detaining.LicenseID.ToString();
                lblDetainDate.Text = DateTime.Now.ToString();
                lblLicenseID.Text = License.LicenseID.ToString();
                lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
                txtFineFees.Text = Detaining.FineFees.ToString();
                return;
            }
            lblDetainDate.Text = DateTime.Now.ToString();
            lblLicenseID.Text = License.LicenseID.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }
        private void _FillDetainingClassWithInfo()
        {
            Detaining.LicenseID = License.LicenseID;
            Detaining.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            if (decimal.TryParse(txtFineFees.Text, out decimal result))
            {
                Detaining.FineFees = result;
            }
        }
        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(License.DriverInfo.PersonID);
            frm.ShowDialog();
        }
        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensesInfo frm = new frmShowLicensesInfo(Detaining.LicenseID);
            frm.ShowDialog();
        }
        private void ctrlDrivingLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            License = clsLicense.FindByLicenseID(obj);
            if (License.LicenseID > 0)
            {
                _FillFieldsWithInfo();
                btnDetain.Enabled = true;
                llShowLicenseHistory.Enabled = true;
            }
        }
        private void btnDetain_Click(object sender, EventArgs e)
        {
            _FillDetainingClassWithInfo();
            if (Detaining.DetainLicense())
            {
                _FillFieldsWithInfo(true);
                MessageBox.Show("License Detained Successfully.");
                btnDetain.Enabled = false;
                llShowLicenseInfo.Enabled = true;
                ctrlDrivingLicenseInfoWithFilter1.Enabled = false;
            }
            else
            {
                MessageBox.Show($"Error Detaining License.\n{clsShared.lastError}");
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
