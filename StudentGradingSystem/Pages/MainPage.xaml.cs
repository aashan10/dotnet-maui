namespace StudentGradingSystem.Pages;


public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }

    private async void NavigateToStudentPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("Student/" + nameof(Student.ListPage));
    }

    private async void NavigateToTeacherPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("Teacher/" + nameof(Teacher.ListPage));

    }
}


