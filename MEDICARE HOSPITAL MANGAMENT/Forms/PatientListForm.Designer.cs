using System.Drawing;
using System.Windows.Forms;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    partial class PatientListForm
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
            pnlToolbar = new Panel();
            lblRecordCount = new Label();
            btnRefresh = new Button();
            btnDeactivate = new Button();
            btnEditPatient = new Button();
            btnRegisterNew = new Button();
            chkActiveOnly = new CheckBox();
            txtSearch = new TextBox();
            lblSearch = new Label();
            pnlGrid = new Panel();
            dgvPatients = new DataGridView();
            pnlHeader.SuspendLayout();
            pnlToolbar.SuspendLayout();
            pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPatients).BeginInit();
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
            pnlHeader.Size = new Size(1150, 80);
            pnlHeader.TabIndex = 0;
            // 
            // lblHeaderSubtitle
            // 
            lblHeaderSubtitle.AutoSize = true;
            lblHeaderSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblHeaderSubtitle.ForeColor = Color.FromArgb(200, 220, 240);
            lblHeaderSubtitle.Location = new Point(22, 45);
            lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            lblHeaderSubtitle.Size = new Size(392, 21);
            lblHeaderSubtitle.TabIndex = 1;
            lblHeaderSubtitle.Text = "Register, search, view history, and update patient files";
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
            lblHeaderTitle.Text = "Patient Registry and Lookup";
            // 
            // pnlToolbar
            // 
            pnlToolbar.BackColor = Color.FromArgb(248, 249, 250);
            pnlToolbar.Controls.Add(lblRecordCount);
            pnlToolbar.Controls.Add(btnRefresh);
            pnlToolbar.Controls.Add(btnDeactivate);
            pnlToolbar.Controls.Add(btnEditPatient);
            pnlToolbar.Controls.Add(btnRegisterNew);
            pnlToolbar.Controls.Add(chkActiveOnly);
            pnlToolbar.Controls.Add(txtSearch);
            pnlToolbar.Controls.Add(lblSearch);
            pnlToolbar.Dock = DockStyle.Top;
            pnlToolbar.Location = new Point(0, 80);
            pnlToolbar.Name = "pnlToolbar";
            pnlToolbar.Padding = new Padding(20, 12, 20, 12);
            pnlToolbar.Size = new Size(1150, 60);
            pnlToolbar.TabIndex = 1;
            // 
            // lblRecordCount
            // 
            lblRecordCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblRecordCount.AutoSize = true;
            lblRecordCount.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblRecordCount.ForeColor = Color.FromArgb(100, 100, 100);
            lblRecordCount.Location = new Point(1020, 20);
            lblRecordCount.Name = "lblRecordCount";
            lblRecordCount.Size = new Size(106, 20);
            lblRecordCount.TabIndex = 7;
            lblRecordCount.Text = "Total Patients: 0";
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(230, 230, 230);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9F);
            btnRefresh.ForeColor = Color.FromArgb(40, 40, 40);
            btnRefresh.Location = new Point(915, 12);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(85, 36);
            btnRefresh.TabIndex = 6;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnDeactivate
            // 
            btnDeactivate.BackColor = Color.FromArgb(220, 53, 69);
            btnDeactivate.Cursor = Cursors.Hand;
            btnDeactivate.Enabled = false;
            btnDeactivate.FlatAppearance.BorderSize = 0;
            btnDeactivate.FlatStyle = FlatStyle.Flat;
            btnDeactivate.Font = new Font("Segoe UI", 9F);
            btnDeactivate.ForeColor = Color.White;
            btnDeactivate.Location = new Point(810, 12);
            btnDeactivate.Name = "btnDeactivate";
            btnDeactivate.Size = new Size(95, 36);
            btnDeactivate.TabIndex = 5;
            btnDeactivate.Text = "Deactivate";
            btnDeactivate.UseVisualStyleBackColor = false;
            btnDeactivate.Click += btnDeactivate_Click;
            // 
            // btnEditPatient
            // 
            btnEditPatient.BackColor = Color.FromArgb(40, 167, 69);
            btnEditPatient.Cursor = Cursors.Hand;
            btnEditPatient.Enabled = false;
            btnEditPatient.FlatAppearance.BorderSize = 0;
            btnEditPatient.FlatStyle = FlatStyle.Flat;
            btnEditPatient.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEditPatient.ForeColor = Color.White;
            btnEditPatient.Location = new Point(690, 12);
            btnEditPatient.Name = "btnEditPatient";
            btnEditPatient.Size = new Size(110, 36);
            btnEditPatient.TabIndex = 4;
            btnEditPatient.Text = "Edit Patient";
            btnEditPatient.UseVisualStyleBackColor = false;
            btnEditPatient.Click += btnEditPatient_Click;
            // 
            // btnRegisterNew
            // 
            btnRegisterNew.BackColor = Color.FromArgb(27, 54, 93);
            btnRegisterNew.Cursor = Cursors.Hand;
            btnRegisterNew.FlatAppearance.BorderSize = 0;
            btnRegisterNew.FlatStyle = FlatStyle.Flat;
            btnRegisterNew.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRegisterNew.ForeColor = Color.White;
            btnRegisterNew.Location = new Point(540, 12);
            btnRegisterNew.Name = "btnRegisterNew";
            btnRegisterNew.Size = new Size(140, 36);
            btnRegisterNew.TabIndex = 3;
            btnRegisterNew.Text = "+ New Patient";
            btnRegisterNew.UseVisualStyleBackColor = false;
            btnRegisterNew.Click += btnRegisterNew_Click;
            // 
            // chkActiveOnly
            // 
            chkActiveOnly.AutoSize = true;
            chkActiveOnly.Checked = true;
            chkActiveOnly.CheckState = CheckState.Checked;
            chkActiveOnly.Font = new Font("Segoe UI", 9F);
            chkActiveOnly.ForeColor = Color.FromArgb(50, 50, 50);
            chkActiveOnly.Location = new Point(415, 18);
            chkActiveOnly.Name = "chkActiveOnly";
            chkActiveOnly.Size = new Size(106, 24);
            chkActiveOnly.TabIndex = 2;
            chkActiveOnly.Text = "Active Only";
            chkActiveOnly.UseVisualStyleBackColor = true;
            chkActiveOnly.CheckedChanged += chkActiveOnly_CheckedChanged;
            // 
            // txtSearch
            // 
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Segoe UI", 10F);
            txtSearch.Location = new Point(85, 15);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search by name, code, NIC, phone...";
            txtSearch.Size = new Size(310, 30);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSearch.ForeColor = Color.FromArgb(50, 50, 50);
            lblSearch.Location = new Point(20, 18);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(66, 21);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Search:";
            // 
            // pnlGrid
            // 
            pnlGrid.Controls.Add(dgvPatients);
            pnlGrid.Dock = DockStyle.Fill;
            pnlGrid.Location = new Point(0, 140);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Padding = new Padding(20);
            pnlGrid.Size = new Size(1150, 510);
            pnlGrid.TabIndex = 2;
            // 
            // dgvPatients
            // 
            dgvPatients.AllowUserToAddRows = false;
            dgvPatients.AllowUserToDeleteRows = false;
            dgvPatients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPatients.BackgroundColor = Color.White;
            dgvPatients.BorderStyle = BorderStyle.Fixed3D;
            dgvPatients.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPatients.Dock = DockStyle.Fill;
            dgvPatients.Location = new Point(20, 20);
            dgvPatients.MultiSelect = false;
            dgvPatients.Name = "dgvPatients";
            dgvPatients.ReadOnly = true;
            dgvPatients.RowHeadersWidth = 30;
            dgvPatients.RowTemplate.Height = 28;
            dgvPatients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPatients.Size = new Size(1110, 470);
            dgvPatients.TabIndex = 0;
            dgvPatients.CellDoubleClick += dgvPatients_CellDoubleClick;
            dgvPatients.SelectionChanged += dgvPatients_SelectionChanged;
            // 
            // PatientListForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1150, 650);
            Controls.Add(pnlGrid);
            Controls.Add(pnlToolbar);
            Controls.Add(pnlHeader);
            MinimumSize = new Size(1000, 500);
            Name = "PatientListForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MediCare - Patient Registry";
            Load += PatientListForm_Load;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlToolbar.ResumeLayout(false);
            pnlToolbar.PerformLayout();
            pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPatients).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblHeaderTitle;
        private Label lblHeaderSubtitle;
        private Panel pnlToolbar;
        private Label lblSearch;
        private TextBox txtSearch;
        private CheckBox chkActiveOnly;
        private Button btnRegisterNew;
        private Button btnEditPatient;
        private Button btnDeactivate;
        private Button btnRefresh;
        private Label lblRecordCount;
        private Panel pnlGrid;
        private DataGridView dgvPatients;
    }
}
