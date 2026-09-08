using System;
using System.Collections.Generic;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Services;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;

namespace MEDICARE_HOSPITAL_MANGAMENT.Helpers
{
    /// <summary>
    /// Automated test suite for Phase 5 (Pharmacy & Billing).
    /// Tests domain validation, financial equations, stock status rules,
    /// and BR-10 overpayment prevention.
    /// </summary>
    public static class Phase5TestRunner
    {
        public static int RunAllTests()
        {
            int passed = 0;
            int failed = 0;

            Console.WriteLine("==================================================================");
            Console.WriteLine("🧪 RUNNING AUTOMATED TESTS: PHASE 5 (PHARMACY & BILLING)");
            Console.WriteLine("==================================================================");

            void Assert(string testName, bool condition, string? failureDetails = null)
            {
                if (condition)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("  [PASS] ");
                    Console.ResetColor();
                    Console.WriteLine(testName);
                    passed++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("  [FAIL] ");
                    Console.ResetColor();
                    Console.WriteLine($"{testName} => {failureDetails}");
                    failed++;
                }
            }

            // ─── Test Group 1: Medicine Model & Validation ──────────────────
            Console.WriteLine("\n--- 1. Medicine Model & Stock Calculations ---");
            {
                var med = new Medicine
                {
                    MedicineID = 1,
                    MedicineCode = "MED001",
                    MedicineName = "Paracetamol 500mg",
                    Unit = "Tablet",
                    UnitPrice = 10.00m,
                    StockQuantity = 5,
                    ReorderLevel = 10,
                    ExpiryDate = DateTime.Today.AddDays(100)
                };

                Assert("Medicine.IsLowStock returns true when Stock <= ReorderLevel", med.IsLowStock);
                Assert("Medicine.StockStatusText returns 'Low Stock'", med.StockStatusText == "Low Stock");

                med.StockQuantity = 0;
                Assert("Medicine.StockStatusText returns 'Out of Stock' when Stock is 0", med.StockStatusText == "Out of Stock");

                med.StockQuantity = 50;
                Assert("Medicine.IsLowStock returns false when Stock > ReorderLevel", !med.IsLowStock);
                Assert("Medicine.StockStatusText returns 'In Stock'", med.StockStatusText == "In Stock");

                med.ExpiryDate = DateTime.Today.AddDays(-1);
                Assert("Medicine.IsExpired returns true for past expiry date", med.IsExpired);

                med.ExpiryDate = DateTime.Today.AddYears(1);
                Assert("Medicine.IsExpired returns false for future expiry date", !med.IsExpired);

                // Service validation tests
                var medService = new MedicineService();

                var invalidMed1 = new Medicine { MedicineCode = "", MedicineName = "Aspirin", Unit = "Tablet" };
                Assert("Rejects empty MedicineCode", !medService.ValidateMedicine(invalidMed1, false, out var err1) && err1.Contains("Code is required"));

                var invalidMed2 = new Medicine { MedicineCode = "MED999", MedicineName = "", Unit = "Tablet" };
                Assert("Rejects empty MedicineName", !medService.ValidateMedicine(invalidMed2, false, out var err2) && err2.Contains("Name is required"));

                var invalidMed3 = new Medicine { MedicineCode = "MED999", MedicineName = "Test", Unit = "", UnitPrice = 10m };
                Assert("Rejects empty Unit", !medService.ValidateMedicine(invalidMed3, false, out var err3) && err3.Contains("Unit"));

                var invalidMed4 = new Medicine { MedicineCode = "MED999", MedicineName = "Test", Unit = "Tab", UnitPrice = -5m };
                Assert("Rejects negative UnitPrice", !medService.ValidateMedicine(invalidMed4, false, out var err4) && err4.Contains("Price cannot be negative"));

                var invalidMed5 = new Medicine { MedicineCode = "MED999", MedicineName = "Test", Unit = "Tab", UnitPrice = 5m, StockQuantity = -10 };
                Assert("Rejects negative StockQuantity", !medService.ValidateMedicine(invalidMed5, false, out var err5) && err5.Contains("Stock Quantity cannot be negative"));
            }

