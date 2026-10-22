using System;
using ClinicApp.Models;
using ClinicApp.Enums;
using ClinicApp.Interfaces;

namespace ClinicApp.Managers;

public class AppointmentManager
{
    private const int MaxAppointments = 500;
    private Appointment[] _appointments = new Appointment[MaxAppointments];
    private int _count = 0;
    private PatientManager _patients;
    private DoctorManager _doctors;

    public int Count => _count;

    public AppointmentManager(PatientManager patients, DoctorManager doctors)
    {
        _patients = patients;
        _doctors = doctors;
    }

    public bool Book(int patientId, int doctorId, DateTime scheduledAt, int durationMinutes = 30)
    {
        Patient? patient = _patients.FindById(patientId);
        Doctor? doctor = _doctors.FindById(doctorId);

        if (patient == null)
        {
            Console.WriteLine($"Помилка: пацієнта з ID {patientId} не знайдено.");
            return false;
        }
        if (doctor == null)
        {
            Console.WriteLine($"Помилка: лікаря з ID {doctorId} не знайдено.");
            return false;
        }

        if (_count < MaxAppointments)
        {
            Appointment appointment = new Appointment(patientId, doctorId, scheduledAt, durationMinutes);
            _appointments[_count] = appointment;
            _count++;
            Console.WriteLine($"Запис [{appointment.Id}] створено: {patient.FullName} -> {doctor.FullName} о {scheduledAt:dd.MM.yyyy HH:mm}");
            return true;
        }
        else
        {
            Console.WriteLine("Досягнуто ліміт записів.");
            return false;
        }
    }

    private Appointment? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].Id == id)
            {
                return _appointments[i];
            }
        }
        return null;
    }

    public Appointment? this[int index]
    {
        get
        {
            if (index < 0 || index >= _count) return null;
            return _appointments[index];
        }
    }

    public bool Cancel(int id, string reason = "")
    {
        Appointment? appointment = FindById(id);
        if (appointment != null)
        {
            return appointment.Cancel(reason);
        }
        return false;
    }

    public bool Complete(int id)
    {
        Appointment? appointment = FindById(id);
        if (appointment != null)
        {
            return appointment.Complete();
        }
        return false;
    }

    public static int CancelAll(ICancellable[] items, string reason = "")
    {
        int cancelledCount = 0;
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].Cancel(reason))
            {
                cancelledCount++;
            }
        }
        return cancelledCount;
    }

    public Appointment[] GetByPatient(int patientId)
    {
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].PatientId == patientId) matches++;
        }

        Appointment[] result = new Appointment[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].PatientId == patientId)
            {
                result[index] = _appointments[i];
                index++;
            }
        }
        return result;
    }

    public Appointment[] GetByDoctor(int doctorId)
    {
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].DoctorId == doctorId) matches++;
        }

        Appointment[] result = new Appointment[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].DoctorId == doctorId)
            {
                result[index] = _appointments[i];
                index++;
            }
        }
        return result;
    }

    public Appointment[] GetByDate(DateTime date)
    {
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].ScheduledAt.Date == date.Date) matches++;
        }

        Appointment[] result = new Appointment[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].ScheduledAt.Date == date.Date)
            {
                result[index] = _appointments[i];
                index++;
            }
        }
        return result;
    }

    public Appointment[] GetByDate(int year, int month, int day)
    {
        return GetByDate(new DateTime(year, month, day));
    }

    public Appointment[] GetUpcoming()
    {
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].IsUpcoming) matches++;
        }

        Appointment[] result = new Appointment[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].IsUpcoming)
            {
                result[index] = _appointments[i];
                index++;
            }
        }
        return result;
    }

    public Appointment[] GetAll()
    {
        Appointment[] copy = new Appointment[_count];
        for (int i = 0; i < _count; i++)
        {
            copy[i] = _appointments[i];
        }
        return copy;
    }

    public void DisplayAppointment(Appointment appointment)
    {
        Patient? patient = _patients.FindById(appointment.PatientId);
        Doctor? doctor = _doctors.FindById(appointment.DoctorId);

        string patientName = patient != null ? patient.FullName : $"Пацієнт #{appointment.PatientId}";
        string doctorName = doctor != null ? doctor.FullName : $"Лікар #{appointment.DoctorId}";

        string notesPart = appointment.Notes.Length > 0 ? $" | {appointment.Notes}" : "";

        Console.WriteLine($"[{appointment.Id}] {patientName} -> {doctorName} | {appointment.ScheduledAt:dd.MM.yyyy HH:mm}-{appointment.EndsAt:HH:mm} | {appointment.Status}{notesPart}");
    }

    public void DisplayList(Appointment[] list)
    {
        if (list.Length == 0)
        {
            Console.WriteLine("не знайдено");
            return;
        }

        for (int i = 0; i < list.Length; i++)
        {
            DisplayAppointment(list[i]);
        }
    }
}