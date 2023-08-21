namespace StudentGradingSystem.Model.ViewModel;

using CommunityToolkit.Mvvm.ComponentModel;
using StudentGradingSystem.Repository;

[QueryProperty(nameof(Student), "Student")]
public partial class DetailsPageViewModel: ObservableObject
{
    [ObservableProperty]
    Student student;

    public DetailsPageViewModel()
	{
	}

    
}

