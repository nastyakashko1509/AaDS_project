

def solution_1(arr: list):
    def find_near_max(first: int, arr: list):
        for i in range(first + 1, len(arr)):
            if arr[i] > arr[first]:
                return arr[i]
        return 0 

    for i in range(len(arr) - 1):
        arr[i] = find_near_max(i, arr)

    arr[-1] = 0 
    return arr
