namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    partial class ReportsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeaderSubtitle = new System.Windows.Forms.Label();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.pnlFilterBox = new System.Windows.Forms.Panel();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.btnExportCsv = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.chkOption = new System.Windows.Forms.CheckBox();
            this.cmbFilter2 = new System.Windows.Forms.ComboBox();
            this.lblFilter2 = new System.Windows.Forms.Label();
            this.cmbFilter1 = new System.Windows.Forms.ComboBox();
            this.lblFilter1 = new System.Windows.Forms.Label();
            this.btnPresetAll = new System.Windows.Forms.Button();
            this.btnPresetMonth = new System.Windows.Forms.Button();
            this.btnPresetWeek = new System.Windows.Forms.Button();
            this.btnPresetToday = new System.Windows.Forms.Button();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblFrom = new System.Windows.Forms.Label();
            this.cmbReportType = new System.Windows.Forms.ComboBox();
            this.lblReportType = new System.Windows.Forms.Label();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblSummaryMetric2 = new System.Windows.Forms.Label();
            this.lblSummaryMetric1 = new System.Windows.Forms.Label();
            this.lblRecordCount = new System.Windows.Forms.Label();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.pnlHeader.SuspendLayout();
            this.pnlFilterBox.SuspendLayout();
            this.pnlSummary.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(49)))), ((int)(((byte)(83)))));
            this.pnlHeader.Controls.Add(this.lblHeaderSubtitle);
            this.pnlHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1084, 65);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblHeaderSubtitle
            // 
            this.lblHeaderSubtitle.AutoSize = true;
            this.lblHeaderSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHeaderSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.lblHeaderSubtitle.Location = new System.Drawing.Point(20, 36);
            this.lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            this.lblHeaderSubtitle.Size = new System.Drawing.Size(437, 15);
            this.lblHeaderSubtitle.TabIndex = 1;
            this.lblHeaderSubtitle.Text = "Multi-criteria analytics, clinical workload, inventory valuation, and revenue export";
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(18, 9);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(326, 25);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "📊 Hospital Reports & Analytical Hub";
            // 
            // pnlFilterBox
            // 
            this.pnlFilterBox.BackColor = System.Drawing.Color.White;
            this.pnlFilterBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFilterBox.Controls.Add(this.btnPrintPreview);
            this.pnlFilterBox.Controls.Add(this.btnExportCsv);
            this.pnlFilterBox.Controls.Add(this.btnGenerate);
            this.pnlFilterBox.Controls.Add(this.chkOption);
            this.pnlFilterBox.Controls.Add(this.cmbFilter2);
            this.pnlFilterBox.Controls.Add(this.lblFilter2);
            this.pnlFilterBox.Controls.Add(this.cmbFilter1);
            this.pnlFilterBox.Controls.Add(this.lblFilter1);
            this.pnlFilterBox.Controls.Add(this.btnPresetAll);
            this.pnlFilterBox.Controls.Add(this.btnPresetMonth);
            this.pnlFilterBox.Controls.Add(this.btnPresetWeek);
            this.pnlFilterBox.Controls.Add(this.btnPresetToday);
            this.pnlFilterBox.Controls.Add(this.dtpTo);
            this.pnlFilterBox.Controls.Add(this.lblTo);
            this.pnlFilterBox.Controls.Add(this.dtpFrom);
            this.pnlFilterBox.Controls.Add(this.lblFrom);
            this.pnlFilterBox.Controls.Add(this.cmbReportType);
            this.pnlFilterBox.Controls.Add(this.lblReportType);
            this.pnlFilterBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilterBox.Location = new System.Drawing.Point(0, 65);
            this.pnlFilterBox.Name = "pnlFilterBox";
            this.pnlFilterBox.Size = new System.Drawing.Size(1084, 110);
            this.pnlFilterBox.TabIndex = 1;
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(148)))), ((int)(((byte)(136)))));
            this.btnPrintPreview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintPreview.FlatAppearance.BorderSize = 0;
            this.btnPrintPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintPreview.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnPrintPreview.ForeColor = System.Drawing.Color.White;
            this.btnPrintPreview.Location = new System.Drawing.Point(920, 62);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(140, 36);
            this.btnPrintPreview.TabIndex = 17;
            this.btnPrintPreview.Text = "🖨️ Print Preview";
            this.btnPrintPreview.UseVisualStyleBackColor = false;
            this.btnPrintPreview.Click += new System.EventHandler(this.BtnPrintPreview_Click);
            // 
            // btnExportCsv
            // 
            this.btnExportCsv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnExportCsv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportCsv.FlatAppearance.BorderSize = 0;
            this.btnExportCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportCsv.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnExportCsv.ForeColor = System.Drawing.Color.White;
            this.btnExportCsv.Location = new System.Drawing.Point(770, 62);
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(140, 36);
            this.btnExportCsv.TabIndex = 16;
            this.btnExportCsv.Text = "📄 Export CSV";
            this.btnExportCsv.UseVisualStyleBackColor = false;
            this.btnExportCsv.Click += new System.EventHandler(this.BtnExportCsv_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(119)))), ((int)(((byte)(242)))));
            this.btnGenerate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerate.FlatAppearance.BorderSize = 0;
            this.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerate.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnGenerate.ForeColor = System.Drawing.Color.White;
            this.btnGenerate.Location = new System.Drawing.Point(620, 62);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(140, 36);
            this.btnGenerate.TabIndex = 15;
            this.btnGenerate.Text = "🔍 Generate";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.BtnGenerate_Click);
            // 
            // chkOption
            // 
            this.chkOption.AutoSize = true;
            this.chkOption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkOption.Location = new System.Drawing.Point(440, 71);
            this.chkOption.Name = "chkOption";
            this.chkOption.Size = new System.Drawing.Size(161, 19);
            this.chkOption.TabIndex = 14;
            this.chkOption.Text = "Below Reorder Level Only";
            this.chkOption.UseVisualStyleBackColor = true;
            this.chkOption.Visible = false;
            // 
            // cmbFilter2
            // 
            this.cmbFilter2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilter2.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbFilter2.FormattingEnabled = true;
            this.cmbFilter2.Location = new System.Drawing.Point(270, 69);
            this.cmbFilter2.Name = "cmbFilter2";
            this.cmbFilter2.Size = new System.Drawing.Size(150, 24);
            this.cmbFilter2.TabIndex = 13;
            // 
            // lblFilter2
            // 
            this.lblFilter2.AutoSize = true;
            this.lblFilter2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFilter2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblFilter2.Location = new System.Drawing.Point(220, 73);
            this.lblFilter2.Name = "lblFilter2";
            this.lblFilter2.Size = new System.Drawing.Size(43, 15);
            this.lblFilter2.TabIndex = 12;
            this.lblFilter2.Text = "Status:";
            // 
            // cmbFilter1
            // 
            this.cmbFilter1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilter1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbFilter1.FormattingEnabled = true;
            this.cmbFilter1.Location = new System.Drawing.Point(70, 69);
            this.cmbFilter1.Name = "cmbFilter1";
            this.cmbFilter1.Size = new System.Drawing.Size(140, 24);
            this.cmbFilter1.TabIndex = 11;
            // 
            // lblFilter1
            // 
            this.lblFilter1.AutoSize = true;
            this.lblFilter1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFilter1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblFilter1.Location = new System.Drawing.Point(18, 73);
            this.lblFilter1.Name = "lblFilter1";
            this.lblFilter1.Size = new System.Drawing.Size(47, 15);
            this.lblFilter1.TabIndex = 10;
            this.lblFilter1.Text = "Doctor:";
            // 
            // btnPresetAll
            // 
            this.btnPresetAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnPresetAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnPresetAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPresetAll.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnPresetAll.Location = new System.Drawing.Point(985, 17);
            this.btnPresetAll.Name = "btnPresetAll";
            this.btnPresetAll.Size = new System.Drawing.Size(75, 26);
            this.btnPresetAll.TabIndex = 9;
            this.btnPresetAll.Text = "All Time";
            this.btnPresetAll.UseVisualStyleBackColor = false;
            this.btnPresetAll.Click += new System.EventHandler(this.BtnPresetAll_Click);
            // 
            // btnPresetMonth
            // 
            this.btnPresetMonth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnPresetMonth.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnPresetMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPresetMonth.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnPresetMonth.Location = new System.Drawing.Point(905, 17);
            this.btnPresetMonth.Name = "btnPresetMonth";
            this.btnPresetMonth.Size = new System.Drawing.Size(75, 26);
            this.btnPresetMonth.TabIndex = 8;
            this.btnPresetMonth.Text = "This Month";
            this.btnPresetMonth.UseVisualStyleBackColor = false;
            this.btnPresetMonth.Click += new System.EventHandler(this.BtnPresetMonth_Click);
            // 
            // btnPresetWeek
            // 
            this.btnPresetWeek.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnPresetWeek.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnPresetWeek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPresetWeek.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnPresetWeek.Location = new System.Drawing.Point(825, 17);
            this.btnPresetWeek.Name = "btnPresetWeek";
            this.btnPresetWeek.Size = new System.Drawing.Size(75, 26);
            this.btnPresetWeek.TabIndex = 7;
            this.btnPresetWeek.Text = "This Week";
            this.btnPresetWeek.UseVisualStyleBackColor = false;
            this.btnPresetWeek.Click += new System.EventHandler(this.BtnPresetWeek_Click);
            // 
            // btnPresetToday
            // 
            this.btnPresetToday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnPresetToday.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnPresetToday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPresetToday.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnPresetToday.Location = new System.Drawing.Point(755, 17);
            this.btnPresetToday.Name = "btnPresetToday";
            this.btnPresetToday.Size = new System.Drawing.Size(65, 26);
            this.btnPresetToday.TabIndex = 6;
            this.btnPresetToday.Text = "Today";
            this.btnPresetToday.UseVisualStyleBackColor = false;
            this.btnPresetToday.Click += new System.EventHandler(this.BtnPresetToday_Click);
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd/MM/yyyy";
            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(620, 18);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(120, 24);
            this.dtpTo.TabIndex = 5;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblTo.Location = new System.Drawing.Point(590, 22);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(23, 15);
            this.lblTo.TabIndex = 4;
            this.lblTo.Text = "To:";
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(460, 18);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(120, 24);
            this.dtpFrom.TabIndex = 3;
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblFrom.Location = new System.Drawing.Point(415, 22);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(38, 15);
            this.lblFrom.TabIndex = 2;
            this.lblFrom.Text = "From:";
            // 
            // cmbReportType
            // 
            this.cmbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReportType.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cmbReportType.FormattingEnabled = true;
            this.cmbReportType.Items.AddRange(new object[] {
            "Appointments Analytics",
            "Doctor Workload Summary",
            "Pharmacy Inventory & Valuation",
            "Hospital Revenue & Invoicing"});
            this.cmbReportType.Location = new System.Drawing.Point(100, 18);
            this.cmbReportType.Name = "cmbReportType";
            this.cmbReportType.Size = new System.Drawing.Size(290, 25);
            this.cmbReportType.TabIndex = 1;
            this.cmbReportType.SelectedIndexChanged += new System.EventHandler(this.CmbReportType_SelectedIndexChanged);
            // 
            // lblReportType
            // 
            this.lblReportType.AutoSize = true;
            this.lblReportType.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblReportType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblReportType.Location = new System.Drawing.Point(18, 22);
            this.lblReportType.Name = "lblReportType";
            this.lblReportType.Size = new System.Drawing.Size(76, 15);
            this.lblReportType.TabIndex = 0;
            this.lblReportType.Text = "Report Type:";
            // 
            // pnlSummary
            // 
            this.pnlSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSummary.Controls.Add(this.lblSummaryMetric2);
            this.pnlSummary.Controls.Add(this.lblSummaryMetric1);
            this.pnlSummary.Controls.Add(this.lblRecordCount);
            this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSummary.Location = new System.Drawing.Point(0, 560);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(1084, 40);
            this.pnlSummary.TabIndex = 2;
            // 
            // lblSummaryMetric2
            // 
            this.lblSummaryMetric2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSummaryMetric2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSummaryMetric2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSummaryMetric2.Location = new System.Drawing.Point(750, 10);
            this.lblSummaryMetric2.Name = "lblSummaryMetric2";
            this.lblSummaryMetric2.Size = new System.Drawing.Size(310, 20);
            this.lblSummaryMetric2.TabIndex = 2;
            this.lblSummaryMetric2.Text = "";
            this.lblSummaryMetric2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSummaryMetric1
            // 
            this.lblSummaryMetric1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSummaryMetric1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSummaryMetric1.Location = new System.Drawing.Point(260, 10);
            this.lblSummaryMetric1.Name = "lblSummaryMetric1";
            this.lblSummaryMetric1.Size = new System.Drawing.Size(460, 20);
            this.lblSummaryMetric1.TabIndex = 1;
            this.lblSummaryMetric1.Text = "";
            this.lblSummaryMetric1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.AutoSize = true;
            this.lblRecordCount.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblRecordCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblRecordCount.Location = new System.Drawing.Point(18, 11);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(107, 17);
            this.lblRecordCount.TabIndex = 0;
            this.lblRecordCount.Text = "Total Records: 0";
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.dgvResults);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 175);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(15);
            this.pnlGrid.Size = new System.Drawing.Size(1084, 385);
            this.pnlGrid.TabIndex = 3;
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AllowUserToResizeRows = false;
            this.dgvResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResults.BackgroundColor = System.Drawing.Color.White;
            this.dgvResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvResults.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvResults.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvResults.ColumnHeadersHeight = 36;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(242)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(74)))), ((int)(((byte)(110)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvResults.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.EnableHeadersVisualStyles = false;
            this.dgvResults.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.dgvResults.Location = new System.Drawing.Point(15, 15);
            this.dgvResults.MultiSelect = false;
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.RowHeadersVisible = false;
            this.dgvResults.RowTemplate.Height = 30;
            this.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResults.Size = new System.Drawing.Size(1054, 355);
            this.dgvResults.TabIndex = 0;
            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1084, 600);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlSummary);
            this.Controls.Add(this.pnlFilterBox);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MinimumSize = new System.Drawing.Size(1000, 500);
            this.Name = "ReportsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reports & Analytics - MediCare HMS";
            this.Load += new System.EventHandler(this.ReportsForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFilterBox.ResumeLayout(false);
            this.pnlFilterBox.PerformLayout();
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderSubtitle;
        private System.Windows.Forms.Panel pnlFilterBox;
        private System.Windows.Forms.Label lblReportType;
        private System.Windows.Forms.ComboBox cmbReportType;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button btnPresetToday;
        private System.Windows.Forms.Button btnPresetWeek;
        private System.Windows.Forms.Button btnPresetMonth;
        private System.Windows.Forms.Button btnPresetAll;
        private System.Windows.Forms.Label lblFilter1;
        private System.Windows.Forms.ComboBox cmbFilter1;
        private System.Windows.Forms.Label lblFilter2;
        private System.Windows.Forms.ComboBox cmbFilter2;
        private System.Windows.Forms.CheckBox chkOption;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnExportCsv;
        private System.Windows.Forms.Button btnPrintPreview;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Label lblRecordCount;
        private System.Windows.Forms.Label lblSummaryMetric1;
        private System.Windows.Forms.Label lblSummaryMetric2;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.DataGridView dgvResults;
    }
}
