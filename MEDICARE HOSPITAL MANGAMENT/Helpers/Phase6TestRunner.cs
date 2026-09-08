using System;
using System.Collections.Generic;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Services;

namespace MEDICARE_HOSPITAL_MANGAMENT.Helpers
{
    /// <summary>
    /// Automated Test Suite for Phase 6:
    /// Module 8 (Hospital Analytical Dashboard & Reporting) &
    /// Module 10 (Master Application Shell & Role-Based Access Control).
    /// </summary>
    public static class Phase6TestRunner
    {
        private static int _passed = 0;
        private static int _failed = 0;

        public static int RunAllTests()
        {
            _passed = 0;
            _failed = 0;

            Console.WriteLine("================================================================================");
            Console.WriteLine("MEDICARE HOSPITAL SYSTEM - PHASE 6 AUTOMATED VERIFICATION SUITE");
            Console.WriteLine("Modules: Analytical Dashboard (Prompt 8) & Master Shell / RBAC (Prompt 10)");
            Console.WriteLine("================================================================================");
            Console.WriteLine();

            // 1. Dashboard Metrics Tests
            TestDashboardMetricsCalculation();

            // 2. Report Data Model Tests
            TestStockReportCalculations();
            TestRevenueReportCalculations();
            TestDoctorWorkloadModel();

            // 3. Report Service Date Range Validation Tests
            TestDateRangeValidation();

            // 4. CSV Export Formatting Tests
            TestCsvExportFormatting();

            // 5. Dashboard Inventory Alerts Tests
            TestInventoryAlertNotices();

            // 6. Role-Based Access Control (RBAC) Matrix Tests
            TestRoleBasedAccessMatrix();

            // 7. Offline Database Service Resilience Tests
            TestServiceResilienceWhenDatabaseOffline();

            Console.WriteLine();
            Console.WriteLine("================================================================================");
            Console.WriteLine($"PHASE 6 TEST RESULTS: {_passed} PASSED, {_failed} FAILED (TOTAL: {_passed + _failed})");
            Console.WriteLine("================================================================================");

            return _failed > 0 ? 1 : 0;
        }

        private static void Assert(string testName, bool condition, string? failureMessage = null)
        {
            if (condition)
            {
                Console.WriteLine($"  [PASS] {testName}");
                _passed++;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  [FAIL] {testName}: {failureMessage ?? "Assertion evaluated to false"}");
                Console.ResetColor();
                _failed++;
            }
        }

        // ─────────────────────────────────────────────────────────────────────────
        // 1. Dashboard Metrics Calculations
        // ─────────────────────────────────────────────────────────────────────────
        private static void TestDashboardMetricsCalculation()
        {
            Console.WriteLine("--- 1. Dashboard Metrics & KPI Calculations ---");

            var mZero = new DashboardMetrics
            {
                TodayAppointments = 0,
                CompletedAppointmentsToday = 0
            };
            Assert("Zero appointments completion rate is 0.0%", mZero.AppointmentCompletionRate == 0.0);

            var mPartial = new DashboardMetrics
            {
                TodayAppointments = 10,
                CompletedAppointmentsToday = 7
            };
            Assert("10 appointments with 7 completed yields 70.0%", Math.Abs(mPartial.AppointmentCompletionRate - 70.0) < 0.001);

            var mFull = new DashboardMetrics
            {
                TodayAppointments = 25,
                CompletedAppointmentsToday = 25
            };
            Assert("25 appointments with 25 completed yields 100.0%", Math.Abs(mFull.AppointmentCompletionRate - 100.0) < 0.001);
        }

        // ─────────────────────────────────────────────────────────────────────────
        // 2. Stock Report Valuation & Thresholds
        // ─────────────────────────────────────────────────────────────────────────
        private static void TestStockReportCalculations()
        {
            Console.WriteLine("--- 2. Stock Report Item Valuation & Thresholds ---");

            var item1 = new StockReportItem
            {
                MedicineCode = "MED-001",
                MedicineName = "Amoxicillin 500mg",
                UnitPrice = 15.50m,
                StockQuantity = 100,
                ReorderLevel = 20
            };
            Assert("Stock valuation (100 * 15.50) equals 1550.00", item1.TotalStockValue == 1550.00m);
            Assert("Stock 100 > Reorder 20 is safe", item1.StockQuantity > item1.ReorderLevel);

            var itemLow = new StockReportItem
            {
                MedicineCode = "MED-002",
                MedicineName = "Paracetamol 500mg",
                UnitPrice = 5.00m,
                StockQuantity = 15,
                ReorderLevel = 20
            };
            Assert("Stock 15 <= Reorder 20 is identified as low stock", itemLow.StockQuantity <= itemLow.ReorderLevel);
            Assert("Low stock valuation (15 * 5.00) equals 75.00", itemLow.TotalStockValue == 75.00m);
        }

