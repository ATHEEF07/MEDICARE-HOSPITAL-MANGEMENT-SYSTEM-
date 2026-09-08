using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Repositories;

namespace MEDICARE_HOSPITAL_MANGAMENT.Services
{
    /// <summary>
    /// Business Logic Layer for Multi-Criteria Analytical Reporting.
    /// Validates date criteria, orchestrates repository execution, and generates exports.
    /// </summary>
    public class ReportService
    {
        private readonly ReportRepository _reportRepo;

        public ReportService()
        {
            _reportRepo = new ReportRepository();
        }

        public ReportService(ReportRepository reportRepo)
        {
            _reportRepo = reportRepo;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Reports Execution
        // ─────────────────────────────────────────────────────────────────────────

        public List<AppointmentReportItem>? GenerateAppointmentReport(
            DateTime fromDate, DateTime toDate, int? doctorId, string? status, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (!ValidateDateRange(fromDate, toDate, out errorMessage))
                return null;

            try
            {
                return _reportRepo.GetAppointmentReport(fromDate, toDate, doctorId, status);
            }
            catch (SqlException ex)
            {
                errorMessage = $"Database error: {ex.Message}";
                return new List<AppointmentReportItem>();
            }
        }

        public List<StockReportItem> GenerateStockReport(bool lowStockOnly, string? category)
        {
            try
            {
                return _reportRepo.GetStockReport(lowStockOnly, category);
            }
            catch (SqlException)
            {
                return new List<StockReportItem>();
            }
        }

        public List<RevenueReportItem>? GenerateRevenueReport(
            DateTime fromDate, DateTime toDate, string? status, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (!ValidateDateRange(fromDate, toDate, out errorMessage))
                return null;

            try
            {
                return _reportRepo.GetRevenueReport(fromDate, toDate, status);
            }
            catch (SqlException ex)
            {
                errorMessage = $"Database error: {ex.Message}";
                return new List<RevenueReportItem>();
            }
        }

        public List<DoctorWorkloadReportItem>? GenerateDoctorWorkloadReport(
            DateTime fromDate, DateTime toDate, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (!ValidateDateRange(fromDate, toDate, out errorMessage))
                return null;

            try
            {
                return _reportRepo.GetDoctorWorkloadReport(fromDate, toDate);
            }
            catch (SqlException ex)
            {
                errorMessage = $"Database error: {ex.Message}";
                return new List<DoctorWorkloadReportItem>();
            }
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Validation & Export Helpers
        // ─────────────────────────────────────────────────────────────────────────

        public bool ValidateDateRange(DateTime fromDate, DateTime toDate, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (fromDate.Date > toDate.Date)
            {
                errorMessage = "Start Date cannot be after End Date.";
                return false;
            }
            return true;
        }

        /// <summary>
        /// Generates a standardized CSV text export for any reporting list.
        /// </summary>
        public string ExportToCsv(string reportTitle, string[] headers, IEnumerable<string[]> rows)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"# MediCare Hospital Management System - {reportTitle}");
            sb.AppendLine($"# Generated On: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            sb.AppendLine();

            sb.AppendLine(string.Join(",", headers.Select(EscapeCsv)));

            foreach (var row in rows)
            {
                sb.AppendLine(string.Join(",", row.Select(EscapeCsv)));
            }

            return sb.ToString();
        }

        private static string EscapeCsv(string? field)
        {
            if (string.IsNullOrEmpty(field)) return "\"\"";
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
            {
                return $"\"{field.Replace("\"", "\"\"")}\"";
            }
            return $"\"{field}\"";
        }
    }
}
