using System.Drawing;
using System.Windows.Forms;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    partial class PatientMedicalHistoryForm
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
            lblHeaderTitle  = new Label();
            lblPatientInfo  = new Label();
            lblVisitCount   = new Label();

            // Split container
            splitMain       = new SplitContainer();

            // Left: visit list
            pnlLeft         = new Panel();
            lblListTitle    = new Label();
            lstVisits       = new ListBox();

            // Right: detail
            pnlRight           = new Panel();
            lblDetailDate      = new Label();
            lblDetailDoctor    = new Label();
            lblDetailApptID    = new Label();
            pnlDetailScroll    = new Panel();
            lblSymptomsHdr     = new Label();
            rtbSymptoms        = new RichTextBox();
            lblDiagnosisHdr    = new Label();
            rtbDiagnosis       = new RichTextBox();
            lblTreatmentHdr    = new Label();
            rtbTreatment       = new RichTextBox();
            lblNotesHdr        = new Label();
            rtbNotes           = new RichTextBox();

            // Footer
            pnlFooter       = new Panel();
            btnClose        = new Button();

            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            pnlLeft.SuspendLayout();
            pnlRight.SuspendLayout();
            pnlDetailScroll.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();

            // ── Header ─────────────────────────────────────────────────────────────
            pnlHeader.BackColor = Color.FromArgb(20, 80, 140);
            pnlHeader.Controls.AddRange(new Control[] { lblHeaderTitle, lblPatientInfo, lblVisitCount });
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 76;
            pnlHeader.Padding = new Padding(20, 10, 20, 10);

            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(20, 10);
            lblHeaderTitle.Text = "📋  Patient Medical History";

            lblPatientInfo.AutoSize = true;
            lblPatientInfo.Font = new Font("Segoe UI", 10F);
            lblPatientInfo.ForeColor = Color.FromArgb(200, 225, 250);
            lblPatientInfo.Location = new Point(22, 42);
            lblPatientInfo.Text = "Patient: —";

            lblVisitCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblVisitCount.AutoSize = true;
            lblVisitCount.Font = new Font("Segoe UI", 9.5F, FontStyle.Italic);
            lblVisitCount.ForeColor = Color.FromArgb(200, 225, 250);
            lblVisitCount.Location = new Point(900, 46);
            lblVisitCount.Text = "0 visits";

            // ── Split Container ────────────────────────────────────────────────────
            splitMain.Dock = DockStyle.Fill;
            splitMain.SplitterDistance = 310;

            // Left panel
            pnlLeft.Controls.AddRange(new Control[] { lblListTitle, lstVisits });
            pnlLeft.Dock = DockStyle.Fill;
            pnlLeft.Padding = new Padding(0);

            lblListTitle.BackColor = Color.FromArgb(230, 240, 255);
            lblListTitle.Dock = DockStyle.Top;
            lblListTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblListTitle.ForeColor = Color.FromArgb(20, 60, 130);
            lblListTitle.Height = 30;
            lblListTitle.Padding = new Padding(8, 6, 0, 0);
            lblListTitle.Text = "Visit Timeline (newest first)";

            lstVisits.Dock = DockStyle.Fill;
            lstVisits.DrawMode = DrawMode.OwnerDrawVariable;
            lstVisits.ItemHeight = 48;
            lstVisits.Font = new Font("Segoe UI", 9F);
            lstVisits.BorderStyle = BorderStyle.None;
            lstVisits.SelectionMode = SelectionMode.One;
            lstVisits.SelectedIndexChanged += new System.EventHandler(lstVisits_SelectedIndexChanged);

            splitMain.Panel1.Controls.Add(pnlLeft);

            // Right panel
            pnlRight.Controls.AddRange(new Control[] { pnlDetailScroll, lblDetailApptID, lblDetailDoctor, lblDetailDate });
            pnlRight.Dock = DockStyle.Fill;
            pnlRight.Padding = new Padding(16, 12, 16, 8);

            lblDetailDate.AutoSize = true;
            lblDetailDate.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblDetailDate.ForeColor = Color.FromArgb(13, 71, 128);
            lblDetailDate.Location = new Point(16, 12);
            lblDetailDate.Text = string.Empty;

            lblDetailDoctor.AutoSize = true;
            lblDetailDoctor.Font = new Font("Segoe UI", 9.5F);
            lblDetailDoctor.ForeColor = Color.FromArgb(60, 80, 110);
            lblDetailDoctor.Location = new Point(16, 36);
            lblDetailDoctor.Text = string.Empty;

            lblDetailApptID.AutoSize = true;
            lblDetailApptID.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            lblDetailApptID.ForeColor = Color.Gray;
            lblDetailApptID.Location = new Point(16, 56);
            lblDetailApptID.Text = string.Empty;

            pnlDetailScroll.AutoScroll = true;
            pnlDetailScroll.Dock = DockStyle.Fill;
            pnlDetailScroll.Padding = new Padding(0, 70, 0, 0);

            int dy = 6;
            AddDetailSection(pnlDetailScroll, lblSymptomsHdr,  "Symptoms",        ref dy, rtbSymptoms,  80);
            AddDetailSection(pnlDetailScroll, lblDiagnosisHdr, "Diagnosis",       ref dy, rtbDiagnosis, 70);
            AddDetailSection(pnlDetailScroll, lblTreatmentHdr, "Treatment Plan",  ref dy, rtbTreatment, 70);
            AddDetailSection(pnlDetailScroll, lblNotesHdr,     "Additional Notes",ref dy, rtbNotes,     60);

            splitMain.Panel2.Controls.Add(pnlRight);

            // ── Footer ─────────────────────────────────────────────────────────────
            pnlFooter.BackColor = Color.FromArgb(240, 244, 248);
            pnlFooter.Controls.Add(btnClose);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Height = 50;

            btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClose.Location = new Point(430, 10);
            btnClose.Size = new Size(140, 32);
            btnClose.Text = "✖  Close";
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.BackColor = Color.FromArgb(13, 71, 128);
            btnClose.ForeColor = Color.White;
            btnClose.Cursor = Cursors.Hand;
            btnClose.Click += new System.EventHandler(btnClose_Click);

            // ── Form ───────────────────────────────────────────────────────────────
            Controls.AddRange(new Control[] { splitMain, pnlHeader, pnlFooter });
            ClientSize = new Size(1000, 640);
            MinimumSize = new Size(860, 560);
            Name = "PatientMedicalHistoryForm";
            Text = "Medical History – MediCare";
            StartPosition = FormStartPosition.CenterParent;

            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            pnlLeft.ResumeLayout(false);
            pnlRight.ResumeLayout(false);
            pnlDetailScroll.ResumeLayout(false);
            pnlFooter.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void AddDetailSection(Panel parent, Label hdrLbl, string title, ref int y, RichTextBox rtb, int height)
        {
            hdrLbl.AutoSize = true;
            hdrLbl.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            hdrLbl.ForeColor = Color.FromArgb(20, 70, 140);
            hdrLbl.Location = new Point(0, y);
            hdrLbl.Text = title;
            parent.Controls.Add(hdrLbl);
            y += 22;

            rtb.Location = new Point(0, y);
            rtb.Size = new Size(610, height);
            rtb.ReadOnly = true;
            rtb.BorderStyle = BorderStyle.FixedSingle;
            rtb.BackColor = Color.FromArgb(252, 254, 255);
            rtb.Font = new Font("Segoe UI", 9.5F);
            rtb.ScrollBars = RichTextBoxScrollBars.Vertical;
            parent.Controls.Add(rtb);
            y += height + 14;
        }

        // Controls
        private Panel pnlHeader, pnlLeft, pnlRight, pnlDetailScroll, pnlFooter;
        private Label lblHeaderTitle, lblPatientInfo, lblVisitCount,
                      lblListTitle, lblDetailDate, lblDetailDoctor, lblDetailApptID,
                      lblSymptomsHdr, lblDiagnosisHdr, lblTreatmentHdr, lblNotesHdr;
        private ListBox lstVisits;
        private SplitContainer splitMain;
        private RichTextBox rtbSymptoms, rtbDiagnosis, rtbTreatment, rtbNotes;
        private Button btnClose;

        #endregion
    }
}
