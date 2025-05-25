using System;
using System.Diagnostics;
using Microsoft.Maui.Controls;

namespace AaDS_project.Topic_3_task_4
{
    public partial class LeadTimeTwoSolution : ContentPage
    {
        public LeadTimeTwoSolution()
        {
            InitializeComponent();
        }

        private void RunButton_Clicked(object sender, EventArgs e)
        {
            int n = 10000;
            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
                arr[i] = n - i;  

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            int[] result = Solution.Solution2(arr);
            stopwatch.Stop();

            double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
            ResultLabel.Text = $"Время выполнения: {elapsedSeconds:F6} секунд";
        }
    }
}
