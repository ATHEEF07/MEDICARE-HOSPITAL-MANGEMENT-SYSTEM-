using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;

namespace MEDICARE_HOSPITAL_MANGAMENT.Repositories
{
    /// <summary>
    /// Data Access Repository for Appointments.
    /// All queries are fully parameterized – no raw string concatenation.
    /// </summary>
    public class AppointmentRepository
    {
        // ─────────────────────────────────────────────────────────────────────────
        // Read Queries
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Retrieves all appointments for a given date, optionally filtered by doctor.
        /// </summary>
        public List<Appointment> GetAppointmentsByDate(DateTime date, int? doctorId = null)
        {
            var list = new List<Appointment>();
            string query = @"
                SELECT a.AppointmentID, a.PatientID, a.DoctorID, a.AppointmentDate,
                       a.StartTime, a.EndTime, a.Reason, a.Status, a.Notes,
                       a.CreatedByUserID, a.CreatedAt,
                       p.PatientCode, p.FirstName + ' ' + p.LastName AS PatientName, p.Phone AS PatientPhone,
                       'Dr. ' + d.FirstName + ' ' + d.LastName AS DoctorName,
                       dep.DepartmentName,
                       u.Username AS CreatedByUsername
                FROM dbo.Appointments a
                INNER JOIN dbo.Patients p ON a.PatientID = p.PatientID
                INNER JOIN dbo.Doctors d ON a.DoctorID = d.DoctorID
                INNER JOIN dbo.Departments dep ON d.DepartmentID = dep.DepartmentID
                INNER JOIN dbo.Users u ON a.CreatedByUserID = u.UserID
                WHERE a.AppointmentDate = @Date";

            if (doctorId.HasValue)
                query += " AND a.DoctorID = @DoctorID";

            query += " ORDER BY a.StartTime ASC;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Date", date.Date);
            if (doctorId.HasValue)
                cmd.Parameters.AddWithValue("@DoctorID", doctorId.Value);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapFromReader(reader));

