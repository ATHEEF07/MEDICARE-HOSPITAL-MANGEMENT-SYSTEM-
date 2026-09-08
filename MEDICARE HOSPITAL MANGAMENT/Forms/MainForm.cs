using System;
using System.Drawing;
using System.Windows.Forms;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    /// <summary>
    /// Master Application Shell and Navigation Container.
    /// Manages top-level window state, session indicators, role-based navigation sidebar,
    /// and dynamic hosting of child functional modules.
    /// </summary>
    public partial class MainForm : Form
    {
        private Form? _activeChildForm;
        private Button? _activeNavButton;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetupSessionDisplay();
            CheckDatabaseStatus();
            ApplyRoleBasedNavigation();

            // Load Executive Dashboard as initial default view
            SetActiveNavButton(btnNavDashboard);
            LoadChildForm(new DashboardForm());
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Session & Security Display
        // ─────────────────────────────────────────────────────────────────────────

        private void SetupSessionDisplay()
        {
            string fullName = SessionManager.CurrentUserFullName;
            if (string.IsNullOrWhiteSpace(fullName)) fullName = SessionManager.CurrentUsername;
            if (string.IsNullOrWhiteSpace(fullName)) fullName = "Administrator";

            lblUserName.Text = $"👤 {fullName}";

            string role = SessionManager.CurrentUserRole;
            if (string.IsNullOrWhiteSpace(role)) role = "Administrator";

            lblUserRole.Text = $"ROLE: {role.ToUpper()}";

            // Role-specific badge styling
            switch (role.ToLower())
            {
                case "doctor":
                    lblUserRole.BackColor = Color.FromArgb(6, 95, 70); // Emerald
                    lblUserRole.ForeColor = Color.FromArgb(167, 243, 208);
                    break;
                case "pharmacist":
                    lblUserRole.BackColor = Color.FromArgb(120, 53, 15); // Amber
                    lblUserRole.ForeColor = Color.FromArgb(254, 215, 170);
                    break;
                case "cashier":
                    lblUserRole.BackColor = Color.FromArgb(19, 78, 74); // Teal
                    lblUserRole.ForeColor = Color.FromArgb(153, 246, 228);
                    break;
                case "receptionist":
                    lblUserRole.BackColor = Color.FromArgb(7, 89, 133); // Sky
                    lblUserRole.ForeColor = Color.FromArgb(186, 230, 253);
                    break;
                default: // Administrator
                    lblUserRole.BackColor = Color.FromArgb(30, 58, 138); // Royal Blue
                    lblUserRole.ForeColor = Color.FromArgb(191, 219, 254);
                    break;
            }
        }

        private void CheckDatabaseStatus()
        {
            bool isOnline = DatabaseHelper.TestConnection();
            if (isOnline)
            {
                lblDbStatus.Text = "🟢 Database: Connected";
                lblDbStatus.ForeColor = Color.FromArgb(74, 222, 128);
            }
            else
            {
                lblDbStatus.Text = "🔴 Database: Offline";
                lblDbStatus.ForeColor = Color.FromArgb(248, 113, 113);
            }
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Role-Based Access Control Navigation
        // ─────────────────────────────────────────────────────────────────────────

        private void ApplyRoleBasedNavigation()
        {
            if (SessionManager.IsAdmin())
            {
                // Full unrestricted access
                btnNavDashboard.Visible     = true;
                btnNavPatients.Visible      = true;
                btnNavDoctors.Visible       = true;
                btnNavAppointments.Visible  = true;
                btnNavConsultations.Visible = true;
                btnNavPharmacy.Visible      = true;
                btnNavBilling.Visible       = true;
                btnNavReports.Visible       = true;
                btnNavUsers.Visible         = true;
            }
            else if (SessionManager.IsDoctor())
            {
                // Clinical consultation & patient records
                btnNavDashboard.Visible     = true;
                btnNavPatients.Visible      = true;
                btnNavDoctors.Visible       = false;
                btnNavAppointments.Visible  = true;
                btnNavConsultations.Visible = true;
                btnNavPharmacy.Visible      = false;
                btnNavBilling.Visible       = false;
                btnNavReports.Visible       = true;
                btnNavUsers.Visible         = false;
            }
            else if (SessionManager.IsReceptionist())
            {
                // Patient registration & scheduling
                btnNavDashboard.Visible     = true;
                btnNavPatients.Visible      = true;
                btnNavDoctors.Visible       = false;
                btnNavAppointments.Visible  = true;
                btnNavConsultations.Visible = false;
                btnNavPharmacy.Visible      = false;
                btnNavBilling.Visible       = false;
                btnNavReports.Visible       = false;
                btnNavUsers.Visible         = false;
            }
            else if (SessionManager.IsPharmacist())
            {
                // Medicine inventory & dispensary
                btnNavDashboard.Visible     = true;
                btnNavPatients.Visible      = false;
                btnNavDoctors.Visible       = false;
                btnNavAppointments.Visible  = false;
                btnNavConsultations.Visible = false;
                btnNavPharmacy.Visible      = true;
                btnNavBilling.Visible       = false;
                btnNavReports.Visible       = true;
                btnNavUsers.Visible         = false;
            }
            else if (SessionManager.IsCashier())
            {
                // Invoicing, billing & payments
                btnNavDashboard.Visible     = true;
                btnNavPatients.Visible      = false;
                btnNavDoctors.Visible       = false;
                btnNavAppointments.Visible  = false;
                btnNavConsultations.Visible = false;
                btnNavPharmacy.Visible      = false;
                btnNavBilling.Visible       = true;
                btnNavReports.Visible       = true;
                btnNavUsers.Visible         = false;
            }
            else
            {
                // Generic authenticated user
                btnNavDashboard.Visible     = true;
                btnNavPatients.Visible      = false;
                btnNavDoctors.Visible       = false;
                btnNavAppointments.Visible  = false;
                btnNavConsultations.Visible = false;
                btnNavPharmacy.Visible      = false;
                btnNavBilling.Visible       = false;
                btnNavReports.Visible       = false;
                btnNavUsers.Visible         = false;
            }
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Child Form Hosting Architecture
        // ─────────────────────────────────────────────────────────────────────────

        public void LoadChildForm(Form childForm)
        {
            if (_activeChildForm != null)
            {
                _activeChildForm.Close();
                _activeChildForm.Dispose();
            }

            _activeChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(childForm);
            pnlContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void SetActiveNavButton(Button btn)
        {
            // Reset previous button style
            if (_activeNavButton != null)
            {
                _activeNavButton.BackColor = Color.FromArgb(30, 41, 59);
                _activeNavButton.ForeColor = Color.FromArgb(226, 232, 240);
            }

            _activeNavButton = btn;
            _activeNavButton.BackColor = Color.FromArgb(37, 99, 235); // Royal Blue
            _activeNavButton.ForeColor = Color.White;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Navigation Handlers
        // ─────────────────────────────────────────────────────────────────────────

        private void BtnNavDashboard_Click(object sender, EventArgs e)
        {
            SetActiveNavButton(btnNavDashboard);
            LoadChildForm(new DashboardForm());
        }

        private void BtnNavPatients_Click(object sender, EventArgs e)
        {
            SetActiveNavButton(btnNavPatients);
            LoadChildForm(new PatientListForm());
        }

        private void BtnNavDoctors_Click(object sender, EventArgs e)
        {
            SetActiveNavButton(btnNavDoctors);
            LoadChildForm(new DoctorForm());
        }

        private void BtnNavAppointments_Click(object sender, EventArgs e)
        {
            SetActiveNavButton(btnNavAppointments);
            LoadChildForm(new AppointmentListForm());
        }

        private void BtnNavConsultations_Click(object sender, EventArgs e)
        {
            SetActiveNavButton(btnNavConsultations);
            LoadChildForm(new AppointmentListForm());
        }

        private void BtnNavPharmacy_Click(object sender, EventArgs e)
        {
            SetActiveNavButton(btnNavPharmacy);
            LoadChildForm(new MedicineForm());
        }

        private void BtnNavBilling_Click(object sender, EventArgs e)
        {
            SetActiveNavButton(btnNavBilling);
            LoadChildForm(new BillingForm());
        }

        private void BtnNavReports_Click(object sender, EventArgs e)
        {
            SetActiveNavButton(btnNavReports);
            LoadChildForm(new ReportsForm());
        }

        private void BtnNavUsers_Click(object sender, EventArgs e)
        {
            SetActiveNavButton(btnNavUsers);
            LoadChildForm(new UserManagementForm());
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Header Actions
        // ─────────────────────────────────────────────────────────────────────────

        private void BtnChangePassword_Click(object sender, EventArgs e)
        {
            using var pwdForm = new ChangePasswordForm();
            pwdForm.ShowDialog(this);
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Are you sure you want to log out of the system?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                SessionManager.Logout();
                this.Hide();

                using var loginForm = new LoginForm();
                if (loginForm.ShowDialog() == DialogResult.OK && SessionManager.IsLoggedIn)
                {
                    SetupSessionDisplay();
                    ApplyRoleBasedNavigation();
                    SetActiveNavButton(btnNavDashboard);
                    LoadChildForm(new DashboardForm());
                    this.Show();
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_activeChildForm != null)
            {
                _activeChildForm.Close();
                _activeChildForm.Dispose();
                _activeChildForm = null;
            }
        }
    }
}
