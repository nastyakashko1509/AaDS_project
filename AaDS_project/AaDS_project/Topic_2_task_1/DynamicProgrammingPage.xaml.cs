namespace AaDS_project.Topic_2_task_1;

public partial class DynamicProgrammingPage : ContentPage
{
	public DynamicProgrammingPage()
	{
		InitializeComponent();
	}

    private async void OnFirstVisualizationClicked1(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("/VisualizationOfTheFirstSolution1");
    }

    private async void OnFirstVisualizationClicked9(object sender, EventArgs e)
    {
    }
}