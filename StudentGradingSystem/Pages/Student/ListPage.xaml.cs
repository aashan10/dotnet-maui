namespace StudentGradingSystem.Pages.Student;

using StudentGradingSystem.Repository;

public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
        StudentList.RefreshCommand = new Command(async () => {
            var repository = new StudentRepository();
            var data = await repository.load();
            StudentList.ItemsSource = data;
            StudentList.IsRefreshing = false;
        });
    }

    async void LoadDummyData(System.Object sender, System.EventArgs e)
    {

        var repository = new StudentRepository();
        var students = await repository.load();

        if (students.Count <= 0)
        {
            var dummy = repository.GetDummyStudents();
            await repository.Save(dummy);

        }
        StudentList.BeginRefresh();
        StudentList.RefreshCommand.Execute("abc");
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        StudentList.BeginRefresh();
    }

    private async void OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        var parameters = new Dictionary<string, object> {
            { "Student", e.Item }
        };
        await Shell.Current.GoToAsync($"Student/{nameof(DetailsPage)}", true, parameters);
    }
}
