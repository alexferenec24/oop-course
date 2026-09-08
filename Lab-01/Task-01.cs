using System;

namespace oop_course.Lab_01
{
    public class Task_01
    {
        public static void Run()
        {
            Console.WriteLine("=== BMI Calculator ===");
            Console.WriteLine("Enter weight (kg):");
            
            if (!double.TryParse(Console.ReadLine(), out double weight))
            {
                Console.WriteLine("Invalid weight input!");
                return;
            }

            Console.WriteLine("Enter height (m): ");
            if (!double.TryParse(Console.ReadLine(), out double height))
            {
                Console.WriteLine("Invalid height input!");
                return;
            }

            if (weight > 0 && height > 0)
            {
                double bmi = weight / (height * height);
                Console.WriteLine($"Your BMI is: {bmi:F2}");
                
                // Додаткова інформація про BMI
                if (bmi < 18.5)
                    Console.WriteLine("Category: Underweight");
                else if (bmi < 25)
                    Console.WriteLine("Category: Normal weight");
                else if (bmi < 30)
                    Console.WriteLine("Category: Overweight");
                else
                    Console.WriteLine("Category: Obese");
            }
            else
            {
                Console.WriteLine("Weight and height must be positive numbers!");
            }
        }
    }
}