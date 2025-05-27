def get_possible_sums(coins: list[int]) -> set[int]:
    possible_sums = {0}
    for coin in coins:
        new_sums = set()
        for s in possible_sums:
            new_sums.add(s + coin)
        possible_sums.update(new_sums)
    return possible_sums

def count_max_cost_2(buyer: list[int], seller: list[int]) -> int | None:
    buyer_sums = get_possible_sums(buyer)
    if not buyer_sums or max(buyer_sums) == 0:
        print("У покупателя нет денег!")
        return None
    
    seller_sums = get_possible_sums(seller)
    max_affordable = max(buyer_sums)
    buyer_sums_set = buyer_sums  
    
    if not seller_sums:
        for p in range(max_affordable - 1, -1, -1):
            if p not in buyer_sums_set:
                return p
        print("Покупатель может точно оплатить любую стоимость до своей максимальной суммы")
        return None
    
    for p in range(max_affordable, 0, -1):
        found = False
        for s in seller_sums:
            if (p + s) in buyer_sums_set:
                found = True
                break
        if not found:
            return p
    
    print("Покупатель может точно оплатить любую стоимость до своей максимальной суммы")
    return None
