using System;
using System.Windows.Forms;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Services;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    public partial class ChangePasswordForm : Form
    {
        private readonly UserService _userService;
        private readonly int _targetUserId;
        private readonly bool _isAdminReset;

        /// <summary>
        /// Self-service password change constructor (uses current user session).
        /// </summary>
        public ChangePasswordForm() : this(SessionManager.CurrentUserId, SessionManager.CurrentUsername, false)
        {
        }

        /// <summary>
        /// Administrator reset constructor for resetting another user's password.
        /// </summary>
        public ChangePasswordForm(int targetUserId, string targetUsername) : this(targetUserId, targetUsername, true)
        {
        }

        private ChangePasswordForm(int targetUserId, string targetUsername, bool isAdminReset)
        {
            InitializeComponent();
            _userService = new UserService();
            _targetUserId = targetUserId;
            _isAdminReset = isAdminReset;

            lblTargetUser.Text = $"User: {targetUsername}";

            if (_isAdminReset)
            {
                lblTitle.Text = "Reset User Password";
                lblCurrentPassword.Visible = false;
                txtCurrentPassword.Visible = false;
                // Shift new password controls up
                lblNewPassword.Top = 110;
                txtNewPassword.Top = 135;
                lblConfirmPassword.Top = 175;
                txtConfirmPassword.Top = 200;
                chkShowPassword.Top = 238;
                btnSubmit.Top = 275;
                btnCancel.Top = 275;
                this.Height = 370;
            }
            else
            {
                lblTitle.Text = "Change Your Password";
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
            {
                MessageBox.Show("New password must be at least 6 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return;
            }

            if (!string.Equals(newPassword, confirmPassword, StringComparison.Ordinal))
            {
                MessageBox.Show("New password and confirmation password do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return;
            }

            bool success;
            string errorMessage;

            if (_isAdminReset)
            {
                success = _userService.ResetPassword(_targetUserId, newPassword, out errorMessage);
            }
            else
            {
                string currentPassword = txtCurrentPassword.Text;
                success = _userService.ChangePassword(_targetUserId, currentPassword, newPassword, confirmPassword, out errorMessage);
            }

            if (success)
            {
                MessageBox.Show("Password updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            char mask = chkShowPassword.Checked ? '\0' : '●';
            txtCurrentPassword.PasswordChar = mask;
            txtNewPassword.PasswordChar = mask;
            txtConfirmPassword.PasswordChar = mask;
        }
    }
}
