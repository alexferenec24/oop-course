using System;

namespace Lab02
{
    public class Task1
    {
        public void Run()
        {
            Console.Write("Введіть кількість пацієнтів: ");
            int N = int.Parse(Console.ReadLine()!);

            double[] weights = new double[N];

            for (int i = 0; i < N; i++)
            {
                Console.Write($"Введіть вагу пацієнта {i + 1}: ");
                weights[i] = double.Parse(Console.ReadLine()!);
            }

            double sum = 0;
            double min = weights[0];
            double max = weights[0];

            foreach (double weight in weights)
            {
                sum += weight;
                if (weight < min) min = weight;
                if (weight > max) max = weight;
            }

            double average = sum / N;

            int aboveAverage = 0;
            foreach (double weight in weights)
            {
                if (weight > average) aboveAverage++;
            }

            Console.WriteLine($"Кількість: {N}");
            Console.WriteLine($"Середня вага: {average:F1} кг");
            Console.WriteLine($"Мін / Макс: {min:F1} / {max:F1} кг");
            Console.WriteLine($"Вище середнього: {aboveAverage}");
        }
    }
}