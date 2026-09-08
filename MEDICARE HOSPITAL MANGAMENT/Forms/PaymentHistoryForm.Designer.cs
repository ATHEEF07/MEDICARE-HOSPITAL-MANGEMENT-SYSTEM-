using System.Drawing;
using System.Windows.Forms;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    partial class PaymentHistoryForm
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

            pnlMetrics = new Panel();
            pnlCardTotal = new Panel();
            lblCardTotalText = new Label();
            lblCardTotalVal = new Label();
            pnlCardCash = new Panel();
            lblCardCashText = new Label();
            lblCardCashVal = new Label();
            pnlCardCard = new Panel();
            lblCardCardText = new Label();
            lblCardCardVal = new Label();
            pnlCardBank = new Panel();
            lblCardBankText = new Label();
            lblCardBankVal = new Label();

            pnlFilters = new Panel();
            lblSearch = new Label();
            txtSearch = new TextBox();
            lblMethodFilter = new Label();
            cmbMethodFilter = new ComboBox();
            lblFromDate = new Label();
            dtpFrom = new DateTimePicker();
            lblToDate = new Label();
            dtpTo = new DateTimePicker();
            btnApplyFilters = new Button();
            btnRefresh = new Button();

            pnlGrid = new Panel();
            dgvPayments = new DataGridView();

            pnlFooter = new Panel();
            btnPrintReceipt = new Button();
            lblCount = new Label();
            btnClose = new Button();

            pnlHeader.SuspendLayout();
            pnlMetrics.SuspendLayout();
            pnlCardTotal.SuspendLayout();
            pnlCardCash.SuspendLayout();
            pnlCardCard.SuspendLayout();
            pnlCardBank.SuspendLayout();
            pnlFilters.SuspendLayout();
            pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPayments).BeginInit();
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
            pnlHeader.Size = new Size(1150, 75);
            pnlHeader.TabIndex = 0;

            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(20, 12);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(490, 35);
            lblHeaderTitle.Text = "📜  Payment Transaction History & Audit Log";

            lblHeaderSubtitle.AutoSize = true;
            lblHeaderSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblHeaderSubtitle.ForeColor = Color.FromArgb(200, 225, 255);
            lblHeaderSubtitle.Location = new Point(22, 45);
            lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            lblHeaderSubtitle.Size = new Size(540, 21);
            lblHeaderSubtitle.Text = "Audit trail of cashier payment collections, payment breakdown, and receipt reprints";

            // ── pnlMetrics (4 Summary Cards) ───────────────────────────────────────
            pnlMetrics.BackColor = Color.FromArgb(245, 248, 252);
            pnlMetrics.Controls.Add(pnlCardBank);
            pnlMetrics.Controls.Add(pnlCardCard);
            pnlMetrics.Controls.Add(pnlCardCash);
            pnlMetrics.Controls.Add(pnlCardTotal);
            pnlMetrics.Dock = DockStyle.Top;
            pnlMetrics.Location = new Point(0, 75);
            pnlMetrics.Name = "pnlMetrics";
            pnlMetrics.Padding = new Padding(15, 10, 15, 10);
            pnlMetrics.Size = new Size(1150, 85);
            pnlMetrics.TabIndex = 1;

            // Total Collections
            pnlCardTotal.BackColor = Color.White;
            pnlCardTotal.BorderStyle = BorderStyle.FixedSingle;
            pnlCardTotal.Controls.Add(lblCardTotalText);
            pnlCardTotal.Controls.Add(lblCardTotalVal);
            pnlCardTotal.Location = new Point(15, 8);
            pnlCardTotal.Name = "pnlCardTotal";
            pnlCardTotal.Size = new Size(260, 68);
            pnlCardTotal.TabIndex = 0;

            lblCardTotalText.AutoSize = true;
            lblCardTotalText.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblCardTotalText.ForeColor = Color.FromArgb(20, 60, 120);
            lblCardTotalText.Location = new Point(12, 8);
            lblCardTotalText.Text = "Total Collections";

            lblCardTotalVal.AutoSize = true;
            lblCardTotalVal.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblCardTotalVal.ForeColor = Color.FromArgb(20, 80, 140);
            lblCardTotalVal.Location = new Point(12, 28);
            lblCardTotalVal.Text = "Rs. 0.00";

            // Cash Collections
            pnlCardCash.BackColor = Color.White;
            pnlCardCash.BorderStyle = BorderStyle.FixedSingle;
            pnlCardCash.Controls.Add(lblCardCashText);
            pnlCardCash.Controls.Add(lblCardCashVal);
            pnlCardCash.Location = new Point(295, 8);
            pnlCardCash.Name = "pnlCardCash";
            pnlCardCash.Size = new Size(260, 68);
            pnlCardCash.TabIndex = 1;

            lblCardCashText.AutoSize = true;
            lblCardCashText.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblCardCashText.ForeColor = Color.FromArgb(30, 100, 50);
            lblCardCashText.Location = new Point(12, 8);
            lblCardCashText.Text = "Cash Collections";

            lblCardCashVal.AutoSize = true;
            lblCardCashVal.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblCardCashVal.ForeColor = Color.FromArgb(30, 120, 50);
            lblCardCashVal.Location = new Point(12, 28);
            lblCardCashVal.Text = "Rs. 0.00";

            // Card Collections
            pnlCardCard.BackColor = Color.White;
            pnlCardCard.BorderStyle = BorderStyle.FixedSingle;
            pnlCardCard.Controls.Add(lblCardCardText);
            pnlCardCard.Controls.Add(lblCardCardVal);
            pnlCardCard.Location = new Point(575, 8);
            pnlCardCard.Name = "pnlCardCard";
            pnlCardCard.Size = new Size(260, 68);
            pnlCardCard.TabIndex = 2;

            lblCardCardText.AutoSize = true;
            lblCardCardText.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblCardCardText.ForeColor = Color.FromArgb(140, 70, 20);
            lblCardCardText.Location = new Point(12, 8);
            lblCardCardText.Text = "Credit/Debit Card";

            lblCardCardVal.AutoSize = true;
            lblCardCardVal.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblCardCardVal.ForeColor = Color.FromArgb(180, 80, 20);
            lblCardCardVal.Location = new Point(12, 28);
            lblCardCardVal.Text = "Rs. 0.00";

            // Bank Transfer
            pnlCardBank.BackColor = Color.White;
            pnlCardBank.BorderStyle = BorderStyle.FixedSingle;
            pnlCardBank.Controls.Add(lblCardBankText);
            pnlCardBank.Controls.Add(lblCardBankVal);
            pnlCardBank.Location = new Point(855, 8);
            pnlCardBank.Name = "pnlCardBank";
            pnlCardBank.Size = new Size(260, 68);
            pnlCardBank.TabIndex = 3;

            lblCardBankText.AutoSize = true;
            lblCardBankText.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblCardBankText.ForeColor = Color.FromArgb(100, 30, 120);
            lblCardBankText.Location = new Point(12, 8);
            lblCardBankText.Text = "Bank Transfers";

            lblCardBankVal.AutoSize = true;
            lblCardBankVal.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblCardBankVal.ForeColor = Color.FromArgb(120, 40, 140);
            lblCardBankVal.Location = new Point(12, 28);
            lblCardBankVal.Text = "Rs. 0.00";

            // ── pnlFilters ─────────────────────────────────────────────────────────
            pnlFilters.BackColor = Color.FromArgb(240, 244, 250);
            pnlFilters.BorderStyle = BorderStyle.FixedSingle;
            pnlFilters.Controls.Add(lblSearch);
            pnlFilters.Controls.Add(txtSearch);
            pnlFilters.Controls.Add(lblMethodFilter);
            pnlFilters.Controls.Add(cmbMethodFilter);
            pnlFilters.Controls.Add(lblFromDate);
            pnlFilters.Controls.Add(dtpFrom);
            pnlFilters.Controls.Add(lblToDate);
            pnlFilters.Controls.Add(dtpTo);
            pnlFilters.Controls.Add(btnApplyFilters);
            pnlFilters.Controls.Add(btnRefresh);
            pnlFilters.Dock = DockStyle.Top;
            pnlFilters.Location = new Point(0, 160);
            pnlFilters.Name = "pnlFilters";
            pnlFilters.Padding = new Padding(12, 8, 12, 8);
            pnlFilters.Size = new Size(1150, 50);
            pnlFilters.TabIndex = 2;

            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSearch.Location = new Point(12, 14);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(59, 20);
            lblSearch.Text = "Search:";

            txtSearch.Font = new Font("Segoe UI", 9F);
            txtSearch.Location = new Point(75, 11);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Patient name, code, receipt #...";
            txtSearch.Size = new Size(230, 27);
            txtSearch.TabIndex = 0;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            lblMethodFilter.AutoSize = true;
            lblMethodFilter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMethodFilter.Location = new Point(320, 14);
            lblMethodFilter.Name = "lblMethodFilter";
            lblMethodFilter.Size = new Size(68, 20);
            lblMethodFilter.Text = "Method:";

            cmbMethodFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMethodFilter.Font = new Font("Segoe UI", 9F);
            cmbMethodFilter.FormattingEnabled = true;
            cmbMethodFilter.Items.AddRange(new object[] { "All Methods", "Cash", "Card", "BankTransfer" });
            cmbMethodFilter.Location = new Point(392, 11);
            cmbMethodFilter.Name = "cmbMethodFilter";
            cmbMethodFilter.Size = new Size(130, 28);
            cmbMethodFilter.TabIndex = 1;
            cmbMethodFilter.SelectedIndexChanged += CmbMethodFilter_SelectedIndexChanged;

            lblFromDate.AutoSize = true;
            lblFromDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFromDate.Location = new Point(540, 14);
            lblFromDate.Name = "lblFromDate";
            lblFromDate.Size = new Size(49, 20);
            lblFromDate.Text = "From:";

            dtpFrom.CustomFormat = "dd/MM/yyyy";
            dtpFrom.Font = new Font("Segoe UI", 9F);
            dtpFrom.Format = DateTimePickerFormat.Custom;
            dtpFrom.Location = new Point(593, 11);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(115, 27);
            dtpFrom.TabIndex = 2;

            lblToDate.AutoSize = true;
            lblToDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblToDate.Location = new Point(720, 14);
            lblToDate.Name = "lblToDate";
            lblToDate.Size = new Size(30, 20);
            lblToDate.Text = "To:";

            dtpTo.CustomFormat = "dd/MM/yyyy";
            dtpTo.Font = new Font("Segoe UI", 9F);
            dtpTo.Format = DateTimePickerFormat.Custom;
            dtpTo.Location = new Point(755, 11);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(115, 27);
            dtpTo.TabIndex = 3;

            btnApplyFilters.BackColor = Color.FromArgb(20, 80, 140);
            btnApplyFilters.FlatStyle = FlatStyle.Flat;
            btnApplyFilters.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnApplyFilters.ForeColor = Color.White;
            btnApplyFilters.Location = new Point(885, 9);
            btnApplyFilters.Name = "btnApplyFilters";
            btnApplyFilters.Size = new Size(100, 30);
            btnApplyFilters.TabIndex = 4;
            btnApplyFilters.Text = "Filter Dates";
            btnApplyFilters.UseVisualStyleBackColor = false;
            btnApplyFilters.Click += BtnApplyFilters_Click;

            btnRefresh.BackColor = Color.FromArgb(235, 245, 255);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.FromArgb(20, 80, 140);
            btnRefresh.Location = new Point(995, 9);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(90, 30);
            btnRefresh.TabIndex = 5;
            btnRefresh.Text = "🔄 Reset";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += BtnRefresh_Click;

            // ── pnlGrid ────────────────────────────────────────────────────────────
            pnlGrid.Controls.Add(dgvPayments);
            pnlGrid.Dock = DockStyle.Fill;
            pnlGrid.Location = new Point(0, 210);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Padding = new Padding(15);
            pnlGrid.Size = new Size(1150, 440);
            pnlGrid.TabIndex = 3;

            dgvPayments.AllowUserToAddRows = false;
            dgvPayments.AllowUserToDeleteRows = false;
            dgvPayments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPayments.BackgroundColor = Color.White;
            dgvPayments.BorderStyle = BorderStyle.Fixed3D;

            dgvHeaderStyle.BackColor = Color.FromArgb(27, 54, 93);
            dgvHeaderStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvHeaderStyle.ForeColor = Color.White;
            dgvPayments.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
            dgvPayments.ColumnHeadersHeight = 32;

            dgvDefaultStyle.Font = new Font("Segoe UI", 9F);
            dgvDefaultStyle.SelectionBackColor = Color.FromArgb(215, 235, 255);
            dgvDefaultStyle.SelectionForeColor = Color.Black;
            dgvPayments.DefaultCellStyle = dgvDefaultStyle;

            dgvPayments.Dock = DockStyle.Fill;
            dgvPayments.EnableHeadersVisualStyles = false;
            dgvPayments.MultiSelect = false;
            dgvPayments.ReadOnly = true;
            dgvPayments.RowHeadersVisible = false;
            dgvPayments.RowTemplate.Height = 28;
            dgvPayments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // ── pnlFooter ──────────────────────────────────────────────────────────
            pnlFooter.BackColor = Color.FromArgb(245, 248, 252);
            pnlFooter.BorderStyle = BorderStyle.FixedSingle;
            pnlFooter.Controls.Add(lblCount);
            pnlFooter.Controls.Add(btnPrintReceipt);
            pnlFooter.Controls.Add(btnClose);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 650);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(15, 8, 15, 8);
            pnlFooter.Size = new Size(1150, 50);
            pnlFooter.TabIndex = 4;

            lblCount.AutoSize = true;
            lblCount.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCount.ForeColor = Color.FromArgb(20, 60, 110);
            lblCount.Location = new Point(15, 14);
            lblCount.Text = "Showing 0 transactions";

            btnPrintReceipt.BackColor = Color.FromArgb(20, 80, 140);
            btnPrintReceipt.FlatStyle = FlatStyle.Flat;
            btnPrintReceipt.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPrintReceipt.ForeColor = Color.White;
            btnPrintReceipt.Location = new Point(850, 8);
            btnPrintReceipt.Name = "btnPrintReceipt";
            btnPrintReceipt.Size = new Size(180, 32);
            btnPrintReceipt.TabIndex = 0;
            btnPrintReceipt.Text = "📄 View / Print Receipt";
            btnPrintReceipt.UseVisualStyleBackColor = false;
            btnPrintReceipt.Click += BtnPrintReceipt_Click;

            btnClose.BackColor = Color.FromArgb(240, 240, 240);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 9.5F);
            btnClose.Location = new Point(1040, 8);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(95, 32);
            btnClose.TabIndex = 1;
            btnClose.Text = "✖️ Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += (s, e) => Close();

            // ── PaymentHistoryForm ─────────────────────────────────────────────────
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1150, 700);
            Controls.Add(pnlGrid);
            Controls.Add(pnlFooter);
            Controls.Add(pnlFilters);
            Controls.Add(pnlMetrics);
            Controls.Add(pnlHeader);
            MinimumSize = new Size(1000, 650);
            Name = "PaymentHistoryForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MediCare - Payment Transaction History";
            Load += PaymentHistoryForm_Load;

            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlMetrics.ResumeLayout(false);
            pnlCardTotal.ResumeLayout(false);
            pnlCardTotal.PerformLayout();
            pnlCardCash.ResumeLayout(false);
            pnlCardCash.PerformLayout();
            pnlCardCard.ResumeLayout(false);
            pnlCardCard.PerformLayout();
            pnlCardBank.ResumeLayout(false);
            pnlCardBank.PerformLayout();
            pnlFilters.ResumeLayout(false);
            pnlFilters.PerformLayout();
            pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPayments).EndInit();
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblHeaderTitle;
        private Label lblHeaderSubtitle;
        private Panel pnlMetrics;
        private Panel pnlCardTotal;
        private Label lblCardTotalText;
        private Label lblCardTotalVal;
        private Panel pnlCardCash;
        private Label lblCardCashText;
        private Label lblCardCashVal;
        private Panel pnlCardCard;
        private Label lblCardCardText;
        private Label lblCardCardVal;
        private Panel pnlCardBank;
        private Label lblCardBankText;
        private Label lblCardBankVal;
        private Panel pnlFilters;
        private Label lblSearch;
        private TextBox txtSearch;
        private Label lblMethodFilter;
        private ComboBox cmbMethodFilter;
        private Label lblFromDate;
        private DateTimePicker dtpFrom;
        private Label lblToDate;
        private DateTimePicker dtpTo;
        private Button btnApplyFilters;
        private Button btnRefresh;
        private Panel pnlGrid;
        private DataGridView dgvPayments;
        private Panel pnlFooter;
        private Label lblCount;
        private Button btnPrintReceipt;
        private Button btnClose;
    }
}
