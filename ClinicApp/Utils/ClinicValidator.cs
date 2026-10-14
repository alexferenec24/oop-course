using System;
using System.Text.RegularExpressions;

namespace ClinicApp.Utils
{
    public static class ClinicValidator
    {
        // Строго ASCII-цифри, рівно 10. \A і \z — якорі на початок/кінець рядка (без "майже кінця")
        private static readonly Regex PhonePattern = new Regex(@"\A[0-9]{10}\z");

        // Спрощена перевірка email: щось + @ + щось + . + щось
        private static readonly Regex EmailPattern = new Regex(@"\A[^@\s]+@[^@\s]+\.[^@\s]+\z");

        public static void ValidateName(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{fieldName} не може бути порожнім.", fieldName);
            if (value.Length > 50)
                throw new ArgumentException($"{fieldName} не може бути довшим за 50 символів.", fieldName);
        }

        public static void ValidatePhone(string phone)
        {
            if (phone == null || !PhonePattern.IsMatch(phone))
                throw new ArgumentException("Телефон має містити рівно 10 цифр (лише 0-9).", nameof(phone));
        }

        public static void ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return; // порожній email — «невідомо», дозволено

            if (!EmailPattern.IsMatch(email))
                throw new ArgumentException("Email має бути у форматі name@domain.tld.", nameof(email));
        }

        public static void ValidateDate(DateTime value, string fieldName)
        {
            if (value > DateTime.Today)
                throw new ArgumentOutOfRangeException(fieldName, "Дата народження не може бути в майбутньому.");
            if (value.Year < 1900)
                throw new ArgumentOutOfRangeException(fieldName, "Дата народження не може бути раніше 1900 року.");
        }

        public static void ValidatePositive(int value, string fieldName)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(fieldName, $"{fieldName} має бути додатним.");
        }
    }
}