using System;

namespace Lab02
{
    public class Task8
    {
        public void Run()
        {
            Console.Write("Введіть кількість відділень: ");
            int D = int.Parse(Console.ReadLine()!);

            Console.Write("Введіть кількість тижнів: ");
            int W = int.Parse(Console.ReadLine()!);

            int[,,] hospital = new int[D, W, 2];

            for (int d = 0; d < D; d++)
            {
                for (int w = 0; w < W; w++)
                {
                    Console.Write($"Відділення {d + 1}, тиждень {w + 1}, ранкова зміна: ");
                    hospital[d, w, 0] = int.Parse(Console.ReadLine()!);
                    Console.Write($"Відділення {d + 1}, тиждень {w + 1}, вечірня зміна: ");
                    hospital[d, w, 1] = int.Parse(Console.ReadLine()!);
                }
            }

            int[] deptTotals = new int[D];
            int maxTotal = 0;
            int maxDept = 0;

            Console.WriteLine();
            for (int d = 0; d < D; d++)
            {
                int total = 0;
                Console.WriteLine($"Відділення {d + 1}:");

                for (int w = 0; w < W; w++)
                {
                    int weeklyTotal = hospital[d, w, 0] + hospital[d, w, 1];
                    total += weeklyTotal;
                    Console.WriteLine($"  Тиждень {w + 1}: ранок {hospital[d, w, 0]}, вечір {hospital[d, w, 1]}, разом {weeklyTotal}");
                }

                deptTotals[d] = total;
                Console.WriteLine($"  Загалом: {total} пацієнтів");
                Console.WriteLine();

                if (total > maxTotal)
                {
                    maxTotal = total;
                    maxDept = d;
                }
            }

            Console.WriteLine($"Найзавантаженіше відділення: {maxDept + 1} ({maxTotal} пацієнтів)");
        }
    }
}