using System;
using System.Data.SqlClient;
using System.Data;
using Shared;

namespace DataAccessLayer
{
    public class clsUserData
    {
        public static bool IsUserIDExists(int UserID)
        {
            bool exists = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "SELECT found=1 FROM Users where UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    exists = true;
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
            return exists;
        }
        public static bool IsUserActive(string UserName)
        {
            bool active = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "SELECT IsActive FROM Users where UserName = @UserName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    active = (bool) reader[0];
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
            return active;
        }
        public static bool IsPersonIDExistsInUsersTable(ref int userId, int personId)
        {
            bool exists = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "SELECT UserID FROM Users where PersonID = @personId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personId", personId);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    userId = (int)reader["UserID"];
                    exists = true;
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
            return exists;
        }
        public static bool CheckUserNamePassword(string UserName, string Password)
        {
            bool success = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT found=1 FROM Users where UserName = @UserName and Password = @Password";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    success = true;
                }
            }
            catch (Exception ex)
            {
                clsShared.lastError = ex.Message;
            }
            finally { connection.Close(); }

            return success;
        }
        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select * from ListUsers";

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
        public static bool AddUser(ref int userId, int PersonID, string UserName, string Password, bool IsActive)
        {
            bool success = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"INSERT INTO [dbo].[Users] ([PersonID], [UserName], [Password], [IsActive])
                            VALUES (@PersonID, @UserName, @Password, @IsActive);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                connection.Open();
                userId = (int)command.ExecuteNonQuery();
                success = true;

            }
            catch (Exception ex)
            {
                clsShared.lastError = ex.Message;
            }
            finally { connection.Close(); }

            return success;
        }
        public static bool UpdateUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            int rowsAffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"UPDATE Users
                               SET [PersonID] = @PersonID
                                  ,[UserName] = @UserName
                                  ,[Password] = @Password
                                  ,[IsActive] = @IsActive
                               WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                connection.Open();
                rowsAffected = (int)command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsShared.lastError = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return rowsAffected > 0;
        }
        public static bool UpdatePassword(int UserID, string NewPassword)
        {
            int rowsAffected = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"UPDATE Users
                               SET [Password] = @NewPassword
                               WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@NewPassword", NewPassword);

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

            return rowsAffected > 0;
        }
        public static bool DeleteUser(int UserID)
        {
            int rowsAffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"DELETE FROM Users WHERE UserID = @UserID";


            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                rowsAffected = (int)command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsShared.lastError = ex.Message;
            }
            finally { connection.Close(); }
            return rowsAffected > 0;
        }
        public static bool GetUserInfoByUserName(ref int UserID, ref int PersonID, string UserName, ref string Password, ref bool IsActive)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from Users where UserName = @UserName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    UserID = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];
                }
                else
                {
                    isFound = false;
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
        public static bool GetUserInfoByUserID(int UserID, ref int PersonID, ref string UserName, ref string Password, ref bool IsActive)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from Users where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];
                }
                else
                {
                    isFound = false;
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
