def get_possible_sums(coins: list[int]) -> set[int]:
    max_sum = sum(coins)
    dp = [False] * (max_sum + 1)
    dp[0] = True
    for coin in coins:
        for j in range(max_sum, coin - 1, -1):
            if dp[j - coin]:
                dp[j] = True
    return {s for s in range(max_sum + 1) if dp[s]}

def count_max_cost_1(buyer: list[int], seller: list[int]) -> int | None:
    buyer_sums = get_possible_sums(buyer)
    seller_sums = get_possible_sums(seller)
    
    possible_p = set()
    for b in buyer_sums:
        for s in seller_sums:
            p = b - s
            if p > 0:
                possible_p.add(p)
    
    max_affordable = max(buyer_sums)
    for p in range(max_affordable, 0, -1):
        if p not in possible_p:
            return p
    
    if max_affordable == 0:  
        print ("У покупателя нет денег!")
        return None
    
    if len(seller) == 0:
        max_not_affordable = max_affordable - 1
        min_buyer_sum = min(buyer_sums)
        print(buyer_sums)
        print(max_not_affordable)
        while max_not_affordable >= min_buyer_sum:
            if buyer_sums.__contains__(max_not_affordable):
                max_not_affordable -= 1
            else:
                return max_not_affordable
        print("Покупатель может точно оплатить любую стоимость до своей максимальной суммы")  
        return None
    else:
        if min(buyer) <= min(seller):
            print("Покупатель может точно оплатить любую стоимость до своей максимальной суммы")
            return None
        return min(buyer) - min(seller) - 1
