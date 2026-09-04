using System;
using System.Collections.Generic;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Repositories;

namespace MEDICARE_HOSPITAL_MANGAMENT.Services
{
    /// <summary>
    /// Business logic service for patient registration, validation, and profile updates.
    /// </summary>
    public class PatientService
    {
        private readonly PatientRepository _patientRepository;

        public PatientService(PatientRepository? patientRepository = null)
        {
            _patientRepository = patientRepository ?? new PatientRepository();
        }

        public List<Patient> GetAllPatients(bool activeOnly = false) => _patientRepository.GetAllPatients(activeOnly);

        public Patient? GetPatientById(int patientId) => _patientRepository.GetPatientById(patientId);

        public Patient? GetPatientByCode(string patientCode) => _patientRepository.GetPatientByCode(patientCode);

        public List<Patient> SearchPatients(string query) => _patientRepository.SearchPatients(query);

        public string GetNextPatientCode() => _patientRepository.GenerateNextPatientCode();

        /// <summary>
        /// Registers a new patient with rigorous data validation.
        /// </summary>
        public bool RegisterPatient(Patient patient, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!ValidatePatientFields(patient, isNew: true, out errorMessage))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(patient.PatientCode))
            {
                patient.PatientCode = _patientRepository.GenerateNextPatientCode();
            }

            try
            {
                int newId = _patientRepository.CreatePatient(patient);
                patient.PatientID = newId;
                return newId > 0;
            }
            catch (Exception ex)
            {
                errorMessage = $"Failed to register patient: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// Updates an existing patient's details.
        /// </summary>
        public bool UpdatePatient(Patient patient, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!ValidatePatientFields(patient, isNew: false, out errorMessage))
            {
                return false;
            }

            try
            {
                return _patientRepository.UpdatePatient(patient);
            }
            catch (Exception ex)
            {
                errorMessage = $"Failed to update patient: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// Soft deletes/deactivates a patient record.
        /// </summary>
        public bool DeactivatePatient(int patientId, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                return _patientRepository.DeactivatePatient(patientId);
            }
            catch (Exception ex)
            {
                errorMessage = $"Failed to deactivate patient: {ex.Message}";
                return false;
            }
        }

        private bool ValidatePatientFields(Patient patient, bool isNew, out string errorMessage)
        {
            if (!ValidationHelper.IsNotEmpty(patient.FirstName, "First Name", out errorMessage))
                return false;

            if (!ValidationHelper.IsNotEmpty(patient.LastName, "Last Name", out errorMessage))
                return false;

            if (patient.DateOfBirth.Date > DateTime.Today)
            {
                errorMessage = "Date of Birth cannot be in the future.";
                return false;
            }

            if (patient.DateOfBirth.Date < new DateTime(1900, 1, 1))
            {
                errorMessage = "Date of Birth must be after 1900.";
                return false;
            }

            if (!ValidationHelper.IsNotEmpty(patient.Phone, "Phone Number", out errorMessage))
                return false;

            if (!ValidationHelper.IsValidPhone(patient.Phone))
            {
                errorMessage = "Phone number must be a valid 10-digit number (e.g. 0771234567).";
                return false;
            }

            // NIC validation if supplied
            if (!string.IsNullOrWhiteSpace(patient.NIC))
            {
                if (!ValidationHelper.IsValidNIC(patient.NIC))
                {
                    errorMessage = "NIC must be in a valid Sri Lankan format (e.g., 9 digits + 'V'/'X' or 12 digits).";
                    return false;
                }

                int? excludeId = isNew ? null : patient.PatientID;
                if (_patientRepository.IsNICTaken(patient.NIC, excludeId))
                {
                    errorMessage = $"A patient with NIC '{patient.NIC.Trim()}' is already registered in the system.";
                    return false;
                }
            }

            // Email validation if supplied
            if (!string.IsNullOrWhiteSpace(patient.Email) && !ValidationHelper.IsValidEmail(patient.Email))
            {
                errorMessage = "Email address format is invalid.";
                return false;
            }

            // Emergency contact phone validation if supplied
            if (!string.IsNullOrWhiteSpace(patient.EmergencyContactPhone) && !ValidationHelper.IsValidPhone(patient.EmergencyContactPhone))
            {
                errorMessage = "Emergency contact phone must be a valid 10-digit number.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }
    }
}
