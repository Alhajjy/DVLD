using System;
using System.Data;
using System.Windows.Forms;
using BusinessLayer;
using DVLD.Properties;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Tests
{
    public partial class frmListTestAppointments : Form
    {
        public frmListTestAppointments(int LDLAppID, clsTestType.enTestTypes TestTypeID)
        {
            InitializeComponent();
            ctrlDrivingLicneseApplicationInfo1.LoadLocalAppInfo(LDLAppID);
            LocalLicenseAppID = LDLAppID;
            TestType = clsTestType.GetTestType((int)TestTypeID);
            _ResetForm(TestTypeID);
        }
        int LocalLicenseAppID { get; set; }
        DataTable Appointments_DT { get; set; }
        clsTestType TestType { get; set; }

        private void _ResetForm(clsTestType.enTestTypes TestTypeID)
        {
            switch (TestTypeID)
            {
                case clsTestType.enTestTypes.VisionTest:
                    pbTestTypeImage.Image = Resources.Vision_512;
                    lblTitle.Text = "Vision Test Appointments";
                    break;
                case clsTestType.enTestTypes.WrittenTest:
                    pbTestTypeImage.Image = Resources.Written_Test_512;
                    lblTitle.Text = "Written Test Appointments";
                    break;
                case clsTestType.enTestTypes.StreetTest:
                    pbTestTypeImage.Image = Resources.driving_test_512;
                    lblTitle.Text = "Street Test Appointments";
                    break;
            }
        }
        private void _RefreshAppointmentsTable()
        {
            Appointments_DT = clsTestAppointment.GetAllTestAppointmentsForThisApplicationByTestTypeID(LocalLicenseAppID, (int)TestType.TestTypeID);
            dgvLicenseTestAppointments.DataSource = Appointments_DT;
            lblRecordsCount.Text = Appointments_DT.Rows.Count.ToString();
        }
        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            if (clsTestAppointment.IsAvailableToOpenTestAppointment(LocalLicenseAppID, (int)TestType.TestTypeID))
            {
                frmScheduleTest frm = new frmScheduleTest(LocalLicenseAppID, TestType.TestTypeID);
                frm.ShowDialog();
                _RefreshAppointmentsTable();
            }
        }

        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _RefreshAppointmentsTable();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dgvLicenseTestAppointments.CurrentRow.Cells[0].Value.ToString(), out int TestAppointmentId))
            {
                if (!clsTestAppointment.IsTestAppointmentLocked(TestAppointmentId))
                {
                    frmScheduleTest frm = new frmScheduleTest(LocalLicenseAppID, TestType.TestTypeID, TestAppointmentId);
                    frm.ShowDialog();
                    _RefreshAppointmentsTable();
                }
                else
                {
                    MessageBox.Show("It`s locked!! So can`t be Edited!");
                }
            }
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dgvLicenseTestAppointments.CurrentRow.Cells[0].Value.ToString(), out int TestAppointmentId))
            {
                if (clsTestAppointment.IsTestAppointmentLocked(TestAppointmentId))
                {
                    MessageBox.Show("Appointment is Locked..");
                    return;
                }
                frmTakeTest frm = new frmTakeTest(TestAppointmentId, TestType.TestTypeID);
                frm.ShowDialog();
                _RefreshAppointmentsTable();
            }
        }

        private void dgvLicenseTestAppointments_RowContextMenuStripNeeded(object sender, DataGridViewRowContextMenuStripNeededEventArgs e)
        {
            if (int.TryParse(dgvLicenseTestAppointments.CurrentRow.Cells[0].Value.ToString(), out int TestAppointmentId))
                if (!clsTestAppointment.IsTestAppointmentLocked(TestAppointmentId))
                    cmsApplications.Items[1].Enabled = true;
                else
                    cmsApplications.Items[1].Enabled = false;
        }
    }
}
