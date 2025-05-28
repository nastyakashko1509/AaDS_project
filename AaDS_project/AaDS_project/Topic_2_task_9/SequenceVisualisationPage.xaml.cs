using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace AaDS_project.Topic_2_task_9
{
    public partial class SequenceVisualisationPage : ContentPage
    {
        public SequenceVisualisationPage()
        {
            InitializeComponent();
        }

        private async void OnFindSubsequenceClicked(object sender, System.EventArgs e)
        {
            try
            {
                List<int> array = ArrayEntry.Text?
                    .Split(',', System.StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => int.Parse(s.Trim()))
                    .ToList() ?? new List<int>();

                StepsContainer.Children.Clear();
                StatusLabel.Text = "🚀 Начинаем поиск подпоследовательности...";
                StatusFrame.BackgroundColor = Color.FromArgb("#ECF0F1");
                await System.Threading.Tasks.Task.Delay(700);

                AddStep($"📋 Исходный массив: [{string.Join(", ", array)}]", "#5DADE2", true);
                await System.Threading.Tasks.Task.Delay(500);

                var result = LongestDivisibleSubsequence(array);

                if (result.Count > 0)
                {
                    AddStep($"🔍 Найдена подпоследовательность длины {result.Count}: [{string.Join(", ", result)}]", "#2ECC71", true);
                    StatusLabel.Text = $"✅ Найдена подпоследовательность длины {result.Count}";
                    StatusFrame.BackgroundColor = Color.FromArgb("#D4EFDF");
                }
                else
                {
                    AddStep("ℹ️ Подходящая подпоследовательность не найдена", "#E74C3C", true);
                    StatusLabel.Text = "ℹ️ Подходящая подпоследовательность не найдена";
                    StatusFrame.BackgroundColor = Color.FromArgb("#FADBD8");
                }
            }
            catch
            {
                StatusLabel.Text = "⚠️ Ошибка: введите корректный список чисел через запятую";
                StatusFrame.BackgroundColor = Color.FromArgb("#FADBD8");
            }
        }

        private List<int> LongestDivisibleSubsequence(List<int> A)
        {
            if (A == null || A.Count == 0)
                return new List<int>();

            var absA = A.Select(x => Math.Abs(x)).ToList();
            int n = A.Count;

            // Сортируем индексы по абсолютным значениям
            var sortedIndices = Enumerable.Range(0, n)
                .OrderBy(i => absA[i])
                .ToList();

            var dp = Enumerable.Repeat(1, n).ToList();
            var prev = Enumerable.Repeat(-1, n).ToList();

            AddStep("🔢 Инициализация массивов dp и prev", "#85C1E9");
            AddStep($"dp = [{string.Join(", ", dp)}]", "#85C1E9");
            AddStep($"prev = [{string.Join(", ", prev)}]", "#85C1E9");

            for (int i = 1; i < n; i++)
            {
                int currentIdx = sortedIndices[i];
                AddStep($"\n🔍 Обрабатываем элемент A[{currentIdx}] = {A[currentIdx]}", "#F5B041");

                for (int j = 0; j < i; j++)
                {
                    int prevIdx = sortedIndices[j];
                    AddStep($"  Сравниваем с A[{prevIdx}] = {A[prevIdx]}", "#F8C471", false);

                    if (absA[currentIdx] % absA[prevIdx] == 0 && dp[prevIdx] + 1 > dp[currentIdx])
                    {
                        dp[currentIdx] = dp[prevIdx] + 1;
                        prev[currentIdx] = prevIdx;
                        AddStep($"    Обновляем: dp[{currentIdx}] = {dp[currentIdx]}, prev[{currentIdx}] = {prevIdx}", "#BB8FCE");
                    }
                }

                AddStep($"Текущее состояние dp: [{string.Join(", ", dp)}]", "#A9DFBF");
            }

            int maxLen = dp.Max();
            int maxLenIndex = dp.IndexOf(maxLen);

            AddStep($"\n📊 Максимальная длина подпоследовательности: {maxLen}", "#E74C3C");
            AddStep($"Индекс последнего элемента: {maxLenIndex}", "#E74C3C");

            var subsequence = new List<int>();
            int current = maxLenIndex;
            while (current != -1)
            {
                subsequence.Add(A[current]);
                current = prev[current];
            }
            subsequence.Reverse();

            AddStep($"🔗 Восстановленная подпоследовательность: [{string.Join(", ", subsequence)}]", "#9B59B6");

            return subsequence;
        }

        private void AddStep(string text, string colorHex, bool bold = true)
        {
            StepsContainer.Children.Add(new Frame
            {
                BackgroundColor = Color.FromArgb("#FBFCFC"),
                BorderColor = Color.FromArgb(colorHex),
                CornerRadius = 10,
                Padding = 10,
                HasShadow = true,
                Content = new Label
                {
                    Text = text,
                    FontSize = 14,
                    TextColor = Color.FromArgb("#2C3E50"),
                    FontAttributes = bold ? FontAttributes.Bold : FontAttributes.None
                }
            });
        }
    }
}