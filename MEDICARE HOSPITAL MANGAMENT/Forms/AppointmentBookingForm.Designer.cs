using System.Drawing;
using System.Windows.Forms;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    partial class AppointmentBookingForm
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
            pnlHeader            = new Panel();
            lblTitle             = new Label();
            pnlBody              = new Panel();
            lblPatient           = new Label();
            cmbPatient           = new ComboBox();
            lblDoctor            = new Label();
            cmbDoctor            = new ComboBox();
            lblDate              = new Label();
            dtpAppointmentDate   = new DateTimePicker();
            lblStartTime         = new Label();
            dtpStartTime         = new DateTimePicker();
            lblDuration          = new Label();
            cmbDuration          = new ComboBox();
            lblEndTime           = new Label();
            btnCheckAvailability = new Button();
            lblAvailability      = new Label();
            lblReason            = new Label();
            txtReason            = new TextBox();
            lblNotes             = new Label();
            txtNotes             = new TextBox();
            pnlFooter            = new Panel();
            btnSave              = new Button();
            btnCancel            = new Button();

            pnlHeader.SuspendLayout();
            pnlBody.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();

            // ── Header ─────────────────────────────────────────────────────────────
            pnlHeader.BackColor = Color.FromArgb(13, 71, 128);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 64;
            pnlHeader.Padding = new Padding(20, 14, 20, 14);

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 16);
            lblTitle.Text = "📅  Book New Appointment";

            // ── Body ───────────────────────────────────────────────────────────────
            pnlBody.AutoScroll = true;
            pnlBody.Controls.AddRange(new Control[] {
                lblPatient, cmbPatient,
                lblDoctor, cmbDoctor,
                lblDate, dtpAppointmentDate,
                lblStartTime, dtpStartTime,
                lblDuration, cmbDuration, lblEndTime,
                btnCheckAvailability, lblAvailability,
                lblReason, txtReason,
                lblNotes, txtNotes
            });
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Padding = new Padding(30, 20, 30, 10);

            int y = 20;

            // Patient
            AddLabel(lblPatient, "Patient: *", 30, y);
            cmbPatient.Location = new Point(170, y - 2);
            cmbPatient.Size = new Size(400, 28);
            cmbPatient.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPatient.Font = new Font("Segoe UI", 9.5F);
            y += 38;

            // Doctor
            AddLabel(lblDoctor, "Doctor: *", 30, y);
            cmbDoctor.Location = new Point(170, y - 2);
            cmbDoctor.Size = new Size(400, 28);
            cmbDoctor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDoctor.Font = new Font("Segoe UI", 9.5F);
            y += 38;

            // Date
            AddLabel(lblDate, "Date: *", 30, y);
            dtpAppointmentDate.Location = new Point(170, y - 2);
            dtpAppointmentDate.Size = new Size(140, 28);
            dtpAppointmentDate.Format = DateTimePickerFormat.Short;
            y += 38;

            // Start time
            AddLabel(lblStartTime, "Start Time: *", 30, y);
            dtpStartTime.Location = new Point(170, y - 2);
            dtpStartTime.Size = new Size(120, 28);
            dtpStartTime.Format = DateTimePickerFormat.Time;
            dtpStartTime.ShowUpDown = true;
            dtpStartTime.ValueChanged += new System.EventHandler(dtpStartTime_ValueChanged);

            AddLabel(new Label(), "Duration:", 310, y);
            cmbDuration.Location = new Point(390, y - 2);
            cmbDuration.Size = new Size(90, 28);
            cmbDuration.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDuration.SelectedIndexChanged += new System.EventHandler(cmbDuration_SelectedIndexChanged);

            lblEndTime.AutoSize = true;
            lblEndTime.Font = new Font("Segoe UI", 9F);
            lblEndTime.ForeColor = Color.FromArgb(70, 130, 180);
            lblEndTime.Location = new Point(500, y + 2);
            lblEndTime.Text = "End: --:--";
            y += 38;

            // Check availability
            btnCheckAvailability.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCheckAvailability.Location = new Point(170, y);
            btnCheckAvailability.Size = new Size(200, 32);
            btnCheckAvailability.Text = "🔍 Check Availability";
            btnCheckAvailability.FlatStyle = FlatStyle.Flat;
            btnCheckAvailability.BackColor = Color.FromArgb(0, 120, 215);
            btnCheckAvailability.ForeColor = Color.White;
            btnCheckAvailability.Cursor = Cursors.Hand;
            btnCheckAvailability.Click += new System.EventHandler(btnCheckAvailability_Click);

            lblAvailability.AutoSize = true;
            lblAvailability.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblAvailability.Location = new Point(380, y + 6);
            lblAvailability.MaximumSize = new Size(340, 0);
            lblAvailability.Text = string.Empty;
            y += 50;

            // Reason
            AddLabel(lblReason, "Reason:", 30, y);
            txtReason.Location = new Point(170, y - 2);
            txtReason.Size = new Size(400, 26);
            txtReason.Font = new Font("Segoe UI", 9.5F);
            txtReason.PlaceholderText = "Brief reason for visit";
            y += 38;

            // Notes
            AddLabel(lblNotes, "Notes:", 30, y);
            txtNotes.Location = new Point(170, y - 2);
            txtNotes.Size = new Size(400, 70);
            txtNotes.Font = new Font("Segoe UI", 9.5F);
            txtNotes.Multiline = true;
            txtNotes.ScrollBars = ScrollBars.Vertical;

            // ── Footer ─────────────────────────────────────────────────────────────
            pnlFooter.BackColor = Color.FromArgb(240, 244, 248);
            pnlFooter.Controls.AddRange(new Control[] { btnSave, btnCancel });
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Height = 58;

            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.Location = new Point(220, 13);
            btnSave.Size = new Size(180, 34);
            btnSave.Text = "✅  Confirm Booking";
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.BackColor = Color.FromArgb(13, 71, 128);
            btnSave.ForeColor = Color.White;
            btnSave.Cursor = Cursors.Hand;
            btnSave.Click += new System.EventHandler(btnSave_Click);

            btnCancel.Font = new Font("Segoe UI", 10F);
            btnCancel.Location = new Point(415, 13);
            btnCancel.Size = new Size(120, 34);
            btnCancel.Text = "Cancel";
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.BackColor = Color.FromArgb(180, 50, 50);
            btnCancel.ForeColor = Color.White;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.Click += new System.EventHandler(btnCancel_Click);

            // ── Form ───────────────────────────────────────────────────────────────
            Controls.AddRange(new Control[] { pnlBody, pnlFooter, pnlHeader });
            ClientSize = new Size(620, 490);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AppointmentBookingForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "New Appointment – MediCare";

            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlBody.ResumeLayout(false);
            pnlBody.PerformLayout();
            pnlFooter.ResumeLayout(false);
            ResumeLayout(false);
        }

        private static void AddLabel(Label lbl, string text, int x, int y)
        {
            lbl.AutoSize = true;
            lbl.Font = new Font("Segoe UI", 9.5F);
            lbl.ForeColor = Color.FromArgb(40, 40, 60);
            lbl.Location = new System.Drawing.Point(x, y + 3);
            lbl.Text = text;
        }

        private Panel pnlHeader, pnlBody, pnlFooter;
        private Label lblTitle, lblPatient, lblDoctor, lblDate, lblStartTime,
                      lblDuration, lblEndTime, lblReason, lblNotes, lblAvailability;
        private ComboBox cmbPatient, cmbDoctor, cmbDuration;
        private DateTimePicker dtpAppointmentDate, dtpStartTime;
        private TextBox txtReason, txtNotes;
        private Button btnCheckAvailability, btnSave, btnCancel;

        #endregion
    }
}
