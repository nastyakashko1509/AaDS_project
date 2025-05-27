namespace AaDS_project
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void GoToTheDataStructuresPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("/DataStructuresPage");
        }

        private async void GoToTheDynamicProgrammingPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("/DynamicProgrammingPage");
        }
    }
}
