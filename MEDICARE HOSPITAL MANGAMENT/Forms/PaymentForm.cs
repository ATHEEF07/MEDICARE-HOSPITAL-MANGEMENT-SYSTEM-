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
    /// Cashier Payment Processing Form.
    /// Collects payments, prevents overpayment (BR-10), updates bill statuses atomically,
    /// and issues official patient receipts.
    /// </summary>
    public partial class PaymentForm : Form
    {
        private readonly PaymentService _paymentService = new();
        private readonly BillingService _billingService = new();

        private readonly int? _preselectedBillId;
        private Bill? _currentBill;
        private Payment? _latestPayment;

        public PaymentForm()
        {
            InitializeComponent();
        }

        public PaymentForm(int billId)
        {
            _preselectedBillId = billId;
            InitializeComponent();
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {
            SetupHistoryGrid();
            cmbPaymentMethod.SelectedIndex = 0; // Default Cash
            LoadPendingBills();

            // Authorization check
            if (!SessionManager.IsCashier() && !SessionManager.IsAdmin())
            {
                btnProcessPayment.Enabled = false;
                MessageBox.Show("Payment collection is restricted to Cashiers and Administrators.",
                    "View Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SetupHistoryGrid()
        {
            dgvPreviousPayments.AutoGenerateColumns = false;
            dgvPreviousPayments.Columns.Clear();

            dgvPreviousPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PaymentID",
                HeaderText = "Rcpt #",
                Width = 70
            });

            dgvPreviousPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PaymentDate",
                HeaderText = "Date & Time",
                Width = 140,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            });

            dgvPreviousPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Amount",
                HeaderText = "Amount (Rs.)",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvPreviousPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PaymentMethod",
                HeaderText = "Method",
                Width = 100
            });

            dgvPreviousPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ReferenceNo",
                HeaderText = "Reference",
                Width = 130
            });

            dgvPreviousPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ReceivedByUserName",
                HeaderText = "Cashier",
                Width = 150
            });
        }

        private void LoadPendingBills()
        {
            var pendingBills = _billingService.GetPendingBills();

            // If a specific bill was preselected but is already paid, ensure it's loaded anyway
            if (_preselectedBillId.HasValue && !pendingBills.Any(b => b.BillID == _preselectedBillId.Value))
            {
                var specificBill = _billingService.GetBillById(_preselectedBillId.Value);
                if (specificBill != null)
                {
                    pendingBills.Insert(0, specificBill);
                }
            }

            var displayList = pendingBills.Select(b => new
            {
                b.BillID,
                DisplayText = $"INV-{b.BillID:D5} | {b.PatientName} – Due: Rs. {b.OutstandingBalance:N2} ({b.Status})"
            }).ToList();

            cmbBill.DataSource = displayList;
            cmbBill.DisplayMember = "DisplayText";
            cmbBill.ValueMember = "BillID";

            if (_preselectedBillId.HasValue && _preselectedBillId.Value > 0)
            {
                cmbBill.SelectedValue = _preselectedBillId.Value;
                cmbBill.Enabled = false; // Locked to preselected invoice
            }
        }

        private void CmbBill_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbBill.SelectedValue != null && int.TryParse(cmbBill.SelectedValue.ToString(), out int billId))
            {
                LoadBillDetails(billId);
            }
        }

        private void LoadBillDetails(int billId)
        {
            _currentBill = _billingService.GetBillById(billId);
            if (_currentBill == null) return;

            lblPatientName.Text = $"Patient: {_currentBill.PatientName}";
            lblPatientCode.Text = $"Code: {_currentBill.PatientCode}";
            lblBillDate.Text = $"Bill Date: {_currentBill.BillDate:dd MMMM yyyy}";
            lblBillDescription.Text = $"Notes: {(!string.IsNullOrWhiteSpace(_currentBill.Description) ? _currentBill.Description : "No description")}";

            lblMetricTotalVal.Text = $"Rs. {_currentBill.TotalAmount:N2}";
            lblMetricPaidVal.Text = $"Rs. {_currentBill.TotalPaid:N2}";
            lblMetricDueVal.Text = $"Rs. {_currentBill.OutstandingBalance:N2}";

            // Pre-fill payment amount with remaining due balance
            txtPaymentAmount.Text = _currentBill.OutstandingBalance.ToString("0.00");
            txtReferenceNo.Text = _paymentService.GenerateNextReferenceNo();

            // Load past payments
            var history = _paymentService.GetPaymentsByBillId(billId);
            dgvPreviousPayments.DataSource = history;
            if (history.Count > 0)
            {
                _latestPayment = history.Last();
            }

            // Button states
            bool canCollect = SessionManager.IsCashier() || SessionManager.IsAdmin();
            bool hasBalance = _currentBill.OutstandingBalance > 0;
            btnProcessPayment.Enabled = canCollect && hasBalance;

            if (!hasBalance)
            {
                lblMetricDueVal.ForeColor = Color.FromArgb(30, 130, 50); // Green when settled
            }
            else
            {
                lblMetricDueVal.ForeColor = Color.FromArgb(180, 40, 20); // Red when due
            }
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Payment Processing
        // ─────────────────────────────────────────────────────────────────────────

        private void BtnProcessPayment_Click(object? sender, EventArgs e)
        {
            if (_currentBill == null) return;

            if (!decimal.TryParse(txtPaymentAmount.Text.Trim(), out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid payment amount greater than Rs. 0.00.",
                    "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPaymentAmount.Focus();
                return;
            }

            // BR-10: Overpayment prevention
            if (amount > _currentBill.OutstandingBalance)
            {
                MessageBox.Show(
                    $"Payment amount (Rs. {amount:N2}) exceeds outstanding balance (Rs. {_currentBill.OutstandingBalance:N2}).\n\n" +
                    "Overpayments are strictly prevented.",
                    "Overpayment Rejected (BR-10)",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtPaymentAmount.Focus();
                return;
            }

            string method = cmbPaymentMethod.SelectedItem?.ToString() ?? "Cash";
            string refNo = txtReferenceNo.Text.Trim();

            string confirmMsg = $"Process payment for {_currentBill.PatientName}?\n\n" +
                               $"• Amount Due: Rs. {_currentBill.OutstandingBalance:N2}\n" +
                               $"• Paying Now: Rs. {amount:N2}\n" +
                               $"• Method: {method}\n" +
                               $"• Reference: {refNo}\n\n" +
                               $"Remaining Balance after this payment: Rs. {(_currentBill.OutstandingBalance - amount):N2}";

            if (MessageBox.Show(confirmMsg, "Confirm Payment", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            var payment = new Payment
            {
                BillID = _currentBill.BillID,
                PaymentDate = DateTime.Now,
                Amount = amount,
                PaymentMethod = method,
                ReferenceNo = refNo,
                ReceivedByUserID = SessionManager.CurrentUserId
            };

            int paymentId = _paymentService.ProcessPayment(payment, out string error);
            if (paymentId > 0)
            {
                _latestPayment = payment;
                MessageBox.Show(
                    $"Payment of Rs. {amount:N2} processed successfully!\nOfficial Receipt Reference: {payment.ReferenceNo}",
                    "Payment Successful",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadBillDetails(_currentBill.BillID);

                // Offer immediate receipt view
                if (MessageBox.Show("Would you like to preview the official payment receipt now?",
                    "View Receipt", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    BtnPrintReceipt_Click(sender, e);
                }
            }
            else
            {
                MessageBox.Show(error, "Payment Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Receipt Generation & Preview
        // ─────────────────────────────────────────────────────────────────────────

        private void BtnPrintReceipt_Click(object? sender, EventArgs e)
        {
            if (_currentBill == null)
            {
                MessageBox.Show("Please select an invoice.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var history = _paymentService.GetPaymentsByBillId(_currentBill.BillID);
            var paymentToShow = _latestPayment ?? history.LastOrDefault();

            if (paymentToShow == null)
            {
                MessageBox.Show("No payments have been recorded for this invoice yet.", "No Receipts", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var sb = new StringBuilder();
            sb.AppendLine("===============================================================");
            sb.AppendLine("                 MEDICARE HOSPITAL MANAGEMENT                  ");
            sb.AppendLine("                 OFFICIAL PAYMENT RECEIPT                      ");
            sb.AppendLine("===============================================================");
            sb.AppendLine($" Receipt No       : {paymentToShow.ReferenceNo ?? $"RCP-{paymentToShow.PaymentID:D5}"}");
            sb.AppendLine($" Payment Date     : {paymentToShow.PaymentDate:dd MMMM yyyy HH:mm:ss}");
            sb.AppendLine($" Invoice Ref      : INV-{_currentBill.BillID:D5}");
            sb.AppendLine($" Cashier Name     : {(!string.IsNullOrWhiteSpace(paymentToShow.ReceivedByUserName) ? paymentToShow.ReceivedByUserName : SessionManager.CurrentUserFullName)}");
            sb.AppendLine("---------------------------------------------------------------");
            sb.AppendLine($" Patient Code     : {_currentBill.PatientCode}");
            sb.AppendLine($" Patient Name     : {_currentBill.PatientName}");
            sb.AppendLine($" Phone Number     : {_currentBill.PatientPhone}");
            sb.AppendLine($" Description      : {_currentBill.Description}");
            sb.AppendLine("---------------------------------------------------------------");
            sb.AppendLine(" FINANCIAL SUMMARY:");
            sb.AppendLine($"   Total Billed   : Rs. {_currentBill.TotalAmount,12:N2}");
            sb.AppendLine($"   Payment Method : {paymentToShow.PaymentMethod}");
            sb.AppendLine($"   AMOUNT PAID    : Rs. {paymentToShow.Amount,12:N2}");
            sb.AppendLine($"   Balance Due    : Rs. {_currentBill.OutstandingBalance,12:N2}");
            sb.AppendLine($"   Invoice Status : {_currentBill.Status.ToUpper()}");
            sb.AppendLine("===============================================================");
            sb.AppendLine(" Thank you for choosing MediCare Hospital. Get well soon!");
            sb.AppendLine(" Helpline: +94 11 234 5678  |  Web: www.medicarehospital.lk");
            sb.AppendLine("===============================================================");

            using var previewForm = new Form
            {
                Text = $"Payment Receipt - {paymentToShow.ReferenceNo}",
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
    }
}
