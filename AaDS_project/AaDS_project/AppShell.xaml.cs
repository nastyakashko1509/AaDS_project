using AaDS_project.Topic_3_task_4;

namespace AaDS_project
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("DataStructuresPage", typeof(DataStructuresPage));
            Routing.RegisterRoute("VisualizationOfTheFirstSolution", typeof(VisualizationOfTheFirstSolution));
            Routing.RegisterRoute("VisualizationOfTheSecondSolution", typeof(VisualizationOfTheSecondSolution));
        }
    }
}
