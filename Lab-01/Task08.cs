using System;
namespace oop_course.Lab_01;

public class Task08
{
    public static void Run()
    {
        Console.WriteLine("=== Health Check System ===");
            
            Console.Write("Enter weight (kg): ");
            if (!double.TryParse(Console.ReadLine(), out double weight) || weight <= 0)
            {
                Console.WriteLine("Invalid weight input!");
                return;
            }

            Console.Write("Enter height (m): ");
            if (!double.TryParse(Console.ReadLine(), out double height) || height <= 0)
            {
                Console.WriteLine("Invalid height input!");
                return;
            }

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

            Console.Write("Enter birth year: ");
            if (!int.TryParse(Console.ReadLine(), out int birthYear) || birthYear < 1900 || birthYear > 2026)
            {
                Console.WriteLine("Invalid birth year input!");
                return;
            }

            Console.Write("Enter systolic pressure: ");
            if (!int.TryParse(Console.ReadLine(), out int systolic) || systolic < 50 || systolic > 250)
            {
                Console.WriteLine("Invalid systolic pressure input!");
                return;
            }

            Console.Write("Enter diastolic pressure: ");
            if (!int.TryParse(Console.ReadLine(), out int diastolic) || diastolic < 30 || diastolic > 200)
            {
                Console.WriteLine("Invalid diastolic pressure input!");
                return;
            }

            double bmi = CalculateBMI(weight, height);
            double totalCost = CalculateCost(price, visits, discount);
            int age = 2026 - birthYear;
            string ageCategory = GetAgeCategory(age);
            string pressureStatus = GetPressureStatus(systolic, diastolic);

            Console.WriteLine("\n=== Results ===");
            Console.WriteLine($"BMI: {bmi:F2} -> {GetBMICategory(bmi)}");
            Console.WriteLine($"Total cost: {totalCost:F2} UAH");
            Console.WriteLine($"Age: {age} years, category: {ageCategory}");
            Console.WriteLine($"Blood pressure: {systolic}/{diastolic} - {pressureStatus}");
        }
    
        static double CalculateBMI(double weight, double height)
        {
            return weight / (height * height);
        }

        static string GetBMICategory(double bmi)
        {
            if (bmi < 18.5)
                return "Underweight";
            else if (bmi < 25)
                return "Normal";
            else if (bmi < 30)
                return "Overweight";
            else
                return "Obese";
        }

        static double CalculateCost(double price, int visits, int discount)
        {
            return price * visits * (1 - discount / 100.0);
        }

        static string GetAgeCategory(int age)
        {
            if (age >= 0 && age <= 17)
                return "Child";
            else if (age >= 18 && age <= 59)
                return "Adult";
            else if (age >= 60)
                return "Pensioner";
            else
                return "Unknown";
        }

        static string GetPressureStatus(int systolic, int diastolic)
        {
            if (systolic < 120 && diastolic < 80)
                return "Normal";
            else if (systolic < 130 && diastolic < 80)
                return "Elevated";
            else if (systolic < 140 || diastolic < 90)
                return "Stage 1 Hypertension";
            else
                return "Stage 2 Hypertension";
        }
    }
