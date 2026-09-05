using System;
using System.Collections.Generic;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Repositories;

namespace MEDICARE_HOSPITAL_MANGAMENT.Services
{
    /// <summary>
    /// Business Logic Layer for Appointment scheduling, enforcing
    /// business rules BR-03, BR-04, BR-11 (overlap prevention, date validation,
    /// status transitions, minimum slot length).
    /// </summary>
    public class AppointmentService
    {
        private readonly AppointmentRepository _appointmentRepo;
        private readonly PatientRepository _patientRepo;
        private readonly DoctorRepository _doctorRepo;

        public AppointmentService()
        {
            _appointmentRepo = new AppointmentRepository();
            _patientRepo     = new PatientRepository();
            _doctorRepo      = new DoctorRepository();
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Read / Query pass-through (with authorization)
        // ─────────────────────────────────────────────────────────────────────────

        public List<Appointment> GetAppointmentsByDate(DateTime date, int? doctorId = null)
            => _appointmentRepo.GetAppointmentsByDate(date, doctorId);

        public List<Appointment> GetAppointmentsForDoctor(int doctorId, DateTime? date = null)
            => _appointmentRepo.GetAppointmentsForDoctor(doctorId, date);

        public List<Appointment> GetAppointmentsForPatient(int patientId)
            => _appointmentRepo.GetAppointmentsForPatient(patientId);

        public List<Appointment> SearchAppointments(DateTime? fromDate, DateTime? toDate, int? doctorId, string? status, string? patientSearch)
            => _appointmentRepo.SearchAppointments(fromDate, toDate, doctorId, status, patientSearch);

        public Appointment? GetAppointmentById(int appointmentId)
            => _appointmentRepo.GetAppointmentById(appointmentId);

        // ─────────────────────────────────────────────────────────────────────────
        // Create
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Validates and creates a new appointment.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when input data is invalid.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the time slot is already taken.</exception>
        public int CreateAppointment(Appointment appt, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!ValidateAppointmentInput(appt, isReschedule: false, out errorMessage))
                return 0;

            appt.Status = "Scheduled";
            appt.CreatedByUserID = SessionManager.CurrentUserId;

            return _appointmentRepo.CreateAppointment(appt);
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Reschedule
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Reschedules an existing appointment to a new date/time slot.
        /// Only 'Scheduled' appointments can be rescheduled.
        /// </summary>
        public bool RescheduleAppointment(Appointment appt, out string errorMessage)
        {
            errorMessage = string.Empty;

            var existing = _appointmentRepo.GetAppointmentById(appt.AppointmentID);
            if (existing == null)
            {
                errorMessage = "Appointment not found.";
                return false;
            }

            if (existing.Status != "Scheduled")
            {
                errorMessage = $"Cannot reschedule: appointment is currently '{existing.Status}'.";
                return false;
            }

            if (!ValidateAppointmentInput(appt, isReschedule: true, out errorMessage))
                return false;

            return _appointmentRepo.UpdateAppointment(appt);
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Status Transitions
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Cancels a Scheduled appointment. Completed/NoShow appointments cannot be cancelled.
        /// </summary>
        public bool CancelAppointment(int appointmentId, out string errorMessage)
        {
            errorMessage = string.Empty;
            var appt = _appointmentRepo.GetAppointmentById(appointmentId);
            if (appt == null) { errorMessage = "Appointment not found."; return false; }

            if (appt.Status == "Completed")
            {
                errorMessage = "A completed appointment cannot be cancelled.";
                return false;
            }
            if (appt.Status == "Cancelled")
            {
                errorMessage = "This appointment is already cancelled.";
                return false;
            }

            return _appointmentRepo.UpdateAppointmentStatus(appointmentId, "Cancelled");
        }

        /// <summary>
        /// Marks a Scheduled appointment as Completed.
        /// Typically called automatically when a consultation is saved.
        /// </summary>
        public bool MarkCompleted(int appointmentId, out string errorMessage)
        {
            errorMessage = string.Empty;
            var appt = _appointmentRepo.GetAppointmentById(appointmentId);
            if (appt == null) { errorMessage = "Appointment not found."; return false; }

            if (appt.Status != "Scheduled")
            {
                errorMessage = $"Cannot mark as Completed: appointment is currently '{appt.Status}'.";
                return false;
            }

            return _appointmentRepo.UpdateAppointmentStatus(appointmentId, "Completed");
        }

        /// <summary>
        /// Marks a Scheduled appointment as NoShow.
        /// </summary>
        public bool MarkNoShow(int appointmentId, out string errorMessage)
        {
            errorMessage = string.Empty;
            var appt = _appointmentRepo.GetAppointmentById(appointmentId);
            if (appt == null) { errorMessage = "Appointment not found."; return false; }

            if (appt.Status != "Scheduled")
            {
                errorMessage = $"Cannot mark as No-Show: appointment is currently '{appt.Status}'.";
                return false;
            }

            return _appointmentRepo.UpdateAppointmentStatus(appointmentId, "NoShow");
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Availability Check (can be called from UI before saving)
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Returns true if the doctor is available for the specified slot.
        /// Optionally excludes the current appointment during a reschedule check.
        /// </summary>
        public bool IsDoctorAvailable(int doctorId, DateTime date, TimeSpan startTime, TimeSpan endTime, int? excludeAppointmentId = null)
        {
            return !_appointmentRepo.HasDoctorOverlap(doctorId, date, startTime, endTime, excludeAppointmentId);
        }

        // ─────────────────────────────────────────────────────────────────────────
        // Private Validation
        // ─────────────────────────────────────────────────────────────────────────

        private bool ValidateAppointmentInput(Appointment appt, bool isReschedule, out string errorMessage)
        {
            errorMessage = string.Empty;

            // Patient exists
            var patient = _patientRepo.GetPatientById(appt.PatientID);
            if (patient == null || !patient.IsActive)
            {
                errorMessage = "Selected patient does not exist or is inactive.";
                return false;
            }

            // Doctor exists
            var doctor = _doctorRepo.GetDoctorById(appt.DoctorID);
            if (doctor == null || !doctor.IsActive)
            {
                errorMessage = "Selected doctor does not exist or is inactive.";
                return false;
            }

            // Date not in the past
            if (appt.AppointmentDate.Date < DateTime.Today)
            {
                errorMessage = "Appointment date cannot be in the past.";
                return false;
            }

            // EndTime > StartTime and minimum 15-minute slot
            if (appt.EndTime <= appt.StartTime)
            {
                errorMessage = "End time must be after start time.";
                return false;
            }
            if ((appt.EndTime - appt.StartTime).TotalMinutes < 15)
            {
                errorMessage = "Appointment slot must be at least 15 minutes long.";
                return false;
            }

            // Double-booking / overlap check
            int? excludeId = isReschedule ? appt.AppointmentID : null;
            if (_appointmentRepo.HasDoctorOverlap(appt.DoctorID, appt.AppointmentDate, appt.StartTime, appt.EndTime, excludeId))
            {
                var start = DateTime.Today.Add(appt.StartTime).ToString("hh:mm tt");
                var end   = DateTime.Today.Add(appt.EndTime).ToString("hh:mm tt");
                errorMessage = $"Doctor already has an active appointment between {start} and {end} on {appt.AppointmentDate:dd/MM/yyyy}. Please choose a different time slot.";
                return false;
            }

            return true;
        }
    }
}
