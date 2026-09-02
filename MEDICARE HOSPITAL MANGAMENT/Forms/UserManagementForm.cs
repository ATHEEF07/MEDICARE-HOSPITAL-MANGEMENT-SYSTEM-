using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Services;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    public partial class UserManagementForm : Form
    {
        private readonly UserService _userService;
        private List<User> _allUsers = new();
        private int _selectedUserId = 0;

        public UserManagementForm()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private void UserManagementForm_Load(object sender, EventArgs e)
        {
            LoadRoles();
            RefreshUserList();
            ResetForm();
        }

        private void LoadRoles()
        {
            try
            {
                var roles = _userService.GetAllRoles();
                cmbRole.DataSource = roles;
                cmbRole.DisplayMember = "RoleName";
                cmbRole.ValueMember = "RoleID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading roles: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshUserList()
        {
            try
            {
                _allUsers = _userService.GetAllUsers();
                ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilter()
        {
            string search = txtSearch.Text.Trim().ToLowerInvariant();
            var filtered = string.IsNullOrWhiteSpace(search)
                ? _allUsers
                : _allUsers.Where(u => u.Username.ToLowerInvariant().Contains(search) ||
                                       u.FullName.ToLowerInvariant().Contains(search) ||
                                       u.RoleName.ToLowerInvariant().Contains(search)).ToList();

            dgvUsers.DataSource = null;
            dgvUsers.DataSource = filtered;

            // Format grid headers
            if (dgvUsers.Columns["PasswordHash"] != null)
                dgvUsers.Columns["PasswordHash"]!.Visible = false;

            if (dgvUsers.Columns["UserID"] != null)
                dgvUsers.Columns["UserID"]!.HeaderText = "ID";
            if (dgvUsers.Columns["Username"] != null)
                dgvUsers.Columns["Username"]!.HeaderText = "Username";
            if (dgvUsers.Columns["FullName"] != null)
                dgvUsers.Columns["FullName"]!.HeaderText = "Full Name";
            if (dgvUsers.Columns["RoleName"] != null)
                dgvUsers.Columns["RoleName"]!.HeaderText = "Role";
            if (dgvUsers.Columns["RoleID"] != null)
                dgvUsers.Columns["RoleID"]!.Visible = false;
            if (dgvUsers.Columns["IsActive"] != null)
                dgvUsers.Columns["IsActive"]!.HeaderText = "Active";
            if (dgvUsers.Columns["CreatedAt"] != null)
            {
                dgvUsers.Columns["CreatedAt"]!.HeaderText = "Created Date";
                dgvUsers.Columns["CreatedAt"]!.DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow != null && dgvUsers.CurrentRow.DataBoundItem is User user)
            {
                _selectedUserId = user.UserID;
                txtUsername.Text = user.Username;
                txtUsername.ReadOnly = true; // Username is immutable once created
                txtFullName.Text = user.FullName;
                cmbRole.SelectedValue = user.RoleID;
                chkIsActive.Checked = user.IsActive;
                txtPassword.Clear();
                txtPassword.Enabled = false; // Password not editable here; use Reset Password button

                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
                btnToggleStatus.Enabled = true;
                btnResetPassword.Enabled = true;
                btnToggleStatus.Text = user.IsActive ? "Deactivate User" : "Activate User";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            _selectedUserId = 0;
            txtUsername.ReadOnly = false;
            txtUsername.Clear();
            txtFullName.Clear();
            txtPassword.Clear();
            txtPassword.Enabled = true;
            chkIsActive.Checked = true;
            if (cmbRole.Items.Count > 0) cmbRole.SelectedIndex = 0;

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnToggleStatus.Enabled = false;
            btnResetPassword.Enabled = false;
            btnToggleStatus.Text = "Toggle Status";
            txtUsername.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var user = new User
            {
                Username = txtUsername.Text.Trim(),
                FullName = txtFullName.Text.Trim(),
                RoleID = cmbRole.SelectedValue != null ? Convert.ToInt32(cmbRole.SelectedValue) : 0,
                IsActive = chkIsActive.Checked
            };

            string password = txtPassword.Text;

            if (_userService.CreateUser(user, password, out string error))
            {
                MessageBox.Show($"User '{user.Username}' registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshUserList();
                ResetForm();
            }
            else
            {
                MessageBox.Show(error, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedUserId <= 0) return;

            var user = new User
            {
                UserID = _selectedUserId,
                Username = txtUsername.Text.Trim(),
                FullName = txtFullName.Text.Trim(),
                RoleID = cmbRole.SelectedValue != null ? Convert.ToInt32(cmbRole.SelectedValue) : 0,
                IsActive = chkIsActive.Checked
            };

            if (_userService.UpdateUser(user, out string error))
            {
                MessageBox.Show("User updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshUserList();
                ResetForm();
            }
            else
            {
                MessageBox.Show(error, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnToggleStatus_Click(object sender, EventArgs e)
        {
            if (_selectedUserId <= 0) return;

            string action = chkIsActive.Checked ? "deactivate" : "activate";
            var result = MessageBox.Show($"Are you sure you want to {action} this user account?", "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            if (_userService.ToggleActiveStatus(_selectedUserId, out string error))
            {
                MessageBox.Show($"User account status changed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshUserList();
                ResetForm();
            }
            else
            {
                MessageBox.Show(error, "Action Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (_selectedUserId <= 0) return;

            using var form = new ChangePasswordForm(_selectedUserId, txtUsername.Text);
            form.ShowDialog(this);
        }
    }
}
