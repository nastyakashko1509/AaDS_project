from solution_1 import input_list, count_max_cost

n = int(input("Введите количество купюр покупателя: "))
buyer = input_list(n)

m = int(input("Введите количество купюр продавца: "))
seller = input_list(m)

result = count_max_cost(buyer, seller)
if result is None:
    print("Покупатель может точно оплатить любую стоимость до своей максимальной суммы")
else:
    print(f"Максимальная стоимость товара, которую нельзя точно оплатить: {result}")