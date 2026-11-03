using System;
using ClinicApp.Enums;
using ClinicApp.Interfaces;
using ClinicApp.Utils;
// Обрано Варіант А (множник знижки в базовому класі Appointment).
// Це найпростіший підхід, який не вимагає створення нових класів для кожного тарифу.
// Він повністю відповідає принципу "відкритий для розширення, закритий для змін" (Open/Closed):
// щоб додати новий тип знижки, ми просто змінюємо значення DiscountMultiplier, не чіпаючи логіку нащадків.
namespace ClinicApp.Models
{
    public class Appointment : IPayable, ICancellable
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
        public bool IsCancelled => Status == AppointmentStatus.Cancelled;
        public string CancellationReason => IsCancelled ? Notes : "";
        public DateTime EndsAt => ScheduledAt.AddMinutes(DurationMinutes);
        public bool IsUpcoming => ScheduledAt > DateTime.Now && Status == AppointmentStatus.Scheduled;
        
        public decimal DiscountMultiplier { get; set; } = 1m;

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

        public virtual decimal GetCost()
        {
            return DurationMinutes * 10m * DiscountMultiplier;
        }

        public virtual string GetDescription()
        {
            return "Прийом";
        }

        public int GetPriority()
        {
            return 3;
        }

        public void MarkPaid()
        {
            if (!IsCancelled)
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
            return $"[{Id}] {GetDescription()} | Пацієнт #{PatientId} → Лікар #{DoctorId} | {ScheduledAt:dd.MM.yyyy HH:mm}-{EndsAt:HH:mm} | {Status} | {GetCost().ToString("F2")} грн";
        }
    }
}