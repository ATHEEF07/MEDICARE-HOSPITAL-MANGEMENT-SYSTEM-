using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace MEDICARE_HOSPITAL_MANGAMENT.Helpers
{
    /// <summary>
    /// Centralized database helper for SQL Server connections and parameterized execution.
    /// Follows strict ADO.NET standards for the 3-layer architecture.
    /// </summary>
    public static class DatabaseHelper
    {
        // Default connection string - prioritizes LocalDB, with fallback to localhost and SQLEXPRESS
        private static string _connectionString =
            "Server=(localdb)\\MSSQLLocalDB;Database=MediCareDB;Trusted_Connection=True;TrustServerCertificate=True;";

        private static readonly string[] _candidateConnectionStrings = new[]
        {
            "Server=(localdb)\\MSSQLLocalDB;Database=MediCareDB;Trusted_Connection=True;TrustServerCertificate=True;",
            "Server=localhost;Database=MediCareDB;Trusted_Connection=True;TrustServerCertificate=True;",
            "Server=.\\SQLEXPRESS;Database=MediCareDB;Trusted_Connection=True;TrustServerCertificate=True;",
            "Server=127.0.0.1;Database=MediCareDB;Trusted_Connection=True;TrustServerCertificate=True;"
        };

        /// <summary>
        /// Gets or sets the active database connection string.
        /// </summary>
        public static string ConnectionString
        {
            get => _connectionString;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _connectionString = value;
                }
            }
        }

        /// <summary>
        /// Creates and returns a new closed SqlConnection instance.
        /// </summary>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        /// <summary>
        /// Tests whether the configured database can be reached and opened.
        /// Automatically discovers and binds to the active local SQL Server instance if available.
        /// </summary>
        /// <param name="errorMessage">Detailed error message if connection fails.</param>
        /// <returns>True if connection succeeded; otherwise, false.</returns>
        public static bool TestConnection(out string errorMessage)
        {
            errorMessage = string.Empty;

            // First test current connection string
            if (TryConnect(_connectionString, out errorMessage))
            {
                return true;
            }

            // Fallback: Probe candidate connection strings
            foreach (var candidate in _candidateConnectionStrings)
            {
                if (candidate == _connectionString) continue;

                if (TryConnect(candidate, out _))
                {
                    _connectionString = candidate;
                    errorMessage = string.Empty;
                    return true;
                }
            }

            return false;
        }

        private static bool TryConnect(string connStr, out string error)
        {
            error = string.Empty;
            try
            {
                var builder = new SqlConnectionStringBuilder(connStr)
                {
                    ConnectTimeout = 2
                };
                using var conn = new SqlConnection(builder.ConnectionString);
                conn.Open();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Tests whether the configured database can be reached and opened (convenience overload).
        /// </summary>
        public static bool TestConnection() => TestConnection(out _);

        /// <summary>
        /// Executes a non-query command (INSERT, UPDATE, DELETE) with parameters.
        /// </summary>
        public static int ExecuteNonQuery(string query, SqlParameter[]? parameters = null, CommandType commandType = CommandType.Text)
        {
            using var conn = GetConnection();
            using var cmd = new SqlCommand(query, conn) { CommandType = commandType };

            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            conn.Open();
            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Executes a scalar query (SELECT COUNT, SCOPE_IDENTITY) with parameters.
        /// </summary>
        public static object? ExecuteScalar(string query, SqlParameter[]? parameters = null, CommandType commandType = CommandType.Text)
        {
            using var conn = GetConnection();
            using var cmd = new SqlCommand(query, conn) { CommandType = commandType };

            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            conn.Open();
            return cmd.ExecuteScalar();
        }

        /// <summary>
        /// Executes a query and returns results in a DataTable.
        /// </summary>
        public static DataTable ExecuteDataTable(string query, SqlParameter[]? parameters = null, CommandType commandType = CommandType.Text)
        {
            var dt = new DataTable();
            using var conn = GetConnection();
            using var cmd = new SqlCommand(query, conn) { CommandType = commandType };

            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            using var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
    }
}
