using System;

namespace Lab02
{
    public class Task3
    {
        public void Run()
        {
            string[] days = { "Понеділок", "Вівторок", "Середа", "Четвер", "П'ятниця", "Субота", "Неділя" };
            int[] counts = new int[7];

            for (int i = 0; i < 7; i++)
            {
                Console.Write($"Введіть кількість пацієнтів на {days[i]}: ");
                counts[i] = int.Parse(Console.ReadLine()!);
            }

            int total = 0;
            int maxIdx = 0;
            int minIdx = 0;

            for (int i = 0; i < 7; i++)
            {
                total += counts[i];
                if (counts[i] > counts[maxIdx]) maxIdx = i;
                if (counts[i] < counts[minIdx]) minIdx = i;
            }

            Console.WriteLine();
            Console.WriteLine($"{"День",-12} {"Пацієнтів",10}");
            Console.WriteLine(new string('-', 24));

            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine($"{days[i],-12} {counts[i],10}");
            }

            Console.WriteLine(new string('-', 24));
            Console.WriteLine($"{"Разом",-12} {total,10}");
            Console.WriteLine();
            Console.WriteLine($"Найбільше: {days[maxIdx]} ({counts[maxIdx]})");
            Console.WriteLine($"Найменше: {days[minIdx]} ({counts[minIdx]})");
        }
    }
}