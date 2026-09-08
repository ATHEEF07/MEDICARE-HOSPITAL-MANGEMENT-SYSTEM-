using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Services;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    /// <summary>
    /// Audit log and transaction history of hospital cashier payments.
    /// Supports multi-parameter filtering, collection summaries, and receipt reprints.
    /// </summary>
    public partial class PaymentHistoryForm : Form
    {
        private readonly PaymentService _paymentService = new();
        private List<Payment> _allPayments = new();

        public PaymentHistoryForm()
        {
            InitializeComponent();
        }

        private void PaymentHistoryForm_Load(object sender, EventArgs e)
        {
            SetupGridColumns();
            cmbMethodFilter.SelectedIndex = 0; // "All Methods"
            dtpFrom.Value = DateTime.Today.AddDays(-30);
            dtpTo.Value = DateTime.Today.AddDays(1);
            LoadPayments();
        }

        private void SetupGridColumns()
        {
            dgvPayments.AutoGenerateColumns = false;
            dgvPayments.Columns.Clear();

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ReferenceNo",
                HeaderText = "Receipt Ref",
                Width = 115
            });

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PaymentDate",
                HeaderText = "Date & Time",
                Width = 140,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            });

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BillID",
                HeaderText = "Inv #",
                Width = 70
            });

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PatientName",
                HeaderText = "Patient Name",
                Width = 180
            });

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PatientCode",
                HeaderText = "Code",
                Width = 90
            });

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Amount",
                HeaderText = "Amount (Rs.)",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PaymentMethod",
                HeaderText = "Method",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ReceivedByUserName",
                HeaderText = "Cashier Name",
                Width = 160
            });
        }

        private void LoadPayments()
        {
            _allPayments = _paymentService.GetAllPayments(500);
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            string term = txtSearch.Text.Trim().ToLowerInvariant();
            string method = cmbMethodFilter.SelectedItem?.ToString() ?? "All Methods";
            DateTime from = dtpFrom.Value.Date;
            DateTime to = dtpTo.Value.Date.AddDays(1).AddTicks(-1);

            var filtered = _allPayments.Where(p =>
            {
                bool matchesMethod = method == "All Methods" || string.Equals(p.PaymentMethod, method, StringComparison.OrdinalIgnoreCase);
                bool matchesDate = p.PaymentDate >= from && p.PaymentDate <= to;
                bool matchesSearch = string.IsNullOrWhiteSpace(term)
                    || p.PatientName.ToLowerInvariant().Contains(term)
                    || p.PatientCode.ToLowerInvariant().Contains(term)
                    || (p.ReferenceNo != null && p.ReferenceNo.ToLowerInvariant().Contains(term))
                    || p.ReceivedByUserName.ToLowerInvariant().Contains(term)
                    || p.BillID.ToString().Contains(term);

                return matchesMethod && matchesDate && matchesSearch;
            }).ToList();

            dgvPayments.DataSource = filtered;
            lblCount.Text = $"Showing {filtered.Count} transaction(s)";

            // Metric Summaries
            decimal totalRevenue = filtered.Sum(p => p.Amount);
            decimal cashRevenue = filtered.Where(p => p.PaymentMethod == "Cash").Sum(p => p.Amount);
            decimal cardRevenue = filtered.Where(p => p.PaymentMethod == "Card").Sum(p => p.Amount);
            decimal bankRevenue = filtered.Where(p => p.PaymentMethod == "BankTransfer").Sum(p => p.Amount);

            lblCardTotalVal.Text = $"Rs. {totalRevenue:N2}";
            lblCardCashVal.Text = $"Rs. {cashRevenue:N2}";
            lblCardCardVal.Text = $"Rs. {cardRevenue:N2}";
            lblCardBankVal.Text = $"Rs. {bankRevenue:N2}";
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Actions
        // ─────────────────────────────────────────────────────────────────────────

        private void BtnPrintReceipt_Click(object? sender, EventArgs e)
        {
            if (dgvPayments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a payment transaction from the grid to view its receipt.",
                    "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var payment = dgvPayments.SelectedRows[0].DataBoundItem as Payment;
            if (payment == null) return;

            var sb = new StringBuilder();
            sb.AppendLine("===============================================================");
            sb.AppendLine("                 MEDICARE HOSPITAL MANAGEMENT                  ");
            sb.AppendLine("                 OFFICIAL PAYMENT RECEIPT                      ");
            sb.AppendLine("                      (DUPLICATE COPY)                         ");
            sb.AppendLine("===============================================================");
            sb.AppendLine($" Receipt No       : {payment.ReferenceNo ?? $"RCP-{payment.PaymentID:D5}"}");
            sb.AppendLine($" Payment Date     : {payment.PaymentDate:dd MMMM yyyy HH:mm:ss}");
            sb.AppendLine($" Invoice Ref      : INV-{payment.BillID:D5}");
            sb.AppendLine($" Cashier Name     : {payment.ReceivedByUserName}");
            sb.AppendLine("---------------------------------------------------------------");
            sb.AppendLine($" Patient Code     : {payment.PatientCode}");
            sb.AppendLine($" Patient Name     : {payment.PatientName}");
            sb.AppendLine($" Service Notes    : {payment.BillDescription}");
            sb.AppendLine("---------------------------------------------------------------");
            sb.AppendLine(" TRANSACTION DETAILS:");
            sb.AppendLine($"   Original Bill  : Rs. {payment.BillTotalAmount,12:N2}");
            sb.AppendLine($"   Payment Method : {payment.PaymentMethod}");
            sb.AppendLine($"   AMOUNT PAID    : Rs. {payment.Amount,12:N2}");
            sb.AppendLine($"   Remaining Due  : Rs. {payment.RemainingBalanceAfterPayment,12:N2}");
            sb.AppendLine("===============================================================");
            sb.AppendLine(" Verified & Generated from Hospital Payment Audit System");
            sb.AppendLine(" Helpline: +94 11 234 5678  |  Web: www.medicarehospital.lk");
            sb.AppendLine("===============================================================");

            using var previewForm = new Form
            {
                Text = $"Duplicate Receipt - {payment.ReferenceNo}",
                Width = 620,
                Height = 600,
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.White
            };

            var txtReceipt = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Consolas", 10F),
                BackColor = Color.White,
                Text = sb.ToString()
            };

            var pnlBtn = new Panel { Dock = DockStyle.Bottom, Height = 50, BackColor = Color.FromArgb(245, 248, 252) };
            var btnCloseReceipt = new Button
            {
                Text = "Close Receipt",
                Location = new Point(480, 10),
                Size = new Size(115, 32),
                BackColor = Color.FromArgb(20, 80, 140),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCloseReceipt.Click += (s, ev) => previewForm.Close();
            pnlBtn.Controls.Add(btnCloseReceipt);

            previewForm.Controls.Add(txtReceipt);
            previewForm.Controls.Add(pnlBtn);
            previewForm.ShowDialog();
        }

        private void TxtSearch_TextChanged(object? sender, EventArgs e) => ApplyFilters();
        private void CmbMethodFilter_SelectedIndexChanged(object? sender, EventArgs e) => ApplyFilters();
        private void BtnApplyFilters_Click(object? sender, EventArgs e) => ApplyFilters();
        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbMethodFilter.SelectedIndex = 0;
            dtpFrom.Value = DateTime.Today.AddDays(-30);
            dtpTo.Value = DateTime.Today.AddDays(1);
            LoadPayments();
        }
    }
}
