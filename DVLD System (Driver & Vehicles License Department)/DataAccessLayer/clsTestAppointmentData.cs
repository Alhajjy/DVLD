using System;
using System.Data;
using System.Data.SqlClient;
using Shared;

namespace DataAccessLayer
{
	public class clsTestAppointmentData
	{
		public static bool GetTestAppointmentByID(int TestAppointmentID, ref int TestTypeID, ref int LocalLicenseApplicationID, ref DateTime AppointmentDate, ref decimal PaidFees, ref int CreatedByUserID, ref bool IsLocked)
		{
			bool isFound = false;
			string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
						connection.Open();
						using (SqlDataReader reader = command.ExecuteReader())
						{

							if (reader.Read())
							{
								isFound = true;

								TestTypeID = (int)reader["TestTypeID"];
								LocalLicenseApplicationID = (int)reader["LocalLicenseApplicationID"];
								AppointmentDate = (DateTime)reader["AppointmentDate"];
								PaidFees = Convert.ToDecimal(reader["PaidFees"]);
								CreatedByUserID = (int)reader["CreatedByUserID"];
								IsLocked = (bool)reader["IsLocked"];
							}
							else
							{
								isFound = false;
							}

						}
					}
				}
			}
			catch (Exception ex)
			{
				isFound = false;
				clsShared.lastError = ex.Message;
			}
			finally
			{

			}

			return isFound;
		}
		public static int AddNewTestAppointment(int TestTypeID, int LocalLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked)
		{
			int TestAppointmentID = -1;
			string query = @"INSERT INTO TestAppointments (TestTypeID, LocalLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked)
							VALUES (@TestTypeID, @LocalLicenseApplicationID, @AppointmentDate, @PaidFees, @CreatedByUserID, @IsLocked)
							SELECT SCOPE_IDENTITY();";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{

						command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
						command.Parameters.AddWithValue("@LocalLicenseApplicationID", LocalLicenseApplicationID);
						command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
						command.Parameters.AddWithValue("@PaidFees", PaidFees);
						command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
						command.Parameters.AddWithValue("@IsLocked", IsLocked);
						connection.Open();
						object result = command.ExecuteScalar();
						if (result != null && int.TryParse(result.ToString(), out int insertedID))
						{
							TestAppointmentID = insertedID;
						}
					}
				}
			}
			catch (Exception ex)
			{
				clsShared.lastError = ex.Message;
			}
			finally
			{

			}

			return TestAppointmentID;
		}
		public static bool UpdateTestAppointment(int TestAppointmentID, int TestTypeID, int LocalLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked)
		{
			int rowsAffected = 0;
			string query = @"UPDATE TestAppointments  
										SET 
										TestTypeID = @TestTypeID, 
							LocalLicenseApplicationID = @LocalLicenseApplicationID, 
							AppointmentDate = @AppointmentDate, 
							PaidFees = @PaidFees, 
							CreatedByUserID = @CreatedByUserID, 
							IsLocked = @IsLocked
							WHERE TestAppointmentID = @TestAppointmentID";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{

						command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
						command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
						command.Parameters.AddWithValue("@LocalLicenseApplicationID", LocalLicenseApplicationID);
						command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
						command.Parameters.AddWithValue("@PaidFees", PaidFees);
						command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
						command.Parameters.AddWithValue("@IsLocked", IsLocked);
						connection.Open();
						rowsAffected = command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				clsShared.lastError = ex.Message;
			}

			finally
			{

			}

			return (rowsAffected > 0);
		}
		public static bool DeleteTestAppointment(int TestAppointmentID)
		{
			int rowsAffected = 0;
			string query = @"Delete TestAppointments 
								where TestAppointmentID = @TestAppointmentID";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
						connection.Open();
						rowsAffected = command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				clsShared.lastError = ex.Message;
			}
			finally
			{

			}
			return (rowsAffected > 0);
		}
		public static bool IsTestAppointmentExist(int TestAppointmentID)
		{
			bool isFound = false;
			string query = "SELECT Found=1 FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
						connection.Open();
						using (SqlDataReader reader = command.ExecuteReader())
						{
							isFound = reader.HasRows;
						}
					}
				}
			}
			catch (Exception ex)
			{
				isFound = false;
				clsShared.lastError = ex.Message;
			}
			finally
			{

			}

			return isFound;
		}
		public static DataTable GetAllTestAppointments()
		{
			DataTable dt = new DataTable();
			string query = "SELECT * FROM TestAppointments order by TestAppointmentID desc";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						connection.Open();
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.HasRows)
							{
								dt.Load(reader);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{

				clsShared.lastError = ex.Message;
			}
			finally
			{
			}

			return dt;
		}
		// other
		public static DataTable GetAllTestAppointmentsForThisApplicationByTestTypeID(int LDLApp, int TestTypeID)
		{
			DataTable dt = new DataTable();
			string query = @"SELECT * FROM TestAppointments
								where LocalLicenseApplicationID = @LocalLicenseApplicationID
								and TestTypeID = @TestTypeID
								order by AppointmentDate desc";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@LocalLicenseApplicationID", LDLApp);
						command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
						connection.Open();
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.HasRows)
							{
								dt.Load(reader);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				clsShared.lastError = ex.Message;
			}
			finally
			{
			}

			return dt;
		}
		public static int GetTestAppointmentTrails(int LocalLicenseApplicationID, int TestTypeID)
		{
			int Trails = 0;
			string query = @"SELECT count(*) as Trails FROM TestAppointments
								where TestTypeID = @TestTypeID
								and   LocalLicenseApplicationID = @LocalLicenseApplicationID
								and   IsLocked = 1;";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
						command.Parameters.AddWithValue("@LocalLicenseApplicationID", LocalLicenseApplicationID);
						connection.Open();
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								Trails = (int)reader["Trails"];
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				clsShared.lastError = ex.Message;
			}
			finally
			{

			}

			return Trails;
		}
		public static bool SetTestAppointmentLocked(int TestAppointmentID)
		{
			int rowsAffected = 0;
			string query = @"UPDATE [dbo].[TestAppointments]
								   SET [IsLocked] = 1
								 WHERE TestAppointmentID = @TestAppointmentID";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{

						command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
						connection.Open();
						rowsAffected = command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				clsShared.lastError = ex.Message;
			}

			finally
			{

			}

			return (rowsAffected > 0);
		}
		public static bool IsTestAppointmentLocked(int TestAppointmentID)
		{
			bool isLocked = false;
			string query = @"select 1 from TestAppointments
								where IsLocked = 1
								and TestAppointmentID = @TestAppointmentID";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
						connection.Open();
						using (SqlDataReader reader = command.ExecuteReader())
						{
							isLocked = reader.HasRows;
						}
					}
				}
			}
			catch (Exception ex)
			{
				clsShared.lastError = ex.Message;
			}
			finally
			{

			}

			return isLocked;
		}
		public static bool IsAvailableToOpenTestAppointmentByTestType(int LocalLicenseApplicationID, int TestTypeID)
		{
			bool Available = false;
			string query = @"select 1 from TestAppointments
									where LocalLicenseApplicationID = @LocalLicenseApplicationID
									and IsLocked = 0
									and TestTypeID = @TestTypeID";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@LocalLicenseApplicationID", LocalLicenseApplicationID);
						command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
						connection.Open();
						using (SqlDataReader reader = command.ExecuteReader())
						{
							Available = !reader.HasRows;
						}
					}
				}
			}
			catch (Exception ex)
			{
				clsShared.lastError = ex.Message;
			}
			finally
			{

			}

			return Available;
		}
		public static int GetPassedTestsNumber(int LocalLicenseApplicationID)
		{
			int PassedTests = -1;
			string query = @"select count(*) as PassedTestsCount from TestAppointments INNER JOIN
										Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
									where LocalLicenseApplicationID = @LocalLicenseApplicationID
									and IsLocked = 1
									and Tests.TestResult = 1";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@LocalLicenseApplicationID", LocalLicenseApplicationID);
						connection.Open();
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								PassedTests = (int)reader["PassedTestsCount"];
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				clsShared.lastError = ex.Message;
			}
			finally
			{

			}

			return PassedTests;
		}
	}
}