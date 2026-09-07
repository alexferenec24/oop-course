using System;

namespace oop_course.Lab_05
{
    public class Task05
    {
        public void Run()
        {
            Console.Write("Enter day number (1-7): ");
            if (!int.TryParse(Console.ReadLine(), out int day))
            {
                Console.WriteLine("Invalid input!");
                return;
            }

            string info = day switch
            {
                1 => "Monday, 08:00-18:00",
                2 => "Tuesday, 08:00-18:00",
                3 => "Wednesday, 09:00-17:00",
                4 => "Thursday, 08:00-18:00",
                5 => "Friday, 08:00-16:00",
                6 => "Saturday, 09:00-14:00",
                7 => "Sunday, closed",
                _ => "Unknown day"
            };

            Console.WriteLine(info);
        }
    }
}