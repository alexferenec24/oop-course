using System;
using ClinicApp;
using ClinicApp.Enums;
using ClinicApp.Managers;
using ClinicApp.Models;
using ClinicApp.Utils;
namespace ClinicApp;

public class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Clinic clinic = new Clinic("Міська клініка №1");

        SeedData(clinic);

        bool running = true;
        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("=== МЕНЮ ===");
            Console.WriteLine("1.  Показати всіх лікарів");
            Console.WriteLine("2.  Додати лікаря");
            Console.WriteLine("3.  Знайти лікаря за спеціальністю (рядок)");
            Console.WriteLine("4.  Показати всіх пацієнтів");
            Console.WriteLine("5.  Додати пацієнта");
            Console.WriteLine("6.  Створити запис на прийом");
            Console.WriteLine("7.  Показати записи на дату");
            Console.WriteLine("8.  Показати майбутні записи");
            Console.WriteLine("9.  Показати записи пацієнта");
            Console.WriteLine("10. Показати записи лікаря");
            Console.WriteLine("11. Скасувати запис");
            Console.WriteLine("12. Завершити запис");
            Console.WriteLine("13. Статистика пацієнтів");
            Console.WriteLine("14. Статистика лікарів");
            Console.WriteLine("15. Звіт клініки");
            Console.WriteLine("16. Демонстрація ClinicFormatter");
            Console.WriteLine("17. Демонстрація індексаторів");
            Console.WriteLine("18. Демонстрація WorkSchedule (value type)");
            Console.WriteLine("19. Демонстрація перевантажень, out, ?., ??");
            Console.WriteLine("0.  Вихід");
            Console.Write("Ваш вибір: ");

            string? choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":  clinic.Doctors.DisplayAll(); break;
                case "2":  AddDoctorFromMenu(clinic); break;
                case "3":  FindDoctorsBySpecialtyFromMenu(clinic); break;
                case "4":  clinic.Patients.DisplayAll(); break;
                case "5":  AddPatientFromMenu(clinic); break;
                case "6":  CreateAppointmentFromMenu(clinic); break;
                case "7":  ShowAppointmentsByDate(clinic); break;
                case "8":  ShowUpcomingAppointments(clinic); break;
                case "9":  ShowAppointmentsByPatient(clinic); break;
                case "10": ShowAppointmentsByDoctor(clinic); break;
                case "11": CancelAppointmentFromMenu(clinic); break;
                case "12": CompleteAppointmentFromMenu(clinic); break;
                case "13": clinic.Patients.DisplayStats(); break;
                case "14": clinic.Doctors.DisplayStats(); break;
                case "15": clinic.GenerateReport(); break;
                case "16": DemoClinicFormatter(); break;
                case "17": DemoIndexers(clinic); break;
                case "18": DemoScheduleValueSemantics(); break;
                case "19": DemoOverloadsAndOut(clinic); break;
                case "0":  running = false; break;
                default:   Console.WriteLine("Невірний вибір."); break;
            }
        }

        Console.WriteLine("До побачення!");
    }

    // ---------- Допоміжні методи введення ----------

    static string ReadString(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int value))
                return value;
            Console.WriteLine("Некоректне число, спробуйте ще.");
        }
    }

    static DateTime ReadDateTime(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            if (DateTime.TryParse(input, out DateTime value))
                return value;
            Console.WriteLine("Некоректна дата/час, спробуйте ще.");
        }
    }

    // ---------- Початкові дані ----------
    static void SeedData(Clinic clinic)
    {
        clinic.Doctors.Add(new Doctor("Олена", "Коваль", Specialty.Cardiology, "LIC-001", "0671111111"));
        clinic.Doctors.Add(new Doctor("Ігор", "Петренко", Specialty.Neurology, "LIC-002", "0672222222"));
        clinic.Doctors.Add(new Doctor("Марія", "Сидоренко", Specialty.Pediatrics, "LIC-003", "0673333333"));

        clinic.Patients.Add(new Patient("Іван", "Іванов", new DateTime(1985, 5, 12), BloodType.APositive, "0501111111"));
        clinic.Patients.Add(new Patient("Анна", "Петрова", new DateTime(1992, 8, 23), BloodType.ONegative, "0502222222"));

        // Book повертає bool, тому ID шукаємо через GetAll().Length
        clinic.Appointments.Book(1, 1, DateTime.Now.AddDays(1).Date.AddHours(10));
        clinic.Appointments.Book(2, 2, DateTime.Now.AddDays(2).Date.AddHours(14));
        clinic.Appointments.Book(1, 3, DateTime.Now.AddDays(-1).Date.AddHours(9));

        // Завершуємо минулий запис — знаходимо його серед усіх
        Appointment[] all = clinic.Appointments.GetAll();
        if (all.Length > 0)
        {
            Appointment last = all[all.Length - 1];
            clinic.Appointments.Complete(last.Id);
        }
    }

    // ---------- Лікарі ----------
    static void AddDoctorFromMenu(Clinic clinic)
    {
        string first = ReadString("Ім'я: ");
        string last  = ReadString("Прізвище: ");

        Specialty specialty = ReadSpecialtyFromMenu();

        string lic   = ReadString("Номер ліцензії: ");
        string phone = ReadString("Телефон (10 цифр): ");

        clinic.Doctors.Add(new Doctor(first, last, specialty, lic, phone));
    }

    static Specialty ReadSpecialtyFromMenu()
    {
        string[] names = Enum.GetNames(typeof(Specialty));
        Console.WriteLine("Оберіть спеціальність:");
        for (int i = 0; i < names.Length; i++)
        {
            Console.WriteLine($"  {i + 1}. {names[i]}");
        }

        while (true)
        {
            int choice = ReadInt("Номер: ");
            int index = choice - 1;
            if (index >= 0 && index < names.Length)
            {
                return (Specialty)index;
            }
            Console.WriteLine("Некоректний номер, спробуйте ще.");
        }
    }

    static void FindDoctorsBySpecialtyFromMenu(Clinic clinic)
    {
        string query = ReadString("Введіть спеціальність (напр., Cardiology): ");
        Doctor[] found = clinic.Doctors.FindBySpecialty(query);

        if (found == null || found.Length == 0)
        {
            Console.WriteLine("Нічого не знайдено.");
            return;
        }

        foreach (Doctor d in found)
        {
            Console.WriteLine(d.ToString());
        }
    }

    // ---------- Пацієнти ----------
    static void AddPatientFromMenu(Clinic clinic)
    {
        string first = ReadString("Ім'я: ");
        string last  = ReadString("Прізвище: ");

        DateTime dob = ReadDateTime("Дата народження (рррр-мм-дд): ");

        BloodType blood = ReadBloodTypeFromMenu();

        string phone = ReadString("Телефон (10 цифр): ");

        clinic.Patients.Add(new Patient(first, last, dob, blood, phone));
    }

    static BloodType ReadBloodTypeFromMenu()
    {
        string[] names = Enum.GetNames(typeof(BloodType));
        Console.WriteLine("Оберіть групу крові:");
        for (int i = 0; i < names.Length; i++)
        {
            Console.WriteLine($"  {i + 1}. {names[i]}");
        }

        while (true)
        {
            int choice = ReadInt("Номер: ");
            int index = choice - 1;
            if (index >= 0 && index < names.Length)
            {
                return (BloodType)index;
            }
            Console.WriteLine("Некоректний номер, спробуйте ще.");
        }
    }

    // ---------- Записи на прийом ----------
    static void CreateAppointmentFromMenu(Clinic clinic)
    {
        clinic.Patients.DisplayAll();
        int pid = ReadInt("ID пацієнта: ");

        clinic.Doctors.DisplayAll();
        int did = ReadInt("ID лікаря: ");

        DateTime at = ReadDateTime("Дата і час (рррр-мм-дд гг:хх): ");

        clinic.Appointments.Book(pid, did, at);
    }

    static void ShowAppointmentsByDate(Clinic clinic)
    {
        DateTime date = ReadDateTime("Введіть дату (рррр-мм-дд): ");

        Appointment[] list = clinic.Appointments.GetByDate(date);
        clinic.Appointments.DisplayList(list);
    }

    static void ShowUpcomingAppointments(Clinic clinic)
    {
        Appointment[] upcoming = clinic.Appointments.GetUpcoming();
        clinic.Appointments.DisplayList(upcoming);
    }

    static void ShowAppointmentsByPatient(Clinic clinic)
    {
        int pid = ReadInt("ID пацієнта: ");
        Appointment[] list = clinic.Appointments.GetByPatient(pid);
        clinic.Appointments.DisplayList(list);
    }

    static void ShowAppointmentsByDoctor(Clinic clinic)
    {
        int did = ReadInt("ID лікаря: ");
        Appointment[] list = clinic.Appointments.GetByDoctor(did);
        clinic.Appointments.DisplayList(list);
    }

    static void CancelAppointmentFromMenu(Clinic clinic)
    {
        int id = ReadInt("ID запису: ");
        string reason = ReadString("Причина (необов'язково): ");

        if (clinic.Appointments.Cancel(id, reason))
        {
            Console.WriteLine("Запис скасовано.");
        }
        else
        {
            Console.WriteLine("Не вдалося скасувати (можливо, запис не існує або вже не Scheduled).");
        }
    }

    static void CompleteAppointmentFromMenu(Clinic clinic)
    {
        int id = ReadInt("ID запису: ");

        if (clinic.Appointments.Complete(id))
        {
            Console.WriteLine("Запис завершено.");
        }
        else
        {
            Console.WriteLine("Не вдалося завершити (можливо, запис не існує або вже не Scheduled).");
        }
    }

    // ---------- Задача 3: демонстрація ClinicFormatter ----------
    static void DemoClinicFormatter()
    {
        Console.WriteLine("=== ClinicFormatter ===");

        Console.WriteLine("--- BloodType ---");
        Console.WriteLine($"APositive -> {ClinicFormatter.FormatBloodType(BloodType.APositive)}");
        Console.WriteLine($"BNegative -> {ClinicFormatter.FormatBloodType(BloodType.BNegative)}");
        Console.WriteLine($"Unknown   -> {ClinicFormatter.FormatBloodType(BloodType.Unknown)}");

        Console.WriteLine("--- Specialty ---");
        Console.WriteLine($"Cardiology -> {ClinicFormatter.FormatSpecialty(Specialty.Cardiology)}");
        Console.WriteLine($"General    -> {ClinicFormatter.FormatSpecialty(Specialty.General)}");
        Console.WriteLine($"Emergency  -> {ClinicFormatter.FormatSpecialty(Specialty.Emergency)}");

        Console.WriteLine("--- Age (відмінювання) ---");
        Console.WriteLine($"1   -> {ClinicFormatter.FormatAge(1)}");
        Console.WriteLine($"3   -> {ClinicFormatter.FormatAge(3)}");
        Console.WriteLine($"11  -> {ClinicFormatter.FormatAge(11)}");
        Console.WriteLine($"21  -> {ClinicFormatter.FormatAge(21)}");
        Console.WriteLine($"22  -> {ClinicFormatter.FormatAge(22)}");
        Console.WriteLine($"111 -> {ClinicFormatter.FormatAge(111)}");

        Console.WriteLine("--- Phone ---");
        Console.WriteLine($"\"0501234567\" -> {ClinicFormatter.FormatPhone("0501234567")}");
        Console.WriteLine($"\"123\"        -> {ClinicFormatter.FormatPhone("123")}");
        Console.WriteLine($"\"05012a4567\" -> {ClinicFormatter.FormatPhone("05012a4567")}");

        Console.WriteLine();
    }

    // ---------- Задача 3: демонстрація індексаторів ----------
    static void DemoIndexers(Clinic clinic)
    {
        Console.WriteLine("=== Індексатори ===");

        Patient? firstPatient         = clinic.Patients[0];
        Doctor?  secondDoctor         = clinic.Doctors[1];
        Appointment? thirdAppointment = clinic.Appointments[2];

        Console.WriteLine($"clinic.Patients[0]        -> {(firstPatient?.ToString() ?? "null (поза межами)")}");
        Console.WriteLine($"clinic.Doctors[1]         -> {(secondDoctor?.ToString() ?? "null (поза межами)")}");
        Console.WriteLine($"clinic.Appointments[2]    -> {(thirdAppointment?.ToString() ?? "null (поза межами)")}");

        Patient? outOfRange = clinic.Patients[999];
        Console.WriteLine($"clinic.Patients[999]      -> {(outOfRange?.ToString() ?? "null (поза межами)")}");

        Console.WriteLine();
    }

    // ---------- Задача 2: демонстрація value type ----------
    static void DemoScheduleValueSemantics()
    {
        Console.WriteLine("=== Демонстрація value type WorkSchedule ===");

        WorkSchedule morning = new WorkSchedule(8, 16);
        WorkSchedule evening = new WorkSchedule(14, 22);

        Console.WriteLine($"morning: {morning}");
        Console.WriteLine($"evening: {evening}");
        Console.WriteLine($"morning.IsNow: {morning.IsNow}");
        Console.WriteLine($"Contains(9):   {morning.Contains(9)}");
        Console.WriteLine($"Contains(20):  {morning.Contains(20)}");

        WorkSchedule copy = morning;
        Console.WriteLine($"copy:    {copy}");
        Console.WriteLine("(copy та morning — незалежні значення, бо WorkSchedule — struct)");

        Doctor d1 = new Doctor("Тест", "Тестовий", Specialty.General, "LIC-T", "0000000000", new WorkSchedule(9, 18));
        Doctor d2 = d1;
        d2.FirstName = "Змінений";
        Console.WriteLine($"Після d2.FirstName = \"Змінений\", d1.FirstName = {d1.FirstName}");
        Console.WriteLine("(клас — reference type: d1 і d2 вказують на один об'єкт)");
        Console.WriteLine();
    }

    // ---------- Задача 4: перевантаження, out, ?., ?? ----------
    static void DemoOverloadsAndOut(Clinic clinic)
    {
        Console.WriteLine("=== Перевантаження методів ===");

        Doctor[] cardiologists = clinic.Doctors.FindBySpecialty(Specialty.Cardiology);
        Console.WriteLine($"FindBySpecialty(Specialty.Cardiology): {cardiologists.Length}");

        Doctor[] found = clinic.Doctors.FindBySpecialty("кардіо");
        Console.WriteLine($"FindBySpecialty(\"кардіо\"):              {found.Length}");

        Appointment[] today = clinic.Appointments.GetByDate(
            DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        Console.WriteLine($"GetByDate(y, m, d) на сьогодні: {today.Length}");
        clinic.Appointments.DisplayList(today);

        Console.WriteLine();
        Console.WriteLine("=== TryFindById (out-параметр) ===");

        // out-параметр не nullable у менеджерах, тому просто викликаємо
        if (clinic.Patients.TryFindById(1, out Patient patient))
        {
            Console.WriteLine("Знайдено пацієнта: " + patient.FullName);
        }
        else
        {
            Console.WriteLine("Пацієнта не знайдено.");
        }

        if (clinic.Doctors.TryFindById(2, out Doctor doctor))
        {
            Console.WriteLine("Знайдено лікаря: " + doctor.FullName);
        }
        else
        {
            Console.WriteLine("Лікаря не знайдено.");
        }

        Patient[] aPositive = clinic.Patients.FindByBloodType(BloodType.APositive);
        Console.WriteLine($"Пацієнтів з A+: {aPositive.Length}");

        Console.WriteLine();
        Console.WriteLine("=== ?. та ?? ===");

        string name = clinic.Patients.FindById(99)?.FullName ?? "не знайдено";
        Console.WriteLine($"FindById(99)?.FullName ?? \"не знайдено\" -> {name}");

        string name2 = clinic.Patients.FindById(1)?.FullName ?? "не знайдено";
        Console.WriteLine($"FindById(1)?.FullName  ?? \"не знайдено\" -> {name2}");

        string docName = clinic.Doctors.FindById(999)?.FullName ?? "невідомий лікар";
        Console.WriteLine($"FindById(999)?.FullName ?? \"невідомий лікар\" -> {docName}");

        Console.WriteLine();
    }
}