using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Services;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    /// <summary>
    /// Pharmacy Inventory Management Form.
    /// Allows Pharmacists and Administrators to track stocks, adjust inventory,
    /// and monitor low-stock thresholds with color-coded alerts.
    /// </summary>
    public partial class MedicineForm : Form
    {
        private readonly MedicineService _medicineService = new();
        private List<Medicine> _allMedicines = new();
        private int _selectedMedicineId = 0;

        public MedicineForm()
        {
            InitializeComponent();
        }

        private void MedicineForm_Load(object sender, EventArgs e)
        {
            SetupGridColumns();
            LoadCategories();
            cmbUnit.SelectedIndex = 0;
            LoadMedicines();

            // Authorization check
            bool canEdit = SessionManager.IsPharmacist() || SessionManager.IsAdmin();
            btnSave.Enabled = canEdit;
            btnUpdate.Enabled = canEdit;
            btnAdjustStock.Enabled = canEdit;
            btnGenCode.Enabled = canEdit;
        }

        private void SetupGridColumns()
        {
            dgvMedicines.AutoGenerateColumns = false;
            dgvMedicines.Columns.Clear();

            dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MedicineID",
                HeaderText = "ID",
                Width = 50,
                Visible = false
            });

            dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MedicineCode",
                HeaderText = "Code",
                Width = 90
            });

            dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MedicineName",
                HeaderText = "Medicine Name",
                Width = 200
            });

            dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Category",
                HeaderText = "Category",
                Width = 130
            });

            dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Unit",
                HeaderText = "Unit",
                Width = 80
            });

            dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UnitPrice",
                HeaderText = "Unit Price (Rs.)",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StockQuantity",
                HeaderText = "Stock",
                Width = 70,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ReorderLevel",
                HeaderText = "Reorder",
                Width = 70,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StockStatusText",
                HeaderText = "Stock Status",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ExpiryDate",
                HeaderText = "Expiry Date",
                Width = 95,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });
        }

        private void LoadCategories()
        {
            cmbCategoryFilter.Items.Clear();
            cmbCategoryFilter.Items.Add("All Categories");

            cmbCategory.Items.Clear();
            var categories = _medicineService.GetCategories();
            foreach (var cat in categories)
            {
                cmbCategoryFilter.Items.Add(cat);
                cmbCategory.Items.Add(cat);
            }

            cmbCategoryFilter.SelectedIndex = 0;
        }

        private void LoadMedicines()
        {
            _allMedicines = _medicineService.GetAllMedicines(activeOnly: false);
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            string search = txtSearch.Text.Trim().ToLowerInvariant();
            string selectedCategory = cmbCategoryFilter.SelectedItem?.ToString() ?? "All Categories";
            bool lowStockOnly = chkLowStockOnly.Checked;

            var filtered = _allMedicines.Where(m =>
            {
                bool matchesSearch = string.IsNullOrWhiteSpace(search)
                    || m.MedicineName.ToLowerInvariant().Contains(search)
                    || m.MedicineCode.ToLowerInvariant().Contains(search)
                    || (m.Category != null && m.Category.ToLowerInvariant().Contains(search));

                bool matchesCategory = selectedCategory == "All Categories"
                    || string.Equals(m.Category, selectedCategory, StringComparison.OrdinalIgnoreCase);

                bool matchesLowStock = !lowStockOnly || m.IsLowStock;

                return matchesSearch && matchesCategory && matchesLowStock;
            }).ToList();

            dgvMedicines.DataSource = filtered;

            // Update Summary metrics
            lblTotalCount.Text = $"Total Medications: {_allMedicines.Count}";
            lblLowStockCount.Text = $"Low Stock Alerts: {_allMedicines.Count(m => m.IsLowStock && m.StockQuantity > 0)}";
            lblOutOfStockCount.Text = $"Out of Stock: {_allMedicines.Count(m => m.StockQuantity == 0)}";
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Grid Formatting & Selection
        // ─────────────────────────────────────────────────────────────────────────

        private void DgvMedicines_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvMedicines.Rows.Count) return;

            var row = dgvMedicines.Rows[e.RowIndex];
            if (row.DataBoundItem is Medicine med)
            {
                if (med.StockQuantity == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 235); // Soft red
                    if (dgvMedicines.Columns[e.ColumnIndex].DataPropertyName == "StockStatusText")
                    {
                        e.CellStyle!.ForeColor = Color.FromArgb(180, 20, 20);
                        e.CellStyle.Font = new Font(dgvMedicines.Font, FontStyle.Bold);
                    }
                }
                else if (med.IsLowStock)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 248, 220); // Soft amber
                    if (dgvMedicines.Columns[e.ColumnIndex].DataPropertyName == "StockStatusText")
                    {
                        e.CellStyle!.ForeColor = Color.FromArgb(180, 100, 0);
                        e.CellStyle.Font = new Font(dgvMedicines.Font, FontStyle.Bold);
                    }
                }
            }
        }

        private void DgvMedicines_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvMedicines.SelectedRows.Count == 0) return;

            var row = dgvMedicines.SelectedRows[0];
            if (row.DataBoundItem is Medicine med)
            {
                _selectedMedicineId = med.MedicineID;
                txtMedicineCode.Text = med.MedicineCode;
                txtMedicineName.Text = med.MedicineName;
                cmbCategory.Text = med.Category ?? string.Empty;
                cmbUnit.Text = med.Unit;
                txtUnitPrice.Text = med.UnitPrice.ToString("0.00");
                txtStockQuantity.Text = med.StockQuantity.ToString();
                txtReorderLevel.Text = med.ReorderLevel.ToString();
                dtpExpiryDate.Value = med.ExpiryDate ?? DateTime.Today.AddYears(1);
                chkIsActive.Checked = med.IsActive;
            }
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Filter & Search Event Handlers
        // ─────────────────────────────────────────────────────────────────────────

        private void TxtSearch_TextChanged(object? sender, EventArgs e) => ApplyFilters();
        private void CmbCategoryFilter_SelectedIndexChanged(object? sender, EventArgs e) => ApplyFilters();
        private void ChkLowStockOnly_CheckedChanged(object? sender, EventArgs e) => ApplyFilters();
        private void BtnRefresh_Click(object? sender, EventArgs e) => LoadMedicines();

        private void BtnGenCode_Click(object? sender, EventArgs e)
        {
            txtMedicineCode.Text = _medicineService.GenerateNextMedicineCode();
        }

        // ─────────────────────────────────────────────────────────────────────────
        // CRUD Operations
        // ─────────────────────────────────────────────────────────────────────────

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            var med = BuildModelFromUI();
            if (med == null) return;

            int newId = _medicineService.CreateMedicine(med, out string error);
            if (newId > 0)
            {
                MessageBox.Show($"Medicine '{med.MedicineName}' added successfully with Code {med.MedicineCode}.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMedicines();
                LoadCategories();
                ClearForm();
            }
            else
            {
                MessageBox.Show(error, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnUpdate_Click(object? sender, EventArgs e)
        {
            if (_selectedMedicineId <= 0)
            {
                MessageBox.Show("Please select a medicine from the list to update.",
                    "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var med = BuildModelFromUI();
            if (med == null) return;
            med.MedicineID = _selectedMedicineId;

            bool ok = _medicineService.UpdateMedicine(med, out string error);
            if (ok)
            {
                MessageBox.Show($"Medicine '{med.MedicineName}' updated successfully.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMedicines();
                LoadCategories();
            }
            else
            {
                MessageBox.Show(error, "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnAdjustStock_Click(object? sender, EventArgs e)
        {
            if (_selectedMedicineId <= 0)
            {
                MessageBox.Show("Please select a medicine from the list to adjust stock.",
                    "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var med = _medicineService.GetById(_selectedMedicineId);
            if (med == null) return;

            string input = Microsoft.VisualBasic.Interaction.InputBox(
                $"Current stock for '{med.MedicineName}' is {med.StockQuantity} {med.Unit}s.\n\nEnter quantity to ADD (positive number) or DEDUCT (negative number):",
                "Adjust Medicine Stock",
                "0");

            if (string.IsNullOrWhiteSpace(input)) return;

            if (!int.TryParse(input, out int change) || change == 0)
            {
                MessageBox.Show("Please enter a valid non-zero integer amount.", "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool ok = _medicineService.AdjustStock(_selectedMedicineId, change, out string error);
            if (ok)
            {
                MessageBox.Show($"Stock successfully adjusted by {change:+0;-0;0} {med.Unit}s.",
                    "Stock Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMedicines();
            }
            else
            {
                MessageBox.Show(error, "Stock Adjustment Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnClear_Click(object? sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            _selectedMedicineId = 0;
            txtMedicineCode.Clear();
            txtMedicineName.Clear();
            cmbCategory.SelectedIndex = -1;
            cmbUnit.SelectedIndex = 0;
            txtUnitPrice.Text = "0.00";
            txtStockQuantity.Text = "0";
            txtReorderLevel.Text = "10";
            dtpExpiryDate.Value = DateTime.Today.AddYears(1);
            chkIsActive.Checked = true;
            dgvMedicines.ClearSelection();
            txtMedicineCode.Focus();
        }

        private Medicine? BuildModelFromUI()
        {
            if (!decimal.TryParse(txtUnitPrice.Text.Trim(), out decimal unitPrice) || unitPrice < 0)
            {
                MessageBox.Show("Please enter a valid non-negative Unit Price.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUnitPrice.Focus();
                return null;
            }

            if (!int.TryParse(txtStockQuantity.Text.Trim(), out int stock) || stock < 0)
            {
                MessageBox.Show("Please enter a valid non-negative Stock Quantity.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStockQuantity.Focus();
                return null;
            }

            if (!int.TryParse(txtReorderLevel.Text.Trim(), out int reorder) || reorder < 0)
            {
                MessageBox.Show("Please enter a valid non-negative Reorder Level.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReorderLevel.Focus();
                return null;
            }

            return new Medicine
            {
                MedicineCode = txtMedicineCode.Text.Trim(),
                MedicineName = txtMedicineName.Text.Trim(),
                Category = string.IsNullOrWhiteSpace(cmbCategory.Text) ? null : cmbCategory.Text.Trim(),
                Unit = string.IsNullOrWhiteSpace(cmbUnit.Text) ? "Tablet" : cmbUnit.Text.Trim(),
                UnitPrice = unitPrice,
                StockQuantity = stock,
                ReorderLevel = reorder,
                ExpiryDate = dtpExpiryDate.Value.Date,
                IsActive = chkIsActive.Checked
            };
        }
    }
}
