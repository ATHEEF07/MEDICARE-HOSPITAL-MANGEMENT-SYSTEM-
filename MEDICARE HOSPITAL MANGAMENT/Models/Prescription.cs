using System;
using System.Collections.Generic;
using System.Linq;

namespace MEDICARE_HOSPITAL_MANGAMENT.Models
{
    /// <summary>
    /// Represents a doctor's prescription for a patient, containing one or more medicine line items.
    /// Maps to the dbo.Prescriptions table.
    /// </summary>
    public class Prescription
    {
        // ─── Database Columns ─────────────────────────────────────────────────────
        public int PrescriptionID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public int? MedicalRecordID { get; set; }
        public DateTime PrescriptionDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Active"; // 'Active', 'Dispensed', 'Cancelled'
        public string? Notes { get; set; }

        // ─── Joined display properties from Patients & Doctors ────────────────────
        public string PatientCode { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;

        // ─── Line Items ───────────────────────────────────────────────────────────
        public List<PrescriptionItem> Items { get; set; } = new();

        // ─── Computed Helpers ─────────────────────────────────────────────────────
        public int TotalItemsCount => Items.Count;
        public decimal EstimatedTotalCost => Items.Sum(i => i.TotalPrice);
        public bool AllItemsInStock => Items.Count > 0 && Items.All(i => i.HasSufficientStock);

        public override string ToString() =>
            $"RX-{PrescriptionID:D5} | {PrescriptionDate:dd/MM/yyyy} – {PatientName} ({Status}) [{Items.Count} items]";
    }
}
