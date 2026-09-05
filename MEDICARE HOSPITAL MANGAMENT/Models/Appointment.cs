using System;

namespace MEDICARE_HOSPITAL_MANGAMENT.Models
{
    /// <summary>
    /// Represents a scheduled patient appointment with a doctor.
    /// Maps to the dbo.Appointments table.
    /// </summary>
    public class Appointment
    {
        // ─── Primary columns ──────────────────────────────────────────────────────
        public int AppointmentID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AppointmentDate { get; set; } = DateTime.Today;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? Reason { get; set; }

        /// <summary>
        /// Valid values: 'Scheduled', 'Completed', 'Cancelled', 'NoShow'
        /// </summary>
        public string Status { get; set; } = "Scheduled";
        public string? Notes { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // ─── Joined display properties (populated by Repository JOIN queries) ────
        public string PatientCode { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public string PatientPhone { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string CreatedByUsername { get; set; } = string.Empty;

        // ─── Computed helpers ────────────────────────────────────────────────────
        /// <summary>
        /// Returns a formatted time-slot string such as "10:00 AM - 10:30 AM".
        /// </summary>
        public string TimeSlotFormatted
        {
            get
            {
                var start = DateTime.Today.Add(StartTime);
                var end = DateTime.Today.Add(EndTime);
                return $"{start:hh:mm tt} – {end:hh:mm tt}";
            }
        }

        /// <summary>
        /// Returns duration of the appointment in minutes.
        /// </summary>
        public int DurationMinutes => (int)(EndTime - StartTime).TotalMinutes;

        public override string ToString() =>
            $"{AppointmentDate:dd/MM/yyyy} {TimeSlotFormatted} – {PatientName} with {DoctorName} [{Status}]";
    }
}
