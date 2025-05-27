from solution_1 import count_max_cost_1
from solution_2 import count_max_cost_2


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

n = int(input("Введите количество купюр покупателя: "))
buyer = input_list(n)

m = int(input("Введите количество купюр продавца: "))
seller = input_list(m)

result_1 = count_max_cost_1(buyer, seller)
result_2 = count_max_cost_2(buyer, seller)

if result_1 != None:
    print(f"Максимальная стоимость товара, которую нельзя точно оплатить (решение 1): {result_1}")

if result_2 != None:
    print(f"Максимальная стоимость товара, которую нельзя точно оплатить (решение 2): {result_2}")
