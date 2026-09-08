using System.Drawing;
using System.Windows.Forms;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    partial class PaymentForm
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

            pnlMain = new Panel();
            grpBillSelect = new GroupBox();
            lblSelectBill = new Label();
            cmbBill = new ComboBox();
            lblPatientName = new Label();
            lblPatientCode = new Label();
            lblBillDate = new Label();
            lblBillDescription = new Label();

            pnlMetrics = new Panel();
            pnlMetricTotal = new Panel();
            lblMetricTotalText = new Label();
            lblMetricTotalVal = new Label();
            pnlMetricPaid = new Panel();
            lblMetricPaidText = new Label();
            lblMetricPaidVal = new Label();
            pnlMetricDue = new Panel();
            lblMetricDueText = new Label();
            lblMetricDueVal = new Label();

            grpPaymentInput = new GroupBox();
            lblAmount = new Label();
            txtPaymentAmount = new TextBox();
            lblMethod = new Label();
            cmbPaymentMethod = new ComboBox();
            lblReference = new Label();
            txtReferenceNo = new TextBox();
            btnProcessPayment = new Button();
            btnPrintReceipt = new Button();

            grpHistory = new GroupBox();
            dgvPreviousPayments = new DataGridView();

            pnlFooter = new Panel();
            btnClose = new Button();

            pnlHeader.SuspendLayout();
            pnlMain.SuspendLayout();
            grpBillSelect.SuspendLayout();
            pnlMetrics.SuspendLayout();
            pnlMetricTotal.SuspendLayout();
            pnlMetricPaid.SuspendLayout();
            pnlMetricDue.SuspendLayout();
            grpPaymentInput.SuspendLayout();
            grpHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPreviousPayments).BeginInit();
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
            pnlHeader.Size = new Size(880, 75);
            pnlHeader.TabIndex = 0;

            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(20, 12);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(420, 35);
            lblHeaderTitle.Text = "💵  Cashier Payment Processing";

            lblHeaderSubtitle.AutoSize = true;
            lblHeaderSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblHeaderSubtitle.ForeColor = Color.FromArgb(200, 225, 255);
            lblHeaderSubtitle.Location = new Point(22, 45);
            lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            lblHeaderSubtitle.Size = new Size(540, 21);
            lblHeaderSubtitle.Text = "Collect customer payments, prevent overpayments, and issue official payment receipts";

            // ── pnlMain ────────────────────────────────────────────────────────────
            pnlMain.AutoScroll = true;
            pnlMain.Controls.Add(grpHistory);
            pnlMain.Controls.Add(grpPaymentInput);
            pnlMain.Controls.Add(pnlMetrics);
            pnlMain.Controls.Add(grpBillSelect);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 75);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(15);
            pnlMain.Size = new Size(880, 565);
            pnlMain.TabIndex = 1;

            // grpBillSelect
            grpBillSelect.Controls.Add(lblSelectBill);
            grpBillSelect.Controls.Add(cmbBill);
            grpBillSelect.Controls.Add(lblPatientName);
            grpBillSelect.Controls.Add(lblPatientCode);
            grpBillSelect.Controls.Add(lblBillDate);
            grpBillSelect.Controls.Add(lblBillDescription);
            grpBillSelect.Dock = DockStyle.Top;
            grpBillSelect.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpBillSelect.Location = new Point(15, 15);
            grpBillSelect.Name = "grpBillSelect";
            grpBillSelect.Padding = new Padding(12);
            grpBillSelect.Size = new Size(850, 105);
            grpBillSelect.TabIndex = 0;
            grpBillSelect.TabStop = false;
            grpBillSelect.Text = "Invoice Information";

            lblSelectBill.AutoSize = true;
            lblSelectBill.Font = new Font("Segoe UI", 8.5F);
            lblSelectBill.Location = new Point(12, 24);
            lblSelectBill.Text = "Select Pending Invoice *";

            cmbBill.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBill.Font = new Font("Segoe UI", 9F);
            cmbBill.Location = new Point(12, 44);
            cmbBill.Name = "cmbBill";
            cmbBill.Size = new Size(380, 28);
            cmbBill.TabIndex = 0;
            cmbBill.SelectedIndexChanged += CmbBill_SelectedIndexChanged;

            lblPatientName.AutoSize = true;
            lblPatientName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPatientName.Location = new Point(410, 24);
            lblPatientName.Text = "Patient: —";

            lblPatientCode.AutoSize = true;
            lblPatientCode.Font = new Font("Segoe UI", 8.5F);
            lblPatientCode.Location = new Point(680, 25);
            lblPatientCode.Text = "Code: —";

            lblBillDate.AutoSize = true;
            lblBillDate.Font = new Font("Segoe UI", 8.5F);
            lblBillDate.Location = new Point(410, 50);
            lblBillDate.Text = "Bill Date: —";

            lblBillDescription.AutoSize = true;
            lblBillDescription.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            lblBillDescription.ForeColor = Color.FromArgb(80, 80, 80);
            lblBillDescription.Location = new Point(410, 75);
            lblBillDescription.Text = "Notes: —";

            // pnlMetrics (3 Cards)
            pnlMetrics.Dock = DockStyle.Top;
            pnlMetrics.Location = new Point(15, 120);
            pnlMetrics.Name = "pnlMetrics";
            pnlMetrics.Padding = new Padding(0, 10, 0, 10);
            pnlMetrics.Size = new Size(850, 95);
            pnlMetrics.TabIndex = 1;

            // Card 1: Total Billed
            pnlMetricTotal.BackColor = Color.FromArgb(240, 245, 250);
            pnlMetricTotal.BorderStyle = BorderStyle.FixedSingle;
            pnlMetricTotal.Controls.Add(lblMetricTotalText);
            pnlMetricTotal.Controls.Add(lblMetricTotalVal);
            pnlMetricTotal.Location = new Point(0, 10);
            pnlMetricTotal.Name = "pnlMetricTotal";
            pnlMetricTotal.Size = new Size(270, 75);
            pnlMetricTotal.TabIndex = 0;

            lblMetricTotalText.AutoSize = true;
            lblMetricTotalText.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMetricTotalText.ForeColor = Color.FromArgb(50, 70, 90);
            lblMetricTotalText.Location = new Point(12, 10);
            lblMetricTotalText.Text = "Total Invoice Amount";

            lblMetricTotalVal.AutoSize = true;
            lblMetricTotalVal.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblMetricTotalVal.ForeColor = Color.FromArgb(20, 60, 110);
            lblMetricTotalVal.Location = new Point(12, 32);
            lblMetricTotalVal.Text = "Rs. 0.00";

            // Card 2: Already Paid
            pnlMetricPaid.BackColor = Color.FromArgb(240, 252, 244);
            pnlMetricPaid.BorderStyle = BorderStyle.FixedSingle;
            pnlMetricPaid.Controls.Add(lblMetricPaidText);
            pnlMetricPaid.Controls.Add(lblMetricPaidVal);
            pnlMetricPaid.Location = new Point(290, 10);
            pnlMetricPaid.Name = "pnlMetricPaid";
            pnlMetricPaid.Size = new Size(270, 75);
            pnlMetricPaid.TabIndex = 1;

            lblMetricPaidText.AutoSize = true;
            lblMetricPaidText.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMetricPaidText.ForeColor = Color.FromArgb(30, 100, 50);
            lblMetricPaidText.Location = new Point(12, 10);
            lblMetricPaidText.Text = "Already Paid";

            lblMetricPaidVal.AutoSize = true;
            lblMetricPaidVal.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblMetricPaidVal.ForeColor = Color.FromArgb(20, 120, 50);
            lblMetricPaidVal.Location = new Point(12, 32);
            lblMetricPaidVal.Text = "Rs. 0.00";

            // Card 3: Outstanding Due
            pnlMetricDue.BackColor = Color.FromArgb(255, 245, 240);
            pnlMetricDue.BorderStyle = BorderStyle.FixedSingle;
            pnlMetricDue.Controls.Add(lblMetricDueText);
            pnlMetricDue.Controls.Add(lblMetricDueVal);
            pnlMetricDue.Location = new Point(580, 10);
            pnlMetricDue.Name = "pnlMetricDue";
            pnlMetricDue.Size = new Size(270, 75);
            pnlMetricDue.TabIndex = 2;

            lblMetricDueText.AutoSize = true;
            lblMetricDueText.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMetricDueText.ForeColor = Color.FromArgb(180, 60, 20);
            lblMetricDueText.Location = new Point(12, 10);
            lblMetricDueText.Text = "Outstanding Balance Due";

            lblMetricDueVal.AutoSize = true;
            lblMetricDueVal.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblMetricDueVal.ForeColor = Color.FromArgb(180, 40, 20);
            lblMetricDueVal.Location = new Point(12, 32);
            lblMetricDueVal.Text = "Rs. 0.00";

            // grpPaymentInput
            grpPaymentInput.Controls.Add(lblAmount);
            grpPaymentInput.Controls.Add(txtPaymentAmount);
            grpPaymentInput.Controls.Add(lblMethod);
            grpPaymentInput.Controls.Add(cmbPaymentMethod);
            grpPaymentInput.Controls.Add(lblReference);
            grpPaymentInput.Controls.Add(txtReferenceNo);
            grpPaymentInput.Controls.Add(btnProcessPayment);
            grpPaymentInput.Controls.Add(btnPrintReceipt);
            grpPaymentInput.Dock = DockStyle.Top;
            grpPaymentInput.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpPaymentInput.Location = new Point(15, 215);
            grpPaymentInput.Name = "grpPaymentInput";
            grpPaymentInput.Padding = new Padding(12);
            grpPaymentInput.Size = new Size(850, 110);
            grpPaymentInput.TabIndex = 2;
            grpPaymentInput.TabStop = false;
            grpPaymentInput.Text = "Payment Entry";

            lblAmount.AutoSize = true;
            lblAmount.Font = new Font("Segoe UI", 8.5F);
            lblAmount.Location = new Point(12, 28);
            lblAmount.Text = "Payment Amount (Rs.) *";

            txtPaymentAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            txtPaymentAmount.Location = new Point(12, 50);
            txtPaymentAmount.Name = "txtPaymentAmount";
            txtPaymentAmount.Size = new Size(160, 30);
            txtPaymentAmount.TabIndex = 0;

            lblMethod.AutoSize = true;
            lblMethod.Font = new Font("Segoe UI", 8.5F);
            lblMethod.Location = new Point(190, 28);
            lblMethod.Text = "Payment Method *";

            cmbPaymentMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPaymentMethod.Font = new Font("Segoe UI", 9.5F);
            cmbPaymentMethod.Items.AddRange(new object[] { "Cash", "Card", "BankTransfer" });
            cmbPaymentMethod.Location = new Point(190, 50);
            cmbPaymentMethod.Name = "cmbPaymentMethod";
            cmbPaymentMethod.Size = new Size(140, 29);
            cmbPaymentMethod.TabIndex = 1;

            lblReference.AutoSize = true;
            lblReference.Font = new Font("Segoe UI", 8.5F);
            lblReference.Location = new Point(345, 28);
            lblReference.Text = "Reference / Receipt No.";

            txtReferenceNo.Font = new Font("Segoe UI", 9.5F);
            txtReferenceNo.Location = new Point(345, 50);
            txtReferenceNo.Name = "txtReferenceNo";
            txtReferenceNo.Size = new Size(160, 29);
            txtReferenceNo.TabIndex = 2;

            btnProcessPayment.BackColor = Color.FromArgb(34, 139, 34);
            btnProcessPayment.FlatStyle = FlatStyle.Flat;
            btnProcessPayment.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnProcessPayment.ForeColor = Color.White;
            btnProcessPayment.Location = new Point(525, 46);
            btnProcessPayment.Name = "btnProcessPayment";
            btnProcessPayment.Size = new Size(175, 36);
            btnProcessPayment.TabIndex = 3;
            btnProcessPayment.Text = "✅  Confirm Payment";
            btnProcessPayment.UseVisualStyleBackColor = false;
            btnProcessPayment.Click += BtnProcessPayment_Click;

            btnPrintReceipt.BackColor = Color.FromArgb(235, 245, 255);
            btnPrintReceipt.FlatStyle = FlatStyle.Flat;
            btnPrintReceipt.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPrintReceipt.ForeColor = Color.FromArgb(20, 80, 140);
            btnPrintReceipt.Location = new Point(710, 46);
            btnPrintReceipt.Name = "btnPrintReceipt";
            btnPrintReceipt.Size = new Size(125, 36);
            btnPrintReceipt.TabIndex = 4;
            btnPrintReceipt.Text = "📄 View Receipt";
            btnPrintReceipt.UseVisualStyleBackColor = false;
            btnPrintReceipt.Click += BtnPrintReceipt_Click;

            // grpHistory
            grpHistory.Controls.Add(dgvPreviousPayments);
            grpHistory.Dock = DockStyle.Fill;
            grpHistory.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpHistory.Location = new Point(15, 325);
            grpHistory.Name = "grpHistory";
            grpHistory.Padding = new Padding(10);
            grpHistory.Size = new Size(850, 225);
            grpHistory.TabIndex = 3;
            grpHistory.TabStop = false;
            grpHistory.Text = "Payment History for this Invoice";

            dgvPreviousPayments.AllowUserToAddRows = false;
            dgvPreviousPayments.AllowUserToDeleteRows = false;
            dgvPreviousPayments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPreviousPayments.BackgroundColor = Color.White;
            dgvPreviousPayments.BorderStyle = BorderStyle.Fixed3D;

            dgvHeaderStyle.BackColor = Color.FromArgb(27, 54, 93);
            dgvHeaderStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvHeaderStyle.ForeColor = Color.White;
            dgvPreviousPayments.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
            dgvPreviousPayments.ColumnHeadersHeight = 28;

            dgvDefaultStyle.Font = new Font("Segoe UI", 9F);
            dgvDefaultStyle.SelectionBackColor = Color.FromArgb(215, 235, 255);
            dgvDefaultStyle.SelectionForeColor = Color.Black;
            dgvPreviousPayments.DefaultCellStyle = dgvDefaultStyle;

            dgvPreviousPayments.Dock = DockStyle.Fill;
            dgvPreviousPayments.EnableHeadersVisualStyles = false;
            dgvPreviousPayments.MultiSelect = false;
            dgvPreviousPayments.ReadOnly = true;
            dgvPreviousPayments.RowHeadersVisible = false;
            dgvPreviousPayments.RowTemplate.Height = 26;
            dgvPreviousPayments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // ── pnlFooter ──────────────────────────────────────────────────────────
            pnlFooter.BackColor = Color.FromArgb(245, 248, 252);
            pnlFooter.BorderStyle = BorderStyle.FixedSingle;
            pnlFooter.Controls.Add(btnClose);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 640);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(880, 50);
            pnlFooter.TabIndex = 2;

            btnClose.BackColor = Color.FromArgb(240, 240, 240);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 9.5F);
            btnClose.Location = new Point(765, 8);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(95, 32);
            btnClose.TabIndex = 0;
            btnClose.Text = "✖️ Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += (s, e) => Close();

            // ── PaymentForm ────────────────────────────────────────────────────────
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(880, 690);
            Controls.Add(pnlMain);
            Controls.Add(pnlFooter);
            Controls.Add(pnlHeader);
            MinimumSize = new Size(850, 600);
            Name = "PaymentForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MediCare - Cashier Payment Processing";
            Load += PaymentForm_Load;

            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlMain.ResumeLayout(false);
            grpBillSelect.ResumeLayout(false);
            grpBillSelect.PerformLayout();
            pnlMetrics.ResumeLayout(false);
            pnlMetricTotal.ResumeLayout(false);
            pnlMetricTotal.PerformLayout();
            pnlMetricPaid.ResumeLayout(false);
            pnlMetricPaid.PerformLayout();
            pnlMetricDue.ResumeLayout(false);
            pnlMetricDue.PerformLayout();
            grpPaymentInput.ResumeLayout(false);
            grpPaymentInput.PerformLayout();
            grpHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPreviousPayments).EndInit();
            pnlFooter.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblHeaderTitle;
        private Label lblHeaderSubtitle;
        private Panel pnlMain;
        private GroupBox grpBillSelect;
        private Label lblSelectBill;
        private ComboBox cmbBill;
        private Label lblPatientName;
        private Label lblPatientCode;
        private Label lblBillDate;
        private Label lblBillDescription;
        private Panel pnlMetrics;
        private Panel pnlMetricTotal;
        private Label lblMetricTotalText;
        private Label lblMetricTotalVal;
        private Panel pnlMetricPaid;
        private Label lblMetricPaidText;
        private Label lblMetricPaidVal;
        private Panel pnlMetricDue;
        private Label lblMetricDueText;
        private Label lblMetricDueVal;
        private GroupBox grpPaymentInput;
        private Label lblAmount;
        private TextBox txtPaymentAmount;
        private Label lblMethod;
        private ComboBox cmbPaymentMethod;
        private Label lblReference;
        private TextBox txtReferenceNo;
        private Button btnProcessPayment;
        private Button btnPrintReceipt;
        private GroupBox grpHistory;
        private DataGridView dgvPreviousPayments;
        private Panel pnlFooter;
        private Button btnClose;
    }
}
