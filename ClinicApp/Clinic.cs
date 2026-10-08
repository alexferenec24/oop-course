using System;

public class Clinic
{
    public string Name { get; set; }
    public PatientManager Patients { get; }
    public DoctorManager Doctors { get; }
    public AppointmentManager Appointments { get; }

    public Clinic(string name)
    {
        Name = name;
        Patients = new PatientManager();
        Doctors = new DoctorManager();
        Appointments = new AppointmentManager(Patients, Doctors);
    }

    public void DisplaySchedule(DateTime date)
    {
        Console.WriteLine($"=== Розклад на {date:dd.MM.yyyy} ===");
        Appointment[] appointments = Appointments.GetByDate(date);
        Appointments.DisplayList(appointments);
        Console.WriteLine();
    }

    public void GenerateReport()
    {
        Console.WriteLine("┌────────────────────────────────────────────────────────┐");
        Console.WriteLine($"│ Звіт — {Name,-46} │");
        Console.WriteLine("├────────────────────────────────────────────────────────┤");
        Console.WriteLine($"│ Пацієнтів:        {Patients.Count,-35} │");
        Console.WriteLine($"│ Лікарів:          {Doctors.Count,-35} │");
        
        Appointment[] upcoming = Appointments.GetUpcoming();
        Console.WriteLine($"│ Майбутніх записів: {upcoming.Length,-34} │");
        
        Console.WriteLine("├────────────────────────────────────────────────────────┤");
        Console.WriteLine("│ Навантаження лікарів (майбутні записи):                │");

        Doctor[] allDoctors = Doctors.GetAll();
        for (int i = 0; i < allDoctors.Length; i++)
        {
            int doctorAppointmentsCount = 0;
            for (int j = 0; j < upcoming.Length; j++)
            {
                if (upcoming[j].DoctorId == allDoctors[i].Id)
                {
                    doctorAppointmentsCount++;
                }
            }
            Console.WriteLine($"│   {allDoctors[i].FullName} ({allDoctors[i].Specialty}): {doctorAppointmentsCount} записів");
        }
        Console.WriteLine("└────────────────────────────────────────────────────────┘");
    }
}