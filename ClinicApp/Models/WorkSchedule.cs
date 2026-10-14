using System;
using ClinicApp.Enums;
using ClinicApp.Utils;

namespace ClinicApp.Models
{
    public struct WorkSchedule
    {
        // Пункт 2 підказок: Start і End — get-only, присвоюються лише в конструкторі
        public int Start { get; }
        public int End { get; }

        // Обчислювані властивості
        public int HoursPerDay => End - Start;

        // Формат D2 — двозначне число з провідним нулем: "08:00–17:00"
        public string Display => $"{Start:D2}:00–{End:D2}:00";

        // Пункт 4 підказок: IsNow — просто Contains для поточної години
        public bool IsNow => Contains(DateTime.Now.Hour);

        public WorkSchedule(int start, int end)
        {
            Start = start;
            End = end;
        }

        // Пункт 4: перевірка діапазону [Start, End)
        public bool Contains(int hour)
        {
            return hour >= Start && hour < End;
        }

        // Специфікація: Display + " (" + HoursPerDay + " год)"
        public override string ToString()
        {
            return $"{Display} ({HoursPerDay} год)";
        }
    }
}