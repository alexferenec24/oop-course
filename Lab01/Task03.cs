using System;

namespace oop_course.Lab_03
{
    public class Task03
    {
        public void Run()
        {
            Console.Write("Enter birth year: ");
            if (!int.TryParse(Console.ReadLine(), out int birthYear) || birthYear < 1900 || birthYear > 2026)
            {
                Console.WriteLine("Invalid birth year input!");
                return;
            }

            int age = 2026 - birthYear;
            string category;

            if (age >= 0 && age <= 17)
                category = "Child";
            else if (age >= 18 && age <= 59)
                category = "Adult";
            else if (age >= 60)
                category = "Pensioner";
            else
                category = "Unknown";

            Console.WriteLine($"Age: {age} years, category: {category}");
        }
    }
}