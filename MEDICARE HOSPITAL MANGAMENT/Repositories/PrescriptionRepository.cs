using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;

namespace MEDICARE_HOSPITAL_MANGAMENT.Repositories
{
    /// <summary>
    /// Data Access Repository for Prescriptions and Prescription Line Items.
    /// Employs multi-table ADO.NET SqlTransactions for atomic writes and dispensing.
    /// </summary>
    public class PrescriptionRepository
    {
        // ─────────────────────────────────────────────────────────────────────────
        // Read Queries
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Retrieves a prescription with all its line items and joined doctor/patient metadata.
        /// </summary>
        public Prescription? GetPrescriptionWithItems(int prescriptionId)
        {
            const string headerQuery = @"
                SELECT rx.PrescriptionID, rx.PatientID, rx.DoctorID, rx.MedicalRecordID,
                       rx.PrescriptionDate, rx.Status, rx.Notes,
                       p.PatientCode, p.FirstName + ' ' + p.LastName AS PatientName,
                       'Dr. ' + d.FirstName + ' ' + d.LastName AS DoctorName,
                       dep.DepartmentName
                FROM dbo.Prescriptions rx
                INNER JOIN dbo.Patients p ON rx.PatientID = p.PatientID
                INNER JOIN dbo.Doctors d ON rx.DoctorID = d.DoctorID
                INNER JOIN dbo.Departments dep ON d.DepartmentID = dep.DepartmentID
                WHERE rx.PrescriptionID = @PrescriptionID;";

            const string itemsQuery = @"
                SELECT pi.PrescriptionItemID, pi.PrescriptionID, pi.MedicineID,
                       pi.Quantity, pi.Dosage, pi.Frequency, pi.DurationDays, pi.Instructions,
                       m.MedicineCode, m.MedicineName, ISNULL(m.Category, '') AS Category,
                       m.Unit, m.UnitPrice, m.StockQuantity AS AvailableStock
                FROM dbo.PrescriptionItems pi
                INNER JOIN dbo.Medicines m ON pi.MedicineID = m.MedicineID
                WHERE pi.PrescriptionID = @PrescriptionID
                ORDER BY pi.PrescriptionItemID ASC;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();

            Prescription? rx = null;
            using (var cmdHeader = new SqlCommand(headerQuery, conn))
            {
                cmdHeader.Parameters.AddWithValue("@PrescriptionID", prescriptionId);
                using var reader = cmdHeader.ExecuteReader();
                if (reader.Read())
                {
                    rx = MapHeaderFromReader(reader);
                }
            }

            if (rx == null) return null;

            using (var cmdItems = new SqlCommand(itemsQuery, conn))
            {
                cmdItems.Parameters.AddWithValue("@PrescriptionID", prescriptionId);
                using var reader = cmdItems.ExecuteReader();
                while (reader.Read())
                {
                    rx.Items.Add(MapItemFromReader(reader));
                }
            }

            return rx;
        }

        /// <summary>
        /// Retrieves prescriptions by status (e.g. 'Active', 'Dispensed', 'Cancelled').
        /// </summary>
        public List<Prescription> GetPrescriptionsByStatus(string status)
        {
            var list = new List<Prescription>();
            const string query = @"
                SELECT rx.PrescriptionID, rx.PatientID, rx.DoctorID, rx.MedicalRecordID,
                       rx.PrescriptionDate, rx.Status, rx.Notes,
                       p.PatientCode, p.FirstName + ' ' + p.LastName AS PatientName,
                       'Dr. ' + d.FirstName + ' ' + d.LastName AS DoctorName,
                       dep.DepartmentName
                FROM dbo.Prescriptions rx
                INNER JOIN dbo.Patients p ON rx.PatientID = p.PatientID
                INNER JOIN dbo.Doctors d ON rx.DoctorID = d.DoctorID
                INNER JOIN dbo.Departments dep ON d.DepartmentID = dep.DepartmentID
                WHERE rx.Status = @Status
                ORDER BY rx.PrescriptionDate DESC;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Status", status);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapHeaderFromReader(reader));

            return list;
        }

        /// <summary>
        /// Retrieves all prescriptions for a specific patient.
        /// </summary>
        public List<Prescription> GetPrescriptionsForPatient(int patientId)
        {
            var list = new List<Prescription>();
            const string query = @"
                SELECT rx.PrescriptionID, rx.PatientID, rx.DoctorID, rx.MedicalRecordID,
                       rx.PrescriptionDate, rx.Status, rx.Notes,
                       p.PatientCode, p.FirstName + ' ' + p.LastName AS PatientName,
                       'Dr. ' + d.FirstName + ' ' + d.LastName AS DoctorName,
                       dep.DepartmentName
                FROM dbo.Prescriptions rx
                INNER JOIN dbo.Patients p ON rx.PatientID = p.PatientID
                INNER JOIN dbo.Doctors d ON rx.DoctorID = d.DoctorID
                INNER JOIN dbo.Departments dep ON d.DepartmentID = dep.DepartmentID
                WHERE rx.PatientID = @PatientID
                ORDER BY rx.PrescriptionDate DESC;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PatientID", patientId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapHeaderFromReader(reader));

            return list;
        }

