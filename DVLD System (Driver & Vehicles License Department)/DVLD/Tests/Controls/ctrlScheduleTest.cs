using System;
using System.Windows.Forms;
using BusinessLayer;
using DVLD.Properties;
using Shared;

namespace DVLD.Tests.Controls
{
    public partial class ctrlScheduleTest : UserControl
    {
        public ctrlScheduleTest()
        {
            InitializeComponent();
        }
        enum enMode { Add, Update }
        enMode _mode = enMode.Add;
        enum enTestAppointmentMode { FirstTime, RetakingTest }
        enTestAppointmentMode _TestMode = enTestAppointmentMode.FirstTime;
        clsTestAppointment TestAppointment = new clsTestAppointment();
        clsLocalDrivingLicenseApp LocalDrivingLicenseApp = new clsLocalDrivingLicenseApp();
        byte Trails = 255;
        clsApplicationType _RetakeTestAppType = new clsApplicationType();

        private void _HandleTestTypeConf(clsTestType.enTestTypes TestTypeID)
        {
            switch (TestTypeID)
            {
                case clsTestType.enTestTypes.VisionTest:
                    pbTestTypeImage.Image = Resources.Vision_Test_32;
                    gbTestType.Text = "Vision Test";
                    break;
                case clsTestType.enTestTypes.WrittenTest:
                    pbTestTypeImage.Image = Resources.Written_Test_512;
                    gbTestType.Text = "Written Test";
                    break;
                case clsTestType.enTestTypes.StreetTest:
                    pbTestTypeImage.Image = Resources.driving_test_512;
                    gbTestType.Text = "Street Test";
                    break;
                default:
                    break;
            }
        }
        private void _RetakeTestConf(byte Trails, clsTestType.enTestTypes TestTypeID, int RetakeTestAppID = -1)
        {
            gbRetakeTestInfo.Enabled = true;
            _RetakeTestAppType = clsApplicationType.GetApplicationType((int)clsApplication.enApplicationType.RetakeTest);
            // view
            lblRetakeAppFees.Text = _RetakeTestAppType.ApplicationTypeFees.ToString();

            lblTotalFees.Text = ((double)clsTestType.GetTestType((int)TestTypeID).Fees + _RetakeTestAppType.ApplicationTypeFees).ToString();

            if (RetakeTestAppID > 0)
            {
                lblRetakeTestAppID.Text = RetakeTestAppID.ToString();
            }
            else
            {
                lblRetakeTestAppID.Text = "-1";
            }
        }
        public bool LoadData(int LDLAppID, clsTestType.enTestTypes TestTypeID, int TestAppointmentID = -1)
        {
            clsTestType TestTypeInfo = clsTestType.GetTestType((int)TestTypeID);
            // global
            _HandleTestTypeConf(TestTypeID);
            Trails = (byte)clsTestAppointment.GetNumberOfTrialsForTestType(LDLAppID, (int)TestTypeID);
            LocalDrivingLicenseApp = clsLocalDrivingLicenseApp.FindLocalLicenseAppWithID(LDLAppID);
            dtpTestDate.MinDate = DateTime.Now;

            lblLocalDrivingLicenseAppID.Text = LocalDrivingLicenseApp.LocalLicenseAppID.ToString();
            lblDrivingClass.Text = LocalDrivingLicenseApp.LicenseClassInfo.ClassName;
            lblFullName.Text = LocalDrivingLicenseApp.FullName;
            gbRetakeTestInfo.Enabled = false;
            lblFees.Text = TestTypeInfo.Fees.ToString();
            lblTrial.Text = Trails.ToString();

            if (TestAppointmentID < 0)
            {
                dtpTestDate.Value = DateTime.Now.AddDays(1);
                TestAppointment.TestTypeID = (int)TestTypeID;
            }
            else
            {
                _mode = enMode.Update;
                //if (DateTime.Compare(DateTime.Now, TestAppointment.AppointmentDate) > 0)
                //{
                //    dtpTestDate.MinDate = TestAppointment.AppointmentDate;
                //}
                //else
                //{
                dtpTestDate.MinDate = DateTime.Now;
                //}
                TestAppointment = clsTestAppointment.FindByTestAppointmentID(TestAppointmentID);
                dtpTestDate.Value = TestAppointment.AppointmentDate;
            }
            if (Trails > 0)
            {
                _TestMode = enTestAppointmentMode.RetakingTest;
                _RetakeTestConf(Trails, TestTypeID);
            }
            return true;
        }
        private void _CollectDataToObject()
        {
            TestAppointment.LocalLicenseApplicationID = LocalDrivingLicenseApp.LocalLicenseAppID;
            TestAppointment.AppointmentDate = dtpTestDate.Value;
            TestAppointment.PaidFees = clsTestType.GetTestType(1).Fees;
            TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            TestAppointment.IsLocked = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _CollectDataToObject();
            if (TestAppointment == null) return;
            if (clsLocalDrivingLicenseApp.DoesPassTestType(TestAppointment.LocalLicenseApplicationID, TestAppointment.TestTypeID))
            {
                MessageBox.Show("This Test Is Passed Already..");
                return;
            }
            if (TestAppointment.Save())
            {
                if (_mode == enMode.Add)
                {
                    MessageBox.Show("Test Appointment Added Successfully..");
                }
                else
                {
                    MessageBox.Show("Test Appointment Updated Successfully..");
                }
            }
            else
            {
                MessageBox.Show("Error Occured while saving!");
                MessageBox.Show(clsShared.lastError);
            }
        }
    }
}
