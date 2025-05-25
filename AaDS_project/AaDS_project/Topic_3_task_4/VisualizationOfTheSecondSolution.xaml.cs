using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace AaDS_project.Topic_3_task_4
{
    public partial class VisualizationOfTheSecondSolution : ContentPage
    {
        public VisualizationOfTheSecondSolution()
        {
            InitializeComponent();
        }

        private async void OnStartVisualizationClicked(object sender, EventArgs e)
        {
            try
            {
                string input = InputArrayEntry.Text;
                int[] arr = Array.ConvertAll(input.Split(','), s => int.Parse(s.Trim()));
                int[] result = new int[arr.Length];
                Stack<int> stack = new Stack<int>();

                StepsContainer.Children.Clear();
                StatusLabel.Text = "🚀 Визуализация начата...";
                StatusFrame.BackgroundColor = Color.FromArgb("#ECF0F1");

                await Task.Delay(800);

                for (int i = arr.Length - 1; i >= 0; i--)
                {
                    var spans = new List<Span>
                    {
                        new Span { Text = "🔹 Текущий элемент: ", FontAttributes = FontAttributes.Bold },
                        new Span { Text = arr[i].ToString(), TextColor = Color.FromArgb("#2980B9") }
                    };
                    AddStep(spans, "#3498DB");

                    await Task.Delay(700);

                    while (stack.Count > 0 && stack.Peek() <= arr[i])
                    {
                        string removed = stack.Peek().ToString();
                        stack.Pop();
                        AddStep($"❌ Удаляем из стека {removed} (меньше или равно {arr[i]})", "#E74C3C");
                        await Task.Delay(600);
                    }

                    result[i] = stack.Count > 0 ? stack.Peek() : 0;
                    AddStep($"📌 Результат[{i}] = {result[i]}", "#27AE60");

                    await Task.Delay(600);

                    stack.Push(arr[i]);
                    AddStep($"📥 Добавляем {arr[i]} в стек: [{string.Join(", ", stack)}]", "#8E44AD");

                    await Task.Delay(900);
                }

                StatusLabel.Text = $"✅ Завершено! Результат: [{string.Join(", ", result)}]";
                StatusFrame.BackgroundColor = Color.FromArgb("#D4EFDF");
                AddStep($"🏁 Итоговый результат: [{string.Join(", ", result)}]", "#2ECC71", true);
            }
            catch
            {
                StatusLabel.Text = "⚠️ Ошибка: введите корректный массив, например: 5, 3, 8, 1";
                StatusFrame.BackgroundColor = Color.FromArgb("#FADBD8");
            }
        }

        private void AddStep(string text, string colorHex, bool bold = false)
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

        private void AddStep(List<Span> spans, string colorHex)
        {
            var formattedLabel = new Label
            {
                FontSize = 14,
                TextColor = Color.FromArgb("#2C3E50"),
                FormattedText = new FormattedString()
            };

            foreach (var span in spans)
                formattedLabel.FormattedText.Spans.Add(span);

            StepsContainer.Children.Add(new Frame
            {
                BackgroundColor = Color.FromArgb("#FBFCFC"),
                BorderColor = Color.FromArgb(colorHex),
                CornerRadius = 10,
                Padding = 10,
                HasShadow = true,
                Content = formattedLabel
            });
        }
    }
}