            // ─── Test Group 2: Prescription Model & Clinical Rules ──────────
            Console.WriteLine("\n--- 2. Prescription Model & Clinical Rules ---");
            {
                var rx = new Prescription
                {
                    PrescriptionID = 100,
                    PatientID = 1,
                    DoctorID = 1,
                    Items = new List<PrescriptionItem>
                    {
                        new() { MedicineID = 1, MedicineName = "Paracetamol", Quantity = 10, UnitPrice = 5.00m, Dosage = "500mg", Frequency = "TDS", DurationDays = 3, AvailableStock = 50 },
                        new() { MedicineID = 2, MedicineName = "Amoxicillin", Quantity = 6, UnitPrice = 20.00m, Dosage = "250mg", Frequency = "BD", DurationDays = 3, AvailableStock = 2 }
                    }
                };

                Assert("Prescription.TotalItemsCount counts line items correctly", rx.TotalItemsCount == 2);
                Assert("Prescription.EstimatedTotalCost calculates sum (10*5 + 6*20 = 170)", rx.EstimatedTotalCost == 170.00m);
                Assert("PrescriptionItem.TotalPrice calculates unit price * quantity (6*20 = 120)", rx.Items[1].TotalPrice == 120.00m);
                Assert("PrescriptionItem.HasSufficientStock is true when available >= quantity", rx.Items[0].HasSufficientStock);
                Assert("PrescriptionItem.HasSufficientStock is false when available < quantity", !rx.Items[1].HasSufficientStock);
                Assert("Prescription.AllItemsInStock returns false when any item lacks stock", !rx.AllItemsInStock);

                var rxService = new PrescriptionService();
                var emptyRx = new Prescription { PatientID = 1, DoctorID = 1, Items = new List<PrescriptionItem>() };
                Assert("Prescription validation requires >= 1 line item", !rxService.ValidatePrescription(emptyRx, out var rxErr1) && rxErr1.Contains("at least one medication"));

                var invalidQtyRx = new Prescription
                {
                    PatientID = 1,
                    DoctorID = 1,
                    Items = new List<PrescriptionItem>
                    {
                        new() { MedicineID = 1, Quantity = 0, DurationDays = 5, Dosage = "500mg", Frequency = "OD" }
                    }
                };
                Assert("Prescription validation rejects Quantity <= 0", !rxService.ValidatePrescription(invalidQtyRx, out var rxErr2) && rxErr2.Contains("Quantity must be greater than 0"));

                var invalidDurationRx = new Prescription
                {
                    PatientID = 1,
                    DoctorID = 1,
                    Items = new List<PrescriptionItem>
                    {
                        new() { MedicineID = 1, Quantity = 10, DurationDays = 0, Dosage = "500mg", Frequency = "OD" }
                    }
                };
                Assert("Prescription validation rejects DurationDays <= 0", !rxService.ValidatePrescription(invalidDurationRx, out var rxErr3) && rxErr3.Contains("Duration (days) must be greater than 0"));

                var invalidDosageRx = new Prescription
                {
                    PatientID = 1,
                    DoctorID = 1,
                    Items = new List<PrescriptionItem>
                    {
                        new() { MedicineID = 1, Quantity = 10, DurationDays = 5, Dosage = "", Frequency = "OD" }
                    }
                };
                Assert("Prescription validation rejects empty Dosage", !rxService.ValidatePrescription(invalidDosageRx, out var rxErr4) && rxErr4.Contains("Dosage specification is mandatory"));
            }

            // ─── Test Group 3: Billing & Invoicing Financial Rules ──────────
            Console.WriteLine("\n--- 3. Billing & Financial Rules ---");
            {
                var bill = new Bill
                {
                    BillID = 50,
                    PatientID = 1,
                    Subtotal = 5000.00m,
                    Discount = 500.00m,
                    TotalAmount = 4500.00m,
                    TotalPaid = 2000.00m
                };

                Assert("Bill.OutstandingBalance = TotalAmount - TotalPaid (4500 - 2000 = 2500)", bill.OutstandingBalance == 2500.00m);
                Assert("Bill.IsFullyPaid is false when balance > 0", !bill.IsFullyPaid);

                bill.TotalPaid = 4500.00m;
                Assert("Bill.OutstandingBalance is 0 when fully paid", bill.OutstandingBalance == 0.00m);
                Assert("Bill.IsFullyPaid is true when TotalPaid >= TotalAmount", bill.IsFullyPaid);

                var billingService = new BillingService();

                var invalidBill1 = new Bill { PatientID = 1, Subtotal = -100m, Discount = 0m };
                Assert("Rejects negative Subtotal", !billingService.ValidateBill(invalidBill1, out var billErr1) && billErr1.Contains("Subtotal cannot be negative"));

                var invalidBill2 = new Bill { PatientID = 1, Subtotal = 1000m, Discount = -50m };
                Assert("Rejects negative Discount", !billingService.ValidateBill(invalidBill2, out var billErr2) && billErr2.Contains("Discount cannot be negative"));

                var invalidBill3 = new Bill { PatientID = 1, Subtotal = 1000m, Discount = 1500m };
                Assert("Rejects Discount exceeding Subtotal", !billingService.ValidateBill(invalidBill3, out var billErr3) && billErr3.Contains("cannot exceed the Subtotal"));

                var invalidBill4 = new Bill { PatientID = 0, Subtotal = 1000m, Discount = 100m };
                Assert("Rejects missing PatientID", !billingService.ValidateBill(invalidBill4, out var billErr4) && billErr4.Contains("Patient must be selected"));
            }

