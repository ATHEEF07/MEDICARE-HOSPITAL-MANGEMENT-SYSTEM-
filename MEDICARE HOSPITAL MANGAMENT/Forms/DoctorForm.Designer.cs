using System.Drawing;
using System.Windows.Forms;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    partial class DoctorForm
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
            grpDoctor = new GroupBox();
            btnClear = new Button();
            btnUpdate = new Button();
            btnSave = new Button();
            chkIsActive = new CheckBox();
            txtPhone = new TextBox();
            lblPhone = new Label();
            txtSpecialization = new TextBox();
            lblSpecialization = new Label();
            cmbDepartment = new ComboBox();
            lblDepartment = new Label();
            txtLastName = new TextBox();
            lblLastName = new Label();
            txtFirstName = new TextBox();
            lblFirstName = new Label();
            cmbUserAccount = new ComboBox();
            lblUserAccount = new Label();
            txtDoctorCode = new TextBox();
            lblDoctorCode = new Label();
            pnlGridContainer = new Panel();
            pnlFilters = new Panel();
            cmbFilterDept = new ComboBox();
            lblFilterDept = new Label();
            txtSearch = new TextBox();
            lblSearch = new Label();
            dgvDoctors = new DataGridView();
            pnlHeader.SuspendLayout();
            pnlSidebar.SuspendLayout();
            grpDoctor.SuspendLayout();
            pnlGridContainer.SuspendLayout();
            pnlFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDoctors).BeginInit();
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
            pnlHeader.Size = new Size(1100, 80);
            pnlHeader.TabIndex = 0;
            // 
            // lblHeaderSubtitle
            // 
            lblHeaderSubtitle.AutoSize = true;
            lblHeaderSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblHeaderSubtitle.ForeColor = Color.FromArgb(200, 220, 240);
            lblHeaderSubtitle.Location = new Point(22, 45);
            lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            lblHeaderSubtitle.Size = new Size(395, 21);
            lblHeaderSubtitle.TabIndex = 1;
            lblHeaderSubtitle.Text = "Register and manage medical specialists and departments";
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(20, 12);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(254, 35);
            lblHeaderTitle.TabIndex = 0;
            lblHeaderTitle.Text = "Doctor Management";
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(248, 249, 250);
            pnlSidebar.Controls.Add(grpDoctor);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 80);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Padding = new Padding(15);
            pnlSidebar.Size = new Size(380, 590);
            pnlSidebar.TabIndex = 1;
            // 
            // grpDoctor
            // 
            grpDoctor.Controls.Add(btnClear);
            grpDoctor.Controls.Add(btnUpdate);
            grpDoctor.Controls.Add(btnSave);
            grpDoctor.Controls.Add(chkIsActive);
            grpDoctor.Controls.Add(txtPhone);
            grpDoctor.Controls.Add(lblPhone);
            grpDoctor.Controls.Add(txtSpecialization);
            grpDoctor.Controls.Add(lblSpecialization);
            grpDoctor.Controls.Add(cmbDepartment);
            grpDoctor.Controls.Add(lblDepartment);
            grpDoctor.Controls.Add(txtLastName);
            grpDoctor.Controls.Add(lblLastName);
            grpDoctor.Controls.Add(txtFirstName);
            grpDoctor.Controls.Add(lblFirstName);
            grpDoctor.Controls.Add(cmbUserAccount);
            grpDoctor.Controls.Add(lblUserAccount);
            grpDoctor.Controls.Add(txtDoctorCode);
            grpDoctor.Controls.Add(lblDoctorCode);
            grpDoctor.Dock = DockStyle.Fill;
            grpDoctor.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpDoctor.ForeColor = Color.FromArgb(27, 54, 93);
            grpDoctor.Location = new Point(15, 15);
            grpDoctor.Name = "grpDoctor";
            grpDoctor.Padding = new Padding(15);
            grpDoctor.Size = new Size(350, 560);
            grpDoctor.TabIndex = 0;
            grpDoctor.TabStop = false;
            grpDoctor.Text = "Doctor Profile";
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.FromArgb(230, 230, 230);
            btnClear.Cursor = Cursors.Hand;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 9.5F);
            btnClear.ForeColor = Color.FromArgb(40, 40, 40);
            btnClear.Location = new Point(15, 510);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(310, 36);
            btnClear.TabIndex = 17;
            btnClear.Text = "Clear Fields";
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
            btnUpdate.Location = new Point(175, 465);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(150, 38);
            btnUpdate.TabIndex = 16;
            btnUpdate.Text = "Update";
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
            btnSave.Location = new Point(15, 465);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(150, 38);
            btnSave.TabIndex = 15;
            btnSave.Text = "Register Doctor";
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
            chkIsActive.Location = new Point(18, 430);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(134, 25);
            chkIsActive.TabIndex = 14;
            chkIsActive.Text = "Active On-Duty";
            chkIsActive.UseVisualStyleBackColor = true;
            // 
            // txtPhone
            // 
            txtPhone.BorderStyle = BorderStyle.FixedSingle;
            txtPhone.Font = new Font("Segoe UI", 9.5F);
            txtPhone.Location = new Point(15, 395);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(310, 29);
            txtPhone.TabIndex = 13;
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Font = new Font("Segoe UI", 8.5F);
            lblPhone.ForeColor = Color.FromArgb(40, 40, 40);
            lblPhone.Location = new Point(12, 373);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(108, 19);
            lblPhone.TabIndex = 12;
            lblPhone.Text = "Contact Number";
            // 
            // txtSpecialization
            // 
            txtSpecialization.BorderStyle = BorderStyle.FixedSingle;
            txtSpecialization.Font = new Font("Segoe UI", 9.5F);
            txtSpecialization.Location = new Point(15, 340);
            txtSpecialization.Name = "txtSpecialization";
            txtSpecialization.Size = new Size(310, 29);
            txtSpecialization.TabIndex = 11;
            // 
            // lblSpecialization
            // 
            lblSpecialization.AutoSize = true;
            lblSpecialization.Font = new Font("Segoe UI", 8.5F);
            lblSpecialization.ForeColor = Color.FromArgb(40, 40, 40);
            lblSpecialization.Location = new Point(12, 318);
            lblSpecialization.Name = "lblSpecialization";
            lblSpecialization.Size = new Size(93, 19);
            lblSpecialization.TabIndex = 10;
            lblSpecialization.Text = "Specialization";
            // 
            // cmbDepartment
            // 
            cmbDepartment.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDepartment.Font = new Font("Segoe UI", 9.5F);
            cmbDepartment.FormattingEnabled = true;
            cmbDepartment.Location = new Point(15, 285);
            cmbDepartment.Name = "cmbDepartment";
            cmbDepartment.Size = new Size(310, 29);
            cmbDepartment.TabIndex = 9;
            // 
            // lblDepartment
            // 
            lblDepartment.AutoSize = true;
            lblDepartment.Font = new Font("Segoe UI", 8.5F);
            lblDepartment.ForeColor = Color.FromArgb(40, 40, 40);
            lblDepartment.Location = new Point(12, 263);
            lblDepartment.Name = "lblDepartment";
            lblDepartment.Size = new Size(141, 19);
            lblDepartment.TabIndex = 8;
            lblDepartment.Text = "Clinical Department *";
            // 
            // txtLastName
            // 
            txtLastName.BorderStyle = BorderStyle.FixedSingle;
            txtLastName.Font = new Font("Segoe UI", 9.5F);
            txtLastName.Location = new Point(15, 230);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(310, 29);
            txtLastName.TabIndex = 7;
            // 
            // lblLastName
            // 
            lblLastName.AutoSize = true;
            lblLastName.Font = new Font("Segoe UI", 8.5F);
            lblLastName.ForeColor = Color.FromArgb(40, 40, 40);
            lblLastName.Location = new Point(12, 208);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(84, 19);
            lblLastName.TabIndex = 6;
            lblLastName.Text = "Last Name *";
            // 
            // txtFirstName
            // 
            txtFirstName.BorderStyle = BorderStyle.FixedSingle;
            txtFirstName.Font = new Font("Segoe UI", 9.5F);
            txtFirstName.Location = new Point(15, 175);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(310, 29);
            txtFirstName.TabIndex = 5;
            // 
            // lblFirstName
            // 
            lblFirstName.AutoSize = true;
            lblFirstName.Font = new Font("Segoe UI", 8.5F);
            lblFirstName.ForeColor = Color.FromArgb(40, 40, 40);
            lblFirstName.Location = new Point(12, 153);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(85, 19);
            lblFirstName.TabIndex = 4;
            lblFirstName.Text = "First Name *";
            // 
            // cmbUserAccount
            // 
            cmbUserAccount.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUserAccount.Font = new Font("Segoe UI", 9.5F);
            cmbUserAccount.FormattingEnabled = true;
            cmbUserAccount.Location = new Point(15, 120);
            cmbUserAccount.Name = "cmbUserAccount";
            cmbUserAccount.Size = new Size(310, 29);
            cmbUserAccount.TabIndex = 3;
            // 
            // lblUserAccount
            // 
            lblUserAccount.AutoSize = true;
            lblUserAccount.Font = new Font("Segoe UI", 8.5F);
            lblUserAccount.ForeColor = Color.FromArgb(40, 40, 40);
            lblUserAccount.Location = new Point(12, 98);
            lblUserAccount.Name = "lblUserAccount";
            lblUserAccount.Size = new Size(177, 19);
            lblUserAccount.TabIndex = 2;
            lblUserAccount.Text = "Linked Doctor User Account";
            // 
            // txtDoctorCode
            // 
            txtDoctorCode.BackColor = Color.FromArgb(240, 240, 240);
            txtDoctorCode.BorderStyle = BorderStyle.FixedSingle;
            txtDoctorCode.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            txtDoctorCode.ForeColor = Color.FromArgb(27, 54, 93);
            txtDoctorCode.Location = new Point(15, 65);
            txtDoctorCode.Name = "txtDoctorCode";
            txtDoctorCode.ReadOnly = true;
            txtDoctorCode.Size = new Size(310, 29);
            txtDoctorCode.TabIndex = 1;
            // 
            // lblDoctorCode
            // 
            lblDoctorCode.AutoSize = true;
            lblDoctorCode.Font = new Font("Segoe UI", 8.5F);
            lblDoctorCode.ForeColor = Color.FromArgb(40, 40, 40);
            lblDoctorCode.Location = new Point(12, 43);
            lblDoctorCode.Name = "lblDoctorCode";
            lblDoctorCode.Size = new Size(86, 19);
            lblDoctorCode.TabIndex = 0;
            lblDoctorCode.Text = "Doctor Code";
            // 
            // pnlGridContainer
            // 
            pnlGridContainer.Controls.Add(dgvDoctors);
            pnlGridContainer.Controls.Add(pnlFilters);
            pnlGridContainer.Dock = DockStyle.Fill;
            pnlGridContainer.Location = new Point(380, 80);
            pnlGridContainer.Name = "pnlGridContainer";
            pnlGridContainer.Padding = new Padding(15);
            pnlGridContainer.Size = new Size(720, 590);
            pnlGridContainer.TabIndex = 2;
            // 
            // pnlFilters
            // 
            pnlFilters.Controls.Add(cmbFilterDept);
            pnlFilters.Controls.Add(lblFilterDept);
            pnlFilters.Controls.Add(txtSearch);
            pnlFilters.Controls.Add(lblSearch);
            pnlFilters.Dock = DockStyle.Top;
            pnlFilters.Location = new Point(15, 15);
            pnlFilters.Name = "pnlFilters";
            pnlFilters.Size = new Size(690, 45);
            pnlFilters.TabIndex = 0;
            // 
            // cmbFilterDept
            // 
            cmbFilterDept.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbFilterDept.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilterDept.Font = new Font("Segoe UI", 9.5F);
            cmbFilterDept.FormattingEnabled = true;
            cmbFilterDept.Location = new Point(480, 7);
            cmbFilterDept.Name = "cmbFilterDept";
            cmbFilterDept.Size = new Size(210, 29);
            cmbFilterDept.TabIndex = 3;
            cmbFilterDept.SelectedIndexChanged += cmbFilterDept_SelectedIndexChanged;
            // 
            // lblFilterDept
            // 
            lblFilterDept.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblFilterDept.AutoSize = true;
            lblFilterDept.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblFilterDept.ForeColor = Color.FromArgb(50, 50, 50);
            lblFilterDept.Location = new Point(370, 10);
            lblFilterDept.Name = "lblFilterDept";
            lblFilterDept.Size = new Size(107, 21);
            lblFilterDept.TabIndex = 2;
            lblFilterDept.Text = "Department:";
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Segoe UI", 9.5F);
            txtSearch.Location = new Point(70, 7);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search by name, code, or specialization...";
            txtSearch.Size = new Size(290, 29);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSearch.ForeColor = Color.FromArgb(50, 50, 50);
            lblSearch.Location = new Point(0, 10);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(66, 21);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Search:";
            // 
            // dgvDoctors
            // 
            dgvDoctors.AllowUserToAddRows = false;
            dgvDoctors.AllowUserToDeleteRows = false;
            dgvDoctors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDoctors.BackgroundColor = Color.White;
            dgvDoctors.BorderStyle = BorderStyle.Fixed3D;
            dgvDoctors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDoctors.Dock = DockStyle.Fill;
            dgvDoctors.Location = new Point(15, 60);
            dgvDoctors.MultiSelect = false;
            dgvDoctors.Name = "dgvDoctors";
            dgvDoctors.ReadOnly = true;
            dgvDoctors.RowHeadersWidth = 30;
            dgvDoctors.RowTemplate.Height = 28;
            dgvDoctors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDoctors.Size = new Size(690, 515);
            dgvDoctors.TabIndex = 1;
            dgvDoctors.SelectionChanged += dgvDoctors_SelectionChanged;
            // 
            // DoctorForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1100, 670);
            Controls.Add(pnlGridContainer);
            Controls.Add(pnlSidebar);
            Controls.Add(pnlHeader);
            MinimumSize = new Size(950, 550);
            Name = "DoctorForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MediCare - Doctor Management";
            Load += DoctorForm_Load;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlSidebar.ResumeLayout(false);
            grpDoctor.ResumeLayout(false);
            grpDoctor.PerformLayout();
            pnlGridContainer.ResumeLayout(false);
            pnlFilters.ResumeLayout(false);
            pnlFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDoctors).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblHeaderTitle;
        private Label lblHeaderSubtitle;
        private Panel pnlSidebar;
        private GroupBox grpDoctor;
        private Label lblDoctorCode;
        private TextBox txtDoctorCode;
        private Label lblUserAccount;
        private ComboBox cmbUserAccount;
        private Label lblFirstName;
        private TextBox txtFirstName;
        private Label lblLastName;
        private TextBox txtLastName;
        private Label lblDepartment;
        private ComboBox cmbDepartment;
        private Label lblSpecialization;
        private TextBox txtSpecialization;
        private Label lblPhone;
        private TextBox txtPhone;
        private CheckBox chkIsActive;
        private Button btnSave;
        private Button btnUpdate;
        private Button btnClear;
        private Panel pnlGridContainer;
        private Panel pnlFilters;
        private Label lblSearch;
        private TextBox txtSearch;
        private Label lblFilterDept;
        private ComboBox cmbFilterDept;
        private DataGridView dgvDoctors;
    }
}
