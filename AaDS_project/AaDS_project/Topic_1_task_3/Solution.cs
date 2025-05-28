namespace AaDS_project.Topic_1_task_3
{
    public class TreeNode
    {
        public int Value;
        public TreeNode Left;
        public TreeNode Right;

        public TreeNode(int value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

    public class Solution
    {
        public TreeNode BuildTreeFromInput(string[] inputLines)
        {
            Dictionary<string, TreeNode> nodes = new Dictionary<string, TreeNode>();
            TreeNode root = null;

            foreach (string line in inputLines)
            {
                string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 3) continue;

                string leftVal = parts[0];
                string parentVal = parts[1];
                string rightVal = parts[2];

                if (!nodes.ContainsKey(parentVal))
                {
                    nodes[parentVal] = new TreeNode(int.Parse(parentVal));
                    if (root == null)
                        root = nodes[parentVal];
                }

                if (leftVal != "None")
                {
                    if (!nodes.ContainsKey(leftVal))
                        nodes[leftVal] = new TreeNode(int.Parse(leftVal));
                    nodes[parentVal].Left = nodes[leftVal];
                }

                if (rightVal != "None")
                {
                    if (!nodes.ContainsKey(rightVal))
                        nodes[rightVal] = new TreeNode(int.Parse(rightVal));
                    nodes[parentVal].Right = nodes[rightVal];
                }
            }

            return root;
        }

        public int CountNodes(TreeNode node)
        {
            if (node == null)
                return 0;
            return 1 + CountNodes(node.Left) + CountNodes(node.Right);
        }

        public void FindNodesWithCondition(TreeNode node, List<TreeNode> nodes)
        {
            if (node == null)
                return;

            int leftCount = CountNodes(node.Left);
            int rightCount = CountNodes(node.Right);

            if (Math.Abs(leftCount - rightCount) == 1)
                nodes.Add(node);

            FindNodesWithCondition(node.Left, nodes);
            FindNodesWithCondition(node.Right, nodes);
        }

        public TreeNode FindMin(TreeNode node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }

        public TreeNode RightDelete(TreeNode root, int value)
        {
            if (root == null)
                return null;

            if (value < root.Value)
            {
                root.Left = RightDelete(root.Left, value);
            }
            else if (value > root.Value)
            {
                root.Right = RightDelete(root.Right, value);
            }
            else
            {
                if (root.Left == null)
                    return root.Right;
                else if (root.Right == null)
                    return root.Left;

                TreeNode temp = FindMin(root.Right);
                root.Value = temp.Value;
                root.Right = RightDelete(root.Right, temp.Value);
            }
            return root;
        }

        public List<int> PreorderTraversal(TreeNode node)
        {
            List<int> result = new List<int>();
            if (node == null)
                return result;

            result.Add(node.Value);
            result.AddRange(PreorderTraversal(node.Left));
            result.AddRange(PreorderTraversal(node.Right));
            return result;
        }

        public List<int> Solve(TreeNode root)
        {
            List<TreeNode> candidates = new List<TreeNode>();
            FindNodesWithCondition(root, candidates);

            if (candidates.Count == 0)
            {
                Console.WriteLine("Нет подходящих вершин для удаления.");
                return PreorderTraversal(root);
            }

            candidates.Sort((a, b) => a.Value.CompareTo(b.Value));
            int middleIndex = candidates.Count / 2;
            int targetValue = candidates[middleIndex].Value;

            root = RightDelete(root, targetValue);
            return PreorderTraversal(root);
        }

        public TreeNode InputTree()
        {
            Console.WriteLine("Введите дерево в формате:");
            Console.WriteLine("левый_потомок корень правый_потомок");
            Console.WriteLine("Для пустого узла используйте 'None'");
            Console.WriteLine("Пример ввода: None 10 None - создает корень 10 без потомков");
            Console.WriteLine("Введите 'end' для завершения ввода");

            Dictionary<string, TreeNode> nodes = new Dictionary<string, TreeNode>();
            TreeNode root = null;

            while (true)
            {
                string line = Console.ReadLine().Trim();
                if (line.ToLower() == "end")
                    break;

                string[] parts = line.Split();
                string leftVal = parts[0];
                string parentVal = parts[1];
                string rightVal = parts[2];

                if (!nodes.ContainsKey(parentVal))
                {
                    nodes[parentVal] = new TreeNode(int.Parse(parentVal));
                    if (root == null)
                        root = nodes[parentVal];
                }

                if (leftVal != "None")
                {
                    if (!nodes.ContainsKey(leftVal))
                        nodes[leftVal] = new TreeNode(int.Parse(leftVal));
                    nodes[parentVal].Left = nodes[leftVal];
                }

                if (rightVal != "None")
                {
                    if (!nodes.ContainsKey(rightVal))
                        nodes[rightVal] = new TreeNode(int.Parse(rightVal));
                    nodes[parentVal].Right = nodes[rightVal];
                }
            }

            return root;
        }
    }
}
