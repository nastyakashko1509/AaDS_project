namespace AaDS_project.Topic_1_task_3;

public partial class Time : ContentPage
{
    private readonly Solution _solution = new();

    public Time()
    {
        InitializeComponent();
        InitializeTestTreeVisualization();
    }

    private void InitializeTestTreeVisualization()
    {
        TreeVisualizationLabel.Text =
            "        10\n" +
            "       /  \\\n" +
            "      5    15\n" +
            "     / \\  / \\\n" +
            "    2  7 12 20\n" +
            "   /   \\\n" +
            "  1     4";
    }

    private TreeNode CreateTestTree()
    {
        return new TreeNode(10)
        {
            Left = new TreeNode(5)
            {
                Left = new TreeNode(2)
                {
                    Left = new TreeNode(1)
                },
                Right = new TreeNode(7)
                {
                    Right = new TreeNode(4)
                }
            },
            Right = new TreeNode(15)
            {
                Left = new TreeNode(12),
                Right = new TreeNode(20)
            }
        };
    }

    private void OnRunTestClicked(object sender, EventArgs e)
    {
        var testTree = CreateTestTree();

        // Замер времени выполнения
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var result = _solution.Solve(testTree);
        stopwatch.Stop();

        // Вывод результатов
        ResultLabel.Text = $"Результат обхода: {string.Join(", ", result)}";
        TimeLabel.Text = $"Время выполнения: {stopwatch.Elapsed.TotalSeconds:F6} секунд";

        // Дополнительные замеры для точности
        RunAdditionalTests(testTree);
    }

    private void RunAdditionalTests(TreeNode testTree)
    {
        // Прогрев (JIT компиляция)
        _solution.Solve(testTree);

        // Основные замеры
        var times = new List<double>();
        for (int i = 0; i < 5; i++)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            _solution.Solve(testTree);
            sw.Stop();
            times.Add(sw.Elapsed.TotalSeconds);
        }

        // Добавляем статистику
        TimeLabel.Text += $"\nСреднее время (5 запусков): {times.Average():F6} сек";
        TimeLabel.Text += $"\nМинимальное время: {times.Min():F6} сек";
    }
}
