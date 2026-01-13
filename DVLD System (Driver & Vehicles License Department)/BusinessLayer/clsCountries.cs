using System;
using System.Data;

namespace BusinessLayer
{
    public class clsCountries
    {
        public static bool GetCountries(ref DataTable dt)
        {
            return DataAccessLayer.clsCountriesData.GetAllCountries(ref dt);
        }
        public static string GetCountryName(int CountryID)
        {
            return DataAccessLayer.clsCountriesData.GetCountryNameByID(CountryID);
        }
        public static int GetCountryID(string CountryName)
        {
            return DataAccessLayer.clsCountriesData.GetCountryIDByName(CountryName);
        }
    }
}
