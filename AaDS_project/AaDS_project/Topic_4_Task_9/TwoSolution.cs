using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AaDS_project.Topic_4_Task_9
{
    class TwoSolution
    {
        static Tuple<bool, int> Solution2(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            if (n == 0)
            {
                return Tuple.Create(true, 0);
            }

            bool[] visited = new bool[n];
            int maxComponentSize = 0;

            for (int i = 0; i < n; i++)
            {
                if (!visited[i])
                {
                    // Начинаем BFS для текущей компоненты связности
                    Queue<int> queue = new Queue<int>();
                    queue.Enqueue(i);
                    visited[i] = true;
                    int componentSize = 0;

                    while (queue.Count > 0)
                    {
                        int v = queue.Dequeue();
                        componentSize++;

                        for (int neighbor = 0; neighbor < n; neighbor++)
                        {
                            if (matrix[v, neighbor] == 1 && !visited[neighbor])
                            {
                                visited[neighbor] = true;
                                queue.Enqueue(neighbor);
                            }
                        }
                    }

                    if (componentSize > maxComponentSize)
                    {
                        maxComponentSize = componentSize;
                    }
                }
            }

            // Проверяем, связаны ли все вершины (т.е. есть только одна компонента)
            if (maxComponentSize == n)
            {
                return Tuple.Create(true, maxComponentSize);
            }
            else
            {
                return Tuple.Create(false, maxComponentSize);
            }
        }

        static void Main(string[] args)
        {
            // Пример использования
            int[,] testMatrix = new int[,]
            {
            {0, 1, 0, 0},
            {1, 0, 1, 0},
            {0, 1, 0, 0},
            {0, 0, 0, 0}
            };

            var result = Solution2(testMatrix);
            Console.WriteLine($"Result: ({result.Item1}, {result.Item2})");
        }
    }
}