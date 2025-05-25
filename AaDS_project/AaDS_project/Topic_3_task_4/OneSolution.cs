namespace AaDS_project.Topic_3_task_4
{
    public class OneSolution
    {
        public int[] ReplaceWithNextGreater(int[] arr)
        {
            int n = arr.Length;

            for (int i = 0; i < n - 1; i++)
            {
                bool found = false;
                for (int j = i + 1; j < n; j++)
                {
                    if (arr[j] > arr[i])
                    {
                        arr[i] = arr[j];
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    arr[i] = 0;
                }
            }

            arr[n - 1] = 0; 
            return arr;
        }
    }
}
