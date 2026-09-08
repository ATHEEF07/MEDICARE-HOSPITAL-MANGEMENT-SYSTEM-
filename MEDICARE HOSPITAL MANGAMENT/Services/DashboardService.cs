using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Repositories;

namespace MEDICARE_HOSPITAL_MANGAMENT.Services
{
    /// <summary>
    /// Business Logic Layer for the Executive Hospital Dashboard.
    /// Manages real-time metric computation and inventory warning summaries.
    /// </summary>
    public class DashboardService
    {
        private readonly ReportRepository _reportRepo;

        public DashboardService()
        {
            _reportRepo = new ReportRepository();
        }

        public DashboardService(ReportRepository reportRepo)
        {
            _reportRepo = reportRepo;
        }

        /// <summary>
        /// Retrieves real-time hospital KPI summary metrics.
        /// Handles database connectivity gracefully.
        /// </summary>
        public DashboardMetrics GetDashboardMetrics()
        {
            try
            {
                return _reportRepo.GetDashboardMetrics();
            }
            catch (SqlException)
            {
                // Return default zeroed metrics when offline or disconnected
                return new DashboardMetrics();
            }
        }

        /// <summary>
        /// Retrieves today's scheduled and in-progress appointments.
        /// </summary>
        public List<AppointmentReportItem> GetTodayAppointments()
        {
            try
            {
                return _reportRepo.GetTodayAppointmentsList();
            }
            catch (SqlException)
            {
                return new List<AppointmentReportItem>();
            }
        }

        /// <summary>
        /// Generates a critical low-stock alert description if any medication is low.
        /// </summary>
        public string GetInventoryAlertNotice(DashboardMetrics metrics)
        {
            if (metrics.LowStockMedicinesCount > 0)
            {
                return $"⚠️ ATTENTION: {metrics.LowStockMedicinesCount} medication(s) are at or below reorder threshold. Replenishment recommended.";
            }

            return "✔️ All medication inventory stocks are within safe operational limits.";
        }
    }
}
