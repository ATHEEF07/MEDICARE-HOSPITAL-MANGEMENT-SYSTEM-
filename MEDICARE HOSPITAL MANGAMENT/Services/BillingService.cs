using System;
using System.Collections.Generic;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Repositories;

namespace MEDICARE_HOSPITAL_MANGAMENT.Services
{
    /// <summary>
    /// Business Logic Layer for Billing and Invoicing.
    /// Enforces subtotal/discount calculations, non-negative amounts, and cashier authorization.
    /// </summary>
    public class BillingService
    {
        private readonly BillRepository _billRepo;
        private readonly PatientRepository _patientRepo;
        private readonly PaymentRepository _paymentRepo;

        public BillingService()
        {
            _billRepo = new BillRepository();
            _patientRepo = new PatientRepository();
            _paymentRepo = new PaymentRepository();
        }

        public BillingService(
            BillRepository billRepo,
            PatientRepository patientRepo,
            PaymentRepository paymentRepo)
        {
            _billRepo = billRepo;
            _patientRepo = patientRepo;
            _paymentRepo = paymentRepo;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Read Pass-Throughs
        // ─────────────────────────────────────────────────────────────────────────

        public Bill? GetBillById(int billId) => _billRepo.GetBillById(billId);
        public List<Bill> GetBillsByPatient(int patientId) => _billRepo.GetBillsByPatient(patientId);
        public List<Bill> GetPendingBills() => _billRepo.GetPendingBills();
        public List<Bill> GetAllBills(int limit = 100) => _billRepo.GetAllBills(limit);
        public List<Bill> SearchBills(string query) => _billRepo.SearchBills(query);

        // ─────────────────────────────────────────────────────────────────────────
        // Bill Creation & Financial Rules
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Validates financial inputs and creates a new invoice/bill.
        /// Automatically computes TotalAmount = Subtotal - Discount.
        /// </summary>
        public int CreateBill(Bill bill, out string errorMessage)
        {
            errorMessage = string.Empty;

            // Authorization: Cashier or Administrator only
            if (!SessionManager.IsCashier() && !SessionManager.IsAdmin())
            {
                errorMessage = "Access denied. Only Cashiers and Administrators can generate bills.";
                return 0;
            }

            if (!ValidateBill(bill, out errorMessage))
                return 0;

            // Enforce financial equation
            bill.TotalAmount = bill.Subtotal - bill.Discount;
            bill.Status = "Pending";

            int newId = _billRepo.CreateBill(bill);
            if (newId <= 0)
            {
                errorMessage = "Database failed to create bill.";
                return 0;
            }

            return newId;
        }

        /// <summary>
        /// Cancels a bill if no payments have been recorded against it.
        /// </summary>
        public bool CancelBill(int billId, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!SessionManager.IsCashier() && !SessionManager.IsAdmin())
            {
                errorMessage = "Access denied. Only Cashiers and Administrators can cancel bills.";
                return false;
            }

            var bill = _billRepo.GetBillById(billId);
            if (bill == null)
            {
                errorMessage = "Bill not found.";
                return false;
            }

            if (bill.Status == "Paid")
            {
                errorMessage = "Cannot cancel a bill that has already been paid in full.";
                return false;
            }

            if (bill.Status == "Cancelled")
            {
                errorMessage = "Bill is already cancelled.";
                return false;
            }

            decimal totalPaid = _paymentRepo.GetTotalPaidForBill(billId);
            if (totalPaid > 0)
            {
                errorMessage = $"Cannot cancel this bill because Rs. {totalPaid:N2} in payments have already been collected against it.";
                return false;
            }

            bool ok = _billRepo.UpdateBillStatus(billId, "Cancelled");
            if (!ok)
            {
                errorMessage = "Database failed to cancel bill.";
                return false;
            }

            return true;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Financial Validation Rules
        // ─────────────────────────────────────────────────────────────────────────

        public bool ValidateBill(Bill bill, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (bill.PatientID <= 0)
            {
                errorMessage = "A valid Patient must be selected.";
                return false;
            }

            try
            {
                var patient = _patientRepo.GetPatientById(bill.PatientID);
                if (patient != null && !patient.IsActive)
                {
                    errorMessage = "The selected patient is invalid or inactive.";
                    return false;
                }
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {
                // Proceed with validation if DB is offline
            }

            if (bill.Subtotal < 0)
            {
                errorMessage = "Bill Subtotal cannot be negative.";
                return false;
            }

            if (bill.Discount < 0)
            {
                errorMessage = "Discount cannot be negative.";
                return false;
            }

            if (bill.Discount > bill.Subtotal)
            {
                errorMessage = $"Discount (Rs. {bill.Discount:N2}) cannot exceed the Subtotal (Rs. {bill.Subtotal:N2}).";
                return false;
            }

            return true;
        }
    }
}
