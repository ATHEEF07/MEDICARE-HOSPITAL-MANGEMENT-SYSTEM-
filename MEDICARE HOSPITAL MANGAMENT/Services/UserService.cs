using System;
using System.Collections.Generic;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Repositories;

namespace MEDICARE_HOSPITAL_MANGAMENT.Services
{
    /// <summary>
    /// Business logic service for user administration, profile management, and password updates.
    /// </summary>
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository? userRepository = null)
        {
            _userRepository = userRepository ?? new UserRepository();
        }

        public List<User> GetAllUsers() => _userRepository.GetAllUsers();

        public List<Role> GetAllRoles() => _userRepository.GetAllRoles();

        public User? GetUserById(int userId) => _userRepository.GetById(userId);

        /// <summary>
        /// Validates and registers a new system user with a securely hashed password.
        /// </summary>
        public bool CreateUser(User user, string plainPassword, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!ValidationHelper.IsNotEmpty(user.FullName, "Full Name", out errorMessage))
                return false;

            if (!ValidationHelper.IsNotEmpty(user.Username, "Username", out errorMessage))
                return false;

            if (user.Username.Trim().Length < 3)
            {
                errorMessage = "Username must be at least 3 characters long.";
                return false;
            }

            if (_userRepository.IsUsernameTaken(user.Username))
            {
                errorMessage = $"Username '{user.Username.Trim()}' is already in use.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(plainPassword) || plainPassword.Length < 6)
            {
                errorMessage = "Password must be at least 6 characters long.";
                return false;
            }

            if (user.RoleID <= 0)
            {
                errorMessage = "A valid system role must be selected.";
                return false;
            }

            try
            {
                user.PasswordHash = PasswordHasher.HashPassword(plainPassword);
                int newId = _userRepository.CreateUser(user);
                user.UserID = newId;
                return newId > 0;
            }
            catch (Exception ex)
            {
                errorMessage = $"Failed to create user: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// Updates a user's details while enforcing admin protection rules.
        /// </summary>
        public bool UpdateUser(User user, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!ValidationHelper.IsNotEmpty(user.FullName, "Full Name", out errorMessage))
                return false;

            if (user.RoleID <= 0)
            {
                errorMessage = "A valid system role must be selected.";
                return false;
            }

            var existing = _userRepository.GetById(user.UserID);
            if (existing == null)
            {
                errorMessage = "User not found.";
                return false;
            }

            // Guard: Cannot deactivate or demote the last remaining active Administrator
            if (existing.RoleName == "Administrator" && (!user.IsActive || user.RoleID != existing.RoleID))
            {
                if (_userRepository.GetActiveAdminCount() <= 1)
                {
                    errorMessage = "Operation blocked: The system must retain at least one active Administrator.";
                    return false;
                }
            }

            try
            {
                return _userRepository.UpdateUser(user);
            }
            catch (Exception ex)
            {
                errorMessage = $"Failed to update user: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// Toggles a user's active status. Protects the last Administrator account.
        /// </summary>
        public bool ToggleActiveStatus(int userId, out string errorMessage)
        {
            errorMessage = string.Empty;
            var user = _userRepository.GetById(userId);
            if (user == null)
            {
                errorMessage = "User not found.";
                return false;
            }

            bool targetStatus = !user.IsActive;

            // If deactivating an Administrator, ensure another active admin remains
            if (!targetStatus && user.RoleName == "Administrator")
            {
                if (_userRepository.GetActiveAdminCount() <= 1)
                {
                    errorMessage = "Cannot deactivate the last active Administrator account.";
                    return false;
                }
            }

            try
            {
                return _userRepository.ToggleUserActiveStatus(userId, targetStatus);
            }
            catch (Exception ex)
            {
                errorMessage = $"Failed to toggle user status: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// Resets a user's password (Administrator action).
        /// </summary>
        public bool ResetPassword(int userId, string newPassword, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
            {
                errorMessage = "New password must be at least 6 characters long.";
                return false;
            }

            try
            {
                string hash = PasswordHasher.HashPassword(newPassword);
                return _userRepository.UpdatePassword(userId, hash);
            }
            catch (Exception ex)
            {
                errorMessage = $"Failed to reset password: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// Self-service password change by an authenticated user.
        /// </summary>
        public bool ChangePassword(int userId, string currentPassword, string newPassword, string confirmPassword, out string errorMessage)
        {
            errorMessage = string.Empty;

            var user = _userRepository.GetById(userId);
            if (user == null)
            {
                errorMessage = "User not found.";
                return false;
            }

            if (!PasswordHasher.VerifyPassword(currentPassword, user.PasswordHash))
            {
                errorMessage = "Current password is incorrect.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
            {
                errorMessage = "New password must be at least 6 characters long.";
                return false;
            }

            if (!string.Equals(newPassword, confirmPassword, StringComparison.Ordinal))
            {
                errorMessage = "New password and confirmation password do not match.";
                return false;
            }

            try
            {
                string hash = PasswordHasher.HashPassword(newPassword);
                return _userRepository.UpdatePassword(userId, hash);
            }
            catch (Exception ex)
            {
                errorMessage = $"Failed to change password: {ex.Message}";
                return false;
            }
        }
    }
}
