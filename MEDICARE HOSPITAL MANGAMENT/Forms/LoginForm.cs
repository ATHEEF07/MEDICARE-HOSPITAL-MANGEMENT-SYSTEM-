using System;
using System.Drawing;
using System.Windows.Forms;
using MEDICARE_HOSPITAL_MANGAMENT.Services;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    public partial class LoginForm : Form
    {
        private readonly AuthenticationService _authService;

        public LoginForm()
        {
            InitializeComponent();
            _authService = new AuthenticationService();
            lblError.Visible = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            var user = _authService.Authenticate(username, password, out string errorMessage);

            if (user != null)
            {
                // Login successful
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                lblError.Text = errorMessage;
                lblError.Visible = true;
                txtPassword.SelectAll();
                txtPassword.Focus();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '●';
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
