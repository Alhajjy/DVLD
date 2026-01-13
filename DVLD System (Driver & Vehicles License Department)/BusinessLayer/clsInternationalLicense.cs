using System;
using System.Data;
using DataAccessLayer;
using Shared;

namespace BusinessLayer
{
    public class clsInternationalLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int InternationalLicenseID { set; get; }
        public int ApplicationID { set; get; }
        public int DriverID { set; get; }
        public int LocalLicenseID { set; get; }
        public clsLicense LocalLicenseInfo { set; get; }
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public bool IsActive { set; get; }
        public int CreatedByUserID { set; get; }

        public clsInternationalLicense()
        {
            this.InternationalLicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LocalLicenseID = -1;
            this.IssueDate = DateTime.MinValue;
            this.ExpirationDate = DateTime.MinValue;
            this.IsActive = false;
            this.CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }
        private clsInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;
            LocalLicenseInfo = clsLicense.FindByLicenseID(IssuedUsingLocalLicenseID);
            Mode = enMode.Update;
        }
        private bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID = (int)clsInternationalLicenseData.AddNewInternationalLicense(this.ApplicationID, this.DriverID, this.LocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
            return (this.InternationalLicenseID != -1);
        }
        private bool _UpdateInternationalLicense()
        {
            return clsInternationalLicenseData.UpdateInternationalLicense(this.InternationalLicenseID, this.ApplicationID, this.DriverID, this.LocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
        }
        public static bool DeleteInternationalLicense(int InternationalLicenseID)
        {
            return clsInternationalLicenseData.DeleteInternationalLicense(InternationalLicenseID);
        }
        public static bool IsInternationalLicenseExistByInternationalLicenseID(int InternationalLicenseID)
        {
            return clsInternationalLicenseData.IsInternationalLicenseExistByInternationalLicenseID(InternationalLicenseID);
        }
        public static bool IsInternationalLicenseExistByDriverID(int DriverID)
        {
            return clsInternationalLicenseData.IsInternationalLicenseExistByDriverID(DriverID);
        }
        public static bool IsInternationalLicenseExistByLocalLicenseID(int IssuedUsingLocalLicenseID)
        {
            return clsInternationalLicenseData.IsInternationalLicenseExistByLocalLicenseID(IssuedUsingLocalLicenseID);
        }
        public static clsInternationalLicense FindByInternationalLicenseID(int InternationalLicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.MinValue;
            DateTime ExpirationDate = DateTime.MinValue;
            bool IsActive = false;
            int CreatedByUserID = -1;

            bool IsFound = clsInternationalLicenseData.GetInternationalLicenseByInternationalLicenseID(InternationalLicenseID, ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID);

            if (IsFound)
                return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            else
                return null;
        }
        public static clsInternationalLicense FindByApplicationID(int ApplicationID)
        {
            int InternationalLicenseID = -1;
            int DriverID = -1;
            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.MinValue;
            DateTime ExpirationDate = DateTime.MinValue;
            bool IsActive = false;
            int CreatedByUserID = -1;

            bool IsFound = clsInternationalLicenseData.GetInternationalLicenseByApplicationID(ref InternationalLicenseID, ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID);

            if (IsFound)
                return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            else
                return null;
        }
        public static clsInternationalLicense FindByDriverID(int DriverID)
        {
            int InternationalLicenseID = -1;
            int ApplicationID = -1;
            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.MinValue;
            DateTime ExpirationDate = DateTime.MinValue;
            bool IsActive = false;
            int CreatedByUserID = -1;

            bool IsFound = clsInternationalLicenseData.GetInternationalLicenseByDriverID(ref InternationalLicenseID, ref ApplicationID, DriverID, ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID);

            if (IsFound)
                return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            else
                return null;
        }
        public static clsInternationalLicense FindByIssuedUsingLocalLicenseID(int IssuedUsingLocalLicenseID)
        {
            int InternationalLicenseID = -1;
            int ApplicationID = -1;
            int DriverID = -1;
            DateTime IssueDate = DateTime.MinValue;
            DateTime ExpirationDate = DateTime.MinValue;
            bool IsActive = false;
            int CreatedByUserID = -1;

            bool IsFound = clsInternationalLicenseData.GetInternationalLicenseByLocalLicenseID(ref InternationalLicenseID, ref ApplicationID, ref DriverID, IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID);

            if (IsFound)
                return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            else
                return null;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInternationalLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateInternationalLicense();
            }
            return false;
        }
        public static DataTable GetInternationalLicenses()
        {
            return clsInternationalLicenseData.GetAllInternationalLicenses();
        }
        public static DataTable GetInternationalLicensesByDriverID(int driverID)
        {
            return clsInternationalLicenseData.GetInternationalLicensesByDriverID(driverID);
        }
        // other
        public bool DoesLicenseHaveAnActiveInternationalLicense()
        {
            return clsInternationalLicenseData.DoesLicenseHaveAnActiveInternationalLicense(LocalLicenseID);
        }
        public bool IssueInternationalLicense(int localLicenseID, int driverID, int createdByUserID)
        {
            // validations
            // user validation
            if (!clsUser.IsUserExists(createdByUserID))
            {
                clsShared.lastError = "Creating User not exists!";
                return false;
            }
            // local license validation
            if (clsLicense.IsLicenseExistByLicenseID(localLicenseID))
            {
                if (clsDetaining.IsLicenseDetained(localLicenseID))
                {
                    clsShared.lastError = "License Detained Can`t issue an interantional version of it!";
                    return false;
                }
            }
            else
            {
                clsShared.lastError = "Local License not exists!";
                return false;
            }
            // international license validation
            if (clsInternationalLicenseData.IsInternationalLicenseExistByLocalLicenseID(localLicenseID))
            {
                // international license validation
                if (clsInternationalLicenseData.IsInternationalLicenseActive(localLicenseID))
                {
                    clsShared.lastError = "Your Local license already have an active internatinal license!";
                    return false;
                }
            }

            // create application
            clsApplication application = new clsApplication();
            application.ApplicationTypeID = clsApplication.enApplicationType.NewInternationalLicense;
            int personID = -1;
            if (clsLicense.GetPersonIDByLicenseID(ref personID, localLicenseID))
                application.PersonID = personID;
            application.ApplicationDate = DateTime.Now;
            application.Status = clsApplication.enStatus.New;
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = (decimal)clsApplicationType.GetApplicationType((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationTypeFees;
            application.CreatedByUserID = createdByUserID;
            if (!application.Save()) return false;
            if (application.ApplicationID < 0) return false;
            this.ApplicationID = application.ApplicationID;

            // prepare object
            DriverID = driverID;
            LocalLicenseID = localLicenseID;
            ExpirationDate = DateTime.Now;
            ExpirationDate = DateTime.Now.AddYears(1);
            IsActive = true;
            CreatedByUserID = createdByUserID;
            // save
            if (Save()) return true;

            return false;
        }
    }
}