        // ─────────────────────────────────────────────────────────────────────────
        // 3. Revenue Report Calculations
        // ─────────────────────────────────────────────────────────────────────────
        private static void TestRevenueReportCalculations()
        {
            Console.WriteLine("--- 3. Revenue Report Item Outstanding Balance ---");

            var bill1 = new RevenueReportItem
            {
                BillID = 101,
                TotalAmount = 5000m,
                TotalPaid = 2000m
            };
            Assert("Outstanding balance (5000 - 2000) equals 3000.00", bill1.OutstandingBalance == 3000.00m);

            var billFullyPaid = new RevenueReportItem
            {
                BillID = 102,
                TotalAmount = 3500m,
                TotalPaid = 3500m
            };
            Assert("Fully paid bill outstanding balance is 0.00", billFullyPaid.OutstandingBalance == 0.00m);

            var billZeroPaid = new RevenueReportItem
            {
                BillID = 103,
                TotalAmount = 1200m,
                TotalPaid = 0m
            };
            Assert("Unpaid bill outstanding balance equals TotalAmount", billZeroPaid.OutstandingBalance == 1200.00m);
        }

        // ─────────────────────────────────────────────────────────────────────────
        // 4. Doctor Workload Data Integrity
        // ─────────────────────────────────────────────────────────────────────────
        private static void TestDoctorWorkloadModel()
        {
            Console.WriteLine("--- 4. Doctor Workload Analytical Item Model ---");

            var doc = new DoctorWorkloadReportItem
            {
                DoctorCode = "DOC-001",
                DoctorName = "Dr. John Watson",
                DepartmentName = "Cardiology",
                TotalAppointments = 15,
                CompletedConsultations = 12,
                CancelledAppointments = 2
            };

            Assert("Doctor workload captures doctor identification correctly", doc.DoctorCode == "DOC-001" && doc.DoctorName == "Dr. John Watson");
            Assert("Doctor workload counts match input", doc.TotalAppointments == 15 && doc.CompletedConsultations == 12 && doc.CancelledAppointments == 2);
        }

        // ─────────────────────────────────────────────────────────────────────────
        // 5. Date Range Validation
        // ─────────────────────────────────────────────────────────────────────────
        private static void TestDateRangeValidation()
        {
            Console.WriteLine("--- 5. Report Date Range Validation ---");

            var reportService = new ReportService();

            var today = DateTime.Today;
            bool validRange = reportService.ValidateDateRange(today.AddDays(-7), today, out string err1);
            Assert("Valid date range (last 7 days) passes validation", validRange && string.IsNullOrEmpty(err1));

            bool sameDay = reportService.ValidateDateRange(today, today, out string err2);
            Assert("Single day range (today to today) passes validation", sameDay && string.IsNullOrEmpty(err2));

            bool invalidRange = reportService.ValidateDateRange(today, today.AddDays(-1), out string err3);
            Assert("Reversed date range (from > to) fails validation with correct error", !invalidRange && err3.Contains("cannot be after End Date"));
        }

        // ─────────────────────────────────────────────────────────────────────────
        // 6. CSV Export Formatting
        // ─────────────────────────────────────────────────────────────────────────
        private static void TestCsvExportFormatting()
        {
            Console.WriteLine("--- 6. CSV Export Data Serialization & Escaping ---");

            var reportService = new ReportService();

            string[] headers = new[] { "Code", "Name", "Category", "UnitPrice" };
            var rows = new List<string[]>
            {
                new[] { "MED-001", "Amoxicillin, 500mg", "Antibiotic", "15.50" },
                new[] { "MED-002", "Paracetamol \"Extra\"", "Analgesic", "5.00" }
            };

            string csv = reportService.ExportToCsv("Test Pharmacy Stock", headers, rows);

            Assert("CSV contains MediCare title metadata header", csv.Contains("# MediCare Hospital Management System - Test Pharmacy Stock"));
            Assert("CSV contains escaped comma field \"\"Amoxicillin, 500mg\"\"", csv.Contains("\"Amoxicillin, 500mg\""));
            Assert("CSV contains escaped double quote field \"\"Paracetamol \"\"Extra\"\"\"\"", csv.Contains("\"Paracetamol \"\"Extra\"\"\""));
        }

