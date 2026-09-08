using System;

namespace MEDICARE_HOSPITAL_MANGAMENT.Models
{
    /// <summary>
    /// Aggregate KPI metrics displayed on the executive hospital dashboard.
    /// </summary>
    public class DashboardMetrics
    {
        public int TotalPatients { get; set; }
        public int TodayAppointments { get; set; }
        public int CompletedAppointmentsToday { get; set; }
        public int AvailableDoctorsCount { get; set; }
        public int LowStockMedicinesCount { get; set; }
        public int PendingBillsCount { get; set; }
        public decimal TodayRevenue { get; set; }
        public decimal PendingBillsTotalDue { get; set; }

        public double AppointmentCompletionRate =>
            TodayAppointments > 0 ? (double)CompletedAppointmentsToday / TodayAppointments * 100.0 : 0.0;
    }

    /// <summary>
    /// Line item for the clinical appointment analytics report.
    /// </summary>
    public class AppointmentReportItem
    {
        public int AppointmentID { get; set; }
        public DateTime Date { get; set; }
        public string TimeSlot { get; set; } = string.Empty;
        public string PatientCode { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? Reason { get; set; }
    }

    /// <summary>
    /// Line item for pharmacy inventory valuation and stock threshold reporting.
    /// </summary>
    public class StockReportItem
    {
        public string MedicineCode { get; set; } = string.Empty;
        public string MedicineName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Unit { get; set; } = "Tablet";
        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public int ReorderLevel { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string StatusText { get; set; } = "In Stock";

        public decimal TotalStockValue => StockQuantity * UnitPrice;
    }

    /// <summary>
    /// Line item for hospital billing, collection, and revenue analytics.
    /// </summary>
    public class RevenueReportItem
    {
        public int BillID { get; set; }
        public DateTime BillDate { get; set; }
        public string PatientCode { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal OutstandingBalance => Math.Max(0m, TotalAmount - TotalPaid);
        public string Status { get; set; } = "Pending";
    }

    /// <summary>
    /// Line item for doctor consultation activity and workload analysis.
    /// </summary>
    public class DoctorWorkloadReportItem
    {
        public string DoctorCode { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public int TotalAppointments { get; set; }
        public int CompletedConsultations { get; set; }
        public int CancelledAppointments { get; set; }
    }
}
