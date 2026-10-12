using System;
using ClinicApp;

namespace ClinicApp
{
    public class Doctor
    {
        private static int _nextId = 1;

        public int Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Specialty Specialty { get; set; }
        public string LicenseNumber { get; set; }
        public string Phone { get; set; }

        // Пункт 5 підказок: одне поле типу WorkSchedule
        public WorkSchedule Schedule { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        // Тепер уся логіка живе в WorkSchedule
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
        {
            Id = _nextId++;
            FirstName = firstName;
            LastName = lastName;
            Specialty = specialty;
            LicenseNumber = licenseNumber;
            Phone = phone;
            // Стандартний розклад, як був раніше: 8–17
            Schedule = new WorkSchedule(8, 17);
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

        // Пункт 5: CanAcceptAt тепер просто делегує до Schedule.Contains
        public bool CanAcceptAt(int hour)
        {
            return Schedule.Contains(hour);
        }

        public override string ToString()
        {
            string status = IsAvailableNow ? "доступний" : "недоступний";
            return $"[{Id}] {FullName} | {Specialty} | {LicenseNumber} | Тел: {Phone} | {Schedule} | {status}";
        }
    }
}