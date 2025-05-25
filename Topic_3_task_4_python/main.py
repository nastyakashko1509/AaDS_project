from solution_1 import solution_1
from solution_2 import solution_2
import time


while(True):
    try:
        n = int(input("Введите размер массива: "))
        break
    except:
        print("Введите целое число!")
        
print("Инициализация массива:")
arr = list()
for i in range(n):
    el = int(input(f"Введите {i + 1}-ой(ый) элемент массива: "))
    arr.append(el)
    
print(f"Исходный массив: {arr}")

#arr = list(range(10000, 0, -1))
#start = time.time()
#solution_2(arr)
#print("Время выполнения:", time.time() - start, "сек.")

arr_1 = solution_1(arr)
arr_2 = solution_2(arr)
print(f"Ответ после 1-го решения: {arr_1}")
print(f"Ответ после 2-го решения: {arr_2}")
