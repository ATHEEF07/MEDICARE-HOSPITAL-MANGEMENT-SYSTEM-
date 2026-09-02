namespace MEDICARE_HOSPITAL_MANGAMENT.Models
{
    /// <summary>
    /// Represents a system role (e.g. Administrator, Receptionist, Doctor, Pharmacist, Cashier).
    /// </summary>
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string? Description { get; set; }

        public override string ToString() => RoleName;
    }
}
