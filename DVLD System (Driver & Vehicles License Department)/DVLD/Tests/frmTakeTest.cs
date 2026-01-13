using System;
using System.Windows.Forms;
using BusinessLayer;

namespace DVLD.Tests
{
    public partial class frmTakeTest : Form
    {
        public frmTakeTest(int TestAppointmentID, clsTestType.enTestTypes TestTypeID)
        {
            InitializeComponent();
            ctrlScheduledTest1.LoadScheduledTestInfo(TestAppointmentID, TestTypeID);
        }
        clsTest Test = new clsTest();

        private void _CollectDataToObject()
        {
            Test.TestAppointmentID = ctrlScheduledTest1.TestAppointment.TestAppointmentID;
            Test.TestResult = rbPass.Checked ? true : false;
            Test.Notes = txtNotes.Text;
            Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _CollectDataToObject();
            if (Test.Save())
            {
                MessageBox.Show("Test Done Successfully..");
                this.Close();
            }
            else
            {
                MessageBox.Show("Some thing went wrong");
            }
        }
    }
}
