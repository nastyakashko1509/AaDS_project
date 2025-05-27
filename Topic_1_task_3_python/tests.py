from solution_1 import TreeNode, solve_task, input_tree, preorder_traversal
import unittest
from unittest.mock import patch
from io import StringIO

class TestTreeOperations(unittest.TestCase):

    def test_no_candidates(self):
        """Тест, когда нет подходящих вершин для удаления"""
        root = TreeNode(10)
        root.left = TreeNode(5)
        root.right = TreeNode(15)
        result = solve_task(root)
        self.assertEqual(result, [10, 5, 15])

    def test_single_candidate(self):
        """Тест с одной подходящей вершиной"""
        root = TreeNode(10)
        root.left = TreeNode(5)
        root.left.right = TreeNode(7)
        root.right = TreeNode(15)
        result = solve_task(root)
        # Должна удалиться вершина 5, останется [10, 15, 7]
        self.assertEqual(result, [10, 15, 7])

    def test_input_tree(self):
        """Тест функции ввода дерева"""
        input_data = [
            "10 5 15",
            "5 2 7",
            "15 None 20",
            "20 18 None",
            ""  # Пустая строка для завершения
        ]
        
        expected_structure = [10, 5, 2, 7, 15, 20, 18]
        
        with patch('builtins.input', side_effect=input_data):
            root = input_tree()
            result = preorder_traversal(root)
            self.assertEqual(result, expected_structure)

    def test_complex_case(self):
        """Тест сложного случая с несколькими уровнями"""
        root = TreeNode(50)
        root.left = TreeNode(30)
        root.left.left = TreeNode(20)
        root.left.left.left = TreeNode(10)
        root.left.right = TreeNode(40)
        root.right = TreeNode(70)
        root.right.left = TreeNode(60)
        root.right.left.right = TreeNode(65)
        root.right.right = TreeNode(80)
        
        result = solve_task(root)
        # Подходящие вершины: 20 (1:0), 60 (0:1), 30 (3:1), 70 (2:1)
        # Сортировка: 20, 30, 60, 70 → средняя 30
        # После удаления 30 должно быть [50, 40, 20, 10, 70, 60, 65, 80]
        self.assertEqual(result, [50, 40, 20, 10, 70, 60, 65, 80])


if __name__ == '__main__':
    unittest.main()