using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;

namespace MEDICARE_HOSPITAL_MANGAMENT.Repositories
{
    /// <summary>
    /// Data Access Repository for Executive Analytics and Operational Reporting.
    /// Executes parameterized aggregate queries across all 12 database tables.
    /// </summary>
    public class ReportRepository
    {
        // ─────────────────────────────────────────────────────────────────────────
        // Dashboard Metrics
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Retrieves real-time operational KPI counts and revenue metrics for the dashboard.
        /// </summary>
        public DashboardMetrics GetDashboardMetrics()
        {
            var metrics = new DashboardMetrics();
            const string query = @"
                SELECT
                    (SELECT COUNT(1) FROM dbo.Patients WHERE IsActive = 1) AS TotalPatients,
                    (SELECT COUNT(1) FROM dbo.Appointments WHERE AppointmentDate = CAST(GETDATE() AS DATE)) AS TodayAppointments,
                    (SELECT COUNT(1) FROM dbo.Appointments WHERE AppointmentDate = CAST(GETDATE() AS DATE) AND Status = 'Completed') AS CompletedAppointmentsToday,
                    (SELECT COUNT(1) FROM dbo.Doctors WHERE IsActive = 1) AS AvailableDoctorsCount,
                    (SELECT COUNT(1) FROM dbo.Medicines WHERE StockQuantity <= ReorderLevel AND IsActive = 1) AS LowStockMedicinesCount,
                    (SELECT COUNT(1) FROM dbo.Bills WHERE Status IN ('Pending', 'PartiallyPaid')) AS PendingBillsCount,
                    (SELECT ISNULL(SUM(Amount), 0.00) FROM dbo.Payments WHERE CAST(PaymentDate AS DATE) = CAST(GETDATE() AS DATE)) AS TodayRevenue,
                    ISNULL(
                        (SELECT ISNULL(SUM(b.TotalAmount), 0.00) FROM dbo.Bills b WHERE b.Status IN ('Pending', 'PartiallyPaid')) -
                        (SELECT ISNULL(SUM(p.Amount), 0.00) FROM dbo.Payments p INNER JOIN dbo.Bills b2 ON p.BillID = b2.BillID WHERE b2.Status IN ('Pending', 'PartiallyPaid')),
                        0.00
                    ) AS PendingBillsTotalDue,
                    (SELECT COUNT(1) FROM dbo.Appointments WHERE AppointmentDate >= CAST(GETDATE() AS DATE) AND Status = 'Scheduled') AS UpcomingAppointmentsCount;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                metrics.TotalPatients              = reader.GetInt32(0);
                metrics.TodayAppointments          = reader.GetInt32(1);
                metrics.CompletedAppointmentsToday = reader.GetInt32(2);
                metrics.AvailableDoctorsCount      = reader.GetInt32(3);
                metrics.LowStockMedicinesCount     = reader.GetInt32(4);
                metrics.PendingBillsCount          = reader.GetInt32(5);
                metrics.TodayRevenue               = reader.GetDecimal(6);
                metrics.PendingBillsTotalDue       = reader.GetDecimal(7);
                metrics.UpcomingAppointmentsCount  = reader.GetInt32(8);
            }

            return metrics;
        }

