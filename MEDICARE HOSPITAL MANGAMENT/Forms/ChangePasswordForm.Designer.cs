using System.Drawing;
using System.Windows.Forms;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    partial class ChangePasswordForm
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
            lblTitle = new Label();
            lblTargetUser = new Label();
            lblCurrentPassword = new Label();
            txtCurrentPassword = new TextBox();
            lblNewPassword = new Label();
            txtNewPassword = new TextBox();
            lblConfirmPassword = new Label();
            txtConfirmPassword = new TextBox();
            chkShowPassword = new CheckBox();
            btnSubmit = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(27, 54, 93);
            lblTitle.Location = new Point(25, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(257, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Change Your Password";
            // 
            // lblTargetUser
            // 
            lblTargetUser.AutoSize = true;
            lblTargetUser.Font = new Font("Segoe UI", 9.5F);
            lblTargetUser.ForeColor = Color.FromArgb(100, 100, 100);
            lblTargetUser.Location = new Point(27, 55);
            lblTargetUser.Name = "lblTargetUser";
            lblTargetUser.Size = new Size(110, 21);
            lblTargetUser.TabIndex = 1;
            lblTargetUser.Text = "User: username";
            // 
            // lblCurrentPassword
            // 
            lblCurrentPassword.AutoSize = true;
            lblCurrentPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCurrentPassword.ForeColor = Color.FromArgb(50, 50, 50);
            lblCurrentPassword.Location = new Point(25, 95);
            lblCurrentPassword.Name = "lblCurrentPassword";
            lblCurrentPassword.Size = new Size(137, 20);
            lblCurrentPassword.TabIndex = 2;
            lblCurrentPassword.Text = "Current Password:";
            // 
            // txtCurrentPassword
            // 
            txtCurrentPassword.BorderStyle = BorderStyle.FixedSingle;
            txtCurrentPassword.Font = new Font("Segoe UI", 9.5F);
            txtCurrentPassword.Location = new Point(28, 120);
            txtCurrentPassword.Name = "txtCurrentPassword";
            txtCurrentPassword.PasswordChar = '●';
            txtCurrentPassword.Size = new Size(330, 29);
            txtCurrentPassword.TabIndex = 3;
            // 
            // lblNewPassword
            // 
            lblNewPassword.AutoSize = true;
            lblNewPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNewPassword.ForeColor = Color.FromArgb(50, 50, 50);
            lblNewPassword.Location = new Point(25, 160);
            lblNewPassword.Name = "lblNewPassword";
            lblNewPassword.Size = new Size(116, 20);
            lblNewPassword.TabIndex = 4;
            lblNewPassword.Text = "New Password:";
            // 
            // txtNewPassword
            // 
            txtNewPassword.BorderStyle = BorderStyle.FixedSingle;
            txtNewPassword.Font = new Font("Segoe UI", 9.5F);
            txtNewPassword.Location = new Point(28, 185);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.PasswordChar = '●';
            txtNewPassword.Size = new Size(330, 29);
            txtNewPassword.TabIndex = 5;
            // 
            // lblConfirmPassword
            // 
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblConfirmPassword.ForeColor = Color.FromArgb(50, 50, 50);
            lblConfirmPassword.Location = new Point(25, 225);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(141, 20);
            lblConfirmPassword.TabIndex = 6;
            lblConfirmPassword.Text = "Confirm Password:";
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
            txtConfirmPassword.Font = new Font("Segoe UI", 9.5F);
            txtConfirmPassword.Location = new Point(28, 250);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.PasswordChar = '●';
            txtConfirmPassword.Size = new Size(330, 29);
            txtConfirmPassword.TabIndex = 7;
            // 
            // chkShowPassword
            // 
            chkShowPassword.AutoSize = true;
            chkShowPassword.Cursor = Cursors.Hand;
            chkShowPassword.Font = new Font("Segoe UI", 8.5F);
            chkShowPassword.ForeColor = Color.FromArgb(90, 90, 90);
            chkShowPassword.Location = new Point(28, 290);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(133, 23);
            chkShowPassword.TabIndex = 8;
            chkShowPassword.Text = "Show passwords";
            chkShowPassword.UseVisualStyleBackColor = true;
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
            // 
            // btnSubmit
            // 
            btnSubmit.BackColor = Color.FromArgb(27, 54, 93);
            btnSubmit.Cursor = Cursors.Hand;
            btnSubmit.FlatAppearance.BorderSize = 0;
            btnSubmit.FlatStyle = FlatStyle.Flat;
            btnSubmit.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnSubmit.ForeColor = Color.White;
            btnSubmit.Location = new Point(28, 330);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(155, 36);
            btnSubmit.TabIndex = 9;
            btnSubmit.Text = "Save Password";
            btnSubmit.UseVisualStyleBackColor = false;
            btnSubmit.Click += btnSubmit_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(230, 230, 230);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 9.5F);
            btnCancel.ForeColor = Color.FromArgb(40, 40, 40);
            btnCancel.Location = new Point(203, 330);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(155, 36);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // ChangePasswordForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(390, 395);
            Controls.Add(btnCancel);
            Controls.Add(btnSubmit);
            Controls.Add(chkShowPassword);
            Controls.Add(txtConfirmPassword);
            Controls.Add(lblConfirmPassword);
            Controls.Add(txtNewPassword);
            Controls.Add(lblNewPassword);
            Controls.Add(txtCurrentPassword);
            Controls.Add(lblCurrentPassword);
            Controls.Add(lblTargetUser);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ChangePasswordForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Password Management";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblTargetUser;
        private Label lblCurrentPassword;
        private TextBox txtCurrentPassword;
        private Label lblNewPassword;
        private TextBox txtNewPassword;
        private Label lblConfirmPassword;
        private TextBox txtConfirmPassword;
        private CheckBox chkShowPassword;
        private Button btnSubmit;
        private Button btnCancel;
    }
}
