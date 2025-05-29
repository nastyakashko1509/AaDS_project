from solution_1 import longest_divisible_subsequence
import time
import random

def input_sequence(n: int):
    seq = list()
    for i in range(n):
        seq = int(input("Введите эл-т последовательнсоти: "))
    return seq
    
#n = int(input("Введите количество эл-ов последовательности: "))
#sequence = input_sequence(n)
n = 10000;
sequence = list()
for i in range(n):
    sequence.append(n - i);

start_time = time.perf_counter()
result = longest_divisible_subsequence(sequence)
end_time = time.perf_counter()

print(f"Длина подпоследовательности: {len(result)}")
print(f"Время выполнения: {end_time - start_time:.6f} секунд")
#print("Наибольшая подпоследовательность:", result)