        /// <summary>
        /// Retrieves today's and upcoming appointments for immediate dashboard viewing.
        /// </summary>
        public List<AppointmentReportItem> GetTodayAppointmentsList()
        {
            var list = new List<AppointmentReportItem>();
            const string query = @"
                SELECT a.AppointmentID, a.AppointmentDate,
                       CONVERT(VARCHAR(5), a.StartTime, 108) + ' - ' + CONVERT(VARCHAR(5), a.EndTime, 108) AS TimeSlot,
                       p.PatientCode, p.FirstName + ' ' + p.LastName AS PatientName,
                       'Dr. ' + d.FirstName + ' ' + d.LastName AS DoctorName,
                       dep.DepartmentName, a.Status, a.Reason
                FROM dbo.Appointments a
                INNER JOIN dbo.Patients p ON a.PatientID = p.PatientID
                INNER JOIN dbo.Doctors d ON a.DoctorID = d.DoctorID
                INNER JOIN dbo.Departments dep ON d.DepartmentID = dep.DepartmentID
                WHERE a.AppointmentDate >= CAST(GETDATE() AS DATE)
                ORDER BY a.AppointmentDate ASC, a.StartTime ASC;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new AppointmentReportItem
                {
                    AppointmentID  = reader.GetInt32(0),
                    Date           = reader.GetDateTime(1),
                    TimeSlot       = reader.GetString(2),
                    PatientCode    = reader.GetString(3),
                    PatientName    = reader.GetString(4),
                    DoctorName     = reader.GetString(5),
                    DepartmentName = reader.GetString(6),
                    Status         = reader.GetString(7),
                    Reason         = reader.IsDBNull(8) ? null : reader.GetString(8)
                });
            }

            return list;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Detailed Analytical Reports
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Generates multi-criteria appointment report filtered by date range, doctor, and status.
        /// </summary>
        public List<AppointmentReportItem> GetAppointmentReport(DateTime fromDate, DateTime toDate, int? doctorId = null, string? status = null)
        {
            var list = new List<AppointmentReportItem>();
            string query = @"
                SELECT a.AppointmentID, a.AppointmentDate,
                       CONVERT(VARCHAR(5), a.StartTime, 108) + ' - ' + CONVERT(VARCHAR(5), a.EndTime, 108) AS TimeSlot,
                       p.PatientCode, p.FirstName + ' ' + p.LastName AS PatientName,
                       'Dr. ' + d.FirstName + ' ' + d.LastName AS DoctorName,
                       dep.DepartmentName, a.Status, a.Reason
                FROM dbo.Appointments a
                INNER JOIN dbo.Patients p ON a.PatientID = p.PatientID
                INNER JOIN dbo.Doctors d ON a.DoctorID = d.DoctorID
                INNER JOIN dbo.Departments dep ON d.DepartmentID = dep.DepartmentID
                WHERE a.AppointmentDate >= @FromDate AND a.AppointmentDate <= @ToDate
                  AND (@DoctorID IS NULL OR a.DoctorID = @DoctorID)
                  AND (@Status IS NULL OR a.Status = @Status)
                ORDER BY a.AppointmentDate DESC, a.StartTime ASC;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@FromDate", fromDate.Date);
            cmd.Parameters.AddWithValue("@ToDate", toDate.Date);
            cmd.Parameters.AddWithValue("@DoctorID", (object?)doctorId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", (object?)status ?? DBNull.Value);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new AppointmentReportItem
                {
                    AppointmentID  = reader.GetInt32(0),
                    Date           = reader.GetDateTime(1),
                    TimeSlot       = reader.GetString(2),
                    PatientCode    = reader.GetString(3),
                    PatientName    = reader.GetString(4),
                    DoctorName     = reader.GetString(5),
                    DepartmentName = reader.GetString(6),
                    Status         = reader.GetString(7),
                    Reason         = reader.IsDBNull(8) ? null : reader.GetString(8)
                });
            }

            return list;
        }

        /// <summary>
        /// Generates pharmacy inventory stock and reorder valuation report.
        /// </summary>
        public List<StockReportItem> GetStockReport(bool lowStockOnly = false, string? category = null)
        {
            var list = new List<StockReportItem>();
            string query = @"
                SELECT MedicineCode, MedicineName, ISNULL(Category, 'General') AS Category,
                       Unit, UnitPrice, StockQuantity, ReorderLevel, ExpiryDate,
                       CASE
                           WHEN StockQuantity = 0 THEN 'Out of Stock'
                           WHEN StockQuantity <= ReorderLevel THEN 'Low Stock'
                           ELSE 'In Stock'
                       END AS StatusText
                FROM dbo.Medicines
                WHERE IsActive = 1
                  AND (@LowStockOnly = 0 OR StockQuantity <= ReorderLevel)
                  AND (@Category IS NULL OR Category = @Category)
                ORDER BY StockQuantity ASC, MedicineName ASC;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@LowStockOnly", lowStockOnly);
            cmd.Parameters.AddWithValue("@Category", (object?)category ?? DBNull.Value);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new StockReportItem
                {
                    MedicineCode  = reader.GetString(0),
                    MedicineName  = reader.GetString(1),
                    Category      = reader.GetString(2),
                    Unit          = reader.GetString(3),
                    UnitPrice     = reader.GetDecimal(4),
                    StockQuantity = reader.GetInt32(5),
                    ReorderLevel  = reader.GetInt32(6),
                    ExpiryDate    = reader.IsDBNull(7) ? null : reader.GetDateTime(7),
                    StatusText    = reader.GetString(8)
                });
            }

            return list;
        }

        /// <summary>
        /// Generates revenue, billing, and outstanding receivables report.
        /// </summary>
        public List<RevenueReportItem> GetRevenueReport(DateTime fromDate, DateTime toDate, string? status = null)
        {
            var list = new List<RevenueReportItem>();
            string query = @"
                SELECT b.BillID, b.BillDate, p.PatientCode, p.FirstName + ' ' + p.LastName AS PatientName,
                       b.Subtotal, b.Discount, b.TotalAmount,
                       ISNULL((SELECT SUM(pay.Amount) FROM dbo.Payments pay WHERE pay.BillID = b.BillID), 0.00) AS TotalPaid,
                       b.Status
                FROM dbo.Bills b
                INNER JOIN dbo.Patients p ON b.PatientID = p.PatientID
                WHERE CAST(b.BillDate AS DATE) >= @FromDate AND CAST(b.BillDate AS DATE) <= @ToDate
                  AND (@Status IS NULL OR b.Status = @Status)
                ORDER BY b.BillDate DESC;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@FromDate", fromDate.Date);
            cmd.Parameters.AddWithValue("@ToDate", toDate.Date);
            cmd.Parameters.AddWithValue("@Status", (object?)status ?? DBNull.Value);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new RevenueReportItem
                {
                    BillID      = reader.GetInt32(0),
                    BillDate    = reader.GetDateTime(1),
                    PatientCode = reader.GetString(2),
                    PatientName = reader.GetString(3),
                    Subtotal    = reader.GetDecimal(4),
                    Discount    = reader.GetDecimal(5),
                    TotalAmount = reader.GetDecimal(6),
                    TotalPaid   = reader.GetDecimal(7),
                    Status      = reader.GetString(8)
                });
            }

            return list;
        }

        /// <summary>
        /// Generates doctor consultation activity and workload report.
        /// </summary>
        public List<DoctorWorkloadReportItem> GetDoctorWorkloadReport(DateTime fromDate, DateTime toDate)
        {
            var list = new List<DoctorWorkloadReportItem>();
            const string query = @"
                SELECT d.DoctorCode, 'Dr. ' + d.FirstName + ' ' + d.LastName AS DoctorName,
                       dep.DepartmentName,
                       ISNULL(COUNT(a.AppointmentID), 0) AS TotalAppointments,
                       ISNULL(SUM(CASE WHEN a.Status = 'Completed' THEN 1 ELSE 0 END), 0) AS CompletedConsultations,
                       ISNULL(SUM(CASE WHEN a.Status = 'Cancelled' THEN 1 ELSE 0 END), 0) AS CancelledAppointments
                FROM dbo.Doctors d
                INNER JOIN dbo.Departments dep ON d.DepartmentID = dep.DepartmentID
                LEFT JOIN dbo.Appointments a ON d.DoctorID = a.DoctorID
                     AND a.AppointmentDate >= @FromDate AND a.AppointmentDate <= @ToDate
                WHERE d.IsActive = 1
                GROUP BY d.DoctorCode, d.FirstName, d.LastName, dep.DepartmentName
                ORDER BY CompletedConsultations DESC, TotalAppointments DESC;";

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@FromDate", fromDate.Date);
            cmd.Parameters.AddWithValue("@ToDate", toDate.Date);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new DoctorWorkloadReportItem
                {
                    DoctorCode             = reader.GetString(0),
                    DoctorName             = reader.GetString(1),
                    DepartmentName         = reader.GetString(2),
                    TotalAppointments      = reader.GetInt32(3),
                    CompletedConsultations = reader.GetInt32(4),
                    CancelledAppointments  = reader.GetInt32(5)
                });
            }

            return list;
        }
    }
}
