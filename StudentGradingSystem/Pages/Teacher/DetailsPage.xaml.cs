using System;
namespace StudentGradingSystem.Pages.Teacher;
using StudentGradingSystem.Model.ViewModel;

public partial class DetailsPage : ContentPage
{
    public DetailsPage()
    {
        InitializeComponent();
        BindingContext = new DetailsPageViewModel();
    }
}

