using System;

namespace oop_course.Lab_04
{
    public class Task04
    {
        public void Run()
        {
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

            string status;

            if (systolic < 120 && diastolic < 80)
                status = "Normal";
            else if (systolic < 130 && diastolic < 80)
                status = "Elevated";
            else if (systolic < 140 || diastolic < 90)
                status = "Stage 1 Hypertension";
            else
                status = "Stage 2 Hypertension";

            Console.WriteLine($"Blood pressure: {systolic}/{diastolic} - {status}");
        }
    }
}