using System;
using System.Data;
using System.Data.SqlClient;
using Shared;

namespace DataAccessLayer
{
	public class clsDriverData
	{
		public static bool GetDriverByID(int DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
		{
			bool isFound = false;
			string query = "SELECT * FROM Drivers WHERE DriverID = @DriverID";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@DriverID", DriverID);
						connection.Open();
						using (SqlDataReader reader = command.ExecuteReader())
						{

							if (reader.Read())
							{
								isFound = true;

								PersonID = (int)reader["PersonID"];
								CreatedByUserID = (int)reader["CreatedByUserID"];
								CreatedDate = (DateTime)reader["CreatedDate"];
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
			}
			finally
			{

			}

			return isFound;
		}
		public static bool GetDriverIDByPersonID(ref int DriverID, int PersonID)
		{
			bool isFound = false;
			string query = "SELECT DriverID FROM Drivers WHERE PersonID = @PersonID";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@PersonID", PersonID);
						connection.Open();
						using (SqlDataReader reader = command.ExecuteReader())
						{

							if (reader.Read())
							{
								DriverID = (int)reader["DriverID"];
								isFound = true;
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

			return isFound;
		}
		public static bool GetDriverIDByLocalDrivingLicenseApplicationID(ref int DriverID, int LocalLicenseAppID)
		{
			bool isFound = false;
			string query = @"SELECT Drivers.DriverID
									FROM Applications INNER JOIN
												LocalLicenseApplications ON Applications.ApplicationID = LocalLicenseApplications.ApplicationID INNER JOIN
												Licenses ON Applications.ApplicationID = Licenses.ApplicationID INNER JOIN
												Drivers ON Licenses.DriverID = Drivers.DriverID
												where LocalLicenseAppID = @LocalLicenseAppID";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@LocalLicenseAppID", LocalLicenseAppID);
						connection.Open();
						using (SqlDataReader reader = command.ExecuteReader())
						{

							if (reader.Read())
							{
								DriverID = (int)reader["DriverID"];
								isFound = true;
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

			return isFound;
		}
		public static int AddNewDriver(int PersonID, int CreatedByUserID, DateTime CreatedDate)
		{
			int DriverID = -1;
			string query = @"INSERT INTO Drivers (PersonID, CreatedByUserID, CreatedDate)
							VALUES (@PersonID, @CreatedByUserID, @CreatedDate)
							SELECT SCOPE_IDENTITY();";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{

						command.Parameters.AddWithValue("@PersonID", PersonID);
						command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
						command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
						connection.Open();
						object result = command.ExecuteScalar();
						if (result != null && int.TryParse(result.ToString(), out int insertedID))
						{
							DriverID = insertedID;
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

			return DriverID;
		}
		public static bool UpdateDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
		{
			int rowsAffected = 0;
			string query = @"UPDATE Drivers  
										SET 
										PersonID = @PersonID, 
							CreatedByUserID = @CreatedByUserID, 
							CreatedDate = @CreatedDate
							WHERE DriverID = @DriverID";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{

						command.Parameters.AddWithValue("@DriverID", DriverID);
						command.Parameters.AddWithValue("@PersonID", PersonID);
						command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
						command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
						connection.Open();
						rowsAffected = command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				return false;
			}

			finally
			{

			}

			return (rowsAffected > 0);
		}
		public static bool DeleteDriver(int DriverID)
		{
			int rowsAffected = 0;
			string query = @"Delete Drivers 
								where DriverID = @DriverID";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@DriverID", DriverID);
						connection.Open();
						rowsAffected = command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
			}
			finally
			{

			}
			return (rowsAffected > 0);
		}
		public static bool IsDriverExist(int DriverID)
		{
			bool isFound = false;
			string query = "SELECT Found=1 FROM Drivers WHERE DriverID = @DriverID";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@DriverID", DriverID);
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
			}
			finally
			{

			}

			return isFound;
		}
		public static bool IsDriverExistByPersonID(int PersonID)
		{
			bool isFound = false;
			string query = "SELECT Found=1 FROM Drivers WHERE PersonID = @PersonID";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@PersonID", PersonID);
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
		public static DataTable GetAllDrivers()
		{
			DataTable dt = new DataTable();
			string query = @"select * from Drivers_View";
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

			}
			finally
			{

			}

			return dt;
		}
	}
}
