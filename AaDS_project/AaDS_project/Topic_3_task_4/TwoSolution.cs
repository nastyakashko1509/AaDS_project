namespace AaDS_project.Topic_3_task_4
{
    public class Solution
    {
        public static int[] Solution2(int[] arr)
        {
            int n = arr.Length;
            int[] result = new int[n];
            Stack<int> stack = new Stack<int>();

            for (int i = n - 1; i >= 0; i--)
            {
                while (stack.Count > 0 && stack.Peek() <= arr[i])
                {
                    stack.Pop();
                }

                result[i] = stack.Count > 0 ? stack.Peek() : 0;

                stack.Push(arr[i]);
            }

            return result;
        }
    }
}
