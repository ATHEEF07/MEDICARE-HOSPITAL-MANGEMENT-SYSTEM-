using System;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Repositories;

namespace MEDICARE_HOSPITAL_MANGAMENT.Services
{
    /// <summary>
    /// Business logic service for user login, credentials verification, and session lifecycle.
    /// </summary>
    public class AuthenticationService
    {
        private readonly UserRepository _userRepository;

        public AuthenticationService(UserRepository? userRepository = null)
        {
            _userRepository = userRepository ?? new UserRepository();
        }

        /// <summary>
        /// Authenticates a user by validating credentials, account status, and password hash.
        /// On success, initializes the active SessionManager state.
        /// </summary>
        /// <param name="username">User's login username.</param>
        /// <param name="password">Plain-text password.</param>
        /// <param name="errorMessage">Output error message if authentication fails.</param>
        /// <returns>The authenticated User object if successful; otherwise, null.</returns>
        public User? Authenticate(string username, string password, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!ValidationHelper.IsNotEmpty(username, "Username", out errorMessage))
            {
                return null;
            }

            if (!ValidationHelper.IsNotEmpty(password, "Password", out errorMessage))
            {
                return null;
            }

            try
            {
                var user = _userRepository.GetByUsername(username);

                // Use generic error message to prevent username enumeration attacks
                if (user == null)
                {
                    errorMessage = "Invalid username or password.";
                    return null;
                }

                if (!user.IsActive)
                {
                    errorMessage = "This account has been deactivated. Please contact your system administrator.";
                    return null;
                }

                bool isPasswordValid = PasswordHasher.VerifyPassword(password, user.PasswordHash);
                if (!isPasswordValid)
                {
                    errorMessage = "Invalid username or password.";
                    return null;
                }

                // Authentication succeeded - establish global session
                SessionManager.CurrentUser = user;
                return user;
            }
            catch (Exception ex)
            {
                errorMessage = $"Database connection error: {ex.Message}";
                return null;
            }
        }

        /// <summary>
        /// Terminates the current authenticated session.
        /// </summary>
        public void Logout()
        {
            SessionManager.Logout();
        }
    }
}
