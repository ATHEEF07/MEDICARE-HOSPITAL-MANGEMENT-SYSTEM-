using System;
using System.Collections.Generic;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Repositories;

namespace MEDICARE_HOSPITAL_MANGAMENT.Services
{
    /// <summary>
    /// Business Logic Layer for Medicine Inventory and Stock Management.
    /// Enforces pricing constraints, positive inventory limits, and unique medicine codes.
    /// </summary>
    public class MedicineService
    {
        private readonly MedicineRepository _medicineRepo;

        public MedicineService()
        {
            _medicineRepo = new MedicineRepository();
        }

        public MedicineService(MedicineRepository medicineRepo)
        {
            _medicineRepo = medicineRepo;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Read Pass-Throughs
        // ─────────────────────────────────────────────────────────────────────────

        public List<Medicine> GetAllMedicines(bool activeOnly = true) => _medicineRepo.GetAllMedicines(activeOnly);
        public List<Medicine> GetLowStockMedicines() => _medicineRepo.GetLowStockMedicines();
        public Medicine? GetById(int medicineId) => _medicineRepo.GetById(medicineId);
        public Medicine? GetByCode(string code) => _medicineRepo.GetByCode(code);
        public List<Medicine> SearchMedicines(string query, bool activeOnly = true) => _medicineRepo.SearchMedicines(query, activeOnly);
        public List<string> GetCategories() => _medicineRepo.GetCategories();
        public string GenerateNextMedicineCode() => _medicineRepo.GenerateNextMedicineCode();

        // ─────────────────────────────────────────────────────────────────────────
        // Business Validation & Write Operations
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Validates and adds a new medicine to inventory.
        /// </summary>
        public int CreateMedicine(Medicine med, out string errorMessage)
        {
            errorMessage = string.Empty;

            // Authorization: Pharmacist or Administrator only
            if (!SessionManager.IsPharmacist() && !SessionManager.IsAdmin())
            {
                errorMessage = "Access denied. Only Pharmacists and Administrators can add medications.";
                return 0;
            }

            if (!ValidateMedicine(med, isNew: true, out errorMessage))
                return 0;

            int newId = _medicineRepo.CreateMedicine(med);
            if (newId <= 0)
            {
                errorMessage = "Database failed to create medicine record.";
                return 0;
            }

            return newId;
        }

        /// <summary>
        /// Validates and updates an existing medicine.
        /// </summary>
        public bool UpdateMedicine(Medicine med, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!SessionManager.IsPharmacist() && !SessionManager.IsAdmin())
            {
                errorMessage = "Access denied. Only Pharmacists and Administrators can edit medications.";
                return false;
            }

            if (med.MedicineID <= 0)
            {
                errorMessage = "Invalid Medicine ID.";
                return false;
            }

            if (!ValidateMedicine(med, isNew: false, out errorMessage))
                return false;

            bool ok = _medicineRepo.UpdateMedicine(med);
            if (!ok)
            {
                errorMessage = "Failed to update medicine record in database.";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Adjusts medicine stock by a positive or negative quantity.
        /// </summary>
        public bool AdjustStock(int medicineId, int quantityChange, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (quantityChange == 0)
            {
                errorMessage = "Stock change cannot be zero.";
                return false;
            }

            var med = _medicineRepo.GetById(medicineId);
            if (med == null)
            {
                errorMessage = "Medicine not found.";
                return false;
            }

            if (med.StockQuantity + quantityChange < 0)
            {
                errorMessage = $"Cannot reduce stock by {Math.Abs(quantityChange)}. Current stock is only {med.StockQuantity}.";
                return false;
            }

            bool ok = _medicineRepo.UpdateStock(medicineId, quantityChange);
            if (!ok)
            {
                errorMessage = "Database failed to update stock level.";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Deactivates a medicine from active use.
        /// </summary>
        public bool DeactivateMedicine(int medicineId, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (!SessionManager.IsPharmacist() && !SessionManager.IsAdmin())
            {
                errorMessage = "Access denied. Only Pharmacists and Administrators can deactivate medications.";
                return false;
            }

            return _medicineRepo.DeactivateMedicine(medicineId);
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Validation Rules
        // ─────────────────────────────────────────────────────────────────────────

        public bool ValidateMedicine(Medicine med, bool isNew, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(med.MedicineCode))
            {
                errorMessage = "Medicine Code is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(med.MedicineName))
            {
                errorMessage = "Medicine Name is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(med.Unit))
            {
                errorMessage = "Unit of measurement (e.g., Tablet, Capsule, Syrup) is required.";
                return false;
            }

            if (med.UnitPrice < 0)
            {
                errorMessage = "Unit Price cannot be negative.";
                return false;
            }

            if (med.StockQuantity < 0)
            {
                errorMessage = "Stock Quantity cannot be negative.";
                return false;
            }

            if (med.ReorderLevel < 0)
            {
                errorMessage = "Reorder Level cannot be negative.";
                return false;
            }

            // Check code uniqueness if DB available
            try
            {
                var existing = _medicineRepo.GetByCode(med.MedicineCode.Trim());
                if (existing != null)
                {
                    if (isNew || existing.MedicineID != med.MedicineID)
                    {
                        errorMessage = $"Medicine Code '{med.MedicineCode}' is already assigned to '{existing.MedicineName}'.";
                        return false;
                    }
                }
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {
                // Proceed with validation if DB is offline
            }

            return true;
        }
    }
}
