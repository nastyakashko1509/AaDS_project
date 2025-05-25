using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace AaDS_project.Topic_3_task_4
{
    public partial class VisualizationOfTheFirstSolution : ContentPage
    {
        private int[] originalArray;
        private int[] workingArray;

        public VisualizationOfTheFirstSolution()
        {
            InitializeComponent();
        }

        private async void OnStartVisualizationClicked(object sender, EventArgs e)
        {
            try
            {
                string input = InputArrayEntry.Text;
                originalArray = Array.ConvertAll(input.Split(','), s => int.Parse(s.Trim()));
                workingArray = (int[])originalArray.Clone();

                StepsContainer.Children.Clear();
                StatusLabel.Text = "🚀 Визуализация начата...";
                StatusFrame.BackgroundColor = Color.FromArgb("#ECF0F1");

                await Task.Delay(700);

                AddStep($"📊 Исходный массив: [{string.Join(", ", workingArray)}]", "#5DADE2", true);

                await VisualizeAlgorithmAsync();
            }
            catch
            {
                StatusLabel.Text = "⚠️ Ошибка: введите корректный массив, например: 3, 1, 7, 2";
                StatusFrame.BackgroundColor = Color.FromArgb("#FADBD8");
            }
        }

        private async Task VisualizeAlgorithmAsync()
        {
            for (int i = 0; i < workingArray.Length - 1; i++)
            {
                int currentValue = workingArray[i];
                bool found = false;
                int replacementValue = 0;

                AddStep($"🔍 Ищем первый больший элемент справа от {currentValue}", "#F7DC6F");
                await Task.Delay(800);

                for (int j = i + 1; j < originalArray.Length; j++)
                {
                    await Task.Delay(400);

                    if (originalArray[j] > currentValue)
                    {
                        replacementValue = originalArray[j];
                        found = true;

                        AddStep($"✅ Найден: {replacementValue} > {currentValue} (на позиции {j})", "#58D68D");
                        break;
                    }
                    else
                    {
                        AddStep($"❌ {originalArray[j]} не больше {currentValue}", "#F1948A");
                    }
                }

                workingArray[i] = found ? replacementValue : 0;

                string updateMessage = found
                    ? $"🔁 Заменяем {currentValue} на {replacementValue}"
                    : $"⚠️ Больше элемента {currentValue} справа нет → ставим 0";

                AddStep(updateMessage, found ? "#5DADE2" : "#D98880");
                AddStep($"📌 Массив после шага {i + 1}: [{string.Join(", ", workingArray)}]", "#AED6F1");

                await Task.Delay(1000);
            }

            workingArray[workingArray.Length - 1] = 0;
            AddStep($"🔚 Последний элемент не имеет правого соседа → заменяем на 0", "#F39C12");
            await Task.Delay(600);

            StatusLabel.Text = $"✅ Завершено! Результат: [{string.Join(", ", workingArray)}]";
            StatusFrame.BackgroundColor = Color.FromArgb("#D4EFDF");

            AddStep($"🏁 Итоговый результат: [{string.Join(", ", workingArray)}]", "#2ECC71", true);
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
    }
}
