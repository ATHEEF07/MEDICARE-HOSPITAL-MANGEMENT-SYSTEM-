using System;
using System.Windows.Forms;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Repositories;
using MEDICARE_HOSPITAL_MANGAMENT.Services;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    /// <summary>
    /// Clinical Consultation Recording Form (Doctor-facing).
    /// Records symptoms, diagnosis, treatment, and notes for a patient visit.
    /// When linked to an appointment, it automatically marks it as 'Completed'.
    /// </summary>
    public partial class ConsultationForm : Form
    {
        private readonly MedicalRecordService _recordService = new();
        private readonly AppointmentService _appointmentService = new();
        private readonly PatientRepository _patientRepo = new();
        private readonly DoctorRepository _doctorRepo = new();

        private readonly Appointment? _linkedAppointment;
        private MedicalRecord? _existingRecord;
        private readonly int _standalonePatientId;
        private readonly int _standaloneDoctorId;

        // ─────────────────────────────────────────────────────────────────────────
        // Constructors
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>Opens a consultation linked to an appointment (most common path).</summary>
        public ConsultationForm(Appointment appointment)
        {
            _linkedAppointment = appointment;
            InitializeComponent();
            PopulatePatientCard(appointment.PatientID);
            PopulateFromAppointment(appointment);
            CheckForExistingRecord(appointment.AppointmentID);
        }

        /// <summary>Opens a standalone consultation (no appointment link).</summary>
        public ConsultationForm(int patientId, int doctorId)
        {
            _standalonePatientId = patientId;
            _standaloneDoctorId = doctorId;
            InitializeComponent();
            PopulatePatientCard(patientId);
            SetDoctorInfo(doctorId);
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Initialization
        // ─────────────────────────────────────────────────────────────────────────

        private void PopulatePatientCard(int patientId)
        {
            var patient = _patientRepo.GetPatientById(patientId);
            if (patient == null) return;

            lblPatientName.Text  = patient.FullName;
            lblPatientCode.Text  = $"Code: {patient.PatientCode}";
            lblPatientAge.Text   = $"Age: {patient.Age} yrs";
            lblPatientGender.Text = $"Gender: {patient.Gender}";
            lblBloodGroup.Text   = $"Blood Group: {patient.BloodGroup ?? "—"}";
            lblPatientPhone.Text = $"Phone: {patient.Phone}";
        }

        private void PopulateFromAppointment(Appointment appt)
        {
            lblDoctor.Text    = $"Doctor: {appt.DoctorName}";
            lblDept.Text      = $"Department: {appt.DepartmentName}";
            lblApptDate.Text  = $"Appointment: {appt.AppointmentDate:dd MMMM yyyy} {appt.TimeSlotFormatted}";
            lblReason.Text    = $"Reason: {appt.Reason ?? "Not specified"}";
            txtSymptoms.Focus();
        }

        private void SetDoctorInfo(int doctorId)
        {
            var doc = _doctorRepo.GetDoctorById(doctorId);
            if (doc == null) return;
            lblDoctor.Text = $"Doctor: {doc.FullName}";
            lblDept.Text   = $"Department: {doc.DepartmentName}";
        }

        private void CheckForExistingRecord(int appointmentId)
        {
            _existingRecord = _recordService.GetByAppointmentId(appointmentId);
            if (_existingRecord != null)
            {
                // Pre-fill with existing data for editing
                txtSymptoms.Text  = _existingRecord.Symptoms ?? string.Empty;
                txtDiagnosis.Text = _existingRecord.Diagnosis ?? string.Empty;
                txtTreatment.Text = _existingRecord.Treatment ?? string.Empty;
                txtNotes.Text     = _existingRecord.Notes ?? string.Empty;
                lblSaveStatus.Text = "ℹ Editing an existing consultation record.";
                lblSaveStatus.ForeColor = System.Drawing.Color.FromArgb(30, 100, 170);
            }
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Event Handlers
        // ─────────────────────────────────────────────────────────────────────────

        private void btnSave_Click(object sender, EventArgs e)
        {
            lblSaveStatus.Text = string.Empty;

            if (_existingRecord != null)
                SaveUpdate();
            else
                SaveNew();
        }

        private void SaveNew()
        {
            var record = BuildRecordFromUI();
            int newId = _recordService.SaveConsultation(record, out string err);

            if (newId > 0)
            {
                lblSaveStatus.ForeColor = System.Drawing.Color.FromArgb(22, 135, 90);
                lblSaveStatus.Text = $"✅ Consultation saved (Record #{newId}). Appointment marked as Completed.";
                _existingRecord = _recordService.GetById(newId);
                btnSave.Text = "💾  Update Record";
            }
            else
            {
                lblSaveStatus.ForeColor = System.Drawing.Color.FromArgb(160, 40, 40);
                lblSaveStatus.Text = $"❌ {err}";
            }
        }

        private void SaveUpdate()
        {
            _existingRecord!.Symptoms  = txtSymptoms.Text.Trim();
            _existingRecord.Diagnosis  = txtDiagnosis.Text.Trim();
            _existingRecord.Treatment  = txtTreatment.Text.Trim();
            _existingRecord.Notes      = txtNotes.Text.Trim();

            if (_recordService.UpdateConsultation(_existingRecord, out string err))
            {
                lblSaveStatus.ForeColor = System.Drawing.Color.FromArgb(22, 135, 90);
                lblSaveStatus.Text = "✅ Consultation record updated.";
            }
            else
            {
                lblSaveStatus.ForeColor = System.Drawing.Color.FromArgb(160, 40, 40);
                lblSaveStatus.Text = $"❌ {err}";
            }
        }

        private void btnPrescribe_Click(object sender, EventArgs e)
        {
            int patientId = _linkedAppointment?.PatientID ?? _standalonePatientId;
            int doctorId = GetCurrentDoctorId();

            if (patientId <= 0)
            {
                MessageBox.Show("Patient information is missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var rxForm = new PrescriptionForm(patientId, doctorId, _existingRecord?.MedicalRecordID);
            if (rxForm.ShowDialog() == DialogResult.OK)
            {
                lblSaveStatus.ForeColor = System.Drawing.Color.FromArgb(20, 120, 60);
                lblSaveStatus.Text = $"✔️ Prescription RX-{rxForm.CreatedPrescriptionID:D5} issued successfully.";
            }
        }

        private void btnViewHistory_Click(object sender, EventArgs e)
        {
            if (_linkedAppointment == null) return;
            using var form = new PatientMedicalHistoryForm(_linkedAppointment.PatientID,
                lblPatientName.Text);
            form.ShowDialog();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear all entered text?", "Confirm Clear",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                txtSymptoms.Clear();
                txtDiagnosis.Clear();
                txtTreatment.Clear();
                txtNotes.Clear();
                lblSaveStatus.Text = string.Empty;
                txtSymptoms.Focus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        // ─────────────────────────────────────────────────────────────────────────
        // Helpers
        // ─────────────────────────────────────────────────────────────────────────

        private MedicalRecord BuildRecordFromUI() => new()
        {
            PatientID     = _linkedAppointment?.PatientID ?? 0,
            DoctorID      = GetCurrentDoctorId(),
            AppointmentID = _linkedAppointment?.AppointmentID,
            VisitDate     = DateTime.Now,
            Symptoms      = txtSymptoms.Text.Trim(),
            Diagnosis     = txtDiagnosis.Text.Trim(),
            Treatment     = txtTreatment.Text.Trim(),
            Notes         = txtNotes.Text.Trim()
        };

        private int GetCurrentDoctorId()
        {
            if (_linkedAppointment != null)
                return _linkedAppointment.DoctorID;

            // Look up by session user ID
            var doc = _doctorRepo.GetDoctorByUserId(SessionManager.CurrentUserId);
            return doc?.DoctorID ?? 0;
        }
    }
}
