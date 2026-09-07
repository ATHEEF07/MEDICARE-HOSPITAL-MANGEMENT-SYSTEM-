using System.Drawing;
using System.Windows.Forms;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    partial class PharmacyDispenseForm
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

            pnlQueue = new Panel();
            pnlQueueTop = new Panel();
            lblQueueTitle = new Label();
            txtSearch = new TextBox();
            cmbStatusFilter = new ComboBox();
            btnRefreshQueue = new Button();
            dgvQueue = new DataGridView();

            pnlWorkbench = new Panel();
            pnlDetailsCard = new Panel();
            lblRxNumber = new Label();
            lblPatientName = new Label();
            lblPatientCode = new Label();
            lblDoctorName = new Label();
            lblDate = new Label();
            lblNotes = new Label();
            pnlGridContainer = new Panel();
            dgvDispenseItems = new DataGridView();
            pnlActionBar = new Panel();
            lblTotalValue = new Label();
            lblStockStatusAlert = new Label();
            btnDispense = new Button();
            btnPrintSlip = new Button();

            pnlHeader.SuspendLayout();
            pnlQueue.SuspendLayout();
            pnlQueueTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvQueue).BeginInit();
            pnlWorkbench.SuspendLayout();
            pnlDetailsCard.SuspendLayout();
            pnlGridContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDispenseItems).BeginInit();
            pnlActionBar.SuspendLayout();
            SuspendLayout();

            // ── pnlHeader ──────────────────────────────────────────────────────────
            pnlHeader.BackColor = Color.FromArgb(20, 80, 140);
            pnlHeader.Controls.Add(lblHeaderSubtitle);
            pnlHeader.Controls.Add(lblHeaderTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(20, 12, 20, 12);
            pnlHeader.Size = new Size(1180, 75);
            pnlHeader.TabIndex = 0;

            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(20, 12);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(460, 35);
            lblHeaderTitle.Text = "💊  Pharmacy Dispensing Workbench";

            lblHeaderSubtitle.AutoSize = true;
            lblHeaderSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblHeaderSubtitle.ForeColor = Color.FromArgb(200, 225, 255);
            lblHeaderSubtitle.Location = new Point(22, 45);
            lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            lblHeaderSubtitle.Size = new Size(580, 21);
            lblHeaderSubtitle.Text = "Review pending prescriptions, verify pharmaceutical inventory, and execute atomic dispensing";

            // ── pnlQueue (Left sidebar) ────────────────────────────────────────────
            pnlQueue.BackColor = Color.FromArgb(245, 248, 252);
            pnlQueue.BorderStyle = BorderStyle.FixedSingle;
            pnlQueue.Controls.Add(dgvQueue);
            pnlQueue.Controls.Add(pnlQueueTop);
            pnlQueue.Dock = DockStyle.Left;
            pnlQueue.Location = new Point(0, 75);
            pnlQueue.Name = "pnlQueue";
            pnlQueue.Size = new Size(380, 630);
            pnlQueue.TabIndex = 1;

            // pnlQueueTop
            pnlQueueTop.Controls.Add(lblQueueTitle);
            pnlQueueTop.Controls.Add(txtSearch);
            pnlQueueTop.Controls.Add(cmbStatusFilter);
            pnlQueueTop.Controls.Add(btnRefreshQueue);
            pnlQueueTop.Dock = DockStyle.Top;
            pnlQueueTop.Location = new Point(0, 0);
            pnlQueueTop.Name = "pnlQueueTop";
            pnlQueueTop.Padding = new Padding(10);
            pnlQueueTop.Size = new Size(378, 125);
            pnlQueueTop.TabIndex = 0;

            lblQueueTitle.AutoSize = true;
            lblQueueTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblQueueTitle.ForeColor = Color.FromArgb(20, 60, 120);
            lblQueueTitle.Location = new Point(10, 8);
            lblQueueTitle.Name = "lblQueueTitle";
            lblQueueTitle.Size = new Size(185, 23);
            lblQueueTitle.Text = "Prescription Queue";

            txtSearch.Font = new Font("Segoe UI", 9F);
            txtSearch.Location = new Point(10, 36);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search by patient name, code, or Rx #...";
            txtSearch.Size = new Size(355, 27);
            txtSearch.TabIndex = 0;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            cmbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatusFilter.Font = new Font("Segoe UI", 9F);
            cmbStatusFilter.FormattingEnabled = true;
            cmbStatusFilter.Items.AddRange(new object[] { "Active (Pending)", "Dispensed", "All Prescriptions" });
            cmbStatusFilter.Location = new Point(10, 74);
            cmbStatusFilter.Name = "cmbStatusFilter";
            cmbStatusFilter.Size = new Size(220, 28);
            cmbStatusFilter.TabIndex = 1;
            cmbStatusFilter.SelectedIndexChanged += CmbStatusFilter_SelectedIndexChanged;

            btnRefreshQueue.BackColor = Color.FromArgb(230, 240, 255);
            btnRefreshQueue.FlatStyle = FlatStyle.Flat;
            btnRefreshQueue.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnRefreshQueue.ForeColor = Color.FromArgb(20, 60, 120);
            btnRefreshQueue.Location = new Point(240, 73);
            btnRefreshQueue.Name = "btnRefreshQueue";
            btnRefreshQueue.Size = new Size(125, 30);
            btnRefreshQueue.TabIndex = 2;
            btnRefreshQueue.Text = "🔄 Refresh";
            btnRefreshQueue.UseVisualStyleBackColor = false;
            btnRefreshQueue.Click += BtnRefreshQueue_Click;

            // dgvQueue
            dgvQueue.AllowUserToAddRows = false;
            dgvQueue.AllowUserToDeleteRows = false;
            dgvQueue.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvQueue.BackgroundColor = Color.White;
            dgvQueue.BorderStyle = BorderStyle.None;

            dgvHeaderStyle1.BackColor = Color.FromArgb(30, 60, 100);
            dgvHeaderStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvHeaderStyle1.ForeColor = Color.White;
            dgvQueue.ColumnHeadersDefaultCellStyle = dgvHeaderStyle1;
            dgvQueue.ColumnHeadersHeight = 30;

            dgvDefaultStyle1.Font = new Font("Segoe UI", 9F);
            dgvDefaultStyle1.SelectionBackColor = Color.FromArgb(215, 235, 255);
            dgvDefaultStyle1.SelectionForeColor = Color.Black;
            dgvQueue.DefaultCellStyle = dgvDefaultStyle1;

            dgvQueue.Dock = DockStyle.Fill;
            dgvQueue.EnableHeadersVisualStyles = false;
            dgvQueue.MultiSelect = false;
            dgvQueue.ReadOnly = true;
            dgvQueue.RowHeadersVisible = false;
            dgvQueue.RowTemplate.Height = 28;
            dgvQueue.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvQueue.SelectionChanged += DgvQueue_SelectionChanged;

            // ── pnlWorkbench (Right content) ───────────────────────────────────────
            pnlWorkbench.Controls.Add(pnlGridContainer);
            pnlWorkbench.Controls.Add(pnlActionBar);
            pnlWorkbench.Controls.Add(pnlDetailsCard);
            pnlWorkbench.Dock = DockStyle.Fill;
            pnlWorkbench.Location = new Point(380, 75);
            pnlWorkbench.Name = "pnlWorkbench";
            pnlWorkbench.Padding = new Padding(12);
            pnlWorkbench.Size = new Size(800, 630);
            pnlWorkbench.TabIndex = 2;

            // pnlDetailsCard
            pnlDetailsCard.BackColor = Color.FromArgb(240, 246, 255);
            pnlDetailsCard.BorderStyle = BorderStyle.FixedSingle;
            pnlDetailsCard.Controls.Add(lblRxNumber);
            pnlDetailsCard.Controls.Add(lblPatientName);
            pnlDetailsCard.Controls.Add(lblPatientCode);
            pnlDetailsCard.Controls.Add(lblDoctorName);
            pnlDetailsCard.Controls.Add(lblDate);
            pnlDetailsCard.Controls.Add(lblNotes);
            pnlDetailsCard.Dock = DockStyle.Top;
            pnlDetailsCard.Location = new Point(12, 12);
            pnlDetailsCard.Name = "pnlDetailsCard";
            pnlDetailsCard.Padding = new Padding(15, 10, 15, 10);
            pnlDetailsCard.Size = new Size(776, 95);
            pnlDetailsCard.TabIndex = 0;

            lblRxNumber.AutoSize = true;
            lblRxNumber.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblRxNumber.ForeColor = Color.FromArgb(20, 80, 140);
            lblRxNumber.Location = new Point(12, 8);
            lblRxNumber.Text = "Prescription: Select a prescription...";

            lblDate.AutoSize = true;
            lblDate.Font = new Font("Segoe UI", 9F);
            lblDate.Location = new Point(500, 10);
            lblDate.Text = "Date: —";

            lblPatientName.AutoSize = true;
            lblPatientName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPatientName.Location = new Point(12, 38);
            lblPatientName.Text = "Patient: —";

            lblPatientCode.AutoSize = true;
            lblPatientCode.Font = new Font("Segoe UI", 9F);
            lblPatientCode.Location = new Point(280, 39);
            lblPatientCode.Text = "Code: —";

            lblDoctorName.AutoSize = true;
            lblDoctorName.Font = new Font("Segoe UI", 9F);
            lblDoctorName.Location = new Point(500, 39);
            lblDoctorName.Text = "Prescribed by: —";

            lblNotes.AutoSize = true;
            lblNotes.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            lblNotes.ForeColor = Color.FromArgb(80, 80, 80);
            lblNotes.Location = new Point(12, 65);
            lblNotes.Text = "Doctor Notes: —";

            // pnlGridContainer
            pnlGridContainer.Controls.Add(dgvDispenseItems);
            pnlGridContainer.Dock = DockStyle.Fill;
            pnlGridContainer.Location = new Point(12, 107);
            pnlGridContainer.Name = "pnlGridContainer";
            pnlGridContainer.Padding = new Padding(0, 10, 0, 10);
            pnlGridContainer.Size = new Size(776, 433);
            pnlGridContainer.TabIndex = 1;

            // dgvDispenseItems
            dgvDispenseItems.AllowUserToAddRows = false;
            dgvDispenseItems.AllowUserToDeleteRows = false;
            dgvDispenseItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDispenseItems.BackgroundColor = Color.White;
            dgvDispenseItems.BorderStyle = BorderStyle.Fixed3D;

            dgvHeaderStyle2.BackColor = Color.FromArgb(27, 54, 93);
            dgvHeaderStyle2.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvHeaderStyle2.ForeColor = Color.White;
            dgvDispenseItems.ColumnHeadersDefaultCellStyle = dgvHeaderStyle2;
            dgvDispenseItems.ColumnHeadersHeight = 32;

            dgvDefaultStyle2.Font = new Font("Segoe UI", 9F);
            dgvDefaultStyle2.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dgvDefaultStyle2.SelectionForeColor = Color.Black;
            dgvDispenseItems.DefaultCellStyle = dgvDefaultStyle2;

            dgvDispenseItems.Dock = DockStyle.Fill;
            dgvDispenseItems.EnableHeadersVisualStyles = false;
            dgvDispenseItems.MultiSelect = false;
            dgvDispenseItems.ReadOnly = true;
            dgvDispenseItems.RowHeadersVisible = false;
            dgvDispenseItems.RowTemplate.Height = 30;
            dgvDispenseItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDispenseItems.CellFormatting += DgvDispenseItems_CellFormatting;

            // pnlActionBar
            pnlActionBar.BackColor = Color.FromArgb(245, 248, 252);
            pnlActionBar.BorderStyle = BorderStyle.FixedSingle;
            pnlActionBar.Controls.Add(lblTotalValue);
            pnlActionBar.Controls.Add(lblStockStatusAlert);
            pnlActionBar.Controls.Add(btnDispense);
            pnlActionBar.Controls.Add(btnPrintSlip);
            pnlActionBar.Dock = DockStyle.Bottom;
            pnlActionBar.Location = new Point(12, 540);
            pnlActionBar.Name = "pnlActionBar";
            pnlActionBar.Padding = new Padding(15, 10, 15, 10);
            pnlActionBar.Size = new Size(776, 78);
            pnlActionBar.TabIndex = 2;

            lblStockStatusAlert.AutoSize = true;
            lblStockStatusAlert.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblStockStatusAlert.ForeColor = Color.FromArgb(20, 120, 50);
            lblStockStatusAlert.Location = new Point(15, 12);
            lblStockStatusAlert.Text = "Stock Status: Ready to Dispense";

            lblTotalValue.AutoSize = true;
            lblTotalValue.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotalValue.ForeColor = Color.FromArgb(20, 60, 120);
            lblTotalValue.Location = new Point(15, 40);
            lblTotalValue.Text = "Prescription Total: Rs. 0.00";

            btnDispense.BackColor = Color.FromArgb(34, 139, 34);
            btnDispense.FlatStyle = FlatStyle.Flat;
            btnDispense.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnDispense.ForeColor = Color.White;
            btnDispense.Location = new Point(480, 15);
            btnDispense.Name = "btnDispense";
            btnDispense.Size = new Size(280, 48);
            btnDispense.TabIndex = 0;
            btnDispense.Text = "✅  Dispense Medications";
            btnDispense.UseVisualStyleBackColor = false;
            btnDispense.Click += BtnDispense_Click;

            btnPrintSlip.BackColor = Color.FromArgb(235, 245, 255);
            btnPrintSlip.FlatStyle = FlatStyle.Flat;
            btnPrintSlip.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPrintSlip.ForeColor = Color.FromArgb(20, 80, 140);
            btnPrintSlip.Location = new Point(330, 20);
            btnPrintSlip.Name = "btnPrintSlip";
            btnPrintSlip.Size = new Size(140, 38);
            btnPrintSlip.TabIndex = 1;
            btnPrintSlip.Text = "📄 View Slip";
            btnPrintSlip.UseVisualStyleBackColor = false;
            btnPrintSlip.Click += BtnPrintSlip_Click;

            // ── PharmacyDispenseForm ───────────────────────────────────────────────
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1180, 705);
            Controls.Add(pnlWorkbench);
            Controls.Add(pnlQueue);
            Controls.Add(pnlHeader);
            MinimumSize = new Size(1000, 650);
            Name = "PharmacyDispenseForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MediCare - Pharmacy Dispensing Workbench";
            Load += PharmacyDispenseForm_Load;

            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlQueue.ResumeLayout(false);
            pnlQueueTop.ResumeLayout(false);
            pnlQueueTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvQueue).EndInit();
            pnlWorkbench.ResumeLayout(false);
            pnlDetailsCard.ResumeLayout(false);
            pnlDetailsCard.PerformLayout();
            pnlGridContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDispenseItems).EndInit();
            pnlActionBar.ResumeLayout(false);
            pnlActionBar.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblHeaderTitle;
        private Label lblHeaderSubtitle;
        private Panel pnlQueue;
        private Panel pnlQueueTop;
        private Label lblQueueTitle;
        private TextBox txtSearch;
        private ComboBox cmbStatusFilter;
        private Button btnRefreshQueue;
        private DataGridView dgvQueue;
        private Panel pnlWorkbench;
        private Panel pnlDetailsCard;
        private Label lblRxNumber;
        private Label lblPatientName;
        private Label lblPatientCode;
        private Label lblDoctorName;
        private Label lblDate;
        private Label lblNotes;
        private Panel pnlGridContainer;
        private DataGridView dgvDispenseItems;
        private Panel pnlActionBar;
        private Label lblStockStatusAlert;
        private Label lblTotalValue;
        private Button btnDispense;
        private Button btnPrintSlip;
    }
}
