def longest_divisible_subsequence(A):
    if not A:
        return []
    
    # Создаем список с абсолютными значениями для сортировки и анализа
    abs_A = [abs(x) for x in A]
    n = len(A)
    
    # Сортируем индексы по абсолютным значениям
    sorted_indices = sorted(range(n), key=lambda i: abs_A[i])
    
    dp = [1] * n
    prev = [-1] * n
    
    for i in range(1, n):
        current_idx = sorted_indices[i]
        for j in range(i):
            prev_idx = sorted_indices[j]
            if (abs_A[current_idx] % abs_A[prev_idx] == 0 and 
                dp[prev_idx] + 1 > dp[current_idx]):
                dp[current_idx] = dp[prev_idx] + 1
                prev[current_idx] = prev_idx
    
    max_len = max(dp)
    max_len_index = dp.index(max_len)
    
    # Восстанавливаем подпоследовательность с оригинальными значениями
    subsequence = []
    current = max_len_index
    while current != -1:
        subsequence.append(A[current])
        current = prev[current]
    
    subsequence.reverse()
    return subsequence