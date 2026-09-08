using System;
using System.Collections.Generic;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Repositories;

namespace MEDICARE_HOSPITAL_MANGAMENT.Services
{
    /// <summary>
    /// Business Logic Layer for Cashier Payments.
    /// Enforces Business Rule BR-10 (Strict Overpayment Prevention, Amount > 0),
    /// cashier authorization, and atomic transaction updates.
    /// </summary>
    public class PaymentService
    {
        private readonly PaymentRepository _paymentRepo;
        private readonly BillRepository _billRepo;

        public PaymentService()
        {
            _paymentRepo = new PaymentRepository();
            _billRepo = new BillRepository();
        }

        public PaymentService(PaymentRepository paymentRepo, BillRepository billRepo)
        {
            _paymentRepo = paymentRepo;
            _billRepo = billRepo;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Read Pass-Throughs
        // ─────────────────────────────────────────────────────────────────────────

        public List<Payment> GetPaymentsByBillId(int billId) => _paymentRepo.GetPaymentsByBillId(billId);
        public Payment? GetPaymentById(int paymentId) => _paymentRepo.GetPaymentById(paymentId);
        public decimal GetTotalPaidForBill(int billId) => _paymentRepo.GetTotalPaidForBill(billId);
        public List<Payment> GetAllPayments(int limit = 100) => _paymentRepo.GetAllPayments(limit);
        public string GenerateNextReferenceNo() => _paymentRepo.GenerateNextReferenceNo();

        // ─────────────────────────────────────────────────────────────────────────
        // Payment Processing & BR-10 Enforcement
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Processes a cashier payment against a bill.
        /// Strictly validates BR-10: Amount > 0 and Amount <= OutstandingBalance.
        /// Records payment and transitions bill status atomically.
        /// </summary>
        /// <summary>
        /// Validates BR-10 financial payment constraints against the bill's current balance.
        /// </summary>
        public bool ValidatePayment(Payment payment, decimal outstandingBalance, out string errorMessage)
        {
            errorMessage = string.Empty;

            // BR-10: Payment amount must be strictly greater than 0
            if (payment.Amount <= 0)
            {
                errorMessage = "Payment amount must be strictly greater than Rs. 0.00.";
                return false;
            }

            // BR-10: Overpayment prevention: cannot exceed bill's outstanding balance
            if (payment.Amount > outstandingBalance)
            {
                errorMessage = $"Payment amount (Rs. {payment.Amount:N2}) exceeds outstanding balance (Rs. {outstandingBalance:N2}).";
                return false;
            }

            string method = payment.PaymentMethod?.Trim() ?? "";
            if (method != "Cash" && method != "Card" && method != "BankTransfer")
            {
                errorMessage = "Invalid payment method. Allowed methods: Cash, Card, BankTransfer.";
                return false;
            }

            return true;
        }

        public int ProcessPayment(Payment payment, out string errorMessage)
        {
            errorMessage = string.Empty;

            // Authorization: Cashier or Administrator only
            if (!SessionManager.IsCashier() && !SessionManager.IsAdmin())
            {
                errorMessage = "Access denied. Only Cashiers and Administrators can collect payments.";
                return 0;
            }

            if (payment.Amount <= 0)
            {
                errorMessage = "Payment amount must be strictly greater than Rs. 0.00.";
                return 0;
            }

            if (payment.BillID <= 0)
            {
                errorMessage = "A valid Bill must be specified.";
                return 0;
            }

            Bill? bill = null;
            try
            {
                bill = _billRepo.GetBillById(payment.BillID);
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                errorMessage = $"Database error: {ex.Message}";
                return 0;
            }

            if (bill == null)
            {
                errorMessage = "Bill not found.";
                return 0;
            }

            if (bill.Status == "Paid")
            {
                errorMessage = "This bill has already been settled in full. No balance is due.";
                return 0;
            }

            if (bill.Status == "Cancelled")
            {
                errorMessage = "Cannot accept payment for a cancelled bill.";
                return 0;
            }

            if (!ValidatePayment(payment, bill.OutstandingBalance, out errorMessage))
            {
                return 0;
            }

            // Assign current cashier if not provided
            if (payment.ReceivedByUserID <= 0)
            {
                payment.ReceivedByUserID = SessionManager.CurrentUserId;
            }

            // Generate reference if empty
            if (string.IsNullOrWhiteSpace(payment.ReferenceNo))
            {
                payment.ReferenceNo = _paymentRepo.GenerateNextReferenceNo();
            }

            // Execute atomic database transaction
            try
            {
                int paymentId = _paymentRepo.RecordPayment(payment);
                if (paymentId <= 0)
                {
                    errorMessage = "Database failed to record payment.";
                    return 0;
                }

                return paymentId;
            }
            catch (Exception ex)
            {
                errorMessage = $"Payment processing failed: {ex.Message}";
                return 0;
            }
        }
    }
}
