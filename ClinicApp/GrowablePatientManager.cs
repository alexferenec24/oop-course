using System;

public class GrowablePatientManager
{
    private Patient[] _patients = new Patient[4];
    private int _count = 0;

    public int Count => _count;
    public int Capacity => _patients.Length;

    private void Resize()
    {
        int newCapacity = _patients.Length * 2;
        Console.WriteLine($"Масив заповнений! Розширення: {_patients.Length} -> {newCapacity}");
        
        Patient[] newArray = new Patient[newCapacity];
        for (int i = 0; i < _count; i++)
        {
            newArray[i] = _patients[i];
        }
        _patients = newArray;
    }

    public void Add(Patient patient)
    {
        if (_count == _patients.Length)
        {
            Resize();
        }

        _patients[_count] = patient;
        _count++;
        
        Console.WriteLine($"  Додано [{patient.Id}]. Розмір: {_count} / {_patients.Length}");
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
        Console.WriteLine($"=== Пацієнти ({_count} / {_patients.Length}) ===");
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
        Console.WriteLine($"Поточна ємність масиву: {_patients.Length}");
        Console.WriteLine(new string('-', 50));
    }
}