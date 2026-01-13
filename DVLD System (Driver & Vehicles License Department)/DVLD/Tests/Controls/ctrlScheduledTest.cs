using System;
using System.Windows.Forms;
using BusinessLayer;

namespace DVLD.Tests.Controls
{
    public partial class ctrlScheduledTest : UserControl
    {
        public ctrlScheduledTest()
        {
            InitializeComponent();
        }

        public clsTestAppointment TestAppointment = new clsTestAppointment();
        public clsLocalDrivingLicenseApp LocalDrivingLicenseApp = new clsLocalDrivingLicenseApp();

        public void LoadScheduledTestInfo(int TestAppointmentID, clsTestType.enTestTypes TestTypeID)
        {
            TestAppointment = clsTestAppointment.FindByTestAppointmentID(TestAppointmentID);
            LocalDrivingLicenseApp = clsLocalDrivingLicenseApp.FindLocalLicenseAppWithID(TestAppointment.LocalLicenseApplicationID);

            lblLocalDrivingLicenseAppID.Text = TestAppointment.LocalLicenseApplicationID.ToString();
            lblDrivingClass.Text = LocalDrivingLicenseApp.LicenseClassInfo.ClassName;
            lblFullName.Text = LocalDrivingLicenseApp.Person.FullName;
            lblTrial.Text = clsTestAppointment.GetNumberOfTrialsForTestType(TestAppointment.LocalLicenseApplicationID, (int)TestTypeID).ToString();
            lblDate.Text = TestAppointment.AppointmentDate.ToString();
            lblFees.Text = TestAppointment.PaidFees.ToString();
            lblTestID.Text = "Not taken yet..";
        }
    }
}
