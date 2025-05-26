from solution_1 import input_tree, TreeNode, preorder_traversal, solve_task

root = input_tree()
print("Дерево успешно введено!")
print("Исходное дерево (прямой обход):", preorder_traversal(root))

result = solve_task(root)
print("Результат после удаления:", result)