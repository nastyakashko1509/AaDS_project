using System;
using System.Collections.Generic;

class OneSolution
{
    static int[,] InputMatrix()
    {
        Console.Write("Введите количество человек: ");
        int n = int.Parse(Console.ReadLine());
        Console.WriteLine("Введите матрицу знакомств построчно (0 или 1 через пробел):");

        int[,] matrix = new int[n, n];
        for (int i = 0; i < n; i++)
        {
            string[] row = Console.ReadLine().Split(' ');
            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = int.Parse(row[j]);
            }
        }
        return matrix;
    }

    static bool CanBeIntroduced(int[,] matrix)
    {
        int sum = 0;
        int n = matrix.GetLength(0);

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                sum += matrix[i, j];
            }
        }

        double graphEdgesCount = sum / 2.0;
        double nodesCount = n;

        return graphEdgesCount > (nodesCount - 1) * (nodesCount - 2) / 2;
    }

    static object Solution1(int[,] matrix)
    {
        bool canIntroduce = CanBeIntroduced(matrix);

        if (canIntroduce)
        {
            return true;
        }
        else
        {
            int n = matrix.GetLength(0);
            bool[] visited = new bool[n];
            int maxSize = 0;

            for (int i = 0; i < n; i++)
            {
                if (!visited[i])
                {
                    Queue<int> queue = new Queue<int>();
                    queue.Enqueue(i);
                    visited[i] = true;
                    int currentSize = 1;

                    while (queue.Count > 0)
                    {
                        int v = queue.Dequeue();
                        for (int u = 0; u < n; u++)
                        {
                            if (matrix[v, u] == 1 && !visited[u])
                            {
                                visited[u] = true;
                                queue.Enqueue(u);
                                currentSize++;
                            }
                        }
                    }

                    maxSize = Math.Max(maxSize, currentSize);
                }
            }

            if (maxSize == n)
            {
                return true;
            }
            else
            {
                return new Tuple<bool, int>(false, maxSize);
            }
        }
    }

    static void Main(string[] args)
    {
        int[,] matrix = InputMatrix();
        object result = Solution1(matrix);

        if (result is bool)
        {
            Console.WriteLine(result);
        }
        else
        {
            var tupleResult = (Tuple<bool, int>)result;
            Console.WriteLine($"({tupleResult.Item1}, {tupleResult.Item2})");
        }
    }
}