using System;
using MEDICARE_HOSPITAL_MANGAMENT.Models;

namespace MEDICARE_HOSPITAL_MANGAMENT.Helpers
{
    /// <summary>
    /// Thread-safe in-memory session manager tracking the currently authenticated user and their active role.
    /// </summary>
    public static class SessionManager
    {
        private static readonly object _lock = new();
        private static User? _currentUser;

        /// <summary>
        /// Gets or sets the currently authenticated user in the active session.
        /// </summary>
        public static User? CurrentUser
        {
            get
            {
                lock (_lock)
                {
                    return _currentUser;
                }
            }
            set
            {
                lock (_lock)
                {
                    _currentUser = value;
                }
            }
        }

        /// <summary>
        /// Gets whether a valid user session is currently active.
        /// </summary>
        public static bool IsLoggedIn => CurrentUser != null;

        /// <summary>
        /// Gets the UserID of the currently logged-in user, or 0 if not logged in.
        /// </summary>
        public static int CurrentUserId => CurrentUser?.UserID ?? 0;

        /// <summary>
        /// Gets the username of the currently logged-in user.
        /// </summary>
        public static string CurrentUsername => CurrentUser?.Username ?? string.Empty;

        /// <summary>
        /// Gets the full name of the currently logged-in user.
        /// </summary>
        public static string CurrentUserFullName => CurrentUser?.FullName ?? string.Empty;

        /// <summary>
        /// Gets the assigned role name of the currently logged-in user (e.g., 'Administrator', 'Doctor').
        /// </summary>
        public static string CurrentUserRole => CurrentUser?.RoleName ?? string.Empty;

        /// <summary>
        /// Checks whether the currently logged-in user belongs to the specified role (case-insensitive).
        /// </summary>
        public static bool HasRole(string roleName)
        {
            if (!IsLoggedIn || string.IsNullOrWhiteSpace(roleName))
                return false;

            return string.Equals(CurrentUserRole, roleName.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Convenience check for Administrator role.
        /// </summary>
        public static bool IsAdmin() => HasRole("Administrator");

        /// <summary>
        /// Convenience check for Doctor role.
        /// </summary>
        public static bool IsDoctor() => HasRole("Doctor");

        /// <summary>
        /// Convenience check for Receptionist role.
        /// </summary>
        public static bool IsReceptionist() => HasRole("Receptionist");

        /// <summary>
        /// Convenience check for Pharmacist role.
        /// </summary>
        public static bool IsPharmacist() => HasRole("Pharmacist");

        /// <summary>
        /// Convenience check for Cashier role.
        /// </summary>
        public static bool IsCashier() => HasRole("Cashier");

        /// <summary>
        /// Clears the active session and logs out the user.
        /// </summary>
        public static void Logout()
        {
            lock (_lock)
            {
                _currentUser = null;
            }
        }
    }
}
