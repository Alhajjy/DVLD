using System;
using System.ComponentModel;
using System.Data;
using DataAccessLayer;
using Shared;

namespace BusinessLayer
{
    public class clsLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 };
        public int LicenseID { set; get; }
        public int ApplicationID { set; get; }
        public int DriverID { set; get; }
        public int LicenseClassID { set; get; }
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public string Notes { set; get; }
        public decimal PaidFees { set; get; }
        public bool IsActive { set; get; }
        public byte IssueReason { set; get; }
        public int CreatedByUserID { set; get; }
        public clsApplication ApplicationInfo = new clsApplication();
        public clsDriver DriverInfo = new clsDriver();
        public clsLicenseClass LicenseClassInfo = new clsLicenseClass();

        public clsLicense()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClassID = -1;
            this.IssueDate = DateTime.MinValue;
            this.ExpirationDate = DateTime.MinValue;
            this.Notes = "";
            this.PaidFees = -1;
            this.IsActive = false;
            this.IssueReason = 0;
            this.CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }
        private clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClassID = LicenseClassID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;
            ApplicationInfo = clsApplication.FindApplicationWithID(ApplicationID);
            DriverInfo = clsDriver.FindByDriverID(this.DriverID);
            LicenseClassInfo = clsLicenseClass.GetLicenseClassByID(this.LicenseClassID);
            Mode = enMode.Update;
        }
        private bool _AddNewLicense()
        {
            ApplicationInfo = clsApplication.FindApplicationWithID(ApplicationID);
            int driverID = -1;
            if (!IsPersonAvailableForLicense(ApplicationInfo.PersonID, LicenseClassID))
            {
                clsShared.lastError = "Person already has a license of this class.";
                return false;
            }
            if (clsDriver.IsPersonIsADriver(ApplicationInfo.PersonID))
            {
                if (!clsDriverData.GetDriverIDByPersonID(ref driverID, ApplicationInfo.PersonID)) return false;
                else this.DriverID = driverID;
            }
            else
            {
                if (!_AddDriver())
                {
                    clsShared.lastError = "Error making person a driver!";
                    return false;
                }

            }

            this.LicenseID = (int)clsLicenseData.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClassID, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID);
            if (this.LicenseID != -1)
            {
                return true;
            }
            return false;
        }
        private bool _UpdateLicense()
        {
            return clsLicenseData.UpdateLicense(this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClassID, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID);
        }
        public static bool DeleteLicense(int LicenseID)
        {
            return clsLicenseData.DeleteLicense(LicenseID);
        }
        public static bool IsLicenseExistByLicenseID(int LicenseID)
        {
            return clsLicenseData.IsLicenseExist(LicenseID);
        }
        public static clsLicense FindByLicenseID(int LicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int LicenseClassID = -1;
            DateTime IssueDate = DateTime.MinValue;
            DateTime ExpirationDate = DateTime.MinValue;
            string Notes = "";
            decimal PaidFees = -1;
            bool IsActive = false;
            byte IssueReason = 0;
            int CreatedByUserID = -1;

            bool IsFound = clsLicenseData.GetLicenseByLicenseID(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClassID, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID);

            if (IsFound)
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
            else
                return null;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateLicense();
            }
            return false;
        }
        public static DataTable GetLicenses()
        {
            return clsLicenseData.GetAllLicenses();
        }
        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicenseData.GetAllLicensesByDriverID(DriverID);
        }
        // other
        public static string GetIssueReasonAsString(enIssueReason issueReason)
        {
            switch (issueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew ";
                case enIssueReason.DamagedReplacement:
                    return "Damage Replacement";
                case enIssueReason.LostReplacement:
                    return "Lost Replacement";
                default:
                    return "Unknown";
            }
        }
        public static bool IsPersonAvailableForLicense(int personID, int licenseClassID)
        {
            if (clsLicenseData.DoesPersonHaveAnActiveLicenseByLicenseClassID(personID, licenseClassID))
            {
                return false;
            }
            return true;
        }
        public static bool IsApplicationIssuedTheLicense(int applicationID)
        {
            return clsLicenseData.IsApplicationIssuedTheLicense(applicationID);
        }
        public static bool GetPersonIDByLicenseID(ref int personID, int licenseID)
        {
            return clsLicenseData.GetPersonIDByLicenseID(ref personID, licenseID);
        }
        public static bool GetLicenseIDByLocalDrivingLicenseAppID(ref int LicenseID, int LocalDrivingLicenseApplicationID)
        {
            return clsLicenseData.GetLicenseIDByLocalDrivingLicenseAppID(ref LicenseID, LocalDrivingLicenseApplicationID);
        }
        public static bool GetLastLicenseIDByDriverID(ref int licenseID, int driverID, int licenseClass)
        {
            return clsLicenseData.GetLastLicenseIDByDriverID(ref licenseID, driverID, licenseClass);
        }
        public static clsLicense GetLastLicenseByDriverIDWithLicenseClass(int DriverID, int LicenseClassID)
        {
            int LicenseID = -1;
            int ApplicationID = -1;
            DateTime IssueDate = DateTime.MinValue;
            DateTime ExpirationDate = DateTime.MinValue;
            string Notes = "";
            decimal PaidFees = -1;
            bool IsActive = false;
            byte IssueReason = 0;
            int CreatedByUserID = -1;

            bool IsFound = clsLicenseData.GetLastLicenseByDriverIDWithLicenseClass(ref LicenseID, ref ApplicationID, DriverID, LicenseClassID, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID);

            if (IsFound)
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
            else
                return null;
        }
        private bool _AddDriver()
        {
            clsDriver driver = new clsDriver();
            driver.PersonID = ApplicationInfo.PersonID;
            driver.CreatedByUserID = this.CreatedByUserID;
            driver.CreatedDate = DateTime.Now;
            if (driver.Save())
            {
                this.DriverID = driver.DriverID;
                return true;
            }
            else
            {
                return false;
            }
        }
        private static bool _DeactivateLicense(int licenseID)
        {
            return clsLicenseData.DeactivateLicense(licenseID);
        }
        public bool IsDateExpired()
        {
            if (clsLicenseData.IsDateExpired(this.LicenseID))
            {
                if (_DeactivateLicense(this.LicenseID))
                    return true;
                return false;
            }
            return false;
        }
        public bool IsLicenseActive()
        {
            return clsLicenseData.IsLicenseActive(this.LicenseID);
        }
        public static bool IsLicenseActive(int licenseID)
        {
            return clsLicenseData.IsLicenseActive(licenseID);
        }
        public static bool IsDateExpired(int licenseID)
        {
            return clsLicenseData.IsDateExpired(licenseID);
        }
        public bool RenewLicense(ref int newlicenseID, clsLicense NewLicense)
        {
            int OldlicenseID = -1;
            if (GetLastLicenseIDByDriverID(ref OldlicenseID, DriverID, LicenseClassID))
            {
                this.LicenseID = OldlicenseID;
            }
            else { return false; }


            if (IsDateExpired())
            {
                NewLicense.Mode = enMode.AddNew;
                NewLicense.LicenseID = -1;
                NewLicense.IssueDate = DateTime.Now;
                NewLicense.ExpirationDate = DateTime.Now.AddYears(NewLicense.LicenseClassInfo.DefaultValidityLength);
                NewLicense.IsActive = true;
                NewLicense.IssueReason = (int)enIssueReason.Renew;

                clsApplication application = new clsApplication();
                application.ApplicationTypeID = clsApplication.enApplicationType.RenewDrivingLicense;
                application.PersonID = NewLicense.ApplicationInfo.PersonID;
                application.ApplicationDate = DateTime.Now;
                application.Status = clsApplication.enStatus.New;
                application.LastStatusDate = DateTime.Now;
                application.PaidFees = (decimal)NewLicense.ApplicationInfo.ApplicationType.ApplicationTypeFees;
                application.CreatedByUserID = NewLicense.CreatedByUserID;

                if (application.Save())
                {
                    NewLicense.ApplicationID = application.ApplicationID;
                    if (NewLicense.Save())
                    {
                        _DeactivateLicense(OldlicenseID);
                        return clsLicenseData.GetLastLicenseIDByDriverID(ref newlicenseID, NewLicense.DriverID, NewLicense.LicenseClassID);
                    }
                    else
                    {
                        clsApplication.DeleteApplication(application.ApplicationID);
                        clsShared.lastError = "Error Creating New License.";
                        return false;
                    }
                }
                else
                {
                    clsShared.lastError = "Error Creating Renew Application.";
                    return false;
                }
            }
            else
            {
                clsShared.lastError = "License does`nt expired yet.";
                return false;
            }
        }
        public bool ReplaceLostOrDamagedLicense(int currentUserID, enIssueReason issueReason)
        {
            // if license not active break operation, and if active deactivate it.
            if (!this.IsLicenseActive())
            {
                clsShared.lastError = "License is Not Active, you can not replace it!";
                return false;
            }

            // Reset and prepare for new data
            int OldLicenseID = LicenseID;
            Mode = enMode.AddNew;
            this.LicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            IssueReason = (byte)issueReason;

            // create application
            clsApplication application = new clsApplication();
            application.ApplicationTypeID = clsApplication.enApplicationType.ReplaceDamagedDrivingLicense;
            application.PersonID = ApplicationInfo.PersonID;
            application.ApplicationDate = DateTime.Now;
            application.Status = clsApplication.enStatus.New;
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = (decimal)ApplicationInfo.ApplicationType.ApplicationTypeFees;
            application.CreatedByUserID = CreatedByUserID;
            if (!application.Save()) return false;
            if (application.ApplicationID < 0) return false;
            this.ApplicationID = application.ApplicationID;

            // issue replaced license
            if (!this.Save())
            {
                clsApplication.DeleteApplication(application.ApplicationID);
            }

            if (!_DeactivateLicense(OldLicenseID)) clsShared.lastError = "Old license Cant deactivated";
            return true;
        }
        // related to other classes
        public bool IssueInternationalDrivingLicense(ref int internationalLicenseID, int createdByUserID)
        {
            clsInternationalLicense internationalLicense = new clsInternationalLicense();
            internationalLicense.LocalLicenseID = this.LicenseID;
            internationalLicense.DriverID = DriverID;
            internationalLicense.IssueDate = DateTime.Now;
            bool result = internationalLicense.IssueInternationalLicense(LicenseID, DriverID, createdByUserID);
            internationalLicenseID = internationalLicense.InternationalLicenseID;
            return result;
        }
    }
}