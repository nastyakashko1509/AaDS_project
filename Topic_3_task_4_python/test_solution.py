from solution_1 import solution_1
from solution_2 import solution_2
import unittest


class TestSolution1(unittest.TestCase):

    def test_regular_case(self):
        arr = [5, 3, 8, 1]
        self.assertEqual(solution_1(arr.copy()), [8, 8, 0, 0])
        self.assertEqual(solution_2(arr.copy()), [8, 8, 0, 0])

    def test_descending_order(self):
        arr = [9, 7, 5, 3]
        self.assertEqual(solution_1(arr.copy()), [0, 0, 0, 0])
        self.assertEqual(solution_2(arr.copy()), [0, 0, 0, 0])

    def test_all_equal_elements(self):
        arr = [4, 4, 4, 4]
        self.assertEqual(solution_1(arr.copy()), [0, 0, 0, 0])
        self.assertEqual(solution_2(arr.copy()), [0, 0, 0, 0])

if __name__ == '__main__':
    unittest.main()
