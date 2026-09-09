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
    /// Appointment Booking and Rescheduling dialog.
    /// Supports creating new appointments and rescheduling existing ones.
    /// Provides a real-time availability check before saving.
    /// </summary>
    public partial class AppointmentBookingForm : Form
    {
        private readonly AppointmentService _appointmentService = new();
        private readonly PatientRepository _patientRepo = new();
        private readonly DoctorRepository _doctorRepo = new();

        private readonly Appointment? _existingAppointment;
        private List<Patient> _allPatients = new();
        private List<Doctor> _allDoctors = new();

        private bool _isReschedule => _existingAppointment != null;

        /// <summary>The date of the newly created or rescheduled appointment.</summary>
        public DateTime BookedDate => dtpAppointmentDate.Value.Date;

        // ─────────────────────────────────────────────────────────────────────────
        // Constructor overloads
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>Creates a blank booking form for a new appointment.</summary>
        public AppointmentBookingForm()
        {
            InitializeComponent();
            LoadDropdowns();
            SetDefaultsForNewAppointment();
        }

        /// <summary>Opens the form pre-filled with an existing appointment for rescheduling.</summary>
        public AppointmentBookingForm(Appointment existingAppointment)
        {
            _existingAppointment = existingAppointment;
            InitializeComponent();
            LoadDropdowns();
            PopulateFromExisting();
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Initialization
        // ─────────────────────────────────────────────────────────────────────────

        private void LoadDropdowns()
        {
            // Patients
            _allPatients = _patientRepo.GetAllPatients(activeOnly: true);
            cmbPatient.Items.Clear();
            cmbPatient.Items.Add("-- Select Patient --");
            foreach (var p in _allPatients)
                cmbPatient.Items.Add($"{p.PatientCode} – {p.FullName} ({p.Phone})");
            cmbPatient.SelectedIndex = 0;

            // Doctors
            _allDoctors = _doctorRepo.GetAllDoctors(activeOnly: true);
            cmbDoctor.Items.Clear();
            cmbDoctor.Items.Add("-- Select Doctor --");
            foreach (var d in _allDoctors)
                cmbDoctor.Items.Add($"{d.DoctorCode} – {d.FullName} [{d.DepartmentName}]");
            cmbDoctor.SelectedIndex = 0;

            // Duration presets
            cmbDuration.Items.Clear();
            cmbDuration.Items.AddRange(new object[] { "15 min", "30 min", "45 min", "60 min" });
            cmbDuration.SelectedIndex = 1; // default 30 min
        }

        private void SetDefaultsForNewAppointment()
        {
            Text = "New Appointment – MediCare";
            lblTitle.Text = "📅  Book New Appointment";
            btnSave.Text = "✅  Confirm Booking";
            dtpAppointmentDate.MinDate = DateTime.Today;
            dtpAppointmentDate.Value = DateTime.Today;
            dtpStartTime.Format = DateTimePickerFormat.Time;
            dtpStartTime.ShowUpDown = true;
            dtpStartTime.Value = DateTime.Today.AddHours(9); // default 09:00
            UpdateEndTime();
        }

        private void PopulateFromExisting()
        {
            Text = "Reschedule Appointment – MediCare";
            lblTitle.Text = "📅  Reschedule Appointment";
            btnSave.Text = "✅  Confirm Reschedule";
            lblAvailability.Text = string.Empty;

            // Select patient
            int patientIdx = _allPatients.FindIndex(p => p.PatientID == _existingAppointment!.PatientID);
            cmbPatient.SelectedIndex = patientIdx >= 0 ? patientIdx + 1 : 0;
            cmbPatient.Enabled = false; // cannot change patient when rescheduling

            // Select doctor
            int doctorIdx = _allDoctors.FindIndex(d => d.DoctorID == _existingAppointment!.DoctorID);
            cmbDoctor.SelectedIndex = doctorIdx >= 0 ? doctorIdx + 1 : 0;

            dtpAppointmentDate.MinDate = DateTime.Today;
            dtpAppointmentDate.Value = _existingAppointment!.AppointmentDate;
            dtpStartTime.Value = DateTime.Today.Add(_existingAppointment.StartTime);
            UpdateEndTime();

            txtReason.Text = _existingAppointment.Reason ?? string.Empty;
            txtNotes.Text  = _existingAppointment.Notes ?? string.Empty;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Event Handlers
        // ─────────────────────────────────────────────────────────────────────────

        private void dtpStartTime_ValueChanged(object sender, EventArgs e) => UpdateEndTime();
        private void cmbDuration_SelectedIndexChanged(object sender, EventArgs e) => UpdateEndTime();

        private void UpdateEndTime()
        {
            int minutes = cmbDuration.SelectedIndex switch
            {
                0 => 15,
                1 => 30,
                2 => 45,
                3 => 60,
                _ => 30
            };
            lblEndTime.Text = $"End: {dtpStartTime.Value.AddMinutes(minutes):hh:mm tt}";
        }

        private void btnCheckAvailability_Click(object sender, EventArgs e)
        {
            if (!TryBuildAppointmentFromUI(out var appt, out string err))
            {
                lblAvailability.ForeColor = Color.FromArgb(160, 40, 40);
                lblAvailability.Text = $"⚠ {err}";
                return;
            }

            int? excludeId = _isReschedule ? _existingAppointment!.AppointmentID : null;
            bool available = _appointmentService.IsDoctorAvailable(
                appt.DoctorID, appt.AppointmentDate, appt.StartTime, appt.EndTime, excludeId);

            if (available)
            {
                lblAvailability.ForeColor = Color.FromArgb(22, 135, 90);
                lblAvailability.Text = "✅ Time slot is available!";
            }
            else
            {
                lblAvailability.ForeColor = Color.FromArgb(160, 40, 40);
                lblAvailability.Text = "❌ This time slot is already booked. Please choose another time.";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lblAvailability.Text = string.Empty;

            if (!TryBuildAppointmentFromUI(out var appt, out string validationErr))
            {
                MessageBox.Show(validationErr, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_isReschedule)
                {
                    appt.AppointmentID = _existingAppointment!.AppointmentID;
                    if (_appointmentService.RescheduleAppointment(appt, out string err2))
                    {
                        MessageBox.Show("Appointment rescheduled successfully.", "Rescheduled",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        lblAvailability.ForeColor = Color.FromArgb(160, 40, 40);
                        lblAvailability.Text = $"❌ {err2}";
                    }
                }
                else
                {
                    int newId = _appointmentService.CreateAppointment(appt, out string err3);
                    if (newId > 0)
                    {
                        MessageBox.Show($"Appointment booked successfully.\nAppointment ID: {newId}", "Booking Confirmed",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        lblAvailability.ForeColor = Color.FromArgb(160, 40, 40);
                        lblAvailability.Text = $"❌ {err3}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Build Appointment from UI controls
        // ─────────────────────────────────────────────────────────────────────────

        private bool TryBuildAppointmentFromUI(out Appointment appt, out string errorMessage)
        {
            appt = new Appointment();
            errorMessage = string.Empty;

            if (cmbPatient.SelectedIndex <= 0)
            {
                errorMessage = "Please select a patient.";
                return false;
            }
            if (cmbDoctor.SelectedIndex <= 0)
            {
                errorMessage = "Please select a doctor.";
                return false;
            }

            appt.PatientID       = _allPatients[cmbPatient.SelectedIndex - 1].PatientID;
            appt.DoctorID        = _allDoctors[cmbDoctor.SelectedIndex - 1].DoctorID;
            appt.AppointmentDate = dtpAppointmentDate.Value.Date;
            appt.StartTime       = dtpStartTime.Value.TimeOfDay;

            int durationMinutes = cmbDuration.SelectedIndex switch { 0 => 15, 2 => 45, 3 => 60, _ => 30 };
            appt.EndTime = appt.StartTime.Add(TimeSpan.FromMinutes(durationMinutes));

            appt.Reason = string.IsNullOrWhiteSpace(txtReason.Text) ? null : txtReason.Text.Trim();
            appt.Notes  = string.IsNullOrWhiteSpace(txtNotes.Text) ? null : txtNotes.Text.Trim();
            appt.CreatedByUserID = SessionManager.CurrentUserId;

            return true;
        }
    }
}
