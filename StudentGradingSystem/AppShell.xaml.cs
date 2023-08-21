namespace StudentGradingSystem;

using StudentGradingSystem.Pages;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("Student/" + nameof(Pages.Student.ListPage), typeof(Pages.Student.ListPage));
        Routing.RegisterRoute("Student/" + nameof(Pages.Student.DetailsPage), typeof(Pages.Student.DetailsPage));

        Routing.RegisterRoute("Teacher/" + nameof(Pages.Teacher.ListPage), typeof(Pages.Teacher.ListPage));
        Routing.RegisterRoute("Teacher/" + nameof(Pages.Teacher.DetailsPage), typeof(Pages.Teacher.DetailsPage));
        Routing.RegisterRoute("Teacher/Create/" + nameof(Pages.Teacher.CreatePage), typeof(Pages.Teacher.CreatePage));
        //Routing.RegisterRoute("Teacher/Edit/" + nameof(Pages.Teacher.CreatePage), typeof(Pages.Teacher.CreatePage));
    }
}
