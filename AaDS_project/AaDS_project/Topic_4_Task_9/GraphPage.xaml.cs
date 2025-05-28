using Microsoft.Maui.Graphics;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using System.Collections.Generic;

namespace AaDS_project.Topic_4_Task_9;

public partial class GraphPage : ContentPage
{
    private int[,] _matrix;
    private List<GraphNode> _nodes = new();
    private List<GraphEdge> _edges = new();

    public GraphPage()
    {
        InitializeComponent();
    }

    private async void OnCheckConnectivityClicked(object sender, EventArgs e)
    {
        try
        {
            if (!int.TryParse(CountEntry.Text, out int n) || n <= 0)
            {
                ShowError("Некорректное количество человек.");
                return;
            }

            var lines = MatrixEditor.Text.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length != n)
            {
                ShowError($"Ожидалось {n} строк, получено {lines.Length}.");
                return;
            }

            _matrix = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                var parts = lines[i].Trim().Split(' ');
                if (parts.Length != n)
                {
                    ShowError($"В строке {i + 1} должно быть {n} чисел.");
                    return;
                }

                for (int j = 0; j < n; j++)
                    _matrix[i, j] = int.Parse(parts[j]);
            }

            StepsContainer.Children.Clear();
            ResultLabel.Text = "🔍 Анализируем граф...";
            ResultFrame.BackgroundColor = Color.FromArgb("#F7DC6F");
            await Task.Delay(500);

            var (isConnected, maxSize) = GraphAnalyzer.Solution1(_matrix);
            _nodes.Clear();
            _edges.Clear();
            InitializeGraph();

            GraphCanvas.InvalidateSurface();

            if (isConnected)
            {
                ResultLabel.Text = "✅ Граф связен (все знакомы через кого-то)";
                ResultFrame.BackgroundColor = Color.FromArgb("#58D68D");
                AddStep("Все вершины графа связаны", "#2ECC71", true);
            }
            else
            {
                ResultLabel.Text = $"⚠️ Граф не связен. Макс. группа: {maxSize}";
                ResultFrame.BackgroundColor = Color.FromArgb("#F39C12");
                AddStep($"Обнаружено несколько компонент связности", "#F39C12");
                AddStep($"Самая большая группа: {maxSize} человек", "#F39C12");
            }

            AddStep("Анализ завершен", "#3498db", true);
        }
        catch
        {
            ShowError("Ошибка при разборе данных. Проверьте формат ввода.");
        }
    }

    private void ShowError(string message)
    {
        ResultLabel.Text = message;
        ResultFrame.BackgroundColor = Color.FromArgb("#FADBD8");
    }

    private void InitializeGraph()
    {
        int n = _matrix.GetLength(0);
        double centerX = 200;
        double centerY = 200;
        double radius = Math.Min(150, 100 + n * 10);

        // Создаем узлы
        for (int i = 0; i < n; i++)
        {
            double angle = 2 * Math.PI * i / n;
            _nodes.Add(new GraphNode
            {
                Id = i,
                Position = new Point(centerX + radius * Math.Cos(angle), centerY + radius * Math.Sin(angle)),
                Color = Colors.Blue
            });
        }

        // Создаем ребра
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (_matrix[i, j] == 1)
                {
                    _edges.Add(new GraphEdge
                    {
                        From = i,
                        To = j,
                        Color = Colors.Gray
                    });
                }
            }
        }
    }

    private void OnCanvasViewPaint(object sender, SKPaintSurfaceEventArgs e)
    {
        var surface = e.Surface;
        var canvas = surface.Canvas;
        canvas.Clear(SKColors.White);

        // Рисуем ребра
        var edgePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Gray,
            StrokeWidth = 2,
            IsAntialias = true
        };

        foreach (var edge in _edges)
        {
            var from = _nodes[edge.From].Position;
            var to = _nodes[edge.To].Position;
            canvas.DrawLine((float)from.X, (float)from.Y, (float)to.X, (float)to.Y, edgePaint);
        }

        // Рисуем узлы
        var nodePaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            IsAntialias = true
        };

        var textPaint = new SKPaint
        {
            Color = SKColors.White,
            TextSize = 16,
            TextAlign = SKTextAlign.Center,
            IsAntialias = true
        };

        foreach (var node in _nodes)
        {
            nodePaint.Color = node.Color.ToSKColor();
            canvas.DrawCircle((float)node.Position.X, (float)node.Position.Y, 20, nodePaint);

            // Обводка узла
            var borderPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Black,
                StrokeWidth = 2,
                IsAntialias = true
            };
            canvas.DrawCircle((float)node.Position.X, (float)node.Position.Y, 20, borderPaint);

            // Номер узла
            canvas.DrawText(node.Id.ToString(),
                (float)node.Position.X,
                (float)node.Position.Y + 6,
                textPaint);
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

public class GraphNode
{
    public int Id { get; set; }
    public Point Position { get; set; }
    public Color Color { get; set; }
}

public class GraphEdge
{
    public int From { get; set; }
    public int To { get; set; }
    public Color Color { get; set; }
}