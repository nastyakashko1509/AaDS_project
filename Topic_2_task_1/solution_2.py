def get_possible_sums(coins: list[int]) -> set[int]:
    max_sum = sum(coins)
    dp = [False] * (max_sum + 1)
    dp[0] = True
    for coin in coins:
        for j in range(max_sum, coin - 1, -1):
            if dp[j - coin]:
                dp[j] = True
    return {s for s in range(max_sum + 1) if dp[s]}

def count_max_cost_2(buyer: list[int], seller: list[int]) -> int | None:
    buyer_sums = get_possible_sums(buyer)
    seller_sums = get_possible_sums(seller)

    max_affordable = max(buyer_sums)
    buyer_sums_set = set(buyer_sums)

    for p in range(max_affordable, 0, -1):
        can_pay_exact = any((p + s) in buyer_sums_set for s in seller_sums)
        if not can_pay_exact:
            return p

    return None

