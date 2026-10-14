using System;
using ClinicApp.Enums;

namespace ClinicApp.Models
{
    public class Appointment
    {
        private static int _nextId = 1;

        private int _durationMinutes;

        public int Id { get; }
        public int PatientId { get; }
        public int DoctorId { get; }
        public DateTime ScheduledAt { get; }
        public AppointmentStatus Status { get; private set; }
        public string Notes { get; private set; }

        public int DurationMinutes
        {
            get => _durationMinutes;
        }

        public DateTime EndsAt => ScheduledAt.AddMinutes(DurationMinutes);

        public bool IsUpcoming => ScheduledAt > DateTime.Now && Status == AppointmentStatus.Scheduled;

        public Appointment(int patientId, int doctorId, DateTime scheduledAt, int durationMinutes = 30)
        {
            Id = _nextId++;
            PatientId = patientId;
            DoctorId = doctorId;
            ScheduledAt = scheduledAt;
            _durationMinutes = durationMinutes;
            Status = AppointmentStatus.Scheduled;
            Notes = "";
        }

        public bool Cancel(string reason = "")
        {
            if (Status == AppointmentStatus.Scheduled)
            {
                Status = AppointmentStatus.Cancelled;
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
            if (Status == AppointmentStatus.Scheduled)
            {
                Status = AppointmentStatus.Completed;
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
}