        /// <summary>
        /// Retrieves recent prescriptions with limit.
        /// </summary>
        public List<Prescription> GetAllPrescriptions(int limit = 100)
        {
            var list = new List<Prescription>();
            string query = $@"
                SELECT TOP ({limit}) rx.PrescriptionID, rx.PatientID, rx.DoctorID, rx.MedicalRecordID,
                       rx.PrescriptionDate, rx.Status, rx.Notes,
                       p.PatientCode, p.FirstName + ' ' + p.LastName AS PatientName,
                       'Dr. ' + d.FirstName + ' ' + d.LastName AS DoctorName,
                       dep.DepartmentName
                FROM dbo.Prescriptions rx
                INNER JOIN dbo.Patients p ON rx.PatientID = p.PatientID
                INNER JOIN dbo.Doctors d ON rx.DoctorID = d.DoctorID
                INNER JOIN dbo.Departments dep ON d.DepartmentID = dep.DepartmentID
                ORDER BY rx.PrescriptionDate DESC;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapHeaderFromReader(reader));

            return list;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Multi-Table Transaction Operations
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Atomically creates a Prescription and all its PrescriptionItems inside a SqlTransaction.
        /// Returns the newly generated PrescriptionID.
        /// </summary>
        public int CreatePrescriptionWithItems(Prescription prescription)
        {
            const string insertHeaderSql = @"
                INSERT INTO dbo.Prescriptions
                    (PatientID, DoctorID, MedicalRecordID, PrescriptionDate, Status, Notes)
                VALUES
                    (@PatientID, @DoctorID, @MedicalRecordID, @PrescriptionDate, @Status, @Notes);
                SELECT SCOPE_IDENTITY();";

            const string insertItemSql = @"
                INSERT INTO dbo.PrescriptionItems
                    (PrescriptionID, MedicineID, Quantity, Dosage, Frequency, DurationDays, Instructions)
                VALUES
                    (@PrescriptionID, @MedicineID, @Quantity, @Dosage, @Frequency, @DurationDays, @Instructions);";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var trans = conn.BeginTransaction();

            try
            {
                int newRxId;
                using (var cmdHeader = new SqlCommand(insertHeaderSql, conn, trans))
                {
                    cmdHeader.Parameters.AddWithValue("@PatientID", prescription.PatientID);
                    cmdHeader.Parameters.AddWithValue("@DoctorID", prescription.DoctorID);
                    cmdHeader.Parameters.AddWithValue("@MedicalRecordID", (object?)prescription.MedicalRecordID ?? DBNull.Value);
                    cmdHeader.Parameters.AddWithValue("@PrescriptionDate", prescription.PrescriptionDate);
                    cmdHeader.Parameters.AddWithValue("@Status", prescription.Status);
                    cmdHeader.Parameters.AddWithValue("@Notes", (object?)prescription.Notes?.Trim() ?? DBNull.Value);

                    object? scalar = cmdHeader.ExecuteScalar();
                    newRxId = scalar != null ? Convert.ToInt32(scalar) : 0;
                }

                if (newRxId <= 0)
                    throw new InvalidOperationException("Failed to insert prescription header.");

                foreach (var item in prescription.Items)
                {
                    using var cmdItem = new SqlCommand(insertItemSql, conn, trans);
                    cmdItem.Parameters.AddWithValue("@PrescriptionID", newRxId);
                    cmdItem.Parameters.AddWithValue("@MedicineID", item.MedicineID);
                    cmdItem.Parameters.AddWithValue("@Quantity", item.Quantity);
                    cmdItem.Parameters.AddWithValue("@Dosage", item.Dosage.Trim());
                    cmdItem.Parameters.AddWithValue("@Frequency", item.Frequency.Trim());
                    cmdItem.Parameters.AddWithValue("@DurationDays", item.DurationDays);
                    cmdItem.Parameters.AddWithValue("@Instructions", (object?)item.Instructions?.Trim() ?? DBNull.Value);

                    cmdItem.ExecuteNonQuery();
                }

                trans.Commit();
                prescription.PrescriptionID = newRxId;
                return newRxId;
            }
            catch
            {
                trans.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Dispenses a prescription atomically:
        /// 1. Verifies that every line item has sufficient stock in dbo.Medicines.
        /// 2. Decrements StockQuantity for each medicine.
        /// 3. Updates prescription status to 'Dispensed'.
        /// If any item lacks stock, throws an InvalidOperationException and rolls back completely.
        /// </summary>
        public bool DispensePrescription(int prescriptionId, List<PrescriptionItem> items)
        {
            const string checkStockSql = @"
                SELECT MedicineName, StockQuantity
                FROM dbo.Medicines WITH (UPDLOCK)
                WHERE MedicineID = @MedicineID;";

            const string deductStockSql = @"
                UPDATE dbo.Medicines SET
                    StockQuantity = StockQuantity - @Quantity
                WHERE MedicineID = @MedicineID
                  AND StockQuantity >= @Quantity;";

            const string updateRxStatusSql = @"
                UPDATE dbo.Prescriptions SET
                    Status = 'Dispensed'
                WHERE PrescriptionID = @PrescriptionID
                  AND Status = 'Active';";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var trans = conn.BeginTransaction();

            try
            {
                // Step 1: Pre-verify all items
                foreach (var item in items)
                {
                    using var cmdCheck = new SqlCommand(checkStockSql, conn, trans);
                    cmdCheck.Parameters.AddWithValue("@MedicineID", item.MedicineID);
                    using var reader = cmdCheck.ExecuteReader();
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException($"Medicine with ID {item.MedicineID} does not exist in inventory.");
                    }

                    string medName = reader.GetString(0);
                    int currentStock = reader.GetInt32(1);
                    reader.Close();

                    if (currentStock < item.Quantity)
                    {
                        throw new InvalidOperationException(
                            $"Cannot dispense prescription: Insufficient stock for medicine '{medName}'. " +
                            $"Required: {item.Quantity}, Available: {currentStock}.");
                    }
                }

                // Step 2: Deduct stock for all items
                foreach (var item in items)
                {
                    using var cmdDeduct = new SqlCommand(deductStockSql, conn, trans);
                    cmdDeduct.Parameters.AddWithValue("@MedicineID", item.MedicineID);
                    cmdDeduct.Parameters.AddWithValue("@Quantity", item.Quantity);
                    int rows = cmdDeduct.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        throw new InvalidOperationException(
                            $"Failed to deduct stock for medicine ID {item.MedicineID}. Stock may have changed concurrently.");
                    }
                }

                // Step 3: Transition Prescription status to 'Dispensed'
                using (var cmdStatus = new SqlCommand(updateRxStatusSql, conn, trans))
                {
                    cmdStatus.Parameters.AddWithValue("@PrescriptionID", prescriptionId);
                    int updated = cmdStatus.ExecuteNonQuery();
                    if (updated <= 0)
                    {
                        throw new InvalidOperationException(
                            $"Prescription {prescriptionId} is not in 'Active' status and cannot be dispensed.");
                    }
                }

                trans.Commit();
                return true;
            }
            catch
            {
                trans.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Updates the status of a prescription (e.g. 'Cancelled').
        /// </summary>
        public bool UpdateStatus(int prescriptionId, string status)
        {
            const string query = "UPDATE dbo.Prescriptions SET Status = @Status WHERE PrescriptionID = @PrescriptionID;";
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PrescriptionID", prescriptionId);
            cmd.Parameters.AddWithValue("@Status", status);
            return cmd.ExecuteNonQuery() > 0;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Mapping Helpers
        // ─────────────────────────────────────────────────────────────────────────

        private static Prescription MapHeaderFromReader(SqlDataReader r)
        {
            return new Prescription
            {
                PrescriptionID   = r.GetInt32(r.GetOrdinal("PrescriptionID")),
                PatientID        = r.GetInt32(r.GetOrdinal("PatientID")),
                DoctorID         = r.GetInt32(r.GetOrdinal("DoctorID")),
                MedicalRecordID  = r.IsDBNull(r.GetOrdinal("MedicalRecordID")) ? null : r.GetInt32(r.GetOrdinal("MedicalRecordID")),
                PrescriptionDate = r.GetDateTime(r.GetOrdinal("PrescriptionDate")),
                Status           = r.GetString(r.GetOrdinal("Status")),
                Notes            = r.IsDBNull(r.GetOrdinal("Notes")) ? null : r.GetString(r.GetOrdinal("Notes")),
                PatientCode      = r.GetString(r.GetOrdinal("PatientCode")),
                PatientName      = r.GetString(r.GetOrdinal("PatientName")),
                DoctorName       = r.GetString(r.GetOrdinal("DoctorName")),
                DepartmentName   = r.GetString(r.GetOrdinal("DepartmentName"))
            };
        }

        private static PrescriptionItem MapItemFromReader(SqlDataReader r)
        {
            return new PrescriptionItem
            {
                PrescriptionItemID = r.GetInt32(r.GetOrdinal("PrescriptionItemID")),
                PrescriptionID     = r.GetInt32(r.GetOrdinal("PrescriptionID")),
                MedicineID         = r.GetInt32(r.GetOrdinal("MedicineID")),
                Quantity           = r.GetInt32(r.GetOrdinal("Quantity")),
                Dosage             = r.GetString(r.GetOrdinal("Dosage")),
                Frequency          = r.GetString(r.GetOrdinal("Frequency")),
                DurationDays       = r.GetInt32(r.GetOrdinal("DurationDays")),
                Instructions       = r.IsDBNull(r.GetOrdinal("Instructions")) ? null : r.GetString(r.GetOrdinal("Instructions")),
                MedicineCode       = r.GetString(r.GetOrdinal("MedicineCode")),
                MedicineName       = r.GetString(r.GetOrdinal("MedicineName")),
                Category           = r.GetString(r.GetOrdinal("Category")),
                Unit               = r.GetString(r.GetOrdinal("Unit")),
                UnitPrice          = r.GetDecimal(r.GetOrdinal("UnitPrice")),
                AvailableStock     = r.GetInt32(r.GetOrdinal("AvailableStock"))
            };
        }
    }
}
