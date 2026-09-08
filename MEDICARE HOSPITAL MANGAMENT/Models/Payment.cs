using System;

namespace MEDICARE_HOSPITAL_MANGAMENT.Models
{
    /// <summary>
    /// Represents a cashier payment receipt recorded against an invoice/bill.
    /// Maps to the dbo.Payments table.
    /// </summary>
    public class Payment
    {
        // ─── Database Columns ─────────────────────────────────────────────────────
        public int PaymentID { get; set; }
        public int BillID { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = "Cash"; // 'Cash', 'Card', 'BankTransfer'
        public string? ReferenceNo { get; set; }            // e.g. 'RCP-10001', Card transaction ref
        public int ReceivedByUserID { get; set; }

        // ─── Joined display properties ────────────────────────────────────────────
        public string ReceivedByUserName { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public string PatientCode { get; set; } = string.Empty;
        public string BillDescription { get; set; } = string.Empty;
        public decimal BillTotalAmount { get; set; }
        public decimal RemainingBalanceAfterPayment { get; set; }

        public override string ToString() =>
            $"RCP-{PaymentID:D5} | {PaymentDate:dd/MM/yyyy HH:mm} – Rs. {Amount:N2} via {PaymentMethod} ({ReferenceNo})";
    }
}
