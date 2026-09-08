using System;

namespace oop_course.Lab_01;

public class Task_03
{
    public static void Run()
    {
        Console.Write("Enter birth year: ");
        if (!int.TryParse(Console.ReadLine(), out int birthYear))
        {
            Console.WriteLine("Invalid input! Please enter a valid year.");
            return;
        }
            
        if (birthYear < 1900 || birthYear > 2026)
        {
            Console.WriteLine("Invalid birth year! Year must be between 1900 and 2026.");
            return;
        }
            
        int age = 2026 - birthYear;
            
        string category;
        if (age >= 0 && age <= 17)
        {
            category = "child";
        }
        else if (age >= 18 && age <= 59)
        {
            category = "adult";
        }
        else if (age >= 60)
        {
            category = "pensioner";
        }
        else
        {
            category = "unknown"; 
        }

            
        Console.WriteLine($"Age: {age}");
        Console.WriteLine($"Category: {category}");
    }
}