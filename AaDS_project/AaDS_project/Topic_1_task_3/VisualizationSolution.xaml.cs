using Microsoft.Maui.Graphics; 

namespace AaDS_project.Topic_1_task_3;

public partial class VisualizationSolution : ContentPage
{
    private TreeNode _treeRoot;
    private List<TreeNode> _candidates = new();
    private int _currentStep = 0;
    private List<string> _solutionSteps = new();

    private Color _currentLeafColor = Colors.Green;
    private bool _isDeleting = false;

    public VisualizationSolution()
    {
        InitializeComponent();
    }

    private void OnBuildTreeClicked(object sender, EventArgs e)
    {
        try
        {
            var inputLines = TreeInput.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (inputLines.Length == 0)
            {
                DisplayAlert("Ошибка", "Введите дерево", "OK");
                return;
            }

            var solution = new Solution();
            _treeRoot = solution.BuildTreeFromInput(inputLines);

            OriginalTreeView.Drawable = new TreeDrawable(_treeRoot);
            OriginalTreeView.Invalidate();

            InfoLabel.Text = "Дерево успешно построено. Нажмите 'Визуализировать решение'";
            VisualizeSolutionButton.IsEnabled = true;

            _currentStep = 0;
            SolutionTreeView.Drawable = new TreeDrawable(_treeRoot);
            SolutionTreeView.Invalidate();
            StepDescription.Text = "Исходное дерево";
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", $"Неверный формат ввода: {ex.Message}", "OK");
        }
    }

    private void OnVisualizeSolutionClicked(object sender, EventArgs e)
    {
        if (_treeRoot == null)
        {
            DisplayAlert("Ошибка", "Сначала постройте дерево", "OK");
            return;
        }

        var solution = new Solution();
        _candidates = new List<TreeNode>();
        solution.FindNodesWithCondition(_treeRoot, _candidates);

        _solutionSteps = new List<string>();

        if (_candidates.Count == 0)
        {
            _solutionSteps.Add("Нет подходящих вершин для удаления (нет вершин, где разница количества узлов в поддеревьях равна 1).");
        }
        else
        {
            _solutionSteps.Add($"Найдено {_candidates.Count} вершин, удовлетворяющих условию (|левое поддерево| - |правое поддерево| = 1):");
            _solutionSteps.AddRange(_candidates.Select((node, i) => $"{i + 1}. Значение: {node.Value}"));

            _candidates.Sort((a, b) => a.Value.CompareTo(b.Value));
            int middleIndex = _candidates.Count / 2;
            var targetNode = _candidates[middleIndex];

            _solutionSteps.Add($"\nОтсортированный список кандидатов: {string.Join(", ", _candidates.Select(n => n.Value))}");
            _solutionSteps.Add($"Выбираем средний элемент (индекс {middleIndex}): значение {targetNode.Value}");

            // Добавляем шаги для визуализации обхода
            _solutionSteps.Add("Начинаем обход дерева для поиска листьев...");
            _solutionSteps.Add("Обход левого поддерева...");
            _solutionSteps.Add("Обход правого поддерева...");
            _solutionSteps.Add("Листья найдены, изменяем цвет...");

            _solutionSteps.Add($"\nУдаляем вершину {targetNode.Value} из дерева...");
        }

        _currentStep = 0;
        _isDeleting = false;
        UpdateVisualization();

        var timer = Application.Current.Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(1.5); // Увеличили интервал для лучшей визуализации
        timer.Tick += async (s, args) =>
        {
            if (_currentStep < _solutionSteps.Count - 1)
            {
                _currentStep++;

                // Меняем цвет листьев на шагах обхода
                if (_solutionSteps[_currentStep].Contains("Обход") ||
                    _solutionSteps[_currentStep].Contains("листьев"))
                {
                    _currentLeafColor = _currentLeafColor == Colors.Green ? Colors.Orange : Colors.Green;
                }

                // Задержка перед удалением
                if (_solutionSteps[_currentStep].Contains("Удаляем вершину") && !_isDeleting)
                {
                    _isDeleting = true;
                    await Task.Delay(1000); // Задержка перед удалением

                    var solution = new Solution();
                    var targetNode = _candidates[_candidates.Count / 2];
                    _treeRoot = solution.RightDelete(_treeRoot, targetNode.Value);

                    var traversal = solution.PreorderTraversal(_treeRoot);
                    _solutionSteps.Add($"\nОбход дерева после удаления (preorder): {string.Join(", ", traversal)}");
                }

                UpdateVisualization();
            }
            else
            {
                timer.Stop();
            }
        };
        timer.Start();
    }

