from solution_1 import longest_divisible_subsequence

def input_sequence(n: int):
    seq = list()
    for i in range(n):
        seq = int(input("Введите эл-т последовательнсоти: "))
    return seq
    
n = int(input("Введите количество эл-ов последовательности: "))
sequence = input_sequence(n)
result = longest_divisible_subsequence(sequence)
print("Наибольшая подпоследовательность:", result)
