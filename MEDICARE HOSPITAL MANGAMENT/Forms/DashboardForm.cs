using System;
using System.Drawing;
using System.Windows.Forms;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Repositories;
using MEDICARE_HOSPITAL_MANGAMENT.Services;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    /// <summary>
    /// Executive Operational Dashboard.
    /// Presents hospital KPI summary cards, today's consultations schedule,
    /// and critical inventory shortage warnings.
    /// </summary>
    public partial class DashboardForm : Form
    {
        private readonly DashboardService _dashboardService = new();
        private readonly AppointmentRepository _appointmentRepo = new();

        public DashboardForm()
        {
            InitializeComponent();
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            SetupGridColumns();
            LoadDashboardData();

            // Enforce quick action button permissions based on session role
            ApplyRolePermissions();
        }

        private void SetupGridColumns()
        {
            dgvTodayAppts.AutoGenerateColumns = false;
            dgvTodayAppts.Columns.Clear();

            dgvTodayAppts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TimeSlot",
                HeaderText = "Time Slot",
                Width = 110
            });

            dgvTodayAppts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PatientName",
                HeaderText = "Patient Name",
                Width = 180
            });

            dgvTodayAppts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PatientCode",
                HeaderText = "Code",
                Width = 90
            });

            dgvTodayAppts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DoctorName",
                HeaderText = "Doctor",
                Width = 180
            });

            dgvTodayAppts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DepartmentName",
                HeaderText = "Department",
                Width = 140
            });

            dgvTodayAppts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Reason",
                HeaderText = "Clinical Reason",
                Width = 200
            });

            dgvTodayAppts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Status",
                HeaderText = "Status",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
        }

        public void LoadDashboardData()
        {
            var metrics = _dashboardService.GetDashboardMetrics();

            // Populate 6 KPI Cards
            lblPatientsVal.Text = metrics.TotalPatients.ToString("N0");

            lblApptsVal.Text = metrics.TodayAppointments.ToString("N0");
            lblApptsSub.Text = $"{metrics.CompletedAppointmentsToday} Completed ({metrics.AppointmentCompletionRate:F0}%)";

            lblDoctorsVal.Text = metrics.AvailableDoctorsCount.ToString("N0");

            lblStockVal.Text = metrics.LowStockMedicinesCount.ToString("N0");
            if (metrics.LowStockMedicinesCount > 0)
            {
                pnlCardStock.BackColor = Color.FromArgb(255, 240, 240);
                lblStockVal.ForeColor = Color.FromArgb(200, 20, 20);
            }
            else
            {
                pnlCardStock.BackColor = Color.White;
                lblStockVal.ForeColor = Color.FromArgb(40, 140, 60);
            }

            lblRevenueVal.Text = $"Rs. {metrics.TodayRevenue:N2}";
            lblDueVal.Text = $"Rs. {metrics.PendingBillsTotalDue:N2}";

            // Inventory alert notice
            string alertMsg = _dashboardService.GetInventoryAlertNotice(metrics);
            lblAlertNotice.Text = alertMsg;
            if (metrics.LowStockMedicinesCount > 0)
            {
                pnlAlert.BackColor = Color.FromArgb(255, 245, 230);
                lblAlertNotice.ForeColor = Color.FromArgb(180, 60, 0);
            }
            else
            {
                pnlAlert.BackColor = Color.FromArgb(240, 255, 240);
                lblAlertNotice.ForeColor = Color.FromArgb(20, 120, 40);
            }

            // Load today's appointment list
            var todayAppts = _dashboardService.GetTodayAppointments();
            dgvTodayAppts.DataSource = todayAppts;
        }

        private void DgvTodayAppts_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvTodayAppts.Rows.Count) return;

            var row = dgvTodayAppts.Rows[e.RowIndex];
            if (row.DataBoundItem is AppointmentReportItem appt)
            {
                if (appt.Status == "Completed")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(240, 255, 240); // Soft green
                }
                else if (appt.Status == "Scheduled")
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
                else if (appt.Status == "Cancelled")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
                    row.DefaultCellStyle.ForeColor = Color.Gray;
                }
            }
        }

        private void DgvTodayAppts_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvTodayAppts.Rows[e.RowIndex].DataBoundItem is AppointmentReportItem reportItem)
            {
                if (SessionManager.IsDoctor() || SessionManager.IsAdmin())
                {
                    var appt = _appointmentRepo.GetAppointmentById(reportItem.AppointmentID);
                    if (appt != null)
                    {
                        using var consultForm = new ConsultationForm(appt);
                        consultForm.ShowDialog();
                        LoadDashboardData();
                    }
                }
            }
        }

        private void ApplyRolePermissions()
        {
            // Tailor quick actions to user role
            btnQuickAppt.Visible     = SessionManager.IsAdmin() || SessionManager.IsReceptionist();
            btnQuickConsult.Visible  = SessionManager.IsAdmin() || SessionManager.IsDoctor();
            btnQuickDispense.Visible = SessionManager.IsAdmin() || SessionManager.IsPharmacist();
            btnQuickBill.Visible     = SessionManager.IsAdmin() || SessionManager.IsCashier();
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Quick Action Handlers
        // ─────────────────────────────────────────────────────────────────────────

        private void BtnRefresh_Click(object? sender, EventArgs e) => LoadDashboardData();

        private void BtnQuickAppt_Click(object? sender, EventArgs e)
        {
            using var form = new AppointmentBookingForm();
            form.ShowDialog();
            LoadDashboardData();
        }

        private void BtnQuickConsult_Click(object? sender, EventArgs e)
        {
            using var form = new AppointmentListForm();
            form.ShowDialog();
            LoadDashboardData();
        }

        private void BtnQuickDispense_Click(object? sender, EventArgs e)
        {
            using var form = new PharmacyDispenseForm();
            form.ShowDialog();
            LoadDashboardData();
        }

        private void BtnQuickBill_Click(object? sender, EventArgs e)
        {
            using var form = new BillingForm();
            form.ShowDialog();
            LoadDashboardData();
        }

        private void BtnQuickReports_Click(object? sender, EventArgs e)
        {
            using var form = new ReportsForm();
            form.ShowDialog();
        }
    }
}
