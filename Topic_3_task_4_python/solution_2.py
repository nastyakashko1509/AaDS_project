

def solution_2(arr):
    n = len(arr)
    result = [0] * n
    stack = []
    
    for i in range(n - 1, -1, -1):
        while stack and stack[-1] <= arr[i]:
            stack.pop()
        
        if stack:
            result[i] = stack[-1]
        else:
            result[i] = 0
        
        stack.append(arr[i])
    
    return result
