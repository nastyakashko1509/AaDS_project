using AaDS_project.Topic_3_task_4;
using AaDS_project.Topic_2_task_1;
using AaDS_project.Topic_4_Task_9;
using AaDS_project.Topic_1_task_3;

namespace AaDS_project
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("DataStructuresPage", typeof(DataStructuresPage));
            Routing.RegisterRoute("DynamicProgrammingPage", typeof(DynamicProgrammingPage));
            Routing.RegisterRoute("VisualizationOfTheFirstSolution", typeof(VisualizationOfTheFirstSolution));
            Routing.RegisterRoute("VisualizationOfTheSecondSolution", typeof(VisualizationOfTheSecondSolution));
            Routing.RegisterRoute("VisualizationOfTheFirstSolution1", typeof(VisualizationOfTheFirstSolution1));
            Routing.RegisterRoute("BinarySearchTreesPage", typeof(BinarySearchTreesPage));
            Routing.RegisterRoute("VisualizationSolution", typeof(VisualizationSolution));
            Routing.RegisterRoute("GraphPage", typeof(GraphPage));
            Routing.RegisterRoute("TaskPage", typeof(TaskPage));   
        }
    }
}
