using System;
using System.Windows.Forms;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Services;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    public partial class DepartmentForm : Form
    {
        private readonly DepartmentService _deptService;
        private int _selectedDeptId = 0;

        public DepartmentForm()
        {
            InitializeComponent();
            _deptService = new DepartmentService();
        }

        private void DepartmentForm_Load(object sender, EventArgs e)
        {
            RefreshDepartmentList();
            ResetForm();
        }

        private void RefreshDepartmentList()
        {
            try
            {
                var depts = _deptService.GetAllDepartments(false);
                dgvDepartments.DataSource = null;
                dgvDepartments.DataSource = depts;

                if (dgvDepartments.Columns["DepartmentID"] != null)
                    dgvDepartments.Columns["DepartmentID"]!.HeaderText = "ID";
                if (dgvDepartments.Columns["DepartmentName"] != null)
                    dgvDepartments.Columns["DepartmentName"]!.HeaderText = "Department Name";
                if (dgvDepartments.Columns["Description"] != null)
                    dgvDepartments.Columns["Description"]!.HeaderText = "Description";
                if (dgvDepartments.Columns["IsActive"] != null)
                    dgvDepartments.Columns["IsActive"]!.HeaderText = "Active";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading departments: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDepartments_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDepartments.CurrentRow != null && dgvDepartments.CurrentRow.DataBoundItem is Department dept)
            {
                _selectedDeptId = dept.DepartmentID;
                txtDepartmentName.Text = dept.DepartmentName;
                txtDescription.Text = dept.Description ?? string.Empty;
                chkIsActive.Checked = dept.IsActive;

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
            _selectedDeptId = 0;
            txtDepartmentName.Clear();
            txtDescription.Clear();
            chkIsActive.Checked = true;

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            txtDepartmentName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var dept = new Department
            {
                DepartmentName = txtDepartmentName.Text.Trim(),
                Description = txtDescription.Text.Trim(),
                IsActive = chkIsActive.Checked
            };

            if (_deptService.CreateDepartment(dept, out string error))
            {
                MessageBox.Show("Department created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshDepartmentList();
                ResetForm();
            }
            else
            {
                MessageBox.Show(error, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedDeptId <= 0) return;

            var dept = new Department
            {
                DepartmentID = _selectedDeptId,
                DepartmentName = txtDepartmentName.Text.Trim(),
                Description = txtDescription.Text.Trim(),
                IsActive = chkIsActive.Checked
            };

            if (_deptService.UpdateDepartment(dept, out string error))
            {
                MessageBox.Show("Department updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshDepartmentList();
                ResetForm();
            }
            else
            {
                MessageBox.Show(error, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
