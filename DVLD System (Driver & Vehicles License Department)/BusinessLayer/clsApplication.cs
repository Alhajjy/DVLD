using System;
using System.Data;
using DataAccessLayer;
using Shared;

namespace BusinessLayer
{
    public class clsApplication
    {
        public enum enMode { Add, Update }
        public enMode Mode;
        public enum enStatus { New = 1, Cancelled, Completed }
        public enum enApplicationType
        {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        }
        public int ApplicationID { get; set; }
        public enApplicationType ApplicationTypeID { get; set; }
        public clsApplicationType ApplicationType { get; set; }
        public int PersonID { get; set; }
        public clsPerson Person { get; set; }
        public DateTime ApplicationDate { get; set; }
        public enStatus Status { get; set; }
        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser User { get; set; }

        public clsApplication()
        {
            Mode = enMode.Add;
            ApplicationID = -1;
            ApplicationTypeID = enApplicationType.NewInternationalLicense;
            PersonID = -1;
            ApplicationDate = DateTime.MinValue;
            Status = enStatus.New;
            LastStatusDate = DateTime.MinValue;
            PaidFees = -1m;
            CreatedByUserID = -1;
        }
        private clsApplication(int applicationID, enApplicationType applicationTypeID, int personID, DateTime applicationDate, enStatus status, DateTime lastStatusDate, decimal paidFees, int createdByUserID)
        {
            Mode = enMode.Update;
            ApplicationID = applicationID;
            ApplicationTypeID = applicationTypeID;
            ApplicationType = clsApplicationType.GetApplicationType((int)applicationTypeID);
            PersonID = personID;
            Person = clsPerson.FindPersonWithID(personID);
            ApplicationDate = applicationDate;
            Status = status;
            LastStatusDate = lastStatusDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
            User = clsUser.GetUserByID(createdByUserID);
        }
        public static DataTable GetAllApplications()
        {
            return DataAccessLayer.clsApplicationData.GetAllApplications();
        }
        public static clsApplication FindApplicationWithID(int applicationID)
        {
            int applicationId = applicationID;
            int applicationTypeID = -1;
            int personID = -1;
            DateTime applicationDate = DateTime.MinValue;
            int status = (int)enStatus.New;
            DateTime lastStatusDate = DateTime.MinValue;
            decimal paidFees = -1m;
            int createdByUserID = -1;
            if (clsApplicationData.GetApplicationByID(applicationId, ref applicationTypeID, ref personID, ref applicationDate, ref status, ref lastStatusDate, ref paidFees, ref createdByUserID))
            {
                return new clsApplication(applicationId, (enApplicationType)applicationTypeID, personID, applicationDate, (enStatus)status, lastStatusDate, paidFees, createdByUserID);
            }
            return null;
        }
        private bool _AddApplication()
        {
            int appId = -1;
            if (DataAccessLayer.clsApplicationData.InsertApplication(ref appId, (int)ApplicationTypeID, PersonID, ApplicationDate, (int)Status, LastStatusDate, PaidFees, CreatedByUserID))
            {
                this.ApplicationID = appId;
                return true;
            }
            return false;
        }
        private bool _UpdateApplication()
        {
            return DataAccessLayer.clsApplicationData.UpdateApplication(ApplicationID, (int)ApplicationTypeID, PersonID, ApplicationDate, (int)Status, LastStatusDate, PaidFees, CreatedByUserID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    bool success = _AddApplication();
                    Mode = enMode.Update;
                    return success;
                case enMode.Update:
                    return _UpdateApplication();
                default:
                    return false;
            }
        }
        // other
        public bool Cancel()
        {
            return clsApplicationData.SetApplicationStatusCancelled(this.ApplicationID);
        }
        public bool SetApplicationCompleted()
        {
            return clsApplicationData.SetApplicationStatusCompleted(this.ApplicationID);
        }
        public static bool SetApplicationCompleted(int applicationID)
        {
            return clsApplicationData.SetApplicationStatusCompleted(applicationID);
        }
        public static bool DeleteApplication(int applicationID)
        {
            return DataAccessLayer.clsApplicationData.DeleteApplication(applicationID);
        }
        public static bool IsApplicationExists(int applicationId)
        {
            return clsApplicationData.IsApplicationExists(applicationId);
        }
        public static bool IsApplicationStatus_(int applicationId, enStatus applicationStatus)
        {
            return clsApplicationData.IsApplicationStatus_(applicationId, (int)applicationStatus);
        }

        public static int GetActiveApplicationIDForLicenseClass(int ApplicationTypeID, int PersonID, int LicenseClassID)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(ApplicationTypeID, PersonID, LicenseClassID);
        }
        public static bool GetApplicantIDByAppID(int applicationID, ref int personID)
        {
            return clsApplicationData.GetApplicantIdByAppID(applicationID, ref personID);
        }
    }
}