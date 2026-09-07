using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;

namespace MEDICARE_HOSPITAL_MANGAMENT.Repositories
{
    /// <summary>
    /// Data Access Repository for Pharmacy Medicines / Stock Inventory.
    /// Uses strictly parameterized SQL queries.
    /// </summary>
    public class MedicineRepository
    {
        // ─────────────────────────────────────────────────────────────────────────
        // Read Queries
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Retrieves all medicines, optionally filtering active only.
        /// </summary>
        public List<Medicine> GetAllMedicines(bool activeOnly = true)
        {
            var list = new List<Medicine>();
            string query = @"
                SELECT MedicineID, MedicineCode, MedicineName, Category, Unit,
                       UnitPrice, StockQuantity, ReorderLevel, ExpiryDate, IsActive
                FROM dbo.Medicines
                WHERE (@ActiveOnly = 0 OR IsActive = 1)
                ORDER BY MedicineName ASC;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ActiveOnly", activeOnly);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapFromReader(reader));

            return list;
        }

        /// <summary>
        /// Retrieves medicines whose stock is at or below their reorder threshold.
        /// </summary>
        public List<Medicine> GetLowStockMedicines()
        {
            var list = new List<Medicine>();
            const string query = @"
                SELECT MedicineID, MedicineCode, MedicineName, Category, Unit,
                       UnitPrice, StockQuantity, ReorderLevel, ExpiryDate, IsActive
                FROM dbo.Medicines
                WHERE StockQuantity <= ReorderLevel AND IsActive = 1
                ORDER BY StockQuantity ASC, MedicineName ASC;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapFromReader(reader));

            return list;
        }

        /// <summary>
        /// Retrieves a medicine by primary key.
        /// </summary>
        public Medicine? GetById(int medicineId)
        {
            const string query = @"
                SELECT MedicineID, MedicineCode, MedicineName, Category, Unit,
                       UnitPrice, StockQuantity, ReorderLevel, ExpiryDate, IsActive
                FROM dbo.Medicines
                WHERE MedicineID = @MedicineID;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MedicineID", medicineId);

            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapFromReader(reader) : null;
        }

        /// <summary>
        /// Retrieves a medicine by unique medicine code.
        /// </summary>
        public Medicine? GetByCode(string code)
        {
            const string query = @"
                SELECT MedicineID, MedicineCode, MedicineName, Category, Unit,
                       UnitPrice, StockQuantity, ReorderLevel, ExpiryDate, IsActive
                FROM dbo.Medicines
                WHERE MedicineCode = @MedicineCode;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MedicineCode", code.Trim());

            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapFromReader(reader) : null;
        }

        /// <summary>
        /// Searches medicines by name, code, or category.
        /// </summary>
        public List<Medicine> SearchMedicines(string searchTerm, bool activeOnly = true)
        {
            var list = new List<Medicine>();
            const string query = @"
                SELECT MedicineID, MedicineCode, MedicineName, Category, Unit,
                       UnitPrice, StockQuantity, ReorderLevel, ExpiryDate, IsActive
                FROM dbo.Medicines
                WHERE (@ActiveOnly = 0 OR IsActive = 1)
                  AND (MedicineName LIKE @Term OR MedicineCode LIKE @Term OR Category LIKE @Term)
                ORDER BY MedicineName ASC;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ActiveOnly", activeOnly);
            cmd.Parameters.AddWithValue("@Term", $"%{searchTerm.Trim()}%");

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapFromReader(reader));

