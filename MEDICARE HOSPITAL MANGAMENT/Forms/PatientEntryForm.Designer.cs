using System.Drawing;
using System.Windows.Forms;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    partial class PatientEntryForm
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
            lblTitle = new Label();
            lblPatientCode = new Label();
            txtPatientCode = new TextBox();
            lblNIC = new Label();
            txtNIC = new TextBox();
            lblFirstName = new Label();
            txtFirstName = new TextBox();
            lblLastName = new Label();
            txtLastName = new TextBox();
            lblDateOfBirth = new Label();
            dtpDateOfBirth = new DateTimePicker();
            lblAgeDisplay = new Label();
            lblGender = new Label();
            cmbGender = new ComboBox();
            lblBloodGroup = new Label();
            cmbBloodGroup = new ComboBox();
            lblPhone = new Label();
            txtPhone = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            lblEmergencyName = new Label();
            txtEmergencyName = new TextBox();
            lblEmergencyPhone = new Label();
            txtEmergencyPhone = new TextBox();
            chkIsActive = new CheckBox();
            btnSave = new Button();
            btnCancel = new Button();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(27, 54, 93);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(20, 15, 20, 15);
            pnlHeader.Size = new Size(680, 65);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(276, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Register New Patient";
            // 
            // lblPatientCode
            // 
            lblPatientCode.AutoSize = true;
            lblPatientCode.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPatientCode.ForeColor = Color.FromArgb(50, 50, 50);
            lblPatientCode.Location = new Point(30, 85);
            lblPatientCode.Name = "lblPatientCode";
            lblPatientCode.Size = new Size(99, 20);
            lblPatientCode.TabIndex = 1;
            lblPatientCode.Text = "Patient Code";
            // 
            // txtPatientCode
            // 
            txtPatientCode.BackColor = Color.FromArgb(240, 240, 240);
            txtPatientCode.BorderStyle = BorderStyle.FixedSingle;
            txtPatientCode.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            txtPatientCode.ForeColor = Color.FromArgb(27, 54, 93);
            txtPatientCode.Location = new Point(30, 110);
            txtPatientCode.Name = "txtPatientCode";
            txtPatientCode.ReadOnly = true;
            txtPatientCode.Size = new Size(290, 29);
            txtPatientCode.TabIndex = 2;
            // 
            // lblNIC
            // 
            lblNIC.AutoSize = true;
            lblNIC.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNIC.ForeColor = Color.FromArgb(50, 50, 50);
            lblNIC.Location = new Point(350, 85);
            lblNIC.Name = "lblNIC";
            lblNIC.Size = new Size(130, 20);
            lblNIC.TabIndex = 3;
            lblNIC.Text = "National ID (NIC)";
            // 
            // txtNIC
            // 
            txtNIC.BorderStyle = BorderStyle.FixedSingle;
            txtNIC.Font = new Font("Segoe UI", 9.5F);
            txtNIC.Location = new Point(350, 110);
            txtNIC.Name = "txtNIC";
            txtNIC.PlaceholderText = "e.g., 199012345678 or 901234567V";
            txtNIC.Size = new Size(290, 29);
            txtNIC.TabIndex = 4;
            // 
            // lblFirstName
            // 
            lblFirstName.AutoSize = true;
            lblFirstName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFirstName.ForeColor = Color.FromArgb(50, 50, 50);
            lblFirstName.Location = new Point(30, 155);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(96, 20);
            lblFirstName.TabIndex = 5;
            lblFirstName.Text = "First Name *";
            // 
            // txtFirstName
            // 
            txtFirstName.BorderStyle = BorderStyle.FixedSingle;
            txtFirstName.Font = new Font("Segoe UI", 9.5F);
            txtFirstName.Location = new Point(30, 180);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(290, 29);
            txtFirstName.TabIndex = 6;
            // 
            // lblLastName
            // 
            lblLastName.AutoSize = true;
            lblLastName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLastName.ForeColor = Color.FromArgb(50, 50, 50);
            lblLastName.Location = new Point(350, 155);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(94, 20);
            lblLastName.TabIndex = 7;
            lblLastName.Text = "Last Name *";
            // 
            // txtLastName
            // 
            txtLastName.BorderStyle = BorderStyle.FixedSingle;
            txtLastName.Font = new Font("Segoe UI", 9.5F);
            txtLastName.Location = new Point(350, 180);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(290, 29);
            txtLastName.TabIndex = 8;
            // 
            // lblDateOfBirth
            // 
            lblDateOfBirth.AutoSize = true;
            lblDateOfBirth.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDateOfBirth.ForeColor = Color.FromArgb(50, 50, 50);
            lblDateOfBirth.Location = new Point(30, 225);
            lblDateOfBirth.Name = "lblDateOfBirth";
            lblDateOfBirth.Size = new Size(110, 20);
            lblDateOfBirth.TabIndex = 9;
            lblDateOfBirth.Text = "Date of Birth *";
            // 
            // dtpDateOfBirth
            // 
            dtpDateOfBirth.CustomFormat = "yyyy-MM-dd";
            dtpDateOfBirth.Font = new Font("Segoe UI", 9.5F);
            dtpDateOfBirth.Format = DateTimePickerFormat.Custom;
            dtpDateOfBirth.Location = new Point(30, 250);
            dtpDateOfBirth.MaxDate = new DateTime(2030, 12, 31);
            dtpDateOfBirth.MinDate = new DateTime(1900, 1, 1);
            dtpDateOfBirth.Name = "dtpDateOfBirth";
            dtpDateOfBirth.Size = new Size(180, 29);
            dtpDateOfBirth.TabIndex = 10;
            dtpDateOfBirth.ValueChanged += dtpDateOfBirth_ValueChanged;
            // 
            // lblAgeDisplay
            // 
            lblAgeDisplay.AutoSize = true;
            lblAgeDisplay.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblAgeDisplay.ForeColor = Color.FromArgb(80, 80, 80);
            lblAgeDisplay.Location = new Point(220, 255);
            lblAgeDisplay.Name = "lblAgeDisplay";
            lblAgeDisplay.Size = new Size(88, 20);
            lblAgeDisplay.TabIndex = 11;
            lblAgeDisplay.Text = "Age: 30 years";
            // 
            // lblGender
            // 
            lblGender.AutoSize = true;
            lblGender.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblGender.ForeColor = Color.FromArgb(50, 50, 50);
            lblGender.Location = new Point(350, 225);
            lblGender.Name = "lblGender";
            lblGender.Size = new Size(71, 20);
            lblGender.TabIndex = 12;
            lblGender.Text = "Gender *";
            // 
            // cmbGender
            // 
            cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGender.Font = new Font("Segoe UI", 9.5F);
            cmbGender.FormattingEnabled = true;
            cmbGender.Location = new Point(350, 250);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(135, 29);
            cmbGender.TabIndex = 13;
            // 
            // lblBloodGroup
            // 
            lblBloodGroup.AutoSize = true;
            lblBloodGroup.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBloodGroup.ForeColor = Color.FromArgb(50, 50, 50);
            lblBloodGroup.Location = new Point(500, 225);
            lblBloodGroup.Name = "lblBloodGroup";
            lblBloodGroup.Size = new Size(98, 20);
            lblBloodGroup.TabIndex = 14;
            lblBloodGroup.Text = "Blood Group";
            // 
            // cmbBloodGroup
            // 
            cmbBloodGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBloodGroup.Font = new Font("Segoe UI", 9.5F);
            cmbBloodGroup.FormattingEnabled = true;
            cmbBloodGroup.Location = new Point(500, 250);
            cmbBloodGroup.Name = "cmbBloodGroup";
            cmbBloodGroup.Size = new Size(140, 29);
            cmbBloodGroup.TabIndex = 15;
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPhone.ForeColor = Color.FromArgb(50, 50, 50);
            lblPhone.Location = new Point(30, 295);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(126, 20);
            lblPhone.TabIndex = 16;
            lblPhone.Text = "Phone Number *";
            // 
            // txtPhone
            // 
            txtPhone.BorderStyle = BorderStyle.FixedSingle;
            txtPhone.Font = new Font("Segoe UI", 9.5F);
            txtPhone.Location = new Point(30, 320);
            txtPhone.Name = "txtPhone";
            txtPhone.PlaceholderText = "e.g. 0771234567";
            txtPhone.Size = new Size(290, 29);
            txtPhone.TabIndex = 17;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEmail.ForeColor = Color.FromArgb(50, 50, 50);
            lblEmail.Location = new Point(350, 295);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(107, 20);
            lblEmail.TabIndex = 18;
            lblEmail.Text = "Email Address";
            // 
            // txtEmail
            // 
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Font = new Font("Segoe UI", 9.5F);
            txtEmail.Location = new Point(350, 320);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "patient@domain.com";
            txtEmail.Size = new Size(290, 29);
            txtEmail.TabIndex = 19;
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAddress.ForeColor = Color.FromArgb(50, 50, 50);
            lblAddress.Location = new Point(30, 365);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(110, 20);
            lblAddress.TabIndex = 20;
            lblAddress.Text = "Home Address";
            // 
            // txtAddress
            // 
            txtAddress.BorderStyle = BorderStyle.FixedSingle;
            txtAddress.Font = new Font("Segoe UI", 9.5F);
            txtAddress.Location = new Point(30, 390);
            txtAddress.Multiline = true;
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(610, 50);
            txtAddress.TabIndex = 21;
            // 
            // lblEmergencyName
            // 
            lblEmergencyName.AutoSize = true;
            lblEmergencyName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEmergencyName.ForeColor = Color.FromArgb(50, 50, 50);
            lblEmergencyName.Location = new Point(30, 455);
            lblEmergencyName.Name = "lblEmergencyName";
            lblEmergencyName.Size = new Size(182, 20);
            lblEmergencyName.TabIndex = 22;
            lblEmergencyName.Text = "Emergency Contact Name";
            // 
            // txtEmergencyName
            // 
            txtEmergencyName.BorderStyle = BorderStyle.FixedSingle;
            txtEmergencyName.Font = new Font("Segoe UI", 9.5F);
            txtEmergencyName.Location = new Point(30, 480);
            txtEmergencyName.Name = "txtEmergencyName";
            txtEmergencyName.Size = new Size(290, 29);
            txtEmergencyName.TabIndex = 23;
            // 
            // lblEmergencyPhone
            // 
            lblEmergencyPhone.AutoSize = true;
            lblEmergencyPhone.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEmergencyPhone.ForeColor = Color.FromArgb(50, 50, 50);
            lblEmergencyPhone.Location = new Point(350, 455);
            lblEmergencyPhone.Name = "lblEmergencyPhone";
            lblEmergencyPhone.Size = new Size(184, 20);
            lblEmergencyPhone.TabIndex = 24;
            lblEmergencyPhone.Text = "Emergency Contact Phone";
            // 
            // txtEmergencyPhone
            // 
            txtEmergencyPhone.BorderStyle = BorderStyle.FixedSingle;
            txtEmergencyPhone.Font = new Font("Segoe UI", 9.5F);
            txtEmergencyPhone.Location = new Point(350, 480);
            txtEmergencyPhone.Name = "txtEmergencyPhone";
            txtEmergencyPhone.Size = new Size(290, 29);
            txtEmergencyPhone.TabIndex = 25;
            // 
            // chkIsActive
            // 
            chkIsActive.AutoSize = true;
            chkIsActive.Checked = true;
            chkIsActive.CheckState = CheckState.Checked;
            chkIsActive.Font = new Font("Segoe UI", 9.5F);
            chkIsActive.ForeColor = Color.FromArgb(40, 40, 40);
            chkIsActive.Location = new Point(30, 525);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(126, 25);
            chkIsActive.TabIndex = 26;
            chkIsActive.Text = "Patient Active";
            chkIsActive.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(27, 54, 93);
            btnSave.Cursor = Cursors.Hand;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(350, 530);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(155, 42);
            btnSave.TabIndex = 27;
            btnSave.Text = "Save Patient";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(230, 230, 230);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F);
            btnCancel.ForeColor = Color.FromArgb(40, 40, 40);
            btnCancel.Location = new Point(515, 530);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(125, 42);
            btnCancel.TabIndex = 28;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // PatientEntryForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(680, 600);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(chkIsActive);
            Controls.Add(txtEmergencyPhone);
            Controls.Add(lblEmergencyPhone);
            Controls.Add(txtEmergencyName);
            Controls.Add(lblEmergencyName);
            Controls.Add(txtAddress);
            Controls.Add(lblAddress);
            Controls.Add(txtEmail);
            Controls.Add(lblEmail);
            Controls.Add(txtPhone);
            Controls.Add(lblPhone);
            Controls.Add(cmbBloodGroup);
            Controls.Add(lblBloodGroup);
            Controls.Add(cmbGender);
            Controls.Add(lblGender);
            Controls.Add(lblAgeDisplay);
            Controls.Add(dtpDateOfBirth);
            Controls.Add(lblDateOfBirth);
            Controls.Add(txtLastName);
            Controls.Add(lblLastName);
            Controls.Add(txtFirstName);
            Controls.Add(lblFirstName);
            Controls.Add(txtNIC);
            Controls.Add(lblNIC);
            Controls.Add(txtPatientCode);
            Controls.Add(lblPatientCode);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PatientEntryForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Patient Entry";
            Load += PatientEntryForm_Load;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblPatientCode;
        private TextBox txtPatientCode;
        private Label lblNIC;
        private TextBox txtNIC;
        private Label lblFirstName;
        private TextBox txtFirstName;
        private Label lblLastName;
        private TextBox txtLastName;
        private Label lblDateOfBirth;
        private DateTimePicker dtpDateOfBirth;
        private Label lblAgeDisplay;
        private Label lblGender;
        private ComboBox cmbGender;
        private Label lblBloodGroup;
        private ComboBox cmbBloodGroup;
        private Label lblPhone;
        private TextBox txtPhone;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblAddress;
        private TextBox txtAddress;
        private Label lblEmergencyName;
        private TextBox txtEmergencyName;
        private Label lblEmergencyPhone;
        private TextBox txtEmergencyPhone;
        private CheckBox chkIsActive;
        private Button btnSave;
        private Button btnCancel;
    }
}
