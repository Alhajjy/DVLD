using System;
using System.Data;
using DataAccessLayer;
using Shared;
using static BusinessLayer.clsApplication;

namespace BusinessLayer
{
    public class clsTestAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int TestAppointmentID { set; get; }
        public int TestTypeID { set; get; }
        public int LocalLicenseApplicationID { set; get; }
        public DateTime AppointmentDate { set; get; }
        public decimal PaidFees { set; get; }
        public int CreatedByUserID { set; get; }
        public bool IsLocked { set; get; }
        public clsTestType TestTypeInfo;

        public clsTestAppointment()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = -1;
            this.LocalLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.MinValue;
            this.PaidFees = -1;
            this.CreatedByUserID = -1;
            this.IsLocked = false;
            Mode = enMode.AddNew;
        }
        private clsTestAppointment(int TestAppointmentID, int TestTypeID, int LocalLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalLicenseApplicationID = LocalLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;

            TestTypeInfo = clsTestType.GetTestType(TestTypeID);
            Mode = enMode.Update;
        }
        private bool _AddNewTestAppointment()
        {
            if (!clsTestAppointmentData.IsAvailableToOpenTestAppointmentByTestType(LocalLicenseApplicationID, TestTypeID))
            {
                clsShared.lastError = "Person Is not Allowed to opern new appointment with this test type!";
                return false;
            }
            if (GetNumberOfTrialsForTestType(LocalLicenseApplicationID, TestTypeID) > 0) {
                clsApplication app = new clsApplication();
                // fill info in app object
                int personId = -1;
                if (!clsLocalDrivingLicenseApp.GetApplicantPersonIDByLDLAppID(LocalLicenseApplicationID, ref personId))
                {
                    return false;
                }
                // initialize data
                app.ApplicationTypeID = clsApplication.enApplicationType.RetakeTest;
                app.PersonID = personId;
                app.ApplicationDate = DateTime.MinValue;
                app.Status = enStatus.New;
                app.LastStatusDate = DateTime.MinValue;
                app.PaidFees = (decimal) clsApplicationType.GetApplicationType((int)enApplicationType.RetakeTest).ApplicationTypeFees;
                app.CreatedByUserID = -1;
                // save
                app.Save();
            }
            this.TestAppointmentID = (int)clsTestAppointmentData.AddNewTestAppointment(this.TestTypeID, this.LocalLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked);
            return (this.TestAppointmentID != -1);
        }
        private bool _UpdateTestAppointment()
        {
            if (!clsTestAppointmentData.IsTestAppointmentLocked(TestAppointmentID))
                return clsTestAppointmentData.UpdateTestAppointment(this.TestAppointmentID, this.TestTypeID, this.LocalLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked);
            return false;
        }
        public static bool DeleteTestAppointment(int TestAppointmentID)
        {
            return clsTestAppointmentData.DeleteTestAppointment(TestAppointmentID);
        }
        public static bool IsTestAppointmentExistByTestAppointmentID(int TestAppointmentID)
        {
            return clsTestAppointmentData.IsTestAppointmentExist(TestAppointmentID);
        }
        public static clsTestAppointment FindByTestAppointmentID(int TestAppointmentID)
        {
            int TestTypeID = -1;
            int LocalLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.MinValue;
            decimal PaidFees = -1;
            int CreatedByUserID = -1;
            bool IsLocked = false;

            bool IsFound = clsTestAppointmentData.GetTestAppointmentByID(TestAppointmentID, ref TestTypeID, ref LocalLicenseApplicationID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked);

            if (IsFound)
                return new clsTestAppointment(TestAppointmentID, TestTypeID, LocalLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked);
            else
                return null;
        }
        public bool Save()
        {
            if (clsLocalDrivingLicenseApp.IsAppCancelled(LocalLicenseApplicationID))
            {
                clsShared.lastError = "Cannot save the test appointment because the associated local driving license application is cancelled.";
                return false;
            }

            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddNewTestAppointment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateTestAppointment();
            }
            return false;
        }
        public static DataTable GetTestAppointments()
        {
            return clsTestAppointmentData.GetAllTestAppointments();
        }
        public static DataTable GetAllTestAppointmentsForThisApplicationByTestTypeID(int LDLApp, int testTypeID)
        {
            return clsTestAppointmentData.GetAllTestAppointmentsForThisApplicationByTestTypeID(LDLApp, testTypeID);
        }
        public static int GetNumberOfTrialsForTestType(int LDLAppID, int TestTypeID)
        {
            return clsTestAppointmentData.GetTestAppointmentTrails(LDLAppID, TestTypeID);
        }
        public static bool LockTheAppointment(int appointmentId)
        {
            return clsTestAppointmentData.SetTestAppointmentLocked(appointmentId);
        }
        public static bool IsTestAppointmentLocked(int appointmentId)
        {
            return clsTestAppointmentData.IsTestAppointmentLocked(appointmentId);
        }
        public static int GetPassedTestsNumber(int localLicenseApplicationID)
        {
            return clsTestAppointmentData.GetPassedTestsNumber(localLicenseApplicationID);
        }
        public static bool IsAvailableToOpenTestAppointment(int LocalLicenseApplicationID, int TestTypeID)
        {
            return clsTestAppointmentData.IsAvailableToOpenTestAppointmentByTestType(LocalLicenseApplicationID, TestTypeID);
        }
    }
}
