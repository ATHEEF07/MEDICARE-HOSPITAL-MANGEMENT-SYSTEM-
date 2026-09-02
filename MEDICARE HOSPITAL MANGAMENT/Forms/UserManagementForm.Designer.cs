using System.Drawing;
using System.Windows.Forms;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    partial class UserManagementForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblHeaderSubtitle = new Label();
            lblHeaderTitle = new Label();
            pnlSidebar = new Panel();
            grpUserDetails = new GroupBox();
            btnResetPassword = new Button();
            btnToggleStatus = new Button();
            btnClear = new Button();
            btnUpdate = new Button();
            btnSave = new Button();
            chkIsActive = new CheckBox();
            txtPassword = new TextBox();
            lblPassword = new Label();
            cmbRole = new ComboBox();
            lblRole = new Label();
            txtFullName = new TextBox();
            lblFullName = new Label();
            txtUsername = new TextBox();
            lblUsername = new Label();
            pnlGridContainer = new Panel();
            pnlSearch = new Panel();
            txtSearch = new TextBox();
            lblSearch = new Label();
            dgvUsers = new DataGridView();
            pnlHeader.SuspendLayout();
            pnlSidebar.SuspendLayout();
            grpUserDetails.SuspendLayout();
            pnlGridContainer.SuspendLayout();
            pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(27, 54, 93);
            pnlHeader.Controls.Add(lblHeaderSubtitle);
            pnlHeader.Controls.Add(lblHeaderTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(20, 15, 20, 15);
            pnlHeader.Size = new Size(1080, 80);
            pnlHeader.TabIndex = 0;
            // 
            // lblHeaderSubtitle
            // 
            lblHeaderSubtitle.AutoSize = true;
            lblHeaderSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblHeaderSubtitle.ForeColor = Color.FromArgb(200, 220, 240);
            lblHeaderSubtitle.Location = new Point(22, 45);
            lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            lblHeaderSubtitle.Size = new Size(410, 21);
            lblHeaderSubtitle.TabIndex = 1;
            lblHeaderSubtitle.Text = "Manage hospital staff accounts and role-based permissions";
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(20, 12);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(335, 35);
            lblHeaderTitle.TabIndex = 0;
            lblHeaderTitle.Text = "User and Role Management";
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(248, 249, 250);
            pnlSidebar.Controls.Add(grpUserDetails);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 80);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Padding = new Padding(15);
            pnlSidebar.Size = new Size(380, 570);
            pnlSidebar.TabIndex = 1;
            // 
            // grpUserDetails
            // 
            grpUserDetails.Controls.Add(btnResetPassword);
            grpUserDetails.Controls.Add(btnToggleStatus);
            grpUserDetails.Controls.Add(btnClear);
            grpUserDetails.Controls.Add(btnUpdate);
            grpUserDetails.Controls.Add(btnSave);
            grpUserDetails.Controls.Add(chkIsActive);
            grpUserDetails.Controls.Add(txtPassword);
            grpUserDetails.Controls.Add(lblPassword);
            grpUserDetails.Controls.Add(cmbRole);
            grpUserDetails.Controls.Add(lblRole);
            grpUserDetails.Controls.Add(txtFullName);
            grpUserDetails.Controls.Add(lblFullName);
            grpUserDetails.Controls.Add(txtUsername);
            grpUserDetails.Controls.Add(lblUsername);
            grpUserDetails.Dock = DockStyle.Fill;
            grpUserDetails.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpUserDetails.ForeColor = Color.FromArgb(27, 54, 93);
            grpUserDetails.Location = new Point(15, 15);
            grpUserDetails.Name = "grpUserDetails";
            grpUserDetails.Padding = new Padding(15);
            grpUserDetails.Size = new Size(350, 540);
            grpUserDetails.TabIndex = 0;
            grpUserDetails.TabStop = false;
            grpUserDetails.Text = "User Details";
            // 
            // btnResetPassword
            // 
            btnResetPassword.BackColor = Color.FromArgb(108, 117, 125);
            btnResetPassword.Cursor = Cursors.Hand;
            btnResetPassword.FlatAppearance.BorderSize = 0;
            btnResetPassword.FlatStyle = FlatStyle.Flat;
            btnResetPassword.Font = new Font("Segoe UI", 9.5F);
            btnResetPassword.ForeColor = Color.White;
            btnResetPassword.Location = new Point(175, 480);
            btnResetPassword.Name = "btnResetPassword";
            btnResetPassword.Size = new Size(150, 36);
            btnResetPassword.TabIndex = 13;
            btnResetPassword.Text = "Reset Password";
            btnResetPassword.UseVisualStyleBackColor = false;
            btnResetPassword.Click += btnResetPassword_Click;
            // 
            // btnToggleStatus
            // 
            btnToggleStatus.BackColor = Color.FromArgb(220, 53, 69);
            btnToggleStatus.Cursor = Cursors.Hand;
            btnToggleStatus.FlatAppearance.BorderSize = 0;
            btnToggleStatus.FlatStyle = FlatStyle.Flat;
            btnToggleStatus.Font = new Font("Segoe UI", 9.5F);
            btnToggleStatus.ForeColor = Color.White;
            btnToggleStatus.Location = new Point(15, 480);
            btnToggleStatus.Name = "btnToggleStatus";
            btnToggleStatus.Size = new Size(150, 36);
            btnToggleStatus.TabIndex = 12;
            btnToggleStatus.Text = "Deactivate";
            btnToggleStatus.UseVisualStyleBackColor = false;
            btnToggleStatus.Click += btnToggleStatus_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.FromArgb(230, 230, 230);
            btnClear.Cursor = Cursors.Hand;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 9.5F);
            btnClear.ForeColor = Color.FromArgb(40, 40, 40);
            btnClear.Location = new Point(15, 435);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(310, 36);
            btnClear.TabIndex = 11;
            btnClear.Text = "Clear / New User";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.FromArgb(40, 167, 69);
            btnUpdate.Cursor = Cursors.Hand;
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Location = new Point(175, 390);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(150, 38);
            btnUpdate.TabIndex = 10;
            btnUpdate.Text = "Update User";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(27, 54, 93);
            btnSave.Cursor = Cursors.Hand;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(15, 390);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(150, 38);
            btnSave.TabIndex = 9;
            btnSave.Text = "Add User";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // chkIsActive
            // 
            chkIsActive.AutoSize = true;
            chkIsActive.Checked = true;
            chkIsActive.CheckState = CheckState.Checked;
            chkIsActive.Font = new Font("Segoe UI", 9.5F);
            chkIsActive.ForeColor = Color.FromArgb(40, 40, 40);
            chkIsActive.Location = new Point(20, 345);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(139, 25);
            chkIsActive.TabIndex = 8;
            chkIsActive.Text = "Account Active";
            chkIsActive.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 9.5F);
            txtPassword.Location = new Point(18, 300);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.Size = new Size(310, 29);
            txtPassword.TabIndex = 7;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblPassword.ForeColor = Color.FromArgb(40, 40, 40);
            lblPassword.Location = new Point(15, 275);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(187, 20);
            lblPassword.TabIndex = 6;
            lblPassword.Text = "Password (min 6 characters)";
            // 
            // cmbRole
            // 
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.Font = new Font("Segoe UI", 9.5F);
            cmbRole.FormattingEnabled = true;
            cmbRole.Location = new Point(18, 225);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(310, 29);
            cmbRole.TabIndex = 5;
            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblRole.ForeColor = Color.FromArgb(40, 40, 40);
            lblRole.Location = new Point(15, 200);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(90, 20);
            lblRole.TabIndex = 4;
            lblRole.Text = "System Role";
            // 
            // txtFullName
            // 
            txtFullName.BorderStyle = BorderStyle.FixedSingle;
            txtFullName.Font = new Font("Segoe UI", 9.5F);
            txtFullName.Location = new Point(18, 150);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(310, 29);
            txtFullName.TabIndex = 3;
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblFullName.ForeColor = Color.FromArgb(40, 40, 40);
            lblFullName.Location = new Point(15, 125);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(76, 20);
            lblFullName.TabIndex = 2;
            lblFullName.Text = "Full Name";
            // 
            // txtUsername
            // 
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 9.5F);
            txtUsername.Location = new Point(18, 75);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(310, 29);
            txtUsername.TabIndex = 1;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblUsername.ForeColor = Color.FromArgb(40, 40, 40);
            lblUsername.Location = new Point(15, 50);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(75, 20);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Username";
            // 
            // pnlGridContainer
            // 
            pnlGridContainer.Controls.Add(dgvUsers);
            pnlGridContainer.Controls.Add(pnlSearch);
            pnlGridContainer.Dock = DockStyle.Fill;
            pnlGridContainer.Location = new Point(380, 80);
            pnlGridContainer.Name = "pnlGridContainer";
            pnlGridContainer.Padding = new Padding(15);
            pnlGridContainer.Size = new Size(700, 570);
            pnlGridContainer.TabIndex = 2;
            // 
            // pnlSearch
            // 
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Controls.Add(lblSearch);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(15, 15);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Size = new Size(670, 45);
            pnlSearch.TabIndex = 0;
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Segoe UI", 10F);
            txtSearch.Location = new Point(70, 7);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search by username, full name, or role...";
            txtSearch.Size = new Size(590, 30);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSearch.ForeColor = Color.FromArgb(50, 50, 50);
            lblSearch.Location = new Point(0, 10);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(68, 23);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Search:";
            // 
            // dgvUsers
            // 
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.BackgroundColor = Color.White;
            dgvUsers.BorderStyle = BorderStyle.Fixed3D;
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Dock = DockStyle.Fill;
            dgvUsers.Location = new Point(15, 60);
            dgvUsers.MultiSelect = false;
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dgvUsers.RowHeadersWidth = 30;
            dgvUsers.RowTemplate.Height = 28;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.Size = new Size(670, 495);
            dgvUsers.TabIndex = 1;
            dgvUsers.SelectionChanged += dgvUsers_SelectionChanged;
            // 
            // UserManagementForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1080, 650);
            Controls.Add(pnlGridContainer);
            Controls.Add(pnlSidebar);
            Controls.Add(pnlHeader);
            MinimumSize = new Size(950, 550);
            Name = "UserManagementForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MediCare - User Management";
            Load += UserManagementForm_Load;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlSidebar.ResumeLayout(false);
            grpUserDetails.ResumeLayout(false);
            grpUserDetails.PerformLayout();
            pnlGridContainer.ResumeLayout(false);
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblHeaderTitle;
        private Label lblHeaderSubtitle;
        private Panel pnlSidebar;
        private GroupBox grpUserDetails;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblRole;
        private ComboBox cmbRole;
        private Label lblPassword;
        private TextBox txtPassword;
        private CheckBox chkIsActive;
        private Button btnSave;
        private Button btnUpdate;
        private Button btnClear;
        private Button btnToggleStatus;
        private Button btnResetPassword;
        private Panel pnlGridContainer;
        private Panel pnlSearch;
        private Label lblSearch;
        private TextBox txtSearch;
        private DataGridView dgvUsers;
    }
}
