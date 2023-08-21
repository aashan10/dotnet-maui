namespace StudentGradingSystem.Pages.Teacher;

using StudentGradingSystem.Model.ViewModel;

public partial class CreatePage : ContentPage
{
	public CreatePage()
	{
		InitializeComponent();
		BindingContext = new AddStudentViewModel();
	}
}
