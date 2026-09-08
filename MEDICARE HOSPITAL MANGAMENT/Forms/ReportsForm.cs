using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Repositories;
using MEDICARE_HOSPITAL_MANGAMENT.Services;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    /// <summary>
    /// Multi-Criteria Analytical Reporting and Data Export Module.
    /// Provides detailed clinical, workload, inventory, and revenue analytics
    /// with customizable date filters, CSV exports, and print previews.
    /// </summary>
    public partial class ReportsForm : Form
    {
        private readonly ReportService _reportService = new();
        private readonly DoctorRepository _doctorRepo = new();

        // Cached report datasets for CSV export and Print Preview
        private object? _currentData;
        private int _currentReportIndex = 0;

        public ReportsForm()
        {
            InitializeComponent();
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            // Default date range: First of current month to today
            var today = DateTime.Today;
            dtpFrom.Value = new DateTime(today.Year, today.Month, 1);
            dtpTo.Value = today;

            LoadFilterData();

            // Default to Appointments Analytics
            cmbReportType.SelectedIndex = 0;
        }

        private void LoadFilterData()
        {
            try
            {
                var doctors = _doctorRepo.GetAllDoctors(activeOnly: true);
                cmbFilter1.Items.Clear();
                cmbFilter1.Items.Add(new ComboItem("All Doctors", 0));
                foreach (var d in doctors)
                {
                    cmbFilter1.Items.Add(new ComboItem(d.FullName, d.DoctorID));
                }
                cmbFilter1.DisplayMember = "Display";
                if (cmbFilter1.Items.Count > 0) cmbFilter1.SelectedIndex = 0;
            }
            catch
            {
                cmbFilter1.Items.Clear();
                cmbFilter1.Items.Add(new ComboItem("All Doctors", 0));
                cmbFilter1.SelectedIndex = 0;
            }
        }

        private void CmbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentReportIndex = cmbReportType.SelectedIndex;
            AdjustFilterControls();
            GenerateCurrentReport();
        }

        private void AdjustFilterControls()
        {
            switch (_currentReportIndex)
            {
                case 0: // Appointments Analytics
                    lblFrom.Visible = dtpFrom.Visible = true;
                    lblTo.Visible = dtpTo.Visible = true;
                    btnPresetToday.Visible = btnPresetWeek.Visible = btnPresetMonth.Visible = btnPresetAll.Visible = true;

                    lblFilter1.Visible = cmbFilter1.Visible = true;
                    lblFilter1.Text = "Doctor:";

                    lblFilter2.Visible = cmbFilter2.Visible = true;
                    lblFilter2.Text = "Status:";
                    cmbFilter2.Items.Clear();
                    cmbFilter2.Items.AddRange(new object[] { "All", "Scheduled", "Completed", "Cancelled" });
                    cmbFilter2.SelectedIndex = 0;

                    chkOption.Visible = false;
                    break;

                case 1: // Doctor Workload
                    lblFrom.Visible = dtpFrom.Visible = true;
                    lblTo.Visible = dtpTo.Visible = true;
                    btnPresetToday.Visible = btnPresetWeek.Visible = btnPresetMonth.Visible = btnPresetAll.Visible = true;

                    lblFilter1.Visible = cmbFilter1.Visible = false;
                    lblFilter2.Visible = cmbFilter2.Visible = false;
                    chkOption.Visible = false;
                    break;

                case 2: // Pharmacy Inventory & Valuation
                    // Inventory is real-time point in time
                    lblFrom.Visible = dtpFrom.Visible = false;
                    lblTo.Visible = dtpTo.Visible = false;
                    btnPresetToday.Visible = btnPresetWeek.Visible = btnPresetMonth.Visible = btnPresetAll.Visible = false;

                    lblFilter1.Visible = cmbFilter1.Visible = false;

                    lblFilter2.Visible = cmbFilter2.Visible = true;
                    lblFilter2.Text = "Category:";
                    cmbFilter2.Items.Clear();
                    cmbFilter2.Items.AddRange(new object[] { "All", "Antibiotic", "Analgesic", "Antihistamine", "Antiviral", "Cardiovascular", "Gastrointestinal", "Other" });
                    cmbFilter2.SelectedIndex = 0;

                    chkOption.Visible = true;
                    chkOption.Text = "Below Reorder Level Only";
                    chkOption.Checked = false;
                    break;

                case 3: // Hospital Revenue & Invoicing
                    lblFrom.Visible = dtpFrom.Visible = true;
                    lblTo.Visible = dtpTo.Visible = true;
                    btnPresetToday.Visible = btnPresetWeek.Visible = btnPresetMonth.Visible = btnPresetAll.Visible = true;

                    lblFilter1.Visible = cmbFilter1.Visible = false;

                    lblFilter2.Visible = cmbFilter2.Visible = true;
                    lblFilter2.Text = "Status:";
                    cmbFilter2.Items.Clear();
                    cmbFilter2.Items.AddRange(new object[] { "All", "Pending", "Partially Paid", "Paid" });
                    cmbFilter2.SelectedIndex = 0;

                    chkOption.Visible = false;
                    break;
            }
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Date Presets
        // ─────────────────────────────────────────────────────────────────────────

        private void BtnPresetToday_Click(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Today;
            dtpTo.Value = DateTime.Today;
            GenerateCurrentReport();
        }

        private void BtnPresetWeek_Click(object sender, EventArgs e)
        {
            var today = DateTime.Today;
            int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
            dtpFrom.Value = today.AddDays(-1 * diff);
            dtpTo.Value = today;
            GenerateCurrentReport();
        }

        private void BtnPresetMonth_Click(object sender, EventArgs e)
        {
            var today = DateTime.Today;
            dtpFrom.Value = new DateTime(today.Year, today.Month, 1);
            dtpTo.Value = today;
            GenerateCurrentReport();
        }

        private void BtnPresetAll_Click(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Today.AddYears(-5);
            dtpTo.Value = DateTime.Today;
            GenerateCurrentReport();
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Report Execution
        // ─────────────────────────────────────────────────────────────────────────

        private void BtnGenerate_Click(object sender, EventArgs e) => GenerateCurrentReport();

        private void GenerateCurrentReport()
        {
            dgvResults.DataSource = null;
            dgvResults.Columns.Clear();
            dgvResults.AutoGenerateColumns = false;

            switch (_currentReportIndex)
            {
                case 0:
                    LoadAppointmentReport();
                    break;
                case 1:
                    LoadDoctorWorkloadReport();
                    break;
                case 2:
                    LoadPharmacyStockReport();
                    break;
                case 3:
                    LoadRevenueReport();
                    break;
            }
        }

        private void LoadAppointmentReport()
        {
            int? doctorId = null;
            if (cmbFilter1.SelectedItem is ComboItem ci && ci.Value > 0) doctorId = ci.Value;

            string? status = cmbFilter2.SelectedItem?.ToString();
            if (status == "All") status = null;

            var list = _reportService.GenerateAppointmentReport(
                dtpFrom.Value.Date, dtpTo.Value.Date, doctorId, status, out string err);

            if (!string.IsNullOrEmpty(err) && list == null)
            {
                MessageBox.Show(err, "Report Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            list ??= new List<AppointmentReportItem>();
            _currentData = list;

            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Date", HeaderText = "Date", Width = 95, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TimeSlot", HeaderText = "Time", Width = 95 });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PatientCode", HeaderText = "Patient Code", Width = 110 });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PatientName", HeaderText = "Patient Name", Width = 160 });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DoctorName", HeaderText = "Doctor", Width = 160 });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DepartmentName", HeaderText = "Department", Width = 130 });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "Status", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Reason", HeaderText = "Clinical Reason", Width = 200 });

            dgvResults.DataSource = list;

            int completed = list.Count(a => a.Status == "Completed");
            int cancelled = list.Count(a => a.Status == "Cancelled");
            lblRecordCount.Text = $"Total Appointments: {list.Count:N0}";
            lblSummaryMetric1.Text = $"Completed: {completed} | Scheduled: {list.Count - completed - cancelled} | Cancelled: {cancelled}";
            lblSummaryMetric2.Text = list.Count > 0 ? $"Completion Rate: {((double)completed / list.Count * 100):F1}%" : "";
        }

        private void LoadDoctorWorkloadReport()
        {
            var list = _reportService.GenerateDoctorWorkloadReport(
                dtpFrom.Value.Date, dtpTo.Value.Date, out string err);

            if (!string.IsNullOrEmpty(err) && list == null)
            {
                MessageBox.Show(err, "Report Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            list ??= new List<DoctorWorkloadReportItem>();
            _currentData = list;

            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DoctorCode", HeaderText = "Doctor Code", Width = 120 });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DoctorName", HeaderText = "Doctor Name", Width = 220 });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DepartmentName", HeaderText = "Department", Width = 180 });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TotalAppointments", HeaderText = "Total Bookings", Width = 130, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CompletedConsultations", HeaderText = "Completed", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CancelledAppointments", HeaderText = "Cancelled", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });

            dgvResults.DataSource = list;

            int totalAppts = list.Sum(d => d.TotalAppointments);
            int totalComp = list.Sum(d => d.CompletedConsultations);
            lblRecordCount.Text = $"Doctors: {list.Count:N0}";
            lblSummaryMetric1.Text = $"Total Appointments: {totalAppts:N0} | Completed Consultations: {totalComp:N0}";
            lblSummaryMetric2.Text = totalAppts > 0 ? $"Overall Completion Rate: {((double)totalComp / totalAppts * 100):F1}%" : "";
        }

        private void LoadPharmacyStockReport()
        {
            bool lowStockOnly = chkOption.Checked;
            string? category = cmbFilter2.SelectedItem?.ToString();
            if (category == "All") category = null;

            var list = _reportService.GenerateStockReport(lowStockOnly, category);
            _currentData = list;

            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MedicineCode", HeaderText = "Code", Width = 90 });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MedicineName", HeaderText = "Medicine Name", Width = 180 });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Category", HeaderText = "Category", Width = 120 });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Unit", HeaderText = "Unit", Width = 80 });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "UnitPrice", HeaderText = "Unit Price", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StockQuantity", HeaderText = "Stock Qty", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ReorderLevel", HeaderText = "Reorder Lvl", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TotalStockValue", HeaderText = "Total Value", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StatusText", HeaderText = "Stock Status", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });

            dgvResults.DataSource = list;

            decimal totalVal = list.Sum(s => s.TotalStockValue);
            int lowStockCount = list.Count(s => s.StockQuantity <= s.ReorderLevel);

            lblRecordCount.Text = $"Medicines: {list.Count:N0}";
            lblSummaryMetric1.Text = $"Inventory Valuation: Rs. {totalVal:N2}";
            lblSummaryMetric2.Text = lowStockCount > 0 ? $"⚠️ Shortage Alert: {lowStockCount} below reorder level" : "✅ All stock healthy";
        }

        private void LoadRevenueReport()
        {
            string? status = cmbFilter2.SelectedItem?.ToString();
            if (status == "All") status = null;

            var list = _reportService.GenerateRevenueReport(
                dtpFrom.Value.Date, dtpTo.Value.Date, status, out string err);

            if (!string.IsNullOrEmpty(err) && list == null)
            {
                MessageBox.Show(err, "Report Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            list ??= new List<RevenueReportItem>();
            _currentData = list;

            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "BillID", HeaderText = "Bill #", Width = 70 });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "BillDate", HeaderText = "Date", Width = 95, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PatientCode", HeaderText = "Patient Code", Width = 110 });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PatientName", HeaderText = "Patient Name", Width = 170 });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Subtotal", HeaderText = "Subtotal", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Discount", HeaderText = "Discount", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TotalAmount", HeaderText = "Net Total", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TotalPaid", HeaderText = "Collected", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "OutstandingBalance", HeaderText = "Outstanding", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "Status", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });

            dgvResults.DataSource = list;

            decimal totalNet = list.Sum(b => b.TotalAmount);
            decimal totalPaid = list.Sum(b => b.TotalPaid);
            decimal totalDue = list.Sum(b => b.OutstandingBalance);

            lblRecordCount.Text = $"Invoices: {list.Count:N0}";
            lblSummaryMetric1.Text = $"Billed: Rs. {totalNet:N2} | Collected: Rs. {totalPaid:N2}";
            lblSummaryMetric2.Text = $"Outstanding Due: Rs. {totalDue:N2}";
        }

        // ─────────────────────────────────────────────────────────────────────────
        // CSV Export
        // ─────────────────────────────────────────────────────────────────────────

        private void BtnExportCsv_Click(object sender, EventArgs e)
        {
            if (_currentData == null)
            {
                MessageBox.Show("Please generate a report first before exporting.", "Export Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string reportName = cmbReportType.SelectedItem?.ToString() ?? "Report";
            string defaultFilename = $"{reportName.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

            using var sfd = new SaveFileDialog
            {
                Filter = "CSV Spreadsheet (*.csv)|*.csv|All Files (*.*)|*.*",
                FileName = defaultFilename,
                Title = "Export Report Data to CSV"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                string csvContent = string.Empty;

                switch (_currentReportIndex)
                {
                    case 0:
                        var appts = (List<AppointmentReportItem>)_currentData;
                        csvContent = _reportService.ExportToCsv(reportName,
                            new[] { "Date", "Time", "PatientCode", "PatientName", "Doctor", "Department", "Status", "Reason" },
                            appts.Select(a => new[] {
                                a.Date.ToString("dd/MM/yyyy"), a.TimeSlot, a.PatientCode, a.PatientName,
                                a.DoctorName, a.DepartmentName, a.Status, a.Reason ?? ""
                            }));
                        break;

                    case 1:
                        var workload = (List<DoctorWorkloadReportItem>)_currentData;
                        csvContent = _reportService.ExportToCsv(reportName,
                            new[] { "DoctorCode", "DoctorName", "Department", "TotalAppointments", "CompletedConsultations", "CancelledAppointments" },
                            workload.Select(w => new[] {
                                w.DoctorCode, w.DoctorName, w.DepartmentName,
                                w.TotalAppointments.ToString(), w.CompletedConsultations.ToString(), w.CancelledAppointments.ToString()
                            }));
                        break;

                    case 2:
                        var stock = (List<StockReportItem>)_currentData;
                        csvContent = _reportService.ExportToCsv(reportName,
                            new[] { "MedicineCode", "MedicineName", "Category", "Unit", "UnitPrice", "StockQuantity", "ReorderLevel", "TotalStockValue", "Status" },
                            stock.Select(s => new[] {
                                s.MedicineCode, s.MedicineName, s.Category, s.Unit,
                                s.UnitPrice.ToString("F2"), s.StockQuantity.ToString(), s.ReorderLevel.ToString(),
                                s.TotalStockValue.ToString("F2"), s.StatusText
                            }));
                        break;

                    case 3:
                        var rev = (List<RevenueReportItem>)_currentData;
                        csvContent = _reportService.ExportToCsv(reportName,
                            new[] { "BillID", "Date", "PatientCode", "PatientName", "Subtotal", "Discount", "TotalAmount", "TotalPaid", "OutstandingBalance", "Status" },
                            rev.Select(r => new[] {
                                r.BillID.ToString(), r.BillDate.ToString("dd/MM/yyyy"), r.PatientCode, r.PatientName,
                                r.Subtotal.ToString("F2"), r.Discount.ToString("F2"), r.TotalAmount.ToString("F2"),
                                r.TotalPaid.ToString("F2"), r.OutstandingBalance.ToString("F2"), r.Status
                            }));
                        break;
                }

                File.WriteAllText(sfd.FileName, csvContent, System.Text.Encoding.UTF8);

                MessageBox.Show($"Report exported successfully!\n\nLocation: {sfd.FileName}",
                    "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to export report:\n{ex.Message}", "Export Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Print Preview
        // ─────────────────────────────────────────────────────────────────────────

        private void BtnPrintPreview_Click(object sender, EventArgs e)
        {
            if (dgvResults.Rows.Count == 0)
            {
                MessageBox.Show("No data rows available to print.", "Print Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using var printDoc = new PrintDocument();
                printDoc.DocumentName = cmbReportType.SelectedItem?.ToString() ?? "MediCare Hospital Report";
                printDoc.DefaultPageSettings.Landscape = true;
                printDoc.PrintPage += PrintDocument_PrintPage;

                using var previewDlg = new PrintPreviewDialog
                {
                    Document = printDoc,
                    Width = 950,
                    Height = 700,
                    StartPosition = FormStartPosition.CenterParent
                };

                previewDlg.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not render print preview:\n{ex.Message}", "Print Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            var g = e.Graphics;
            if (g == null) return;

            using var titleFont = new Font("Segoe UI", 16, FontStyle.Bold);
            using var subFont = new Font("Segoe UI", 9, FontStyle.Regular);
            using var headerFont = new Font("Segoe UI Semibold", 9, FontStyle.Bold);
            using var cellFont = new Font("Segoe UI", 8.5f, FontStyle.Regular);
            using var summaryFont = new Font("Segoe UI Semibold", 9.5f, FontStyle.Bold);

            using var headerBrush = new SolidBrush(Color.FromArgb(24, 49, 83));
            using var altRowBrush = new SolidBrush(Color.FromArgb(245, 247, 250));
            using var textBrush = new SolidBrush(Color.FromArgb(30, 41, 59));
            using var borderPen = new Pen(Color.FromArgb(203, 213, 225));

            int marginX = e.MarginBounds.Left - 50;
            int currentY = e.MarginBounds.Top - 50;
            int totalWidth = e.MarginBounds.Width + 100;

            // Report Header
            g.DrawString("MediCare Hospital Management System", titleFont, headerBrush, marginX, currentY);
            currentY += 28;

            string subText = $"{cmbReportType.SelectedItem}  |  Generated: {DateTime.Now:dd/MM/yyyy HH:mm}  |  By: {SessionManager.CurrentUser?.Username ?? "User"}";
            g.DrawString(subText, subFont, Brushes.Gray, marginX, currentY);
            currentY += 20;

            g.DrawLine(Pens.LightGray, marginX, currentY, marginX + totalWidth, currentY);
            currentY += 12;

            // Table Headers
            int colsCount = dgvResults.Columns.Count;
            int colWidth = totalWidth / Math.Max(1, colsCount);
            int rowHeight = 22;

            g.FillRectangle(new SolidBrush(Color.FromArgb(240, 243, 246)), marginX, currentY, totalWidth, rowHeight);
            g.DrawRectangle(borderPen, marginX, currentY, totalWidth, rowHeight);

            for (int i = 0; i < colsCount; i++)
            {
                var rect = new Rectangle(marginX + (i * colWidth), currentY, colWidth, rowHeight);
                g.DrawString(dgvResults.Columns[i].HeaderText, headerFont, headerBrush, rect);
            }
            currentY += rowHeight;

            // Table Rows
            int maxRowsOnPage = (e.MarginBounds.Bottom - currentY - 50) / rowHeight;
            int rowLimit = Math.Min(dgvResults.Rows.Count, maxRowsOnPage);

            for (int r = 0; r < rowLimit; r++)
            {
                if (r % 2 == 1)
                {
                    g.FillRectangle(altRowBrush, marginX, currentY, totalWidth, rowHeight);
                }

                for (int c = 0; c < colsCount; c++)
                {
                    var val = dgvResults.Rows[r].Cells[c].FormattedValue?.ToString() ?? "";
                    var rect = new Rectangle(marginX + (c * colWidth), currentY, colWidth, rowHeight);
                    g.DrawString(val, cellFont, textBrush, rect);
                }

                g.DrawLine(Pens.WhiteSmoke, marginX, currentY + rowHeight, marginX + totalWidth, currentY + rowHeight);
                currentY += rowHeight;
            }

            currentY += 10;
            g.DrawLine(Pens.Gray, marginX, currentY, marginX + totalWidth, currentY);
            currentY += 8;

            // Summary text
            string sumText = $"{lblRecordCount.Text}   |   {lblSummaryMetric1.Text}   |   {lblSummaryMetric2.Text}";
            g.DrawString(sumText, summaryFont, headerBrush, marginX, currentY);

            e.HasMorePages = false;
        }

        private sealed class ComboItem
        {
            public string Display { get; }
            public int Value { get; }
            public ComboItem(string display, int value) { Display = display; Value = value; }
            public override string ToString() => Display;
        }
    }
}
