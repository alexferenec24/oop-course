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

    private bool AddAppointment(Appointment appointment)
    {
        if (_count < MaxAppointments)
        {
            _appointments[_count] = appointment;
            _count++;
            Patient? p = _patients.FindById(appointment.PatientId);
            Doctor? d = _doctors.FindById(appointment.DoctorId);
            Console.WriteLine($"Запис [{appointment.Id}] створено: {p?.FullName} -> {d?.FullName} о {appointment.ScheduledAt:dd.MM.yyyy HH:mm}");
            return true;
        }
        Console.WriteLine("Досягнуто ліміт записів.");
        return false;
    }

    private bool ValidateIds(int patientId, int doctorId)
    {
        if (_patients.FindById(patientId) == null)
        {
            Console.WriteLine($"Помилка: пацієнта з ID {patientId} не знайдено.");
            return false;
        }
        if (_doctors.FindById(doctorId) == null)
        {
            Console.WriteLine($"Помилка: лікаря з ID {doctorId} не знайдено.");
            return false;
        }
        return true;
    }

    public bool Book(int patientId, int doctorId, DateTime scheduledAt, int durationMinutes = 30)
    {
        if (!ValidateIds(patientId, doctorId)) return false;
        return AddAppointment(new RegularAppointment(patientId, doctorId, scheduledAt, durationMinutes));
    }

    public bool BookUrgent(int patientId, int doctorId, DateTime scheduledAt, string urgencyNote = "", int durationMinutes = 30)
    {
        if (!ValidateIds(patientId, doctorId)) return false;
        return AddAppointment(new UrgentAppointment(patientId, doctorId, scheduledAt, urgencyNote, durationMinutes));
    }

    public bool BookSpecialist(int patientId, int doctorId, DateTime scheduledAt, string topic = "", int durationMinutes = 45)
    {
        if (!ValidateIds(patientId, doctorId)) return false;
        return AddAppointment(new SpecialistAppointment(patientId, doctorId, scheduledAt, topic, durationMinutes));
    }

    private Appointment? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].Id == id) return _appointments[i];
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
        if (appointment != null) return appointment.Cancel(reason);
        return false;
    }

    public bool Complete(int id)
    {
        Appointment? appointment = FindById(id);
        if (appointment != null) return appointment.Complete();
        return false;
    }

    public static int CancelAll(ICancellable[] items, string reason = "")
    {
        int cancelledCount = 0;
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].Cancel(reason)) cancelledCount++;
        }
        return cancelledCount;
    }

    public Appointment[] GetByPatient(int patientId)
    {
        int matches = 0;
        for (int i = 0; i < _count; i++) if (_appointments[i].PatientId == patientId) matches++;
        Appointment[] result = new Appointment[matches];
        int index = 0;
        for (int i = 0; i < _count; i++) if (_appointments[i].PatientId == patientId) result[index++] = _appointments[i];
        return result;
    }

    public Appointment[] GetByDoctor(int doctorId)
    {
        int matches = 0;
        for (int i = 0; i < _count; i++) if (_appointments[i].DoctorId == doctorId) matches++;
        Appointment[] result = new Appointment[matches];
        int index = 0;
        for (int i = 0; i < _count; i++) if (_appointments[i].DoctorId == doctorId) result[index++] = _appointments[i];
        return result;
    }

    public Appointment[] GetByDate(DateTime date)
    {
        int matches = 0;
        for (int i = 0; i < _count; i++) if (_appointments[i].ScheduledAt.Date == date.Date) matches++;
        Appointment[] result = new Appointment[matches];
        int index = 0;
        for (int i = 0; i < _count; i++) if (_appointments[i].ScheduledAt.Date == date.Date) result[index++] = _appointments[i];
        return result;
    }

    public Appointment[] GetByDate(int year, int month, int day) => GetByDate(new DateTime(year, month, day));

    public Appointment[] GetUpcoming()
    {
        int matches = 0;
        for (int i = 0; i < _count; i++) if (_appointments[i].IsUpcoming) matches++;
        Appointment[] result = new Appointment[matches];
        int index = 0;
        for (int i = 0; i < _count; i++) if (_appointments[i].IsUpcoming) result[index++] = _appointments[i];
        return result;
    }

    public Appointment[] GetAll()
    {
        Appointment[] copy = new Appointment[_count];
        for (int i = 0; i < _count; i++) copy[i] = _appointments[i];
        return copy;
    }

    public Appointment[] GetUrgent()
    {
        int matches = 0;
        for (int i = 0; i < _count; i++) if (_appointments[i] is UrgentAppointment) matches++;
        Appointment[] result = new Appointment[matches];
        int index = 0;
        for (int i = 0; i < _count; i++) if (_appointments[i] is UrgentAppointment) result[index++] = _appointments[i];
        return result;
    }

    public Appointment[] GetSpecialist()
    {
        int matches = 0;
        for (int i = 0; i < _count; i++) if (_appointments[i] is SpecialistAppointment) matches++;
        Appointment[] result = new Appointment[matches];
        int index = 0;
        for (int i = 0; i < _count; i++) if (_appointments[i] is SpecialistAppointment) result[index++] = _appointments[i];
        return result;
    }

    public Appointment[] GetRegular()
    {
        int matches = 0;
        for (int i = 0; i < _count; i++) if (_appointments[i] is RegularAppointment) matches++;
        Appointment[] result = new Appointment[matches];
        int index = 0;
        for (int i = 0; i < _count; i++) if (_appointments[i] is RegularAppointment) result[index++] = _appointments[i];
        return result;
    }

    public void DisplayAppointment(Appointment appointment)
    {
        Patient? patient = _patients.FindById(appointment.PatientId);
        Doctor? doctor = _doctors.FindById(appointment.DoctorId);
        string patientName = patient != null ? patient.FullName : $"Пацієнт #{appointment.PatientId}";
        string doctorName = doctor != null ? doctor.FullName : $"Лікар #{appointment.DoctorId}";
        
        Console.WriteLine($"[{appointment.Id}] {appointment.GetDescription()} | {patientName} → {doctorName} | {appointment.ScheduledAt:dd.MM.yyyy HH:mm}-{appointment.EndsAt:HH:mm} | {appointment.Status} | {appointment.GetCost().ToString("F2")} грн");
    }

    public void DisplayList(Appointment[] list)
    {
        if (list.Length == 0) { Console.WriteLine("не знайдено"); return; }
        for (int i = 0; i < list.Length; i++) DisplayAppointment(list[i]);
    }
}