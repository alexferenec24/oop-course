using System;
using System.Globalization;
using ClinicApp.Enums;
using ClinicApp.Interfaces;
using ClinicApp.Managers;
using ClinicApp.Models;
using ClinicApp.Utils;

namespace ClinicApp;

public class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        Clinic clinic = new Clinic("Міська клініка №1");
        SeedData(clinic);

        bool running = true;
        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("┌──────────────────────────────────────────────┐");
            Console.WriteLine("│               МЕДИЧНА КЛІНІКА                │");
            Console.WriteLine("├──────────────────────────────────────────────┤");
            Console.WriteLine("│ 1. Пацієнти      - реєстрація, пошук         │");
            Console.WriteLine("│ 2. Лікарі        - персонал, розклад         │");
            Console.WriteLine("│ 3. Записи        - прийоми, скасування       │");
            Console.WriteLine("│ 4. Медична картка- діагнози, рецепти         │");
            Console.WriteLine("│ 5. Рахунки       - оплата, борги             │");
            Console.WriteLine("│ 6. Звіт          - загальна статистика       │");
            Console.WriteLine("│ 0. Вихід                                     │");
            Console.WriteLine("└──────────────────────────────────────────────┘");
            Console.Write("Оберіть розділ: ");

            string? choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1": PatientsMenu(clinic); break;
                case "2": DoctorsMenu(clinic); break;
                case "3": AppointmentsMenu(clinic); break;
                case "4": MedicalCardMenu(clinic); break;
                case "5": BillingMenu(clinic); break;
                case "6": clinic.GenerateReport(); break;
                case "0": running = false; break;
                default: Console.WriteLine("Невірний вибір."); break;
            }
        }

        Console.WriteLine("До побачення!");
    }

    static void PatientsMenu(Clinic clinic)
    {
        bool inMenu = true;
        while (inMenu)
        {
            Console.WriteLine("\n--- Пацієнти ---");
            Console.WriteLine("1. Показати всіх пацієнтів");
            Console.WriteLine("2. Додати пацієнта");
            Console.WriteLine("3. Статистика пацієнтів");
            Console.WriteLine("0. Назад");
            Console.Write("Оберіть: ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1": clinic.Patients.DisplayAll(); break;
                case "2": AddPatientFromMenu(clinic); break;
                case "3": clinic.Patients.DisplayStats(); break;
                case "0": inMenu = false; break;
                default: Console.WriteLine("Невірний вибір."); break;
            }
        }
    }

    static void DoctorsMenu(Clinic clinic)
    {
        bool inMenu = true;
        while (inMenu)
        {
            Console.WriteLine("\n--- Лікарі ---");
            Console.WriteLine("1. Показати всіх лікарів");
            Console.WriteLine("2. Додати лікаря");
            Console.WriteLine("3. Знайти за спеціальністю");
            Console.WriteLine("4. Статистика лікарів");
            Console.WriteLine("5. Вільні години лікаря");
            Console.WriteLine("0. Назад");
            Console.Write("Оберіть: ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1": clinic.Doctors.DisplayAll(); break;
                case "2": AddDoctorFromMenu(clinic); break;
                case "3": FindDoctorsBySpecialtyFromMenu(clinic); break;
                case "4": clinic.Doctors.DisplayStats(); break;
                case "5": ShowDoctorAvailableSlots(clinic); break;
                case "0": inMenu = false; break;
                default: Console.WriteLine("Невірний вибір."); break;
            }
        }
    }

    static void AppointmentsMenu(Clinic clinic)
    {
        bool inMenu = true;
        while (inMenu)
        {
            Console.WriteLine("\n--- Записи ---");
            Console.WriteLine("1. Створити запис на прийом");
            Console.WriteLine("2. Показати записи на дату");
            Console.WriteLine("3. Показати майбутні записи");
            Console.WriteLine("4. Показати записи пацієнта");
            Console.WriteLine("5. Показати записи лікаря");
            Console.WriteLine("6. Скасувати запис");
            Console.WriteLine("7. Завершити запис");
            Console.WriteLine("8. Скасувати всі записи пацієнта");
            Console.WriteLine("0. Назад");
            Console.Write("Оберіть: ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1": CreateAppointmentFromMenu(clinic); break;
                case "2": ShowAppointmentsByDate(clinic); break;
                case "3": ShowUpcomingAppointments(clinic); break;
                case "4": ShowAppointmentsByPatient(clinic); break;
                case "5": ShowAppointmentsByDoctor(clinic); break;
                case "6": CancelAppointmentFromMenu(clinic); break;
                case "7": CompleteAppointmentFromMenu(clinic); break;
                case "8": CancelAllPatientAppointments(clinic); break;
                case "0": inMenu = false; break;
                default: Console.WriteLine("Невірний вибір."); break;
            }
        }
    }

    static void BillingMenu(Clinic clinic)
    {
        bool inMenu = true;
        while (inMenu)
        {
            Console.WriteLine("\n--- Рахунки ---");
            Console.WriteLine("1. Борги пацієнта");
            Console.WriteLine("2. Всі неоплачені записи");
            Console.WriteLine("3. Оплатити запис");
            Console.WriteLine("4. Загальний борг клініки");
            Console.WriteLine("0. Назад");
            Console.Write("Оберіть: ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1": ShowPatientDebt(clinic); break;
                case "2": clinic.Billing.DisplayUnpaid(clinic.Billing.GetAllUnpaid()); break;
                case "3": PayAppointment(clinic); break;
                case "4": Console.WriteLine($"Загальний борг по клініці: {clinic.Billing.GetTotalDebt().ToString("F2")} грн"); break;
                case "0": inMenu = false; break;
                default: Console.WriteLine("Невірний вибір."); break;
            }
        }
    }

    static void MedicalCardMenu(Clinic clinic)
    {
        bool inMenu = true;
        while (inMenu)
        {
            Console.WriteLine("\n--- Медична картка ---");
            Console.WriteLine("1. Картка пацієнта (зведення)");
            Console.WriteLine("2. Усі записи пацієнта");
            Console.WriteLine("3. Додати діагноз");
            Console.WriteLine("4. Додати аналіз");
            Console.WriteLine("5. Додати рецепт");
            Console.WriteLine("6. Записи лікаря");
            Console.WriteLine("0. Назад");
            Console.Write("Оберіть: ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1": ShowPatientSummary(clinic); break;
                case "2": ShowPatientRecords(clinic); break;
                case "3": AddDiagnosisFromMenu(clinic); break;
                case "4": AddLabResultFromMenu(clinic); break;
                case "5": AddPrescriptionFromMenu(clinic); break;
                case "6": ShowDoctorRecords(clinic); break;
                case "0": inMenu = false; break;
                default: Console.WriteLine("Невірний вибір."); break;
            }
        }
    }

    static void ShowPatientDebt(Clinic clinic)
    {
        int pid = ReadInt("ID пацієнта: ");
        Console.WriteLine($"Неоплачені записи пацієнта #{pid}:");
        clinic.Billing.DisplayUnpaid(clinic.Billing.GetUnpaidByPatient(pid));
        Console.WriteLine($"Борг: {clinic.Billing.GetPatientDebt(pid).ToString("F2")} грн");
    }

    static void PayAppointment(Clinic clinic)
    {
        int id = ReadInt("ID запису для оплати: ");
        if (clinic.Billing.PayAppointment(id))
        {
            Console.WriteLine($"Запис [{id}] оплачено.");
        }
        else
        {
            Console.WriteLine("Не вдалося оплатити: запис не знайдено, вже оплачено або скасовано.");
        }
    }

    static void ShowDoctorAvailableSlots(Clinic clinic)
    {
        int did = ReadInt("ID лікаря: ");
        Doctor? doctor = clinic.Doctors.FindById(did);
        if (doctor == null)
        {
            Console.WriteLine("Лікаря не знайдено.");
            return;
        }

        DateTime date = ReadDateTimeExact("Дата (dd.MM.yyyy): ");
        int count = ReadInt("Скільки слотів: ");

        try
        {
            ISchedulable schedulable = doctor;
            DateTime[] slots = schedulable.GetAvailableSlots(date, count);
            for (int i = 0; i < slots.Length; i++)
            {
                Console.WriteLine(slots[i].ToString("HH:mm"));
            }
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    static void CancelAllPatientAppointments(Clinic clinic)
    {
        int pid = ReadInt("ID пацієнта: ");
        string reason = ReadString("Причина (Enter — без причини): ");
        
        Appointment[] appointments = clinic.Appointments.GetByPatient(pid);
        int cancelledCount = AppointmentManager.CancelAll(appointments, reason);
        Console.WriteLine($"Скасовано записів: {cancelledCount}");
    }

    static void ShowPatientSummary(Clinic clinic)
    {
        int pid = ReadInt("ID пацієнта: ");
        clinic.MedicalRecords.DisplayPatientSummary(pid);
    }

    static void ShowPatientRecords(Clinic clinic)
    {
        int pid = ReadInt("ID пацієнта: ");
        MedicalRecord[] records = clinic.MedicalRecords.GetByPatient(pid);
        clinic.MedicalRecords.DisplayList(records);
    }

    static void AddDiagnosisFromMenu(Clinic clinic)
    {
        try
        {
            clinic.Patients.DisplayAll();
            int pid = ReadInt("ID пацієнта: ");
            clinic.Doctors.DisplayAll();
            int did = ReadInt("ID лікаря: ");
            string code = ReadString("Код діагнозу (напр. J06.9): ");
            string desc = ReadString("Опис: ");
            int chronicInput = ReadInt("Хронічне? (1=так, 0=ні): ");
            clinic.MedicalRecords.Add(new Diagnosis(pid, did, DateTime.Today, code, desc, chronicInput == 1));
        }
        catch (Exception ex) when (ex is ArgumentException || ex is ArgumentOutOfRangeException)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    static void AddLabResultFromMenu(Clinic clinic)
    {
        try
        {
            clinic.Patients.DisplayAll();
            int pid = ReadInt("ID пацієнта: ");
            clinic.Doctors.DisplayAll();
            int did = ReadInt("ID лікаря: ");
            string name  = ReadString("Назва аналізу: ");
            double value = ReadDouble("Значення (число): ");
            string unit  = ReadString("Одиниці виміру: ");
            string range = ReadString("Норма (напр. 4.0-9.0): ");
            int normalInput = ReadInt("В нормі? (1=так, 0=ні): ");
            clinic.MedicalRecords.Add(new LabResult(pid, did, DateTime.Today, name, value, unit, range, normalInput == 1));
        }
        catch (Exception ex) when (ex is ArgumentException || ex is ArgumentOutOfRangeException)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    static void AddPrescriptionFromMenu(Clinic clinic)
    {
        try
        {
            clinic.Patients.DisplayAll();
            int pid = ReadInt("ID пацієнта: ");
            clinic.Doctors.DisplayAll();
            int did = ReadInt("ID лікаря: ");
            string med   = ReadString("Препарат: ");
            string dose  = ReadString("Дозування (напр. 10 мг): ");
            int days     = ReadInt("Кількість днів: ");
            string instr = ReadString("Інструкція (Enter — пропустити): ");
            clinic.MedicalRecords.Add(new Prescription(pid, did, DateTime.Today, med, dose, days, instr));
        }
        catch (Exception ex) when (ex is ArgumentException || ex is ArgumentOutOfRangeException)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    static void ShowDoctorRecords(Clinic clinic)
    {
        int did = ReadInt("ID лікаря: ");
        MedicalRecord[] records = clinic.MedicalRecords.GetByDoctor(did);
        clinic.MedicalRecords.DisplayList(records);
    }

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
            if (int.TryParse(Console.ReadLine(), out int value)) return value;
            Console.WriteLine("Некоректне число, спробуйте ще.");
        }
    }

    static double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out double value)) return value;
            Console.WriteLine("Некоректне число, спробуйте ще.");
        }
    }

    static DateTime ReadDateTime(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (DateTime.TryParse(Console.ReadLine(), out DateTime value)) return value;
            Console.WriteLine("Некоректна дата/час, спробуйте ще.");
        }
    }

    static DateTime ReadDateTimeExact(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime value)) return value;
            Console.WriteLine("Некоректна дата, спробуйте ще.");
        }
    }

    static void SeedData(Clinic clinic)
    {
        clinic.Doctors.Add(new Doctor("Олена", "Коваль", Specialty.Cardiology, "LIC-001", "0671111111", new WorkSchedule(8, 16)));
        clinic.Doctors.Add(new Doctor("Ігор", "Петренко", Specialty.Neurology, "LIC-002", "0672222222"));
        clinic.Doctors.Add(new Doctor("Марія", "Сидоренко", Specialty.Pediatrics, "LIC-003", "0673333333"));

        clinic.Patients.Add(new Patient("Іван", "Іванов", new DateTime(1985, 5, 12), BloodType.APositive, "0501111111"));
        clinic.Patients.Add(new Patient("Анна", "Петрова", new DateTime(1992, 8, 23), BloodType.ONegative, "0502222222"));

        clinic.Appointments.Book(1, 1, DateTime.Now.AddDays(1).Date.AddHours(10), 30);
        clinic.Appointments.Book(2, 2, DateTime.Now.AddDays(2).Date.AddHours(14), 45);
        clinic.Appointments.Book(1, 3, DateTime.Now.AddDays(-1).Date.AddHours(9), 20);

        Appointment[] all = clinic.Appointments.GetAll();
        if (all.Length > 0)
        {
            Appointment last = all[all.Length - 1];
            clinic.Appointments.Complete(last.Id);
        }

        clinic.MedicalRecords.Add(new Diagnosis(1, 1, DateTime.Today.AddDays(-30), "I10", "Гіпертонічна хвороба", isChronic: true));
        clinic.MedicalRecords.Add(new Diagnosis(1, 1, DateTime.Today.AddDays(-5), "J06.9", "Гострий ринофарингіт"));
        clinic.MedicalRecords.Add(new LabResult(1, 1, DateTime.Today, "Гемоглобін", 145, "г/л", "120-160", isNormal: true));
        clinic.MedicalRecords.Add(new LabResult(1, 1, DateTime.Today, "Холестерин", 6.2, "ммоль/л", "< 5.2", isNormal: false));
        clinic.MedicalRecords.Add(new Prescription(1, 1, DateTime.Today.AddDays(-5), "Лізиноприл", "10 мг", 30, "1 раз на добу вранці"));
        clinic.MedicalRecords.Add(new Prescription(2, 2, DateTime.Today.AddDays(-40), "Аспірин", "100 мг", 10, ""));
        clinic.MedicalRecords.Add(new Diagnosis(2, 2, DateTime.Today.AddDays(-10), "K29.7", "Гастрит"));
        clinic.MedicalRecords.Add(new LabResult(1, 3, DateTime.Today.AddDays(-2), "Глюкоза", 5.4, "ммоль/л", "3.9-5.5", isNormal: true));

        DateTime tomorrow = DateTime.Today.AddDays(1);
        clinic.Appointments.Book(1, 1, tomorrow.AddHours(10), 30);
        clinic.Appointments.BookUrgent(2, 2, tomorrow.AddHours(11), "біль у грудях", 30);
        clinic.Appointments.BookSpecialist(1, 3, tomorrow.AddHours(12), "кардіологія", 60);

        Console.WriteLine("\n=== new vs override ===");
        UrgentAppointment urgent = new UrgentAppointment(1, 1, tomorrow, "тест");
        
        Appointment asBase = urgent;
        Console.WriteLine($"Через Appointment:     {asBase.GetDescription()} | пріоритет {asBase.GetPriority()}");
        
        UrgentAppointment asUrgent = urgent;
        Console.WriteLine($"Через UrgentAppointment: {asUrgent.GetDescription()} | пріоритет {asUrgent.GetPriority()}");

        // 1. Чому GetDescription() через змінну типу Appointment дає опис термінового прийому?
        // Відповідь: Тому що GetDescription() оголошений як virtual у базовому класі Appointment, а в UrgentAppointment він перевизначений через override. 
        // При виклику віртуального методу через базовий тип викликається реалізація з реального об'єкта (UrgentAppointment) — це поліморфізм.

        // 2. Чому GetPriority() через ту саму змінну повертає 3, хоча об'єкт — UrgentAppointment?
        // Відповідь: Тому що GetPriority() НЕ є віртуальним (у нього немає слова virtual). 
        // У UrgentAppointment він прихований через 'new', тому компілятор обирає метод залежно від типу змінної, а не реального об'єкта.

        // 3. Що треба змінити в Appointment і UrgentAppointment, щоб GetPriority() теж працював поліморфно?
        // Відповідь: У Appointment додати слово virtual до GetPriority(). 
        // У UrgentAppointment замінити 'new' на 'override' і додати слово override до GetPriority().
    }

    static void AddDoctorFromMenu(Clinic clinic)
    {
        try
        {
            string first = ReadString("Ім'я: ");
            string last  = ReadString("Прізвище: ");
            Specialty specialty = ReadSpecialtyFromMenu();
            string lic   = ReadString("Номер ліцензії: ");
            string phone = ReadString("Телефон (10 цифр): ");
            int startHour = ReadInt("Початок роботи (0-23): ");
            int endHour   = ReadInt("Кінець роботи (1-24): ");
            clinic.Doctors.Add(new Doctor(first, last, specialty, lic, phone, new WorkSchedule(startHour, endHour)));
        }
        catch (Exception ex) when (ex is ArgumentException || ex is ArgumentOutOfRangeException)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    static Specialty ReadSpecialtyFromMenu()
    {
        string[] names = Enum.GetNames(typeof(Specialty));
        Console.WriteLine("Оберіть спеціальність:");
        for (int i = 0; i < names.Length; i++) Console.WriteLine($"  {i + 1}. {names[i]}");
        while (true)
        {
            int choice = ReadInt("Номер: ");
            int index = choice - 1;
            if (index >= 0 && index < names.Length) return (Specialty)index;
            Console.WriteLine("Некоректний номер, спробуйте ще.");
        }
    }

    static void FindDoctorsBySpecialtyFromMenu(Clinic clinic)
    {
        string query = ReadString("Введіть спеціальність (напр., Cardiology): ");
        Doctor[] found = clinic.Doctors.FindBySpecialty(query);
        if (found == null || found.Length == 0) { Console.WriteLine("Нічого не знайдено."); return; }
        foreach (Doctor d in found) Console.WriteLine(d.ToString());
    }

    static void AddPatientFromMenu(Clinic clinic)
    {
        try
        {
            string first = ReadString("Ім'я: ");
            string last  = ReadString("Прізвище: ");
            DateTime dob = ReadDateTime("Дата народження (рррр-мм-дд): ");
            BloodType blood = ReadBloodTypeFromMenu();
            string phone = ReadString("Телефон (10 цифр): ");
            clinic.Patients.Add(new Patient(first, last, dob, blood, phone));
        }
        catch (Exception ex) when (ex is ArgumentException || ex is ArgumentOutOfRangeException)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    static BloodType ReadBloodTypeFromMenu()
    {
        string[] names = Enum.GetNames(typeof(BloodType));
        Console.WriteLine("Оберіть групу крові:");
        for (int i = 0; i < names.Length; i++) Console.WriteLine($"  {i + 1}. {names[i]}");
        while (true)
        {
            int choice = ReadInt("Номер: ");
            int index = choice - 1;
            if (index >= 0 && index < names.Length) return (BloodType)index;
            Console.WriteLine("Некоректний номер, спробуйте ще.");
        }
    }

    static void CreateAppointmentFromMenu(Clinic clinic)
    {
        try
        {
            clinic.Patients.DisplayAll();
            int pid = ReadInt("ID пацієнта: ");
            clinic.Doctors.DisplayAll();
            int did = ReadInt("ID лікаря: ");
            DateTime at = ReadDateTime("Дата і час (рррр-мм-дд гг:хх): ");
            int duration = ReadInt("Тривалість (хв): ");
            clinic.Appointments.Book(pid, did, at, duration);
        }
        catch (Exception ex) when (ex is ArgumentException || ex is ArgumentOutOfRangeException)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    static void ShowAppointmentsByDate(Clinic clinic)
    {
        DateTime date = ReadDateTime("Введіть дату (рррр-мм-дд): ");
        clinic.Appointments.DisplayList(clinic.Appointments.GetByDate(date));
    }

    static void ShowUpcomingAppointments(Clinic clinic)
    {
        clinic.Appointments.DisplayList(clinic.Appointments.GetUpcoming());
    }

    static void ShowAppointmentsByPatient(Clinic clinic)
    {
        int pid = ReadInt("ID пацієнта: ");
        clinic.Appointments.DisplayList(clinic.Appointments.GetByPatient(pid));
    }

    static void ShowAppointmentsByDoctor(Clinic clinic)
    {
        int did = ReadInt("ID лікаря: ");
        clinic.Appointments.DisplayList(clinic.Appointments.GetByDoctor(did));
    }

    static void CancelAppointmentFromMenu(Clinic clinic)
    {
        int id = ReadInt("ID запису: ");
        string reason = ReadString("Причина (необов'язково): ");
        if (clinic.Appointments.Cancel(id, reason)) Console.WriteLine("Запис скасовано.");
        else Console.WriteLine("Не вдалося скасувати (можливо, запис не існує або вже не Scheduled).");
    }

    static void CompleteAppointmentFromMenu(Clinic clinic)
    {
        int id = ReadInt("ID запису: ");
        if (clinic.Appointments.Complete(id)) Console.WriteLine("Запис завершено.");
        else Console.WriteLine("Не вдалося завершити (можливо, запис не існує або вже не Scheduled).");
    }
}