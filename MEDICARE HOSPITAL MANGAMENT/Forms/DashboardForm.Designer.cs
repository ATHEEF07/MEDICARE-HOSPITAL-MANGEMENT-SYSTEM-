using System.Drawing;
using System.Windows.Forms;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    partial class DashboardForm
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
            btnRefresh = new Button();

            pnlMetrics = new FlowLayoutPanel();
            pnlCardPatients = new Panel();
            lblPatientsTitle = new Label();
            lblPatientsVal = new Label();
            lblPatientsSub = new Label();

            pnlCardAppts = new Panel();
            lblApptsTitle = new Label();
            lblApptsVal = new Label();
            lblApptsSub = new Label();

            pnlCardDoctors = new Panel();
            lblDoctorsTitle = new Label();
            lblDoctorsVal = new Label();
            lblDoctorsSub = new Label();

            pnlCardStock = new Panel();
            lblStockTitle = new Label();
            lblStockVal = new Label();
            lblStockSub = new Label();

            pnlCardRevenue = new Panel();
            lblRevenueTitle = new Label();
            lblRevenueVal = new Label();
            lblRevenueSub = new Label();

            pnlCardDue = new Panel();
            lblDueTitle = new Label();
            lblDueVal = new Label();
            lblDueSub = new Label();

            pnlAlert = new Panel();
            lblAlertNotice = new Label();

            pnlCenter = new Panel();
            pnlGridTitle = new Panel();
            lblGridTitle = new Label();
            lblGridSub = new Label();
            dgvTodayAppts = new DataGridView();

            pnlQuickActions = new Panel();
            lblQuickActions = new Label();
            btnQuickAppt = new Button();
            btnQuickConsult = new Button();
            btnQuickDispense = new Button();
            btnQuickBill = new Button();
            btnQuickReports = new Button();

            pnlHeader.SuspendLayout();
            pnlMetrics.SuspendLayout();
            pnlCardPatients.SuspendLayout();
            pnlCardAppts.SuspendLayout();
            pnlCardDoctors.SuspendLayout();
            pnlCardStock.SuspendLayout();
            pnlCardRevenue.SuspendLayout();
            pnlCardDue.SuspendLayout();
            pnlAlert.SuspendLayout();
            pnlCenter.SuspendLayout();
            pnlGridTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTodayAppts).BeginInit();
            pnlQuickActions.SuspendLayout();
            SuspendLayout();

            // ── pnlHeader ──────────────────────────────────────────────────────────
            pnlHeader.BackColor = Color.FromArgb(20, 80, 140);
            pnlHeader.Controls.Add(btnRefresh);
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
            lblHeaderTitle.Size = new Size(390, 35);
            lblHeaderTitle.Text = "📊  Executive Hospital Dashboard";

            lblHeaderSubtitle.AutoSize = true;
            lblHeaderSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblHeaderSubtitle.ForeColor = Color.FromArgb(200, 225, 255);
            lblHeaderSubtitle.Location = new Point(22, 45);
            lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            lblHeaderSubtitle.Size = new Size(540, 21);
            lblHeaderSubtitle.Text = "Real-time clinical operations, department capacity, and financial performance";

            btnRefresh.BackColor = Color.FromArgb(235, 245, 255);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.FromArgb(20, 80, 140);
            btnRefresh.Location = new Point(1040, 20);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(120, 34);
            btnRefresh.TabIndex = 0;
            btnRefresh.Text = "🔄 Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += BtnRefresh_Click;

            // ── pnlMetrics (Flow layout with 6 cards) ──────────────────────────────
            pnlMetrics.AutoSize = true;
            pnlMetrics.BackColor = Color.FromArgb(245, 248, 252);
            pnlMetrics.Controls.Add(pnlCardPatients);
            pnlMetrics.Controls.Add(pnlCardAppts);
            pnlMetrics.Controls.Add(pnlCardDoctors);
            pnlMetrics.Controls.Add(pnlCardStock);
            pnlMetrics.Controls.Add(pnlCardRevenue);
            pnlMetrics.Controls.Add(pnlCardDue);
            pnlMetrics.Dock = DockStyle.Top;
            pnlMetrics.Location = new Point(0, 75);
            pnlMetrics.Name = "pnlMetrics";
            pnlMetrics.Padding = new Padding(12, 10, 12, 10);
            pnlMetrics.Size = new Size(1180, 106);
            pnlMetrics.TabIndex = 1;

            // Helper to style metric card
            void StyleCard(Panel pnl, Label title, Label val, Label sub, Color titleClr, Color valClr)
            {
                pnl.BackColor = Color.White;
                pnl.BorderStyle = BorderStyle.FixedSingle;
                pnl.Margin = new Padding(6);
                pnl.Size = new Size(178, 84);

                title.AutoSize = true;
                title.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
                title.ForeColor = titleClr;
                title.Location = new Point(10, 6);

                val.AutoSize = true;
                val.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
                val.ForeColor = valClr;
                val.Location = new Point(10, 24);

                sub.AutoSize = true;
                sub.Font = new Font("Segoe UI", 7.5F);
                sub.ForeColor = Color.FromArgb(120, 120, 120);
                sub.Location = new Point(10, 58);

                pnl.Controls.Add(title);
                pnl.Controls.Add(val);
                pnl.Controls.Add(sub);
            }

            StyleCard(pnlCardPatients, lblPatientsTitle, lblPatientsVal, lblPatientsSub, Color.FromArgb(40, 80, 130), Color.FromArgb(20, 70, 130));
            lblPatientsTitle.Text = "REGISTERED PATIENTS";
            lblPatientsVal.Text = "0";
            lblPatientsSub.Text = "Active hospital registry";

            StyleCard(pnlCardAppts, lblApptsTitle, lblApptsVal, lblApptsSub, Color.FromArgb(30, 100, 130), Color.FromArgb(20, 100, 140));
            lblApptsTitle.Text = "TODAY'S VISITS";
            lblApptsVal.Text = "0";
            lblApptsSub.Text = "0 Completed";

            StyleCard(pnlCardDoctors, lblDoctorsTitle, lblDoctorsVal, lblDoctorsSub, Color.FromArgb(30, 110, 70), Color.FromArgb(20, 120, 60));
            lblDoctorsTitle.Text = "ACTIVE DOCTORS";
            lblDoctorsVal.Text = "0";
            lblDoctorsSub.Text = "Specialists on duty";

            StyleCard(pnlCardStock, lblStockTitle, lblStockVal, lblStockSub, Color.FromArgb(160, 60, 20), Color.FromArgb(180, 40, 20));
            lblStockTitle.Text = "LOW STOCK ALERTS";
            lblStockVal.Text = "0";
            lblStockSub.Text = "Items at/below reorder";

            StyleCard(pnlCardRevenue, lblRevenueTitle, lblRevenueVal, lblRevenueSub, Color.FromArgb(20, 110, 50), Color.FromArgb(20, 120, 50));
            lblRevenueTitle.Text = "TODAY'S REVENUE";
            lblRevenueVal.Text = "Rs. 0";
            lblRevenueSub.Text = "Cashier collections";

            StyleCard(pnlCardDue, lblDueTitle, lblDueVal, lblDueSub, Color.FromArgb(150, 80, 20), Color.FromArgb(160, 80, 20));
            lblDueTitle.Text = "PENDING RECEIVABLES";
            lblDueVal.Text = "Rs. 0";
            lblDueSub.Text = "Unsettled invoices";

            // ── pnlAlert (Inventory Banner) ────────────────────────────────────────
            pnlAlert.BackColor = Color.FromArgb(255, 248, 220); // Soft amber
            pnlAlert.BorderStyle = BorderStyle.FixedSingle;
            pnlAlert.Controls.Add(lblAlertNotice);
            pnlAlert.Dock = DockStyle.Top;
            pnlAlert.Location = new Point(0, 181);
            pnlAlert.Name = "pnlAlert";
            pnlAlert.Padding = new Padding(15, 6, 15, 6);
            pnlAlert.Size = new Size(1180, 32);
            pnlAlert.TabIndex = 2;

            lblAlertNotice.AutoSize = true;
            lblAlertNotice.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAlertNotice.ForeColor = Color.FromArgb(160, 70, 0);
            lblAlertNotice.Location = new Point(15, 6);
            lblAlertNotice.Text = "Notice: Inventory monitoring active.";

            // ── pnlQuickActions (Bottom launcher buttons) ──────────────────────────
            pnlQuickActions.BackColor = Color.FromArgb(240, 245, 252);
            pnlQuickActions.BorderStyle = BorderStyle.FixedSingle;
            pnlQuickActions.Controls.Add(lblQuickActions);
            pnlQuickActions.Controls.Add(btnQuickAppt);
            pnlQuickActions.Controls.Add(btnQuickConsult);
            pnlQuickActions.Controls.Add(btnQuickDispense);
            pnlQuickActions.Controls.Add(btnQuickBill);
            pnlQuickActions.Controls.Add(btnQuickReports);
            pnlQuickActions.Dock = DockStyle.Bottom;
            pnlQuickActions.Location = new Point(0, 650);
            pnlQuickActions.Name = "pnlQuickActions";
            pnlQuickActions.Padding = new Padding(15, 8, 15, 8);
            pnlQuickActions.Size = new Size(1180, 55);
            pnlQuickActions.TabIndex = 4;

            lblQuickActions.AutoSize = true;
            lblQuickActions.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblQuickActions.ForeColor = Color.FromArgb(20, 60, 110);
            lblQuickActions.Location = new Point(15, 16);
            lblQuickActions.Text = "Quick Actions:";

            void StyleQuickBtn(Button btn, string text, Color bg, Color fg, int x, int w)
            {
                btn.Text = text;
                btn.BackColor = bg;
                btn.ForeColor = fg;
                btn.FlatStyle = FlatStyle.Flat;
                btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                btn.Location = new Point(x, 10);
                btn.Size = new Size(w, 33);
                btn.UseVisualStyleBackColor = false;
            }

            StyleQuickBtn(btnQuickAppt,     "📅  Book Appointment", Color.FromArgb(20, 80, 140),  Color.White, 120, 175);
            StyleQuickBtn(btnQuickConsult,  "🩺  Consultations",     Color.FromArgb(20, 120, 80), Color.White, 305, 155);
            StyleQuickBtn(btnQuickDispense, "💊  Pharmacy Dispense", Color.FromArgb(140, 60, 20), Color.White, 470, 165);
            StyleQuickBtn(btnQuickBill,     "💳  New Invoice",       Color.FromArgb(50, 70, 130), Color.White, 645, 140);
            StyleQuickBtn(btnQuickReports,  "📈  Full Reports",      Color.FromArgb(235, 245, 255), Color.FromArgb(20, 80, 140), 795, 145);

            btnQuickAppt.Click     += BtnQuickAppt_Click;
            btnQuickConsult.Click  += BtnQuickConsult_Click;
            btnQuickDispense.Click += BtnQuickDispense_Click;
            btnQuickBill.Click     += BtnQuickBill_Click;
            btnQuickReports.Click  += BtnQuickReports_Click;

            // ── pnlCenter (Today's Consultations Grid) ─────────────────────────────
            pnlCenter.Controls.Add(dgvTodayAppts);
            pnlCenter.Controls.Add(pnlGridTitle);
            pnlCenter.Dock = DockStyle.Fill;
            pnlCenter.Location = new Point(0, 213);
            pnlCenter.Name = "pnlCenter";
            pnlCenter.Padding = new Padding(15, 10, 15, 10);
            pnlCenter.Size = new Size(1180, 437);
            pnlCenter.TabIndex = 3;

            // pnlGridTitle
            pnlGridTitle.Controls.Add(lblGridSub);
            pnlGridTitle.Controls.Add(lblGridTitle);
            pnlGridTitle.Dock = DockStyle.Top;
            pnlGridTitle.Location = new Point(15, 10);
            pnlGridTitle.Name = "pnlGridTitle";
            pnlGridTitle.Size = new Size(1150, 36);
            pnlGridTitle.TabIndex = 0;

            lblGridTitle.AutoSize = true;
            lblGridTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblGridTitle.ForeColor = Color.FromArgb(20, 60, 110);
            lblGridTitle.Location = new Point(0, 5);
            lblGridTitle.Text = "Today's & Upcoming Scheduled Consultations";

            lblGridSub.AutoSize = true;
            lblGridSub.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            lblGridSub.ForeColor = Color.Gray;
            lblGridSub.Location = new Point(410, 10);
            lblGridSub.Text = "(Double-click any scheduled appointment to launch doctor consultation)";

            // dgvTodayAppts
            dgvTodayAppts.AllowUserToAddRows = false;
            dgvTodayAppts.AllowUserToDeleteRows = false;
            dgvTodayAppts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTodayAppts.BackgroundColor = Color.White;
            dgvTodayAppts.BorderStyle = BorderStyle.Fixed3D;

            dgvHeaderStyle.BackColor = Color.FromArgb(27, 54, 93);
            dgvHeaderStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvHeaderStyle.ForeColor = Color.White;
            dgvTodayAppts.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
            dgvTodayAppts.ColumnHeadersHeight = 32;

            dgvDefaultStyle.Font = new Font("Segoe UI", 9F);
            dgvDefaultStyle.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dgvDefaultStyle.SelectionForeColor = Color.Black;
            dgvTodayAppts.DefaultCellStyle = dgvDefaultStyle;

            dgvTodayAppts.Dock = DockStyle.Fill;
            dgvTodayAppts.EnableHeadersVisualStyles = false;
            dgvTodayAppts.MultiSelect = false;
            dgvTodayAppts.ReadOnly = true;
            dgvTodayAppts.RowHeadersVisible = false;
            dgvTodayAppts.RowTemplate.Height = 28;
            dgvTodayAppts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTodayAppts.CellDoubleClick += DgvTodayAppts_CellDoubleClick;
            dgvTodayAppts.CellFormatting += DgvTodayAppts_CellFormatting;

            // ── DashboardForm ──────────────────────────────────────────────────────
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1180, 705);
            Controls.Add(pnlCenter);
            Controls.Add(pnlQuickActions);
            Controls.Add(pnlAlert);
            Controls.Add(pnlMetrics);
            Controls.Add(pnlHeader);
            MinimumSize = new Size(1000, 650);
            Name = "DashboardForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MediCare - Executive Hospital Dashboard";
            Load += DashboardForm_Load;

            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlMetrics.ResumeLayout(false);
            pnlCardPatients.ResumeLayout(false);
            pnlCardPatients.PerformLayout();
            pnlCardAppts.ResumeLayout(false);
            pnlCardAppts.PerformLayout();
            pnlCardDoctors.ResumeLayout(false);
            pnlCardDoctors.PerformLayout();
            pnlCardStock.ResumeLayout(false);
            pnlCardStock.PerformLayout();
            pnlCardRevenue.ResumeLayout(false);
            pnlCardRevenue.PerformLayout();
            pnlCardDue.ResumeLayout(false);
            pnlCardDue.PerformLayout();
            pnlAlert.ResumeLayout(false);
            pnlAlert.PerformLayout();
            pnlCenter.ResumeLayout(false);
            pnlGridTitle.ResumeLayout(false);
            pnlGridTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTodayAppts).EndInit();
            pnlQuickActions.ResumeLayout(false);
            pnlQuickActions.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlHeader;
        private Label lblHeaderTitle;
        private Label lblHeaderSubtitle;
        private Button btnRefresh;
        private FlowLayoutPanel pnlMetrics;
        private Panel pnlCardPatients;
        private Label lblPatientsTitle, lblPatientsVal, lblPatientsSub;
        private Panel pnlCardAppts;
        private Label lblApptsTitle, lblApptsVal, lblApptsSub;
        private Panel pnlCardDoctors;
        private Label lblDoctorsTitle, lblDoctorsVal, lblDoctorsSub;
        private Panel pnlCardStock;
        private Label lblStockTitle, lblStockVal, lblStockSub;
        private Panel pnlCardRevenue;
        private Label lblRevenueTitle, lblRevenueVal, lblRevenueSub;
        private Panel pnlCardDue;
        private Label lblDueTitle, lblDueVal, lblDueSub;
        private Panel pnlAlert;
        private Label lblAlertNotice;
        private Panel pnlCenter;
        private Panel pnlGridTitle;
        private Label lblGridTitle;
        private Label lblGridSub;
        private DataGridView dgvTodayAppts;
        private Panel pnlQuickActions;
        private Label lblQuickActions;
        private Button btnQuickAppt;
        private Button btnQuickConsult;
        private Button btnQuickDispense;
        private Button btnQuickBill;
        private Button btnQuickReports;
    }
}
