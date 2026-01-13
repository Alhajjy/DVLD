using System;
using System.Data;
using DataAccessLayer;
using Shared;

namespace BusinessLayer
{
    public class clsDriver
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int DriverID { set; get; }
        public int PersonID { set; get; }
        public int CreatedByUserID { set; get; }
        public DateTime CreatedDate { set; get; }
        public clsPerson PersonInfo = new clsPerson();

        public clsDriver()
        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.MinValue;
            Mode = enMode.AddNew;
        }
        private clsDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
            PersonInfo = clsPerson.FindPersonWithID(this.PersonID);
            Mode = enMode.Update;
        }
        private bool _AddNewDriver()
        {
            if (IsPersonIsADriver(this.PersonID))
            {
                clsShared.lastError = "Person is a Driver already";
                return true;
            }
            this.DriverID = (int)clsDriverData.AddNewDriver(this.PersonID, this.CreatedByUserID, this.CreatedDate);
            return (this.DriverID != -1);
        }
        private bool _UpdateDriver()
        {
            return clsDriverData.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID, this.CreatedDate);
        }
        public static bool DeleteDriver(int DriverID)
        {
            return clsDriverData.DeleteDriver(DriverID);
        }
        public static bool IsDriverExistByDriverID(int DriverID)
        {
            return clsDriverData.IsDriverExist(DriverID);
        }
        public static bool IsPersonIsADriver(int personID)
        {
            return clsDriverData.IsDriverExistByPersonID(personID);
        }
        public static clsDriver FindByDriverID(int DriverID)
        {
            int PersonID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.MinValue;

            bool IsFound = clsDriverData.GetDriverByID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate);

            if (IsFound)
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;
        }
        public static bool GetDriverIDByPersonID(ref int driverID, int PersonID)
        {
            return clsDriverData.GetDriverIDByPersonID(ref driverID, PersonID);
        }
        public static bool GetDriverIDByLDLAppID(ref int driverID, int LDLAppID)
        {
            return clsDriverData.GetDriverIDByLocalDrivingLicenseApplicationID(ref driverID, LDLAppID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateDriver();
            }
            return false;
        }
        public static DataTable GetDrivers()
        {
            return clsDriverData.GetAllDrivers();
        }
    }
}
