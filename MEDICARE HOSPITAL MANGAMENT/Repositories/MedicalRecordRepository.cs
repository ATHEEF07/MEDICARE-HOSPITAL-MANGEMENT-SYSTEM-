using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;

namespace MEDICARE_HOSPITAL_MANGAMENT.Repositories
{
    /// <summary>
    /// Data Access Repository for Medical Records (clinical consultations).
    /// All queries are parameterized – no string concatenation of user input.
    /// </summary>
    public class MedicalRecordRepository
    {
        // ─────────────────────────────────────────────────────────────────────────
        // Read Queries
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Returns the full history of medical records for a patient, newest first.
        /// </summary>
        public List<MedicalRecord> GetRecordsByPatientId(int patientId)
        {
            var list = new List<MedicalRecord>();
            const string query = @"
                SELECT mr.MedicalRecordID, mr.PatientID, mr.DoctorID, mr.AppointmentID,
                       mr.VisitDate, mr.Symptoms, mr.Diagnosis, mr.Treatment, mr.Notes,
                       p.PatientCode, p.FirstName + ' ' + p.LastName AS PatientName,
                       p.Gender AS PatientGender,
                       ISNULL(p.BloodGroup, '') AS PatientBloodGroup,
                       DATEDIFF(YEAR, p.DateOfBirth, GETDATE())
                           - CASE WHEN MONTH(p.DateOfBirth) > MONTH(GETDATE())
                                       OR (MONTH(p.DateOfBirth) = MONTH(GETDATE()) AND DAY(p.DateOfBirth) > DAY(GETDATE()))
                               THEN 1 ELSE 0 END AS PatientAge,
                       'Dr. ' + d.FirstName + ' ' + d.LastName AS DoctorName,
                       dep.DepartmentName
                FROM dbo.MedicalRecords mr
                INNER JOIN dbo.Patients p ON mr.PatientID = p.PatientID
                INNER JOIN dbo.Doctors d ON mr.DoctorID = d.DoctorID
                INNER JOIN dbo.Departments dep ON d.DepartmentID = dep.DepartmentID
                WHERE mr.PatientID = @PatientID
                ORDER BY mr.VisitDate DESC;";

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
        /// Returns all medical records authored by a doctor, newest first.
        /// </summary>
        public List<MedicalRecord> GetRecordsByDoctorId(int doctorId)
        {
            var list = new List<MedicalRecord>();
            const string query = @"
                SELECT mr.MedicalRecordID, mr.PatientID, mr.DoctorID, mr.AppointmentID,
                       mr.VisitDate, mr.Symptoms, mr.Diagnosis, mr.Treatment, mr.Notes,
                       p.PatientCode, p.FirstName + ' ' + p.LastName AS PatientName,
                       p.Gender AS PatientGender,
                       ISNULL(p.BloodGroup, '') AS PatientBloodGroup,
                       DATEDIFF(YEAR, p.DateOfBirth, GETDATE())
                           - CASE WHEN MONTH(p.DateOfBirth) > MONTH(GETDATE())
                                       OR (MONTH(p.DateOfBirth) = MONTH(GETDATE()) AND DAY(p.DateOfBirth) > DAY(GETDATE()))
                               THEN 1 ELSE 0 END AS PatientAge,
                       'Dr. ' + d.FirstName + ' ' + d.LastName AS DoctorName,
                       dep.DepartmentName
                FROM dbo.MedicalRecords mr
                INNER JOIN dbo.Patients p ON mr.PatientID = p.PatientID
                INNER JOIN dbo.Doctors d ON mr.DoctorID = d.DoctorID
                INNER JOIN dbo.Departments dep ON d.DepartmentID = dep.DepartmentID
                WHERE mr.DoctorID = @DoctorID
                ORDER BY mr.VisitDate DESC;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@DoctorID", doctorId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapFromReader(reader));

            return list;
        }

        /// <summary>
        /// Returns a single medical record by its primary key.
        /// </summary>
        public MedicalRecord? GetById(int recordId)
        {
            const string query = @"
                SELECT mr.MedicalRecordID, mr.PatientID, mr.DoctorID, mr.AppointmentID,
                       mr.VisitDate, mr.Symptoms, mr.Diagnosis, mr.Treatment, mr.Notes,
                       p.PatientCode, p.FirstName + ' ' + p.LastName AS PatientName,
                       p.Gender AS PatientGender,
                       ISNULL(p.BloodGroup, '') AS PatientBloodGroup,
                       DATEDIFF(YEAR, p.DateOfBirth, GETDATE())
                           - CASE WHEN MONTH(p.DateOfBirth) > MONTH(GETDATE())
                                       OR (MONTH(p.DateOfBirth) = MONTH(GETDATE()) AND DAY(p.DateOfBirth) > DAY(GETDATE()))
                               THEN 1 ELSE 0 END AS PatientAge,
                       'Dr. ' + d.FirstName + ' ' + d.LastName AS DoctorName,
                       dep.DepartmentName
                FROM dbo.MedicalRecords mr
                INNER JOIN dbo.Patients p ON mr.PatientID = p.PatientID
                INNER JOIN dbo.Doctors d ON mr.DoctorID = d.DoctorID
                INNER JOIN dbo.Departments dep ON d.DepartmentID = dep.DepartmentID
                WHERE mr.MedicalRecordID = @MedicalRecordID;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MedicalRecordID", recordId);

            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapFromReader(reader) : null;
        }

        /// <summary>
        /// Returns the medical record linked to a specific appointment (if one exists).
        /// </summary>
        public MedicalRecord? GetByAppointmentId(int appointmentId)
        {
            const string query = @"
                SELECT mr.MedicalRecordID, mr.PatientID, mr.DoctorID, mr.AppointmentID,
                       mr.VisitDate, mr.Symptoms, mr.Diagnosis, mr.Treatment, mr.Notes,
                       p.PatientCode, p.FirstName + ' ' + p.LastName AS PatientName,
                       p.Gender AS PatientGender,
                       ISNULL(p.BloodGroup, '') AS PatientBloodGroup,
                       DATEDIFF(YEAR, p.DateOfBirth, GETDATE())
                           - CASE WHEN MONTH(p.DateOfBirth) > MONTH(GETDATE())
                                       OR (MONTH(p.DateOfBirth) = MONTH(GETDATE()) AND DAY(p.DateOfBirth) > DAY(GETDATE()))
                               THEN 1 ELSE 0 END AS PatientAge,
                       'Dr. ' + d.FirstName + ' ' + d.LastName AS DoctorName,
                       dep.DepartmentName
                FROM dbo.MedicalRecords mr
                INNER JOIN dbo.Patients p ON mr.PatientID = p.PatientID
                INNER JOIN dbo.Doctors d ON mr.DoctorID = d.DoctorID
                INNER JOIN dbo.Departments dep ON d.DepartmentID = dep.DepartmentID
                WHERE mr.AppointmentID = @AppointmentID;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@AppointmentID", appointmentId);

            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapFromReader(reader) : null;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Write Operations
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Inserts a new medical record and returns the generated MedicalRecordID.
        /// </summary>
        public int CreateRecord(MedicalRecord record)
        {
            const string query = @"
                INSERT INTO dbo.MedicalRecords
                    (PatientID, DoctorID, AppointmentID, VisitDate, Symptoms, Diagnosis, Treatment, Notes)
                VALUES
                    (@PatientID, @DoctorID, @AppointmentID, @VisitDate, @Symptoms, @Diagnosis, @Treatment, @Notes);
                SELECT SCOPE_IDENTITY();";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            BindWriteParameters(cmd, record);

            object? result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : 0;
        }

        /// <summary>
        /// Updates an existing medical record (Symptoms, Diagnosis, Treatment, Notes only).
        /// </summary>
        public bool UpdateRecord(MedicalRecord record)
        {
            const string query = @"
                UPDATE dbo.MedicalRecords SET
                    Symptoms    = @Symptoms,
                    Diagnosis   = @Diagnosis,
                    Treatment   = @Treatment,
                    Notes       = @Notes
                WHERE MedicalRecordID = @MedicalRecordID;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Symptoms", (object?)record.Symptoms ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Diagnosis", (object?)record.Diagnosis ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Treatment", (object?)record.Treatment ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Notes", (object?)record.Notes ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MedicalRecordID", record.MedicalRecordID);

            return cmd.ExecuteNonQuery() > 0;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Private helpers
        // ─────────────────────────────────────────────────────────────────────────

        private static void BindWriteParameters(SqlCommand cmd, MedicalRecord record)
        {
            cmd.Parameters.AddWithValue("@PatientID", record.PatientID);
            cmd.Parameters.AddWithValue("@DoctorID", record.DoctorID);
            cmd.Parameters.AddWithValue("@AppointmentID", record.AppointmentID.HasValue ? (object)record.AppointmentID.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@VisitDate", record.VisitDate);
            cmd.Parameters.AddWithValue("@Symptoms", (object?)record.Symptoms ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Diagnosis", (object?)record.Diagnosis ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Treatment", (object?)record.Treatment ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Notes", (object?)record.Notes ?? DBNull.Value);
        }

        private static MedicalRecord MapFromReader(SqlDataReader reader)
        {
            int apptOrdinal = reader.GetOrdinal("AppointmentID");
            return new MedicalRecord
            {
                MedicalRecordID  = reader.GetInt32(reader.GetOrdinal("MedicalRecordID")),
                PatientID        = reader.GetInt32(reader.GetOrdinal("PatientID")),
                DoctorID         = reader.GetInt32(reader.GetOrdinal("DoctorID")),
                AppointmentID    = reader.IsDBNull(apptOrdinal) ? null : reader.GetInt32(apptOrdinal),
                VisitDate        = reader.GetDateTime(reader.GetOrdinal("VisitDate")),
                Symptoms         = reader.IsDBNull(reader.GetOrdinal("Symptoms")) ? null : reader.GetString(reader.GetOrdinal("Symptoms")),
                Diagnosis        = reader.IsDBNull(reader.GetOrdinal("Diagnosis")) ? null : reader.GetString(reader.GetOrdinal("Diagnosis")),
                Treatment        = reader.IsDBNull(reader.GetOrdinal("Treatment")) ? null : reader.GetString(reader.GetOrdinal("Treatment")),
                Notes            = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes")),
                PatientCode      = reader.GetString(reader.GetOrdinal("PatientCode")),
                PatientName      = reader.GetString(reader.GetOrdinal("PatientName")),
                PatientGender    = reader.GetString(reader.GetOrdinal("PatientGender")),
                PatientBloodGroup = reader.GetString(reader.GetOrdinal("PatientBloodGroup")),
                PatientAge       = reader.GetInt32(reader.GetOrdinal("PatientAge")),
                DoctorName       = reader.GetString(reader.GetOrdinal("DoctorName")),
                DepartmentName   = reader.GetString(reader.GetOrdinal("DepartmentName"))
            };
        }
    }
}
