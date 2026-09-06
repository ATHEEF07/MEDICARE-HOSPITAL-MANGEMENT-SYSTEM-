using System;
using System.Collections.Generic;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Repositories;

namespace MEDICARE_HOSPITAL_MANGAMENT.Services
{
    /// <summary>
    /// Business Logic Layer for clinical consultations (Medical Records).
    /// Enforces BR-05: Doctor-only write access, mandatory Symptoms + Diagnosis,
    /// and auto-completion of the linked appointment on save.
    /// </summary>
    public class MedicalRecordService
    {
        private readonly MedicalRecordRepository _recordRepo;
        private readonly AppointmentRepository   _appointmentRepo;
        private readonly PatientRepository       _patientRepo;
        private readonly DoctorRepository        _doctorRepo;

        public MedicalRecordService()
        {
            _recordRepo      = new MedicalRecordRepository();
            _appointmentRepo = new AppointmentRepository();
            _patientRepo     = new PatientRepository();
            _doctorRepo      = new DoctorRepository();
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Read pass-throughs
        // ─────────────────────────────────────────────────────────────────────────

        public MedicalRecord? GetById(int recordId) => _recordRepo.GetById(recordId);
        public MedicalRecord? GetByAppointmentId(int appointmentId) => _recordRepo.GetByAppointmentId(appointmentId);
        public List<MedicalRecord> GetRecordsByPatientId(int patientId) => _recordRepo.GetRecordsByPatientId(patientId);
        public List<MedicalRecord> GetRecordsByDoctorId(int doctorId)   => _recordRepo.GetRecordsByDoctorId(doctorId);

        // ─────────────────────────────────────────────────────────────────────────
        // Create
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Validates and creates a new medical record.
        /// When the record is linked to an AppointmentID, that appointment is
        /// automatically transitioned to 'Completed'.
        /// </summary>
        public int SaveConsultation(MedicalRecord record, out string errorMessage)
        {
            errorMessage = string.Empty;

            // BR-05: Only Doctor or Administrator may record consultations
            if (!SessionManager.IsDoctor() && !SessionManager.IsAdmin())
            {
                errorMessage = "Access denied. Only Doctors and Administrators can record consultations.";
                return 0;
            }

            // Validate mandatory clinical fields
            if (!ValidateRecord(record, out errorMessage))
                return 0;

            // Persist
            int newId = _recordRepo.CreateRecord(record);
            if (newId <= 0)
            {
                errorMessage = "Failed to save consultation. Please try again.";
                return 0;
            }

            // Auto-complete the linked appointment (BR-05 side effect)
            if (record.AppointmentID.HasValue)
            {
                _appointmentRepo.UpdateAppointmentStatus(record.AppointmentID.Value, "Completed");
            }

            return newId;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Update
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Updates an existing medical record's clinical data.
        /// </summary>
        public bool UpdateConsultation(MedicalRecord record, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!SessionManager.IsDoctor() && !SessionManager.IsAdmin())
            {
                errorMessage = "Access denied. Only Doctors and Administrators can update consultations.";
                return false;
            }

            if (!ValidateRecord(record, out errorMessage))
                return false;

            if (record.MedicalRecordID <= 0)
            {
                errorMessage = "Invalid medical record reference.";
                return false;
            }

            return _recordRepo.UpdateRecord(record);
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Private validation
        // ─────────────────────────────────────────────────────────────────────────

        private bool ValidateRecord(MedicalRecord record, out string errorMessage)
        {
            errorMessage = string.Empty;

            // Patient must exist
            var patient = _patientRepo.GetPatientById(record.PatientID);
            if (patient == null || !patient.IsActive)
            {
                errorMessage = "Patient not found or is inactive.";
                return false;
            }

            // Doctor must exist
            var doctor = _doctorRepo.GetDoctorById(record.DoctorID);
            if (doctor == null || !doctor.IsActive)
            {
                errorMessage = "Doctor not found or is inactive.";
                return false;
            }

            // Symptoms mandatory
            if (string.IsNullOrWhiteSpace(record.Symptoms))
            {
                errorMessage = "Symptoms are required. Please describe the patient's presenting complaints.";
                return false;
            }

            // Diagnosis mandatory
            if (string.IsNullOrWhiteSpace(record.Diagnosis))
            {
                errorMessage = "Diagnosis is required. Please enter the clinical diagnosis.";
                return false;
            }

            return true;
        }
    }
}
