using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Services;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    public partial class DoctorForm : Form
    {
        private readonly DoctorService _doctorService;
        private readonly DepartmentService _deptService;
        private List<Doctor> _allDoctors = new();
        private int _selectedDoctorId = 0;

        public DoctorForm()
        {
            InitializeComponent();
            _doctorService = new DoctorService();
            _deptService = new DepartmentService();
        }

        private void DoctorForm_Load(object sender, EventArgs e)
        {
            LoadDepartments();
            RefreshDoctorList();
            ResetForm();
        }

        private void LoadDepartments()
        {
            try
            {
                var depts = _deptService.GetAllDepartments(false);

                // Department assignment dropdown
                cmbDepartment.DataSource = new List<Department>(depts);
                cmbDepartment.DisplayMember = "DepartmentName";
                cmbDepartment.ValueMember = "DepartmentID";

                // Filter dropdown
                var filterList = new List<Department> { new Department { DepartmentID = 0, DepartmentName = "-- All Departments --" } };
                filterList.AddRange(depts);
                cmbFilterDept.DataSource = filterList;
                cmbFilterDept.DisplayMember = "DepartmentName";
                cmbFilterDept.ValueMember = "DepartmentID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading departments: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadEligibleUserAccounts(int? currentDoctorUserId = null)
        {
            try
            {
                var users = _doctorService.GetEligibleDoctorUserAccounts(currentDoctorUserId);
                cmbUserAccount.DataSource = users;
                cmbUserAccount.DisplayMember = "FullName";
                cmbUserAccount.ValueMember = "UserID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading doctor user accounts: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshDoctorList()
        {
            try
            {
                _allDoctors = _doctorService.GetAllDoctors(false);
                ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading doctors: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilter()
        {
            int filterDeptId = (cmbFilterDept.SelectedValue is int id) ? id : 0;
            string search = txtSearch.Text.Trim().ToLowerInvariant();

            var filtered = _allDoctors.AsEnumerable();

            if (filterDeptId > 0)
            {
                filtered = filtered.Where(d => d.DepartmentID == filterDeptId);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                filtered = filtered.Where(d => d.FirstName.ToLowerInvariant().Contains(search) ||
                                               d.LastName.ToLowerInvariant().Contains(search) ||
                                               d.DoctorCode.ToLowerInvariant().Contains(search) ||
                                               (d.Specialization != null && d.Specialization.ToLowerInvariant().Contains(search)));
            }

            dgvDoctors.DataSource = null;
            dgvDoctors.DataSource = filtered.ToList();

            // Format columns
            if (dgvDoctors.Columns["DoctorID"] != null)
                dgvDoctors.Columns["DoctorID"]!.Visible = false;
            if (dgvDoctors.Columns["UserID"] != null)
                dgvDoctors.Columns["UserID"]!.Visible = false;
            if (dgvDoctors.Columns["DepartmentID"] != null)
                dgvDoctors.Columns["DepartmentID"]!.Visible = false;

            if (dgvDoctors.Columns["DoctorCode"] != null)
                dgvDoctors.Columns["DoctorCode"]!.HeaderText = "Code";
            if (dgvDoctors.Columns["FullName"] != null)
                dgvDoctors.Columns["FullName"]!.HeaderText = "Doctor Name";
            if (dgvDoctors.Columns["DepartmentName"] != null)
                dgvDoctors.Columns["DepartmentName"]!.HeaderText = "Department";
            if (dgvDoctors.Columns["Specialization"] != null)
                dgvDoctors.Columns["Specialization"]!.HeaderText = "Specialization";
            if (dgvDoctors.Columns["Phone"] != null)
                dgvDoctors.Columns["Phone"]!.HeaderText = "Phone";
            if (dgvDoctors.Columns["Username"] != null)
                dgvDoctors.Columns["Username"]!.HeaderText = "Account";
            if (dgvDoctors.Columns["IsActive"] != null)
                dgvDoctors.Columns["IsActive"]!.HeaderText = "Active";
        }

        private void cmbFilterDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void dgvDoctors_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDoctors.CurrentRow != null && dgvDoctors.CurrentRow.DataBoundItem is Doctor doc)
            {
                _selectedDoctorId = doc.DoctorID;
                txtDoctorCode.Text = doc.DoctorCode;
                txtFirstName.Text = doc.FirstName;
                txtLastName.Text = doc.LastName;
                txtSpecialization.Text = doc.Specialization ?? string.Empty;
                txtPhone.Text = doc.Phone ?? string.Empty;
                chkIsActive.Checked = doc.IsActive;

                cmbDepartment.SelectedValue = doc.DepartmentID;
                LoadEligibleUserAccounts(doc.UserID);
                cmbUserAccount.SelectedValue = doc.UserID;
                cmbUserAccount.Enabled = false; // Cannot reassign doctor user account

                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            _selectedDoctorId = 0;
            txtDoctorCode.Text = _doctorService.GetNextDoctorCode();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtSpecialization.Clear();
            txtPhone.Clear();
            chkIsActive.Checked = true;

            cmbUserAccount.Enabled = true;
            LoadEligibleUserAccounts();

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            txtFirstName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int userId = (cmbUserAccount.SelectedValue is int uid) ? uid : 0;
            int deptId = (cmbDepartment.SelectedValue is int did) ? did : 0;

            var doctor = new Doctor
            {
                UserID = userId,
                DepartmentID = deptId,
                DoctorCode = txtDoctorCode.Text.Trim(),
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Specialization = txtSpecialization.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                IsActive = chkIsActive.Checked
            };

            if (_doctorService.CreateDoctor(doctor, out string error))
            {
                MessageBox.Show($"Doctor '{doctor.FullName}' ({doctor.DoctorCode}) registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshDoctorList();
                ResetForm();
            }
            else
            {
                MessageBox.Show(error, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedDoctorId <= 0) return;

            int deptId = (cmbDepartment.SelectedValue is int did) ? did : 0;

            var doctor = new Doctor
            {
                DoctorID = _selectedDoctorId,
                DepartmentID = deptId,
                DoctorCode = txtDoctorCode.Text.Trim(),
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Specialization = txtSpecialization.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                IsActive = chkIsActive.Checked
            };

            if (_doctorService.UpdateDoctor(doctor, out string error))
            {
                MessageBox.Show("Doctor details updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshDoctorList();
                ResetForm();
            }
            else
            {
                MessageBox.Show(error, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
