using System;

namespace oop_course.Lab_02
{
    public class Task02
    {
        public void Run()
        {
            Console.Write("Enter price per visit: ");
            if (!double.TryParse(Console.ReadLine(), out double price) || price <= 0)
            {
                Console.WriteLine("Invalid price input!");
                return;
            }

            Console.Write("Enter number of visits: ");
            if (!int.TryParse(Console.ReadLine(), out int visits) || visits <= 0)
            {
                Console.WriteLine("Invalid visits input!");
                return;
            }

            Console.Write("Enter discount (%): ");
            if (!int.TryParse(Console.ReadLine(), out int discount) || discount < 0 || discount > 100)
            {
                Console.WriteLine("Invalid discount input!");
                return;
            }

            double total = price * visits * (1 - discount / 100.0);
            Console.WriteLine($"Total cost: {total:F2} UAH");
        }
    }
}