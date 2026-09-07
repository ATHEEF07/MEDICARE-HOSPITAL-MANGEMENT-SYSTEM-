using System.Drawing;
using System.Windows.Forms;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    partial class PrescriptionForm
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
            DataGridViewCellStyle dgvHeaderStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle dgvDefaultStyle = new DataGridViewCellStyle();

            pnlHeader = new Panel();
            lblHeaderSubtitle = new Label();
            lblHeaderTitle = new Label();

            pnlContext = new Panel();
            lblPatient = new Label();
            cmbPatient = new ComboBox();
            lblDoctor = new Label();
            cmbDoctor = new ComboBox();
            lblDate = new Label();
            dtpDate = new DateTimePicker();

            grpAddItem = new GroupBox();
            lblMedicine = new Label();
            cmbMedicine = new ComboBox();
            lblDosage = new Label();
            txtDosage = new TextBox();
            lblFrequency = new Label();
            cmbFrequency = new ComboBox();
            lblDuration = new Label();
            numDuration = new NumericUpDown();
            lblQuantity = new Label();
            numQuantity = new NumericUpDown();
            lblInstructions = new Label();
            txtInstructions = new TextBox();
            btnAddItem = new Button();

            pnlCenter = new Panel();
            dgvItems = new DataGridView();

            pnlFooter = new Panel();
            lblNotes = new Label();
            txtNotes = new TextBox();
            lblSummary = new Label();
            btnSave = new Button();
            btnClear = new Button();
            btnClose = new Button();

            pnlHeader.SuspendLayout();
            pnlContext.SuspendLayout();
            grpAddItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDuration).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
            pnlCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            pnlFooter.SuspendLayout();
            SuspendLayout();

            // ── pnlHeader ──────────────────────────────────────────────────────────
            pnlHeader.BackColor = Color.FromArgb(20, 80, 140);
            pnlHeader.Controls.Add(lblHeaderSubtitle);
            pnlHeader.Controls.Add(lblHeaderTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(20, 12, 20, 12);
            pnlHeader.Size = new Size(1100, 75);
            pnlHeader.TabIndex = 0;

            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(20, 12);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(460, 35);
            lblHeaderTitle.Text = "📝  Medical Prescription Authoring";

            lblHeaderSubtitle.AutoSize = true;
            lblHeaderSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblHeaderSubtitle.ForeColor = Color.FromArgb(200, 225, 255);
            lblHeaderSubtitle.Location = new Point(22, 45);
            lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            lblHeaderSubtitle.Size = new Size(520, 21);
            lblHeaderSubtitle.Text = "Doctor's pharmaceutical order and multi-item prescription drafting";

            // ── pnlContext (Patient & Doctor bar) ──────────────────────────────────
            pnlContext.BackColor = Color.FromArgb(235, 245, 255);
            pnlContext.BorderStyle = BorderStyle.FixedSingle;
            pnlContext.Controls.Add(lblPatient);
            pnlContext.Controls.Add(cmbPatient);
            pnlContext.Controls.Add(lblDoctor);
            pnlContext.Controls.Add(cmbDoctor);
            pnlContext.Controls.Add(lblDate);
            pnlContext.Controls.Add(dtpDate);
            pnlContext.Dock = DockStyle.Top;
            pnlContext.Location = new Point(0, 75);
            pnlContext.Name = "pnlContext";
            pnlContext.Padding = new Padding(15, 10, 15, 10);
            pnlContext.Size = new Size(1100, 52);
            pnlContext.TabIndex = 1;

            lblPatient.AutoSize = true;
            lblPatient.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPatient.ForeColor = Color.FromArgb(13, 71, 128);
            lblPatient.Location = new Point(15, 15);
            lblPatient.Name = "lblPatient";
            lblPatient.Size = new Size(67, 20);
            lblPatient.Text = "Patient *:";

            cmbPatient.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPatient.Font = new Font("Segoe UI", 9.5F);
            cmbPatient.FormattingEnabled = true;
            cmbPatient.Location = new Point(88, 11);
            cmbPatient.Name = "cmbPatient";
            cmbPatient.Size = new Size(300, 29);
            cmbPatient.TabIndex = 0;

            lblDoctor.AutoSize = true;
            lblDoctor.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDoctor.ForeColor = Color.FromArgb(13, 71, 128);
            lblDoctor.Location = new Point(410, 15);
            lblDoctor.Name = "lblDoctor";
            lblDoctor.Size = new Size(67, 20);
            lblDoctor.Text = "Doctor *:";

            cmbDoctor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDoctor.Font = new Font("Segoe UI", 9.5F);
            cmbDoctor.FormattingEnabled = true;
            cmbDoctor.Location = new Point(483, 11);
            cmbDoctor.Name = "cmbDoctor";
            cmbDoctor.Size = new Size(280, 29);
            cmbDoctor.TabIndex = 1;

            lblDate.AutoSize = true;
            lblDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDate.ForeColor = Color.FromArgb(13, 71, 128);
            lblDate.Location = new Point(785, 15);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(46, 20);
            lblDate.Text = "Date:";

            dtpDate.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpDate.Font = new Font("Segoe UI", 9.5F);
            dtpDate.Format = DateTimePickerFormat.Custom;
            dtpDate.Location = new Point(837, 11);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(180, 29);
            dtpDate.TabIndex = 2;

            // ── grpAddItem (Medication input group) ────────────────────────────────
            grpAddItem.BackColor = Color.FromArgb(248, 250, 252);
            grpAddItem.Controls.Add(lblMedicine);
            grpAddItem.Controls.Add(cmbMedicine);
            grpAddItem.Controls.Add(lblDosage);
            grpAddItem.Controls.Add(txtDosage);
            grpAddItem.Controls.Add(lblFrequency);
            grpAddItem.Controls.Add(cmbFrequency);
            grpAddItem.Controls.Add(lblDuration);
            grpAddItem.Controls.Add(numDuration);
            grpAddItem.Controls.Add(lblQuantity);
            grpAddItem.Controls.Add(numQuantity);
            grpAddItem.Controls.Add(lblInstructions);
            grpAddItem.Controls.Add(txtInstructions);
            grpAddItem.Controls.Add(btnAddItem);
            grpAddItem.Dock = DockStyle.Top;
            grpAddItem.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            grpAddItem.Location = new Point(0, 127);
            grpAddItem.Name = "grpAddItem";
            grpAddItem.Padding = new Padding(12);
            grpAddItem.Size = new Size(1100, 110);
            grpAddItem.TabIndex = 2;
            grpAddItem.TabStop = false;
            grpAddItem.Text = "Add Prescribed Medication";

            // Row 1
            lblMedicine.AutoSize = true;
            lblMedicine.Font = new Font("Segoe UI", 8.5F);
            lblMedicine.Location = new Point(14, 25);
            lblMedicine.Text = "Medicine *";

            cmbMedicine.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMedicine.Font = new Font("Segoe UI", 9F);
            cmbMedicine.Location = new Point(14, 46);
            cmbMedicine.Name = "cmbMedicine";
            cmbMedicine.Size = new Size(260, 28);
            cmbMedicine.TabIndex = 0;

            lblDosage.AutoSize = true;
            lblDosage.Font = new Font("Segoe UI", 8.5F);
            lblDosage.Location = new Point(285, 25);
            lblDosage.Text = "Dosage *";

            txtDosage.Font = new Font("Segoe UI", 9F);
            txtDosage.Location = new Point(285, 46);
            txtDosage.Name = "txtDosage";
            txtDosage.PlaceholderText = "e.g. 500mg, 1 tab";
            txtDosage.Size = new Size(130, 27);
            txtDosage.TabIndex = 1;

            lblFrequency.AutoSize = true;
            lblFrequency.Font = new Font("Segoe UI", 8.5F);
            lblFrequency.Location = new Point(425, 25);
            lblFrequency.Text = "Frequency *";

            cmbFrequency.Font = new Font("Segoe UI", 9F);
            cmbFrequency.Location = new Point(425, 46);
            cmbFrequency.Name = "cmbFrequency";
            cmbFrequency.Size = new Size(170, 28);
            cmbFrequency.TabIndex = 2;
            cmbFrequency.Items.AddRange(new object[] {
                "OD (Once Daily)",
                "BD (Twice Daily)",
                "TDS (3 Times Daily)",
                "QDS (4 Times Daily)",
                "PRN (As Needed)",
                "Nocte (At Night)",
                "Mane (In Morning)"
            });

            lblDuration.AutoSize = true;
            lblDuration.Font = new Font("Segoe UI", 8.5F);
            lblDuration.Location = new Point(605, 25);
            lblDuration.Text = "Days *";

            numDuration.Font = new Font("Segoe UI", 9F);
            numDuration.Location = new Point(605, 46);
            numDuration.Minimum = 1;
            numDuration.Maximum = 365;
            numDuration.Value = 5;
            numDuration.Size = new Size(65, 27);
            numDuration.TabIndex = 3;

            lblQuantity.AutoSize = true;
            lblQuantity.Font = new Font("Segoe UI", 8.5F);
            lblQuantity.Location = new Point(680, 25);
            lblQuantity.Text = "Qty *";

            numQuantity.Font = new Font("Segoe UI", 9F);
            numQuantity.Location = new Point(680, 46);
            numQuantity.Minimum = 1;
            numQuantity.Maximum = 10000;
            numQuantity.Value = 10;
            numQuantity.Size = new Size(70, 27);
            numQuantity.TabIndex = 4;

            lblInstructions.AutoSize = true;
            lblInstructions.Font = new Font("Segoe UI", 8.5F);
            lblInstructions.Location = new Point(760, 25);
            lblInstructions.Text = "Instructions";

            txtInstructions.Font = new Font("Segoe UI", 9F);
            txtInstructions.Location = new Point(760, 46);
            txtInstructions.Name = "txtInstructions";
            txtInstructions.PlaceholderText = "e.g. After meals";
            txtInstructions.Size = new Size(180, 27);
            txtInstructions.TabIndex = 5;

            btnAddItem.BackColor = Color.FromArgb(20, 80, 140);
            btnAddItem.FlatStyle = FlatStyle.Flat;
            btnAddItem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAddItem.ForeColor = Color.White;
            btnAddItem.Location = new Point(950, 44);
            btnAddItem.Name = "btnAddItem";
            btnAddItem.Size = new Size(135, 31);
            btnAddItem.TabIndex = 6;
            btnAddItem.Text = "➕ Add Item";
            btnAddItem.UseVisualStyleBackColor = false;
            btnAddItem.Click += BtnAddItem_Click;

            // ── pnlCenter (Line items grid) ───────────────────────────────────────
            pnlCenter.Controls.Add(dgvItems);
            pnlCenter.Dock = DockStyle.Fill;
            pnlCenter.Location = new Point(0, 237);
            pnlCenter.Name = "pnlCenter";
            pnlCenter.Padding = new Padding(12);
            pnlCenter.Size = new Size(1100, 310);
            pnlCenter.TabIndex = 3;

            dgvItems.AllowUserToAddRows = false;
            dgvItems.AllowUserToDeleteRows = false;
            dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvItems.BackgroundColor = Color.White;
            dgvItems.BorderStyle = BorderStyle.Fixed3D;

            dgvHeaderStyle.BackColor = Color.FromArgb(27, 54, 93);
            dgvHeaderStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvHeaderStyle.ForeColor = Color.White;
            dgvItems.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
            dgvItems.ColumnHeadersHeight = 32;

            dgvDefaultStyle.Font = new Font("Segoe UI", 9F);
            dgvDefaultStyle.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dgvDefaultStyle.SelectionForeColor = Color.Black;
            dgvItems.DefaultCellStyle = dgvDefaultStyle;

            dgvItems.Dock = DockStyle.Fill;
            dgvItems.EnableHeadersVisualStyles = false;
            dgvItems.MultiSelect = false;
            dgvItems.ReadOnly = true;
            dgvItems.RowHeadersVisible = false;
            dgvItems.RowTemplate.Height = 28;
            dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItems.CellContentClick += DgvItems_CellContentClick;
            dgvItems.CellFormatting += DgvItems_CellFormatting;

            // ── pnlFooter ──────────────────────────────────────────────────────────
            pnlFooter.BackColor = Color.FromArgb(245, 248, 252);
            pnlFooter.BorderStyle = BorderStyle.FixedSingle;
            pnlFooter.Controls.Add(lblNotes);
            pnlFooter.Controls.Add(txtNotes);
            pnlFooter.Controls.Add(lblSummary);
            pnlFooter.Controls.Add(btnSave);
            pnlFooter.Controls.Add(btnClear);
            pnlFooter.Controls.Add(btnClose);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 547);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(15, 8, 15, 8);
            pnlFooter.Size = new Size(1100, 110);
            pnlFooter.TabIndex = 4;

            lblNotes.AutoSize = true;
            lblNotes.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblNotes.Location = new Point(15, 10);
            lblNotes.Text = "Doctor's Remarks / Pharmacy Notes:";

            txtNotes.Font = new Font("Segoe UI", 9F);
            txtNotes.Location = new Point(15, 30);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.PlaceholderText = "General instructions, allergy notes, or dispensing precautions...";
            txtNotes.Size = new Size(520, 68);
            txtNotes.TabIndex = 0;

            lblSummary.AutoSize = true;
            lblSummary.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblSummary.ForeColor = Color.FromArgb(20, 60, 120);
            lblSummary.Location = new Point(560, 25);
            lblSummary.Name = "lblSummary";
            lblSummary.Size = new Size(380, 25);
            lblSummary.Text = "Total Items: 0  |  Est. Pharmacy Cost: Rs. 0.00";

            btnSave.BackColor = Color.FromArgb(20, 80, 140);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(560, 60);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(190, 38);
            btnSave.TabIndex = 1;
            btnSave.Text = "💾 Save Prescription";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += BtnSave_Click;

            btnClear.BackColor = Color.FromArgb(240, 240, 240);
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 9.5F);
            btnClear.Location = new Point(760, 60);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(120, 38);
            btnClear.TabIndex = 2;
            btnClear.Text = "🧹 Clear All";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += BtnClear_Click;

            btnClose.BackColor = Color.FromArgb(240, 240, 240);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 9.5F);
            btnClose.Location = new Point(890, 60);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(100, 38);
            btnClose.TabIndex = 3;
            btnClose.Text = "✖️ Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += (s, e) => Close();

            // ── PrescriptionForm ───────────────────────────────────────────────────
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1100, 657);
            Controls.Add(pnlCenter);
            Controls.Add(grpAddItem);
            Controls.Add(pnlFooter);
            Controls.Add(pnlContext);
            Controls.Add(pnlHeader);
            MinimumSize = new Size(1000, 600);
            Name = "PrescriptionForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MediCare - Medical Prescription Authoring";
            Load += PrescriptionForm_Load;

            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlContext.ResumeLayout(false);
            pnlContext.PerformLayout();
            grpAddItem.ResumeLayout(false);
            grpAddItem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numDuration).EndInit();
            ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
            pnlCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblHeaderTitle;
        private Label lblHeaderSubtitle;
        private Panel pnlContext;
        private Label lblPatient;
        private ComboBox cmbPatient;
        private Label lblDoctor;
        private ComboBox cmbDoctor;
        private Label lblDate;
        private DateTimePicker dtpDate;
        private GroupBox grpAddItem;
        private Label lblMedicine;
        private ComboBox cmbMedicine;
        private Label lblDosage;
        private TextBox txtDosage;
        private Label lblFrequency;
        private ComboBox cmbFrequency;
        private Label lblDuration;
        private NumericUpDown numDuration;
        private Label lblQuantity;
        private NumericUpDown numQuantity;
        private Label lblInstructions;
        private TextBox txtInstructions;
        private Button btnAddItem;
        private Panel pnlCenter;
        private DataGridView dgvItems;
        private Panel pnlFooter;
        private Label lblNotes;
        private TextBox txtNotes;
        private Label lblSummary;
        private Button btnSave;
        private Button btnClear;
        private Button btnClose;
    }
}
