using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Services;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    /// <summary>
    /// Patient Medical History viewer with a split-view layout.
    /// Left: Chronological list of all past consultations.
    /// Right: Full clinical details of the selected visit.
    /// </summary>
    public partial class PatientMedicalHistoryForm : Form
    {
        private readonly MedicalRecordService _recordService = new();
        private List<MedicalRecord> _records = new();

        // ─────────────────────────────────────────────────────────────────────────
        // Constructor
        // ─────────────────────────────────────────────────────────────────────────

        public PatientMedicalHistoryForm(int patientId, string patientName)
        {
            InitializeComponent();
            Text = $"Medical History – {patientName} – MediCare";
            lblPatientInfo.Text = $"Patient: {patientName}";
            LoadHistory(patientId);
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Data Loading
        // ─────────────────────────────────────────────────────────────────────────

        private void LoadHistory(int patientId)
        {
            try
            {
                _records = _recordService.GetRecordsByPatientId(patientId);
                PopulateList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading patient history:\n{ex.Message}", "Load Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateList()
        {
            lstVisits.Items.Clear();
            foreach (var r in _records)
                lstVisits.Items.Add(new VisitListItem(r));

            lblVisitCount.Text = $"{_records.Count} visit(s) on record";

            if (lstVisits.Items.Count > 0)
                lstVisits.SelectedIndex = 0;
            else
                ClearDetailPanel();
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Event Handlers
        // ─────────────────────────────────────────────────────────────────────────

        private void lstVisits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstVisits.SelectedItem is not VisitListItem item) return;
            ShowDetail(item.Record);
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        // ─────────────────────────────────────────────────────────────────────────
        // Display helpers
        // ─────────────────────────────────────────────────────────────────────────

        private void ShowDetail(MedicalRecord r)
        {
            lblDetailDate.Text       = $"Visit Date: {r.VisitDate:dddd, dd MMMM yyyy – hh:mm tt}";
            lblDetailDoctor.Text     = $"Attending: {r.DoctorName}  |  {r.DepartmentName}";
            lblDetailApptID.Text     = r.AppointmentID.HasValue
                ? $"Linked Appointment #: {r.AppointmentID.Value}"
                : "Linked Appointment: — (standalone)";

            SetRichText(rtbSymptoms,  r.Symptoms  ?? "—");
            SetRichText(rtbDiagnosis, r.Diagnosis ?? "—");
            SetRichText(rtbTreatment, r.Treatment ?? "—");
            SetRichText(rtbNotes,     r.Notes     ?? "—");
        }

        private void ClearDetailPanel()
        {
            lblDetailDate.Text   = string.Empty;
            lblDetailDoctor.Text = string.Empty;
            lblDetailApptID.Text = string.Empty;
            rtbSymptoms.Clear();
            rtbDiagnosis.Clear();
            rtbTreatment.Clear();
            rtbNotes.Clear();
        }

        private static void SetRichText(RichTextBox rtb, string text)
        {
            rtb.Clear();
            rtb.SelectionColor = Color.FromArgb(30, 40, 60);
            rtb.AppendText(text);
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Visit List Item Wrapper
        // ─────────────────────────────────────────────────────────────────────────

        private sealed class VisitListItem
        {
            public MedicalRecord Record { get; }
            public VisitListItem(MedicalRecord r) => Record = r;
            public override string ToString() =>
                $"{Record.VisitDate:dd/MM/yyyy}   {Record.DoctorName}\n  Dx: {Truncate(Record.Diagnosis, 50)}";

            private static string Truncate(string? s, int max) =>
                s == null ? "—" : s.Length <= max ? s : s[..max] + "…";
        }
    }
}
