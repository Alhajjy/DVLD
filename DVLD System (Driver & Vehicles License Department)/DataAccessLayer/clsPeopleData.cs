using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;
using Shared;

namespace DataAccessLayer
{
    public class clsPeopleData
    {
        // Select All People
        public static DataTable GetPeople()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"SELECT        People.PersonID, 
                                People.NationalNo, 
                                People.FirstName, 
                                People.SecondName, 
                                People.ThirdName, 
                                People.LastName, 
                                People.BirthDate, 
                                CASE 
                                    WHEN People.Gender = 1 THEN 'Male' 
                                    ELSE 'Female' 
                                END AS Gender,
                                People.Address, 
                                People.Phone, 
                                People.Email, 
                                Countries.CountryName, 
                                People.ImagePath
                 FROM           People 
                 INNER JOIN     Countries ON People.NationalityID = Countries.CountryID";

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

        // Crud Operations
        public static bool GetPersonByID(int PersonID, ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName,
                            ref string LastName,ref  DateTime BirthDate, ref bool Gender,
                             ref string Address, ref string Phone, ref string Email,
                            ref int NationalityID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from People where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    NationalNo = (string)reader["NationalNo"]; // National No

                    FirstName = (string)reader["FirstName"]; // First Name

                    if (reader["SecondName"] != DBNull.Value) SecondName = (string)reader["SecondName"]; // Second Name
                    else SecondName = "";

                    if (reader["ThirdName"] != DBNull.Value) ThirdName = (string)reader["ThirdName"]; // Third Name
                    else ThirdName = "";

                    LastName = (string)reader["LastName"]; // Last Name

                    BirthDate = (DateTime)reader["BirthDate"]; // Birth Date

                    Gender = (bool)reader["Gender"]; // Gender

                    if (reader["Address"] != DBNull.Value) Address = (string)reader["Address"]; // Address
                    else Address = "";

                    if (reader["Phone"] != DBNull.Value) Phone = (string)reader["Phone"]; // Phone
                    else Phone = "";

                    if (reader["Email"] != DBNull.Value) Email = (string)reader["Email"]; // Email
                    else Email = "";

                    NationalityID = (int)reader["NationalityID"]; // Nationality ID

                    if (reader["ImagePath"] != DBNull.Value) ImagePath = (string)reader["ImagePath"]; // Image Path
                    else ImagePath = "";
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
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static bool GetPersonByNationalNo(ref int PersonID, string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName,
                            ref string LastName, ref DateTime BirthDate, ref bool Gender,
                             ref string Address, ref string Phone, ref string Email,
                            ref int NationalityID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from People where NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    PersonID = (int)reader["PersonID"]; // Person ID

                    FirstName = (string)reader["FirstName"]; // First Name

                    if (reader["SecondName"] != DBNull.Value) SecondName = (string)reader["SecondName"]; // Second Name
                    else SecondName = "";

                    if (reader["ThirdName"] != DBNull.Value) ThirdName = (string)reader["ThirdName"]; // Third Name
                    else ThirdName = "";

                    LastName = (string)reader["LastName"]; // Last Name

                    BirthDate = (DateTime)reader["BirthDate"]; // Birth Date

                    Gender = (bool)reader["Gender"]; // Gender

                    if (reader["Address"] != DBNull.Value) Address = (string)reader["Address"]; // Address
                    else Address = "";
                                       if (reader["Phone"] != DBNull.Value) Phone = (string)reader["Phone"]; // Phone
                    else Phone = "";

                    if (reader["Email"] != DBNull.Value) Email = (string)reader["Email"]; // Email
                    else Email = "";

                    NationalityID = (int)reader["NationalityID"]; // Nationality ID

                    if (reader["ImagePath"] != DBNull.Value) ImagePath = (string)reader["ImagePath"]; // Image Path
                    else ImagePath = "";
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
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool InsertPerson(ref int ID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime BirthDate, bool Gender, string Address, string Phone, string Email, int NationalityID, string ImagePath)
        {
            bool done = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"INSERT INTO [dbo].[People]
           ([NationalNo]
           ,[FirstName]
           ,[SecondName]
           ,[ThirdName]
           ,[LastName]
           ,[BirthDate]
           ,[Gender]
           ,[Address]
           ,[Phone]
           ,[Email]
           ,[NationalityID]
           ,[ImagePath])
     VALUES
           (@NationalNo
           ,@FirstName
           ,@SecondName
           ,@ThirdName
           ,@LastName
           ,@BirthDate
           ,@Gender
           ,@Address
           ,@Phone
           ,@Email
           ,@NationalityID
           ,@ImagePath);
            SELECT SCOPE_IDENTITY();";


            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
            //
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            //
            if (string.IsNullOrEmpty(SecondName))
                cmd.Parameters.AddWithValue("@SecondName", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@SecondName", SecondName);
            //
            if (string.IsNullOrEmpty(ThirdName))
                cmd.Parameters.AddWithValue("@ThirdName", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@ThirdName", ThirdName);
            //
            cmd.Parameters.AddWithValue("@LastName", LastName);
            //
            cmd.Parameters.AddWithValue("@BirthDate", BirthDate);
            //
            cmd.Parameters.AddWithValue("@Gender", Gender);
            //
            if (string.IsNullOrEmpty(Address))
                cmd.Parameters.AddWithValue("@Address", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@Address", Address);
            //
            if (string.IsNullOrEmpty(Phone))
                cmd.Parameters.AddWithValue("@Phone", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@Phone", Phone);
            //
            if (string.IsNullOrEmpty(Email))
                cmd.Parameters.AddWithValue("@Email", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@Email", Email);
            //
            cmd.Parameters.AddWithValue("@NationalityID", NationalityID);
            //
            if (string.IsNullOrEmpty(ImagePath))
                cmd.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@ImagePath", ImagePath);


            try
            {
                connection.Open();
                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    ID = InsertedID;
                    done = true;
                }
            }
            catch (Exception ex)
            {
                clsShared.lastError = ex.Message;
                done = false;
            }
            finally
            {
                connection.Close();
            }
            return done;
        }

        public static bool UpdatePerson(int ID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime BirthDate, bool Gender, string Address, string Phone, string Email, int NationalityID, string ImagePath)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"UPDATE [dbo].[People]
                            SET [NationalNo] = @NationalNo
                                ,[FirstName] = @FirstName
                                ,[SecondName] = @SecondName
                                ,[ThirdName] = @ThirdName
                                ,[LastName] = @LastName
                                ,[BirthDate] = @BirthDate
                                ,[Gender] = @Gender
                                ,[Address] = @Address
                                ,[Phone] = @Phone
                                ,[Email] = @Email
                                ,[NationalityID] = @NationalityID
                                ,[ImagePath] = @ImagePath
                            WHERE PersonID = @ID";

            SqlCommand cmd = new SqlCommand(query, connection);

            // Put Values
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            //
            if (!string.IsNullOrWhiteSpace(SecondName)) cmd.Parameters.AddWithValue("@SecondName", SecondName);
            else cmd.Parameters.AddWithValue("@SecondName", System.DBNull.Value);
            //
            if (!string.IsNullOrWhiteSpace(ThirdName)) cmd.Parameters.AddWithValue("@ThirdName", ThirdName);
            else cmd.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);
            //
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@BirthDate", BirthDate);
            cmd.Parameters.AddWithValue("@Gender", Gender);
            //
            if (!string.IsNullOrWhiteSpace(Address)) cmd.Parameters.AddWithValue("@Address", Address);
            else cmd.Parameters.AddWithValue("@Address", System.DBNull.Value);
            //
            if (!string.IsNullOrWhiteSpace(Phone)) cmd.Parameters.AddWithValue("@Phone", Phone);
            else cmd.Parameters.AddWithValue("@Phone", System.DBNull.Value);
            //
            if (!string.IsNullOrWhiteSpace(Email)) cmd.Parameters.AddWithValue("@Email", Email);
            else cmd.Parameters.AddWithValue("@Email", System.DBNull.Value);
            //
            cmd.Parameters.AddWithValue("@NationalityID", NationalityID);
            //
            if (!string.IsNullOrWhiteSpace(ImagePath)) cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
            else cmd.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);


            try
            {
                connection.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsShared.lastError = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return (rowsAffected > 0);
        }

        public static bool DeletePerson(int ID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "DELETE FROM People WHERE PersonID = @PersonID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@PersonID", ID);

            try
            {
                connection.Open();
                rowsAffected = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsShared.lastError = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return (rowsAffected > 0);
        }
        // Other Methods
        public static bool IsNationalNumberExists(string NationalNo)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select top 1 NationalNo from People where NationalNo = @NationalNo";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
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

        public static bool IsPersonIDExists(int ID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select top 1 NationalNo from People where PersonID = @PersonID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@PersonID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
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