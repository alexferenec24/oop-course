using System;
namespace oop_course.Lab_01;

public class Task07
{
    public static void Run()
    {
        Console.Write("Enter number of visits: ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
            {
                Console.WriteLine("Invalid input! Please enter a positive number.");
                return;
            }

            decimal[] costs = new decimal[n];

            for (int i = 0; i < n; i++)
            {
                Console.Write($"Enter cost for visit #{i + 1}: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal cost) || cost < 0)
                {
                    Console.WriteLine("Invalid cost! Please enter a positive number.");
                    return;
                }
                costs[i] = cost;
            }

            decimal sum = 0;
            decimal min = decimal.MaxValue;
            decimal max = decimal.MinValue;

            foreach (decimal cost in costs)
            {
                sum += cost;
                if (cost < min) min = cost;
                if (cost > max) max = cost;
            }

            decimal average = sum / n;

            int aboveAverage = 0;
            for (int i = 0; i < n; i++)
            {
                if (costs[i] > average) aboveAverage++;
            }

            int firstExpensiveIndex = -1;
            int index = 0;
            while (index < n)
            {
                if (costs[index] > 1000)
                {
                    firstExpensiveIndex = index;
                    break;
                }
                index++;
            }

            Console.WriteLine("\nReport on visits");
            Console.WriteLine($"Count: {n}");
            Console.WriteLine($"Total: {sum:F2} UAH");
            Console.WriteLine($"Average: {average:F2} UAH");
            Console.WriteLine($"Min / Max: {min:F2} / {max:F2} UAH");
            Console.WriteLine($"Above average: {aboveAverage} of {n}");

            if (firstExpensiveIndex != -1)
            {
                Console.WriteLine($"First > 1000: #{firstExpensiveIndex + 1} - {costs[firstExpensiveIndex]:F2} UAH");
            }
            else
            {
                Console.WriteLine("First > 1000: none");
            }
    }
}