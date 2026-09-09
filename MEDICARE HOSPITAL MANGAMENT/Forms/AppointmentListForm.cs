using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Repositories;
using MEDICARE_HOSPITAL_MANGAMENT.Services;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    /// <summary>
    /// Master Appointment Scheduling Management Form.
    /// Allows filtering by date, doctor, status, and patient name.
    /// Provides quick action buttons for all appointment lifecycle transitions.
    /// </summary>
    public partial class AppointmentListForm : Form
    {
        private readonly AppointmentService _appointmentService = new();
        private readonly DoctorRepository _doctorRepo = new();
        private List<Appointment> _currentList = new();

        public AppointmentListForm()
        {
            InitializeComponent();
            SetupRolePermissions();
            LoadDoctorFilter();
            LoadStatusFilter();
            LoadTodayAppointments();
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Initialization
        // ─────────────────────────────────────────────────────────────────────────

        private void SetupRolePermissions()
        {
            bool canCreate  = SessionManager.IsAdmin() || SessionManager.IsReceptionist();
            bool canConsult = SessionManager.IsDoctor() || SessionManager.IsAdmin();

            btnNewAppointment.Enabled = canCreate;
            btnReschedule.Enabled     = canCreate;
            btnCancel.Enabled         = canCreate || SessionManager.IsDoctor();
            btnStartConsultation.Enabled = canConsult;
        }

        private void LoadDoctorFilter()
        {
            var doctors = _doctorRepo.GetAllDoctors(activeOnly: true);
            cmbDoctorFilter.Items.Clear();
            cmbDoctorFilter.Items.Add(new ComboItem("All Doctors", 0));
            foreach (var d in doctors)
                cmbDoctorFilter.Items.Add(new ComboItem(d.FullName, d.DoctorID));
            cmbDoctorFilter.DisplayMember = "Display";
            cmbDoctorFilter.SelectedIndex = 0;
        }

        private void LoadStatusFilter()
        {
            cmbStatusFilter.Items.Clear();
            cmbStatusFilter.Items.AddRange(new object[] { "All", "Scheduled", "Completed", "Cancelled", "NoShow" });
            cmbStatusFilter.SelectedIndex = 0;
        }

        private void LoadTodayAppointments()
        {
            dtpFilterDate.Value = DateTime.Today;
            ApplyFilters();
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Data Loading
        // ─────────────────────────────────────────────────────────────────────────

        private void ApplyFilters()
        {
            try
            {
                int? doctorId = null;
                if (cmbDoctorFilter.SelectedItem is ComboItem di && di.Value > 0)
                    doctorId = di.Value;

                string? status = null;
                if (cmbStatusFilter.SelectedItem is string s && s != "All")
                    status = s;

                string? search = string.IsNullOrWhiteSpace(txtPatientSearch.Text) ? null : txtPatientSearch.Text;

                DateTime? from = chkUseDate.Checked ? dtpFilterDate.Value.Date : null;
                DateTime? to   = chkUseDate.Checked ? dtpFilterDate.Value.Date : null;

                _currentList = _appointmentService.SearchAppointments(from, to, doctorId, status, search);
                PopulateGrid(_currentList);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading appointments:\n{ex.Message}", "Load Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateGrid(List<Appointment> appointments)
        {
            dgvAppointments.Rows.Clear();

            foreach (var appt in appointments)
            {
                int rowIdx = dgvAppointments.Rows.Add(
                    appt.AppointmentID,
                    appt.AppointmentDate.ToString("dd/MM/yyyy"),
                    appt.TimeSlotFormatted,
                    $"{appt.PatientName} ({appt.PatientCode})",
                    appt.DoctorName,
                    appt.DepartmentName,
                    appt.Reason ?? "-",
                    appt.Status
                );

                // Colour-code rows by status
                var row = dgvAppointments.Rows[rowIdx];
                row.DefaultCellStyle.ForeColor = appt.Status switch
                {
                    "Completed"  => Color.FromArgb(22, 135, 90),
                    "Cancelled"  => Color.FromArgb(160, 60, 60),
                    "NoShow"     => Color.FromArgb(140, 100, 30),
                    _            => Color.FromArgb(30, 30, 60)
                };
            }

            lblRecordCount.Text = $"{appointments.Count} appointment(s) found";
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Event Handlers
        // ─────────────────────────────────────────────────────────────────────────

        private void btnSearch_Click(object sender, EventArgs e) => ApplyFilters();
        private void btnRefresh_Click(object sender, EventArgs e) => ApplyFilters();
        private void txtPatientSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ApplyFilters();
        }
        private void cmbDoctorFilter_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilters();
        private void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilters();
        private void chkUseDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpFilterDate.Enabled = chkUseDate.Checked;
            ApplyFilters();
        }
        private void dtpFilterDate_ValueChanged(object sender, EventArgs e)
        {
            if (chkUseDate.Checked) ApplyFilters();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            dtpFilterDate.Value = DateTime.Today;
            chkUseDate.Checked = true;
            ApplyFilters();
        }

        private void btnNewAppointment_Click(object sender, EventArgs e)
        {
            using var form = new AppointmentBookingForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                dtpFilterDate.Value = form.BookedDate;
                chkUseDate.Checked = true;
                ApplyFilters();
            }
        }

        private void btnReschedule_Click(object sender, EventArgs e)
        {
            var appt = GetSelectedAppointment();
            if (appt == null) return;

            if (appt.Status != "Scheduled")
            {
                MessageBox.Show($"Only 'Scheduled' appointments can be rescheduled.\nThis appointment is '{appt.Status}'.",
                    "Cannot Reschedule", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var form = new AppointmentBookingForm(appt);
            if (form.ShowDialog() == DialogResult.OK)
            {
                dtpFilterDate.Value = form.BookedDate;
                chkUseDate.Checked = true;
                ApplyFilters();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var appt = GetSelectedAppointment();
            if (appt == null) return;

            if (MessageBox.Show($"Cancel appointment for {appt.PatientName} on {appt.AppointmentDate:dd/MM/yyyy} at {appt.TimeSlotFormatted}?\n\nThis action cannot be undone.",
                "Confirm Cancellation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            if (_appointmentService.CancelAppointment(appt.AppointmentID, out string err))
            {
                MessageBox.Show("Appointment cancelled successfully.", "Cancelled",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ApplyFilters();
            }
            else
            {
                MessageBox.Show(err, "Cannot Cancel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMarkCompleted_Click(object sender, EventArgs e)
        {
            var appt = GetSelectedAppointment();
            if (appt == null) return;

            if (_appointmentService.MarkCompleted(appt.AppointmentID, out string err))
            {
                MessageBox.Show("Appointment marked as Completed.", "Updated",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ApplyFilters();
            }
            else
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMarkNoShow_Click(object sender, EventArgs e)
        {
            var appt = GetSelectedAppointment();
            if (appt == null) return;

            if (_appointmentService.MarkNoShow(appt.AppointmentID, out string err))
            {
                MessageBox.Show("Appointment marked as No-Show.", "Updated",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ApplyFilters();
            }
            else
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStartConsultation_Click(object sender, EventArgs e)
        {
            var appt = GetSelectedAppointment();
            if (appt == null) return;

            if (appt.Status != "Scheduled")
            {
                MessageBox.Show("Only scheduled appointments can open a consultation.",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var form = new ConsultationForm(appt);
            form.ShowDialog();
            ApplyFilters();
        }

        private void dgvAppointments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var appt = GetSelectedAppointment();
            if (appt == null) return;

            // Double-click opens consultation for doctors, booking detail for others
            if (SessionManager.IsDoctor() || SessionManager.IsAdmin())
                btnStartConsultation_Click(sender, e);
            else
                btnReschedule_Click(sender, e);
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Helpers
        // ─────────────────────────────────────────────────────────────────────────

        private Appointment? GetSelectedAppointment()
        {
            if (dgvAppointments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an appointment first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            int id = Convert.ToInt32(dgvAppointments.SelectedRows[0].Cells["colID"].Value);
            return _currentList.Find(a => a.AppointmentID == id);
        }

        // Simple display wrapper for ComboBox items
        private sealed class ComboItem
        {
            public string Display { get; }
            public int Value { get; }
            public ComboItem(string display, int value) { Display = display; Value = value; }
            public override string ToString() => Display;
        }
    }
}
