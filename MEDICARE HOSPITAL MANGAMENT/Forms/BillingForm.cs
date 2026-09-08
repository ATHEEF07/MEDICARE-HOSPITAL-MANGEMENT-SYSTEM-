using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Repositories;
using MEDICARE_HOSPITAL_MANGAMENT.Services;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    /// <summary>
    /// Invoicing and Billing Management Form.
    /// Enables cashiers to generate itemized bills, apply discounts, and manage invoices.
    /// </summary>
    public partial class BillingForm : Form
    {
        private readonly BillingService _billingService = new();
        private readonly PatientRepository _patientRepo = new();
        private readonly AppointmentRepository _appointmentRepo = new();

        private readonly int? _preselectedPatientId;
        private readonly int? _preselectedAppointmentId;

        public class BillChargeItem
        {
            public string Description { get; set; } = string.Empty;
            public decimal Amount { get; set; }
        }

        private readonly List<BillChargeItem> _charges = new();
        private List<Bill> _recentBills = new();

        private static readonly Dictionary<string, decimal> PresetFeeMap = new()
        {
            { "Doctor Consultation Fee", 2500.00m },
            { "Emergency Consultation Fee", 3500.00m },
            { "Hospital Registration / Card Fee", 500.00m },
            { "Pharmacy Medication Dispensing", 1500.00m },
            { "Full Blood Count (FBC) Lab Test", 1200.00m },
            { "Fasting Blood Sugar (FBS) Test", 600.00m },
            { "Lipid Profile Test", 2200.00m },
            { "ECG (Electrocardiogram)", 1500.00m },
            { "Chest X-Ray Examination", 2800.00m },
            { "Nursing & Observation Care Fee", 1000.00m },
            { "Hospital Bed Day Charge (Ward)", 3000.00m },
            { "Dressing / Minor Wound Treatment", 850.00m }
        };

        public BillingForm()
        {
            InitializeComponent();
        }

        public BillingForm(int patientId, int? appointmentId = null)
        {
            _preselectedPatientId = patientId;
            _preselectedAppointmentId = appointmentId;
            InitializeComponent();
        }

        private void BillingForm_Load(object sender, EventArgs e)
        {
            SetupChargesGrid();
            SetupBillsGrid();
            LoadPatients();
            cmbStatusFilter.SelectedIndex = 0;
            LoadRecentBills();

            // Authorization check
            bool canBill = SessionManager.IsCashier() || SessionManager.IsAdmin();
            btnGenerateBill.Enabled = canBill;
            btnPaySelected.Enabled = canBill;
            btnCancelBill.Enabled = canBill;
        }

        private void SetupChargesGrid()
        {
            dgvCharges.AutoGenerateColumns = false;
            dgvCharges.Columns.Clear();

            dgvCharges.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Description",
                HeaderText = "Service Description",
                Width = 360
            });

            dgvCharges.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Amount",
                HeaderText = "Fee (Rs.)",
                Width = 140,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
        }

        private void SetupBillsGrid()
        {
            dgvBills.AutoGenerateColumns = false;
            dgvBills.Columns.Clear();

            dgvBills.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BillID",
                HeaderText = "Inv #",
                Width = 65
            });

            dgvBills.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BillDate",
                HeaderText = "Date",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yy" }
            });

            dgvBills.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PatientName",
                HeaderText = "Patient",
                Width = 140
            });

            dgvBills.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalAmount",
                HeaderText = "Total (Rs.)",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvBills.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "OutstandingBalance",
                HeaderText = "Due (Rs.)",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvBills.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Status",
                HeaderText = "Status",
                Width = 95,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
        }

        private void LoadPatients()
        {
            var patients = _patientRepo.GetAllPatients();
            cmbPatient.DataSource = patients;
            cmbPatient.DisplayMember = "FullName";
            cmbPatient.ValueMember = "PatientID";

            if (_preselectedPatientId.HasValue && _preselectedPatientId.Value > 0)
            {
                cmbPatient.SelectedValue = _preselectedPatientId.Value;
                cmbPatient.Enabled = false;
            }
        }

        private void CmbPatient_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbPatient.SelectedValue != null && int.TryParse(cmbPatient.SelectedValue.ToString(), out int patientId))
            {
                LoadPatientAppointments(patientId);
            }
        }

        private void LoadPatientAppointments(int patientId)
        {
            var appts = _appointmentRepo.GetAppointmentsForPatient(patientId);
            var items = new List<object> { new { AppointmentID = (int?)null, DisplayText = "-- No Appointment Link --" } };

            foreach (var a in appts)
            {
                items.Add(new
                {
                    AppointmentID = (int?)a.AppointmentID,
                    DisplayText = $"Appt #{a.AppointmentID} ({a.AppointmentDate:dd/MM} - Dr. {a.DoctorName})"
                });
            }

            cmbAppointment.DataSource = items;
            cmbAppointment.DisplayMember = "DisplayText";
            cmbAppointment.ValueMember = "AppointmentID";

            if (_preselectedAppointmentId.HasValue && _preselectedAppointmentId.Value > 0)
            {
                cmbAppointment.SelectedValue = _preselectedAppointmentId.Value;
            }
        }

        private void CmbPresetServices_SelectedIndexChanged(object? sender, EventArgs e)
        {
            string selected = cmbPresetServices.SelectedItem?.ToString() ?? "";
            if (PresetFeeMap.TryGetValue(selected, out decimal fee))
            {
                txtCustomDesc.Text = selected;
                txtChargeAmount.Text = fee.ToString("0.00");
            }
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Line Item Charge Management
        // ─────────────────────────────────────────────────────────────────────────

        private void BtnAddCharge_Click(object? sender, EventArgs e)
        {
            string desc = txtCustomDesc.Text.Trim();
            if (string.IsNullOrWhiteSpace(desc))
            {
                MessageBox.Show("Please select or enter a service description.", "Description Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomDesc.Focus();
                return;
            }

            if (!decimal.TryParse(txtChargeAmount.Text.Trim(), out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid positive fee amount.", "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtChargeAmount.Focus();
                return;
            }

            _charges.Add(new BillChargeItem { Description = desc, Amount = amount });
            RefreshChargesGrid();
            RecalculateTotals();

            txtCustomDesc.Clear();
            txtChargeAmount.Clear();
            cmbPresetServices.SelectedIndex = 0;
            cmbPresetServices.Focus();
        }

        private void BtnRemoveCharge_Click(object? sender, EventArgs e)
        {
            if (dgvCharges.SelectedRows.Count > 0)
            {
                int index = dgvCharges.SelectedRows[0].Index;
                if (index >= 0 && index < _charges.Count)
                {
                    _charges.RemoveAt(index);
                    RefreshChargesGrid();
                    RecalculateTotals();
                }
            }
        }

        private void RefreshChargesGrid()
        {
            dgvCharges.DataSource = null;
            dgvCharges.DataSource = _charges;
        }

        private void RecalculateTotals()
        {
            decimal subtotal = _charges.Sum(c => c.Amount);
            lblSubtotalValue.Text = $"Rs. {subtotal:N2}";

            decimal.TryParse(txtDiscount.Text.Trim(), out decimal discount);
            if (discount < 0) discount = 0;
            if (discount > subtotal) discount = subtotal;

            decimal total = Math.Max(0m, subtotal - discount);
            lblTotalValue.Text = $"Rs. {total:N2}";
        }

        private void TxtDiscount_TextChanged(object? sender, EventArgs e)
        {
            RecalculateTotals();
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Bill Generation
        // ─────────────────────────────────────────────────────────────────────────

        private void BtnGenerateBill_Click(object? sender, EventArgs e)
        {
            if (cmbPatient.SelectedValue == null || !int.TryParse(cmbPatient.SelectedValue.ToString(), out int patientId))
            {
                MessageBox.Show("Please select a patient.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_charges.Count == 0)
            {
                MessageBox.Show("Please add at least one charge or service fee.", "Charges Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal subtotal = _charges.Sum(c => c.Amount);
            if (!decimal.TryParse(txtDiscount.Text.Trim(), out decimal discount) || discount < 0)
            {
                MessageBox.Show("Please enter a valid non-negative discount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiscount.Focus();
                return;
            }

            if (discount > subtotal)
            {
                MessageBox.Show($"Discount (Rs. {discount:N2}) cannot exceed Subtotal (Rs. {subtotal:N2}).", "Discount Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? apptId = null;
            if (cmbAppointment.SelectedValue != null && int.TryParse(cmbAppointment.SelectedValue.ToString(), out int parsedApptId) && parsedApptId > 0)
            {
                apptId = parsedApptId;
            }

            // Compose description from line items if user notes empty
            string notes = txtBillNotes.Text.Trim();
            if (string.IsNullOrWhiteSpace(notes))
            {
                notes = string.Join("; ", _charges.Select(c => $"{c.Description} (Rs. {c.Amount:N2})"));
            }

            var bill = new Bill
            {
                PatientID = patientId,
                AppointmentID = apptId,
                BillDate = dtpBillDate.Value,
                Description = notes,
                Subtotal = subtotal,
                Discount = discount,
                TotalAmount = subtotal - discount,
                Status = "Pending"
            };

            int newId = _billingService.CreateBill(bill, out string error);
            if (newId > 0)
            {
                string msg = $"Invoice INV-{newId:D5} generated successfully for Rs. {bill.TotalAmount:N2}!\n\n" +
                             $"Would you like to open the payment window to collect payment right now?";

                LoadRecentBills();
                ClearForm();

                if (MessageBox.Show(msg, "Invoice Created", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using var payForm = new PaymentForm(newId);
                    payForm.ShowDialog();
                    LoadRecentBills();
                }
            }
            else
            {
                MessageBox.Show(error, "Generation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnClearForm_Click(object? sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            _charges.Clear();
            RefreshChargesGrid();
            txtDiscount.Text = "0.00";
            txtBillNotes.Clear();
            txtCustomDesc.Clear();
            txtChargeAmount.Clear();
            cmbPresetServices.SelectedIndex = 0;
            RecalculateTotals();
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Recent Bills List & Actions
        // ─────────────────────────────────────────────────────────────────────────

        private void LoadRecentBills()
        {
            string status = cmbStatusFilter.SelectedItem?.ToString() ?? "All Statuses";
            if (status == "Pending" || status == "PartiallyPaid")
                _recentBills = _billingService.GetPendingBills().Where(b => status == "All Statuses" || b.Status == status).ToList();
            else
                _recentBills = _billingService.GetAllBills(100);

            ApplyRecentBillsFilter();
        }

        private void ApplyRecentBillsFilter()
        {
            string term = txtSearchRecent.Text.Trim().ToLowerInvariant();
            string status = cmbStatusFilter.SelectedItem?.ToString() ?? "All Statuses";

            var filtered = _recentBills.Where(b =>
            {
                bool matchesStatus = status == "All Statuses" || b.Status == status;
                bool matchesTerm = string.IsNullOrWhiteSpace(term)
                    || b.PatientName.ToLowerInvariant().Contains(term)
                    || b.PatientCode.ToLowerInvariant().Contains(term)
                    || b.BillID.ToString().Contains(term);

                return matchesStatus && matchesTerm;
            }).ToList();

            dgvBills.DataSource = filtered;
        }

        private void DgvBills_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvBills.Rows.Count) return;

            var row = dgvBills.Rows[e.RowIndex];
            if (row.DataBoundItem is Bill b)
            {
                if (b.Status == "Paid")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(240, 255, 240); // Soft green
                }
                else if (b.Status == "PartiallyPaid")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 250, 235); // Soft yellow
                }
                else if (b.Status == "Cancelled")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245); // Gray
                    row.DefaultCellStyle.ForeColor = Color.Gray;
                }
            }
        }

        private void BtnPaySelected_Click(object? sender, EventArgs e)
        {
            if (dgvBills.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an invoice from the list to process payment.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var bill = dgvBills.SelectedRows[0].DataBoundItem as Bill;
            if (bill == null) return;

            if (bill.Status == "Paid")
            {
                MessageBox.Show($"Invoice INV-{bill.BillID:D5} is already settled in full.", "Fully Paid", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (bill.Status == "Cancelled")
            {
                MessageBox.Show("Cannot process payment for a cancelled invoice.", "Cancelled Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var payForm = new PaymentForm(bill.BillID);
            payForm.ShowDialog();
            LoadRecentBills();
        }

        private void BtnCancelBill_Click(object? sender, EventArgs e)
        {
            if (dgvBills.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an invoice from the list to cancel.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var bill = dgvBills.SelectedRows[0].DataBoundItem as Bill;
            if (bill == null) return;

            if (MessageBox.Show($"Are you sure you want to cancel Invoice INV-{bill.BillID:D5} for {bill.PatientName}?",
                "Confirm Cancellation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            bool ok = _billingService.CancelBill(bill.BillID, out string error);
            if (ok)
            {
                MessageBox.Show($"Invoice INV-{bill.BillID:D5} has been cancelled.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadRecentBills();
            }
            else
            {
                MessageBox.Show(error, "Cancellation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TxtSearchRecent_TextChanged(object? sender, EventArgs e) => ApplyRecentBillsFilter();
        private void CmbStatusFilter_SelectedIndexChanged(object? sender, EventArgs e) => LoadRecentBills();
        private void BtnRefreshBills_Click(object? sender, EventArgs e) => LoadRecentBills();
    }
}
