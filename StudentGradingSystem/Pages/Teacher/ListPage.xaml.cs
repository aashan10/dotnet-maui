namespace StudentGradingSystem.Pages.Teacher;

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
        await Shell.Current.GoToAsync($"Teacher/{nameof(DetailsPage)}", true, parameters);
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

    async void AddNewStudent(System.Object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync($"Teacher/Create/{nameof(CreatePage)}", true);
    }

    async Task DeleteStudent(int StudentId)
    {
        var repository = new StudentRepository();

        await repository.Delete(StudentId);
    }
}
