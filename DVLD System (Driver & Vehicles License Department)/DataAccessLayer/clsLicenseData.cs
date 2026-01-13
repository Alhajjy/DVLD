using System;
using System.Data;
using System.Data.SqlClient;
using Shared;

namespace DataAccessLayer
{
	public class clsLicenseData
	{
		public static bool GetLicenseByID(int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClassID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref decimal PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
		{
			bool isFound = false;
			string query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@LicenseID", LicenseID);
						connection.Open();
						using (SqlDataReader reader = command.ExecuteReader())
						{

							if (reader.Read())
							{
								isFound = true;

								ApplicationID = (int)reader["ApplicationID"];
								DriverID = (int)reader["DriverID"];
								LicenseClassID = (int)reader["LicenseClassID"];
								IssueDate = (DateTime)reader["IssueDate"];
								ExpirationDate = (DateTime)reader["ExpirationDate"];

								if (reader["Notes"] != DBNull.Value)
									Notes = (string)reader["Notes"];
								else
									Notes = "";


								if (reader["PaidFees"] != DBNull.Value)
									PaidFees = (decimal)reader["PaidFees"];
								else
									PaidFees = -1;

								IsActive = (bool)reader["IsActive"];
								IssueReason = (byte)reader["IssueReason"];
								CreatedByUserID = (int)reader["CreatedByUserID"];
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
		public static bool GetLicenseByLicenseID(int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClassID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref decimal PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
		{
			bool isFound = false;
			string query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@LicenseID", LicenseID);
						connection.Open();
						using (SqlDataReader reader = command.ExecuteReader())
						{

							if (reader.Read())
							{
								isFound = true;

								ApplicationID = (int)reader["ApplicationID"];
								DriverID = (int)reader["DriverID"];
								LicenseClassID = (int)reader["LicenseClassID"];
								IssueDate = (DateTime)reader["IssueDate"];
								ExpirationDate = (DateTime)reader["ExpirationDate"];

								if (reader["Notes"] != DBNull.Value)
									Notes = (string)reader["Notes"];
								else
									Notes = "";


								if (reader["PaidFees"] != DBNull.Value)
									PaidFees = (decimal)reader["PaidFees"];
								else
									PaidFees = -1;

								IsActive = (bool)reader["IsActive"];
								IssueReason = (byte)reader["IssueReason"];
								CreatedByUserID = (int)reader["CreatedByUserID"];
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
		public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClassID, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
		{
			int LicenseID = -1;
			string query = @"INSERT INTO Licenses (ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID)
							VALUES (@ApplicationID, @DriverID, @LicenseClassID, @IssueDate, @ExpirationDate, @Notes, @PaidFees, @IsActive, @IssueReason, @CreatedByUserID)
							SELECT SCOPE_IDENTITY();";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{

						command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
						command.Parameters.AddWithValue("@DriverID", DriverID);
						command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
						command.Parameters.AddWithValue("@IssueDate", IssueDate);
						command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

						if (Notes != "")
							command.Parameters.AddWithValue("@Notes", Notes);
						else
							command.Parameters.AddWithValue("@Notes", DBNull.Value);

						if (PaidFees != -1)
							command.Parameters.AddWithValue("@PaidFees", PaidFees);
						else
							command.Parameters.AddWithValue("@PaidFees", DBNull.Value);
						command.Parameters.AddWithValue("@IsActive", IsActive);
						command.Parameters.AddWithValue("@IssueReason", IssueReason);
						command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
						connection.Open();
						object result = command.ExecuteScalar();
						if (result != null && int.TryParse(result.ToString(), out int insertedID))
						{
							LicenseID = insertedID;
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

			return LicenseID;
		}
		public static bool UpdateLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
		{
			int rowsAffected = 0;
			string query = @"UPDATE Licenses  
										SET 
										ApplicationID = @ApplicationID, 
							DriverID = @DriverID, 
							LicenseClassID = @LicenseClassID, 
							IssueDate = @IssueDate, 
							ExpirationDate = @ExpirationDate, 
							Notes = @Notes, 
							PaidFees = @PaidFees, 
							IsActive = @IsActive, 
							IssueReason = @IssueReason, 
							CreatedByUserID = @CreatedByUserID
							WHERE LicenseID = @LicenseID";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{

						command.Parameters.AddWithValue("@LicenseID", LicenseID);
						command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
						command.Parameters.AddWithValue("@DriverID", DriverID);
						command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
						command.Parameters.AddWithValue("@IssueDate", IssueDate);
						command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
						command.Parameters.AddWithValue("@Notes", Notes);
						command.Parameters.AddWithValue("@PaidFees", PaidFees);
						command.Parameters.AddWithValue("@IsActive", IsActive);
						command.Parameters.AddWithValue("@IssueReason", IssueReason);
						command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
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
		public static bool DeleteLicense(int LicenseID)
		{
			int rowsAffected = 0;
			string query = @"Delete Licenses 
								where LicenseID = @LicenseID";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@LicenseID", LicenseID);
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
		public static bool IsLicenseExist(int LicenseID)
		{
			bool isFound = false;
			string query = "SELECT Found=1 FROM Licenses WHERE LicenseID = @LicenseID";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@LicenseID", LicenseID);
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
		public static DataTable GetAllLicenses()
		{
			DataTable dt = new DataTable();
			string query = "SELECT * FROM Licenses";
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
		public static DataTable GetAllLicensesByDriverID(int DriverID)
		{
			DataTable dt = new DataTable();
			string query = @"SELECT Licenses.LicenseID, Licenses.ApplicationID, LicenseClasses.ClassName, Licenses.IssueDate, Licenses.ExpirationDate, Licenses.IsActive
								FROM Licenses INNER JOIN
									 LicenseClasses ON Licenses.LicenseClassID = LicenseClasses.LicenseClassID
									 where DriverID = @DriverID";
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
		public static bool DoesPersonHaveAnActiveLicenseByLicenseClassID(int PersonID, int LicenseClassID)
		{
			bool isFound = false;
			string query = @"SELECT top 1 found=1 FROM Licenses
									WHERE (select PersonID from Drivers where Drivers.DriverID = Licenses.DriverID) = @PersonID
									and   Licenses.LicenseClassID = @LicenseClassID
									and IsActive = 1
									and ExpirationDate < Getdate()
									order by IssueDate desc;";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@PersonID", PersonID);
						command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
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
				clsShared.lastError = ex.Message;
			}
			finally
			{

			}

			return isFound;
		}
		public static bool IsApplicationIssuedTheLicense(int ApplicationID)
		{
			bool isFound = false;
			string query = @"SELECT top(1) 1 FROM Licenses where ApplicationID = @ApplicationID;";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
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
				clsShared.lastError = ex.Message;
			}
			finally
			{

			}

			return isFound;
		}
		public static bool GetPersonIDByLicenseID(ref int personID, int LicenseID)
		{
			bool isFound = false;
			string query = @"SELECT Drivers.PersonID
									FROM Licenses INNER JOIN
											Drivers ON Licenses.DriverID = Drivers.DriverID INNER JOIN
											People ON Drivers.PersonID = People.PersonID
											where LicenseID = @LicenseID;";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@LicenseID", LicenseID);
						connection.Open();
						using (SqlDataReader reader = command.ExecuteReader())
						{

							if (reader.Read())
							{
								personID = (int)reader["PersonID"];
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
		public static bool GetLicenseIDByLocalDrivingLicenseAppID(ref int LicenseID, int LocalDrivingLicenseApplicationID)
		{
			bool isFound = false;
			string query = @"SELECT * FROM Licenses WHERE ApplicationID = (
								select ApplicationID from LocalLicenseApplications
									   where LocalLicenseAppID = @LocalLicenseAppID);";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@LocalLicenseAppID", LocalDrivingLicenseApplicationID);
						connection.Open();
						using (SqlDataReader reader = command.ExecuteReader())
						{

							if (reader.Read())
							{
								isFound = true;

								LicenseID = (int)reader["LicenseID"];
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
		public static bool GetLastLicenseIDByDriverID(ref int LicenseID, int DriverID, int LicenseClassID)
		{
			bool isFound = false;
			string query = @"SELECT top 1 LicenseID FROM Licenses
									WHERE  DriverID = @DriverID
									and    LicenseClassID = @LicenseClassID
									order by IssueDate desc;";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@DriverID", DriverID);
						command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
						connection.Open();
						using (SqlDataReader reader = command.ExecuteReader())
						{

							if (reader.Read())
							{
								LicenseID = (int)reader["LicenseID"];
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
		public static bool GetLastLicenseByDriverIDWithLicenseClass(ref int LicenseID, ref int ApplicationID, int DriverID, int LicenseClassID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref decimal PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
		{
			bool isFound = false;
			string query = @"SELECT top 1 * FROM Licenses
									WHERE DriverID = @DriverID
									and   LicenseClassID = 3
									order by IssueDate desc";
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
								LicenseID = (int)reader["LicenseID"];
								ApplicationID = (int)reader["ApplicationID"];
								IssueDate = (DateTime)reader["IssueDate"];
								ExpirationDate = (DateTime)reader["ExpirationDate"];

								if (reader["Notes"] != DBNull.Value)
									Notes = (string)reader["Notes"];
								else
									Notes = "";


								if (reader["PaidFees"] != DBNull.Value)
									PaidFees = (decimal)reader["PaidFees"];
								else
									PaidFees = -1;

								IsActive = (bool)reader["IsActive"];
								IssueReason = (byte)reader["IssueReason"];
								CreatedByUserID = (int)reader["CreatedByUserID"];
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
		public static bool IsDateExpired(int LicenseID)
		{
			bool isFound = false;
			string query = @"SELECT Found=1 FROM Licenses
									WHERE Licenses.LicenseID = @LicenseID
									AND ExpirationDate <= Getdate()";

			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@LicenseID", LicenseID);
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
				clsShared.lastError = ex.Message;
			}
			finally
			{

			}

			return isFound;
		}
		public static bool IsLicenseActive(int LicenseID)
		{
			bool isFound = false;
			string query = @"SELECT Found=1 FROM Licenses
									WHERE Licenses.LicenseID = @LicenseID
									AND IsActive = 1";

			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@LicenseID", LicenseID);
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
				clsShared.lastError = ex.Message;
			}
			finally
			{

			}

			return isFound;
		}
		public static bool DeactivateLicense(int LicenseID)
		{
			int rowsAffected = 0;
			string query = @"UPDATE Licenses  
							SET IsActive = 0
							WHERE LicenseID = @LicenseID";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@LicenseID", LicenseID);
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

	}
}
