using System;

namespace MEDICARE_HOSPITAL_MANGAMENT.Models
{
    /// <summary>
    /// Represents an invoice/bill generated for patient healthcare services and medications.
    /// Maps to the dbo.Bills table.
    /// </summary>
    public class Bill
    {
        // ─── Database Columns ─────────────────────────────────────────────────────
        public int BillID { get; set; }
        public int PatientID { get; set; }
        public int? AppointmentID { get; set; }
        public DateTime BillDate { get; set; } = DateTime.Now;
        public string? Description { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending"; // 'Pending', 'PartiallyPaid', 'Paid', 'Cancelled'

        // ─── Joined display properties ────────────────────────────────────────────
        public string PatientCode { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public string PatientPhone { get; set; } = string.Empty;
        public decimal TotalPaid { get; set; }

        // ─── Computed Helpers ─────────────────────────────────────────────────────
        /// <summary>
        /// Net outstanding balance remaining to be paid.
        /// </summary>
        public decimal OutstandingBalance => Math.Max(0m, TotalAmount - TotalPaid);

        /// <summary>
        /// True if the total amount has been paid in full.
        /// </summary>
        public bool IsFullyPaid => TotalPaid >= TotalAmount && TotalAmount > 0;

        public override string ToString() =>
            $"INV-{BillID:D5} | {BillDate:dd/MM/yyyy} – {PatientName} [Total: Rs. {TotalAmount:N2}, Due: Rs. {OutstandingBalance:N2}] ({Status})";
    }
}
