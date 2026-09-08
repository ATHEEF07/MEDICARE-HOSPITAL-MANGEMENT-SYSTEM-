using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;

namespace MEDICARE_HOSPITAL_MANGAMENT.Repositories
{
    /// <summary>
    /// Data Access Repository for Payments.
    /// Executes payments and updates Bill status atomically inside an ADO.NET transaction.
    /// </summary>
    public class PaymentRepository
    {
        // ─────────────────────────────────────────────────────────────────────────
        // Read Queries
        // ─────────────────────────────────────────────────────────────────────────

        private const string SelectBaseQuery = @"
            SELECT pay.PaymentID, pay.BillID, pay.PaymentDate, pay.Amount,
                   pay.PaymentMethod, pay.ReferenceNo, pay.ReceivedByUserID,
                   u.FullName AS ReceivedByUserName,
                   p.FirstName + ' ' + p.LastName AS PatientName,
                   p.PatientCode,
                   b.Description AS BillDescription,
                   b.TotalAmount AS BillTotalAmount,
                   (b.TotalAmount - ISNULL((SELECT SUM(p2.Amount) FROM dbo.Payments p2 WHERE p2.BillID = b.BillID), 0)) AS RemainingBalanceAfterPayment
            FROM dbo.Payments pay
            INNER JOIN dbo.Users u ON pay.ReceivedByUserID = u.UserID
            INNER JOIN dbo.Bills b ON pay.BillID = b.BillID
            INNER JOIN dbo.Patients p ON b.PatientID = p.PatientID";

        /// <summary>
        /// Retrieves all payments recorded against a specific bill.
        /// </summary>
        public List<Payment> GetPaymentsByBillId(int billId)
        {
            var list = new List<Payment>();
            string query = $"{SelectBaseQuery} WHERE pay.BillID = @BillID ORDER BY pay.PaymentDate ASC;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@BillID", billId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapFromReader(reader));

            return list;
        }

