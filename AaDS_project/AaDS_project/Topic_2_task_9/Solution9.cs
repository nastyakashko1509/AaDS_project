using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AaDS_project.Topic_2_task_9
{
    public static class Solution9
    {
        public static List<int> LongestDivisibleSubsequence(List<int> A)
        {
            if (A == null || A.Count == 0)
                return new List<int>();

            var absA = A.Select(x => Math.Abs(x)).ToList();
            int n = A.Count;

            var sortedIndices = Enumerable.Range(0, n).OrderBy(i => absA[i]).ToList();
            var dp = Enumerable.Repeat(1, n).ToList();
            var prev = Enumerable.Repeat(-1, n).ToList();

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
            int maxLenIndex = dp.IndexOf(maxLen);

            var subsequence = new List<int>();
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

