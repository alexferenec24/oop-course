using System;
using System.Collections.Generic;
using ClinicApp.Models;
using ClinicApp.Enums;

namespace ClinicApp.Managers;

public class PatientManager
{
    private List<Patient> _patients = new List<Patient>();

    public int Count => _patients.Count;

    public void Add(Patient patient)
    {
        _patients.Add(patient);
        Console.WriteLine($"Пацієнта [{patient.Id}] {patient.FullName} додано.");
    }

    public Patient? FindById(int id)
    {
        for (int i = 0; i < _patients.Count; i++)
        {
            if (_patients[i].Id == id)
            {
                return _patients[i];
            }
        }
        return null;
    }

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
        string lowerQuery = query.ToLower();
        List<Patient> result = new List<Patient>();

        for (int i = 0; i < _patients.Count; i++)
        {
            if (_patients[i].FirstName.ToLower().Contains(lowerQuery) ||
                _patients[i].LastName.ToLower().Contains(lowerQuery))
            {
                result.Add(_patients[i]);
            }
        }

        return result.ToArray();
    }

    public Patient[] FindByBloodType(BloodType bloodType)
    {
        List<Patient> result = new List<Patient>();

        for (int i = 0; i < _patients.Count; i++)
        {
            if (_patients[i].BloodType == bloodType)
            {
                result.Add(_patients[i]);
            }
        }

        return result.ToArray();
    }

    public Patient[] GetAll()
    {
        return _patients.ToArray();
    }

    public Patient? this[int index]
    {
        get
        {
            if (index < 0 || index >= _patients.Count) return null;
            return _patients[index];
        }
    }

    public bool Remove(int id)
    {
        for (int i = 0; i < _patients.Count; i++)
        {
            if (_patients[i].Id == id)
            {
                _patients.RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    public void DisplayAll()
    {
        Console.WriteLine($"=== Пацієнти ({_patients.Count}) ===");
        if (_patients.Count == 0)
        {
            Console.WriteLine("Список порожній.");
        }
        else
        {
            for (int i = 0; i < _patients.Count; i++)
            {
                Console.WriteLine(_patients[i].ToString());
            }
        }
        Console.WriteLine(new string('-', 50));
    }

    public void DisplayStats()
    {
        Console.WriteLine("=== Статистика пацієнтів ===");
        if (_patients.Count == 0)
        {
            Console.WriteLine("Немає даних для статистики.");
            Console.WriteLine(new string('-', 50));
            return;
        }

        int totalAge = 0;
        int minAge = int.MaxValue;
        int maxAge = int.MinValue;
        Patient? youngest = null;
        Patient? oldest = null;
        int adultsCount = 0;

        for (int i = 0; i < _patients.Count; i++)
        {
            int age = _patients[i].Age;
            totalAge += age;

            if (age < minAge)
            {
                minAge = age;
                youngest = _patients[i];
            }
            if (age > maxAge)
            {
                maxAge = age;
                oldest = _patients[i];
            }
            if (_patients[i].IsAdult)
            {
                adultsCount++;
            }
        }

        double averageAge = (double)totalAge / _patients.Count;

        Console.WriteLine($"Всього:        {_patients.Count}");
        Console.WriteLine($"Середній вік:  {averageAge:F1} р.");
        Console.WriteLine($"Наймолодший:   {youngest?.FullName} ({minAge} р.)");
        Console.WriteLine($"Найстарший:    {oldest?.FullName} ({maxAge} р.)");
        Console.WriteLine($"Дорослих:      {adultsCount} з {_patients.Count}");
        Console.WriteLine(new string('-', 50));
    }
}