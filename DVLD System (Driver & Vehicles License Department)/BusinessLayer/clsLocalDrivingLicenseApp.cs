using System;
using System.Data;
using DataAccessLayer;
using Shared;

namespace BusinessLayer
{
    public class clsLocalDrivingLicenseApp : clsApplication
    {
        enMode _Mode = enMode.Add;
        public int LocalLicenseAppID { get; set; }
        public int LicenseClassID { get; set; }
        public clsLicenseClass LicenseClassInfo;
        public string FullName
        {
            get
            {
                return base.Person.FullName;
            }
        }

        public clsLocalDrivingLicenseApp()
        {
            LocalLicenseAppID = -1;
            LicenseClassID = -1;
            _Mode = enMode.Add;
        }
        public clsLocalDrivingLicenseApp(int localLicenseAppID, int licenseClassID,
            int applicationID, int appApplicationTypeID, int appPersonID, DateTime appApplicationDate,
            enStatus appStatus, DateTime appLastStatusDate, decimal appPaidFees, int appCreatedByUserID)
        {
            LocalLicenseAppID = localLicenseAppID;
            LicenseClassID = licenseClassID;
            ApplicationID = applicationID;
            PersonID = appPersonID;
            ApplicationDate = appApplicationDate;
            Status = appStatus;
            LastStatusDate = appLastStatusDate;
            PaidFees = appPaidFees;
            CreatedByUserID = appCreatedByUserID;

            Person = clsPerson.FindPersonWithID(PersonID);
            ApplicationType = clsApplicationType.GetApplicationType((int)ApplicationTypeID);
            LicenseClassInfo = clsLicenseClass.GetLicenseClassByID(licenseClassID);
            _Mode = enMode.Update;
        }
        public static DataTable GetAllLocalLicenseApps()
        {
            return clsLocalDrivingLicenseAppData.GetLocalDrivingLicenseApps();
        }
        public static DataTable GetLDLAppsView()
        {
            return clsLocalDrivingLicenseAppData.GetLocalDrivingLicenseAppsView();
        }
        public static clsLocalDrivingLicenseApp FindLocalLicenseAppWithID(int localLicenseAppID)
        {
            int applicationID = -1;
            int licenseClassID = -1;
            if (clsLocalDrivingLicenseAppData.GetLocalDrivingLicenseAppByID(localLicenseAppID, ref applicationID, ref licenseClassID))
            {
                if (IsApplicationExists(applicationID))
                {
                    clsApplication app = FindApplicationWithID(applicationID);
                    if (app != null)
                    {
                        return new clsLocalDrivingLicenseApp(localLicenseAppID, licenseClassID, app.ApplicationID, (int)app.ApplicationTypeID, app.PersonID,
                            app.ApplicationDate, app.Status, app.LastStatusDate, app.PaidFees, app.CreatedByUserID);
                    }
                }
            }
            return null;
        }
        public static bool DeleteLocalDrivingLicenseApp(int localLicenseAppID)
        {
            if (IsAppHaveAtLeastOneTestAppointment(localLicenseAppID))
            {
                clsShared.lastError = "Cannot delete the application because it has a data connected to it.";
                return false;
            }
            int baseAppID = FindLocalLicenseAppWithID(localLicenseAppID).ApplicationID;
            bool mainResult = clsLocalDrivingLicenseAppData.DeleteLocalDrivingLicenseApp(localLicenseAppID);
            bool baseResult = DeleteApplication(baseAppID);
            return (baseResult && mainResult);
        }
        private bool _AddLocalDrivingLicenseApp()
        {
            int localLicenseAppID = -1;
            if (clsLocalDrivingLicenseAppData.InsertLocalDrivingLicenseApp(ref localLicenseAppID, ApplicationID, LicenseClassID))
            {
                LocalLicenseAppID = localLicenseAppID;
                return true;
            }
            return false;
        }
        private bool _UpdateLocalDrivingLicenseApp()
        {
            if (IsAppHaveAtLeastOneTestAppointment(LocalLicenseAppID))
            {
                clsShared.lastError = "Cannot Update the application because it has a data connected to it.";
                return false;
            }
            return clsLocalDrivingLicenseAppData.UpdateLocalDrivingLicenseApp(LocalLicenseAppID, ApplicationID, LicenseClassID);
        }
        public bool Save()
        {
            bool baseAppSuccess = false;
            bool success = false;

            if (_Mode == enMode.Add && !DoesPersonAvailableToOpenApplication())
            {
                clsShared.lastError = $"{PersonID}: {Person.FullName}\nPerson Not Allowed to Open New Application";
                return false;
            }
            // base info save
            base.Mode = _Mode;
            baseAppSuccess = base.Save();
            if (!baseAppSuccess) return false;

            // current info save
            if (_Mode == enMode.Add)
            {
                success = _AddLocalDrivingLicenseApp();
                if (success)
                    _Mode = enMode.Update;
            }
            else
            {
                success = _UpdateLocalDrivingLicenseApp();
            }

            // local... app save failure case (in add mode)
            if (_Mode == enMode.Add && !success)
            {
                clsApplicationData.DeleteApplication(ApplicationID);
                return false;
            }
            return (success && baseAppSuccess);
        }
        public static bool IsLocalDrivingLicenseAppExists(int LDLAppID)
        {
            return clsLocalDrivingLicenseAppData.IsLocalDrivingLicenseAppExists(LDLAppID);
        }
        public bool DoesPersonAvailableToOpenApplication()
        {
            return !clsLocalDrivingLicenseAppData.DoesPersonHaveActiveApplicationWithSameLicenseClass(PersonID, LicenseClassID);
        }
        public static bool DoesPersonAvailableToOpenApplication(int personID, int licenseClassID)
        {
            return !clsLocalDrivingLicenseAppData.DoesPersonHaveActiveApplicationWithSameLicenseClass(personID, licenseClassID);
        }
        public static bool IsAppHaveAtLeastOneTestAppointment(int localLicenseAppID)
        {
            return clsLocalDrivingLicenseAppData.IsAppHaveAtLeastTestAppointment(localLicenseAppID);
        }
        public static bool IsAppCanBeCancelled(int localLicenseAppID)
        {
            return clsLocalDrivingLicenseAppData.IsAppCanBeCancelled(localLicenseAppID);
        }
        public static bool IsAppCancelled(int localLicenseAppID)
        {
            return clsLocalDrivingLicenseAppData.IsAppCancelled(localLicenseAppID);
        }
        public static bool DoesPassTestType(int localLicenseAppID, int testTypeID)
        {
            return clsLocalDrivingLicenseAppData.DoesPassTestType(localLicenseAppID, testTypeID);
        }
        public static bool GetApplicantPersonIDByLDLAppID(int LDLAppID, ref int personID)
        {
            return clsLocalDrivingLicenseAppData.GetApplicantIdByLDLAppID(LDLAppID, ref personID);
        }
    }
}