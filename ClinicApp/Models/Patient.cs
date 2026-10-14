using System;
using ClinicApp.Enums;
using ClinicApp.Utils;

namespace ClinicApp.Models
{
    public class Patient
    {
        private static int _nextId = 1;

        private string _firstName = "";
        private string _lastName = "";
        private DateTime _dateOfBirth;
        private string _phone = "";

        public int Id { get; }
        public BloodType BloodType { get; set; }
        public string Email { get; set; }

        public string FirstName
        {
            get => _firstName;
            set
            {
                ClinicValidator.ValidateName(value, nameof(FirstName));
                _firstName = value;
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                ClinicValidator.ValidateName(value, nameof(LastName));
                _lastName = value;
            }
        }

        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                ClinicValidator.ValidateDate(value, nameof(DateOfBirth));
                _dateOfBirth = value;
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                ClinicValidator.ValidatePhone(value);
                _phone = value;
            }
        }

        public string FullName => $"{FirstName} {LastName}";

        public int Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - DateOfBirth.Year;
                if (DateOfBirth.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

        public bool IsAdult => Age >= 18;

        public Patient()
            : this("Невідомий", "Пацієнт", new DateTime(1900, 1, 1), BloodType.Unknown, "0000000000")
        {
        }

        public Patient(string firstName, string lastName)
            : this(firstName, lastName, new DateTime(1900, 1, 1), BloodType.Unknown, "0000000000")
        {
        }

        public Patient(string firstName, string lastName, DateTime dob, BloodType bloodType, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dob;
            BloodType = bloodType;
            Phone = phone;
            Email = "";

            Id = _nextId++;
        }

        public string GetAgeCategory()
        {
            if (Age < 18) return "дитина";
            if (Age < 60) return "дорослий";
            return "літній";
        }

        public override string ToString()
        {
            return $"[{Id}] {FullName} | Вік: {ClinicFormatter.FormatAge(Age)} ({GetAgeCategory()}) | "
                   + $"Кров: {ClinicFormatter.FormatBloodType(BloodType)} | Тел: {ClinicFormatter.FormatPhone(Phone)}";
        }
    }
}