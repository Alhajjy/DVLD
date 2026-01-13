using System;
using System.Data;
using DataAccessLayer;
using Shared;

namespace BusinessLayer
{
    public class clsDetaining
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int DetainingID { set; get; }
        public int LicenseID { set; get; }
        clsLicense LicenseInfo = new clsLicense();
        public DateTime DetainDate { set; get; }
        public decimal FineFees { set; get; }
        public int CreatedByUserID { set; get; }
        public bool IsReleased { set; get; }
        public DateTime ReleaseDate { set; get; }
        public int ReleasedByUserID { set; get; }
        public int ReleaseApplicationID { set; get; }

        public clsDetaining()
        {
            this.DetainingID = -1;
            this.LicenseID = -1;
            this.DetainDate = DateTime.MinValue;
            this.FineFees = -1;
            this.CreatedByUserID = -1;
            this.IsReleased = false;
            this.ReleaseDate = DateTime.MinValue;
            this.ReleasedByUserID = -1;
            this.ReleaseApplicationID = -1;
            Mode = enMode.AddNew;
        }
        private clsDetaining(int DetainingID, int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainingID = DetainingID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;
            LicenseInfo = clsLicense.FindByLicenseID(this.LicenseID);
            Mode = enMode.Update;
        }
        private bool _AddNewDetaining()
        {
            this.DetainingID = (int)clsDetainingData.AddNewDetaining(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);
            return (this.DetainingID != -1);
        }
        private bool _UpdateDetaining()
        {
            return clsDetainingData.UpdateDetaining(this.DetainingID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);
        }
        public static bool DeleteDetaining(int DetainingID)
        {
            return clsDetainingData.DeleteDetaining(DetainingID);
        }
        public static bool IsDetainingExist(int DetainingID)
        {
            return clsDetainingData.IsDetainingExist(DetainingID);
        }
        public static bool IsDetainingExistByLicenseID(int LicenseID)
        {
            return clsDetainingData.IsDetainingExistByLicenseID(LicenseID);
        }
        public static clsDetaining FindByDetainingID(int DetainingID)
        {
            int LicenseID = -1;
            DateTime DetainDate = DateTime.MinValue;
            decimal FineFees = -1;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime ReleaseDate = DateTime.MinValue;
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;

            bool IsFound = clsDetainingData.GetDetainingByDetainingID(DetainingID, ref LicenseID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID);

            if (IsFound)
                return new clsDetaining(DetainingID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            else
                return null;
        }
        public static clsDetaining FindByLicenseID(int LicenseID)
        {
            int DetainingID = -1;
            DateTime DetainDate = DateTime.MinValue;
            decimal FineFees = -1;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime ReleaseDate = DateTime.MinValue;
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;

            bool IsFound = clsDetainingData.GetDetainingByLicenseID(ref DetainingID, LicenseID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID);

            if (IsFound)
                return new clsDetaining(DetainingID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            else
                return null;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetaining())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateDetaining();
            }
            return false;
        }
        public static DataTable GetAllDetainings()
        {
            return clsDetainingData.GetAllDetainings();
        }
        // other
        public static bool IsLicenseDetained(int licenseID)
        {
            return clsDetainingData.IsLicenseDetained(licenseID);
        }
        public static bool IsLicenseReleased(int licenseID)
        {
            return clsDetainingData.IsLicenseReleased(licenseID);
        }
        public bool DetainLicense()
        {
            // checks
            if (FineFees == -1)
            {
                clsShared.lastError = "Enter Fine Fees..";
                return false;
            }
            if (clsDetainingData.IsLicenseDetained(LicenseID))
            {
                clsShared.lastError = "License allready Detained!";
                return false;
            }
            if (!clsLicense.IsLicenseActive(LicenseID))
            {
                clsShared.lastError = "License is not active!, cant detain!";
                return false;
            }

            // every thing is ok, detain..
            this.DetainDate = DateTime.Now;
            return Save();
        }
        public bool ReleaseDetainedLicense(int releasedByUserID)
        {
            // checks
            if (IsDetainingExist(this.DetainingID))
            {
                if (IsLicenseReleased(this.LicenseID))
                {
                    clsShared.lastError = "License already released!";
                    return false;
                }
            }
            else
            {
                clsShared.lastError = "Can`t found the detaining!";
                return false;
            }

            // create release application
            clsApplication application = new clsApplication();
            application.ApplicationTypeID = clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            application.PersonID = LicenseInfo.ApplicationInfo.PersonID;
            application.ApplicationDate = DateTime.Now;
            application.Status = clsApplication.enStatus.New;
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = (decimal)LicenseInfo.ApplicationInfo.ApplicationType.ApplicationTypeFees;
            application.CreatedByUserID = CreatedByUserID;
            if (!application.Save()) return false;
            if (application.ApplicationID < 0) return false;
            this.ReleaseApplicationID = application.ApplicationID;


            // every thing is ok, release..
            ReleaseDate = DateTime.Now;
            ReleasedByUserID = releasedByUserID;
            if (clsDetainingData.ReleaseDetainedLicense(DetainingID, ReleaseDate, releasedByUserID, application.ApplicationID))
                return true;
            return false;
        }
    }
}
