using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;

namespace MEDICARE_HOSPITAL_MANGAMENT.Repositories
{
    /// <summary>
    /// Data Access Repository for Patients with parameterized queries and soft deletion.
    /// </summary>
    public class PatientRepository
    {
        public List<Patient> GetAllPatients(bool activeOnly = false)
        {
            var list = new List<Patient>();
            string query = @"
                SELECT PatientID, PatientCode, NIC, FirstName, LastName, DateOfBirth, Gender, 
                       BloodGroup, Phone, Email, Address, EmergencyContactName, EmergencyContactPhone, 
                       RegisteredAt, IsActive
                FROM dbo.Patients";

            if (activeOnly)
            {
                query += " WHERE IsActive = 1";
            }
            query += " ORDER BY PatientID DESC;";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(MapPatientFromReader(reader));
            }
            return list;
        }

        public Patient? GetPatientById(int patientId)
        {
            const string query = @"
                SELECT PatientID, PatientCode, NIC, FirstName, LastName, DateOfBirth, Gender, 
                       BloodGroup, Phone, Email, Address, EmergencyContactName, EmergencyContactPhone, 
                       RegisteredAt, IsActive
                FROM dbo.Patients
                WHERE PatientID = @PatientID;";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@PatientID", SqlDbType.Int) { Value = patientId });

            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return MapPatientFromReader(reader);
            }
            return null;
        }

        public Patient? GetPatientByCode(string patientCode)
        {
            const string query = @"
                SELECT PatientID, PatientCode, NIC, FirstName, LastName, DateOfBirth, Gender, 
                       BloodGroup, Phone, Email, Address, EmergencyContactName, EmergencyContactPhone, 
                       RegisteredAt, IsActive
                FROM dbo.Patients
                WHERE PatientCode = @PatientCode;";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@PatientCode", SqlDbType.VarChar, 20) { Value = patientCode.Trim() });

            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return MapPatientFromReader(reader);
            }
            return null;
        }

        public List<Patient> SearchPatients(string query)
        {
            var list = new List<Patient>();
            const string sql = @"
                SELECT PatientID, PatientCode, NIC, FirstName, LastName, DateOfBirth, Gender, 
                       BloodGroup, Phone, Email, Address, EmergencyContactName, EmergencyContactPhone, 
                       RegisteredAt, IsActive
                FROM dbo.Patients
                WHERE PatientCode LIKE @Search
                   OR NIC LIKE @Search
                   OR FirstName LIKE @Search
                   OR LastName LIKE @Search
                   OR Phone LIKE @Search
                ORDER BY PatientID DESC;";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@Search", SqlDbType.VarChar, 100) { Value = $"%{query.Trim()}%" });

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(MapPatientFromReader(reader));
            }
            return list;
        }

        public int CreatePatient(Patient patient)
        {
            const string query = @"
                INSERT INTO dbo.Patients (
                    PatientCode, NIC, FirstName, LastName, DateOfBirth, Gender, 
                    BloodGroup, Phone, Email, Address, EmergencyContactName, EmergencyContactPhone, 
                    RegisteredAt, IsActive
                ) VALUES (
                    @PatientCode, @NIC, @FirstName, @LastName, @DateOfBirth, @Gender, 
                    @BloodGroup, @Phone, @Email, @Address, @EmergencyContactName, @EmergencyContactPhone, 
                    SYSDATETIME(), @IsActive
                );
                SELECT SCOPE_IDENTITY();";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add(new SqlParameter("@PatientCode", SqlDbType.VarChar, 20) { Value = patient.PatientCode.Trim() });
            cmd.Parameters.Add(new SqlParameter("@NIC", SqlDbType.VarChar, 20) { Value = (object?)patient.NIC?.Trim() ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar, 50) { Value = patient.FirstName.Trim() });
            cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 50) { Value = patient.LastName.Trim() });
            cmd.Parameters.Add(new SqlParameter("@DateOfBirth", SqlDbType.Date) { Value = patient.DateOfBirth.Date });
            cmd.Parameters.Add(new SqlParameter("@Gender", SqlDbType.VarChar, 20) { Value = patient.Gender.Trim() });
            cmd.Parameters.Add(new SqlParameter("@BloodGroup", SqlDbType.VarChar, 5) { Value = (object?)patient.BloodGroup?.Trim() ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.VarChar, 20) { Value = patient.Phone.Trim() });
            cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 100) { Value = (object?)patient.Email?.Trim() ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.VarChar, 250) { Value = (object?)patient.Address?.Trim() ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@EmergencyContactName", SqlDbType.VarChar, 100) { Value = (object?)patient.EmergencyContactName?.Trim() ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@EmergencyContactPhone", SqlDbType.VarChar, 20) { Value = (object?)patient.EmergencyContactPhone?.Trim() ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = patient.IsActive });

            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public bool UpdatePatient(Patient patient)
        {
            const string query = @"
                UPDATE dbo.Patients
                SET NIC = @NIC,
                    FirstName = @FirstName,
                    LastName = @LastName,
                    DateOfBirth = @DateOfBirth,
                    Gender = @Gender,
                    BloodGroup = @BloodGroup,
                    Phone = @Phone,
                    Email = @Email,
                    Address = @Address,
                    EmergencyContactName = @EmergencyContactName,
                    EmergencyContactPhone = @EmergencyContactPhone,
                    IsActive = @IsActive
                WHERE PatientID = @PatientID;";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add(new SqlParameter("@NIC", SqlDbType.VarChar, 20) { Value = (object?)patient.NIC?.Trim() ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar, 50) { Value = patient.FirstName.Trim() });
            cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 50) { Value = patient.LastName.Trim() });
            cmd.Parameters.Add(new SqlParameter("@DateOfBirth", SqlDbType.Date) { Value = patient.DateOfBirth.Date });
            cmd.Parameters.Add(new SqlParameter("@Gender", SqlDbType.VarChar, 20) { Value = patient.Gender.Trim() });
            cmd.Parameters.Add(new SqlParameter("@BloodGroup", SqlDbType.VarChar, 5) { Value = (object?)patient.BloodGroup?.Trim() ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.VarChar, 20) { Value = patient.Phone.Trim() });
            cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 100) { Value = (object?)patient.Email?.Trim() ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.VarChar, 250) { Value = (object?)patient.Address?.Trim() ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@EmergencyContactName", SqlDbType.VarChar, 100) { Value = (object?)patient.EmergencyContactName?.Trim() ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@EmergencyContactPhone", SqlDbType.VarChar, 20) { Value = (object?)patient.EmergencyContactPhone?.Trim() ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = patient.IsActive });
            cmd.Parameters.Add(new SqlParameter("@PatientID", SqlDbType.Int) { Value = patient.PatientID });

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool DeactivatePatient(int patientId)
        {
            const string query = "UPDATE dbo.Patients SET IsActive = 0 WHERE PatientID = @PatientID;";
            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@PatientID", SqlDbType.Int) { Value = patientId });

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public string GenerateNextPatientCode()
        {
            const string query = "SELECT MAX(PatientID) FROM dbo.Patients;";
            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);

            conn.Open();
            object? result = cmd.ExecuteScalar();
            int nextId = (result != null && result != DBNull.Value) ? Convert.ToInt32(result) + 1 : 1;
            return $"PAT{nextId:D3}";
        }

        public bool IsNICTaken(string nic, int? excludePatientId = null)
        {
            if (string.IsNullOrWhiteSpace(nic))
                return false;

            const string query = @"
                SELECT COUNT(1) FROM dbo.Patients 
                WHERE NIC = @NIC 
                  AND (@ExcludeID IS NULL OR PatientID != @ExcludeID);";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add(new SqlParameter("@NIC", SqlDbType.VarChar, 20) { Value = nic.Trim() });
            cmd.Parameters.Add(new SqlParameter("@ExcludeID", SqlDbType.Int) { Value = (object?)excludePatientId ?? DBNull.Value });

            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        private static Patient MapPatientFromReader(SqlDataReader reader)
        {
            return new Patient
            {
                PatientID = reader.GetInt32(reader.GetOrdinal("PatientID")),
                PatientCode = reader.GetString(reader.GetOrdinal("PatientCode")),
                NIC = reader.IsDBNull(reader.GetOrdinal("NIC")) ? null : reader.GetString(reader.GetOrdinal("NIC")),
                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                Gender = reader.GetString(reader.GetOrdinal("Gender")),
                BloodGroup = reader.IsDBNull(reader.GetOrdinal("BloodGroup")) ? null : reader.GetString(reader.GetOrdinal("BloodGroup")),
                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
                EmergencyContactName = reader.IsDBNull(reader.GetOrdinal("EmergencyContactName")) ? null : reader.GetString(reader.GetOrdinal("EmergencyContactName")),
                EmergencyContactPhone = reader.IsDBNull(reader.GetOrdinal("EmergencyContactPhone")) ? null : reader.GetString(reader.GetOrdinal("EmergencyContactPhone")),
                RegisteredAt = reader.GetDateTime(reader.GetOrdinal("RegisteredAt")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
            };
        }
    }
}
