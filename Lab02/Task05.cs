using System;

namespace Lab02
{
    public class Task5
    {
        public void Run()
        {
            Console.Write("Введіть розмір матриці: ");
            int N = int.Parse(Console.ReadLine()!);

            int[,] matrix = new int[N, N];

            for (int i = 0; i < N; i++)
            {
                Console.Write($"Введіть рядок {i + 1} ({N} чисел через пробіл): ");
                string[] input = Console.ReadLine()!.Split(' ');
                for (int j = 0; j < N; j++)
                {
                    matrix[i, j] = int.Parse(input[j]);
                }
            }

            int[] mainDiagonal = new int[N];
            int[] secondaryDiagonal = new int[N];
            int mainSum = 0;
            int secondarySum = 0;

            for (int i = 0; i < N; i++)
            {
                mainDiagonal[i] = matrix[i, i];
                mainSum += matrix[i, i];

                secondaryDiagonal[i] = matrix[i, N - 1 - i];
                secondarySum += matrix[i, N - 1 - i];
            }

            Console.WriteLine($"Головна діагональ: {string.Join(", ", mainDiagonal)}");
            Console.WriteLine($"Сума головної: {mainSum}");
            Console.WriteLine($"Побічна діагональ: {string.Join(", ", secondaryDiagonal)}");
            Console.WriteLine($"Сума побічної: {secondarySum}");
        }
    }
}