using System;
using Shared;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class clsTestTypsData
    {
        public static DataTable GetTestTypes()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from TestTypes";

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
        public static bool GetTestType(int TestTypeID, ref string Title, ref string Description, ref decimal Fees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "select * from TestTypes where TestTypeID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("ID", TestTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Title = (string)reader[1];
                    Description = (string)reader[2];
                    Fees = (decimal)reader[3];
                    isFound = true;
                }
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
        public static bool UpdateTestTypeInfo(int TestTypeID, string Title, string Description, decimal Fees)
        {
            int rowsAffected = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"UPDATE [dbo].[TestTypes]
                               SET [Title] = @Title
                                  ,[Description] = @Description
                                  ,[Fees] = @Fees
                             WHERE TestTypeID = @ID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", TestTypeID);
            command.Parameters.AddWithValue("@Title", Title);
            command.Parameters.AddWithValue("@Description", Description);
            command.Parameters.AddWithValue("@Fees", Fees);

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
