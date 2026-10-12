using System;
using ClinicApp;

public class Patient
{
    private static int _nextId = 1;

    public int Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public BloodType BloodType { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

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
        : this("Невідомий", "Пацієнт", DateTime.Today, BloodType.Unknown, "0000000000")
    {
    }

    public Patient(string firstName, string lastName)
        : this(firstName, lastName, DateTime.Today, BloodType.Unknown, "0000000000")
    {
    }

    public Patient(string firstName, string lastName, DateTime dob, BloodType bloodType, string phone)
    {
        Id = _nextId++;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dob;
        BloodType = bloodType;
        Phone = phone;
        Email = "";
    }

    public string GetAgeCategory()
    {
        if (Age < 18) return "дитина";
        if (Age < 60) return "дорослий";
        return "літній";
    }

    public override string ToString()
    {
        return $"[{Id}] {FullName} | Вік: {Age} ({GetAgeCategory()}) | Кров: {BloodType} | Тел: {Phone}";
    }
}