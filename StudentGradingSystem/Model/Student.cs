using System;
namespace StudentGradingSystem.Model;

using StudentGradingSystem.Repository;

public class Student
{

    public int StudentID { get; set; }


    public string StudentName { get; set; }

    public List<SubjectResult> Results { get; set; }

    public Student()
    {
        Results = new List<SubjectResult>();
    }

    public float GetTotalObtainedMarks {
        get
        {
            float total = 0;

            foreach(var subject in Results)
            {
                total += subject.CalculateTotal();
            }

            return total;
        }
    }

    public float GetFullMarks
    {
        get
        {
            float total = 0;

            foreach (var subject in Results)
            {
                total += subject.FullMarks;
            }

            return total;
        }
    }


    public Result CalculateResult()
    {

        var TotalSubjects = Results.Count;
        if (TotalSubjects <= 0)
        {
            return Result.FAIL;
        }
        float ObtainedMarks = 0;
        float FullMarks = 0;

        foreach (SubjectResult result in Results)
        {
            ObtainedMarks += result.GetTotalScore;
            FullMarks += result.FullMarks;
        }
        if (FullMarks == 0)
        {
            return Result.PASS;
        }
        var Percent = (ObtainedMarks / FullMarks) * 100;

        if (Percent > 75)
        {
            return Result.PASS;
        }
        return Result.FAIL;
    }

    public string GetResultString
    {
        get
        {
            var result = CalculateResult();

            if (result == Result.PASS)
            {
                return "Pass";
            }

            return "Fail";
        }
    }
    
}


public enum Result
{
    PASS,
    FAIL
}
