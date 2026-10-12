using System;
using ClinicApp;

class Program
{
    static DoctorManager doctorManager = new DoctorManager();
    static Patient[] patients = new Patient[100];
    static int patientCount = 0;
    static Appointment[] appointments = new Appointment[100];
    static int appointmentCount = 0;

    static void Main()
    {
        SeedData();
        bool running = true;
        while (running)
        {
            Console.WriteLine("\n=== МЕНЮ ===");
            Console.WriteLine("1. Показати всіх лікарів");
            Console.WriteLine("2. Додати лікаря");
            Console.WriteLine("3. Знайти лікаря за спеціальністю");
            Console.WriteLine("4. Показати статистику лікарів");
            Console.WriteLine("5. Показати всіх пацієнтів");
            Console.WriteLine("6. Додати пацієнта");
            Console.WriteLine("7. Створити запис на прийом");
            Console.WriteLine("8. Показати всі записи");
            Console.WriteLine("0. Вихід");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1": doctorManager.DisplayAll(); break;
                case "2": AddDoctorFromMenu(); break;
                case "3": FindDoctorsBySpecialtyFromMenu(); break;
                case "4": doctorManager.DisplayStats(); break;
                case "5": ShowAllPatients(); break;
                case "6": AddPatientFromMenu(); break;
                case "7": CreateAppointmentFromMenu(); break;
                case "8": ShowAllAppointments(); break;
                case "0": running = false; break;
                default: Console.WriteLine("Невірний вибір."); break;
            }
        }
    }

    static void SeedData()
    {
        // Пункт 6 підказок: початкові дані передаються значеннями enum, а не рядками
        doctorManager.Add(new Doctor("Олена", "Коваль", Specialty.Cardiology, "LIC-001", "0671111111"));
        doctorManager.Add(new Doctor("Ігор", "Петренко", Specialty.Neurology, "LIC-002", "0672222222"));
        doctorManager.Add(new Doctor("Марія", "Сидоренко", Specialty.Pediatrics, "LIC-003", "0673333333"));

        patients[patientCount++] = new Patient("Іван", "Іванов", new DateTime(1985, 5, 12), BloodType.APositive, "0501111111");
        patients[patientCount++] = new Patient("Анна", "Петрова", new DateTime(1992, 8, 23), BloodType.ONegative, "0502222222");
    }

    // Демонстрація явного приведення (ТипEnum)число для Specialty
    static void AddDoctorFromMenu()
    {
        Console.Write("Ім'я: ");
        string first = Console.ReadLine();
        Console.Write("Прізвище: ");
        string last = Console.ReadLine();

        Specialty specialty = ReadSpecialtyFromMenu();

        Console.Write("Номер ліцензії: ");
        string lic = Console.ReadLine();
        Console.Write("Телефон: ");
        string phone = Console.ReadLine();

        doctorManager.Add(new Doctor(first, last, specialty, lic, phone));
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
            Console.Write("Номер: ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                int index = choice - 1;
                if (index >= 0 && index < names.Length)
                {
                    // Явне приведення числа до типу перерахування
                    return (Specialty)index;
                }
            }
            Console.WriteLine("Некоректний номер, спробуйте ще.");
        }
    }

    static void FindDoctorsBySpecialtyFromMenu()
    {
        Console.Write("Введіть спеціальність (напр., Cardiology): ");
        string query = Console.ReadLine();
        Doctor[] found = doctorManager.FindBySpecialty(query);

        if (found.Length == 0)
        {
            Console.WriteLine("Нічого не знайдено.");
            return;
        }

        foreach (Doctor d in found)
        {
            Console.WriteLine(d.ToString());
        }
    }

    static void AddPatientFromMenu()
    {
        if (patientCount >= patients.Length)
        {
            Console.WriteLine("Масив пацієнтів заповнений.");
            return;
        }

        Console.Write("Ім'я: ");
        string first = Console.ReadLine();
        Console.Write("Прізвище: ");
        string last = Console.ReadLine();
        Console.Write("Дата народження (рррр-мм-дд): ");
        DateTime dob = DateTime.Parse(Console.ReadLine());

        BloodType blood = ReadBloodTypeFromMenu();

        Console.Write("Телефон: ");
        string phone = Console.ReadLine();

        patients[patientCount++] = new Patient(first, last, dob, blood, phone);
        Console.WriteLine("Пацієнта додано.");
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
            Console.Write("Номер: ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                int index = choice - 1;
                if (index >= 0 && index < names.Length)
                {
                    return (BloodType)index;
                }
            }
            Console.WriteLine("Некоректний номер, спробуйте ще.");
        }
    }

    static void ShowAllPatients()
    {
        if (patientCount == 0)
        {
            Console.WriteLine("Пацієнтів немає.");
            return;
        }
        for (int i = 0; i < patientCount; i++)
        {
            Console.WriteLine(patients[i].ToString());
        }
    }

    static void CreateAppointmentFromMenu()
    {
        if (appointmentCount >= appointments.Length)
        {
            Console.WriteLine("Масив записів заповнений.");
            return;
        }

        ShowAllPatients();
        Console.Write("ID пацієнта: ");
        int pid = int.Parse(Console.ReadLine());

        doctorManager.DisplayAll();
        Console.Write("ID лікаря: ");
        int did = int.Parse(Console.ReadLine());

        Console.Write("Дата і час (рррр-мм-дд гг:хх): ");
        DateTime at = DateTime.Parse(Console.ReadLine());

        appointments[appointmentCount++] = new Appointment(pid, did, at);
        Console.WriteLine("Запис створено.");
    }

    static void ShowAllAppointments()
    {
        if (appointmentCount == 0)
        {
            Console.WriteLine("Записів немає.");
            return;
        }
        for (int i = 0; i < appointmentCount; i++)
        {
            Console.WriteLine(appointments[i].ToString());
        }
    }
}