    private void UpdateVisualization()
    {
        StepDescription.Text = _solutionSteps[_currentStep];

        // Обновляем Drawable с текущим цветом листьев
        SolutionTreeView.Drawable = new TreeDrawable(_treeRoot, _currentLeafColor);

        if (_currentStep == _solutionSteps.Count - 1 && _candidates.Count > 0)
        {
            ResultLabel.Text = $"Удалена вершина со значением: {_candidates[_candidates.Count / 2].Value}";

            var solution = new Solution();
            var traversal = solution.PreorderTraversal(_treeRoot);
            TraversalResult.Text = string.Join(", ", traversal);
        }

        SolutionTreeView.Invalidate();
    }
}

public class TreeDrawable : IDrawable
{
    private readonly TreeNode _root;
    private readonly Dictionary<TreeNode, (PointF Center, RectF Bounds)> _nodePositions = new();
    private float _scale = 1.0f;
    private Color _leafColor;

    public TreeDrawable(TreeNode root, Color leafColor = null)
    {
        _root = root;
        _leafColor = leafColor ?? Colors.Green;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        if (_root == null) return;

        canvas.FontColor = Colors.Black;
        canvas.FontSize = 14;

        CalculateScale(dirtyRect);
        DrawConnections(canvas, _root);
        DrawNodes(canvas);
    }

    private void DrawNodes(ICanvas canvas)
    {
        foreach (var kvp in _nodePositions)
        {
            var node = kvp.Key;
            var pos = kvp.Value.Center;
            var bounds = kvp.Value.Bounds;

            // Определяем цвет узла
            Color nodeColor;
            if (node.Left == null && node.Right == null) // Это лист
            {
                nodeColor = _leafColor;
            }
            else if (node == _root) // Это корень
            {
                nodeColor = Color.FromArgb("#512BD4");
            }
            else // Обычный узел
            {
                nodeColor = Color.FromArgb("#2B0B98");
            }

            // Рисуем круг
            canvas.FillColor = nodeColor;
            canvas.FillCircle(pos.X * _scale, pos.Y * _scale, 15 * _scale);

            // Рисуем текст
            canvas.FontColor = Colors.White;
            canvas.DrawString(node.Value.ToString(),
                new RectF(bounds.X * _scale, bounds.Y * _scale, bounds.Width * _scale, bounds.Height * _scale),
                HorizontalAlignment.Center,
                VerticalAlignment.Center);
        }
    }

    private void CalculateScale(RectF dirtyRect)
    {
        // Сначала рассчитываем позиции без масштабирования
        CalculateNodePositions(_root, 300, 30, 150);

        // Находим максимальные координаты
        float maxX = _nodePositions.Values.Max(p => p.Center.X);
        float maxY = _nodePositions.Values.Max(p => p.Center.Y);

        // Рассчитываем масштаб
        float requiredWidth = maxX + 50;
        float requiredHeight = maxY + 50;

        _scale = Math.Min(
            dirtyRect.Width / requiredWidth,
            dirtyRect.Height / requiredHeight
        );

        // Ограничиваем масштаб
        _scale = Math.Min(Math.Max(_scale, 0.5f), 2.0f);
    }

    private void CalculateNodePositions(TreeNode node, float x, float y, float xOffset)
    {
        if (node == null) return;

        _nodePositions[node] = (new PointF(x, y), new RectF(x - 15, y - 15, 30, 30));

        if (node.Left != null)
        {
            CalculateNodePositions(node.Left, x - xOffset, y + 60, xOffset / 1.8f);
        }

        if (node.Right != null)
        {
            CalculateNodePositions(node.Right, x + xOffset, y + 60, xOffset / 1.8f);
        }
    }

    private void DrawConnections(ICanvas canvas, TreeNode node)
    {
        if (node == null) return;

        if (node.Left != null && _nodePositions.TryGetValue(node.Left, out var leftPos))
        {
            var start = _nodePositions[node].Center;
            var end = leftPos.Center;
            canvas.StrokeColor = Colors.Black;
            canvas.StrokeSize = 2 * _scale;
            canvas.DrawLine(start.X * _scale, start.Y * _scale, end.X * _scale, end.Y * _scale);
            DrawConnections(canvas, node.Left);
        }

        if (node.Right != null && _nodePositions.TryGetValue(node.Right, out var rightPos))
        {
            var start = _nodePositions[node].Center;
            var end = rightPos.Center;
            canvas.StrokeColor = Colors.Black;
            canvas.StrokeSize = 2 * _scale;
            canvas.DrawLine(start.X * _scale, start.Y * _scale, end.X * _scale, end.Y * _scale);
            DrawConnections(canvas, node.Right);
        }
    }
}
