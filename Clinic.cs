using System;
using ClinicApp.Managers;
using ClinicApp.Models;

namespace ClinicApp;

public class Clinic
{
    public string Name { get; set; }

    public DoctorManager Doctors { get; }
    public PatientManager Patients { get; }
    public AppointmentManager Appointments { get; }
    public MedicalRecordManager MedicalRecords { get; }   // ← ДОДАНО

    public Clinic(string name)
    {
        Name = name;
        Doctors = new DoctorManager();
        Patients = new PatientManager();
        Appointments = new AppointmentManager(Patients, Doctors);
        MedicalRecords = new MedicalRecordManager();      // ← ДОДАНО
    }

    public void GenerateReport()
    {
        Console.WriteLine($"=== ЗВІТ КЛІНІКИ: {Name} ===");
        Console.WriteLine($"Лікарів:      {Doctors.Count}");
        Console.WriteLine($"Пацієнтів:    {Patients.Count}");
        Console.WriteLine($"Записів:      {Appointments.Count}");
        Console.WriteLine($"Мед. записів: {MedicalRecords.Count}");   // ← ДОДАНО (опційно, для краси)
        Console.WriteLine(new string('=', 50));
    }
}