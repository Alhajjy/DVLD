using System;
using System.Windows.Forms;
using BusinessLayer;

namespace DVLD.Applications.Local_Driving_License
{
    public partial class ctrlDrivingLicneseApplicationInfo : UserControl
    {
        public ctrlDrivingLicneseApplicationInfo()
        {
            InitializeComponent();
        }
        public clsLocalDrivingLicenseApp LocalDrivingLicenseApp = new clsLocalDrivingLicenseApp();
        private bool _FillFieldsWithInfo()
        {
            lblLocalDrivingLicenseApplicationID.Text = LocalDrivingLicenseApp.LocalLicenseAppID.ToString();
            lblAppliedFor.Text = LocalDrivingLicenseApp.LicenseClassInfo.ClassName;
            lblPassedTests.Text = clsTestAppointment.GetPassedTestsNumber(LocalDrivingLicenseApp.LocalLicenseAppID).ToString();
            if (!ctrlApplicationBasicInfo1.LoadAppInfo(LocalDrivingLicenseApp.ApplicationID))
            {
                return false;
            }
            return true;
        }
        public bool LoadLocalAppInfo(int LDLAppID)
        {
            if (!clsLocalDrivingLicenseApp.IsLocalDrivingLicenseAppExists(LDLAppID))
            {
                MessageBox.Show("Local Driving License Application Does`nt Exists!");
                return false;
            }
            else
            {
                LocalDrivingLicenseApp = clsLocalDrivingLicenseApp.FindLocalLicenseAppWithID(LDLAppID);
                if (_FillFieldsWithInfo())
                    return true;
                return false;
            }
        }
    }
}
