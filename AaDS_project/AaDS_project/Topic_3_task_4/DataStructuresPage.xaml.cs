namespace AaDS_project.Topic_3_task_4;

public partial class DataStructuresPage : ContentPage
{
	public DataStructuresPage()
	{
		InitializeComponent();
	}

    private async void OnFirstVisualizationClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("/VisualizationOfTheFirstSolution");
    }

    private async void OnSecondVisualizationClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("/VisualizationOfTheSecondSolution");
    }

}