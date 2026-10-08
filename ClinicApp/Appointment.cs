using System;

public class Appointment
{
    private static int _nextId = 1;

    public int Id { get; }
    public int PatientId { get; }
    public int DoctorId { get; }
    public DateTime ScheduledAt { get; }
    public int DurationMinutes { get; }
    public string Status { get; private set; }
    public string Notes { get; private set; }

    public DateTime EndsAt => ScheduledAt.AddMinutes(DurationMinutes);

    public bool IsUpcoming => ScheduledAt > DateTime.Now && Status == "Scheduled";

    public Appointment(int patientId, int doctorId, DateTime scheduledAt, int durationMinutes = 30)
    {
        Id = _nextId++;
        PatientId = patientId;
        DoctorId = doctorId;
        ScheduledAt = scheduledAt;
        DurationMinutes = durationMinutes;
        Status = "Scheduled";
        Notes = "";
    }

    public bool Cancel(string reason = "")
    {
        if (Status == "Scheduled")
        {
            Status = "Cancelled";
            if (!string.IsNullOrEmpty(reason))
            {
                Notes = reason;
            }
            return true;
        }
        return false;
    }

    public bool Complete()
    {
        if (Status == "Scheduled")
        {
            Status = "Completed";
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        string result = $"[{Id}] Пацієнт #{PatientId} -> Лікар #{DoctorId} | {ScheduledAt:dd.MM.yyyy HH:mm}-{EndsAt:HH:mm} | {Status}";
        
        if (Notes.Length > 0)
        {
            result += $" | {Notes}";
        }
        
        return result;
    }
}