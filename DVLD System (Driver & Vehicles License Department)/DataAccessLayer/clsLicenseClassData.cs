using System;
using System.Data;
using System.Data.SqlClient;
using Shared;

namespace DataAccessLayer
{
    public class clsLicenseClassData
    {
        public static DataTable GetAllClasses()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM LicenseClasses";

            SqlCommand cmd = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
            }
            catch (Exception ex)
            {
                clsShared.lastError = ex.Message;
            }
            finally { connection.Close(); }
            return dt;
        }
        public static bool GetLicenseClassByID(int LicenseClassID, ref string ClassName, ref string ClassDescription, ref int MinimumAllowedAge, ref int DefaultValidityLength, ref decimal ClassFees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"SELECT LicenseClasses.* FROM LicenseClasses
                                where LicenseClassID = @LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ClassName = (string)reader["ClassName"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = (int)reader["MinimumAllowedAge"];
                    DefaultValidityLength = (int)reader["DefaultValidityLength"];
                    ClassFees = (decimal)reader["ClassFees"];

                    isFound = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                clsShared.lastError = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
    }
}
