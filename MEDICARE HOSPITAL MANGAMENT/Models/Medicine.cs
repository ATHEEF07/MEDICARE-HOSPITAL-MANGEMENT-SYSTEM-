using System;

namespace MEDICARE_HOSPITAL_MANGAMENT.Models
{
    /// <summary>
    /// Represents a pharmaceutical item/drug in the hospital inventory.
    /// Maps to the dbo.Medicines table.
    /// </summary>
    public class Medicine
    {
        public int MedicineID { get; set; }
        public string MedicineCode { get; set; } = string.Empty;
        public string MedicineName { get; set; } = string.Empty;
        public string? Category { get; set; }
        public string Unit { get; set; } = "Tablet"; // e.g. Tablet, Capsule, Syrup, Inhaler, Vial
        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public int ReorderLevel { get; set; } = 10;
        public DateTime? ExpiryDate { get; set; }
        public bool IsActive { get; set; } = true;

        // ─── Computed Properties ──────────────────────────────────────────────────
        /// <summary>
        /// Indicates if available stock has dropped to or below the reorder threshold.
        /// </summary>
        public bool IsLowStock => StockQuantity <= ReorderLevel;

        /// <summary>
        /// Indicates if the medication has passed its expiry date.
        /// </summary>
        public bool IsExpired => ExpiryDate.HasValue && ExpiryDate.Value.Date < DateTime.Today;

        /// <summary>
        /// Human-readable stock status description.
        /// </summary>
        public string StockStatusText =>
            StockQuantity == 0 ? "Out of Stock" :
            IsLowStock ? "Low Stock" : "In Stock";

        public override string ToString() => $"{MedicineCode} - {MedicineName} ({StockQuantity} {Unit}s available)";
    }
}
