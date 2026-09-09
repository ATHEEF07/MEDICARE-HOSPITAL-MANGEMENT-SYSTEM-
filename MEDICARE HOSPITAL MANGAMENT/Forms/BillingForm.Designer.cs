using System.Drawing;
using System.Windows.Forms;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    partial class BillingForm
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
            DataGridViewCellStyle dgvHeaderStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dgvDefaultStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dgvHeaderStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dgvDefaultStyle2 = new DataGridViewCellStyle();

            pnlHeader = new Panel();
            lblHeaderSubtitle = new Label();
            lblHeaderTitle = new Label();

            splitContainer = new SplitContainer();
            pnlInvoiceBuilder = new Panel();
            grpPatientAppt = new GroupBox();
            lblPatient = new Label();
            cmbPatient = new ComboBox();
            lblAppointment = new Label();
            cmbAppointment = new ComboBox();
            lblDate = new Label();
            dtpBillDate = new DateTimePicker();

            grpCharges = new GroupBox();
            lblChargeDesc = new Label();
            cmbPresetServices = new ComboBox();
            txtCustomDesc = new TextBox();
            lblChargeAmount = new Label();
            txtChargeAmount = new TextBox();
            btnAddCharge = new Button();
            btnRemoveCharge = new Button();
            dgvCharges = new DataGridView();

            grpCalculation = new GroupBox();
            lblSubtotalText = new Label();
            lblSubtotalValue = new Label();
            lblDiscount = new Label();
            txtDiscount = new TextBox();
            lblTotalText = new Label();
            lblTotalValue = new Label();
            lblBillDesc = new Label();
            txtBillNotes = new TextBox();
            btnGenerateBill = new Button();
            btnClearForm = new Button();

            pnlRecentBills = new Panel();
            pnlRecentFilter = new Panel();
            lblRecentTitle = new Label();
            txtSearchRecent = new TextBox();
            cmbStatusFilter = new ComboBox();
            btnRefreshBills = new Button();
            btnPaySelected = new Button();
            btnCancelBill = new Button();
            dgvBills = new DataGridView();

            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            pnlInvoiceBuilder.SuspendLayout();
            grpPatientAppt.SuspendLayout();
            grpCharges.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCharges).BeginInit();
            grpCalculation.SuspendLayout();
            pnlRecentBills.SuspendLayout();
            pnlRecentFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBills).BeginInit();
            SuspendLayout();

            // ── pnlHeader ──────────────────────────────────────────────────────────
            pnlHeader.BackColor = Color.FromArgb(20, 80, 140);
            pnlHeader.Controls.Add(lblHeaderSubtitle);
            pnlHeader.Controls.Add(lblHeaderTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(20, 12, 20, 12);
            pnlHeader.Size = new Size(1220, 75);
            pnlHeader.TabIndex = 0;

            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(20, 12);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(420, 35);
            lblHeaderTitle.Text = "💳  Hospital Billing & Invoicing";

            lblHeaderSubtitle.AutoSize = true;
            lblHeaderSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblHeaderSubtitle.ForeColor = Color.FromArgb(200, 225, 255);
            lblHeaderSubtitle.Location = new Point(22, 45);
            lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            lblHeaderSubtitle.Size = new Size(540, 21);
            lblHeaderSubtitle.Text = "Generate patient invoices for consultations, medications, laboratory tests, and care services";

            // ── splitContainer ─────────────────────────────────────────────────────
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 75);
            splitContainer.Name = "splitContainer";
            splitContainer.SplitterDistance = 600;
            splitContainer.TabIndex = 1;

            // ── Panel 1: Invoice Builder ───────────────────────────────────────────
            pnlInvoiceBuilder.AutoScroll = true;
            pnlInvoiceBuilder.Controls.Add(grpCalculation);
            pnlInvoiceBuilder.Controls.Add(grpCharges);
            pnlInvoiceBuilder.Controls.Add(grpPatientAppt);
            pnlInvoiceBuilder.Dock = DockStyle.Fill;
            pnlInvoiceBuilder.Location = new Point(0, 0);
            pnlInvoiceBuilder.Name = "pnlInvoiceBuilder";
            pnlInvoiceBuilder.Padding = new Padding(12);
            pnlInvoiceBuilder.Size = new Size(600, 645);
            pnlInvoiceBuilder.TabIndex = 0;
            splitContainer.Panel1.Controls.Add(pnlInvoiceBuilder);

            // grpPatientAppt
            grpPatientAppt.Controls.Add(lblPatient);
            grpPatientAppt.Controls.Add(cmbPatient);
            grpPatientAppt.Controls.Add(lblAppointment);
            grpPatientAppt.Controls.Add(cmbAppointment);
            grpPatientAppt.Controls.Add(lblDate);
            grpPatientAppt.Controls.Add(dtpBillDate);
            grpPatientAppt.Dock = DockStyle.Top;
            grpPatientAppt.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpPatientAppt.Location = new Point(12, 12);
            grpPatientAppt.Name = "grpPatientAppt";
            grpPatientAppt.Padding = new Padding(10);
            grpPatientAppt.Size = new Size(576, 95);
            grpPatientAppt.TabIndex = 0;
            grpPatientAppt.TabStop = false;
            grpPatientAppt.Text = "1. Patient & Context";

            lblPatient.AutoSize = true;
            lblPatient.Font = new Font("Segoe UI", 8.5F);
            lblPatient.Location = new Point(10, 26);
            lblPatient.Text = "Select Patient *";

            cmbPatient.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPatient.Font = new Font("Segoe UI", 9F);
            cmbPatient.Location = new Point(10, 48);
            cmbPatient.Name = "cmbPatient";
            cmbPatient.Size = new Size(220, 28);
            cmbPatient.TabIndex = 0;
            cmbPatient.SelectedIndexChanged += CmbPatient_SelectedIndexChanged;

            lblAppointment.AutoSize = true;
            lblAppointment.Font = new Font("Segoe UI", 8.5F);
            lblAppointment.Location = new Point(240, 26);
            lblAppointment.Text = "Link Appointment (Optional)";

            cmbAppointment.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAppointment.Font = new Font("Segoe UI", 9F);
            cmbAppointment.Location = new Point(240, 48);
            cmbAppointment.Name = "cmbAppointment";
            cmbAppointment.Size = new Size(180, 28);
            cmbAppointment.TabIndex = 1;

            lblDate.AutoSize = true;
            lblDate.Font = new Font("Segoe UI", 8.5F);
            lblDate.Location = new Point(430, 26);
            lblDate.Text = "Invoice Date";

            dtpBillDate.CustomFormat = "dd/MM/yyyy";
            dtpBillDate.Font = new Font("Segoe UI", 9F);
            dtpBillDate.Format = DateTimePickerFormat.Custom;
            dtpBillDate.Location = new Point(430, 48);
            dtpBillDate.Name = "dtpBillDate";
            dtpBillDate.Size = new Size(135, 27);
            dtpBillDate.TabIndex = 2;

            // grpCharges
            grpCharges.Controls.Add(dgvCharges);
            grpCharges.Controls.Add(lblChargeDesc);
            grpCharges.Controls.Add(cmbPresetServices);
            grpCharges.Controls.Add(txtCustomDesc);
            grpCharges.Controls.Add(lblChargeAmount);
            grpCharges.Controls.Add(txtChargeAmount);
            grpCharges.Controls.Add(btnAddCharge);
            grpCharges.Controls.Add(btnRemoveCharge);
            grpCharges.Dock = DockStyle.Top;
            grpCharges.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpCharges.Location = new Point(12, 107);
            grpCharges.Name = "grpCharges";
            grpCharges.Padding = new Padding(10);
            grpCharges.Size = new Size(576, 265);
            grpCharges.TabIndex = 1;
            grpCharges.TabStop = false;
            grpCharges.Text = "2. Service Charges & Fees";

            lblChargeDesc.AutoSize = true;
            lblChargeDesc.Font = new Font("Segoe UI", 8.5F);
            lblChargeDesc.Location = new Point(10, 22);
            lblChargeDesc.Text = "Service / Charge Description *";

            cmbPresetServices.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPresetServices.Font = new Font("Segoe UI", 9F);
            cmbPresetServices.Items.AddRange(new object[] {
                "-- Select Preset Service --",
                "Doctor Consultation Fee",
                "Emergency Consultation Fee",
                "Hospital Registration / Card Fee",
                "Pharmacy Medication Dispensing",
                "Full Blood Count (FBC) Lab Test",
                "Fasting Blood Sugar (FBS) Test",
                "Lipid Profile Test",
                "ECG (Electrocardiogram)",
                "Chest X-Ray Examination",
                "Nursing & Observation Care Fee",
                "Hospital Bed Day Charge (Ward)",
                "Dressing / Minor Wound Treatment"
            });
            cmbPresetServices.Location = new Point(10, 42);
            cmbPresetServices.Name = "cmbPresetServices";
            cmbPresetServices.Size = new Size(230, 28);
            cmbPresetServices.TabIndex = 0;
            cmbPresetServices.SelectedIndexChanged += CmbPresetServices_SelectedIndexChanged;

            txtCustomDesc.Font = new Font("Segoe UI", 9F);
            txtCustomDesc.Location = new Point(10, 74);
            txtCustomDesc.Name = "txtCustomDesc";
            txtCustomDesc.PlaceholderText = "Or type custom service description...";
            txtCustomDesc.Size = new Size(230, 27);
            txtCustomDesc.TabIndex = 1;

            lblChargeAmount.AutoSize = true;
            lblChargeAmount.Font = new Font("Segoe UI", 8.5F);
            lblChargeAmount.Location = new Point(250, 22);
            lblChargeAmount.Text = "Fee Amount (Rs.) *";

            txtChargeAmount.Font = new Font("Segoe UI", 9F);
            txtChargeAmount.Location = new Point(250, 42);
            txtChargeAmount.Name = "txtChargeAmount";
            txtChargeAmount.PlaceholderText = "0.00";
            txtChargeAmount.Size = new Size(130, 27);
            txtChargeAmount.TabIndex = 2;

            btnAddCharge.BackColor = Color.FromArgb(20, 80, 140);
            btnAddCharge.FlatStyle = FlatStyle.Flat;
            btnAddCharge.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnAddCharge.ForeColor = Color.White;
            btnAddCharge.Location = new Point(390, 40);
            btnAddCharge.Name = "btnAddCharge";
            btnAddCharge.Size = new Size(100, 30);
            btnAddCharge.TabIndex = 3;
            btnAddCharge.Text = "➕ Add Fee";
            btnAddCharge.UseVisualStyleBackColor = false;
            btnAddCharge.Click += BtnAddCharge_Click;

            btnRemoveCharge.BackColor = Color.FromArgb(240, 240, 240);
            btnRemoveCharge.FlatStyle = FlatStyle.Flat;
            btnRemoveCharge.Font = new Font("Segoe UI", 8.5F);
            btnRemoveCharge.Location = new Point(495, 40);
            btnRemoveCharge.Name = "btnRemoveCharge";
            btnRemoveCharge.Size = new Size(70, 30);
            btnRemoveCharge.TabIndex = 4;
            btnRemoveCharge.Text = "❌ Del";
            btnRemoveCharge.UseVisualStyleBackColor = false;
            btnRemoveCharge.Click += BtnRemoveCharge_Click;

            dgvCharges.AllowUserToAddRows = false;
            dgvCharges.AllowUserToDeleteRows = false;
            dgvCharges.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCharges.BackgroundColor = Color.White;
            dgvCharges.BorderStyle = BorderStyle.Fixed3D;

            dgvHeaderStyle1.BackColor = Color.FromArgb(27, 54, 93);
            dgvHeaderStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvHeaderStyle1.ForeColor = Color.White;
            dgvCharges.ColumnHeadersDefaultCellStyle = dgvHeaderStyle1;
            dgvCharges.ColumnHeadersHeight = 28;

            dgvDefaultStyle1.Font = new Font("Segoe UI", 9F);
            dgvDefaultStyle1.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dgvDefaultStyle1.SelectionForeColor = Color.Black;
            dgvCharges.DefaultCellStyle = dgvDefaultStyle1;

            dgvCharges.EnableHeadersVisualStyles = false;
            dgvCharges.Location = new Point(10, 108);
            dgvCharges.MultiSelect = false;
            dgvCharges.Name = "dgvCharges";
            dgvCharges.ReadOnly = true;
            dgvCharges.RowHeadersVisible = false;
            dgvCharges.RowTemplate.Height = 26;
            dgvCharges.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCharges.Size = new Size(556, 145);
            dgvCharges.TabIndex = 5;

            // grpCalculation
            grpCalculation.Controls.Add(lblSubtotalText);
            grpCalculation.Controls.Add(lblSubtotalValue);
            grpCalculation.Controls.Add(lblDiscount);
            grpCalculation.Controls.Add(txtDiscount);
            grpCalculation.Controls.Add(lblTotalText);
            grpCalculation.Controls.Add(lblTotalValue);
            grpCalculation.Controls.Add(lblBillDesc);
            grpCalculation.Controls.Add(txtBillNotes);
            grpCalculation.Controls.Add(btnGenerateBill);
            grpCalculation.Controls.Add(btnClearForm);
            grpCalculation.Dock = DockStyle.Top;
            grpCalculation.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpCalculation.Location = new Point(12, 372);
            grpCalculation.Name = "grpCalculation";
            grpCalculation.Padding = new Padding(10);
            grpCalculation.Size = new Size(576, 260);
            grpCalculation.TabIndex = 2;
            grpCalculation.TabStop = false;
            grpCalculation.Text = "3. Financial Calculation & Generation";

            lblSubtotalText.AutoSize = true;
            lblSubtotalText.Font = new Font("Segoe UI", 10F);
            lblSubtotalText.Location = new Point(15, 28);
            lblSubtotalText.Text = "Gross Subtotal:";

            lblSubtotalValue.AutoSize = true;
            lblSubtotalValue.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblSubtotalValue.Location = new Point(160, 26);
            lblSubtotalValue.Text = "Rs. 0.00";

            lblDiscount.AutoSize = true;
            lblDiscount.Font = new Font("Segoe UI", 10F);
            lblDiscount.Location = new Point(15, 62);
            lblDiscount.Text = "Discount (Rs.):";

            txtDiscount.Font = new Font("Segoe UI", 9.5F);
            txtDiscount.Location = new Point(160, 58);
            txtDiscount.Name = "txtDiscount";
            txtDiscount.Size = new Size(130, 29);
            txtDiscount.TabIndex = 0;
            txtDiscount.Text = "0.00";
            txtDiscount.TextChanged += TxtDiscount_TextChanged;

            lblTotalText.AutoSize = true;
            lblTotalText.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotalText.ForeColor = Color.FromArgb(20, 80, 140);
            lblTotalText.Location = new Point(15, 98);
            lblTotalText.Text = "Net Total Due:";

            lblTotalValue.AutoSize = true;
            lblTotalValue.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotalValue.ForeColor = Color.FromArgb(20, 80, 140);
            lblTotalValue.Location = new Point(160, 93);
            lblTotalValue.Text = "Rs. 0.00";

            lblBillDesc.AutoSize = true;
            lblBillDesc.Font = new Font("Segoe UI", 8.5F);
            lblBillDesc.Location = new Point(15, 132);
            lblBillDesc.Text = "Invoice Summary / Description Notes:";

            txtBillNotes.Font = new Font("Segoe UI", 9F);
            txtBillNotes.Location = new Point(15, 152);
            txtBillNotes.Multiline = true;
            txtBillNotes.Name = "txtBillNotes";
            txtBillNotes.PlaceholderText = "e.g. General consultation and standard prescription medications...";
            txtBillNotes.Size = new Size(545, 45);
            txtBillNotes.TabIndex = 1;

            btnGenerateBill.BackColor = Color.FromArgb(20, 80, 140);
            btnGenerateBill.FlatStyle = FlatStyle.Flat;
            btnGenerateBill.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnGenerateBill.ForeColor = Color.White;
            btnGenerateBill.Location = new Point(15, 208);
            btnGenerateBill.Name = "btnGenerateBill";
            btnGenerateBill.Size = new Size(260, 40);
            btnGenerateBill.TabIndex = 2;
            btnGenerateBill.Text = "💾  Generate Invoice";
            btnGenerateBill.UseVisualStyleBackColor = false;
            btnGenerateBill.Click += BtnGenerateBill_Click;

            btnClearForm.BackColor = Color.FromArgb(240, 240, 240);
            btnClearForm.FlatStyle = FlatStyle.Flat;
            btnClearForm.Font = new Font("Segoe UI", 9.5F);
            btnClearForm.Location = new Point(290, 208);
            btnClearForm.Name = "btnClearForm";
            btnClearForm.Size = new Size(130, 40);
            btnClearForm.TabIndex = 3;
            btnClearForm.Text = "🧹 Clear";
            btnClearForm.UseVisualStyleBackColor = false;
            btnClearForm.Click += BtnClearForm_Click;

            // ── Panel 2: Recent Bills ──────────────────────────────────────────────
            pnlRecentBills.Controls.Add(dgvBills);
            pnlRecentBills.Controls.Add(pnlRecentFilter);
            pnlRecentBills.Dock = DockStyle.Fill;
            pnlRecentBills.Location = new Point(0, 0);
            pnlRecentBills.Name = "pnlRecentBills";
            pnlRecentBills.Padding = new Padding(12);
            pnlRecentBills.Size = new Size(616, 645);
            pnlRecentBills.TabIndex = 0;
            splitContainer.Panel2.Controls.Add(pnlRecentBills);

            // pnlRecentFilter
            pnlRecentFilter.BackColor = Color.FromArgb(245, 248, 252);
            pnlRecentFilter.BorderStyle = BorderStyle.FixedSingle;
            pnlRecentFilter.Controls.Add(lblRecentTitle);
            pnlRecentFilter.Controls.Add(txtSearchRecent);
            pnlRecentFilter.Controls.Add(cmbStatusFilter);
            pnlRecentFilter.Controls.Add(btnRefreshBills);
            pnlRecentFilter.Controls.Add(btnPaySelected);
            pnlRecentFilter.Controls.Add(btnCancelBill);
            pnlRecentFilter.Dock = DockStyle.Top;
            pnlRecentFilter.Location = new Point(12, 12);
            pnlRecentFilter.Name = "pnlRecentFilter";
            pnlRecentFilter.Padding = new Padding(10);
            pnlRecentFilter.Size = new Size(592, 100);
            pnlRecentFilter.TabIndex = 0;

            lblRecentTitle.AutoSize = true;
            lblRecentTitle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblRecentTitle.ForeColor = Color.FromArgb(20, 60, 120);
            lblRecentTitle.Location = new Point(10, 8);
            lblRecentTitle.Text = "Invoices & Billing History";

            txtSearchRecent.Font = new Font("Segoe UI", 9F);
            txtSearchRecent.Location = new Point(10, 34);
            txtSearchRecent.Name = "txtSearchRecent";
            txtSearchRecent.PlaceholderText = "Search by patient name, code, or invoice #...";
            txtSearchRecent.Size = new Size(240, 27);
            txtSearchRecent.TabIndex = 0;
            txtSearchRecent.TextChanged += TxtSearchRecent_TextChanged;

            cmbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatusFilter.Font = new Font("Segoe UI", 9F);
            cmbStatusFilter.FormattingEnabled = true;
            cmbStatusFilter.Items.AddRange(new object[] { "All Statuses", "Pending", "PartiallyPaid", "Paid", "Cancelled" });
            cmbStatusFilter.Location = new Point(260, 33);
            cmbStatusFilter.Name = "cmbStatusFilter";
            cmbStatusFilter.Size = new Size(130, 28);
            cmbStatusFilter.TabIndex = 1;
            cmbStatusFilter.SelectedIndexChanged += CmbStatusFilter_SelectedIndexChanged;

            btnRefreshBills.BackColor = Color.FromArgb(235, 245, 255);
            btnRefreshBills.FlatStyle = FlatStyle.Flat;
            btnRefreshBills.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnRefreshBills.ForeColor = Color.FromArgb(20, 80, 140);
            btnRefreshBills.Location = new Point(400, 32);
            btnRefreshBills.Name = "btnRefreshBills";
            btnRefreshBills.Size = new Size(80, 30);
            btnRefreshBills.TabIndex = 2;
            btnRefreshBills.Text = "🔄 Refresh";
            btnRefreshBills.UseVisualStyleBackColor = false;
            btnRefreshBills.Click += BtnRefreshBills_Click;

            btnPaySelected.BackColor = Color.FromArgb(34, 139, 34);
            btnPaySelected.FlatStyle = FlatStyle.Flat;
            btnPaySelected.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPaySelected.ForeColor = Color.White;
            btnPaySelected.Location = new Point(10, 66);
            btnPaySelected.Name = "btnPaySelected";
            btnPaySelected.Size = new Size(160, 28);
            btnPaySelected.TabIndex = 3;
            btnPaySelected.Text = "💵 Collect Payment";
            btnPaySelected.UseVisualStyleBackColor = false;
            btnPaySelected.Click += BtnPaySelected_Click;

            btnCancelBill.BackColor = Color.FromArgb(200, 40, 40);
            btnCancelBill.FlatStyle = FlatStyle.Flat;
            btnCancelBill.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnCancelBill.ForeColor = Color.White;
            btnCancelBill.Location = new Point(180, 66);
            btnCancelBill.Name = "btnCancelBill";
            btnCancelBill.Size = new Size(110, 28);
            btnCancelBill.TabIndex = 4;
            btnCancelBill.Text = "❌ Cancel Bill";
            btnCancelBill.UseVisualStyleBackColor = false;
            btnCancelBill.Click += BtnCancelBill_Click;

            // dgvBills
            dgvBills.AllowUserToAddRows = false;
            dgvBills.AllowUserToDeleteRows = false;
            dgvBills.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBills.BackgroundColor = Color.White;
            dgvBills.BorderStyle = BorderStyle.Fixed3D;

            dgvHeaderStyle2.BackColor = Color.FromArgb(27, 54, 93);
            dgvHeaderStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvHeaderStyle2.ForeColor = Color.White;
            dgvBills.ColumnHeadersDefaultCellStyle = dgvHeaderStyle2;
            dgvBills.ColumnHeadersHeight = 30;

            dgvDefaultStyle2.Font = new Font("Segoe UI", 9F);
            dgvDefaultStyle2.SelectionBackColor = Color.FromArgb(215, 235, 255);
            dgvDefaultStyle2.SelectionForeColor = Color.Black;
            dgvBills.DefaultCellStyle = dgvDefaultStyle2;

            dgvBills.Dock = DockStyle.Fill;
            dgvBills.EnableHeadersVisualStyles = false;
            dgvBills.MultiSelect = false;
            dgvBills.ReadOnly = true;
            dgvBills.RowHeadersVisible = false;
            dgvBills.RowTemplate.Height = 28;
            dgvBills.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBills.CellFormatting += DgvBills_CellFormatting;

            // ── BillingForm ────────────────────────────────────────────────────────
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1220, 720);
            Controls.Add(splitContainer);
            Controls.Add(pnlHeader);
            MinimumSize = new Size(1000, 650);
            Name = "BillingForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MediCare - Hospital Billing & Invoicing";
            Load += BillingForm_Load;

            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            pnlInvoiceBuilder.ResumeLayout(false);
            grpPatientAppt.ResumeLayout(false);
            grpPatientAppt.PerformLayout();
            grpCharges.ResumeLayout(false);
            grpCharges.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCharges).EndInit();
            grpCalculation.ResumeLayout(false);
            grpCalculation.PerformLayout();
            pnlRecentBills.ResumeLayout(false);
            pnlRecentFilter.ResumeLayout(false);
            pnlRecentFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBills).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblHeaderTitle;
        private Label lblHeaderSubtitle;
        private SplitContainer splitContainer;
        private Panel pnlInvoiceBuilder;
        private GroupBox grpPatientAppt;
        private Label lblPatient;
        private ComboBox cmbPatient;
        private Label lblAppointment;
        private ComboBox cmbAppointment;
        private Label lblDate;
        private DateTimePicker dtpBillDate;
        private GroupBox grpCharges;
        private Label lblChargeDesc;
        private ComboBox cmbPresetServices;
        private TextBox txtCustomDesc;
        private Label lblChargeAmount;
        private TextBox txtChargeAmount;
        private Button btnAddCharge;
        private Button btnRemoveCharge;
        private DataGridView dgvCharges;
        private GroupBox grpCalculation;
        private Label lblSubtotalText;
        private Label lblSubtotalValue;
        private Label lblDiscount;
        private TextBox txtDiscount;
        private Label lblTotalText;
        private Label lblTotalValue;
        private Label lblBillDesc;
        private TextBox txtBillNotes;
        private Button btnGenerateBill;
        private Button btnClearForm;
        private Panel pnlRecentBills;
        private Panel pnlRecentFilter;
        private Label lblRecentTitle;
        private TextBox txtSearchRecent;
        private ComboBox cmbStatusFilter;
        private Button btnRefreshBills;
        private Button btnPaySelected;
        private Button btnCancelBill;
        private DataGridView dgvBills;
    }
}
