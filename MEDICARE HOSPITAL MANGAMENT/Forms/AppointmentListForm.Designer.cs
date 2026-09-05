using System.Drawing;
using System.Windows.Forms;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    partial class AppointmentListForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlHeader          = new Panel();
            lblHeaderTitle     = new Label();
            lblHeaderSubtitle  = new Label();
            pnlFilters         = new Panel();
            lblDateFilter      = new Label();
            chkUseDate         = new CheckBox();
            dtpFilterDate      = new DateTimePicker();
            btnToday           = new Button();
            lblDoctorFilter    = new Label();
            cmbDoctorFilter    = new ComboBox();
            lblStatusFilter    = new Label();
            cmbStatusFilter    = new ComboBox();
            lblPatientSearch   = new Label();
            txtPatientSearch   = new TextBox();
            btnSearch          = new Button();
            pnlActions         = new Panel();
            btnNewAppointment  = new Button();
            btnReschedule      = new Button();
            btnCancel          = new Button();
            btnMarkCompleted   = new Button();
            btnMarkNoShow      = new Button();
            btnStartConsultation = new Button();
            btnRefresh         = new Button();
            lblRecordCount     = new Label();
            pnlGrid            = new Panel();
            dgvAppointments    = new DataGridView();
            colID              = new DataGridViewTextBoxColumn();
            colDate            = new DataGridViewTextBoxColumn();
            colTime            = new DataGridViewTextBoxColumn();
            colPatient         = new DataGridViewTextBoxColumn();
            colDoctor          = new DataGridViewTextBoxColumn();
            colDept            = new DataGridViewTextBoxColumn();
            colReason          = new DataGridViewTextBoxColumn();
            colStatus          = new DataGridViewTextBoxColumn();

            pnlHeader.SuspendLayout();
            pnlFilters.SuspendLayout();
            pnlActions.SuspendLayout();
            pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAppointments).BeginInit();
            SuspendLayout();

            // ── Header ─────────────────────────────────────────────────────────────
            pnlHeader.BackColor = Color.FromArgb(13, 71, 128);
            pnlHeader.Controls.Add(lblHeaderSubtitle);
            pnlHeader.Controls.Add(lblHeaderTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Padding = new Padding(20, 14, 20, 14);
            pnlHeader.Size = new Size(1200, 80);

            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(20, 10);
            lblHeaderTitle.Text = "Appointment Scheduling & Management";

            lblHeaderSubtitle.AutoSize = true;
            lblHeaderSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblHeaderSubtitle.ForeColor = Color.FromArgb(200, 220, 240);
            lblHeaderSubtitle.Location = new Point(22, 44);
            lblHeaderSubtitle.Text = "Book, reschedule, cancel, and manage all patient appointments";

            // ── Filters panel ──────────────────────────────────────────────────────
            pnlFilters.BackColor = Color.FromArgb(240, 245, 252);
            pnlFilters.Controls.AddRange(new Control[] {
                lblDateFilter, chkUseDate, dtpFilterDate, btnToday,
                lblDoctorFilter, cmbDoctorFilter,
                lblStatusFilter, cmbStatusFilter,
                lblPatientSearch, txtPatientSearch, btnSearch
            });
            pnlFilters.Dock = DockStyle.Top;
            pnlFilters.Padding = new Padding(16, 10, 16, 10);
            pnlFilters.Size = new Size(1200, 60);

            // Date filter
            int x = 16;
            chkUseDate.AutoSize = true;
            chkUseDate.Checked = true;
            chkUseDate.Font = new Font("Segoe UI", 9F);
            chkUseDate.Location = new Point(x, 20);
            chkUseDate.Text = "Date:";
            chkUseDate.CheckedChanged += new System.EventHandler(chkUseDate_CheckedChanged);
            x += 60;

            dtpFilterDate.Format = DateTimePickerFormat.Short;
            dtpFilterDate.Location = new Point(x, 17);
            dtpFilterDate.Size = new Size(110, 26);
            dtpFilterDate.ValueChanged += new System.EventHandler(dtpFilterDate_ValueChanged);
            x += 120;

            btnToday.Font = new Font("Segoe UI", 8F);
            btnToday.Location = new Point(x, 17);
            btnToday.Size = new Size(55, 26);
            btnToday.Text = "Today";
            btnToday.FlatStyle = FlatStyle.Flat;
            btnToday.BackColor = Color.FromArgb(13, 71, 128);
            btnToday.ForeColor = Color.White;
            btnToday.Click += new System.EventHandler(btnToday_Click);
            x += 70;

            lblDoctorFilter.AutoSize = true;
            lblDoctorFilter.Font = new Font("Segoe UI", 9F);
            lblDoctorFilter.Location = new Point(x, 21);
            lblDoctorFilter.Text = "Doctor:";
            x += 55;

            cmbDoctorFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDoctorFilter.Location = new Point(x, 17);
            cmbDoctorFilter.Size = new Size(160, 26);
            cmbDoctorFilter.SelectedIndexChanged += new System.EventHandler(cmbDoctorFilter_SelectedIndexChanged);
            x += 175;

            lblStatusFilter.AutoSize = true;
            lblStatusFilter.Font = new Font("Segoe UI", 9F);
            lblStatusFilter.Location = new Point(x, 21);
            lblStatusFilter.Text = "Status:";
            x += 52;

            cmbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatusFilter.Location = new Point(x, 17);
            cmbStatusFilter.Size = new Size(120, 26);
            cmbStatusFilter.SelectedIndexChanged += new System.EventHandler(cmbStatusFilter_SelectedIndexChanged);
            x += 135;

            lblPatientSearch.AutoSize = true;
            lblPatientSearch.Font = new Font("Segoe UI", 9F);
            lblPatientSearch.Location = new Point(x, 21);
            lblPatientSearch.Text = "Patient:";
            x += 55;

            txtPatientSearch.Location = new Point(x, 17);
            txtPatientSearch.Size = new Size(160, 26);
            txtPatientSearch.PlaceholderText = "Code / Name / Phone";
            txtPatientSearch.KeyDown += new KeyEventHandler(txtPatientSearch_KeyDown);
            x += 170;

            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSearch.Location = new Point(x, 16);
            btnSearch.Size = new Size(80, 28);
            btnSearch.Text = "🔍 Search";
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.BackColor = Color.FromArgb(0, 120, 215);
            btnSearch.ForeColor = Color.White;
            btnSearch.Click += new System.EventHandler(btnSearch_Click);

            // ── Action buttons ─────────────────────────────────────────────────────
            pnlActions.BackColor = Color.FromArgb(248, 249, 250);
            pnlActions.Controls.AddRange(new Control[] {
                btnNewAppointment, btnReschedule, btnCancel, btnMarkCompleted,
                btnMarkNoShow, btnStartConsultation, btnRefresh, lblRecordCount
            });
            pnlActions.Dock = DockStyle.Top;
            pnlActions.Padding = new Padding(16, 10, 16, 10);
            pnlActions.Size = new Size(1200, 56);

            StyleActionBtn(btnNewAppointment,  "➕ New Appointment",  Color.FromArgb(13, 71, 128),  10);
            StyleActionBtn(btnReschedule,       "📅 Reschedule",       Color.FromArgb(60, 120, 40),  150);
            StyleActionBtn(btnCancel,           "❌ Cancel Appt.",     Color.FromArgb(160, 40, 40),  270);
            StyleActionBtn(btnMarkCompleted,    "✅ Mark Completed",   Color.FromArgb(22, 135, 90),  390);
            StyleActionBtn(btnMarkNoShow,       "🚫 No-Show",          Color.FromArgb(130, 80, 0),   530);
            StyleActionBtn(btnStartConsultation,"🩺 Consultation",    Color.FromArgb(100, 30, 120),  640);
            StyleActionBtn(btnRefresh,          "🔄 Refresh",          Color.FromArgb(90, 90, 90),   780);

            btnNewAppointment.Click += new System.EventHandler(btnNewAppointment_Click);
            btnReschedule.Click     += new System.EventHandler(btnReschedule_Click);
            btnCancel.Click         += new System.EventHandler(btnCancel_Click);
            btnMarkCompleted.Click  += new System.EventHandler(btnMarkCompleted_Click);
            btnMarkNoShow.Click     += new System.EventHandler(btnMarkNoShow_Click);
            btnStartConsultation.Click += new System.EventHandler(btnStartConsultation_Click);
            btnRefresh.Click        += new System.EventHandler(btnRefresh_Click);

            lblRecordCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblRecordCount.AutoSize = true;
            lblRecordCount.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblRecordCount.ForeColor = Color.Gray;
            lblRecordCount.Location = new Point(900, 22);
            lblRecordCount.Text = "0 appointment(s)";

            // ── Grid ───────────────────────────────────────────────────────────────
            pnlGrid.Controls.Add(dgvAppointments);
            pnlGrid.Dock = DockStyle.Fill;
            pnlGrid.Padding = new Padding(10);

            dgvAppointments.Dock = DockStyle.Fill;
            dgvAppointments.AllowUserToAddRows = false;
            dgvAppointments.AllowUserToDeleteRows = false;
            dgvAppointments.ReadOnly = true;
            dgvAppointments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAppointments.MultiSelect = false;
            dgvAppointments.RowHeadersVisible = false;
            dgvAppointments.BorderStyle = BorderStyle.None;
            dgvAppointments.BackgroundColor = Color.White;
            dgvAppointments.GridColor = Color.FromArgb(230, 235, 245);
            dgvAppointments.Font = new Font("Segoe UI", 9.5F);
            dgvAppointments.RowTemplate.Height = 30;
            dgvAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAppointments.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(13, 71, 128);
            dgvAppointments.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvAppointments.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvAppointments.ColumnHeadersHeight = 34;
            dgvAppointments.EnableHeadersVisualStyles = false;
            dgvAppointments.CellDoubleClick += new DataGridViewCellEventHandler(dgvAppointments_CellDoubleClick);

            // Columns
            SetupColumn(colID,      "colID",     "ID",          50,  false);
            SetupColumn(colDate,    "colDate",   "Date",        90,  true);
            SetupColumn(colTime,    "colTime",   "Time Slot",   140, true);
            SetupColumn(colPatient, "colPatient","Patient",     180, true);
            SetupColumn(colDoctor,  "colDoctor", "Doctor",      160, true);
            SetupColumn(colDept,    "colDept",   "Department",  130, true);
            SetupColumn(colReason,  "colReason", "Reason",      200, true);
            SetupColumn(colStatus,  "colStatus", "Status",      90,  true);
            dgvAppointments.Columns.AddRange(colID, colDate, colTime, colPatient, colDoctor, colDept, colReason, colStatus);

            // ── Form ───────────────────────────────────────────────────────────────
            Controls.AddRange(new Control[] { pnlGrid, pnlActions, pnlFilters, pnlHeader });
            ClientSize = new Size(1200, 720);
            MinimumSize = new Size(1000, 600);
            Name = "AppointmentListForm";
            Text = "Appointments – MediCare Hospital Management";
            StartPosition = FormStartPosition.CenterScreen;

            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlFilters.ResumeLayout(false);
            pnlFilters.PerformLayout();
            pnlActions.ResumeLayout(false);
            pnlActions.PerformLayout();
            pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAppointments).EndInit();
            ResumeLayout(false);
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Design helpers
        // ─────────────────────────────────────────────────────────────────────────

        private static void StyleActionBtn(Button btn, string text, Color color, int x)
        {
            btn.Text = text;
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btn.Location = new Point(x, 12);
            btn.Size = new Size(120, 32);
            btn.Cursor = Cursors.Hand;
        }

        private static void SetupColumn(DataGridViewTextBoxColumn col, string name, string header, int width, bool fill)
        {
            col.Name = name;
            col.HeaderText = header;
            if (fill) { col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; col.MinimumWidth = width; }
            else { col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None; col.Width = width; }
            col.ReadOnly = true;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Controls
        // ─────────────────────────────────────────────────────────────────────────
        private Panel pnlHeader, pnlFilters, pnlActions, pnlGrid;
        private Label lblHeaderTitle, lblHeaderSubtitle, lblDateFilter, lblDoctorFilter,
                      lblStatusFilter, lblPatientSearch, lblRecordCount;
        private CheckBox chkUseDate;
        private DateTimePicker dtpFilterDate;
        private ComboBox cmbDoctorFilter, cmbStatusFilter;
        private TextBox txtPatientSearch;
        private Button btnToday, btnSearch, btnNewAppointment, btnReschedule,
                       btnCancel, btnMarkCompleted, btnMarkNoShow, btnStartConsultation, btnRefresh;
        private DataGridView dgvAppointments;
        private DataGridViewTextBoxColumn colID, colDate, colTime, colPatient,
                                          colDoctor, colDept, colReason, colStatus;

        #endregion
    }
}
