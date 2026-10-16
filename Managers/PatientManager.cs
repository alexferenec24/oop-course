using System;
using ClinicApp.Models;
using ClinicApp.Enums;

namespace ClinicApp.Managers;

public class PatientManager
{
    private const int MaxPatients = 100;
    private Patient[] _patients = new Patient[MaxPatients];
    private int _count = 0;

    public int Count => _count;

    public void Add(Patient patient)
    {
        if (_count < MaxPatients)
        {
            _patients[_count] = patient;
            _count++;
            Console.WriteLine($"Пацієнта [{patient.Id}] {patient.FullName} додано.");
        }
        else
        {
            Console.WriteLine("Досягнуто ліміт пацієнтів. Неможливо додати нового.");
        }
    }

    public Patient? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].Id == id)
            {
                return _patients[i];
            }
        }
        return null;
    }

    // Паттерн TryXxx: bool + out
    public bool TryFindById(int id, out Patient patient)
    {
        Patient? found = FindById(id);
        if (found != null)
        {
            patient = found;
            return true;
        }
        patient = null;
        return false;
    }

    public Patient[] FindByName(string query)
    {
        int matches = 0;
        string lowerQuery = query.ToLower();

        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].FirstName.ToLower().Contains(lowerQuery) ||
                _patients[i].LastName.ToLower().Contains(lowerQuery))
            {
                matches++;
            }
        }

        Patient[] result = new Patient[matches];
        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].FirstName.ToLower().Contains(lowerQuery) ||
                _patients[i].LastName.ToLower().Contains(lowerQuery))
            {
                result[index] = _patients[i];
                index++;
            }
        }

        return result;
    }

    // Пошук за точною групою крові (enum == enum)
    public Patient[] FindByBloodType(BloodType bloodType)
    {
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].BloodType == bloodType)
            {
                matches++;
            }
        }

        Patient[] result = new Patient[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].BloodType == bloodType)
            {
                result[index] = _patients[i];
                index++;
            }
        }
        return result;
    }

    public Patient[] GetAll()
    {
        Patient[] copy = new Patient[_count];
        for (int i = 0; i < _count; i++)
        {
            copy[i] = _patients[i];
        }
        return copy;
    }

    // Індексатор: clinic.Patients[0]
    public Patient? this[int index]
    {
        get
        {
            if (index < 0 || index >= _count) return null;
            return _patients[index];
        }
    }

    public bool Remove(int id)
    {
        int indexToRemove = -1;

        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].Id == id)
            {
                indexToRemove = i;
                break;
            }
        }

        if (indexToRemove == -1) return false;

        for (int i = indexToRemove; i < _count - 1; i++)
        {
            _patients[i] = _patients[i + 1];
        }

        _patients[_count - 1] = null;
        _count--;
        return true;
    }

    public void DisplayAll()
    {
        Console.WriteLine($"=== Пацієнти ({_count} / {MaxPatients}) ===");
        if (_count == 0)
        {
            Console.WriteLine("Список порожній.");
        }
        else
        {
            for (int i = 0; i < _count; i++)
            {
                Console.WriteLine(_patients[i].ToString());
            }
        }
        Console.WriteLine(new string('-', 50));
    }

    public void DisplayStats()
    {
        Console.WriteLine("=== Статистика пацієнтів ===");
        if (_count == 0)
        {
            Console.WriteLine("Немає даних для статистики.");
            Console.WriteLine(new string('-', 50));
            return;
        }

        int totalAge = 0;
        int minAge = int.MaxValue;
        int maxAge = int.MinValue;
        int minIndex = 0;
        int maxIndex = 0;
        int adultsCount = 0;

        for (int i = 0; i < _count; i++)
        {
            int age = _patients[i].Age;
            totalAge += age;

            if (age < minAge)
            {
                minAge = age;
                minIndex = i;
            }
            if (age > maxAge)
            {
                maxAge = age;
                maxIndex = i;
            }
            if (_patients[i].IsAdult)
            {
                adultsCount++;
            }
        }

        double averageAge = (double)totalAge / _count;

        Console.WriteLine($"Всього:        {_count}");
        Console.WriteLine($"Середній вік:  {averageAge:F1} р.");
        Console.WriteLine($"Наймолодший:   {_patients[minIndex].FullName} ({minAge} р.)");
        Console.WriteLine($"Найстарший:    {_patients[maxIndex].FullName} ({maxAge} р.)");
        Console.WriteLine($"Дорослих:      {adultsCount} з {_count}");
        Console.WriteLine(new string('-', 50));
    }
}