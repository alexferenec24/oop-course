using System;
using oop_course.Lab_01;
using oop_course.Lab_02;
using oop_course.Lab_03;
using oop_course.Lab_04;
using oop_course.Lab_05;
using oop_course.Lab_06;
using oop_course.Lab_07;
using oop_course.Lab_08;

namespace oop_course
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Task 1: BMI ===");
            Task01 task1 = new Task01();
            task1.Run();

            Console.WriteLine("\n=== Task 2: Cost with Discount ===");
            Task02 task2 = new Task02();
            task2.Run();

            Console.WriteLine("\n=== Task 3: Age Category ===");
            Task03 task3 = new Task03();
            task3.Run();

            Console.WriteLine("\n=== Task 4: Blood Pressure ===");
            Task04 task4 = new Task04();
            task4.Run();

            Console.WriteLine("\n=== Task 5: Clinic Schedule ===");
            Task05 task5 = new Task05();
            task5.Run();

            Console.WriteLine("\n=== Task 6: Medical Card Analysis ===");
            Task06 task6 = new Task06();
            task6.Run();

            Console.WriteLine("\n=== Task 7: Visits Statistics ===");
            Task07 task7 = new Task07();
            task7.Run();

            Console.WriteLine("\n=== Task 8: Methods (BMI, Cost, Age, Pressure) ===");
            Task08 task8 = new Task08();
            task8.Run();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}