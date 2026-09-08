using System;
namespace oop_course.Lab_01;

public class Task_04
{
    public static void Run()
    {
        Console.Write("Enter systolic pressure: ");
        if (!int.TryParse(Console.ReadLine(), out int systolic))
        {
            Console.WriteLine("Invalid input! Please enter a valid number.");
            return;
        }

           
        Console.Write("Enter diastolic pressure: ");
        if (!int.TryParse(Console.ReadLine(), out int diastolic))
        {
            Console.WriteLine("Invalid input! Please enter a valid number.");
            return;
        }

            
        if (systolic < 50 || systolic > 250 || diastolic < 30 || diastolic > 200)
        {
            Console.WriteLine("Invalid pressure values! Please enter realistic values.");
            return;
        }

            
        string category;
            
        if (systolic < 120 && diastolic < 80)
        {
            category = "Normal";
        }
        else if (systolic < 130 && diastolic < 80)
        {
            category = "Elevated";
        }
        else if (systolic < 140 || diastolic < 90)
        {
            category = "Stage 1 Hypertension";
        }
        else
        {
            category = "Stage 2 Hypertension";
        }
            
        Console.WriteLine($"Systolic: {systolic}, Diastolic: {diastolic} - {category}");
    }
}