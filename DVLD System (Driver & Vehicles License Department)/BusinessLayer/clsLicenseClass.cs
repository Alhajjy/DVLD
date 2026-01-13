using System;
using System.Data;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsLicenseClass
    {
        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public int MinimumAllowedAge { get; set; }
        public int DefaultValidityLength { get; set; }
        public decimal ClassFees { get; set; }
        public static DataTable GetLicenseClasses()
        {
            return clsLicenseClassData.GetAllClasses();
        }

        public clsLicenseClass()
        {
            LicenseClassID = -1;
            ClassName = "";
            ClassDescription = "";
            MinimumAllowedAge = -1;
            DefaultValidityLength = -1;
            ClassFees = 0.0m;
        }
        public clsLicenseClass(int licenseClassID, string className, string classDescription, int minimumAllowedAge, int defaultValidityLength, decimal classFees)
        {
            LicenseClassID = licenseClassID;
            ClassName = className;
            ClassDescription = classDescription;
            MinimumAllowedAge = minimumAllowedAge;
            DefaultValidityLength = defaultValidityLength;
            ClassFees = classFees;
        }
        public static clsLicenseClass GetLicenseClassByID(int licenseClassID)
        {
            int id = licenseClassID;
            string className = "";
            string classDescription = "";
            int minimumAllowedAge = 0;
            int defaultValidityLength = 0;
            decimal classFees = 0.0m;

            if (clsLicenseClassData.GetLicenseClassByID(licenseClassID, ref className, ref classDescription, ref minimumAllowedAge, ref defaultValidityLength, ref classFees))
            {
                return new clsLicenseClass(id, className, classDescription, minimumAllowedAge, defaultValidityLength, classFees);
            }
            return null;
        }
    }
}
