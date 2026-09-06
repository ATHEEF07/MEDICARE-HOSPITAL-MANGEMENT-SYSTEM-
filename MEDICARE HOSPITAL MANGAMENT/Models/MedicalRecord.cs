using System;

namespace MEDICARE_HOSPITAL_MANGAMENT.Models
{
    /// <summary>
    /// Represents a clinical consultation / medical record recorded by a doctor.
    /// Maps to the dbo.MedicalRecords table.
    /// </summary>
    public class MedicalRecord
    {
        // ─── Primary columns ──────────────────────────────────────────────────────
        public int MedicalRecordID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }

        /// <summary>
        /// Optional: links this record to a specific scheduled appointment.
        /// </summary>
        public int? AppointmentID { get; set; }

        public DateTime VisitDate { get; set; } = DateTime.Now;
        public string? Symptoms { get; set; }
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public string? Notes { get; set; }

        // ─── Joined display properties (populated by Repository JOIN queries) ────
        public string PatientCode { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public int PatientAge { get; set; }
        public string PatientGender { get; set; } = string.Empty;
        public string PatientBloodGroup { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;

        public override string ToString() =>
            $"{VisitDate:dd/MM/yyyy} – {PatientName} (Dx: {Diagnosis}) by {DoctorName}";
    }
}
