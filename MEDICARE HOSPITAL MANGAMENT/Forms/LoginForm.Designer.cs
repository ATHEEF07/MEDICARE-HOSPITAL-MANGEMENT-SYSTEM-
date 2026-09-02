using System.Drawing;
using System.Windows.Forms;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    partial class LoginForm
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
            lblSubtitle = new Label();
            lblTitle = new Label();
            lblHeaderIcon = new Label();
            pnlBody = new Panel();
            lblError = new Label();
            btnExit = new Button();
            btnLogin = new Button();
            chkShowPassword = new CheckBox();
            txtPassword = new TextBox();
            lblPassword = new Label();
            txtUsername = new TextBox();
            lblUsername = new Label();
            pnlHeader.SuspendLayout();
            pnlBody.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(27, 54, 93);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblHeaderIcon);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(460, 110);
            pnlHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblSubtitle.ForeColor = Color.FromArgb(200, 220, 240);
            lblSubtitle.Location = new Point(78, 62);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(244, 21);
            lblSubtitle.TabIndex = 2;
            lblSubtitle.Text = "Hospital Management System v1.0";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(76, 22);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(137, 37);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "MediCare";
            // 
            // lblHeaderIcon
            // 
            lblHeaderIcon.AutoSize = true;
            lblHeaderIcon.Font = new Font("Segoe UI", 26F);
            lblHeaderIcon.ForeColor = Color.White;
            lblHeaderIcon.Location = new Point(20, 22);
            lblHeaderIcon.Name = "lblHeaderIcon";
            lblHeaderIcon.Size = new Size(65, 60);
            lblHeaderIcon.TabIndex = 0;
            lblHeaderIcon.Text = "🏥";
            // 
            // pnlBody
            // 
            pnlBody.BackColor = Color.White;
            pnlBody.Controls.Add(lblError);
            pnlBody.Controls.Add(btnExit);
            pnlBody.Controls.Add(btnLogin);
            pnlBody.Controls.Add(chkShowPassword);
            pnlBody.Controls.Add(txtPassword);
            pnlBody.Controls.Add(lblPassword);
            pnlBody.Controls.Add(txtUsername);
            pnlBody.Controls.Add(lblUsername);
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Location = new Point(0, 110);
            pnlBody.Name = "pnlBody";
            pnlBody.Padding = new Padding(30);
            pnlBody.Size = new Size(460, 370);
            pnlBody.TabIndex = 1;
            // 
            // lblError
            // 
            lblError.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblError.ForeColor = Color.FromArgb(200, 35, 51);
            lblError.Location = new Point(34, 210);
            lblError.Name = "lblError";
            lblError.Size = new Size(392, 45);
            lblError.TabIndex = 7;
            lblError.Text = "Error message goes here";
            lblError.Visible = false;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.FromArgb(240, 240, 240);
            btnExit.Cursor = Cursors.Hand;
            btnExit.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Segoe UI", 10F);
            btnExit.ForeColor = Color.FromArgb(50, 50, 50);
            btnExit.Location = new Point(34, 305);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(392, 40);
            btnExit.TabIndex = 6;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(27, 54, 93);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(34, 258);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(392, 42);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Sign In";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // chkShowPassword
            // 
            chkShowPassword.AutoSize = true;
            chkShowPassword.Cursor = Cursors.Hand;
            chkShowPassword.Font = new Font("Segoe UI", 8.5F);
            chkShowPassword.ForeColor = Color.FromArgb(90, 90, 90);
            chkShowPassword.Location = new Point(34, 180);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(133, 23);
            chkShowPassword.TabIndex = 4;
            chkShowPassword.Text = "Show password";
            chkShowPassword.UseVisualStyleBackColor = true;
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 11F);
            txtPassword.Location = new Point(34, 142);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.Size = new Size(392, 32);
            txtPassword.TabIndex = 3;
            txtPassword.KeyDown += txtPassword_KeyDown;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(40, 40, 40);
            lblPassword.Location = new Point(30, 115);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(82, 21);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "Password";
            // 
            // txtUsername
            // 
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 11F);
            txtUsername.Location = new Point(34, 60);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(392, 32);
            txtUsername.TabIndex = 1;
            txtUsername.KeyDown += txtUsername_KeyDown;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(40, 40, 40);
            lblUsername.Location = new Point(30, 33);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(87, 21);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Username";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(460, 480);
            Controls.Add(pnlBody);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MediCare Hospital - Authentication";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlBody.ResumeLayout(false);
            pnlBody.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblHeaderIcon;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel pnlBody;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private CheckBox chkShowPassword;
        private Label lblError;
        private Button btnLogin;
        private Button btnExit;
    }
}