            return list;
        }

        /// <summary>
        /// Retrieves all appointments for a specific doctor, with optional date filter.
        /// </summary>
        public List<Appointment> GetAppointmentsForDoctor(int doctorId, DateTime? date = null)
        {
            var list = new List<Appointment>();
            string query = @"
                SELECT a.AppointmentID, a.PatientID, a.DoctorID, a.AppointmentDate,
                       a.StartTime, a.EndTime, a.Reason, a.Status, a.Notes,
                       a.CreatedByUserID, a.CreatedAt,
                       p.PatientCode, p.FirstName + ' ' + p.LastName AS PatientName, p.Phone AS PatientPhone,
                       'Dr. ' + d.FirstName + ' ' + d.LastName AS DoctorName,
                       dep.DepartmentName,
                       u.Username AS CreatedByUsername
                FROM dbo.Appointments a
                INNER JOIN dbo.Patients p ON a.PatientID = p.PatientID
                INNER JOIN dbo.Doctors d ON a.DoctorID = d.DoctorID
                INNER JOIN dbo.Departments dep ON d.DepartmentID = dep.DepartmentID
                INNER JOIN dbo.Users u ON a.CreatedByUserID = u.UserID
                WHERE a.DoctorID = @DoctorID";

            if (date.HasValue)
                query += " AND a.AppointmentDate = @Date";

            query += " ORDER BY a.AppointmentDate DESC, a.StartTime ASC;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@DoctorID", doctorId);
            if (date.HasValue)
                cmd.Parameters.AddWithValue("@Date", date.Value.Date);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapFromReader(reader));

            return list;
        }

        /// <summary>
        /// Retrieves all appointments for a specific patient (full history).
        /// </summary>
        public List<Appointment> GetAppointmentsForPatient(int patientId)
        {
            var list = new List<Appointment>();
            const string query = @"
                SELECT a.AppointmentID, a.PatientID, a.DoctorID, a.AppointmentDate,
                       a.StartTime, a.EndTime, a.Reason, a.Status, a.Notes,
                       a.CreatedByUserID, a.CreatedAt,
                       p.PatientCode, p.FirstName + ' ' + p.LastName AS PatientName, p.Phone AS PatientPhone,
                       'Dr. ' + d.FirstName + ' ' + d.LastName AS DoctorName,
                       dep.DepartmentName,
                       u.Username AS CreatedByUsername
                FROM dbo.Appointments a
                INNER JOIN dbo.Patients p ON a.PatientID = p.PatientID
                INNER JOIN dbo.Doctors d ON a.DoctorID = d.DoctorID
                INNER JOIN dbo.Departments dep ON d.DepartmentID = dep.DepartmentID
                INNER JOIN dbo.Users u ON a.CreatedByUserID = u.UserID
                WHERE a.PatientID = @PatientID
                ORDER BY a.AppointmentDate DESC, a.StartTime ASC;";

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
        /// Returns a filtered list of appointments across multiple optional criteria.
        /// </summary>
        public List<Appointment> SearchAppointments(DateTime? fromDate, DateTime? toDate, int? doctorId, string? status, string? patientSearch)
        {
            var list = new List<Appointment>();
            var conditions = new System.Collections.Generic.List<string>();

            string query = @"
                SELECT a.AppointmentID, a.PatientID, a.DoctorID, a.AppointmentDate,
                       a.StartTime, a.EndTime, a.Reason, a.Status, a.Notes,
                       a.CreatedByUserID, a.CreatedAt,
                       p.PatientCode, p.FirstName + ' ' + p.LastName AS PatientName, p.Phone AS PatientPhone,
                       'Dr. ' + d.FirstName + ' ' + d.LastName AS DoctorName,
                       dep.DepartmentName,
                       u.Username AS CreatedByUsername
                FROM dbo.Appointments a
                INNER JOIN dbo.Patients p ON a.PatientID = p.PatientID
                INNER JOIN dbo.Doctors d ON a.DoctorID = d.DoctorID
                INNER JOIN dbo.Departments dep ON d.DepartmentID = dep.DepartmentID
                INNER JOIN dbo.Users u ON a.CreatedByUserID = u.UserID";

            if (fromDate.HasValue) conditions.Add("a.AppointmentDate >= @FromDate");
            if (toDate.HasValue) conditions.Add("a.AppointmentDate <= @ToDate");
            if (doctorId.HasValue) conditions.Add("a.DoctorID = @DoctorID");
            if (!string.IsNullOrWhiteSpace(status)) conditions.Add("a.Status = @Status");
            if (!string.IsNullOrWhiteSpace(patientSearch))
                conditions.Add("(p.PatientCode LIKE @Search OR p.FirstName LIKE @Search OR p.LastName LIKE @Search OR p.Phone LIKE @Search)");

            if (conditions.Count > 0)
                query += " WHERE " + string.Join(" AND ", conditions);

            query += " ORDER BY a.AppointmentDate DESC, a.StartTime ASC;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            if (fromDate.HasValue) cmd.Parameters.AddWithValue("@FromDate", fromDate.Value.Date);
            if (toDate.HasValue) cmd.Parameters.AddWithValue("@ToDate", toDate.Value.Date);
            if (doctorId.HasValue) cmd.Parameters.AddWithValue("@DoctorID", doctorId.Value);
            if (!string.IsNullOrWhiteSpace(status)) cmd.Parameters.AddWithValue("@Status", status);
            if (!string.IsNullOrWhiteSpace(patientSearch)) cmd.Parameters.AddWithValue("@Search", $"%{patientSearch.Trim()}%");

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapFromReader(reader));

            return list;
        }

        /// <summary>
        /// Retrieves a single appointment by its ID.
        /// </summary>
        public Appointment? GetAppointmentById(int appointmentId)
        {
            const string query = @"
                SELECT a.AppointmentID, a.PatientID, a.DoctorID, a.AppointmentDate,
                       a.StartTime, a.EndTime, a.Reason, a.Status, a.Notes,
                       a.CreatedByUserID, a.CreatedAt,
                       p.PatientCode, p.FirstName + ' ' + p.LastName AS PatientName, p.Phone AS PatientPhone,
                       'Dr. ' + d.FirstName + ' ' + d.LastName AS DoctorName,
                       dep.DepartmentName,
                       u.Username AS CreatedByUsername
                FROM dbo.Appointments a
                INNER JOIN dbo.Patients p ON a.PatientID = p.PatientID
                INNER JOIN dbo.Doctors d ON a.DoctorID = d.DoctorID
                INNER JOIN dbo.Departments dep ON d.DepartmentID = dep.DepartmentID
                INNER JOIN dbo.Users u ON a.CreatedByUserID = u.UserID
                WHERE a.AppointmentID = @AppointmentID;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@AppointmentID", appointmentId);

            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapFromReader(reader) : null;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Conflict Detection
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Checks whether the specified doctor has any 'Scheduled' appointments that
        /// overlap with the given time interval using the standard interval-overlap formula:
        ///     (StartTime &lt; @EndTime AND EndTime &gt; @StartTime)
        /// </summary>
        /// <param name="excludeAppointmentId">Pass the existing appointment ID when rescheduling to exclude it from the check.</param>
        public bool HasDoctorOverlap(int doctorId, DateTime date, TimeSpan startTime, TimeSpan endTime, int? excludeAppointmentId = null)
        {
            const string query = @"
                SELECT COUNT(1)
                FROM dbo.Appointments
                WHERE DoctorID     = @DoctorID
                  AND AppointmentDate = @AppointmentDate
                  AND Status         = 'Scheduled'
                  AND (@ExcludeID IS NULL OR AppointmentID <> @ExcludeID)
                  AND (StartTime < @EndTime AND EndTime > @StartTime);";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@DoctorID", doctorId);
            cmd.Parameters.AddWithValue("@AppointmentDate", date.Date);
            cmd.Parameters.AddWithValue("@StartTime", startTime);
            cmd.Parameters.AddWithValue("@EndTime", endTime);

            if (excludeAppointmentId.HasValue)
                cmd.Parameters.AddWithValue("@ExcludeID", excludeAppointmentId.Value);
            else
                cmd.Parameters.AddWithValue("@ExcludeID", DBNull.Value);

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Write Operations
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Creates a new appointment record and returns the generated AppointmentID.
        /// </summary>
        public int CreateAppointment(Appointment appt)
        {
            const string query = @"
                INSERT INTO dbo.Appointments
                    (PatientID, DoctorID, AppointmentDate, StartTime, EndTime, Reason, Status, Notes, CreatedByUserID)
                VALUES
                    (@PatientID, @DoctorID, @AppointmentDate, @StartTime, @EndTime, @Reason, @Status, @Notes, @CreatedByUserID);
                SELECT SCOPE_IDENTITY();";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            BindWriteParameters(cmd, appt);

            object? result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : 0;
        }

        /// <summary>
        /// Updates an existing appointment record.
        /// </summary>
        public bool UpdateAppointment(Appointment appt)
        {
            const string query = @"
                UPDATE dbo.Appointments SET
                    PatientID       = @PatientID,
                    DoctorID        = @DoctorID,
                    AppointmentDate = @AppointmentDate,
                    StartTime       = @StartTime,
                    EndTime         = @EndTime,
                    Reason          = @Reason,
                    Status          = @Status,
                    Notes           = @Notes
                WHERE AppointmentID = @AppointmentID;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            BindWriteParameters(cmd, appt);
            cmd.Parameters.AddWithValue("@AppointmentID", appt.AppointmentID);

            return cmd.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Updates only the status column of an appointment.
        /// </summary>
        public bool UpdateAppointmentStatus(int appointmentId, string status)
        {
            const string query = @"
                UPDATE dbo.Appointments
                SET Status = @Status
                WHERE AppointmentID = @AppointmentID;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@AppointmentID", appointmentId);

            return cmd.ExecuteNonQuery() > 0;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Private helpers
        // ─────────────────────────────────────────────────────────────────────────

        private static void BindWriteParameters(SqlCommand cmd, Appointment appt)
        {
            cmd.Parameters.AddWithValue("@PatientID", appt.PatientID);
            cmd.Parameters.AddWithValue("@DoctorID", appt.DoctorID);
            cmd.Parameters.AddWithValue("@AppointmentDate", appt.AppointmentDate.Date);
            cmd.Parameters.AddWithValue("@StartTime", appt.StartTime);
            cmd.Parameters.AddWithValue("@EndTime", appt.EndTime);
            cmd.Parameters.AddWithValue("@Reason", (object?)appt.Reason ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", appt.Status);
            cmd.Parameters.AddWithValue("@Notes", (object?)appt.Notes ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedByUserID", appt.CreatedByUserID);
        }

        private static Appointment MapFromReader(SqlDataReader reader)
        {
            return new Appointment
            {
                AppointmentID    = reader.GetInt32(reader.GetOrdinal("AppointmentID")),
                PatientID        = reader.GetInt32(reader.GetOrdinal("PatientID")),
                DoctorID         = reader.GetInt32(reader.GetOrdinal("DoctorID")),
                AppointmentDate  = reader.GetDateTime(reader.GetOrdinal("AppointmentDate")),
                StartTime        = reader.GetTimeSpan(reader.GetOrdinal("StartTime")),
                EndTime          = reader.GetTimeSpan(reader.GetOrdinal("EndTime")),
                Reason           = reader.IsDBNull(reader.GetOrdinal("Reason")) ? null : reader.GetString(reader.GetOrdinal("Reason")),
                Status           = reader.GetString(reader.GetOrdinal("Status")),
                Notes            = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes")),
                CreatedByUserID  = reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
                CreatedAt        = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                PatientCode      = reader.GetString(reader.GetOrdinal("PatientCode")),
                PatientName      = reader.GetString(reader.GetOrdinal("PatientName")),
                PatientPhone     = reader.IsDBNull(reader.GetOrdinal("PatientPhone")) ? string.Empty : reader.GetString(reader.GetOrdinal("PatientPhone")),
                DoctorName       = reader.GetString(reader.GetOrdinal("DoctorName")),
                DepartmentName   = reader.GetString(reader.GetOrdinal("DepartmentName")),
                CreatedByUsername = reader.GetString(reader.GetOrdinal("CreatedByUsername"))
            };
        }
    }
}
