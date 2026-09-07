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
    /// Pharmacist Dispensing Workbench.
    /// Manages active prescription queue, verifies real-time inventory levels,
    /// and executes atomic stock decrement transactions upon dispensing.
    /// </summary>
    public partial class PharmacyDispenseForm : Form
    {
        private readonly PharmacyService _pharmacyService = new();
        private readonly PrescriptionService _prescriptionService = new();
        private List<Prescription> _queueList = new();
        private Prescription? _selectedRx;

        public PharmacyDispenseForm()
        {
            InitializeComponent();
        }

        private void PharmacyDispenseForm_Load(object sender, EventArgs e)
        {
            SetupQueueGrid();
            SetupItemsGrid();
            cmbStatusFilter.SelectedIndex = 0; // "Active (Pending)"
            LoadQueue();

            // Authorization check
            if (!SessionManager.IsPharmacist() && !SessionManager.IsAdmin())
            {
                btnDispense.Enabled = false;
                MessageBox.Show("Prescription dispensing is restricted to Pharmacists and Administrators.",
                    "View Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SetupQueueGrid()
        {
            dgvQueue.AutoGenerateColumns = false;
            dgvQueue.Columns.Clear();

            dgvQueue.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PrescriptionID",
                HeaderText = "Rx #",
                Width = 70
            });

            dgvQueue.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PatientName",
                HeaderText = "Patient",
                Width = 150
            });

            dgvQueue.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Status",
                HeaderText = "Status",
                Width = 80
            });

            dgvQueue.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PrescriptionDate",
                HeaderText = "Date",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM" }
            });
        }

        private void SetupItemsGrid()
        {
            dgvDispenseItems.AutoGenerateColumns = false;
            dgvDispenseItems.Columns.Clear();

            dgvDispenseItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MedicineName",
                HeaderText = "Medicine Name",
                Width = 200
            });

            dgvDispenseItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Dosage",
                HeaderText = "Dosage",
                Width = 110
            });

            dgvDispenseItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Frequency",
                HeaderText = "Frequency",
                Width = 120
            });

            dgvDispenseItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantity",
                HeaderText = "Prescribed",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvDispenseItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "AvailableStock",
                HeaderText = "In Stock",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvDispenseItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Unit",
                HeaderText = "Unit",
                Width = 70
            });

            dgvDispenseItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UnitPrice",
                HeaderText = "Unit (Rs.)",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvDispenseItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalPrice",
                HeaderText = "Total (Rs.)",
                Width = 95,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvDispenseItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Instructions",
                HeaderText = "Patient Instructions",
                Width = 180
            });
        }

        private void LoadQueue()
        {
            string filter = cmbStatusFilter.SelectedItem?.ToString() ?? "Active (Pending)";
            if (filter.StartsWith("Active"))
                _queueList = _pharmacyService.GetPendingPrescriptionsQueue();
            else if (filter.StartsWith("Dispensed"))
                _queueList = _prescriptionService.GetPrescriptionsByStatus("Dispensed");
            else
                _queueList = _prescriptionService.GetAllPrescriptions(100);

            ApplyQueueSearch();
        }

        private void ApplyQueueSearch()
        {
            string term = txtSearch.Text.Trim().ToLowerInvariant();
            var filtered = _queueList.Where(rx =>
            {
                if (string.IsNullOrWhiteSpace(term)) return true;
                return rx.PatientName.ToLowerInvariant().Contains(term)
                    || rx.PatientCode.ToLowerInvariant().Contains(term)
                    || rx.PrescriptionID.ToString().Contains(term);
            }).ToList();

            dgvQueue.DataSource = filtered;

            if (filtered.Count == 0)
            {
                ClearWorkbench();
            }
        }

        private void DgvQueue_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvQueue.SelectedRows.Count == 0) return;

            var row = dgvQueue.SelectedRows[0];
            if (row.DataBoundItem is Prescription queueItem)
            {
                LoadPrescriptionDetails(queueItem.PrescriptionID);
            }
        }

        private void LoadPrescriptionDetails(int prescriptionId)
        {
            _selectedRx = _pharmacyService.GetPrescriptionForDispensing(prescriptionId);
            if (_selectedRx == null)
            {
                ClearWorkbench();
                return;
            }

            lblRxNumber.Text = $"Prescription: RX-{_selectedRx.PrescriptionID:D5} ({_selectedRx.Status})";
            lblDate.Text = $"Date: {_selectedRx.PrescriptionDate:dd/MM/yyyy HH:mm}";
            lblPatientName.Text = $"Patient: {_selectedRx.PatientName}";
            lblPatientCode.Text = $"Code: {_selectedRx.PatientCode}";
            lblDoctorName.Text = $"Prescribed by: {_selectedRx.DoctorName} ({_selectedRx.DepartmentName})";
            lblNotes.Text = $"Doctor Notes: {(!string.IsNullOrWhiteSpace(_selectedRx.Notes) ? _selectedRx.Notes : "None specified")}";

            dgvDispenseItems.DataSource = _selectedRx.Items;

            decimal totalVal = _selectedRx.Items.Sum(i => i.TotalPrice);
            lblTotalValue.Text = $"Prescription Total: Rs. {totalVal:N2}";

            // Stock sufficiency verification
            bool canDispenseRole = SessionManager.IsPharmacist() || SessionManager.IsAdmin();

            if (_selectedRx.Status == "Dispensed")
            {
                lblStockStatusAlert.Text = "Status: Already Dispensed";
                lblStockStatusAlert.ForeColor = Color.FromArgb(20, 100, 180);
                btnDispense.Enabled = false;
                btnDispense.Text = "Already Dispensed";
            }
            else if (_selectedRx.Status == "Cancelled")
            {
                lblStockStatusAlert.Text = "Status: Cancelled by Doctor";
                lblStockStatusAlert.ForeColor = Color.FromArgb(160, 40, 40);
                btnDispense.Enabled = false;
                btnDispense.Text = "Prescription Cancelled";
            }
            else
            {
                bool hasStock = _pharmacyService.CheckStockAvailability(_selectedRx.PrescriptionID, out var shortages);
                if (hasStock)
                {
                    lblStockStatusAlert.Text = "✔️ All medications in stock and ready to dispense";
                    lblStockStatusAlert.ForeColor = Color.FromArgb(20, 130, 40);
                    btnDispense.Enabled = canDispenseRole;
                    btnDispense.Text = "✅  Dispense Medications";
                }
                else
                {
                    lblStockStatusAlert.Text = $"⚠️ Insufficient stock for {shortages.Count} medication(s)";
                    lblStockStatusAlert.ForeColor = Color.FromArgb(180, 40, 20);
                    btnDispense.Enabled = false;
                    btnDispense.Text = "❌  Insufficient Stock";
                }
            }
        }

        private void DgvDispenseItems_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (_selectedRx == null || e.RowIndex < 0 || e.RowIndex >= _selectedRx.Items.Count) return;

            var item = _selectedRx.Items[e.RowIndex];
            if (!item.HasSufficientStock)
            {
                dgvDispenseItems.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 235);
                if (dgvDispenseItems.Columns[e.ColumnIndex].DataPropertyName == "AvailableStock")
                {
                    e.CellStyle!.ForeColor = Color.FromArgb(200, 20, 20);
                    e.CellStyle.Font = new Font(dgvDispenseItems.Font, FontStyle.Bold);
                }
            }
        }

        private void ClearWorkbench()
        {
            _selectedRx = null;
            lblRxNumber.Text = "Prescription: Select a prescription...";
            lblDate.Text = "Date: —";
            lblPatientName.Text = "Patient: —";
            lblPatientCode.Text = "Code: —";
            lblDoctorName.Text = "Prescribed by: —";
            lblNotes.Text = "Doctor Notes: —";
            dgvDispenseItems.DataSource = null;
            lblTotalValue.Text = "Prescription Total: Rs. 0.00";
            lblStockStatusAlert.Text = "Stock Status: No prescription selected";
            btnDispense.Enabled = false;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Dispensing Workflow
        // ─────────────────────────────────────────────────────────────────────────

        private void BtnDispense_Click(object? sender, EventArgs e)
        {
            if (_selectedRx == null) return;

            string prompt = $"Are you sure you want to dispense Prescription RX-{_selectedRx.PrescriptionID:D5} for {_selectedRx.PatientName}?\n\n" +
                            $"This will deduct {_selectedRx.Items.Count} medicine line items from the hospital pharmacy inventory.";

            if (MessageBox.Show(prompt, "Confirm Dispensing", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            bool ok = _pharmacyService.DispensePrescription(_selectedRx.PrescriptionID, out string error);
            if (ok)
            {
                MessageBox.Show(
                    $"Prescription RX-{_selectedRx.PrescriptionID:D5} has been successfully dispensed!\nInventory balances have been updated.",
                    "Dispensed Successfully",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadQueue();
                LoadPrescriptionDetails(_selectedRx.PrescriptionID);
            }
            else
            {
                MessageBox.Show(error, "Dispensing Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnPrintSlip_Click(object? sender, EventArgs e)
        {
            if (_selectedRx == null)
            {
                MessageBox.Show("Please select a prescription first.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var sb = new StringBuilder();
            sb.AppendLine("===============================================================");
            sb.AppendLine("                 MEDICARE HOSPITAL PHARMACY                    ");
            sb.AppendLine("                 Medication Dispensing Slip                    ");
            sb.AppendLine("===============================================================");
            sb.AppendLine($" Prescription ID : RX-{_selectedRx.PrescriptionID:D5}");
            sb.AppendLine($" Date Issued     : {_selectedRx.PrescriptionDate:dd MMMM yyyy HH:mm}");
            sb.AppendLine($" Status          : {_selectedRx.Status.ToUpper()}");
            sb.AppendLine("---------------------------------------------------------------");
            sb.AppendLine($" Patient Code    : {_selectedRx.PatientCode}");
            sb.AppendLine($" Patient Name    : {_selectedRx.PatientName}");
            sb.AppendLine($" Doctor          : {_selectedRx.DoctorName} ({_selectedRx.DepartmentName})");
            sb.AppendLine("---------------------------------------------------------------");
            sb.AppendLine(" PRESCRIBED MEDICATIONS:");
            sb.AppendLine("---------------------------------------------------------------");

            int idx = 1;
            foreach (var item in _selectedRx.Items)
            {
                sb.AppendLine($" {idx++}. {item.MedicineName}");
                sb.AppendLine($"    Dosage       : {item.Dosage} | Frequency: {item.Frequency}");
                sb.AppendLine($"    Duration     : {item.DurationDays} days | Qty: {item.Quantity} {item.Unit}s");
                if (!string.IsNullOrWhiteSpace(item.Instructions))
                    sb.AppendLine($"    Instructions : {item.Instructions}");
                sb.AppendLine($"    Price        : Rs. {item.UnitPrice:N2} each | Total: Rs. {item.TotalPrice:N2}");
                sb.AppendLine();
            }

            sb.AppendLine("---------------------------------------------------------------");
            sb.AppendLine($" TOTAL ESTIMATED VALUE : Rs. {_selectedRx.Items.Sum(i => i.TotalPrice):N2}");
            if (!string.IsNullOrWhiteSpace(_selectedRx.Notes))
            {
                sb.AppendLine($" Doctor Notes          : {_selectedRx.Notes}");
            }
            sb.AppendLine("===============================================================");
            sb.AppendLine(" Pharmacist Signature  : _____________________________________");
            sb.AppendLine(" Hospital Contact      : +94 11 234 5678 | info@medicare.lk");
            sb.AppendLine("===============================================================");

            // Show preview form / box
            using var previewForm = new Form
            {
                Text = $"Dispensing Slip - RX-{_selectedRx.PrescriptionID:D5}",
                Width = 650,
                Height = 650,
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.White
            };

            var txtSlip = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Consolas", 9.5F),
                BackColor = Color.White,
                Text = sb.ToString()
            };

            var pnlBtn = new Panel { Dock = DockStyle.Bottom, Height = 50, BackColor = Color.FromArgb(245, 248, 252) };
            var btnClosePreview = new Button
            {
                Text = "Close",
                Location = new Point(520, 10),
                Size = new Size(95, 30),
                BackColor = Color.FromArgb(20, 80, 140),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnClosePreview.Click += (s, ev) => previewForm.Close();
            pnlBtn.Controls.Add(btnClosePreview);

            previewForm.Controls.Add(txtSlip);
            previewForm.Controls.Add(pnlBtn);
            previewForm.ShowDialog();
        }

        private void TxtSearch_TextChanged(object? sender, EventArgs e) => ApplyQueueSearch();
        private void CmbStatusFilter_SelectedIndexChanged(object? sender, EventArgs e) => LoadQueue();
        private void BtnRefreshQueue_Click(object? sender, EventArgs e) => LoadQueue();
    }
}
