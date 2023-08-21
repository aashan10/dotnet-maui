using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace StudentGradingSystem.Model.ViewModel;

public partial class SubjectMarks : ObservableObject
{
    private string subjectName;
    private float written;
    private float oral;
    private float attendance;
    private float project;

    public string SubjectName
    {
        get => subjectName;
        set => SetProperty(ref subjectName, value);
    }

    public float Written
    {
        get => written;
        set => SetProperty(ref written, value);
    }

    public float Oral
    {
        get => oral;
        set => SetProperty(ref oral, value);
    }

    public float Attendance
    {
        get => attendance;
        set => SetProperty(ref attendance, value);
    }

    public float Project
    {
        get => project;
        set => SetProperty(ref project, value);
    }
}

