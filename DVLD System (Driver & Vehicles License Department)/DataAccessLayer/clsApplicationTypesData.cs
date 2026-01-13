using System;
using System.Data;
using System.Data.SqlClient;
using Shared;

namespace DataAccessLayer.ApplicationTypes
{
    public class clsApplicationTypesData
    {
        public static DataTable GetApplicationTypes()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from ApplicationTypes";

            SqlCommand cmd = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
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
            return dt;
        }
        public static bool GetApplicationTypeByID(int AppTypeID, ref string TypeTitle, ref double TypeFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "select * from ApplicationTypes where ApplicationTypeID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", AppTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    TypeTitle = (string)reader["ApplicationTypeTitle"];
                    TypeFees = (double)(decimal)reader["ApplicationTypeFees"];
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
        public static bool UpdateApplicationTypeInfo(int AppTypeID, string TypeTitle, double TypeFees)
        {
            int rowsAffected = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"UPDATE [dbo].[ApplicationTypes]
                               SET [ApplicationTypeTitle] = @ApplicationTypeTitle
                                  ,[ApplicationTypeFees] = @ApplicationTypeFees
                             WHERE ApplicationTypeID = @ID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", AppTypeID);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", TypeTitle);
            command.Parameters.AddWithValue("@ApplicationTypeFees", TypeFees);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsShared.lastError = ex.Message;
            }
            finally { connection.Close(); }
            return (rowsAffected > 0);
        }
    }
}
