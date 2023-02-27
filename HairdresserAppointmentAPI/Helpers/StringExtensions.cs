using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace HairdresserAppointmentAPI.Helpers
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value?.Trim());
        }

        public static bool IsNotNullOrEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static string IsNull(this string value, string value2)
        {
            return value.IsNotNullOrEmpty() ? value : value2;
        }

        public static string HashString(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return null;
            }

            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(value);
            var hashedValue = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashedValue);
        }

    }
}