        // ─────────────────────────────────────────────────────────────────────────
        // 7. Inventory Alert Notices
        // ─────────────────────────────────────────────────────────────────────────
        private static void TestInventoryAlertNotices()
        {
            Console.WriteLine("--- 7. Inventory Alert Generation ---");

            var dashboardService = new DashboardService();

            var safeMetrics = new DashboardMetrics { LowStockMedicinesCount = 0 };
            string safeNotice = dashboardService.GetInventoryAlertNotice(safeMetrics);
            Assert("Zero low stock items generates safe operational notice", safeNotice.Contains("within safe operational limits"));

            var alertMetrics = new DashboardMetrics { LowStockMedicinesCount = 4 };
            string alertNotice = dashboardService.GetInventoryAlertNotice(alertMetrics);
            Assert("4 low stock items triggers urgent shortage notice", alertNotice.Contains("4 medication(s) are at or below reorder threshold"));
        }

        // ─────────────────────────────────────────────────────────────────────────
        // 8. Role-Based Access Control Matrix
        // ─────────────────────────────────────────────────────────────────────────
        private static void TestRoleBasedAccessMatrix()
        {
            Console.WriteLine("--- 8. Role-Based Access Control (RBAC) Matrix ---");

            // Test Administrator Role
            SessionManager.CurrentUser = new User { UserID = 1, Username = "admin", FullName = "Admin User", RoleName = "Administrator" };
            Assert("Administrator role matches IsAdmin()", SessionManager.IsAdmin());
            Assert("Administrator role does not evaluate to Doctor", !SessionManager.IsDoctor());
            Assert("Administrator role does not evaluate to Pharmacist", !SessionManager.IsPharmacist());

            // Test Doctor Role
            SessionManager.CurrentUser = new User { UserID = 2, Username = "doctor1", FullName = "Dr. Jane", RoleName = "Doctor" };
            Assert("Doctor role matches IsDoctor()", SessionManager.IsDoctor());
            Assert("Doctor role does not evaluate to Admin", !SessionManager.IsAdmin());
            Assert("Doctor role does not evaluate to Cashier", !SessionManager.IsCashier());

            // Test Receptionist Role
            SessionManager.CurrentUser = new User { UserID = 3, Username = "reception1", FullName = "Bob Smith", RoleName = "Receptionist" };
            Assert("Receptionist role matches IsReceptionist()", SessionManager.IsReceptionist());
            Assert("Receptionist does not evaluate to Doctor", !SessionManager.IsDoctor());

            // Test Pharmacist Role
            SessionManager.CurrentUser = new User { UserID = 4, Username = "pharm1", FullName = "Alice Cole", RoleName = "Pharmacist" };
            Assert("Pharmacist role matches IsPharmacist()", SessionManager.IsPharmacist());
            Assert("Pharmacist does not evaluate to Cashier", !SessionManager.IsCashier());

            // Test Cashier Role
            SessionManager.CurrentUser = new User { UserID = 5, Username = "cashier1", FullName = "Charlie Doe", RoleName = "Cashier" };
            Assert("Cashier role matches IsCashier()", SessionManager.IsCashier());
            Assert("Cashier does not evaluate to Pharmacist", !SessionManager.IsPharmacist());

            // Reset Session
            SessionManager.Logout();
            Assert("Logout clears active session", !SessionManager.IsLoggedIn && SessionManager.CurrentUser == null);
        }

        // ─────────────────────────────────────────────────────────────────────────
        // 9. Offline Database Resilience
        // ─────────────────────────────────────────────────────────────────────────
        private static void TestServiceResilienceWhenDatabaseOffline()
        {
            Console.WriteLine("--- 9. Offline Database Graceful Degradation ---");

            var dashService = new DashboardService();
            var reportService = new ReportService();

            // When DB is unreachable, services should return safe defaults without throwing unhandled exceptions
            var metrics = dashService.GetDashboardMetrics();
            Assert("DashboardService.GetDashboardMetrics returns non-null safe object when offline", metrics != null);

            var todayAppts = dashService.GetTodayAppointments();
            Assert("DashboardService.GetTodayAppointments returns non-null list when offline", todayAppts != null);

            var apptReport = reportService.GenerateAppointmentReport(DateTime.Today, DateTime.Today, null, null, out string _);
            Assert("ReportService.GenerateAppointmentReport returns empty list without crash when offline", apptReport != null);

            var stockReport = reportService.GenerateStockReport(false, null);
            Assert("ReportService.GenerateStockReport returns empty list without crash when offline", stockReport != null);

            var revReport = reportService.GenerateRevenueReport(DateTime.Today, DateTime.Today, null, out string _);
            Assert("ReportService.GenerateRevenueReport returns empty list without crash when offline", revReport != null);

            var docReport = reportService.GenerateDoctorWorkloadReport(DateTime.Today, DateTime.Today, out string _);
            Assert("ReportService.GenerateDoctorWorkloadReport returns empty list without crash when offline", docReport != null);
        }
    }
}
