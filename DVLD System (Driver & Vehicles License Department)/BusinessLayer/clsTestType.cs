using System;
using System.Data;

namespace BusinessLayer
{
    public class clsTestType
    {
        public enum enTestTypes { VisionTest =1, WrittenTest, StreetTest}
        public enTestTypes TestTypeID;
        public string Title;
        public string Description;
        public decimal Fees;

        public clsTestType()
        {
            TestTypeID = enTestTypes.VisionTest;
            Title = string.Empty;
            Description = string.Empty;
            Fees = -1;
        }
        private clsTestType(enTestTypes testTypeID, string title, string description, decimal fees)
        {
            TestTypeID = testTypeID;
            Title = title;
            Description = description;
            Fees = fees;
        }
        public static DataTable AllTestTypes()
        {
            return DataAccessLayer.clsTestTypsData.GetTestTypes();
        }
        public static clsTestType GetTestType(int TypeID)
        {
            string title = "";
            string description = "";
            decimal fees = -1;
            if (DataAccessLayer.clsTestTypsData.GetTestType(TypeID, ref title, ref description, ref fees))
            {
                return new clsTestType((enTestTypes)TypeID, title, description, fees);
            }
            return null;
        }
        public bool UpdateTestType()
        {
            return DataAccessLayer.clsTestTypsData.UpdateTestTypeInfo((int)TestTypeID, Title, Description, Fees);
        }
    }
}
