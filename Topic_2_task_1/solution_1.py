def input_list(n: int) -> list[int]:
    money = []
    for i in range(n):
        while True:
            try:
                x = int(input(f"Введите номинал {i+1}-ой купюры: "))
                if x > 0:
                    money.append(x)
                    break
                else:
                    print("Значение должно быть > 0")
            except ValueError:
                print("Введите целое число")
    return money

def get_possible_sums(coins: list[int]) -> set[int]:
    max_sum = sum(coins)
    dp = [False] * (max_sum + 1)
    dp[0] = True
    for coin in coins:
        for j in range(max_sum, coin - 1, -1):
            if dp[j - coin]:
                dp[j] = True
    return {s for s in range(max_sum + 1) if dp[s]}

def count_max_cost(buyer: list[int], seller: list[int]) -> int | None:
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
    
    # Если все p от 1 до max_affordable возможны
    if max_affordable == 0:  # У покупателя нет денег
        return None
    # Если есть деньги, но все p возможны - значит минимальная купюра покупателя ≤ минимальной продавца
    if min(buyer) <= min(seller):
        return None
    return min(buyer) - min(seller) - 1