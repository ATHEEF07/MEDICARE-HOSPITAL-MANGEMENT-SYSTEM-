using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;

namespace MEDICARE_HOSPITAL_MANGAMENT.Repositories
{
    /// <summary>
    /// Data Access Repository for Patient Billing and Invoicing.
    /// Uses parameterized queries and supports SqlTransaction integration.
    /// </summary>
    public class BillRepository
    {
        // ─────────────────────────────────────────────────────────────────────────
        // Read Queries
        // ─────────────────────────────────────────────────────────────────────────

        private const string SelectBaseQuery = @"
            SELECT b.BillID, b.PatientID, b.AppointmentID, b.BillDate, b.Description,
                   b.Subtotal, b.Discount, b.TotalAmount, b.Status,
                   p.PatientCode, p.FirstName + ' ' + p.LastName AS PatientName,
                   p.Phone AS PatientPhone,
                   ISNULL((SELECT SUM(Amount) FROM dbo.Payments WHERE BillID = b.BillID), 0.00) AS TotalPaid
            FROM dbo.Bills b
            INNER JOIN dbo.Patients p ON b.PatientID = p.PatientID";

        /// <summary>
        /// Retrieves a bill by its unique BillID.
        /// </summary>
        public Bill? GetBillById(int billId)
        {
            string query = $"{SelectBaseQuery} WHERE b.BillID = @BillID;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@BillID", billId);

            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapFromReader(reader) : null;
        }

        /// <summary>
        /// Retrieves all bills associated with a given patient.
        /// </summary>
        public List<Bill> GetBillsByPatient(int patientId)
        {
            var list = new List<Bill>();
            string query = $"{SelectBaseQuery} WHERE b.PatientID = @PatientID ORDER BY b.BillDate DESC;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PatientID", patientId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapFromReader(reader));

            return list;
        }

        /// <summary>
        /// Retrieves bills that have an unpaid balance (Pending or PartiallyPaid).
        /// </summary>
        public List<Bill> GetPendingBills()
        {
            var list = new List<Bill>();
            string query = $"{SelectBaseQuery} WHERE b.Status IN ('Pending', 'PartiallyPaid') ORDER BY b.BillDate ASC;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapFromReader(reader));

            return list;
        }

        /// <summary>
        /// Retrieves recent bills with a configurable limit.
        /// </summary>
        public List<Bill> GetAllBills(int limit = 100)
        {
            var list = new List<Bill>();
            string query = $@"
                SELECT TOP ({limit}) b.BillID, b.PatientID, b.AppointmentID, b.BillDate, b.Description,
                       b.Subtotal, b.Discount, b.TotalAmount, b.Status,
                       p.PatientCode, p.FirstName + ' ' + p.LastName AS PatientName,
                       p.Phone AS PatientPhone,
                       ISNULL((SELECT SUM(Amount) FROM dbo.Payments WHERE BillID = b.BillID), 0.00) AS TotalPaid
                FROM dbo.Bills b
                INNER JOIN dbo.Patients p ON b.PatientID = p.PatientID
                ORDER BY b.BillDate DESC;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapFromReader(reader));

            return list;
        }

        /// <summary>
        /// Searches bills by patient name, patient code, or bill ID.
        /// </summary>
        public List<Bill> SearchBills(string term)
        {
            var list = new List<Bill>();
            string query = $@"{SelectBaseQuery}
                WHERE p.FirstName LIKE @Term OR p.LastName LIKE @Term
                   OR p.PatientCode LIKE @Term OR CAST(b.BillID AS VARCHAR) = @ExactTerm
                ORDER BY b.BillDate DESC;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Term", $"%{term.Trim()}%");
            cmd.Parameters.AddWithValue("@ExactTerm", term.Trim());

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapFromReader(reader));

            return list;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Write Operations
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Creates a new bill and returns the generated BillID.
        /// </summary>
        public int CreateBill(Bill bill)
        {
            const string query = @"
                INSERT INTO dbo.Bills
                    (PatientID, AppointmentID, BillDate, Description, Subtotal, Discount, TotalAmount, Status)
                VALUES
                    (@PatientID, @AppointmentID, @BillDate, @Description, @Subtotal, @Discount, @TotalAmount, @Status);
                SELECT SCOPE_IDENTITY();";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PatientID", bill.PatientID);
            cmd.Parameters.AddWithValue("@AppointmentID", (object?)bill.AppointmentID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@BillDate", bill.BillDate);
            cmd.Parameters.AddWithValue("@Description", (object?)bill.Description?.Trim() ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Subtotal", bill.Subtotal);
            cmd.Parameters.AddWithValue("@Discount", bill.Discount);
            cmd.Parameters.AddWithValue("@TotalAmount", bill.TotalAmount);
            cmd.Parameters.AddWithValue("@Status", bill.Status);

            object? res = cmd.ExecuteScalar();
            int newId = res != null ? Convert.ToInt32(res) : 0;
            bill.BillID = newId;
            return newId;
        }

        /// <summary>
        /// Updates the status of a bill ('Pending', 'PartiallyPaid', 'Paid', 'Cancelled').
        /// Supports participation in an external transaction.
        /// </summary>
        public bool UpdateBillStatus(int billId, string status, SqlTransaction? transaction = null)
        {
            const string query = "UPDATE dbo.Bills SET Status = @Status WHERE BillID = @BillID;";
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
                cmd.Parameters.AddWithValue("@BillID", billId);
                cmd.Parameters.AddWithValue("@Status", status);
                return cmd.ExecuteNonQuery() > 0;
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

        // ─────────────────────────────────────────────────────────────────────────
        // Mapping Helper
        // ─────────────────────────────────────────────────────────────────────────

        private static Bill MapFromReader(SqlDataReader r)
        {
            return new Bill
            {
                BillID        = r.GetInt32(r.GetOrdinal("BillID")),
                PatientID     = r.GetInt32(r.GetOrdinal("PatientID")),
                AppointmentID = r.IsDBNull(r.GetOrdinal("AppointmentID")) ? null : r.GetInt32(r.GetOrdinal("AppointmentID")),
                BillDate      = r.GetDateTime(r.GetOrdinal("BillDate")),
                Description   = r.IsDBNull(r.GetOrdinal("Description")) ? null : r.GetString(r.GetOrdinal("Description")),
                Subtotal      = r.GetDecimal(r.GetOrdinal("Subtotal")),
                Discount      = r.GetDecimal(r.GetOrdinal("Discount")),
                TotalAmount   = r.GetDecimal(r.GetOrdinal("TotalAmount")),
                Status        = r.GetString(r.GetOrdinal("Status")),
                PatientCode   = r.GetString(r.GetOrdinal("PatientCode")),
                PatientName   = r.GetString(r.GetOrdinal("PatientName")),
                PatientPhone  = r.GetString(r.GetOrdinal("PatientPhone")),
                TotalPaid     = r.GetDecimal(r.GetOrdinal("TotalPaid"))
            };
        }
    }
}
