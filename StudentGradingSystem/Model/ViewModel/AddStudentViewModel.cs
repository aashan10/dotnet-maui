using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using StudentGradingSystem.Repository;

namespace StudentGradingSystem.Model.ViewModel;

public partial class AddStudentViewModel : ObservableObject
{
    private string studentName;
    private int studentID;
    private ObservableCollection<SubjectMarks> subjects = new ObservableCollection<SubjectMarks>();

    public string StudentName
    {
        get => studentName;
        set => SetProperty(ref studentName, value);
    }

    public int StudentID
    {
        get => studentID;
        set => SetProperty(ref studentID, value);
    }

    public ObservableCollection<SubjectMarks> Subjects
    {
        get => subjects;
        set => SetProperty(ref subjects, value);
    }

    public ICommand AddSubjectCommand { get; }
    public ICommand AddRecordCommand { get; }

    public AddStudentViewModel()
    {
        AddSubjectCommand = new Command(AddSubject);
        AddRecordCommand = new Command(() => {
            AddRecord();
        });
    }

    private void AddSubject()
    {
        Subjects.Add(new SubjectMarks());
    }

    private async Task AddRecord()
    {
        var studentResults = new List<SubjectResult>();
        foreach (var subjectMarks in Subjects)
        {
            var subjectResult = new SubjectResult(
                subjectMarks.SubjectName,
                subjectMarks.Written,
                subjectMarks.Oral,
                subjectMarks.Attendance,
                subjectMarks.Project
            );
            studentResults.Add(subjectResult);
        }

        var student = new Student
        {
            StudentName = StudentName,
            Results = studentResults
        };
        var repository = new StudentRepository();

        // Retrieve existing student data
        var studentList = await repository.load();

        // Add the new student or update if already exists
        var existingStudent = studentList.FirstOrDefault(s => s.StudentName == StudentName);
        if (existingStudent != null)
        {
            existingStudent.Results.AddRange(studentResults);
        }
        else
        {
            studentList.Add(student);
        }

        // Save updated student data
        await repository.Save(studentList);

        // Clear input fields
        StudentName = string.Empty;
        Subjects.Clear();
    }
}

