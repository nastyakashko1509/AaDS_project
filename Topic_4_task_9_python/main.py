from solution_1 import solution_1
from solution_2 import input_matrix, solution_2
import time
import numpy as np

size = 20

sparse_matrix = np.zeros((size, size), dtype=int)
for i in range(size - 1):
    sparse_matrix[i, i + 1] = 1
    sparse_matrix[i + 1, i] = 1

dense_matrix = np.zeros((size, size), dtype=int)
for i in range(size):
    for j in range(size):
        if i != j:
            dense_matrix[i, j] = 1

print("Разряженная матрица:")
start_time = time.perf_counter()
print(f"Ответ (solution_1): {solution_1(sparse_matrix)}")
end_time = time.perf_counter()
print(f"Время выполнения: {end_time - start_time:.6f} секунд")

start_time = time.perf_counter()
print(f"Ответ (solution_2): {solution_2(sparse_matrix)}")
end_time = time.perf_counter()
print(f"Время выполнения: {end_time - start_time:.6f} секунд")



print("Плотная матрица:")
start_time = time.perf_counter()
print(f"Ответ (solution_1): {solution_1(sparse_matrix)}")
end_time = time.perf_counter()
print(f"Время выполнения: {end_time - start_time:.6f} секунд")

start_time = time.perf_counter()
print(f"Ответ (solution_2): {solution_2(sparse_matrix)}")
end_time = time.perf_counter()
print(f"Время выполнения: {end_time - start_time:.6f} секунд")
