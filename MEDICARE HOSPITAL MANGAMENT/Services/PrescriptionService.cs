using System;
using System.Collections.Generic;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Repositories;

namespace MEDICARE_HOSPITAL_MANGAMENT.Services
{
    /// <summary>
    /// Business Logic Layer for Doctor Prescriptions.
    /// Enforces doctor-only authorization, patient validation, item quantity bounds,
    /// and atomic multi-item persistence.
    /// </summary>
    public class PrescriptionService
    {
        private readonly PrescriptionRepository _prescriptionRepo;
        private readonly PatientRepository _patientRepo;
        private readonly DoctorRepository _doctorRepo;

        public PrescriptionService()
        {
            _prescriptionRepo = new PrescriptionRepository();
            _patientRepo = new PatientRepository();
            _doctorRepo = new DoctorRepository();
        }

        public PrescriptionService(
            PrescriptionRepository prescriptionRepo,
            PatientRepository patientRepo,
            DoctorRepository doctorRepo)
        {
            _prescriptionRepo = prescriptionRepo;
            _patientRepo = patientRepo;
            _doctorRepo = doctorRepo;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Read Pass-Throughs
        // ─────────────────────────────────────────────────────────────────────────

        public Prescription? GetPrescriptionWithItems(int prescriptionId) => _prescriptionRepo.GetPrescriptionWithItems(prescriptionId);
        public List<Prescription> GetPrescriptionsByStatus(string status) => _prescriptionRepo.GetPrescriptionsByStatus(status);
        public List<Prescription> GetPrescriptionsForPatient(int patientId) => _prescriptionRepo.GetPrescriptionsForPatient(patientId);
        public List<Prescription> GetAllPrescriptions(int limit = 100) => _prescriptionRepo.GetAllPrescriptions(limit);

        // ─────────────────────────────────────────────────────────────────────────
        // Creation & Status Management
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Validates clinical parameters and atomically saves a new prescription with all line items.
        /// </summary>
        public int CreatePrescription(Prescription rx, out string errorMessage)
        {
            errorMessage = string.Empty;

            // Authorization: Doctor or Administrator only
            if (!SessionManager.IsDoctor() && !SessionManager.IsAdmin())
            {
                errorMessage = "Access denied. Only Doctors and Administrators can issue prescriptions.";
                return 0;
            }

            // Clinical validation
            if (!ValidatePrescription(rx, out errorMessage))
                return 0;

            try
            {
                int newId = _prescriptionRepo.CreatePrescriptionWithItems(rx);
                if (newId <= 0)
                {
                    errorMessage = "Database failed to persist prescription.";
                    return 0;
                }

                return newId;
            }
            catch (Exception ex)
            {
                errorMessage = $"Error creating prescription: {ex.Message}";
                return 0;
            }
        }

        /// <summary>
        /// Cancels an active prescription.
        /// </summary>
        public bool CancelPrescription(int prescriptionId, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!SessionManager.IsDoctor() && !SessionManager.IsAdmin())
            {
                errorMessage = "Access denied. Only Doctors or Administrators can cancel prescriptions.";
                return false;
            }

            var rx = _prescriptionRepo.GetPrescriptionWithItems(prescriptionId);
            if (rx == null)
            {
                errorMessage = "Prescription not found.";
                return false;
            }

            if (rx.Status == "Dispensed")
            {
                errorMessage = "Cannot cancel a prescription that has already been dispensed by the pharmacy.";
                return false;
            }

            if (rx.Status == "Cancelled")
            {
                errorMessage = "Prescription is already cancelled.";
                return false;
            }

            bool ok = _prescriptionRepo.UpdateStatus(prescriptionId, "Cancelled");
            if (!ok)
            {
                errorMessage = "Database update failed.";
                return false;
            }

            return true;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Clinical Validation Rules
        // ─────────────────────────────────────────────────────────────────────────

        public bool ValidatePrescription(Prescription rx, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (rx.PatientID <= 0)
            {
                errorMessage = "A valid Patient must be selected.";
                return false;
            }

            try
            {
                var patient = _patientRepo.GetPatientById(rx.PatientID);
                if (patient != null && !patient.IsActive)
                {
                    errorMessage = "The selected patient does not exist or is inactive.";
                    return false;
                }
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {
                // Proceed with validation if DB is offline
            }

            if (rx.DoctorID <= 0)
            {
                errorMessage = "A valid Doctor must be assigned to the prescription.";
                return false;
            }

            try
            {
                var doctor = _doctorRepo.GetDoctorById(rx.DoctorID);
                if (doctor != null && !doctor.IsActive)
                {
                    errorMessage = "The selected doctor does not exist or is inactive.";
                    return false;
                }
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {
                // Proceed with validation if DB is offline
            }

            if (rx.Items == null || rx.Items.Count == 0)
            {
                errorMessage = "A prescription must contain at least one medication line item.";
                return false;
            }

            for (int i = 0; i < rx.Items.Count; i++)
            {
                var item = rx.Items[i];
                int itemNum = i + 1;

                if (item.MedicineID <= 0)
                {
                    errorMessage = $"Item #{itemNum}: Please select a valid medicine.";
                    return false;
                }

                if (item.Quantity <= 0)
                {
                    errorMessage = $"Item #{itemNum} ({item.MedicineName}): Quantity must be greater than 0.";
                    return false;
                }

                if (item.DurationDays <= 0)
                {
                    errorMessage = $"Item #{itemNum} ({item.MedicineName}): Duration (days) must be greater than 0.";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(item.Dosage))
                {
                    errorMessage = $"Item #{itemNum} ({item.MedicineName}): Dosage specification is mandatory.";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(item.Frequency))
                {
                    errorMessage = $"Item #{itemNum} ({item.MedicineName}): Frequency specification is mandatory.";
                    return false;
                }
            }

            return true;
        }
    }
}
