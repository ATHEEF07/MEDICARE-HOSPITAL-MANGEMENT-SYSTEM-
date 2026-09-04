using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MEDICARE_HOSPITAL_MANGAMENT.Helpers
{
    /// <summary>
    /// Provides reusable input validation methods for patients, users, medical numbers, and formats.
    /// </summary>
    public static class ValidationHelper
    {
        // Sri Lankan National Identity Card (NIC) patterns:
        // Old NIC format: 9 digits followed by 'V' or 'X' (e.g., 901234567V)
        private static readonly Regex OldNicRegex = new(@"^[0-9]{9}[vVxX]$", RegexOptions.Compiled);
        // New NIC format: 12 digits (e.g., 199012345678, 200012345678)
        private static readonly Regex NewNicRegex = new(@"^[0-9]{12}$", RegexOptions.Compiled);

        // Phone: 10 digits starting with 0 (e.g., 0771234567) or with +94
        private static readonly Regex PhoneRegex = new(@"^(?:0|\+94)[0-9]{9}$", RegexOptions.Compiled);

        // Standard email format
        private static readonly Regex EmailRegex = new(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Validates whether a given NIC conforms to either the Sri Lankan Old (9 digits + V/X) or New (12 digits) format.
        /// </summary>
        public static bool IsValidNIC(string? nic)
        {
            if (string.IsNullOrWhiteSpace(nic))
                return false;

            string clean = nic.Trim();
            return OldNicRegex.IsMatch(clean) || NewNicRegex.IsMatch(clean);
        }

        /// <summary>
        /// Validates whether a phone number contains a valid 10-digit format (e.g., 0771234567).
        /// </summary>
        public static bool IsValidPhone(string? phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            string clean = phone.Trim().Replace(" ", "").Replace("-", "");
            return PhoneRegex.IsMatch(clean);
        }

        /// <summary>
        /// Validates whether an email address matches standard syntax.
        /// </summary>
        public static bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return EmailRegex.IsMatch(email.Trim());
        }

        /// <summary>
        /// Validates that a string field is not null or whitespace.
        /// </summary>
        public static bool IsNotEmpty(string? input, string fieldName, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                errorMessage = $"{fieldName} is required.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }

        /// <summary>
        /// Validates that an input is a valid positive decimal value.
        /// </summary>
        public static bool IsPositiveDecimal(string? input, string fieldName, out decimal value, out string errorMessage)
        {
            value = 0;
            if (string.IsNullOrWhiteSpace(input) || !decimal.TryParse(input.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out value) && !decimal.TryParse(input.Trim(), out value))
            {
                errorMessage = $"{fieldName} must be a valid numeric amount.";
                return false;
            }

            if (value < 0)
            {
                errorMessage = $"{fieldName} cannot be negative.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }

        /// <summary>
        /// Validates that an input is a valid positive integer value.
        /// </summary>
        public static bool IsPositiveInteger(string? input, string fieldName, out int value, out string errorMessage)
        {
            value = 0;
            if (string.IsNullOrWhiteSpace(input) || !int.TryParse(input.Trim(), out value))
            {
                errorMessage = $"{fieldName} must be a valid whole number.";
                return false;
            }

            if (value <= 0)
            {
                errorMessage = $"{fieldName} must be greater than zero.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }

        /// <summary>
        /// Validates that the end date/time is strictly equal to or after the start date/time.
        /// </summary>
        public static bool IsValidDateRange(DateTime start, DateTime end, out string errorMessage)
        {
            if (end < start)
            {
                errorMessage = "End date/time cannot be earlier than start date/time.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }
    }
}
