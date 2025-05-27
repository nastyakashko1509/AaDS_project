using System;
using System.Collections.Generic;
using System.Linq;

namespace AaDS_project.Topic_2_task_9
{
    class Solution
    {
        public static List<int> LongestDivisibleSubsequence(List<int> A)
        {
            if (A == null || A.Count == 0)
            {
                return new List<int>();
            }

            // Создаем список с абсолютными значениями для сортировки и анализа
            List<int> absA = A.Select(x => Math.Abs(x)).ToList();
            int n = A.Count;

            // Сортируем индексы по абсолютным значениям
            List<int> sortedIndices = Enumerable.Range(0, n)
                .OrderBy(i => absA[i])
                .ToList();

            int[] dp = new int[n];
            int[] prev = new int[n];
            for (int i = 0; i < n; i++)
            {
                dp[i] = 1;
                prev[i] = -1;
            }

            for (int i = 1; i < n; i++)
            {
                int currentIdx = sortedIndices[i];
                for (int j = 0; j < i; j++)
                {
                    int prevIdx = sortedIndices[j];
                    if (absA[currentIdx] % absA[prevIdx] == 0 &&
                        dp[prevIdx] + 1 > dp[currentIdx])
                    {
                        dp[currentIdx] = dp[prevIdx] + 1;
                        prev[currentIdx] = prevIdx;
                    }
                }
            }

            int maxLen = dp.Max();
            int maxLenIndex = Array.IndexOf(dp, maxLen);

            // Восстанавливаем подпоследовательность с оригинальными значениями
            List<int> subsequence = new List<int>();
            int current = maxLenIndex;
            while (current != -1)
            {
                subsequence.Add(A[current]);
                current = prev[current];
            }

            subsequence.Reverse();
            return subsequence;
        }
    }
}