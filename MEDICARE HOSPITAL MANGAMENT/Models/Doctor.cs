namespace MEDICARE_HOSPITAL_MANGAMENT.Models
{
    /// <summary>
    /// Represents a medical doctor profile attached to a system user account and clinical department.
    /// </summary>
    public class Doctor
    {
        public int DoctorID { get; set; }
        public int UserID { get; set; }
        public int DepartmentID { get; set; }
        public string DoctorCode { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Specialization { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; } = true;

        // Joined display properties
        public string DepartmentName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string FullName => $"Dr. {FirstName} {LastName}".Trim();

        public override string ToString() => $"{FullName} ({DoctorCode}) - {Specialization}";
    }
}
