using System;

namespace Lab02
{
    public class Task4
    {
        public void Run()
        {
            Console.Write("Введіть кількість лікарів: ");
            int N = int.Parse(Console.ReadLine()!);

            Console.Write("Введіть кількість днів: ");
            int M = int.Parse(Console.ReadLine()!);

            int[,] matrix = new int[N, M];

            for (int i = 0; i < N; i++)
            {
                Console.Write($"Введіть прийоми лікаря {i + 1} ({M} чисел через пробіл): ");
                string[] input = Console.ReadLine()!.Split(' ');
                for (int j = 0; j < M; j++)
                {
                    matrix[i, j] = int.Parse(input[j]);
                }
            }

            int maxVal = matrix[0, 0];
            int maxRow = 0;
            int maxCol = 0;

            for (int i = 0; i < N; i++)
            {
                int rowSum = 0;
                for (int j = 0; j < M; j++)
                {
                    rowSum += matrix[i, j];
                    if (matrix[i, j] > maxVal)
                    {
                        maxVal = matrix[i, j];
                        maxRow = i;
                        maxCol = j;
                    }
                }
                Console.WriteLine($"Лікар {i + 1}: {rowSum} прийомів");
            }

            Console.Write("По днях: ");
            for (int j = 0; j < M; j++)
            {
                int colSum = 0;
                for (int i = 0; i < N; i++)
                {
                    colSum += matrix[i, j];
                }
                if (j > 0) Console.Write(", ");
                Console.Write(colSum);
            }
            Console.WriteLine();

            Console.WriteLine($"Максимум: {maxVal} (Лікар {maxRow + 1}, День {maxCol + 1})");
        }
    }
}