using System;
using System.Data;

namespace BusinessLayer
{
    public class clsApplicationType
    {
        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public double ApplicationTypeFees { get; set; }

        public clsApplicationType()
        {
            ApplicationTypeID = -1;
            ApplicationTypeTitle = "";
            ApplicationTypeFees = 0;
        }
        public clsApplicationType(int AppTypeID, string TypeTitle, double TypeFees)
        {
            ApplicationTypeID = AppTypeID;
            ApplicationTypeTitle = TypeTitle;
            ApplicationTypeFees = TypeFees;
        }
        public static DataTable AllApplicationTypes()
        {
            return DataAccessLayer.ApplicationTypes.clsApplicationTypesData.GetApplicationTypes();
        }
        public static clsApplicationType GetApplicationType(int AppTypeID)
        {
            string TypeTitle = "";
            double TypeFees = -1;
            if (DataAccessLayer.ApplicationTypes.clsApplicationTypesData.GetApplicationTypeByID(AppTypeID, ref TypeTitle, ref TypeFees))
            {
                return new clsApplicationType(AppTypeID, TypeTitle, TypeFees);
            }
            return null;
        }
        public bool UpdateApplicationType()
        {
            return DataAccessLayer.ApplicationTypes.clsApplicationTypesData.UpdateApplicationTypeInfo(ApplicationTypeID, ApplicationTypeTitle, ApplicationTypeFees);
        }
    }
}
