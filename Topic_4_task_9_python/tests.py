from solution_1 import solution_1
from solution_2 import solution_2
import unittest
import numpy as np


class TestSolution1(unittest.TestCase):

    def test_complete_graph(self):
        # Полный граф (все знакомы со всеми)
        matrix = np.array([
            [0, 1, 1, 1],
            [1, 0, 1, 1],
            [1, 1, 0, 1],
            [1, 1, 1, 0]
        ])
        self.assertEqual(solution_1(matrix), True)
        self.assertEqual(solution_2(matrix), True)

    def test_disconnected_graph(self):
        # Граф с двумя компонентами связности
        matrix = np.array([
            [0, 1, 0, 0],
            [1, 0, 0, 0],
            [0, 0, 0, 1],
            [0, 0, 1, 0]
        ])
        self.assertEqual(solution_1(matrix), (False, 2))
        self.assertEqual(solution_2(matrix), (False, 2))

    def test_single_large_component(self):
        # Граф с одной большой компонентой связности
        matrix = np.array([
            [0, 1, 1, 0, 0],
            [1, 0, 1, 0, 0],
            [1, 1, 0, 1, 0],
            [0, 0, 1, 0, 0],
            [0, 0, 0, 0, 0]
        ])
        self.assertEqual(solution_1(matrix), (False, 4))
        self.assertEqual(solution_2(matrix), (False, 4))

    def test_empty_graph(self):
        # Граф без рёбер
        matrix = np.array([
            [0, 0, 0],
            [0, 0, 0],
            [0, 0, 0]
        ])
        self.assertEqual(solution_1(matrix), (False, 1))
        self.assertEqual(solution_2(matrix), (False, 1))

    def test_threshold_case(self):
        # Граф на грани условия can_be_introduced
        # Для 4 вершин условие: рёбер > (4-1)(4-2)/2 = 3
        # Здесь 3 ребра (не больше), поэтому False
        matrix = np.array([
            [0, 1, 1, 1],
            [1, 0, 0, 0],
            [1, 0, 0, 0],
            [1, 0, 0, 0]
        ])
        self.assertEqual(solution_1(matrix), True)
        self.assertEqual(solution_2(matrix), True)


if __name__ == '__main__':
    unittest.main()