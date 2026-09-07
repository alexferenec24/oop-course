using System;

namespace oop_course.Lab_01
{
    public class Task01
    {
        public void Run()
        {
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

            double bmi = weight / (height * height);
            string category;

            if (bmi < 18.5)
                category = "Underweight";
            else if (bmi < 25)
                category = "Normal";
            else if (bmi < 30)
                category = "Overweight";
            else
                category = "Obese";

            Console.WriteLine($"BMI: {bmi:F2} -> {category}");
        }
    }
}