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

        // ����� ������� ����������
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var result = _solution.Solve(testTree);
        stopwatch.Stop();

        // ����� �����������
        ResultLabel.Text = $"��������� ������: {string.Join(", ", result)}";
        TimeLabel.Text = $"����� ����������: {stopwatch.Elapsed.TotalSeconds:F6} ������";

        // �������������� ������ ��� ��������
        RunAdditionalTests(testTree);
    }

    private void RunAdditionalTests(TreeNode testTree)
    {
        // ������� (JIT ����������)
        _solution.Solve(testTree);

        // �������� ������
        var times = new List<double>();
        for (int i = 0; i < 5; i++)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            _solution.Solve(testTree);
            sw.Stop();
            times.Add(sw.Elapsed.TotalSeconds);
        }

        // ��������� ����������
        TimeLabel.Text += $"\n������� ����� (5 ��������): {times.Average():F6} ���";
        TimeLabel.Text += $"\n����������� �����: {times.Min():F6} ���";
    }
}
