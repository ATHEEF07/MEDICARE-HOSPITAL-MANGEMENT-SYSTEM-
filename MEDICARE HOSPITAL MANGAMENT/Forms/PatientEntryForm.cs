using System;
using System.Windows.Forms;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Services;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    public partial class PatientEntryForm : Form
    {
        private readonly PatientService _patientService;
        private readonly int _patientId;
        private readonly bool _isEditMode;

        /// <summary>
        /// Constructor for Registering a New Patient.
        /// </summary>
        public PatientEntryForm() : this(0)
        {
        }

        /// <summary>
        /// Constructor for Editing an Existing Patient.
        /// </summary>
        public PatientEntryForm(int patientId)
        {
            InitializeComponent();
            _patientService = new PatientService();
            _patientId = patientId;
            _isEditMode = patientId > 0;
        }

        private void PatientEntryForm_Load(object sender, EventArgs e)
        {
            PopulateDropdowns();

            if (_isEditMode)
            {
                lblTitle.Text = "Update Patient Information";
                btnSave.Text = "Update Patient";
                LoadPatientData();
            }
            else
            {
                lblTitle.Text = "Register New Patient";
                btnSave.Text = "Register Patient";
                txtPatientCode.Text = _patientService.GetNextPatientCode();
                dtpDateOfBirth.Value = DateTime.Today.AddYears(-30);
                UpdateAgeLabel();
            }
        }

        private void PopulateDropdowns()
        {
            cmbGender.Items.Clear();
            cmbGender.Items.AddRange(new object[] { "Male", "Female", "Other" });
            cmbGender.SelectedIndex = 0;

            cmbBloodGroup.Items.Clear();
            cmbBloodGroup.Items.AddRange(new object[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-", "Unknown" });
            cmbBloodGroup.SelectedIndex = 0;
        }

        private void LoadPatientData()
        {
            var patient = _patientService.GetPatientById(_patientId);
            if (patient == null)
            {
                MessageBox.Show("Patient record could not be found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            txtPatientCode.Text = patient.PatientCode;
            txtNIC.Text = patient.NIC ?? string.Empty;
            txtFirstName.Text = patient.FirstName;
            txtLastName.Text = patient.LastName;
            dtpDateOfBirth.Value = patient.DateOfBirth;
            cmbGender.SelectedItem = patient.Gender;
            cmbBloodGroup.SelectedItem = string.IsNullOrWhiteSpace(patient.BloodGroup) ? "Unknown" : patient.BloodGroup;
            txtPhone.Text = patient.Phone;
            txtEmail.Text = patient.Email ?? string.Empty;
            txtAddress.Text = patient.Address ?? string.Empty;
            txtEmergencyName.Text = patient.EmergencyContactName ?? string.Empty;
            txtEmergencyPhone.Text = patient.EmergencyContactPhone ?? string.Empty;
            chkIsActive.Checked = patient.IsActive;

            UpdateAgeLabel();
        }

        private void dtpDateOfBirth_ValueChanged(object sender, EventArgs e)
        {
            UpdateAgeLabel();
        }

        private void UpdateAgeLabel()
        {
            var today = DateTime.Today;
            int age = today.Year - dtpDateOfBirth.Value.Year;
            if (dtpDateOfBirth.Value.Date > today.AddYears(-age))
                age--;
            lblAgeDisplay.Text = $"Age: {Math.Max(0, age)} years";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var patient = new Patient
            {
                PatientID = _patientId,
                PatientCode = txtPatientCode.Text.Trim(),
                NIC = string.IsNullOrWhiteSpace(txtNIC.Text) ? null : txtNIC.Text.Trim(),
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                DateOfBirth = dtpDateOfBirth.Value.Date,
                Gender = cmbGender.SelectedItem?.ToString() ?? "Male",
                BloodGroup = cmbBloodGroup.SelectedItem?.ToString() == "Unknown" ? null : cmbBloodGroup.SelectedItem?.ToString(),
                Phone = txtPhone.Text.Trim(),
                Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim(),
                EmergencyContactName = string.IsNullOrWhiteSpace(txtEmergencyName.Text) ? null : txtEmergencyName.Text.Trim(),
                EmergencyContactPhone = string.IsNullOrWhiteSpace(txtEmergencyPhone.Text) ? null : txtEmergencyPhone.Text.Trim(),
                IsActive = chkIsActive.Checked
            };

            bool success;
            string errorMessage;

            if (_isEditMode)
            {
                success = _patientService.UpdatePatient(patient, out errorMessage);
            }
            else
            {
                success = _patientService.RegisterPatient(patient, out errorMessage);
            }

            if (success)
            {
                string msg = _isEditMode
                    ? "Patient details updated successfully!"
                    : $"Patient registered successfully with Code: {patient.PatientCode}!";
                MessageBox.Show(msg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
