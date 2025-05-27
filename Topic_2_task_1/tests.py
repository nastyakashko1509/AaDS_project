from solution_1 import count_max_cost, input_list
import unittest

class TestMaxCost(unittest.TestCase):

    def test_buyer_can_pay_exact(self):
        """Покупатель может точно оплатить товар"""
        buyer = [5, 10]
        seller = [2, 3]
        # Доступные суммы покупателя: 0,5,10,15
        # Доступные суммы продавца: 0,2,3,5
        # Возможные p: 5-0=5, 10-0=10, 15-0=15, 5-2=3, 10-2=8, 15-2=13, и т.д.
        # Максимальная невозможная p в диапазоне 1-15: 14
        self.assertEqual(count_max_cost(buyer, seller), 14)

    def test_no_seller_coins(self):
        """У продавца нет купюр"""
        buyer = [3, 4]
        seller = []
        # Доступные суммы покупателя: 0,3,4,7
        # Доступные суммы продавца: 0
        # Возможные p: 3,4,7
        # Максимальная невозможная p: 6
        self.assertEqual(count_max_cost(buyer, seller), 6)

    def test_buyer_single_coin(self):
        """У покупателя одна купюра"""
        buyer = [7]
        seller = [3]
        # Доступные суммы покупателя: 0,7
        # Доступные суммы продавца: 0,3
        # Возможные p: 7-0=7, 7-3=4
        # Максимальная невозможная p: 6
        self.assertEqual(count_max_cost(buyer, seller), 6)

    def test_all_payments_possible(self):
        """Все стоимости можно оплатить"""
        buyer = [1, 2]
        seller = [1]
        # Любую p от 1 до 3 можно оплатить
        self.assertIsNone(count_max_cost(buyer, seller))

    def test_large_values(self):
        """Тест с большими значениями купюр"""
        buyer = [50, 100]
        seller = [20, 30]
        # Доступные суммы покупателя: 0,50,100,150
        # Доступные суммы продавца: 0,20,30,50
        # Возможные p: 50,100,150,30,80,130,20,70,120,0
        # Максимальная невозможная p: 149
        self.assertEqual(count_max_cost(buyer, seller), 149)

    def test_prime_numbers(self):
        """Тест с простыми числами"""
        buyer = [3, 5, 7]
        seller = [2]
        # Доступные суммы покупателя: 0,3,5,7,8,10,12,15
        # Доступные суммы продавца: 0,2
        # Максимальная невозможная p: 14
        self.assertEqual(count_max_cost(buyer, seller), 14)

    def test_no_possible_payment(self):
        """Невозможно никакой оплаты (у продавца сумма больше)"""
        buyer = [5]
        seller = [10]
        # Правильный результат: 4 (5-1, но 1 нельзя дать)
        self.assertEqual(count_max_cost(buyer, seller), 4)

    def test_input_list_function(self):
        """Тест функции ввода данных"""
        from io import StringIO
        from unittest.mock import patch
        
        # Эмулируем ввод пользователя
        with patch('builtins.input', side_effect=['5', '10', 'abc', '15']):
            with patch('sys.stdout', new_callable=StringIO) as mock_out:
                result = input_list(3)
                self.assertEqual(result, [5, 10, 15])
                self.assertIn("Введите целое число", mock_out.getvalue())


if __name__ == '__main__':
    unittest.main()