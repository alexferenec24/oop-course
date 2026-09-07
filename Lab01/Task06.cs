using System;

namespace oop_course.Lab_06
{
    public class Task06
    {
        public void Run()
        {
            Console.Write("Enter medical card number: ");
            if (!int.TryParse(Console.ReadLine(), out int cardNumber) || cardNumber < 10000 || cardNumber > 999999)
            {
                Console.WriteLine("Invalid card number!");
                return;
            }

            int lastDigit = cardNumber % 10;

            string department = lastDigit switch
            {
                0 or 1 => "General Therapy",
                2 or 3 => "Surgery",
                4 or 5 => "Cardiology",
                6 or 7 => "Neurology",
                8 or 9 => "Ophthalmology",
                _ => "Unknown"
            };

            string hasDiscount = cardNumber % 2 == 0 ? "Yes" : "No";
            string hasCheckup = cardNumber % 3 == 0 ? "Yes" : "No";

            Console.WriteLine($"Department: {department}");
            Console.WriteLine($"Discount card: {hasDiscount}");
            Console.WriteLine($"Routine checkup: {hasCheckup}");
        }
    }
}