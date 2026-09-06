using System.Drawing;
using System.Windows.Forms;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    partial class ConsultationForm
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
            pnlHeader       = new Panel();
            lblFormTitle    = new Label();

            // Patient card
            pnlPatientCard  = new Panel();
            lblPatientName  = new Label();
            lblPatientCode  = new Label();
            lblPatientAge   = new Label();
            lblPatientGender = new Label();
            lblBloodGroup   = new Label();
            lblPatientPhone = new Label();
            lblDoctor       = new Label();
            lblDept         = new Label();
            lblApptDate     = new Label();
            lblReason       = new Label();

            // Clinical input
            pnlClinical     = new Panel();
            lblSymptomsHdr  = new Label();
            txtSymptoms     = new TextBox();
            lblDiagnosisHdr = new Label();
            txtDiagnosis    = new TextBox();
            lblTreatmentHdr = new Label();
            txtTreatment    = new TextBox();
            lblNotesHdr     = new Label();
            txtNotes        = new TextBox();

            // Status
            lblSaveStatus   = new Label();

            // Footer
            pnlFooter       = new Panel();
            btnSave         = new Button();
            btnPrescribe    = new Button();
            btnViewHistory  = new Button();
            btnClear        = new Button();
            btnClose        = new Button();

            pnlHeader.SuspendLayout();
            pnlPatientCard.SuspendLayout();
            pnlClinical.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();

            // ── Header ─────────────────────────────────────────────────────────────
            pnlHeader.BackColor = Color.FromArgb(20, 80, 140);
            pnlHeader.Controls.Add(lblFormTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 60;
            pnlHeader.Padding = new Padding(20, 12, 20, 12);

            lblFormTitle.AutoSize = true;
            lblFormTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblFormTitle.ForeColor = Color.White;
            lblFormTitle.Location = new Point(20, 13);
            lblFormTitle.Text = "🩺  Clinical Consultation";

            // ── Patient Card ───────────────────────────────────────────────────────
            pnlPatientCard.BackColor = Color.FromArgb(235, 245, 255);
            pnlPatientCard.Controls.AddRange(new Control[] {
                lblPatientName, lblPatientCode, lblPatientAge, lblPatientGender,
                lblBloodGroup, lblPatientPhone, lblDoctor, lblDept, lblApptDate, lblReason
            });
            pnlPatientCard.Dock = DockStyle.Top;
            pnlPatientCard.Padding = new Padding(20, 10, 20, 10);
            pnlPatientCard.Height = 118;

            lblPatientName.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblPatientName.ForeColor = Color.FromArgb(13, 71, 128);
            lblPatientName.Location = new Point(20, 8);
            lblPatientName.AutoSize = true;
            lblPatientName.Text = "Patient Name";

            int cx = 20, cy = 36;
            InfoLabel(lblPatientCode,   "Code: —",          cx,       cy);
            InfoLabel(lblPatientAge,    "Age: —",            cx + 140, cy);
            InfoLabel(lblPatientGender, "Gender: —",         cx + 250, cy);
            InfoLabel(lblBloodGroup,    "Blood Group: —",    cx + 380, cy);
            InfoLabel(lblPatientPhone,  "Phone: —",          cx + 530, cy);
            cy += 24;
            InfoLabel(lblDoctor,        "Doctor: —",         cx,       cy);
            InfoLabel(lblDept,          "Department: —",     cx + 260, cy);
            cy += 22;
            InfoLabel(lblApptDate,      "Appointment: —",    cx,       cy);
            InfoLabel(lblReason,        "Reason: —",         cx + 340, cy);

            // ── Clinical Input ─────────────────────────────────────────────────────
            pnlClinical.AutoScroll = true;
            pnlClinical.Controls.AddRange(new Control[] {
                lblSymptomsHdr, txtSymptoms,
                lblDiagnosisHdr, txtDiagnosis,
                lblTreatmentHdr, txtTreatment,
                lblNotesHdr, txtNotes,
                lblSaveStatus
            });
            pnlClinical.Dock = DockStyle.Fill;
            pnlClinical.Padding = new Padding(20, 12, 20, 8);

            int py = 12;

            ClinicalLabel(lblSymptomsHdr, "Presenting Symptoms: *", py);
            py += 22;
            txtSymptoms.Location = new Point(20, py);
            txtSymptoms.Size = new Size(780, 80);
            txtSymptoms.Multiline = true;
            txtSymptoms.ScrollBars = ScrollBars.Vertical;
            txtSymptoms.Font = new Font("Segoe UI", 10F);
            txtSymptoms.PlaceholderText = "Describe the patient's chief complaints and presenting symptoms…";
            py += 88;

            ClinicalLabel(lblDiagnosisHdr, "Diagnosis: *", py);
            py += 22;
            txtDiagnosis.Location = new Point(20, py);
            txtDiagnosis.Size = new Size(780, 70);
            txtDiagnosis.Multiline = true;
            txtDiagnosis.ScrollBars = ScrollBars.Vertical;
            txtDiagnosis.Font = new Font("Segoe UI", 10F);
            txtDiagnosis.PlaceholderText = "Clinical diagnosis…";
            py += 78;

            ClinicalLabel(lblTreatmentHdr, "Treatment Plan:", py);
            py += 22;
            txtTreatment.Location = new Point(20, py);
            txtTreatment.Size = new Size(780, 65);
            txtTreatment.Multiline = true;
            txtTreatment.ScrollBars = ScrollBars.Vertical;
            txtTreatment.Font = new Font("Segoe UI", 10F);
            txtTreatment.PlaceholderText = "Prescribed treatment, procedures, referrals…";
            py += 73;

            ClinicalLabel(lblNotesHdr, "Additional Notes:", py);
            py += 22;
            txtNotes.Location = new Point(20, py);
            txtNotes.Size = new Size(780, 55);
            txtNotes.Multiline = true;
            txtNotes.ScrollBars = ScrollBars.Vertical;
            txtNotes.Font = new Font("Segoe UI", 10F);
            txtNotes.PlaceholderText = "Internal notes, follow-up instructions…";
            py += 63;

            lblSaveStatus.AutoSize = false;
            lblSaveStatus.Location = new Point(20, py);
            lblSaveStatus.Size = new Size(780, 22);
            lblSaveStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSaveStatus.Text = string.Empty;

            // ── Footer ─────────────────────────────────────────────────────────────
            pnlFooter.BackColor = Color.FromArgb(240, 244, 248);
            pnlFooter.Controls.AddRange(new Control[] { btnSave, btnPrescribe, btnViewHistory, btnClear, btnClose });
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Height = 58;

            StyleFooterBtn(btnSave,        "💾  Save Record",          Color.FromArgb(13, 71, 128),    15,  150);
            StyleFooterBtn(btnPrescribe,   "💊  Prescribe Rx",         Color.FromArgb(20, 120, 70),   175,  150);
            StyleFooterBtn(btnViewHistory, "📋  Full History",         Color.FromArgb(80, 50, 130),   335,  140);
            StyleFooterBtn(btnClear,       "🗑  Clear",                Color.FromArgb(120, 80, 30),   485,   85);
            StyleFooterBtn(btnClose,       "✖  Close",                Color.FromArgb(150, 40, 40),   580,   85);

            btnSave.Click        += new System.EventHandler(btnSave_Click);
            btnPrescribe.Click   += new System.EventHandler(btnPrescribe_Click);
            btnViewHistory.Click += new System.EventHandler(btnViewHistory_Click);
            btnClear.Click       += new System.EventHandler(btnClear_Click);
            btnClose.Click       += new System.EventHandler(btnClose_Click);

            // ── Form ───────────────────────────────────────────────────────────────
            Controls.AddRange(new Control[] { pnlClinical, pnlPatientCard, pnlHeader, pnlFooter });
            ClientSize = new Size(840, 680);
            MinimumSize = new Size(800, 620);
            Name = "ConsultationForm";
            Text = "Clinical Consultation – MediCare";
            StartPosition = FormStartPosition.CenterParent;

            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlPatientCard.ResumeLayout(false);
            pnlPatientCard.PerformLayout();
            pnlClinical.ResumeLayout(false);
            pnlClinical.PerformLayout();
            pnlFooter.ResumeLayout(false);
            ResumeLayout(false);
        }

        private static void InfoLabel(Label lbl, string text, int x, int y)
        {
            lbl.AutoSize = true;
            lbl.Font = new Font("Segoe UI", 9F);
            lbl.ForeColor = Color.FromArgb(60, 70, 90);
            lbl.Location = new Point(x, y);
            lbl.Text = text;
        }

        private void ClinicalLabel(Label lbl, string text, int y)
        {
            lbl.AutoSize = true;
            lbl.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lbl.ForeColor = Color.FromArgb(30, 70, 130);
            lbl.Location = new Point(20, y);
            lbl.Text = text;
            pnlClinical.Controls.Add(lbl);
        }

        private static void StyleFooterBtn(Button btn, string text, Color color, int x, int w)
        {
            btn.Text = text;
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btn.Location = new Point(x, 13);
            btn.Size = new Size(w, 32);
            btn.Cursor = Cursors.Hand;
        }

        // Controls
        private Panel pnlHeader, pnlPatientCard, pnlClinical, pnlFooter;
        private Label lblFormTitle, lblPatientName, lblPatientCode, lblPatientAge, lblPatientGender,
                      lblBloodGroup, lblPatientPhone, lblDoctor, lblDept, lblApptDate, lblReason,
                      lblSymptomsHdr, lblDiagnosisHdr, lblTreatmentHdr, lblNotesHdr, lblSaveStatus;
        private TextBox txtSymptoms, txtDiagnosis, txtTreatment, txtNotes;
        private Button btnSave, btnPrescribe, btnViewHistory, btnClear, btnClose;

        #endregion
    }
}
