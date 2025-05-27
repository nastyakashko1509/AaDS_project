using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace AaDS_project.Topic_2_task_1;

public partial class VisualizationOfTheFirstSolution1 : ContentPage
{
    public VisualizationOfTheFirstSolution1()
    {
        InitializeComponent();
    }

    private async void OnStartVisualizationClicked(object sender, EventArgs e)
    {
        try
        {
            List<int> buyer = BuyerEntry.Text?
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s.Trim()))
                .ToList() ?? new();

            List<int> seller = SellerEntry.Text?
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s.Trim()))
                .ToList() ?? new();

            StepsContainer.Children.Clear();
            StatusLabel.Text = "🚀 Визуализация начата...";
            StatusFrame.BackgroundColor = Color.FromArgb("#ECF0F1");
            await Task.Delay(700);

            AddStep($"💰 Покупатель: [{string.Join(", ", buyer)}]", "#5DADE2", true);
            AddStep($"🏪 Продавец: [{string.Join(", ", seller)}]", "#F5B041", true);
            await Task.Delay(700);

            var buyerSums = Solution_1.GetPossibleSums(buyer);
            var sellerSums = Solution_1.GetPossibleSums(seller);

            AddStep($"📊 Все возможные суммы покупателя: [{string.Join(", ", buyerSums.OrderBy(x => x))}]", "#85C1E9");
            AddStep($"📊 Все возможные суммы продавца: [{string.Join(", ", sellerSums.OrderBy(x => x))}]", "#F8C471");
            await Task.Delay(1000);

            HashSet<int> possibleP = new();
            foreach (int b in buyerSums)
            {
                foreach (int s in sellerSums)
                {
                    int p = b - s;
                    if (p > 0)
                        possibleP.Add(p);
                }
            }

            AddStep($"🧠 Все возможные стоимости товаров, которые можно оплатить: [{string.Join(", ", possibleP.OrderBy(x => x))}]", "#BB8FCE");

            int maxAffordable = buyerSums.Max();
            for (int p = 1; p <= maxAffordable; p++)
            {
                if (!possibleP.Contains(p))
                {
                    AddStep($"🛑 Покупатель не может оплатить стоимость {p}", "#E74C3C", true);
                    StatusLabel.Text = $"❗ Максимальная стоимость, которую нельзя оплатить: {p}";
                    StatusFrame.BackgroundColor = Color.FromArgb("#FADBD8");
                    return;
                }
            }

            AddStep("✅ Все стоимости до максимальной суммы покупателя могут быть оплачены", "#2ECC71", true);
            StatusLabel.Text = "✅ Все стоимости возможны для оплаты";
            StatusFrame.BackgroundColor = Color.FromArgb("#D4EFDF");
        }
        catch
        {
            StatusLabel.Text = "⚠️ Ошибка: введите корректные списки чисел через запятую";
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
}
