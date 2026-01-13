using System;
using System.Data;
using System.Data.SqlClient;
using Shared;

namespace DataAccessLayer
{
	public class clsDetainingData
	{
		public static bool GetDetainingByID(int DetainingID, ref int LicenseID, ref DateTime DetainDate, ref decimal FineFees, ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
		{
			bool isFound = false;
			string query = @"SELECT * FROM DetainedLicense WHERE DetainingID = @DetainingID";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@DetainingID", DetainingID);
						connection.Open();
						using (SqlDataReader reader = command.ExecuteReader())
						{

							if (reader.Read())
							{
								isFound = true;

								LicenseID = (int)reader["LicenseID"];
								DetainDate = (DateTime)reader["DetainDate"];
								FineFees = Convert.ToDecimal(reader["FineFees"]);
								CreatedByUserID = (int)reader["CreatedByUserID"];
								IsReleased = (bool)reader["IsReleased"];

								if (reader["ReleaseDate"] != DBNull.Value)
									ReleaseDate = (DateTime)reader["ReleaseDate"];
								else
									ReleaseDate = DateTime.MinValue;


								if (reader["ReleasedByUserID"] != DBNull.Value)
									ReleasedByUserID = (int)reader["ReleasedByUserID"];
								else
									ReleasedByUserID = -1;


								if (reader["ReleaseApplicationID"] != DBNull.Value)
									ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
								else
									ReleaseApplicationID = -1;

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
		public static bool GetDetainingByDetainingID(int DetainingID, ref int LicenseID, ref DateTime DetainDate, ref decimal FineFees, ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
		{
			bool isFound = false;
			string query = "SELECT * FROM DetainedLicense WHERE DetainingID = @DetainingID";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@DetainingID", DetainingID);
						connection.Open();
						using (SqlDataReader reader = command.ExecuteReader())
						{

							if (reader.Read())
							{
								isFound = true;

								LicenseID = (int)reader["LicenseID"];
								DetainDate = (DateTime)reader["DetainDate"];
								FineFees = Convert.ToDecimal(reader["FineFees"]);
								CreatedByUserID = (int)reader["CreatedByUserID"];
								IsReleased = (bool)reader["IsReleased"];

								if (reader["ReleaseDate"] != DBNull.Value)
									ReleaseDate = (DateTime)reader["ReleaseDate"];
								else
									ReleaseDate = DateTime.MinValue;


								if (reader["ReleasedByUserID"] != DBNull.Value)
									ReleasedByUserID = (int)reader["ReleasedByUserID"];
								else
									ReleasedByUserID = -1;


								if (reader["ReleaseApplicationID"] != DBNull.Value)
									ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
								else
									ReleaseApplicationID = -1;

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
		public static bool GetDetainingByLicenseID(ref int DetainingID, int LicenseID, ref DateTime DetainDate, ref decimal FineFees, ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
		{
			bool isFound = false;
			string query = @"SELECT * FROM DetainedLicense WHERE LicenseID = @LicenseID
									order by DetainDate desc";
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

								DetainingID = (int)reader["DetainingID"];
								DetainDate = (DateTime)reader["DetainDate"];
								FineFees = Convert.ToDecimal(reader["FineFees"]);
								CreatedByUserID = (int)reader["CreatedByUserID"];
								IsReleased = (bool)reader["IsReleased"];

								if (reader["ReleaseDate"] != DBNull.Value)
									ReleaseDate = (DateTime)reader["ReleaseDate"];
								else
									ReleaseDate = DateTime.MinValue;


								if (reader["ReleasedByUserID"] != DBNull.Value)
									ReleasedByUserID = (int)reader["ReleasedByUserID"];
								else
									ReleasedByUserID = -1;


								if (reader["ReleaseApplicationID"] != DBNull.Value)
									ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
								else
									ReleaseApplicationID = -1;

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
		public static int AddNewDetaining(int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
		{
			int DetainingID = -1;
			string query = @"INSERT INTO DetainedLicense (LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID)
							VALUES (@LicenseID, @DetainDate, @FineFees, @CreatedByUserID, @IsReleased, @ReleaseDate, @ReleasedByUserID, @ReleaseApplicationID)
							SELECT SCOPE_IDENTITY();";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{

						command.Parameters.AddWithValue("@LicenseID", LicenseID);
						command.Parameters.AddWithValue("@DetainDate", DetainDate);
						command.Parameters.AddWithValue("@FineFees", FineFees);
						command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
						command.Parameters.AddWithValue("@IsReleased", IsReleased);

						if (ReleaseDate != DateTime.MinValue)
							command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
						else
							command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);

						if (ReleasedByUserID != -1)
							command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
						else
							command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);

						if (ReleaseApplicationID != -1)
							command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
						else
							command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);
						connection.Open();
						object result = command.ExecuteScalar();
						if (result != null && int.TryParse(result.ToString(), out int insertedID))
						{
							DetainingID = insertedID;
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

			return DetainingID;
		}
		public static bool UpdateDetaining(int DetainingID, int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
		{
			int rowsAffected = 0;
			string query = @"UPDATE DetainedLicense  
										SET 
										LicenseID = @LicenseID, 
							DetainDate = @DetainDate, 
							FineFees = @FineFees, 
							CreatedByUserID = @CreatedByUserID, 
							IsReleased = @IsReleased, 
							ReleaseDate = @ReleaseDate, 
							ReleasedByUserID = @ReleasedByUserID, 
							ReleaseApplicationID = @ReleaseApplicationID
							WHERE DetainingID = @DetainingID";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{

						command.Parameters.AddWithValue("@DetainingID", DetainingID);
						command.Parameters.AddWithValue("@LicenseID", LicenseID);
						command.Parameters.AddWithValue("@DetainDate", DetainDate);
						command.Parameters.AddWithValue("@FineFees", FineFees);
						command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
						command.Parameters.AddWithValue("@IsReleased", IsReleased);
						command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
						command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
						command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
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
		public static bool DeleteDetaining(int DetainingID)
		{
			int rowsAffected = 0;
			string query = @"Delete DetainedLicense 
								where DetainingID = @DetainingID";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@DetainingID", DetainingID);
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
		public static bool IsDetainingExist(int DetainingID)
		{
			bool isFound = false;
			string query = "SELECT Found=1 FROM DetainedLicense WHERE DetainingID = @DetainingID";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@DetainingID", DetainingID);
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
		public static bool IsDetainingExistByLicenseID(int LicenseID)
		{
			bool isFound = false;
			string query = "SELECT Found=1 FROM DetainedLicense WHERE LicenseID = @LicenseID";
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
		public static DataTable GetAllDetainings()
		{
			DataTable dt = new DataTable();
			string query = "select * from DetainedLicenses_View";
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
		// other
		public static bool IsLicenseDetained(int LicenseID)
		{
			bool isFound = false;
			string query = "SELECT Found=1 FROM DetainedLicense WHERE LicenseID = @LicenseID and IsReleased = 0";
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
				isFound = false;
			}
			finally
			{

			}

			return isFound;
		}
		public static bool IsLicenseReleased(int LicenseID)
		{
			bool isRelesased = false;
			string query = @"SELECT top 1 IsReleased FROM DetainedLicense WHERE LicenseID = @LicenseID
							    order by DetainDate desc";
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
							isRelesased = (bool)reader["IsReleased"];
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

			return isRelesased;
		}
		public static bool ReleaseDetainedLicense(int DetainingID, DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
		{
			int rowsAffected = 0;
			string query = @"UPDATE DetainedLicense  
								SET IsReleased = 1,
								ReleaseDate = @ReleaseDate,
								ReleasedByUserID = @ReleasedByUserID,
								ReleaseApplicationID = @ReleaseApplicationID
								WHERE DetainingID = @DetainingID;";
			try
			{
				using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{

						command.Parameters.AddWithValue("@DetainingID", DetainingID);
						command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
						command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
						command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
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
