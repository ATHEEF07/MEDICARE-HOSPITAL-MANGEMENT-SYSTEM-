using System;

namespace MEDICARE_HOSPITAL_MANGAMENT.Models
{
    /// <summary>
    /// Represents a patient registered in the MediCare hospital management system.
    /// </summary>
    public class Patient
    {
        public int PatientID { get; set; }
        public string PatientCode { get; set; } = string.Empty;
        public string? NIC { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = DateTime.Today;
        public string Gender { get; set; } = "Male";
        public string? BloodGroup { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;

        // Computed properties
        public string FullName => $"{FirstName} {LastName}".Trim();

        public int Age
        {
            get
            {
                var today = DateTime.Today;
                int age = today.Year - DateOfBirth.Year;
                if (DateOfBirth.Date > today.AddYears(-age))
                {
                    age--;
                }
                return Math.Max(0, age);
            }
        }

        public override string ToString() => $"{FullName} ({PatientCode}) - NIC: {NIC ?? "N/A"}";
    }
}
