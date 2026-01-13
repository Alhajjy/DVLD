using System;
using Shared;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
	public class clsLocalDrivingLicenseAppData
	{
		public static DataTable GetLocalDrivingLicenseApps()
		{
			DataTable dt = new DataTable();

			SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

			string query = @"SELECT * FROM LocalLicenseApplications";

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
		public static DataTable GetLocalDrivingLicenseAppsView()
		{
			DataTable dt = new DataTable();

			SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

			string query = @"SELECT * FROM [dbo].[LocalDrivingLicenseApps_View1] order by ApplicationDate desc";

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


		public static bool InsertLocalDrivingLicenseApp(ref int LocalLicenseAppID, int ApplicationID, int LicenseClassID)
		{
			bool done = false;
			SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

			string query = @"INSERT INTO [dbo].[LocalLicenseApplications]
								   ([ApplicationID]
								   ,[LicenseClassID])
							 VALUES
								   (@ApplicationID, @LicenseClassID);
									SELECT SCOPE_IDENTITY();";


			SqlCommand cmd = new SqlCommand(query, connection);

			cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
			cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
			try
			{
				connection.Open();
				object result = cmd.ExecuteScalar();

				if (result != null && int.TryParse(result.ToString(), out int InsertedID))
				{
					LocalLicenseAppID = InsertedID;
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
		public static bool GetLocalDrivingLicenseAppByID(int LocalLicenseAppID, ref int ApplicationID, ref int LicenseClassID)
		{
			bool isFound = false;

			SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

			string query = @"select * from LocalLicenseApplications
								where LocalLicenseAppID = @LocalLicenseAppID";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@LocalLicenseAppID", LocalLicenseAppID);

			try
			{
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					isFound = true;

					ApplicationID = (int)reader["ApplicationID"];
					LicenseClassID = (int)reader["LicenseClassID"];
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
		public static bool UpdateLocalDrivingLicenseApp(int LocalLicenseAppID, int ApplicationID, int LicenseClassID)
		{
			int rowsAffected = 0;

			SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

			string query = @"UPDATE [dbo].[LocalLicenseApplications]
								   SET [ApplicationID] = @ApplicationID
									  ,[LicenseClassID] = @LicenseClassID
								 WHERE LocalLicenseAppID = @LocalLicenseAppID";

			SqlCommand cmd = new SqlCommand(query, connection);

			// Put Values
			cmd.Parameters.AddWithValue("@LocalLicenseAppID", LocalLicenseAppID);
			cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
			cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);


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
		public static bool DeleteLocalDrivingLicenseApp(int LocalLicenseAppID)
		{
			int rowsAffected = 0;

			SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

			string query = @"DELETE FROM [dbo].[LocalLicenseApplications] WHERE LocalLicenseAppID = @LocalLicenseAppID";

			SqlCommand cmd = new SqlCommand(query, connection);

			cmd.Parameters.AddWithValue("@LocalLicenseAppID", LocalLicenseAppID);

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
		public static bool IsLocalDrivingLicenseAppExists(int LocalLicenseAppID)
		{
			bool isFound = false;

			SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

			string query = "SELECT found=1 FROM [dbo].[LocalLicenseApplications] where LocalLicenseAppID = @LocalLicenseAppID";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@LocalLicenseAppID", LocalLicenseAppID);

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

		// other
		//uuu
		public static bool DoesPersonHaveActiveApplicationWithSameLicenseClass(int PersonID, int LicenseClassID)
		{
			bool isPersonHave = false;

			SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

			string query = @"SELECT 1
								FROM LocalLicenseApplications L
								JOIN Applications A ON
									A.ApplicationID = L.ApplicationID
								WHERE A.PersonID = @PersonID
								  AND A.Status != 2
								  AND L.LicenseClassID = @LicenseClassID;";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
			command.Parameters.AddWithValue("@PersonID", PersonID);

			try
			{
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.HasRows)
				{
					isPersonHave = true;
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

			return isPersonHave;
		}
		public static bool IsAppHaveAtLeastTestAppointment(int LocalLicenseAppID)
		{
			bool have = false;

			SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

			string query = @"select top 1 1 from TestAppointments inner join LocalLicenseApplications
									on TestAppointments.LocalLicenseApplicationID = LocalLicenseApplications.LocalLicenseAppID
									where LocalLicenseApplicationID = @LocalLicenseApplicationID";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@LocalLicenseApplicationID", LocalLicenseAppID);

			try
			{
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.HasRows)
				{
					have = true;
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

			return have;
		}
		public static bool IsAppCanBeCancelled(int LocalLicenseAppID)
		{
			bool have = false;

			SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

			string query = @"SELECT 1 FROM Applications INNER JOIN LocalLicenseApplications
									ON Applications.ApplicationID = LocalLicenseApplications.ApplicationID
									where LocalLicenseAppID = @LocalLicenseAppID
									and   Status = 1";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@LocalLicenseAppID", LocalLicenseAppID);

			try
			{
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.HasRows)
				{
					have = true;
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

			return have;
		}
		public static bool IsAppCancelled(int LocalLicenseAppID)
		{
			bool have = false;

			SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

			string query = @"SELECT 1 FROM Applications INNER JOIN LocalLicenseApplications
									ON Applications.ApplicationID = LocalLicenseApplications.ApplicationID
									where LocalLicenseAppID = @LocalLicenseAppID
									and   Status = 2";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@LocalLicenseAppID", LocalLicenseAppID);

			try
			{
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.HasRows)
				{
					have = true;
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

			return have;
		}
		public static bool DoesPassTestType(int LocalLicenseAppID, int TestTypeID)
		{
			bool pass = false;

			SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

			string query = @"SELECT 1 FROM Tests INNER JOIN TestAppointments
												ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID inner join LocalLicenseApplications
												ON TestAppointments.LocalLicenseApplicationID = LocalLicenseApplications.LocalLicenseAppID
							where LocalLicenseApplications.LocalLicenseAppID = @LocalLicenseAppID and
									TestAppointments.TestTypeID = @TestTypeID and
									Tests.TestResult = 1;";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@LocalLicenseAppID", LocalLicenseAppID);
			command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

			try
			{
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.HasRows)
				{
					pass = true;
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

			return pass;
		}
		public static bool GetApplicantIdByLDLAppID(int LDLAppID, ref int PersonID)
		{
			bool isFound = false;

			SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

			string query = @"SELECT Applications.PersonID
								FROM Applications INNER JOIN
									LocalLicenseApplications ON Applications.ApplicationID = LocalLicenseApplications.ApplicationID
								where LocalLicenseAppID = @LocalLicenseAppID";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@LocalLicenseAppID", LDLAppID);

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

