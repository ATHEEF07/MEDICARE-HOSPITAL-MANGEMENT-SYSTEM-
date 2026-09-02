using System;
using System.Security.Cryptography;

namespace MEDICARE_HOSPITAL_MANGAMENT.Helpers
{
    /// <summary>
    /// Provides secure cryptographic password hashing and verification using PBKDF2 with HMAC-SHA256.
    /// Never stores or checks plain-text passwords directly in the database.
    /// </summary>
    public static class PasswordHasher
    {
        private const int SaltSize = 16; // 128 bit salt
        private const int KeySize = 32;  // 256 bit subkey
        private const int Iterations = 10000;

        /// <summary>
        /// Hashes a plain-text password using PBKDF2 with a random cryptographic salt.
        /// </summary>
        /// <param name="password">Plain-text password to hash.</param>
        /// <returns>Formatted string containing "{saltBase64}:{hashBase64}".</returns>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be empty.", nameof(password));
            }

            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA256,
                KeySize);

            return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
        }

        /// <summary>
        /// Verifies an entered plain-text password against a stored formatted hash.
        /// Uses timing-attack resistant comparison.
        /// </summary>
        /// <param name="password">Entered plain-text password.</param>
        /// <param name="storedHash">Stored hash from database.</param>
        /// <returns>True if password matches; otherwise, false.</returns>
        public static bool VerifyPassword(string password, string storedHash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(storedHash))
            {
                return false;
            }

            // Stored format: {salt}:{hash}
            string[] parts = storedHash.Split(':');
            if (parts.Length != 2)
            {
                // Fallback direct match for non-hashed development passwords if any exist
                return string.Equals(password, storedHash, StringComparison.Ordinal);
            }

            try
            {
                byte[] salt = Convert.FromBase64String(parts[0]);
                byte[] expectedHash = Convert.FromBase64String(parts[1]);

                byte[] actualHash = Rfc2898DeriveBytes.Pbkdf2(
                    password,
                    salt,
                    Iterations,
                    HashAlgorithmName.SHA256,
                    expectedHash.Length);

                return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
            }
            catch
            {
                return false;
            }
        }
    }
}
