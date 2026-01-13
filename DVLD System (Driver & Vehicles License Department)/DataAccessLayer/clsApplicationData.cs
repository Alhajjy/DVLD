using System;
using Shared;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
	public class clsApplicationData
	{
		public static DataTable GetAllApplications()
		{
			DataTable dt = new DataTable();

			SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

			string query = @"SELECT * FROM Applications;";

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
		public static bool InsertApplication(ref int ApplicationID, int ApplicationTypeID, int PersonID, DateTime ApplicationDate, int Status, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
		{
			bool done = false;
			SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

			string query = @"INSERT INTO [dbo].[Applications]
								   ([ApplicationTypeID]
								   ,[PersonID]
								   ,[ApplicationDate]
								   ,[Status]
								   ,[LastStatusDate]
								   ,[PaidFees]
								   ,[CreatedByUserID])
							 VALUES
								   (@ApplicationTypeID
								   ,@PersonID
								   ,@ApplicationDate
								   ,@Status
								   ,@LastStatusDate
								   ,@PaidFees
								   ,@CreatedByUserID);
									SELECT SCOPE_IDENTITY();";


			SqlCommand cmd = new SqlCommand(query, connection);

			cmd.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
			cmd.Parameters.AddWithValue("@PersonID", PersonID);
			cmd.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
			cmd.Parameters.AddWithValue("@Status", Status);
			cmd.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
			cmd.Parameters.Add("@PaidFees", SqlDbType.SmallMoney).Value = PaidFees;
			cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
			try
			{
				connection.Open();
				object result = cmd.ExecuteScalar();

				if (result != null && int.TryParse(result.ToString(), out int InsertedID))
				{
					ApplicationID = InsertedID;
					done = true;
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
			return done;
		}
		public static bool GetApplicationByID(int ApplicationID, ref int ApplicationTypeID, ref int PersonID, ref DateTime ApplicationDate, ref int Status, ref DateTime LastStatusDate, ref decimal PaidFees, ref int CreatedByUserID)
		{
			bool isFound = false;

			SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

			string query = "SELECT * FROM [dbo].[Applications] where ApplicationID = @ApplicationID";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

			try
			{
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					isFound = true;

					ApplicationTypeID = (int)reader["ApplicationTypeID"];
					PersonID = (int)reader["PersonID"];
					ApplicationDate = (DateTime)reader["ApplicationDate"];
					Status = Convert.ToInt32(reader["Status"]);
					LastStatusDate = (DateTime)reader["LastStatusDate"];
					PaidFees = (decimal)reader["PaidFees"];
					CreatedByUserID = (int)reader["CreatedByUserID"];
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
		public static bool UpdateApplication(int ApplicationID, int ApplicationTypeID, int PersonID, DateTime ApplicationDate, int Status, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
		{
			int rowsAffected = 0;

			SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

			string query = @"UPDATE [dbo].[Applications]
							   SET [ApplicationTypeID] = @ApplicationTypeID
								  ,[PersonID] = @PersonID
								  ,[ApplicationDate] = @ApplicationDate
								  ,[Status] = @Status
								  ,[LastStatusDate] = @LastStatusDate
								  ,[PaidFees] = @PaidFees
								  ,[CreatedByUserID] = @CreatedByUserID
							 WHERE ApplicationID = @ApplicationID";

			SqlCommand cmd = new SqlCommand(query, connection);

			// Put Values
			cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
			cmd.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
			cmd.Parameters.AddWithValue("@PersonID", PersonID);
			cmd.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
			cmd.Parameters.AddWithValue("@Status", Status);
			cmd.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
			cmd.Parameters.Add("@PaidFees", SqlDbType.SmallMoney).Value = PaidFees;
			cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


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
		public static bool DeleteApplication(int ApplicationID)
		{
			int rowsAffected = 0;

			SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

			string query = @"DELETE FROM [dbo].[Applications] WHERE ApplicationID = @ApplicationID";

			SqlCommand cmd = new SqlCommand(query, connection);

			cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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
		// other
		public static bool IsApplicationExists(int ApplicationID)
		{
			bool isFound = false;

			SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

			string query = "SELECT found=1 FROM [dbo].[Applications] where ApplicationID = @ApplicationID";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

			try
			{
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

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
		public static bool IsApplicationStatus_(int ApplicationID, int Status)
		{
			bool isFound = false;

			SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

			string query = @"SELECT found=1 FROM [dbo].[Applications]
									where ApplicationID = @ApplicationID
									and   Status = @Status";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
			command.Parameters.AddWithValue("@Status", Status);

			try
			{
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

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
		public static bool SetApplicationStatusCancelled(int ApplicationID)
		{
			int rowsAffected = -1;

			SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

			string query = @"UPDATE [dbo].[Applications]
							   SET [Status] = 2,
									[LastStatusDate] = @LastStatusDate
							 WHERE ApplicationID = @ApplicationID
							 and Status = 1;
							 ";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
			command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);

			try
			{
				connection.Open();
				rowsAffected = command.ExecuteNonQuery();
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
		public static bool SetApplicationStatusCompleted(int ApplicationID)
		{
			int rowsAffected = -1;

			SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

			string query = @"UPDATE [dbo].[Applications]
							   SET [Status] = 3,
								   [LastStatusDate] = @LastStatusDate
							 WHERE ApplicationID = @ApplicationID and Status = 1
							 ";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
			command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);

			try
			{
				connection.Open();
				rowsAffected = command.ExecuteNonQuery();
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
		public static int GetActiveApplicationIDForLicenseClass(int ApplicationTypeID, int PersonID, int LicenseClassID)
		{
			int ActiveApplicationID = -1;

			SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

			string query = @"SELECT Applications.ApplicationID as ActiveApplicationID
								FROM Applications INNER JOIN
									  LocalLicenseApplications ON Applications.ApplicationID = LocalLicenseApplications.ApplicationID
								where PersonID = @PersonID
								and Applications.ApplicationTypeID = ApplicationTypeID
								and LocalLicenseApplications.LicenseClassID = LicenseClassID
								and Status = 1;";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@PersonID", PersonID);
			command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
			command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
			try
			{
				connection.Open();
				object result = command.ExecuteScalar();


				if (result != null && int.TryParse(result.ToString(), out int AppID))
				{
					ActiveApplicationID = AppID;
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

			return ActiveApplicationID;
		}
		public static bool GetApplicantIdByAppID(int ApplicationID, ref int PersonID)
		{
			bool isFound = false;

			SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

			string query = @"select PersonID from Applications
								where ApplicationID = @ApplicationID";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

			try
			{
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					isFound = true;

					PersonID = (int)reader["PersonID"];
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
