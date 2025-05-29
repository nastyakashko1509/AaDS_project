from solution_1 import longest_divisible_subsequence
import unittest

class TestLongestDivisibleSubsequence(unittest.TestCase):

    def test_empty_array(self):
        """Тест пустого массива"""
        self.assertEqual(longest_divisible_subsequence([]), [])

    def test_single_element(self):
        """Тест массива из одного элемента"""
        self.assertEqual(longest_divisible_subsequence([5]), [5])

    def test_all_equal_elements(self):
        """Тест массива с одинаковыми элементами"""
        self.assertEqual(longest_divisible_subsequence([2, 2, 2, 2]), [2, 2, 2, 2])

    def test_powers_of_two(self):
        """Тест степеней двойки"""
        self.assertEqual(longest_divisible_subsequence([1, 2, 4, 8, 16]), [1, 2, 4, 8, 16])

    def test_standard_case(self):
        """Стандартный тестовый случай"""
        self.assertEqual(longest_divisible_subsequence([1, 2, 4, 8, 3, 9, 27, 81]), [1, 3, 9, 27, 81])

    def test_no_valid_subsequence(self):
        """Тест без валидной подпоследовательности"""
        result = longest_divisible_subsequence([5, 7, 11, 13])
        self.assertTrue(len(result) == 1)  # Должен вернуть любой один элемент
        self.assertIn(result[0], [5, 7, 11, 13])

    def test_negative_numbers(self):
        """Тест с отрицательными числами"""
        self.assertEqual(longest_divisible_subsequence([-2, -4, -8, -16]), [-2, -4, -8, -16])

    def test_large_input(self):
        """Стресс-тест на большом массиве"""
        large_input = [1] + [2 ** i for i in range(1, 100)]
        result = longest_divisible_subsequence(large_input)
        self.assertEqual(len(result), 100)
        self.assertEqual(result, large_input)

    def test_mixed_numbers(self):
        """Тест со смешанными числами"""
        self.assertEqual(longest_divisible_subsequence([3, 6, 2, 4, 12, 24]), [2, 4, 12, 24])

    def test_prime_numbers(self):
        """Тест с простыми числами"""
        result = longest_divisible_subsequence([2, 3, 5, 7, 11])
        self.assertTrue(len(result) == 1)
        self.assertIn(result[0], [2, 3, 5, 7, 11])


if __name__ == '__main__':
    unittest.main()