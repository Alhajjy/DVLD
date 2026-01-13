using System;
using System.Windows.Forms;
using BusinessLayer;

namespace DVLD.Tests
{
    public partial class frmScheduleTest : Form
    {
        public frmScheduleTest(int LDLAppID, clsTestType.enTestTypes TestTypeID, int TestAppointmentID = -1)
        {
            InitializeComponent();
            ctrlScheduleTest1.LoadData(LDLAppID, TestTypeID, TestAppointmentID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
