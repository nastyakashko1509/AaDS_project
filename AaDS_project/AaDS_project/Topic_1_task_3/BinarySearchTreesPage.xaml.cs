namespace AaDS_project.Topic_1_task_3;

public partial class BinarySearchTreesPage : ContentPage
{
	public BinarySearchTreesPage()
	{
		InitializeComponent();
	}

    private async void OnVisualizationClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("/VisualizationSolution");
    }
}