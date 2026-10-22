using System;
using ClinicApp.Enums;
using ClinicApp.Interfaces;
using ClinicApp.Utils;

namespace ClinicApp.Models
{
    public class Doctor : ISchedulable
    {
        private static int _nextId = 1;

        private string _firstName = "";
        private string _lastName = "";
        private string _licenseNumber = "";
        private string _phone = "";

        public int Id { get; }
        public Specialty Specialty { get; set; }
        public WorkSchedule Schedule { get; set; }

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

        public string LicenseNumber
        {
            get => _licenseNumber;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Номер ліцензії не може бути порожнім.", nameof(LicenseNumber));
                _licenseNumber = value;
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
        public int WorkingHoursPerDay => Schedule.HoursPerDay;
        public bool IsAvailableNow => Schedule.IsNow;

        public Doctor()
            : this("Невідомий", "Лікар", Specialty.General, "LIC-000", "0000000000")
        {
        }

        public Doctor(string firstName, string lastName, Specialty specialty)
            : this(firstName, lastName, specialty, "LIC-000", "0000000000")
        {
        }

        public Doctor(string firstName, string lastName, Specialty specialty,
                      string licenseNumber, string phone)
            : this(firstName, lastName, specialty, licenseNumber, phone, new WorkSchedule(8, 17))
        {
        }

        public Doctor(string firstName, string lastName, Specialty specialty,
                      string licenseNumber, string phone, WorkSchedule schedule)
        {
            FirstName = firstName;
            LastName = lastName;
            Specialty = specialty;
            LicenseNumber = licenseNumber;
            Phone = phone;
            Schedule = schedule;

            Id = _nextId++;
        }

        public bool CanAcceptAt(int hour)
        {
            return Schedule.Contains(hour);
        }

        public bool CanSchedule(DateTime at)
        {
            return Schedule.Contains(at.Hour);
        }

        public DateTime[] GetAvailableSlots(DateTime date, int slotCount)
        {
            ClinicValidator.ValidatePositive(slotCount, nameof(slotCount));

            int maxSlots = Math.Min(slotCount, Schedule.HoursPerDay);
            DateTime[] result = new DateTime[maxSlots];

            for (int i = 0; i < maxSlots; i++)
            {
                result[i] = date.Date.AddHours(Schedule.Start + i);
            }

            return result;
        }

        public override string ToString()
        {
            string status = IsAvailableNow ? "доступний" : "недоступний";
            return $"[{Id}] {FullName} | {ClinicFormatter.FormatSpecialty(Specialty)} | {LicenseNumber} | "
                 + $"Тел: {ClinicFormatter.FormatPhone(Phone)} | {Schedule} | {status}";
        }
    }
}