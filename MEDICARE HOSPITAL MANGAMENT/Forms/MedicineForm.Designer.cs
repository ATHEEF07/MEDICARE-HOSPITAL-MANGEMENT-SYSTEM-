using System.Drawing;
using System.Windows.Forms;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    partial class MedicineForm
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

            pnlSearch = new Panel();
            lblSearch = new Label();
            txtSearch = new TextBox();
            lblCategoryFilter = new Label();
            cmbCategoryFilter = new ComboBox();
            chkLowStockOnly = new CheckBox();
            btnRefresh = new Button();

            pnlSidebar = new Panel();
            grpMedicine = new GroupBox();
            lblMedicineCode = new Label();
            txtMedicineCode = new TextBox();
            btnGenCode = new Button();
            lblMedicineName = new Label();
            txtMedicineName = new TextBox();
            lblCategory = new Label();
            cmbCategory = new ComboBox();
            lblUnit = new Label();
            cmbUnit = new ComboBox();
            lblUnitPrice = new Label();
            txtUnitPrice = new TextBox();
            lblStockQuantity = new Label();
            txtStockQuantity = new TextBox();
            lblReorderLevel = new Label();
            txtReorderLevel = new TextBox();
            lblExpiryDate = new Label();
            dtpExpiryDate = new DateTimePicker();
            chkIsActive = new CheckBox();
            btnSave = new Button();
            btnUpdate = new Button();
            btnAdjustStock = new Button();
            btnClear = new Button();

            pnlMain = new Panel();
            dgvMedicines = new DataGridView();
            pnlSummary = new Panel();
            lblTotalCount = new Label();
            lblLowStockCount = new Label();
            lblOutOfStockCount = new Label();

            pnlHeader.SuspendLayout();
            pnlSearch.SuspendLayout();
            pnlSidebar.SuspendLayout();
            grpMedicine.SuspendLayout();
            pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMedicines).BeginInit();
            pnlSummary.SuspendLayout();
            SuspendLayout();

            // ── pnlHeader ──────────────────────────────────────────────────────────
            pnlHeader.BackColor = Color.FromArgb(27, 54, 93);
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
            lblHeaderTitle.Size = new Size(420, 35);
            lblHeaderTitle.Text = "💊  Pharmacy Stock Management";

            lblHeaderSubtitle.AutoSize = true;
            lblHeaderSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblHeaderSubtitle.ForeColor = Color.FromArgb(200, 220, 240);
            lblHeaderSubtitle.Location = new Point(22, 45);
            lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            lblHeaderSubtitle.Size = new Size(480, 21);
            lblHeaderSubtitle.Text = "Manage medications inventory, track unit prices, and monitor low-stock thresholds";

            // ── pnlSearch ──────────────────────────────────────────────────────────
            pnlSearch.BackColor = Color.FromArgb(241, 245, 249);
            pnlSearch.Controls.Add(lblSearch);
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Controls.Add(lblCategoryFilter);
            pnlSearch.Controls.Add(cmbCategoryFilter);
            pnlSearch.Controls.Add(chkLowStockOnly);
            pnlSearch.Controls.Add(btnRefresh);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(0, 75);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Padding = new Padding(15, 10, 15, 10);
            pnlSearch.Size = new Size(1180, 50);
            pnlSearch.TabIndex = 1;

            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSearch.Location = new Point(15, 15);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(59, 20);
            lblSearch.Text = "Search:";

            txtSearch.Font = new Font("Segoe UI", 9.5F);
            txtSearch.Location = new Point(78, 11);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search by code, medicine name, or category...";
            txtSearch.Size = new Size(320, 29);
            txtSearch.TabIndex = 0;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            lblCategoryFilter.AutoSize = true;
            lblCategoryFilter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCategoryFilter.Location = new Point(415, 15);
            lblCategoryFilter.Name = "lblCategoryFilter";
            lblCategoryFilter.Size = new Size(77, 20);
            lblCategoryFilter.Text = "Category:";

            cmbCategoryFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoryFilter.Font = new Font("Segoe UI", 9.5F);
            cmbCategoryFilter.FormattingEnabled = true;
            cmbCategoryFilter.Location = new Point(496, 11);
            cmbCategoryFilter.Name = "cmbCategoryFilter";
            cmbCategoryFilter.Size = new Size(180, 29);
            cmbCategoryFilter.TabIndex = 1;
            cmbCategoryFilter.SelectedIndexChanged += CmbCategoryFilter_SelectedIndexChanged;

            chkLowStockOnly.AutoSize = true;
            chkLowStockOnly.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            chkLowStockOnly.ForeColor = Color.FromArgb(180, 40, 20);
            chkLowStockOnly.Location = new Point(695, 14);
            chkLowStockOnly.Name = "chkLowStockOnly";
            chkLowStockOnly.Size = new Size(162, 24);
            chkLowStockOnly.TabIndex = 2;
            chkLowStockOnly.Text = "⚠️ Low Stock Only";
            chkLowStockOnly.UseVisualStyleBackColor = true;
            chkLowStockOnly.CheckedChanged += ChkLowStockOnly_CheckedChanged;

            btnRefresh.BackColor = Color.FromArgb(230, 240, 255);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.FromArgb(20, 60, 120);
            btnRefresh.Location = new Point(875, 10);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(95, 30);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "🔄 Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += BtnRefresh_Click;

            // ── pnlSidebar ─────────────────────────────────────────────────────────
            pnlSidebar.BackColor = Color.FromArgb(248, 249, 250);
            pnlSidebar.Controls.Add(grpMedicine);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 125);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Padding = new Padding(12);
            pnlSidebar.Size = new Size(380, 580);
            pnlSidebar.TabIndex = 2;

            grpMedicine.Controls.Add(lblMedicineCode);
            grpMedicine.Controls.Add(txtMedicineCode);
            grpMedicine.Controls.Add(btnGenCode);
            grpMedicine.Controls.Add(lblMedicineName);
            grpMedicine.Controls.Add(txtMedicineName);
            grpMedicine.Controls.Add(lblCategory);
            grpMedicine.Controls.Add(cmbCategory);
            grpMedicine.Controls.Add(lblUnit);
            grpMedicine.Controls.Add(cmbUnit);
            grpMedicine.Controls.Add(lblUnitPrice);
            grpMedicine.Controls.Add(txtUnitPrice);
            grpMedicine.Controls.Add(lblStockQuantity);
            grpMedicine.Controls.Add(txtStockQuantity);
            grpMedicine.Controls.Add(lblReorderLevel);
            grpMedicine.Controls.Add(txtReorderLevel);
            grpMedicine.Controls.Add(lblExpiryDate);
            grpMedicine.Controls.Add(dtpExpiryDate);
            grpMedicine.Controls.Add(chkIsActive);
            grpMedicine.Controls.Add(btnSave);
            grpMedicine.Controls.Add(btnUpdate);
            grpMedicine.Controls.Add(btnAdjustStock);
            grpMedicine.Controls.Add(btnClear);
            grpMedicine.Dock = DockStyle.Fill;
            grpMedicine.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            grpMedicine.Location = new Point(12, 12);
            grpMedicine.Name = "grpMedicine";
            grpMedicine.Size = new Size(356, 556);
            grpMedicine.TabIndex = 0;
            grpMedicine.TabStop = false;
            grpMedicine.Text = "Medicine Details";

            int y = 26;
            // Code
            lblMedicineCode.AutoSize = true;
            lblMedicineCode.Font = new Font("Segoe UI", 8.5F);
            lblMedicineCode.Location = new Point(14, y);
            lblMedicineCode.Text = "Medicine Code *";

            txtMedicineCode.Font = new Font("Segoe UI", 9F);
            txtMedicineCode.Location = new Point(14, y + 18);
            txtMedicineCode.Size = new Size(240, 27);

            btnGenCode.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnGenCode.Location = new Point(260, y + 17);
            btnGenCode.Size = new Size(82, 28);
            btnGenCode.Text = "Auto";
            btnGenCode.Click += BtnGenCode_Click;

            // Name
            y += 50;
            lblMedicineName.AutoSize = true;
            lblMedicineName.Font = new Font("Segoe UI", 8.5F);
            lblMedicineName.Location = new Point(14, y);
            lblMedicineName.Text = "Medicine Name *";

            txtMedicineName.Font = new Font("Segoe UI", 9F);
            txtMedicineName.Location = new Point(14, y + 18);
            txtMedicineName.Size = new Size(328, 27);

            // Category & Unit
            y += 50;
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Segoe UI", 8.5F);
            lblCategory.Location = new Point(14, y);
            lblCategory.Text = "Category";

            cmbCategory.Font = new Font("Segoe UI", 9F);
            cmbCategory.Location = new Point(14, y + 18);
            cmbCategory.Size = new Size(160, 28);

            lblUnit.AutoSize = true;
            lblUnit.Font = new Font("Segoe UI", 8.5F);
            lblUnit.Location = new Point(182, y);
            lblUnit.Text = "Unit *";

            cmbUnit.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUnit.Font = new Font("Segoe UI", 9F);
            cmbUnit.Location = new Point(182, y + 18);
            cmbUnit.Size = new Size(160, 28);
            cmbUnit.Items.AddRange(new object[] { "Tablet", "Capsule", "Syrup", "Injection", "Inhaler", "Ointment", "Vial", "Sachet", "Bottle" });

            // Unit Price & Stock
            y += 50;
            lblUnitPrice.AutoSize = true;
            lblUnitPrice.Font = new Font("Segoe UI", 8.5F);
            lblUnitPrice.Location = new Point(14, y);
            lblUnitPrice.Text = "Unit Price (Rs.) *";

            txtUnitPrice.Font = new Font("Segoe UI", 9F);
            txtUnitPrice.Location = new Point(14, y + 18);
            txtUnitPrice.Size = new Size(160, 27);
            txtUnitPrice.Text = "0.00";

            lblStockQuantity.AutoSize = true;
            lblStockQuantity.Font = new Font("Segoe UI", 8.5F);
            lblStockQuantity.Location = new Point(182, y);
            lblStockQuantity.Text = "Stock Quantity *";

            txtStockQuantity.Font = new Font("Segoe UI", 9F);
            txtStockQuantity.Location = new Point(182, y + 18);
            txtStockQuantity.Size = new Size(160, 27);
            txtStockQuantity.Text = "0";

            // Reorder Level & Expiry
            y += 50;
            lblReorderLevel.AutoSize = true;
            lblReorderLevel.Font = new Font("Segoe UI", 8.5F);
            lblReorderLevel.Location = new Point(14, y);
            lblReorderLevel.Text = "Reorder Level *";

            txtReorderLevel.Font = new Font("Segoe UI", 9F);
            txtReorderLevel.Location = new Point(14, y + 18);
            txtReorderLevel.Size = new Size(160, 27);
            txtReorderLevel.Text = "10";

            lblExpiryDate.AutoSize = true;
            lblExpiryDate.Font = new Font("Segoe UI", 8.5F);
            lblExpiryDate.Location = new Point(182, y);
            lblExpiryDate.Text = "Expiry Date";

            dtpExpiryDate.Font = new Font("Segoe UI", 9F);
            dtpExpiryDate.Format = DateTimePickerFormat.Short;
            dtpExpiryDate.Location = new Point(182, y + 18);
            dtpExpiryDate.Size = new Size(160, 27);

            // Active Checkbox
            y += 50;
            chkIsActive.AutoSize = true;
            chkIsActive.Checked = true;
            chkIsActive.Font = new Font("Segoe UI", 9F);
            chkIsActive.Location = new Point(14, y);
            chkIsActive.Text = "Active in Hospital Formulary";

            // Action Buttons
            y += 35;
            btnSave.BackColor = Color.FromArgb(20, 80, 140);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(14, y);
            btnSave.Size = new Size(160, 34);
            btnSave.Text = "💾 Save New";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += BtnSave_Click;

            btnUpdate.BackColor = Color.FromArgb(40, 120, 70);
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Location = new Point(182, y);
            btnUpdate.Size = new Size(160, 34);
            btnUpdate.Text = "✏️ Update";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += BtnUpdate_Click;

            y += 42;
            btnAdjustStock.BackColor = Color.FromArgb(235, 243, 250);
            btnAdjustStock.FlatStyle = FlatStyle.Flat;
            btnAdjustStock.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAdjustStock.ForeColor = Color.FromArgb(20, 80, 140);
            btnAdjustStock.Location = new Point(14, y);
            btnAdjustStock.Size = new Size(160, 34);
            btnAdjustStock.Text = "📦 Adjust Stock";
            btnAdjustStock.UseVisualStyleBackColor = false;
            btnAdjustStock.Click += BtnAdjustStock_Click;

            btnClear.BackColor = Color.FromArgb(240, 240, 240);
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 9F);
            btnClear.Location = new Point(182, y);
            btnClear.Size = new Size(160, 34);
            btnClear.Text = "🧹 Clear Form";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += BtnClear_Click;

            // ── pnlMain (Data Grid) ────────────────────────────────────────────────
            pnlMain.Controls.Add(dgvMedicines);
            pnlMain.Controls.Add(pnlSummary);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(380, 125);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(12);
            pnlMain.Size = new Size(800, 580);
            pnlMain.TabIndex = 3;

            dgvMedicines.AllowUserToAddRows = false;
            dgvMedicines.AllowUserToDeleteRows = false;
            dgvMedicines.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMedicines.BackgroundColor = Color.White;
            dgvMedicines.BorderStyle = BorderStyle.Fixed3D;

            dgvHeaderStyle.BackColor = Color.FromArgb(27, 54, 93);
            dgvHeaderStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvHeaderStyle.ForeColor = Color.White;
            dgvHeaderStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvMedicines.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
            dgvMedicines.ColumnHeadersHeight = 35;

            dgvDefaultStyle.Font = new Font("Segoe UI", 9F);
            dgvDefaultStyle.SelectionBackColor = Color.FromArgb(210, 230, 250);
            dgvDefaultStyle.SelectionForeColor = Color.Black;
            dgvMedicines.DefaultCellStyle = dgvDefaultStyle;

            dgvMedicines.Dock = DockStyle.Fill;
            dgvMedicines.EnableHeadersVisualStyles = false;
            dgvMedicines.MultiSelect = false;
            dgvMedicines.ReadOnly = true;
            dgvMedicines.RowHeadersVisible = false;
            dgvMedicines.RowTemplate.Height = 28;
            dgvMedicines.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMedicines.SelectionChanged += DgvMedicines_SelectionChanged;
            dgvMedicines.CellFormatting += DgvMedicines_CellFormatting;

            // ── pnlSummary ─────────────────────────────────────────────────────────
            pnlSummary.BackColor = Color.FromArgb(245, 248, 252);
            pnlSummary.BorderStyle = BorderStyle.FixedSingle;
            pnlSummary.Controls.Add(lblTotalCount);
            pnlSummary.Controls.Add(lblLowStockCount);
            pnlSummary.Controls.Add(lblOutOfStockCount);
            pnlSummary.Dock = DockStyle.Bottom;
            pnlSummary.Location = new Point(12, 532);
            pnlSummary.Name = "pnlSummary";
            pnlSummary.Size = new Size(776, 36);
            pnlSummary.TabIndex = 1;

            lblTotalCount.AutoSize = true;
            lblTotalCount.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalCount.ForeColor = Color.FromArgb(20, 60, 100);
            lblTotalCount.Location = new Point(15, 8);
            lblTotalCount.Text = "Total Medications: 0";

            lblLowStockCount.AutoSize = true;
            lblLowStockCount.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLowStockCount.ForeColor = Color.FromArgb(180, 100, 0);
            lblLowStockCount.Location = new Point(220, 8);
            lblLowStockCount.Text = "Low Stock Alerts: 0";

            lblOutOfStockCount.AutoSize = true;
            lblOutOfStockCount.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblOutOfStockCount.ForeColor = Color.FromArgb(180, 20, 20);
            lblOutOfStockCount.Location = new Point(420, 8);
            lblOutOfStockCount.Text = "Out of Stock: 0";

            // ── MedicineForm ───────────────────────────────────────────────────────
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1180, 705);
            Controls.Add(pnlMain);
            Controls.Add(pnlSidebar);
            Controls.Add(pnlSearch);
            Controls.Add(pnlHeader);
            MinimumSize = new Size(1000, 650);
            Name = "MedicineForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MediCare - Pharmacy Stock Management";
            Load += MedicineForm_Load;

            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            pnlSidebar.ResumeLayout(false);
            grpMedicine.ResumeLayout(false);
            grpMedicine.PerformLayout();
            pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMedicines).EndInit();
            pnlSummary.ResumeLayout(false);
            pnlSummary.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblHeaderTitle;
        private Label lblHeaderSubtitle;
        private Panel pnlSearch;
        private Label lblSearch;
        private TextBox txtSearch;
        private Label lblCategoryFilter;
        private ComboBox cmbCategoryFilter;
        private CheckBox chkLowStockOnly;
        private Button btnRefresh;
        private Panel pnlSidebar;
        private GroupBox grpMedicine;
        private Label lblMedicineCode;
        private TextBox txtMedicineCode;
        private Button btnGenCode;
        private Label lblMedicineName;
        private TextBox txtMedicineName;
        private Label lblCategory;
        private ComboBox cmbCategory;
        private Label lblUnit;
        private ComboBox cmbUnit;
        private Label lblUnitPrice;
        private TextBox txtUnitPrice;
        private Label lblStockQuantity;
        private TextBox txtStockQuantity;
        private Label lblReorderLevel;
        private TextBox txtReorderLevel;
        private Label lblExpiryDate;
        private DateTimePicker dtpExpiryDate;
        private CheckBox chkIsActive;
        private Button btnSave;
        private Button btnUpdate;
        private Button btnAdjustStock;
        private Button btnClear;
        private Panel pnlMain;
        private DataGridView dgvMedicines;
        private Panel pnlSummary;
        private Label lblTotalCount;
        private Label lblLowStockCount;
        private Label lblOutOfStockCount;
    }
}
