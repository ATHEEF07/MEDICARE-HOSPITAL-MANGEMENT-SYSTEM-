using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;

namespace MEDICARE_HOSPITAL_MANGAMENT.Repositories
{
    /// <summary>
    /// Data access repository for hospital departments.
    /// </summary>
    public class DepartmentRepository
    {
        public List<Department> GetAllDepartments(bool activeOnly = false)
        {
            var list = new List<Department>();
            string query = "SELECT DepartmentID, DepartmentName, Description, IsActive FROM dbo.Departments";
            if (activeOnly)
            {
                query += " WHERE IsActive = 1";
            }
            query += " ORDER BY DepartmentName ASC;";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Department
                {
                    DepartmentID = reader.GetInt32(reader.GetOrdinal("DepartmentID")),
                    DepartmentName = reader.GetString(reader.GetOrdinal("DepartmentName")),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                });
            }
            return list;
        }

        public Department? GetDepartmentById(int departmentId)
        {
            const string query = @"
                SELECT DepartmentID, DepartmentName, Description, IsActive 
                FROM dbo.Departments 
                WHERE DepartmentID = @DepartmentID;";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@DepartmentID", SqlDbType.Int) { Value = departmentId });

            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Department
                {
                    DepartmentID = reader.GetInt32(reader.GetOrdinal("DepartmentID")),
                    DepartmentName = reader.GetString(reader.GetOrdinal("DepartmentName")),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                };
            }
            return null;
        }

        public int CreateDepartment(Department dept)
        {
            const string query = @"
                INSERT INTO dbo.Departments (DepartmentName, Description, IsActive)
                VALUES (@DepartmentName, @Description, @IsActive);
                SELECT SCOPE_IDENTITY();";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add(new SqlParameter("@DepartmentName", SqlDbType.VarChar, 100) { Value = dept.DepartmentName.Trim() });
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 200) { Value = (object?)dept.Description?.Trim() ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = dept.IsActive });

            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public bool UpdateDepartment(Department dept)
        {
            const string query = @"
                UPDATE dbo.Departments 
                SET DepartmentName = @DepartmentName,
                    Description = @Description,
                    IsActive = @IsActive
                WHERE DepartmentID = @DepartmentID;";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add(new SqlParameter("@DepartmentName", SqlDbType.VarChar, 100) { Value = dept.DepartmentName.Trim() });
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 200) { Value = (object?)dept.Description?.Trim() ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = dept.IsActive });
            cmd.Parameters.Add(new SqlParameter("@DepartmentID", SqlDbType.Int) { Value = dept.DepartmentID });

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool IsDepartmentNameTaken(string name, int? excludeId = null)
        {
            const string query = @"
                SELECT COUNT(1) FROM dbo.Departments 
                WHERE DepartmentName = @DepartmentName 
                  AND (@ExcludeID IS NULL OR DepartmentID != @ExcludeID);";

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add(new SqlParameter("@DepartmentName", SqlDbType.VarChar, 100) { Value = name.Trim() });
            cmd.Parameters.Add(new SqlParameter("@ExcludeID", SqlDbType.Int) { Value = (object?)excludeId ?? DBNull.Value });

            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
    }
}
