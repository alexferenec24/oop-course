using System;
using ClinicApp.Enums;
using ClinicApp.Interfaces;
using ClinicApp.Utils;

namespace ClinicApp.Models
{
    public class Appointment : IPayable
    {
        private static int _nextId = 1;
        private int _durationMinutes;
        private bool _isPaid;

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

        public bool IsPaid => _isPaid;

        public DateTime EndsAt => ScheduledAt.AddMinutes(DurationMinutes);

        public bool IsUpcoming => ScheduledAt > DateTime.Now && Status == AppointmentStatus.Scheduled;

        public Appointment(int patientId, int doctorId, DateTime scheduledAt, int durationMinutes = 30)
        {
            PatientId = patientId;
            DoctorId = doctorId;
            ScheduledAt = scheduledAt;

            ClinicValidator.ValidatePositive(durationMinutes, nameof(DurationMinutes));
            _durationMinutes = durationMinutes;

            Status = AppointmentStatus.Scheduled;
            Notes = "";
            _isPaid = false;

            Id = _nextId++;
        }

        public decimal GetCost()
        {
            return DurationMinutes * 10m;
        }

        public void MarkPaid()
        {
            if (Status != AppointmentStatus.Cancelled)
            {
                _isPaid = true;
            }
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