using System;
using System.Windows.Forms;
using BusinessLayer;
using DVLD.Licenses;
using DVLD.Licenses.Local_Licenses;
using Shared;

namespace DVLD.Applications.Renew_Local_License
{
    public partial class frmRenewLocalDrivingLicenseApplication : Form
    {
        public frmRenewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }
        clsLicense _ExistingLicense = new clsLicense();
        clsLicense _NewLicsense = new clsLicense();
        private void _FillFieldsWithExpectedInfo()
        {
            if (_ExistingLicense != null)
            {
                lblApplicationDate.Text = DateTime.Now.ToString();
                lblIssueDate.Text = DateTime.Now.ToString();
                lblApplicationFees.Text = _ExistingLicense.ApplicationInfo.ApplicationType.ApplicationTypeFees.ToString();
                lblLicenseFees.Text = _ExistingLicense.LicenseClassInfo.ClassFees.ToString();
                lblOldLicenseID.Text = _ExistingLicense.LicenseID.ToString();
                lblExpirationDate.Text = DateTime.Now.AddYears(_ExistingLicense.LicenseClassInfo.DefaultValidityLength).ToString();
                lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
                lblTotalFees.Text = (_ExistingLicense.ApplicationInfo.ApplicationType.ApplicationTypeFees + (double)_ExistingLicense.LicenseClassInfo.ClassFees).ToString();
                txtNotes.Text = _ExistingLicense.Notes;
            }
        }
        private void _FillNewLicenseWithInfo()
        {
            _NewLicsense = _ExistingLicense;
            _NewLicsense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _NewLicsense.Notes = txtNotes.Text;

        }

        private void ctrlDrivingLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _ExistingLicense = clsLicense.FindByLicenseID(obj);
            if (_ExistingLicense != null)
            {
                _FillFieldsWithExpectedInfo();
                btnRenewLicense.Enabled = true;
                llShowLicenseHistory.Enabled = true;
            }
        }
        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            if (_ExistingLicense != null)
            {
                _FillNewLicenseWithInfo();
                int newlicenseID = -1;
                if (_ExistingLicense.RenewLicense(ref newlicenseID, _NewLicsense))
                {
                    btnRenewLicense.Enabled = false;
                    llShowLicenseInfo.Enabled = true;
                    MessageBox.Show($"License Renewed Successfully!\n New License ID: {newlicenseID}");
                }
                else
                {
                    MessageBox.Show($"Can`t Renew!\n {clsShared.lastError}");
                }
            }
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(_ExistingLicense.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensesInfo frm = new frmShowLicensesInfo(_NewLicsense.LicenseID);
            frm.ShowDialog();
        }
    }
}
