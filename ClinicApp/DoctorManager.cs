using System;
namespace ClinicApp;

public class DoctorManager
{
    private const int MaxDoctors = 50;
    private Doctor[] _doctors = new Doctor[MaxDoctors];
    private int _count = 0;

    public int Count => _count;

    public void Add(Doctor doctor)
    {
        if (_count < MaxDoctors)
        {
            _doctors[_count] = doctor;
            _count++;
            Console.WriteLine($"Лікаря [{doctor.Id}] {doctor.FullName} додано.");
        }
        else
        {
            Console.WriteLine("Досягнуто ліміт лікарів. Неможливо додати нового.");
        }
    }

    public Doctor? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Id == id)
            {
                return _doctors[i];
            }
        }
        return null;
    }

    // Пункт 8 підказок: порівнюємо текстовий запит із текстовим поданням enum
    public Doctor[] FindBySpecialty(string specialty)
    {
        string lowerQuery = specialty.ToLower();
        int matches = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Specialty.ToString().ToLower().Contains(lowerQuery))
            {
                matches++;
            }
        }

        Doctor[] result = new Doctor[matches];
        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Specialty.ToString().ToLower().Contains(lowerQuery))
            {
                result[index] = _doctors[i];
                index++;
            }
        }

        return result;
    }

    public Doctor[] GetAll()
    {
        Doctor[] copy = new Doctor[_count];
        for (int i = 0; i < _count; i++)
        {
            copy[i] = _doctors[i];
        }
        return copy;
    }
    // Індексатор: clinic.Doctors[1]
    public Doctor? this[int index]
    {
        get
        {
            if (index < 0 || index >= _count) return null;
            return _doctors[index];
        }
    }

    public bool Remove(int id)
    {
        int indexToRemove = -1;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Id == id)
            {
                indexToRemove = i;
                break;
            }
        }

        if (indexToRemove == -1) return false;

        for (int i = indexToRemove; i < _count - 1; i++)
        {
            _doctors[i] = _doctors[i + 1];
        }

        _doctors[_count - 1] = null;
        _count--;
        return true;
    }

    public void DisplayAll()
    {
        Console.WriteLine($"=== Лікарі ({_count} / {MaxDoctors}) ===");
        if (_count == 0)
        {
            Console.WriteLine("Список порожній.");
        }
        else
        {
            for (int i = 0; i < _count; i++)
            {
                Console.WriteLine(_doctors[i].ToString());
            }
        }
        Console.WriteLine(new string('-', 50));
    }

    public void DisplayStats()
    {
        Console.WriteLine("=== Статистика лікарів ===");
        if (_count == 0)
        {
            Console.WriteLine("Немає даних для статистики.");
            Console.WriteLine(new string('-', 50));
            return;
        }

        int availableCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].IsAvailableNow)
            {
                availableCount++;
            }
        }

        Console.WriteLine($"Всього лікарів:       {_count}");
        Console.WriteLine($"Доступні зараз:       {availableCount}");
        Console.WriteLine("Унікальні спеціальності:");

        for (int i = 0; i < _count; i++)
        {
            bool isUnique = true;
            for (int j = 0; j < i; j++)
            {
                if (_doctors[i].Specialty == _doctors[j].Specialty)
                {
                    isUnique = false;
                    break;
                }
            }

            if (isUnique)
            {
                int specCount = 0;
                for (int k = 0; k < _count; k++)
                {
                    if (_doctors[k].Specialty == _doctors[i].Specialty)
                    {
                        specCount++;
                    }
                }
                Console.WriteLine($"  - {_doctors[i].Specialty} ({specCount} лікарів)");
            }
        }
        Console.WriteLine(new string('-', 50));
    }
}