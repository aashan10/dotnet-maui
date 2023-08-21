using System;
namespace StudentGradingSystem.Model;


public class SubjectResult
{
    private float FullMark = 50;

    public string SubjectName { get; set; }
    public float Written { get; set; }
    public float Oral { get; set; }
    public float Attendance { get; set; }
    public float Project { get; set; }

    public SubjectResult(string subjectName, float written, float oral, float attendance, float project)
    {
        SubjectName = subjectName;
        Written = written;
        Oral = oral;
        Attendance = attendance;
        Project = project;
    }

    public float FullMarks
    {
        get
        {
            return FullMark;
        }

        set
        {
            FullMark = value;
        }
    }

    public float CalculateTotal()
    {
        return Written + Oral + Attendance + Project;
    }

    public float GetWrittenPercentage
    {
        get
        {
            return (Written / FullMark) * 100;
        }
    }

    public float GetOralPercentage
    {
        get
        {
            return (Oral / FullMark) * 100;
        }
    }

    public float GetAttendancePercentage
    {
        get
        {
            return (Attendance / FullMark) * 100;
        }
    }

    public float GetProjectPercentage
    {
        get
        {
            return (Project / FullMark) * 100;
        }
    }

    public float GetTotalPercentage
    {
        get
        {
            var totalScore = CalculateTotal();
            return (totalScore / FullMark) * 100;
        }
    }

    public float GetTotalScore
    {
        get
        {
            return CalculateTotal();
        }
    }

    public float Total
    {
        get
        {
            return CalculateTotal();
        }
    }
}
