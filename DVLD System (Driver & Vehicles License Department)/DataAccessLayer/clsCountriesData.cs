using System;
using System.Data;
using System.Data.SqlClient;
using Shared;

namespace DataAccessLayer
{
    public class clsCountriesData
    {
        public static bool GetAllCountries(ref DataTable dt)
        {
            bool done = false ;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from Countries";

            SqlCommand cmd = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                    done = true;
                }
            }
            catch (Exception ex)
            {
                connection.Close();
                clsShared.lastError = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return done;
        }

        public static string GetCountryNameByID(int CountryID)
        {
            string CountryName = "";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from Countries where CountryID = @ID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", CountryID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    CountryName = (string)reader["CountryName"];
                }
                ;
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
            return CountryName;
        }

        public static int GetCountryIDByName(string CountryName)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from Countries where CountryName = @CountryName;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ID = (int)reader["CountryID"];
                }
                ;
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
            return ID;
        }
    }
}
