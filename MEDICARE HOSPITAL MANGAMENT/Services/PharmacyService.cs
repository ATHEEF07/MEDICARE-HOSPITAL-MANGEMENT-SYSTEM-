using System;
using System.Collections.Generic;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Repositories;

namespace MEDICARE_HOSPITAL_MANGAMENT.Services
{
    /// <summary>
    /// Business Logic Layer for Pharmacy Dispensing.
    /// Enforces pharmacist authorization, stock pre-checks, and atomic stock decrements.
    /// </summary>
    public class PharmacyService
    {
        private readonly PrescriptionRepository _prescriptionRepo;
        private readonly MedicineRepository _medicineRepo;

        public PharmacyService()
        {
            _prescriptionRepo = new PrescriptionRepository();
            _medicineRepo = new MedicineRepository();
        }

        public PharmacyService(PrescriptionRepository prescriptionRepo, MedicineRepository medicineRepo)
        {
            _prescriptionRepo = prescriptionRepo;
            _medicineRepo = medicineRepo;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Queries & Queue
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Retrieves all pending active prescriptions waiting to be dispensed.
        /// </summary>
        public List<Prescription> GetPendingPrescriptionsQueue()
        {
            return _prescriptionRepo.GetPrescriptionsByStatus("Active");
        }

        /// <summary>
        /// Retrieves a prescription with full item stock status for the dispensing workbench.
        /// </summary>
        public Prescription? GetPrescriptionForDispensing(int prescriptionId)
        {
            return _prescriptionRepo.GetPrescriptionWithItems(prescriptionId);
        }

        /// <summary>
        /// Checks stock availability for all items in a prescription without modifying data.
        /// Returns true if all items can be fulfilled; otherwise returns false with shortage descriptions.
        /// </summary>
        public bool CheckStockAvailability(int prescriptionId, out List<string> shortages)
        {
            shortages = new List<string>();

            var rx = _prescriptionRepo.GetPrescriptionWithItems(prescriptionId);
            if (rx == null)
            {
                shortages.Add("Prescription record not found.");
                return false;
            }

            foreach (var item in rx.Items)
            {
                var med = _medicineRepo.GetById(item.MedicineID);
                if (med == null)
                {
                    shortages.Add($"Medicine '{item.MedicineName}' (ID {item.MedicineID}) no longer exists in inventory.");
                    continue;
                }

                if (med.StockQuantity < item.Quantity)
                {
                    shortages.Add(
                        $"Insufficient stock for '{med.MedicineName}': Required {item.Quantity} {med.Unit}s, Available {med.StockQuantity} {med.Unit}s.");
                }
            }

            return shortages.Count == 0;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Atomic Dispense Workflow
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Atomically validates stock sufficiency, decrements medicine inventory,
        /// and transitions the prescription to 'Dispensed'.
        /// </summary>
        public bool DispensePrescription(int prescriptionId, out string errorMessage)
        {
            errorMessage = string.Empty;

            // Authorization: Pharmacist or Administrator only
            if (!SessionManager.IsPharmacist() && !SessionManager.IsAdmin())
            {
                errorMessage = "Access denied. Only Pharmacists and Administrators can dispense prescriptions.";
                return false;
            }

            var rx = _prescriptionRepo.GetPrescriptionWithItems(prescriptionId);
            if (rx == null)
            {
                errorMessage = "Prescription not found.";
                return false;
            }

            if (rx.Status != "Active")
            {
                errorMessage = $"Cannot dispense prescription: Current status is '{rx.Status}'. Only 'Active' prescriptions can be dispensed.";
                return false;
            }

            if (rx.Items.Count == 0)
            {
                errorMessage = "Cannot dispense an empty prescription with no medication line items.";
                return false;
            }

            // Pre-check stock levels
            if (!CheckStockAvailability(prescriptionId, out var shortages))
            {
                errorMessage = string.Join("\n", shortages);
                return false;
            }

            // Execute atomic database transaction
            try
            {
                bool success = _prescriptionRepo.DispensePrescription(prescriptionId, rx.Items);
                if (!success)
                {
                    errorMessage = "Failed to finalize prescription dispensing.";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
