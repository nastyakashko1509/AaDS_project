import numpy as np
from collections import deque

def solution_1(matrix: np.ndarray) -> tuple[bool, int]:
    n = matrix.shape[0]
    if n == 0:
        return True
    
    visited = [False] * n
    max_component_size = 0
    
    for i in range(n):
        if not visited[i]:
            # Начинаем BFS для текущей компоненты связности
            queue = deque()
            queue.append(i)
            visited[i] = True
            component_size = 0
            
            while queue:
                v = queue.popleft()
                component_size += 1
                
                for neighbor in range(n):
                    if matrix[v, neighbor] == 1 and not visited[neighbor]:
                        visited[neighbor] = True
                        queue.append(neighbor)
            
            if component_size > max_component_size:
                max_component_size = component_size
    
    # Проверяем, связаны ли все вершины (т.е. есть только одна компонента)
    if max_component_size == n:
        return True
    else:
        return (False, max_component_size)