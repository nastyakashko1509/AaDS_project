using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AaDS_project.Topic_4_Task_9
{
    public static class GraphAnalyzer
    {
        public static (bool, int) Solution1(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            if (n == 0) return (true, 0);

            bool[] visited = new bool[n];
            int maxComponentSize = 0;

            for (int i = 0; i < n; i++)
            {
                if (!visited[i])
                {
                    var queue = new Queue<int>();
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
                        maxComponentSize = componentSize;
                }
            }

            return maxComponentSize == n ? (true, n) : (false, maxComponentSize);
        }

        public static (bool, int) Solution2(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            if (n == 0) return (true, 0);

            // Check if the graph can be introduced (has enough edges to be connected)
            double graphEdgesCount = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    graphEdgesCount += matrix[i, j];
                }
            }

            if (graphEdgesCount > (n - 1) * (n - 2) / 2)
            {
                return (true, n);
            }

            // If not, find the largest connected component
            bool[] visited = new bool[n];
            int maxComponentSize = 0;

            for (int i = 0; i < n; i++)
            {
                if (!visited[i])
                {
                    var queue = new Queue<int>();
                    queue.Enqueue(i);
                    visited[i] = true;
                    int componentSize = 1;

                    while (queue.Count > 0)
                    {
                        int v = queue.Dequeue();

                        for (int u = 0; u < n; u++)
                        {
                            if (matrix[v, u] == 1 && !visited[u])
                            {
                                visited[u] = true;
                                queue.Enqueue(u);
                                componentSize++;
                            }
                        }
                    }

                    if (componentSize > maxComponentSize)
                    {
                        maxComponentSize = componentSize;
                    }
                }
            }

            return maxComponentSize == n ? (true, n) : (false, maxComponentSize);
        }
    }
}
