using System;

namespace Lab02
{
    public class Task7
    {
        public void Run()
        {
            Console.Write("Введіть кількість пацієнтів: ");
            int N = int.Parse(Console.ReadLine()!);

            string[] names = new string[N];
            double[] bmis = new double[N];

            for (int i = 0; i < N; i++)
            {
                Console.Write($"Введіть ім'я пацієнта {i + 1}: ");
                names[i] = Console.ReadLine()!;
                Console.Write($"Введіть IMT пацієнта {i + 1}: ");
                bmis[i] = double.Parse(Console.ReadLine()!);
            }

            for (int i = 0; i < N - 1; i++)
            {
                for (int j = 0; j < N - 1 - i; j++)
                {
                    if (bmis[j] < bmis[j + 1])
                    {
                        (bmis[j], bmis[j + 1]) = (bmis[j + 1], bmis[j]);
                        (names[j], names[j + 1]) = (names[j + 1], names[j]);
                    }
                }
            }

            Console.WriteLine("\nРейтинг IMT:");
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine($"{i + 1}. {names[i]} — {bmis[i]:F1}");
            }
        }
    }
}