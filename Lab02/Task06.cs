using System;

namespace Lab02
{
    public class Task6
    {
        public void Run()
        {
            Console.Write("Введіть кількість лікарів: ");
            int N = int.Parse(Console.ReadLine()!);

            int[][] costs = new int[N][];

            for (int i = 0; i < N; i++)
            {
                Console.Write($"Введіть кількість прийомів лікаря {i + 1}: ");
                int K = int.Parse(Console.ReadLine()!);
                costs[i] = new int[K];

                Console.Write($"Введіть вартості прийомів лікаря {i + 1} ({K} чисел через пробіл): ");
                string[] input = Console.ReadLine()!.Split(' ');
                for (int j = 0; j < K; j++)
                {
                    costs[i][j] = int.Parse(input[j]);
                }
            }

            int maxSum = 0;
            int maxDoctor = 0;

            for (int i = 0; i < N; i++)
            {
                int sum = 0;
                for (int j = 0; j < costs[i].Length; j++)
                {
                    sum += costs[i][j];
                }

                double average = (double)sum / costs[i].Length;

                Console.WriteLine($"Лікар {i + 1}: прийомів {costs[i].Length}, сума {sum} грн, середнє {average:F2} грн");

                if (sum > maxSum)
                {
                    maxSum = sum;
                    maxDoctor = i;
                }
            }

            Console.WriteLine($"Найбільший дохід у лікаря {maxDoctor + 1}: {maxSum} грн");
        }
    }
}