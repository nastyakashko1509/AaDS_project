using System.Diagnostics;

namespace AaDS_project.Topic_2_task_9
{
    public partial class LeadTimeTask9 : ContentPage
    {
        public LeadTimeTask9()
        {
            InitializeComponent();
        }

        private void RunButton_Clicked(object sender, EventArgs e)
        {
            int n = 10000;
            var arr = new List<int>();
            for (int i = 0; i < n; i++)
                arr.Add(n - i);

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            var result = Solution9.LongestDivisibleSubsequence(arr);
            stopwatch.Stop();

            double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
            ResultLabel.Text = $"⏱ Время выполнения: {elapsedSeconds:F6} секунд\n📏 Длина подпоследовательности: {result.Count}";
        }
    }
}
