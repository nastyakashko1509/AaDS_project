using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Maui.Controls;

namespace AaDS_project.Topic_2_task_1;

public partial class LeadTimeSolution1 : ContentPage
{
    public LeadTimeSolution1()
    {
        InitializeComponent();
    }

    private void OnMeasureTimeClicked(object sender, EventArgs e)
    {
        List<int> buyer = new() { 1, 2, 5, 10, 20, 50 };
        List<int> seller = new() { 1, 1, 2, 2, 5 };

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        int? result = Solution_1.CountMaxCost(buyer, seller);

        stopwatch.Stop();
        double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;

        ResultLabel.Text = $"Результат: {result}\nВремя выполнения: {elapsedSeconds:F6} секунд";
    }
}
