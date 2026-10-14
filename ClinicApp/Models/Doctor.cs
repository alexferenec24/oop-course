using System;
using ClinicApp.Enums;
using ClinicApp.Utils;

namespace ClinicApp.Models
{
    public class Doctor
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
            set => _firstName = value;
        }

        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }

        public string LicenseNumber
        {
            get => _licenseNumber;
            set => _licenseNumber = value;
        }

        public string Phone
        {
            get => _phone;
            set => _phone = value;
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
            Id = _nextId++;
            FirstName = firstName;
            LastName = lastName;
            Specialty = specialty;
            LicenseNumber = licenseNumber;
            Phone = phone;
            Schedule = schedule;
        }

        public bool CanAcceptAt(int hour)
        {
            return Schedule.Contains(hour);
        }

        public override string ToString()
        {
            string status = IsAvailableNow ? "доступний" : "недоступний";
            return $"[{Id}] {FullName} | {ClinicFormatter.FormatSpecialty(Specialty)} | {LicenseNumber} | "
                 + $"Тел: {ClinicFormatter.FormatPhone(Phone)} | {Schedule} | {status}";
        }
    }
}