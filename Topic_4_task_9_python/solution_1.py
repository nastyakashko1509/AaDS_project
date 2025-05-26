import numpy as np
from collections import deque
from solution_2 import solution_2

def input_matrix():
    n = int(input("Введите количество человек: "))
    print("Введите матрицу знакомств построчно (0 или 1 через пробел):")
    matrix = []
    for _ in range(n):
        row = list(map(int, input().split()))
        matrix.append(row)
    return np.array(matrix)
    
def can_be_introduced(matrix: np.ndarray):
    graph_edges_count = matrix.sum() / 2
    n = float(matrix.shape[0])
    
    if graph_edges_count > (n-1)*(n-2) / 2:
        return True
    else:
        return False    
    
def solution_1(matrix: np.ndarray):
    can_introduce = can_be_introduced(matrix)
    
    if can_introduce:
        return True
    else:
        n = matrix.shape[0]
        visited = [False] * n
        max_size = 0

        for i in range(n):
            if not visited[i]:
                queue = deque([i])
                visited[i] = True
                current_size = 1
                
                while queue:
                    v = queue.popleft()
                    for u in range(n):
                        if matrix[v][u] == 1 and not visited[u]:
                            visited[u] = True
                            queue.append(u)
                            current_size += 1
                
                max_size = max(max_size, current_size)
        
        if max_size == n:
            return True
        else:
            return (False, max_size)