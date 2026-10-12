using System;
using System.Text;

namespace ClinicApp
{
    // Пункт 1 підказок: static class без екземплярів, лише public static методи
    public static class ClinicFormatter
    {
        // Пункт 2: switch-вираз для перетворення enum → рядок
        public static string FormatBloodType(BloodType bt)
        {
            return bt switch
            {
                BloodType.APositive => "A+",
                BloodType.ANegative => "A-",
                BloodType.BPositive => "B+",
                BloodType.BNegative => "B-",
                BloodType.ABPositive => "AB+",
                BloodType.ABNegative => "AB-",
                BloodType.OPositive => "O+",
                BloodType.ONegative => "O-",
                _ => "Невідомо"
            };
        }

        public static string FormatSpecialty(Specialty s)
        {
            return s switch
            {
                Specialty.General => "Терапія",
                Specialty.Cardiology => "Кардіологія",
                Specialty.Neurology => "Неврологія",
                Specialty.Pediatrics => "Педіатрія",
                Specialty.Surgery => "Хірургія",
                Specialty.Orthopedics => "Ортопедія",
                Specialty.Dermatology => "Дерматологія",
                Specialty.Emergency => "Невідкладна допомога",
                _ => "Невідомо"
            };
        }

        // Пункт 3: правила українського відмінювання
        public static string FormatAge(int age)
        {
            // остача від ділення на 100 у діапазоні 11–19 → завжди "років"
            int mod100 = age % 100;
            if (mod100 >= 11 && mod100 <= 19)
            {
                return $"{age} років";
            }

            // інакше дивимось останню цифру
            int mod10 = age % 10;
            if (mod10 == 1)
            {
                return $"{age} рік";
            }
            if (mod10 >= 2 && mod10 <= 4)
            {
                return $"{age} роки";
            }
            return $"{age} років";
        }

        // Пункт 4: телефон → "(050) 123-4567"
        public static string FormatPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone) || phone.Length != 10)
            {
                return phone;
            }

            // усі 10 символів мають бути цифрами
            for (int i = 0; i < phone.Length; i++)
            {
                if (!char.IsDigit(phone[i]))
                {
                    return phone;
                }
            }

            // (050) 123-4567
            return $"({phone.Substring(0, 3)}) {phone.Substring(3, 3)}-{phone.Substring(6, 4)}";
        }
    }
}