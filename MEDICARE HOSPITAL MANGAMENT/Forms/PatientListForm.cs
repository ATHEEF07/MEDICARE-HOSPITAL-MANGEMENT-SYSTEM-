using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Services;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    public partial class PatientListForm : Form
    {
        private readonly PatientService _patientService;
        private List<Patient> _allPatients = new();
        private int _selectedPatientId = 0;

        public PatientListForm()
        {
            InitializeComponent();
            _patientService = new PatientService();
        }

        private void PatientListForm_Load(object sender, EventArgs e)
        {
            RefreshPatientList();
        }

        public void RefreshPatientList()
        {
            try
            {
                bool activeOnly = chkActiveOnly.Checked;
                _allPatients = _patientService.GetAllPatients(activeOnly);
                ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading patient records: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilter()
        {
            string query = txtSearch.Text.Trim().ToLowerInvariant();

            var filtered = string.IsNullOrWhiteSpace(query)
                ? _allPatients
                : _allPatients.Where(p => p.PatientCode.ToLowerInvariant().Contains(query) ||
                                          p.FullName.ToLowerInvariant().Contains(query) ||
                                          (p.NIC != null && p.NIC.ToLowerInvariant().Contains(query)) ||
                                          p.Phone.ToLowerInvariant().Contains(query)).ToList();

            dgvPatients.DataSource = null;
            dgvPatients.DataSource = filtered;

            // Format column headers
            if (dgvPatients.Columns["PatientID"] != null)
                dgvPatients.Columns["PatientID"]!.Visible = false;
            if (dgvPatients.Columns["FirstName"] != null)
                dgvPatients.Columns["FirstName"]!.Visible = false;
            if (dgvPatients.Columns["LastName"] != null)
                dgvPatients.Columns["LastName"]!.Visible = false;
            if (dgvPatients.Columns["DateOfBirth"] != null)
                dgvPatients.Columns["DateOfBirth"]!.Visible = false;
            if (dgvPatients.Columns["Address"] != null)
                dgvPatients.Columns["Address"]!.Visible = false;
            if (dgvPatients.Columns["EmergencyContactName"] != null)
                dgvPatients.Columns["EmergencyContactName"]!.Visible = false;
            if (dgvPatients.Columns["EmergencyContactPhone"] != null)
                dgvPatients.Columns["EmergencyContactPhone"]!.Visible = false;

            if (dgvPatients.Columns["PatientCode"] != null)
            {
                dgvPatients.Columns["PatientCode"]!.HeaderText = "Patient Code";
                dgvPatients.Columns["PatientCode"]!.Width = 110;
            }
            if (dgvPatients.Columns["FullName"] != null)
                dgvPatients.Columns["FullName"]!.HeaderText = "Full Name";
            if (dgvPatients.Columns["NIC"] != null)
                dgvPatients.Columns["NIC"]!.HeaderText = "NIC";
            if (dgvPatients.Columns["Age"] != null)
            {
                dgvPatients.Columns["Age"]!.HeaderText = "Age";
                dgvPatients.Columns["Age"]!.Width = 65;
            }
            if (dgvPatients.Columns["Gender"] != null)
            {
                dgvPatients.Columns["Gender"]!.HeaderText = "Gender";
                dgvPatients.Columns["Gender"]!.Width = 85;
            }
            if (dgvPatients.Columns["BloodGroup"] != null)
            {
                dgvPatients.Columns["BloodGroup"]!.HeaderText = "Blood Group";
                dgvPatients.Columns["BloodGroup"]!.Width = 95;
            }
            if (dgvPatients.Columns["Phone"] != null)
                dgvPatients.Columns["Phone"]!.HeaderText = "Phone";
            if (dgvPatients.Columns["Email"] != null)
                dgvPatients.Columns["Email"]!.HeaderText = "Email";
            if (dgvPatients.Columns["RegisteredAt"] != null)
            {
                dgvPatients.Columns["RegisteredAt"]!.HeaderText = "Registered Date";
                dgvPatients.Columns["RegisteredAt"]!.DefaultCellStyle.Format = "yyyy-MM-dd";
                dgvPatients.Columns["RegisteredAt"]!.Width = 120;
            }
            if (dgvPatients.Columns["IsActive"] != null)
            {
                dgvPatients.Columns["IsActive"]!.HeaderText = "Active";
                dgvPatients.Columns["IsActive"]!.Width = 65;
            }

            lblRecordCount.Text = $"Total Patients: {filtered.Count}";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void chkActiveOnly_CheckedChanged(object sender, EventArgs e)
        {
            RefreshPatientList();
        }

        private void dgvPatients_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPatients.CurrentRow != null && dgvPatients.CurrentRow.DataBoundItem is Patient p)
            {
                _selectedPatientId = p.PatientID;
                btnEditPatient.Enabled = true;
                btnDeactivate.Enabled = p.IsActive;
            }
            else
            {
                _selectedPatientId = 0;
                btnEditPatient.Enabled = false;
                btnDeactivate.Enabled = false;
            }
        }

        private void btnRegisterNew_Click(object sender, EventArgs e)
        {
            using var form = new PatientEntryForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                RefreshPatientList();
            }
        }

        private void btnEditPatient_Click(object sender, EventArgs e)
        {
            if (_selectedPatientId <= 0) return;

            using var form = new PatientEntryForm(_selectedPatientId);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                RefreshPatientList();
            }
        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            if (_selectedPatientId <= 0) return;

            var result = MessageBox.Show(
                "Are you sure you want to deactivate this patient record?",
                "Confirm Deactivation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (_patientService.DeactivatePatient(_selectedPatientId, out string error))
                {
                    MessageBox.Show("Patient deactivated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshPatientList();
                }
                else
                {
                    MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshPatientList();
        }

        private void dgvPatients_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnEditPatient.PerformClick();
            }
        }
    }
}
