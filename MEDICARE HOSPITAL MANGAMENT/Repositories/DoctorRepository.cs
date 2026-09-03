using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;

namespace MEDICARE_HOSPITAL_MANGAMENT.Repositories
{
    /// <summary>
    /// Data access repository for doctor records, joined with departments and user accounts.
    /// </summary>
    public class DoctorRepository
    {
        public List<Doctor> GetAllDoctors(bool activeOnly = false)
        {
            var list = new List<Doctor>();
            string query = @"
                SELECT d.DoctorID, d.UserID, d.DepartmentID, d.DoctorCode, d.FirstName, d.LastName, 
                       d.Specialization, d.Phone, d.IsActive, 
                       dept.DepartmentName, u.Username
                FROM dbo.Doctors d
                INNER JOIN dbo.Departments dept ON d.DepartmentID = dept.DepartmentID
                INNER JOIN dbo.Users u ON d.UserID = u.UserID";

            if (activeOnly)
            {
                query += " WHERE d.IsActive = 1";
            }
            query += " ORDER BY d.DoctorID ASC;";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(MapDoctorFromReader(reader));
            }
            return list;
        }

        public Doctor? GetDoctorById(int doctorId)
        {
            const string query = @"
                SELECT d.DoctorID, d.UserID, d.DepartmentID, d.DoctorCode, d.FirstName, d.LastName, 
                       d.Specialization, d.Phone, d.IsActive, 
                       dept.DepartmentName, u.Username
                FROM dbo.Doctors d
                INNER JOIN dbo.Departments dept ON d.DepartmentID = dept.DepartmentID
                INNER JOIN dbo.Users u ON d.UserID = u.UserID
                WHERE d.DoctorID = @DoctorID;";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@DoctorID", SqlDbType.Int) { Value = doctorId });

            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return MapDoctorFromReader(reader);
            }
            return null;
        }

        public Doctor? GetDoctorByUserId(int userId)
        {
            const string query = @"
                SELECT d.DoctorID, d.UserID, d.DepartmentID, d.DoctorCode, d.FirstName, d.LastName, 
                       d.Specialization, d.Phone, d.IsActive, 
                       dept.DepartmentName, u.Username
                FROM dbo.Doctors d
                INNER JOIN dbo.Departments dept ON d.DepartmentID = dept.DepartmentID
                INNER JOIN dbo.Users u ON d.UserID = u.UserID
                WHERE d.UserID = @UserID;";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = userId });

            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return MapDoctorFromReader(reader);
            }
            return null;
        }

        public List<Doctor> GetDoctorsByDepartment(int departmentId)
        {
            var list = new List<Doctor>();
            const string query = @"
                SELECT d.DoctorID, d.UserID, d.DepartmentID, d.DoctorCode, d.FirstName, d.LastName, 
                       d.Specialization, d.Phone, d.IsActive, 
                       dept.DepartmentName, u.Username
                FROM dbo.Doctors d
                INNER JOIN dbo.Departments dept ON d.DepartmentID = dept.DepartmentID
                INNER JOIN dbo.Users u ON d.UserID = u.UserID
                WHERE d.DepartmentID = @DepartmentID AND d.IsActive = 1
                ORDER BY d.LastName, d.FirstName;";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@DepartmentID", SqlDbType.Int) { Value = departmentId });

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(MapDoctorFromReader(reader));
            }
            return list;
        }

        public int CreateDoctor(Doctor doctor)
        {
            const string query = @"
                INSERT INTO dbo.Doctors (UserID, DepartmentID, DoctorCode, FirstName, LastName, Specialization, Phone, IsActive)
                VALUES (@UserID, @DepartmentID, @DoctorCode, @FirstName, @LastName, @Specialization, @Phone, @IsActive);
                SELECT SCOPE_IDENTITY();";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = doctor.UserID });
            cmd.Parameters.Add(new SqlParameter("@DepartmentID", SqlDbType.Int) { Value = doctor.DepartmentID });
            cmd.Parameters.Add(new SqlParameter("@DoctorCode", SqlDbType.VarChar, 20) { Value = doctor.DoctorCode.Trim() });
            cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar, 50) { Value = doctor.FirstName.Trim() });
            cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 50) { Value = doctor.LastName.Trim() });
            cmd.Parameters.Add(new SqlParameter("@Specialization", SqlDbType.VarChar, 100) { Value = (object?)doctor.Specialization?.Trim() ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.VarChar, 20) { Value = (object?)doctor.Phone?.Trim() ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = doctor.IsActive });

            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public bool UpdateDoctor(Doctor doctor)
        {
            const string query = @"
                UPDATE dbo.Doctors 
                SET DepartmentID = @DepartmentID,
                    FirstName = @FirstName,
                    LastName = @LastName,
                    Specialization = @Specialization,
                    Phone = @Phone,
                    IsActive = @IsActive
                WHERE DoctorID = @DoctorID;";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add(new SqlParameter("@DepartmentID", SqlDbType.Int) { Value = doctor.DepartmentID });
            cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar, 50) { Value = doctor.FirstName.Trim() });
            cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 50) { Value = doctor.LastName.Trim() });
            cmd.Parameters.Add(new SqlParameter("@Specialization", SqlDbType.VarChar, 100) { Value = (object?)doctor.Specialization?.Trim() ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.VarChar, 20) { Value = (object?)doctor.Phone?.Trim() ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = doctor.IsActive });
            cmd.Parameters.Add(new SqlParameter("@DoctorID", SqlDbType.Int) { Value = doctor.DoctorID });

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Generates the next sequential DoctorCode (e.g. DOC001, DOC002).
        /// </summary>
        public string GenerateNextDoctorCode()
        {
            const string query = "SELECT MAX(DoctorID) FROM dbo.Doctors;";
            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);

            conn.Open();
            object? result = cmd.ExecuteScalar();
            int nextId = (result != null && result != DBNull.Value) ? Convert.ToInt32(result) + 1 : 1;
            return $"DOC{nextId:D3}";
        }

        /// <summary>
        /// Retrieves users with role 'Doctor' who do not yet have a doctor profile attached,
        /// or matches the current doctor's user when editing.
        /// </summary>
        public List<User> GetEligibleDoctorUserAccounts(int? currentDoctorUserId = null)
        {
            var list = new List<User>();
            const string query = @"
                SELECT u.UserID, u.Username, u.FullName, u.RoleID, r.RoleName, u.IsActive, u.CreatedAt, u.PasswordHash
                FROM dbo.Users u
                INNER JOIN dbo.Roles r ON u.RoleID = r.RoleID
                WHERE r.RoleName = 'Doctor' 
                  AND u.IsActive = 1
                  AND (u.UserID NOT IN (SELECT UserID FROM dbo.Doctors) OR u.UserID = @CurrentUserID)
                ORDER BY u.FullName ASC;";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@CurrentUserID", SqlDbType.Int) { Value = (object?)currentDoctorUserId ?? DBNull.Value });

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new User
                {
                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                    Username = reader.GetString(reader.GetOrdinal("Username")),
                    FullName = reader.GetString(reader.GetOrdinal("FullName")),
                    RoleID = reader.GetInt32(reader.GetOrdinal("RoleID")),
                    RoleName = reader.GetString(reader.GetOrdinal("RoleName")),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                });
            }
            return list;
        }

        private static Doctor MapDoctorFromReader(SqlDataReader reader)
        {
            return new Doctor
            {
                DoctorID = reader.GetInt32(reader.GetOrdinal("DoctorID")),
                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                DepartmentID = reader.GetInt32(reader.GetOrdinal("DepartmentID")),
                DoctorCode = reader.GetString(reader.GetOrdinal("DoctorCode")),
                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                Specialization = reader.IsDBNull(reader.GetOrdinal("Specialization")) ? null : reader.GetString(reader.GetOrdinal("Specialization")),
                Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString(reader.GetOrdinal("Phone")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                DepartmentName = reader.GetString(reader.GetOrdinal("DepartmentName")),
                Username = reader.GetString(reader.GetOrdinal("Username"))
            };
        }
    }
}
