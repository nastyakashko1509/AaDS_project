using AaDS_project.Topic_3_task_4;
using System.Diagnostics;

namespace AaDS_project.Topic_4_Task_9;

public partial class LeadTimeSolutions : ContentPage
{
	public LeadTimeSolutions()
	{
		InitializeComponent();
	}

    private void RunButton_1_Clicked(object sender, EventArgs e)
    {
        int size = 20;

        int[,] sparseMatrix = new int[size, size];
        for (int i = 0; i < size - 1; i++)
        {
            sparseMatrix[i, i + 1] = 1;
            sparseMatrix[i + 1, i] = 1;
        }

        int[,] denseMatrix = new int[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (i != j)
                    denseMatrix[i, j] = 1;
            }
        }

        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        var (isConnected, componentSize) = GraphAnalyzer.Solution1(sparseMatrix);
        stopwatch.Stop();
        double elapsedSeconds_sparse = stopwatch.Elapsed.TotalSeconds;

        stopwatch.Restart();
        (isConnected, componentSize) = GraphAnalyzer.Solution1(denseMatrix);
        stopwatch.Stop();
        double elapsedSeconds_dense = stopwatch.Elapsed.TotalSeconds;

        ResultLabel_1.Text = $"Время выполнения (мало рёбер): {elapsedSeconds_sparse:F6} секунд";
        ResultLabel_2.Text = $"Время выполнения (много рёбер): {elapsedSeconds_dense:F6} секунд";
    }

    private void RunButton_2_Clicked(object sender, EventArgs e)
    {
        int size = 20;

        int[,] sparseMatrix = new int[size, size];
        for (int i = 0; i < size - 1; i++)
        {
            sparseMatrix[i, i + 1] = 1;
            sparseMatrix[i + 1, i] = 1;
        }

        int[,] denseMatrix = new int[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (i != j)
                    denseMatrix[i, j] = 1;
            }
        }

        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        var (isConnected, componentSize) = GraphAnalyzer.Solution2(sparseMatrix);
        stopwatch.Stop();
        double elapsedSeconds_sparse = stopwatch.Elapsed.TotalSeconds;

        stopwatch.Restart();
        (isConnected, componentSize) = GraphAnalyzer.Solution2(denseMatrix);
        stopwatch.Stop();
        double elapsedSeconds_dense = stopwatch.Elapsed.TotalSeconds;

        ResultLabel_3.Text = $"Время выполнения (мало рёбер): {elapsedSeconds_sparse:F6} секунд";
        ResultLabel_4.Text = $"Время выполнения (много рёбер): {elapsedSeconds_dense:F6} секунд";
    }

}