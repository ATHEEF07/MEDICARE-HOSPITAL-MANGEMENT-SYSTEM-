using System;
using System.Collections.Generic;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Repositories;

namespace MEDICARE_HOSPITAL_MANGAMENT.Services
{
    public class DoctorService
    {
        private readonly DoctorRepository _doctorRepository;

        public DoctorService(DoctorRepository? doctorRepository = null)
        {
            _doctorRepository = doctorRepository ?? new DoctorRepository();
        }

        public List<Doctor> GetAllDoctors(bool activeOnly = false) => _doctorRepository.GetAllDoctors(activeOnly);

        public Doctor? GetDoctorById(int doctorId) => _doctorRepository.GetDoctorById(doctorId);

        public Doctor? GetDoctorByUserId(int userId) => _doctorRepository.GetDoctorByUserId(userId);

        public List<Doctor> GetDoctorsByDepartment(int departmentId) => _doctorRepository.GetDoctorsByDepartment(departmentId);

        public List<User> GetEligibleDoctorUserAccounts(int? currentDoctorUserId = null) =>
            _doctorRepository.GetEligibleDoctorUserAccounts(currentDoctorUserId);

        public string GetNextDoctorCode() => _doctorRepository.GenerateNextDoctorCode();

        public bool CreateDoctor(Doctor doctor, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (doctor.UserID <= 0)
            {
                errorMessage = "A valid User Account with role 'Doctor' must be selected.";
                return false;
            }

            if (doctor.DepartmentID <= 0)
            {
                errorMessage = "A Department must be assigned to the doctor.";
                return false;
            }

            if (!ValidationHelper.IsNotEmpty(doctor.FirstName, "First Name", out errorMessage))
                return false;

            if (!ValidationHelper.IsNotEmpty(doctor.LastName, "Last Name", out errorMessage))
                return false;

            if (!string.IsNullOrWhiteSpace(doctor.Phone) && !ValidationHelper.IsValidPhone(doctor.Phone))
            {
                errorMessage = "Doctor phone number must be a valid 10-digit number (e.g. 0771234567).";
                return false;
            }

            if (string.IsNullOrWhiteSpace(doctor.DoctorCode))
            {
                doctor.DoctorCode = _doctorRepository.GenerateNextDoctorCode();
            }

            try
            {
                int newId = _doctorRepository.CreateDoctor(doctor);
                doctor.DoctorID = newId;
                return newId > 0;
            }
            catch (Exception ex)
            {
                errorMessage = $"Failed to register doctor: {ex.Message}";
                return false;
            }
        }

        public bool UpdateDoctor(Doctor doctor, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (doctor.DepartmentID <= 0)
            {
                errorMessage = "A Department must be assigned to the doctor.";
                return false;
            }

            if (!ValidationHelper.IsNotEmpty(doctor.FirstName, "First Name", out errorMessage))
                return false;

            if (!ValidationHelper.IsNotEmpty(doctor.LastName, "Last Name", out errorMessage))
                return false;

            if (!string.IsNullOrWhiteSpace(doctor.Phone) && !ValidationHelper.IsValidPhone(doctor.Phone))
            {
                errorMessage = "Doctor phone number must be a valid 10-digit number (e.g. 0771234567).";
                return false;
            }

            try
            {
                return _doctorRepository.UpdateDoctor(doctor);
            }
            catch (Exception ex)
            {
                errorMessage = $"Failed to update doctor: {ex.Message}";
                return false;
            }
        }
    }
}
