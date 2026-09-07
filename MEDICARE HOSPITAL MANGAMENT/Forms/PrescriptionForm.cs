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
    /// Doctor-facing prescription authoring form.
    /// Supports multi-item prescription creation, medicine lookup, and line-item management.
    /// </summary>
    public partial class PrescriptionForm : Form
    {
        private readonly PrescriptionService _prescriptionService = new();
        private readonly MedicineService _medicineService = new();
        private readonly PatientRepository _patientRepo = new();
        private readonly DoctorRepository _doctorRepo = new();

        private readonly int? _preselectedPatientId;
        private readonly int? _preselectedDoctorId;
        private readonly int? _linkedMedicalRecordId;

        private List<Medicine> _medicinesList = new();
        private readonly List<PrescriptionItem> _items = new();

        public int CreatedPrescriptionID { get; private set; }

        public PrescriptionForm()
        {
            InitializeComponent();
        }

        public PrescriptionForm(int patientId, int doctorId, int? medicalRecordId = null)
        {
            _preselectedPatientId = patientId;
            _preselectedDoctorId = doctorId;
            _linkedMedicalRecordId = medicalRecordId;
            InitializeComponent();
        }

        private void PrescriptionForm_Load(object sender, EventArgs e)
        {
            SetupItemsGrid();
            LoadPatients();
            LoadDoctors();
            LoadMedicines();

            cmbFrequency.SelectedIndex = 2; // Default TDS
            RefreshSummary();

            // Authorization check
            if (!SessionManager.IsDoctor() && !SessionManager.IsAdmin())
            {
                btnSave.Enabled = false;
                btnAddItem.Enabled = false;
                MessageBox.Show("Prescription authoring is restricted to Doctors and Administrators.",
                    "View Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SetupItemsGrid()
        {
            dgvItems.AutoGenerateColumns = false;
            dgvItems.Columns.Clear();

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MedicineName",
                HeaderText = "Medicine Name",
                Width = 200
            });

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Dosage",
                HeaderText = "Dosage",
                Width = 120
            });

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Frequency",
                HeaderText = "Frequency",
                Width = 140
            });

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DurationDays",
                HeaderText = "Days",
                Width = 60,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantity",
                HeaderText = "Qty",
                Width = 60,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UnitPrice",
                HeaderText = "Unit (Rs.)",
                Width = 85,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalPrice",
                HeaderText = "Total (Rs.)",
                Width = 95,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "AvailableStock",
                HeaderText = "In Stock",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Instructions",
                HeaderText = "Instructions",
                Width = 160
            });

            // Action: Remove button column
            var btnRemoveCol = new DataGridViewButtonColumn
            {
                HeaderText = "Action",
                Text = "❌ Remove",
                UseColumnTextForButtonValue = true,
                Width = 85
            };
            dgvItems.Columns.Add(btnRemoveCol);
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
                cmbPatient.Enabled = false; // Locked to preselected patient
            }
        }

        private void LoadDoctors()
        {
            var doctors = _doctorRepo.GetAllDoctors();
            cmbDoctor.DataSource = doctors;
            cmbDoctor.DisplayMember = "FullName";
            cmbDoctor.ValueMember = "DoctorID";

            if (_preselectedDoctorId.HasValue && _preselectedDoctorId.Value > 0)
            {
                cmbDoctor.SelectedValue = _preselectedDoctorId.Value;
                cmbDoctor.Enabled = false;
            }
            else if (SessionManager.IsDoctor())
            {
                // Select currently logged-in doctor
                var currentDoc = _doctorRepo.GetDoctorByUserId(SessionManager.CurrentUserId);
                if (currentDoc != null)
                {
                    cmbDoctor.SelectedValue = currentDoc.DoctorID;
                }
            }
        }

        private void LoadMedicines()
        {
            _medicinesList = _medicineService.GetAllMedicines(activeOnly: true);

            var displayList = _medicinesList.Select(m => new
            {
                m.MedicineID,
                DisplayName = $"{m.MedicineName} ({m.StockQuantity} {m.Unit}s in stock) - Rs. {m.UnitPrice:N2}"
            }).ToList();

            cmbMedicine.DataSource = displayList;
            cmbMedicine.DisplayMember = "DisplayName";
            cmbMedicine.ValueMember = "MedicineID";
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Item Management
        // ─────────────────────────────────────────────────────────────────────────

        private void BtnAddItem_Click(object? sender, EventArgs e)
        {
            if (cmbMedicine.SelectedValue == null || !int.TryParse(cmbMedicine.SelectedValue.ToString(), out int medId))
            {
                MessageBox.Show("Please select a medicine.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var med = _medicinesList.FirstOrDefault(m => m.MedicineID == medId);
            if (med == null) return;

            string dosage = txtDosage.Text.Trim();
            if (string.IsNullOrWhiteSpace(dosage))
            {
                MessageBox.Show("Please enter the dosage (e.g., 500mg or 1 tablet).", "Dosage Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDosage.Focus();
                return;
            }

            string frequency = cmbFrequency.Text.Trim();
            if (string.IsNullOrWhiteSpace(frequency))
            {
                MessageBox.Show("Please select or enter the frequency.", "Frequency Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbFrequency.Focus();
                return;
            }

            int days = (int)numDuration.Value;
            int qty = (int)numQuantity.Value;
            string instructions = txtInstructions.Text.Trim();

            // Check if already in grid
            if (_items.Any(i => i.MedicineID == medId))
            {
                MessageBox.Show($"'{med.MedicineName}' is already added to this prescription. Remove or modify the existing item.",
                    "Duplicate Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var item = new PrescriptionItem
            {
                MedicineID = med.MedicineID,
                MedicineCode = med.MedicineCode,
                MedicineName = med.MedicineName,
                Category = med.Category ?? string.Empty,
                Unit = med.Unit,
                UnitPrice = med.UnitPrice,
                AvailableStock = med.StockQuantity,
                Dosage = dosage,
                Frequency = frequency,
                DurationDays = days,
                Quantity = qty,
                Instructions = instructions
            };

            _items.Add(item);
            RefreshGrid();
            RefreshSummary();

            // Reset inputs for next item
            txtDosage.Clear();
            txtInstructions.Clear();
            numQuantity.Value = 10;
            numDuration.Value = 5;
            cmbMedicine.Focus();
        }

        private void DgvItems_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            // Handle Remove button click
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvItems.Columns.Count - 1)
            {
                if (e.RowIndex < _items.Count)
                {
                    _items.RemoveAt(e.RowIndex);
                    RefreshGrid();
                    RefreshSummary();
                }
            }
        }

        private void DgvItems_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _items.Count) return;

            var item = _items[e.RowIndex];
            if (!item.HasSufficientStock)
            {
                // Soft amber/red highlight when stock is insufficient
                dgvItems.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 240, 240);
                if (dgvItems.Columns[e.ColumnIndex].DataPropertyName == "AvailableStock")
                {
                    e.CellStyle!.ForeColor = Color.FromArgb(180, 20, 20);
                    e.CellStyle.Font = new Font(dgvItems.Font, FontStyle.Bold);
                }
            }
        }

        private void RefreshGrid()
        {
            dgvItems.DataSource = null;
            dgvItems.DataSource = _items;
        }

        private void RefreshSummary()
        {
            int count = _items.Count;
            decimal totalCost = _items.Sum(i => i.TotalPrice);
            lblSummary.Text = $"Total Items: {count}  |  Est. Pharmacy Cost: Rs. {totalCost:N2}";
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Save & Clear
        // ─────────────────────────────────────────────────────────────────────────

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (cmbPatient.SelectedValue == null || !int.TryParse(cmbPatient.SelectedValue.ToString(), out int patientId))
            {
                MessageBox.Show("Please select a patient.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbDoctor.SelectedValue == null || !int.TryParse(cmbDoctor.SelectedValue.ToString(), out int doctorId))
            {
                MessageBox.Show("Please select a doctor.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_items.Count == 0)
            {
                MessageBox.Show("Please add at least one medication to the prescription.", "Empty Prescription", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Warning if any item has low stock
            var insufficient = _items.Where(i => !i.HasSufficientStock).ToList();
            if (insufficient.Count > 0)
            {
                string warnMsg = "Warning: The following medications have insufficient hospital pharmacy stock:\n\n" +
                                 string.Join("\n", insufficient.Select(i => $"• {i.MedicineName}: Prescribed {i.Quantity}, In Stock: {i.AvailableStock}")) +
                                 "\n\nYou may still issue this prescription for patient fulfillment, but pharmacy dispense may require restock. Proceed?";

                if (MessageBox.Show(warnMsg, "Stock Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
            }

            var rx = new Prescription
            {
                PatientID = patientId,
                DoctorID = doctorId,
                MedicalRecordID = _linkedMedicalRecordId,
                PrescriptionDate = dtpDate.Value,
                Status = "Active",
                Notes = txtNotes.Text.Trim(),
                Items = new List<PrescriptionItem>(_items)
            };

            int newId = _prescriptionService.CreatePrescription(rx, out string error);
            if (newId > 0)
            {
                CreatedPrescriptionID = newId;
                MessageBox.Show($"Prescription RX-{newId:D5} created successfully with {_items.Count} items.",
                    "Prescription Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(error, "Failed to Save Prescription", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnClear_Click(object? sender, EventArgs e)
        {
            if (_items.Count > 0 && MessageBox.Show("Clear all prescription items?", "Confirm Clear",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            _items.Clear();
            RefreshGrid();
            RefreshSummary();
            txtNotes.Clear();
            txtDosage.Clear();
            txtInstructions.Clear();
        }
    }
}