            // ─── Test Group 4: Payment Rules & BR-10 Overpayment Prevention ─
            Console.WriteLine("\n--- 4. Payment Rules & BR-10 Overpayment Prevention ---");
            {
                var paymentService = new PaymentService();

                var paymentZero = new Payment { BillID = 1, Amount = 0m, PaymentMethod = "Cash" };
                Assert("BR-10: Payment amount <= 0 is strictly rejected",
                    !paymentService.ValidatePayment(paymentZero, 1000m, out var payErr1) && payErr1.Contains("greater than Rs. 0.00"));

                var paymentNeg = new Payment { BillID = 1, Amount = -100m, PaymentMethod = "Cash" };
                Assert("BR-10: Negative payment is strictly rejected",
                    !paymentService.ValidatePayment(paymentNeg, 1000m, out var payErr2) && payErr2.Contains("greater than Rs. 0.00"));

                var paymentOver = new Payment { BillID = 1, Amount = 1500m, PaymentMethod = "Cash" };
                Assert("BR-10: Overpayment (paying Rs. 1500 on Rs. 1000 balance) is strictly rejected",
                    !paymentService.ValidatePayment(paymentOver, 1000m, out var payErr3) && payErr3.Contains("exceeds outstanding balance"));

                var paymentValidPartial = new Payment { BillID = 1, Amount = 500m, PaymentMethod = "Card" };
                Assert("BR-10: Valid partial payment (Rs. 500 on Rs. 1000 balance) is accepted",
                    paymentService.ValidatePayment(paymentValidPartial, 1000m, out _));

                var paymentValidFull = new Payment { BillID = 1, Amount = 1000m, PaymentMethod = "Cash" };
                Assert("BR-10: Valid full settlement (Rs. 1000 on Rs. 1000 balance) is accepted",
                    paymentService.ValidatePayment(paymentValidFull, 1000m, out _));

                var paymentInvalidMethod = new Payment { BillID = 1, Amount = 500m, PaymentMethod = "Crypto" };
                Assert("Rejects invalid payment method ('Crypto')",
                    !paymentService.ValidatePayment(paymentInvalidMethod, 1000m, out var payErr4) && payErr4.Contains("Invalid payment method"));
            }

            // ─── Test Group 5: Database Connection & Seed Data Check ───────
            Console.WriteLine("\n--- 5. Database Connection & System Health ---");
            {
                bool dbReachable = DatabaseHelper.TestConnection(out string dbErr);
                if (dbReachable)
                {
                    Assert("Database connection to MediCareDB succeeded", true);

                    var medRepo = new Repositories.MedicineRepository();
                    var meds = medRepo.GetAllMedicines(activeOnly: false);
                    Assert($"Medicines inventory contains records (count: {meds.Count})", meds.Count > 0);

                    var lowStock = medRepo.GetLowStockMedicines();
                    Console.WriteLine($"      -> Found {lowStock.Count} medicine(s) at or below reorder level.");

                    var billRepo = new Repositories.BillRepository();
                    var pendingBills = billRepo.GetPendingBills();
                    Assert($"BillRepository queries successfully (pending bills count: {pendingBills.Count})", true);

                    var payRepo = new Repositories.PaymentRepository();
                    string nextRef = payRepo.GenerateNextReferenceNo();
                    Assert($"PaymentRepository generated next receipt reference: '{nextRef}'", !string.IsNullOrWhiteSpace(nextRef));
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"  [SKIP] SQL Server connection skipped: {dbErr}");
                    Console.WriteLine("         (Database connection not active on localhost; unit validation completed offline)");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("\n==================================================================");
            Console.ForegroundColor = failed == 0 ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"TEST RESULTS: {passed} PASSED, {failed} FAILED");
            Console.ResetColor();
            Console.WriteLine("==================================================================");

            return failed;
        }
    }
}
