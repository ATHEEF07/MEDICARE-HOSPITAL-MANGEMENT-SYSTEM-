namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    partial class MainForm
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
            this.pnlTopHeader = new System.Windows.Forms.Panel();
            this.lblHeaderSubtitle = new System.Windows.Forms.Label();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.pnlUserSection = new System.Windows.Forms.Panel();
            this.lblUserRole = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.pnlNavContainer = new System.Windows.Forms.Panel();
            this.btnNavUsers = new System.Windows.Forms.Button();
            this.btnNavReports = new System.Windows.Forms.Button();
            this.btnNavBilling = new System.Windows.Forms.Button();
            this.btnNavPharmacy = new System.Windows.Forms.Button();
            this.btnNavConsultations = new System.Windows.Forms.Button();
            this.btnNavAppointments = new System.Windows.Forms.Button();
            this.btnNavDoctors = new System.Windows.Forms.Button();
            this.btnNavPatients = new System.Windows.Forms.Button();
            this.btnNavDashboard = new System.Windows.Forms.Button();
            this.pnlNavHeader = new System.Windows.Forms.Panel();
            this.lblNavHeader = new System.Windows.Forms.Label();
            this.pnlSidebarBottom = new System.Windows.Forms.Panel();
            this.lblSystemVersion = new System.Windows.Forms.Label();
            this.lblDbStatus = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlTopHeader.SuspendLayout();
            this.pnlUserSection.SuspendLayout();
            this.pnlSidebar.SuspendLayout();
            this.pnlNavContainer.SuspendLayout();
            this.pnlNavHeader.SuspendLayout();
            this.pnlSidebarBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTopHeader
            // 
            this.pnlTopHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlTopHeader.Controls.Add(this.lblHeaderSubtitle);
            this.pnlTopHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlTopHeader.Controls.Add(this.pnlUserSection);
            this.pnlTopHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlTopHeader.Name = "pnlTopHeader";
            this.pnlTopHeader.Size = new System.Drawing.Size(1264, 60);
            this.pnlTopHeader.TabIndex = 0;
            // 
            // lblHeaderSubtitle
            // 
            this.lblHeaderSubtitle.AutoSize = true;
            this.lblHeaderSubtitle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHeaderSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblHeaderSubtitle.Location = new System.Drawing.Point(22, 34);
            this.lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            this.lblHeaderSubtitle.Size = new System.Drawing.Size(256, 13);
            this.lblHeaderSubtitle.TabIndex = 2;
            this.lblHeaderSubtitle.Text = "Enterprise Clinical Care & Hospital Administration";
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(20, 9);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(370, 25);
            this.lblHeaderTitle.TabIndex = 1;
            this.lblHeaderTitle.Text = "🏥 MediCare Hospital Management System";
            // 
            // pnlUserSection
            // 
            this.pnlUserSection.Controls.Add(this.lblUserRole);
            this.pnlUserSection.Controls.Add(this.lblUserName);
            this.pnlUserSection.Controls.Add(this.btnChangePassword);
            this.pnlUserSection.Controls.Add(this.btnLogout);
            this.pnlUserSection.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlUserSection.Location = new System.Drawing.Point(744, 0);
            this.pnlUserSection.Name = "pnlUserSection";
            this.pnlUserSection.Size = new System.Drawing.Size(520, 60);
            this.pnlUserSection.TabIndex = 0;
            // 
            // lblUserRole
            // 
            this.lblUserRole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.lblUserRole.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblUserRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblUserRole.Location = new System.Drawing.Point(10, 32);
            this.lblUserRole.Name = "lblUserRole";
            this.lblUserRole.Size = new System.Drawing.Size(220, 18);
            this.lblUserRole.TabIndex = 3;
            this.lblUserRole.Text = "ROLE: ADMINISTRATOR";
            this.lblUserRole.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUserName
            // 
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblUserName.ForeColor = System.Drawing.Color.White;
            this.lblUserName.Location = new System.Drawing.Point(10, 10);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(220, 20);
            this.lblUserName.TabIndex = 2;
            this.lblUserName.Text = "👤 Administrator";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnChangePassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChangePassword.FlatAppearance.BorderSize = 0;
            this.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePassword.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnChangePassword.ForeColor = System.Drawing.Color.White;
            this.btnChangePassword.Location = new System.Drawing.Point(245, 14);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(145, 32);
            this.btnChangePassword.TabIndex = 1;
            this.btnChangePassword.Text = "🔑 Change Password";
            this.btnChangePassword.UseVisualStyleBackColor = false;
            this.btnChangePassword.Click += new System.EventHandler(this.BtnChangePassword_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(400, 14);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(100, 32);
            this.btnLogout.TabIndex = 0;
            this.btnLogout.Text = "🚪 Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.BtnLogout_Click);
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlSidebar.Controls.Add(this.pnlNavContainer);
            this.pnlSidebar.Controls.Add(this.pnlNavHeader);
            this.pnlSidebar.Controls.Add(this.pnlSidebarBottom);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 60);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(220, 661);
            this.pnlSidebar.TabIndex = 1;
            // 
            // pnlNavContainer
            // 
            this.pnlNavContainer.AutoScroll = true;
            this.pnlNavContainer.Controls.Add(this.btnNavUsers);
            this.pnlNavContainer.Controls.Add(this.btnNavReports);
            this.pnlNavContainer.Controls.Add(this.btnNavBilling);
            this.pnlNavContainer.Controls.Add(this.btnNavPharmacy);
            this.pnlNavContainer.Controls.Add(this.btnNavConsultations);
            this.pnlNavContainer.Controls.Add(this.btnNavAppointments);
            this.pnlNavContainer.Controls.Add(this.btnNavDoctors);
            this.pnlNavContainer.Controls.Add(this.btnNavPatients);
            this.pnlNavContainer.Controls.Add(this.btnNavDashboard);
            this.pnlNavContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNavContainer.Location = new System.Drawing.Point(0, 35);
            this.pnlNavContainer.Name = "pnlNavContainer";
            this.pnlNavContainer.Size = new System.Drawing.Size(220, 561);
            this.pnlNavContainer.TabIndex = 2;
            // 
            // btnNavUsers
            // 
            this.btnNavUsers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavUsers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavUsers.FlatAppearance.BorderSize = 0;
            this.btnNavUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavUsers.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnNavUsers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnNavUsers.Location = new System.Drawing.Point(0, 360);
            this.btnNavUsers.Name = "btnNavUsers";
            this.btnNavUsers.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnNavUsers.Size = new System.Drawing.Size(220, 45);
            this.btnNavUsers.TabIndex = 8;
            this.btnNavUsers.Text = "⚙️  User Accounts";
            this.btnNavUsers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavUsers.UseVisualStyleBackColor = true;
            this.btnNavUsers.Click += new System.EventHandler(this.BtnNavUsers_Click);
            // 
            // btnNavReports
            // 
            this.btnNavReports.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavReports.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavReports.FlatAppearance.BorderSize = 0;
            this.btnNavReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavReports.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnNavReports.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnNavReports.Location = new System.Drawing.Point(0, 315);
            this.btnNavReports.Name = "btnNavReports";
            this.btnNavReports.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnNavReports.Size = new System.Drawing.Size(220, 45);
            this.btnNavReports.TabIndex = 7;
            this.btnNavReports.Text = "📈  Reports & Analytics";
            this.btnNavReports.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavReports.UseVisualStyleBackColor = true;
            this.btnNavReports.Click += new System.EventHandler(this.BtnNavReports_Click);
            // 
            // btnNavBilling
            // 
            this.btnNavBilling.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavBilling.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavBilling.FlatAppearance.BorderSize = 0;
            this.btnNavBilling.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavBilling.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnNavBilling.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnNavBilling.Location = new System.Drawing.Point(0, 270);
            this.btnNavBilling.Name = "btnNavBilling";
            this.btnNavBilling.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnNavBilling.Size = new System.Drawing.Size(220, 45);
            this.btnNavBilling.TabIndex = 6;
            this.btnNavBilling.Text = "💳  Billing & Payments";
            this.btnNavBilling.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavBilling.UseVisualStyleBackColor = true;
            this.btnNavBilling.Click += new System.EventHandler(this.BtnNavBilling_Click);
            // 
            // btnNavPharmacy
            // 
            this.btnNavPharmacy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavPharmacy.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavPharmacy.FlatAppearance.BorderSize = 0;
            this.btnNavPharmacy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavPharmacy.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnNavPharmacy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnNavPharmacy.Location = new System.Drawing.Point(0, 225);
            this.btnNavPharmacy.Name = "btnNavPharmacy";
            this.btnNavPharmacy.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnNavPharmacy.Size = new System.Drawing.Size(220, 45);
            this.btnNavPharmacy.TabIndex = 5;
            this.btnNavPharmacy.Text = "💊  Pharmacy & Stock";
            this.btnNavPharmacy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavPharmacy.UseVisualStyleBackColor = true;
            this.btnNavPharmacy.Click += new System.EventHandler(this.BtnNavPharmacy_Click);
            // 
            // btnNavConsultations
            // 
            this.btnNavConsultations.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavConsultations.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavConsultations.FlatAppearance.BorderSize = 0;
            this.btnNavConsultations.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavConsultations.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnNavConsultations.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnNavConsultations.Location = new System.Drawing.Point(0, 180);
            this.btnNavConsultations.Name = "btnNavConsultations";
            this.btnNavConsultations.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnNavConsultations.Size = new System.Drawing.Size(220, 45);
            this.btnNavConsultations.TabIndex = 4;
            this.btnNavConsultations.Text = "🩺  Consultations";
            this.btnNavConsultations.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavConsultations.UseVisualStyleBackColor = true;
            this.btnNavConsultations.Click += new System.EventHandler(this.BtnNavConsultations_Click);
            // 
            // btnNavAppointments
            // 
            this.btnNavAppointments.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavAppointments.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavAppointments.FlatAppearance.BorderSize = 0;
            this.btnNavAppointments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavAppointments.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnNavAppointments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnNavAppointments.Location = new System.Drawing.Point(0, 135);
            this.btnNavAppointments.Name = "btnNavAppointments";
            this.btnNavAppointments.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnNavAppointments.Size = new System.Drawing.Size(220, 45);
            this.btnNavAppointments.TabIndex = 3;
            this.btnNavAppointments.Text = "📅  Appointments";
            this.btnNavAppointments.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavAppointments.UseVisualStyleBackColor = true;
            this.btnNavAppointments.Click += new System.EventHandler(this.BtnNavAppointments_Click);
            // 
            // btnNavDoctors
            // 
            this.btnNavDoctors.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavDoctors.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavDoctors.FlatAppearance.BorderSize = 0;
            this.btnNavDoctors.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavDoctors.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnNavDoctors.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnNavDoctors.Location = new System.Drawing.Point(0, 90);
            this.btnNavDoctors.Name = "btnNavDoctors";
            this.btnNavDoctors.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnNavDoctors.Size = new System.Drawing.Size(220, 45);
            this.btnNavDoctors.TabIndex = 2;
            this.btnNavDoctors.Text = "👨‍⚕️  Doctors & Depts";
            this.btnNavDoctors.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavDoctors.UseVisualStyleBackColor = true;
            this.btnNavDoctors.Click += new System.EventHandler(this.BtnNavDoctors_Click);
            // 
            // btnNavPatients
            // 
            this.btnNavPatients.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavPatients.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavPatients.FlatAppearance.BorderSize = 0;
            this.btnNavPatients.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavPatients.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnNavPatients.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnNavPatients.Location = new System.Drawing.Point(0, 45);
            this.btnNavPatients.Name = "btnNavPatients";
            this.btnNavPatients.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnNavPatients.Size = new System.Drawing.Size(220, 45);
            this.btnNavPatients.TabIndex = 1;
            this.btnNavPatients.Text = "👥  Patients Directory";
            this.btnNavPatients.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavPatients.UseVisualStyleBackColor = true;
            this.btnNavPatients.Click += new System.EventHandler(this.BtnNavPatients_Click);
            // 
            // btnNavDashboard
            // 
            this.btnNavDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnNavDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavDashboard.FlatAppearance.BorderSize = 0;
            this.btnNavDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavDashboard.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnNavDashboard.ForeColor = System.Drawing.Color.White;
            this.btnNavDashboard.Location = new System.Drawing.Point(0, 0);
            this.btnNavDashboard.Name = "btnNavDashboard";
            this.btnNavDashboard.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnNavDashboard.Size = new System.Drawing.Size(220, 45);
            this.btnNavDashboard.TabIndex = 0;
            this.btnNavDashboard.Text = "📊  Dashboard";
            this.btnNavDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavDashboard.UseVisualStyleBackColor = false;
            this.btnNavDashboard.Click += new System.EventHandler(this.BtnNavDashboard_Click);
            // 
            // pnlNavHeader
            // 
            this.pnlNavHeader.Controls.Add(this.lblNavHeader);
            this.pnlNavHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNavHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlNavHeader.Name = "pnlNavHeader";
            this.pnlNavHeader.Size = new System.Drawing.Size(220, 35);
            this.pnlNavHeader.TabIndex = 1;
            // 
            // lblNavHeader
            // 
            this.lblNavHeader.AutoSize = true;
            this.lblNavHeader.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblNavHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblNavHeader.Location = new System.Drawing.Point(18, 12);
            this.lblNavHeader.Name = "lblNavHeader";
            this.lblNavHeader.Size = new System.Drawing.Size(89, 12);
            this.lblNavHeader.TabIndex = 0;
            this.lblNavHeader.Text = "MAIN NAVIGATION";
            // 
            // pnlSidebarBottom
            // 
            this.pnlSidebarBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlSidebarBottom.Controls.Add(this.lblSystemVersion);
            this.pnlSidebarBottom.Controls.Add(this.lblDbStatus);
            this.pnlSidebarBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSidebarBottom.Location = new System.Drawing.Point(0, 596);
            this.pnlSidebarBottom.Name = "pnlSidebarBottom";
            this.pnlSidebarBottom.Size = new System.Drawing.Size(220, 65);
            this.pnlSidebarBottom.TabIndex = 0;
            // 
            // lblSystemVersion
            // 
            this.lblSystemVersion.AutoSize = true;
            this.lblSystemVersion.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSystemVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSystemVersion.Location = new System.Drawing.Point(14, 38);
            this.lblSystemVersion.Name = "lblSystemVersion";
            this.lblSystemVersion.Size = new System.Drawing.Size(127, 13);
            this.lblSystemVersion.TabIndex = 1;
            this.lblSystemVersion.Text = "MediCare HMS v1.0 (.NET)";
            // 
            // lblDbStatus
            // 
            this.lblDbStatus.AutoSize = true;
            this.lblDbStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDbStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(222)))), ((int)(((byte)(128)))));
            this.lblDbStatus.Location = new System.Drawing.Point(14, 15);
            this.lblDbStatus.Name = "lblDbStatus";
            this.lblDbStatus.Size = new System.Drawing.Size(130, 15);
            this.lblDbStatus.TabIndex = 0;
            this.lblDbStatus.Text = "🟢 Database: Connected";
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(220, 60);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1044, 661);
            this.pnlContent.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1264, 721);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlSidebar);
            this.Controls.Add(this.pnlTopHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MinimumSize = new System.Drawing.Size(1100, 680);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MediCare Hospital Management System";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnlTopHeader.ResumeLayout(false);
            this.pnlTopHeader.PerformLayout();
            this.pnlUserSection.ResumeLayout(false);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlNavContainer.ResumeLayout(false);
            this.pnlNavHeader.ResumeLayout(false);
            this.pnlNavHeader.PerformLayout();
            this.pnlSidebarBottom.ResumeLayout(false);
            this.pnlSidebarBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTopHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderSubtitle;
        private System.Windows.Forms.Panel pnlUserSection;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblUserRole;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlNavHeader;
        private System.Windows.Forms.Label lblNavHeader;
        private System.Windows.Forms.Panel pnlNavContainer;
        private System.Windows.Forms.Button btnNavDashboard;
        private System.Windows.Forms.Button btnNavPatients;
        private System.Windows.Forms.Button btnNavDoctors;
        private System.Windows.Forms.Button btnNavAppointments;
        private System.Windows.Forms.Button btnNavConsultations;
        private System.Windows.Forms.Button btnNavPharmacy;
        private System.Windows.Forms.Button btnNavBilling;
        private System.Windows.Forms.Button btnNavReports;
        private System.Windows.Forms.Button btnNavUsers;
        private System.Windows.Forms.Panel pnlSidebarBottom;
        private System.Windows.Forms.Label lblDbStatus;
        private System.Windows.Forms.Label lblSystemVersion;
        private System.Windows.Forms.Panel pnlContent;
    }
}
