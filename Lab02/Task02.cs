using System;

namespace Lab02
{
    public class Task2
    {
        public void Run()
        {
            Console.Write("Введіть кількість прийомів: ");
            int N = int.Parse(Console.ReadLine()!);

            int[] queue = new int[N];

            for (int i = 0; i < N; i++)
            {
                Console.Write($"Введіть вартість прийому {i + 1}: ");
                queue[i] = int.Parse(Console.ReadLine()!);
            }

            string before = string.Join(" ", queue);

            for (int i = 0; i < N - 1; i++)
            {
                for (int j = 0; j < N - 1 - i; j++)
                {
                    if (queue[j] > queue[j + 1])
                    {
                        (queue[j], queue[j + 1]) = (queue[j + 1], queue[j]);
                    }
                }
            }

            string after = string.Join(" ", queue);

            Console.WriteLine($"Черга (до): {before}");
            Console.WriteLine($"Черга (після): {after}");
            Console.WriteLine($"Найдешевший: {queue[0]} грн");
            Console.WriteLine($"Найдорожчий: {queue[N - 1]} грн");
        }
    }
}