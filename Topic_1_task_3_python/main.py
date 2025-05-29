from solution_1 import input_tree, preorder_traversal, solve


root = input_tree()
print("Дерево успешно введено!")
print("Исходное дерево (прямой обход):", preorder_traversal(root))

result = solve(root)
print("Результат после удаления:", result)
