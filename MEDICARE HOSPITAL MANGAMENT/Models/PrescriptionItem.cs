using System;

namespace MEDICARE_HOSPITAL_MANGAMENT.Models
{
    /// <summary>
    /// Represents an individual medication line item prescribed to a patient.
    /// Maps to the dbo.PrescriptionItems table.
    /// </summary>
    public class PrescriptionItem
    {
        // ─── Database Columns ─────────────────────────────────────────────────────
        public int PrescriptionItemID { get; set; }
        public int PrescriptionID { get; set; }
        public int MedicineID { get; set; }
        public int Quantity { get; set; }
        public string Dosage { get; set; } = string.Empty;       // e.g. "500mg", "1 tablet", "10ml"
        public string Frequency { get; set; } = string.Empty;    // e.g. "OD", "BD", "TDS", "QDS", "PRN", "Nocte"
        public int DurationDays { get; set; } = 1;               // e.g. 5 days, 7 days
        public string? Instructions { get; set; }                // e.g. "Take after meals with water"

        // ─── Joined display properties from Medicines ─────────────────────────────
        public string MedicineCode { get; set; } = string.Empty;
        public string MedicineName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Unit { get; set; } = "Tablet";
        public decimal UnitPrice { get; set; }
        public int AvailableStock { get; set; }

        // ─── Computed Helpers ─────────────────────────────────────────────────────
        public decimal TotalPrice => UnitPrice * Quantity;
        public bool HasSufficientStock => AvailableStock >= Quantity;

        public override string ToString() =>
            $"{MedicineName} – {Dosage} ({Frequency}) for {DurationDays} days [Qty: {Quantity}]";
    }
}
