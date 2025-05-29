from solution_1 import TreeNode, count_nodes, find_nodes_with_condition, right_delete, solve
import unittest

import unittest

class TestTreeFunctions(unittest.TestCase):
    def setUp(self):
        # Дерево для тестов
        #       10
        #      /  \
        #     5    15
        #    / \   / \
        #   2   7 12 20
        #  / \
        # 1   3
        self.root = TreeNode(10)
        self.root.left = TreeNode(5)
        self.root.right = TreeNode(15)
        self.root.left.left = TreeNode(2)
        self.root.left.right = TreeNode(7)
        self.root.right.left = TreeNode(12)
        self.root.right.right = TreeNode(20)
        self.root.left.left.left = TreeNode(1)
        self.root.left.left.right = TreeNode(3)

    def test_count_nodes(self):
        self.assertEqual(count_nodes(self.root), 9)  # Исправлено с 8 на 9
        self.assertEqual(count_nodes(self.root.left), 5)
        self.assertEqual(count_nodes(self.root.right), 3)
        self.assertEqual(count_nodes(None), 0)

    def test_find_nodes_with_condition(self):
        nodes = []
        find_nodes_with_condition(self.root, nodes)
        self.assertEqual(len(nodes), 0)  # В исходном дереве нет подходящих вершин

        # Добавляем вершину 4 как правого потомка 3 → теперь подходит 2 и 3
        self.root.left.left.right.right = TreeNode(4)
        nodes = []
        find_nodes_with_condition(self.root, nodes)
        self.assertEqual(len(nodes), 2)  # Теперь подходят 2 и 3
        self.assertEqual({node.value for node in nodes}, {2, 3})  # Проверяем значения

    def test_right_delete(self):
        new_root = right_delete(self.root, 5)
        self.assertEqual(new_root.left.value, 7)
        self.assertEqual(new_root.left.left.value, 2)
        self.assertEqual(new_root.left.right, None)

    def test_solve_no_nodes_to_delete(self):
        result = solve(self.root)
        self.assertEqual(result, [10, 5, 2, 1, 3, 7, 15, 12, 20])

    def test_solve_with_node_to_delete(self):
        self.root.left.left.right.right = TreeNode(4)
        result = solve(self.root)
        # После удаления средней по значению вершины (2 или 3)
        # В данном случае вершины [2, 3] → средняя 3 → удаляем 3
        # Дерево после удаления:
        #       10
        #      /  \
        #     5    15
        #    / \   / \
        #   2   7 12 20
        #  / \
        # 1   4
        self.assertEqual(result, [10, 5, 2, 1, 4, 7, 15, 12, 20])

    def test_empty_tree(self):
        result = solve(None)
        self.assertEqual(result, [])

    def test_single_node_tree(self):
        root = TreeNode(1)
        result = solve(root)
        self.assertEqual(result, [1])

if __name__ == '__main__':
    unittest.main()