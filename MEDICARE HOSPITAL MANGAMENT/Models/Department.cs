namespace MEDICARE_HOSPITAL_MANGAMENT.Models
{
    /// <summary>
    /// Represents a clinical or administrative hospital department (e.g., General Medicine, Pediatrics, Cardiology).
    /// </summary>
    public class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;

        public override string ToString() => DepartmentName;
    }
}