        /// <summary>
        /// Retrieves a single payment receipt by primary key.
        /// </summary>
        public Payment? GetPaymentById(int paymentId)
        {
            string query = $"{SelectBaseQuery} WHERE pay.PaymentID = @PaymentID;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PaymentID", paymentId);

            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapFromReader(reader) : null;
        }

        /// <summary>
        /// Calculates the cumulative sum of payments recorded for a bill.
        /// </summary>
        public decimal GetTotalPaidForBill(int billId)
        {
            const string query = "SELECT ISNULL(SUM(Amount), 0.00) FROM dbo.Payments WHERE BillID = @BillID;";
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@BillID", billId);

            object? res = cmd.ExecuteScalar();
            return res != null && res != DBNull.Value ? Convert.ToDecimal(res) : 0m;
        }

        /// <summary>
        /// Retrieves recent payment transactions across all bills.
        /// </summary>
        public List<Payment> GetAllPayments(int limit = 100)
        {
            var list = new List<Payment>();
            string query = $@"
                SELECT TOP ({limit}) pay.PaymentID, pay.BillID, pay.PaymentDate, pay.Amount,
                       pay.PaymentMethod, pay.ReferenceNo, pay.ReceivedByUserID,
                       u.FullName AS ReceivedByUserName,
                       p.FirstName + ' ' + p.LastName AS PatientName,
                       p.PatientCode,
                       b.Description AS BillDescription,
                       b.TotalAmount AS BillTotalAmount,
                       (b.TotalAmount - ISNULL((SELECT SUM(p2.Amount) FROM dbo.Payments p2 WHERE p2.BillID = b.BillID), 0)) AS RemainingBalanceAfterPayment
                FROM dbo.Payments pay
                INNER JOIN dbo.Users u ON pay.ReceivedByUserID = u.UserID
                INNER JOIN dbo.Bills b ON pay.BillID = b.BillID
                INNER JOIN dbo.Patients p ON b.PatientID = p.PatientID
                ORDER BY pay.PaymentDate DESC;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapFromReader(reader));

            return list;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Transactional Write Operations
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Records a payment and automatically updates the linked Bill status ('PartiallyPaid' or 'Paid')
        /// atomically inside a database transaction.
        /// </summary>
        public int RecordPayment(Payment payment)
        {
            const string insertSql = @"
                INSERT INTO dbo.Payments
                    (BillID, PaymentDate, Amount, PaymentMethod, ReferenceNo, ReceivedByUserID)
                VALUES
                    (@BillID, @PaymentDate, @Amount, @PaymentMethod, @ReferenceNo, @ReceivedByUserID);
                SELECT SCOPE_IDENTITY();";

            const string sumPaymentsSql = @"
                SELECT ISNULL(SUM(Amount), 0.00)
                FROM dbo.Payments
                WHERE BillID = @BillID;";

            const string getBillTotalSql = @"
                SELECT TotalAmount
                FROM dbo.Bills WITH (UPDLOCK)
                WHERE BillID = @BillID;";

            const string updateBillStatusSql = @"
                UPDATE dbo.Bills SET
                    Status = @Status
                WHERE BillID = @BillID;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var trans = conn.BeginTransaction();

            try
            {
                // 1. Verify bill and get total amount
                decimal totalAmount;
                using (var cmdBill = new SqlCommand(getBillTotalSql, conn, trans))
                {
                    cmdBill.Parameters.AddWithValue("@BillID", payment.BillID);
                    object? res = cmdBill.ExecuteScalar();
                    if (res == null || res == DBNull.Value)
                        throw new InvalidOperationException($"Bill with ID {payment.BillID} not found.");
                    totalAmount = Convert.ToDecimal(res);
                }

                // 2. Insert Payment record
                int newPaymentId;
                using (var cmdInsert = new SqlCommand(insertSql, conn, trans))
                {
                    cmdInsert.Parameters.AddWithValue("@BillID", payment.BillID);
                    cmdInsert.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                    cmdInsert.Parameters.AddWithValue("@Amount", payment.Amount);
                    cmdInsert.Parameters.AddWithValue("@PaymentMethod", payment.PaymentMethod);
                    cmdInsert.Parameters.AddWithValue("@ReferenceNo", (object?)payment.ReferenceNo?.Trim() ?? DBNull.Value);
                    cmdInsert.Parameters.AddWithValue("@ReceivedByUserID", payment.ReceivedByUserID);

                    object? scalar = cmdInsert.ExecuteScalar();
                    newPaymentId = scalar != null ? Convert.ToInt32(scalar) : 0;
                }

                if (newPaymentId <= 0)
                    throw new InvalidOperationException("Failed to record payment.");

                // 3. Compute total paid so far including this payment
                decimal totalPaid;
                using (var cmdSum = new SqlCommand(sumPaymentsSql, conn, trans))
                {
                    cmdSum.Parameters.AddWithValue("@BillID", payment.BillID);
                    totalPaid = Convert.ToDecimal(cmdSum.ExecuteScalar());
                }

                // 4. Determine new bill status
                string newStatus = (totalPaid >= totalAmount) ? "Paid" : "PartiallyPaid";

                using (var cmdUpdate = new SqlCommand(updateBillStatusSql, conn, trans))
                {
                    cmdUpdate.Parameters.AddWithValue("@Status", newStatus);
                    cmdUpdate.Parameters.AddWithValue("@BillID", payment.BillID);
                    cmdUpdate.ExecuteNonQuery();
                }

                trans.Commit();
                payment.PaymentID = newPaymentId;
                return newPaymentId;
            }
            catch
            {
                trans.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Generates the next sequential payment receipt reference number (e.g. RCP-10001, RCP-10002).
        /// </summary>
        public string GenerateNextReferenceNo()
        {
            const string query = @"
                SELECT TOP 1 ReferenceNo
                FROM dbo.Payments
                WHERE ReferenceNo LIKE 'RCP-%'
                ORDER BY ReferenceNo DESC;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            object? res = cmd.ExecuteScalar();

            if (res != null && res != DBNull.Value)
            {
                string lastRef = res.ToString() ?? "";
                string numPart = lastRef.Replace("RCP-", "");
                if (int.TryParse(numPart, out int num))
                    return $"RCP-{(num + 1):D5}";
            }

            // Fallback based on count
            const string countQuery = "SELECT COUNT(1) FROM dbo.Payments;";
            using var countCmd = new SqlCommand(countQuery, conn);
            int count = Convert.ToInt32(countCmd.ExecuteScalar());
            return $"RCP-{(10001 + count):D5}";
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Mapping Helper
        // ─────────────────────────────────────────────────────────────────────────

        private static Payment MapFromReader(SqlDataReader r)
        {
            return new Payment
            {
                PaymentID            = r.GetInt32(r.GetOrdinal("PaymentID")),
                BillID               = r.GetInt32(r.GetOrdinal("BillID")),
                PaymentDate          = r.GetDateTime(r.GetOrdinal("PaymentDate")),
                Amount               = r.GetDecimal(r.GetOrdinal("Amount")),
                PaymentMethod        = r.GetString(r.GetOrdinal("PaymentMethod")),
                ReferenceNo          = r.IsDBNull(r.GetOrdinal("ReferenceNo")) ? null : r.GetString(r.GetOrdinal("ReferenceNo")),
                ReceivedByUserID     = r.GetInt32(r.GetOrdinal("ReceivedByUserID")),
                ReceivedByUserName   = r.GetString(r.GetOrdinal("ReceivedByUserName")),
                PatientName          = r.GetString(r.GetOrdinal("PatientName")),
                PatientCode          = r.GetString(r.GetOrdinal("PatientCode")),
                BillDescription      = r.IsDBNull(r.GetOrdinal("BillDescription")) ? string.Empty : r.GetString(r.GetOrdinal("BillDescription")),
                BillTotalAmount      = r.GetDecimal(r.GetOrdinal("BillTotalAmount")),
                RemainingBalanceAfterPayment = r.GetDecimal(r.GetOrdinal("RemainingBalanceAfterPayment"))
            };
        }
    }
}
