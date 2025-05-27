using System;
using System.Collections.Generic;
using System.Linq;

namespace AaDS_project.Topic_2_task_1;

public class Solution_1
{
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

        HashSet<int> result = new();
        for (int i = 0; i <= maxSum; i++)
        {
            if (dp[i])
                result.Add(i);
        }

        return result;
    }

    public static int? CountMaxCost(List<int> buyer, List<int> seller)
    {
        var buyerSums = GetPossibleSums(buyer);
        var sellerSums = GetPossibleSums(seller);

        HashSet<int> possibleP = new();

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

        if (maxAffordable == 0)
        {
            Console.WriteLine("У покупателя нет денег!");
            return null;
        }

        if (buyer.Min() <= seller.Min())
        {
            Console.WriteLine("Покупатель может точно оплатить любую стоимость до своей максимальной суммы");
            return null;
        }

        return buyer.Min() - seller.Min() - 1;
    }
}
