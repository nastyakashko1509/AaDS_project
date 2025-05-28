namespace AaDS_project.Topic_4_Task_9;

public partial class TaskPage : ContentPage
{
	public TaskPage()
	{
		InitializeComponent();
	}

    private async void OnFirstVisualizationClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("/GraphPage");
    }
}