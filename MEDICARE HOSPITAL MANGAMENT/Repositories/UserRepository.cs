using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;

namespace MEDICARE_HOSPITAL_MANGAMENT.Repositories
{
    /// <summary>
    /// Data Access Repository for Users and Roles using parameterized ADO.NET queries.
    /// </summary>
    public class UserRepository
    {
        /// <summary>
        /// Retrieves a user by username including the joined RoleName.
        /// </summary>
        public User? GetByUsername(string username)
        {
            const string query = @"
                SELECT u.UserID, u.Username, u.PasswordHash, u.FullName, u.RoleID, 
                       r.RoleName, u.IsActive, u.CreatedAt
                FROM dbo.Users u
                INNER JOIN dbo.Roles r ON u.RoleID = r.RoleID
                WHERE u.Username = @Username;";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.VarChar, 50) { Value = username.Trim() });

            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return MapUserFromReader(reader);
            }
            return null;
        }

        /// <summary>
        /// Retrieves a user by their unique primary key ID.
        /// </summary>
        public User? GetById(int userId)
        {
            const string query = @"
                SELECT u.UserID, u.Username, u.PasswordHash, u.FullName, u.RoleID, 
                       r.RoleName, u.IsActive, u.CreatedAt
                FROM dbo.Users u
                INNER JOIN dbo.Roles r ON u.RoleID = r.RoleID
                WHERE u.UserID = @UserID;";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = userId });

            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return MapUserFromReader(reader);
            }
            return null;
        }

        /// <summary>
        /// Retrieves all registered users in the system.
        /// </summary>
        public List<User> GetAllUsers()
        {
            var list = new List<User>();
            const string query = @"
                SELECT u.UserID, u.Username, u.PasswordHash, u.FullName, u.RoleID, 
                       r.RoleName, u.IsActive, u.CreatedAt
                FROM dbo.Users u
                INNER JOIN dbo.Roles r ON u.RoleID = r.RoleID
                ORDER BY u.UserID ASC;";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(MapUserFromReader(reader));
            }
            return list;
        }

        /// <summary>
        /// Retrieves all system roles.
        /// </summary>
        public List<Role> GetAllRoles()
        {
            var list = new List<Role>();
            const string query = "SELECT RoleID, RoleName, Description FROM dbo.Roles ORDER BY RoleID ASC;";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Role
                {
                    RoleID = reader.GetInt32(reader.GetOrdinal("RoleID")),
                    RoleName = reader.GetString(reader.GetOrdinal("RoleName")),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description"))
                });
            }
            return list;
        }

        /// <summary>
        /// Inserts a new user record and returns the newly generated UserID.
        /// </summary>
        public int CreateUser(User user)
        {
            const string query = @"
                INSERT INTO dbo.Users (Username, PasswordHash, FullName, RoleID, IsActive, CreatedAt)
                VALUES (@Username, @PasswordHash, @FullName, @RoleID, @IsActive, SYSDATETIME());
                SELECT SCOPE_IDENTITY();";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.VarChar, 50) { Value = user.Username.Trim() });
            cmd.Parameters.Add(new SqlParameter("@PasswordHash", SqlDbType.VarChar, 255) { Value = user.PasswordHash });
            cmd.Parameters.Add(new SqlParameter("@FullName", SqlDbType.VarChar, 100) { Value = user.FullName.Trim() });
            cmd.Parameters.Add(new SqlParameter("@RoleID", SqlDbType.Int) { Value = user.RoleID });
            cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = user.IsActive });

            conn.Open();
            object? result = cmd.ExecuteScalar();
            return Convert.ToInt32(result);
        }

        /// <summary>
        /// Updates a user's details (FullName, RoleID, IsActive).
        /// </summary>
        public bool UpdateUser(User user)
        {
            const string query = @"
                UPDATE dbo.Users 
                SET FullName = @FullName, 
                    RoleID = @RoleID, 
                    IsActive = @IsActive
                WHERE UserID = @UserID;";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add(new SqlParameter("@FullName", SqlDbType.VarChar, 100) { Value = user.FullName.Trim() });
            cmd.Parameters.Add(new SqlParameter("@RoleID", SqlDbType.Int) { Value = user.RoleID });
            cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = user.IsActive });
            cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = user.UserID });

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Updates a user's password hash.
        /// </summary>
        public bool UpdatePassword(int userId, string newPasswordHash)
        {
            const string query = @"
                UPDATE dbo.Users 
                SET PasswordHash = @PasswordHash
                WHERE UserID = @UserID;";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add(new SqlParameter("@PasswordHash", SqlDbType.VarChar, 255) { Value = newPasswordHash });
            cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = userId });

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Toggles a user's active status.
        /// </summary>
        public bool ToggleUserActiveStatus(int userId, bool isActive)
        {
            const string query = "UPDATE dbo.Users SET IsActive = @IsActive WHERE UserID = @UserID;";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = isActive });
            cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = userId });

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Checks whether a username is already registered to another account.
        /// </summary>
        public bool IsUsernameTaken(string username, int? excludeUserId = null)
        {
            const string query = @"
                SELECT COUNT(1) FROM dbo.Users 
                WHERE Username = @Username 
                  AND (@ExcludeID IS NULL OR UserID != @ExcludeID);";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.VarChar, 50) { Value = username.Trim() });
            cmd.Parameters.Add(new SqlParameter("@ExcludeID", SqlDbType.Int) { Value = (object?)excludeUserId ?? DBNull.Value });

            conn.Open();
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }

        /// <summary>
        /// Counts how many active Administrators currently exist.
        /// Used to prevent accidentally deactivating or deleting the last admin.
        /// </summary>
        public int GetActiveAdminCount()
        {
            const string query = @"
                SELECT COUNT(1) 
                FROM dbo.Users u
                INNER JOIN dbo.Roles r ON u.RoleID = r.RoleID
                WHERE r.RoleName = 'Administrator' AND u.IsActive = 1;";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);

            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        private static User MapUserFromReader(SqlDataReader reader)
        {
            return new User
            {
                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                Username = reader.GetString(reader.GetOrdinal("Username")),
                PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                FullName = reader.GetString(reader.GetOrdinal("FullName")),
                RoleID = reader.GetInt32(reader.GetOrdinal("RoleID")),
                RoleName = reader.GetString(reader.GetOrdinal("RoleName")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
            };
        }
    }
}
