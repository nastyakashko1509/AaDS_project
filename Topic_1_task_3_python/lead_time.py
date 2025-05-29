from solution_1 import TreeNode, solve
import time


def build_test_tree():

    root = TreeNode(10)    
    root.left = TreeNode(5)
    root.right = TreeNode(15)    
    root.left.left = TreeNode(2)
    root.left.right = TreeNode(7)
    root.right.left = TreeNode(12)
    root.right.right = TreeNode(20)    
    root.left.left.left = TreeNode(1)
    root.left.right.right = TreeNode(7)
    
    return root

def measure_performance():
    root = build_test_tree()
    print("Дерево: ", root)
    
    print("Начинаем выполнение алгоритма...")
    start = time.perf_counter()
    result = solve(root)
    end = time.perf_counter()
    
    print(f"Результат обхода: {result}")
    print(f"Время выполнения: {(end - start) * 1000:.6f} миллисекунд")

if __name__ == "__main__":
    measure_performance()