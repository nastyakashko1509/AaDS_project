using System;
using System.Collections.Generic;
using System.Linq;

namespace AaDS_project.Topic_2_task_1
{
    class Solution
    {
        public static List<int> InputList(int n)
        {
            List<int> money = new List<int>();
            for (int i = 0; i < n; i++)
            {
                while (true)
                {
                    Console.Write($"Введите номинал {i + 1}-ой купюры: ");
                    if (int.TryParse(Console.ReadLine(), out int x))
                    {
                        if (x > 0)
                        {
                            money.Add(x);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Значение должно быть > 0");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Введите целое число");
                    }
                }
            }
            return money;
        }

        public static HashSet<int> GetPossibleSums(List<int> coins)
        {
            int maxSum = coins.Sum();
            bool[] dp = new bool[maxSum + 1];
            dp[0] = true;

            foreach (int coin in coins)
            {
                for (int j = maxSum; j >= coin; j--)
                {
                    if (dp[j - coin])
                    {
                        dp[j] = true;
                    }
                }
            }

            HashSet<int> result = new HashSet<int>();
            for (int s = 0; s <= maxSum; s++)
            {
                if (dp[s])
                {
                    result.Add(s);
                }
            }
            return result;
        }

        public static int? CountMaxCost(List<int> buyer, List<int> seller)
        {
            HashSet<int> buyerSums = GetPossibleSums(buyer);
            HashSet<int> sellerSums = GetPossibleSums(seller);

            HashSet<int> possibleP = new HashSet<int>();
            foreach (int b in buyerSums)
            {
                foreach (int s in sellerSums)
                {
                    int p = b - s;
                    if (p > 0)
                    {
                        possibleP.Add(p);
                    }
                }
            }

            int maxAffordable = buyerSums.Max();
            for (int p = maxAffordable; p > 0; p--)
            {
                if (!possibleP.Contains(p))
                {
                    return p;
                }
            }

            // Если все p от 1 до maxAffordable возможны
            if (maxAffordable == 0) // У покупателя нет денег
            {
                return null;
            }
            // Если есть деньги, но все p возможны - значит минимальная купюра покупателя ≤ минимальной продавца
            if (buyer.Min() <= seller.Min())
            {
                return null;
            }
            return buyer.Min() - seller.Min() - 1;
        }
    }
}