            return list;
        }

        /// <summary>
        /// Retrieves distinct medicine categories.
        /// </summary>
        public List<string> GetCategories()
        {
            var list = new List<string>();
            const string query = @"
                SELECT DISTINCT Category
                FROM dbo.Medicines
                WHERE Category IS NOT NULL AND LTRIM(RTRIM(Category)) <> ''
                ORDER BY Category ASC;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(reader.GetString(0));

            return list;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Write Operations
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Inserts a new medicine into the inventory and returns generated MedicineID.
        /// </summary>
        public int CreateMedicine(Medicine med)
        {
            const string query = @"
                INSERT INTO dbo.Medicines
                    (MedicineCode, MedicineName, Category, Unit, UnitPrice, StockQuantity, ReorderLevel, ExpiryDate, IsActive)
                VALUES
                    (@MedicineCode, @MedicineName, @Category, @Unit, @UnitPrice, @StockQuantity, @ReorderLevel, @ExpiryDate, @IsActive);
                SELECT SCOPE_IDENTITY();";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            BindParameters(cmd, med);

            object? res = cmd.ExecuteScalar();
            return res != null ? Convert.ToInt32(res) : 0;
        }

        /// <summary>
        /// Updates an existing medicine record.
        /// </summary>
        public bool UpdateMedicine(Medicine med)
        {
            const string query = @"
                UPDATE dbo.Medicines SET
                    MedicineCode  = @MedicineCode,
                    MedicineName  = @MedicineName,
                    Category      = @Category,
                    Unit          = @Unit,
                    UnitPrice     = @UnitPrice,
                    StockQuantity = @StockQuantity,
                    ReorderLevel  = @ReorderLevel,
                    ExpiryDate    = @ExpiryDate,
                    IsActive      = @IsActive
                WHERE MedicineID  = @MedicineID;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MedicineID", med.MedicineID);
            BindParameters(cmd, med);

            return cmd.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Increments or decrements medicine stock atomically.
        /// Supports participation in an external SqlTransaction.
        /// </summary>
        public bool UpdateStock(int medicineId, int quantityChange, SqlTransaction? transaction = null)
        {
            const string query = @"
                UPDATE dbo.Medicines SET
                    StockQuantity = StockQuantity + @QtyChange
                WHERE MedicineID = @MedicineID
                  AND (StockQuantity + @QtyChange) >= 0;";

            SqlCommand cmd;
            SqlConnection? localConn = null;

            if (transaction != null)
            {
                cmd = new SqlCommand(query, transaction.Connection, transaction);
            }
            else
            {
                localConn = DatabaseHelper.GetConnection();
                localConn.Open();
                cmd = new SqlCommand(query, localConn);
            }

            try
            {
                cmd.Parameters.AddWithValue("@MedicineID", medicineId);
                cmd.Parameters.AddWithValue("@QtyChange", quantityChange);
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
            finally
            {
                if (localConn != null)
                {
                    cmd.Dispose();
                    localConn.Dispose();
                }
            }
        }

        /// <summary>
        /// Deactivates a medicine from active use.
        /// </summary>
        public bool DeactivateMedicine(int medicineId)
        {
            const string query = "UPDATE dbo.Medicines SET IsActive = 0 WHERE MedicineID = @MedicineID;";
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MedicineID", medicineId);
            return cmd.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Generates the next sequential medicine code (e.g. MED016).
        /// </summary>
        public string GenerateNextMedicineCode()
        {
            const string query = @"
                SELECT TOP 1 MedicineCode
                FROM dbo.Medicines
                WHERE MedicineCode LIKE 'MED%'
                ORDER BY MedicineCode DESC;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            object? res = cmd.ExecuteScalar();

            if (res != null && res != DBNull.Value)
            {
                string lastCode = res.ToString() ?? "";
                if (lastCode.Length > 3 && int.TryParse(lastCode.Substring(3), out int num))
                    return $"MED{(num + 1):D3}";
            }

            // Fallback
            const string countQuery = "SELECT COUNT(1) FROM dbo.Medicines;";
            using var countCmd = new SqlCommand(countQuery, conn);
            int count = Convert.ToInt32(countCmd.ExecuteScalar());
            return $"MED{(count + 1):D3}";
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Mapping & Binding Helpers
        // ─────────────────────────────────────────────────────────────────────────

        private static Medicine MapFromReader(SqlDataReader r)
        {
            return new Medicine
            {
                MedicineID    = r.GetInt32(r.GetOrdinal("MedicineID")),
                MedicineCode  = r.GetString(r.GetOrdinal("MedicineCode")),
                MedicineName  = r.GetString(r.GetOrdinal("MedicineName")),
                Category      = r.IsDBNull(r.GetOrdinal("Category")) ? null : r.GetString(r.GetOrdinal("Category")),
                Unit          = r.GetString(r.GetOrdinal("Unit")),
                UnitPrice     = r.GetDecimal(r.GetOrdinal("UnitPrice")),
                StockQuantity = r.GetInt32(r.GetOrdinal("StockQuantity")),
                ReorderLevel  = r.GetInt32(r.GetOrdinal("ReorderLevel")),
                ExpiryDate    = r.IsDBNull(r.GetOrdinal("ExpiryDate")) ? null : r.GetDateTime(r.GetOrdinal("ExpiryDate")),
                IsActive      = r.GetBoolean(r.GetOrdinal("IsActive"))
            };
        }

        private static void BindParameters(SqlCommand cmd, Medicine med)
        {
            cmd.Parameters.AddWithValue("@MedicineCode", med.MedicineCode.Trim().ToUpper());
            cmd.Parameters.AddWithValue("@MedicineName", med.MedicineName.Trim());
            cmd.Parameters.AddWithValue("@Category", (object?)med.Category?.Trim() ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Unit", med.Unit.Trim());
            cmd.Parameters.AddWithValue("@UnitPrice", med.UnitPrice);
            cmd.Parameters.AddWithValue("@StockQuantity", med.StockQuantity);
            cmd.Parameters.AddWithValue("@ReorderLevel", med.ReorderLevel);
            cmd.Parameters.AddWithValue("@ExpiryDate", (object?)med.ExpiryDate?.Date ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IsActive", med.IsActive);
        }
    }
}
