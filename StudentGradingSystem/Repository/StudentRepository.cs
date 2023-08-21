using System;
using System.Text.Json;
using StudentGradingSystem.Model;

namespace StudentGradingSystem.Repository;


public class StudentRepository
{
    public StudentRepository()
    {
    }

    public const string STORAGE_FILE_NAME = "Resources/Storage/students.json";

    public string GetStorageFilePath()
    {
        var AppDataDirectory = FileSystem.AppDataDirectory;
        return Path.Combine(AppDataDirectory, STORAGE_FILE_NAME);
    }

    private void CreateStorageDirectory()
    {

        if (!Directory.Exists(Path.Combine(FileSystem.AppDataDirectory, "Resources")))
        {
            Directory.CreateDirectory(Path.Combine(FileSystem.AppDataDirectory, "Resources"));
        }

        if (!Directory.Exists(Path.Combine(FileSystem.AppDataDirectory, "Resources/Storage")))
        {
            Directory.CreateDirectory(Path.Combine(FileSystem.AppDataDirectory, "Resources/Storage"));
        }

        if (!File.Exists(Path.Combine(FileSystem.AppDataDirectory, STORAGE_FILE_NAME)))
        {
            File.WriteAllText(Path.Combine(FileSystem.AppDataDirectory, STORAGE_FILE_NAME), "[]");
        }


    }

    public async Task Delete(int studentId)
    {
        var permission = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

        if (!permission.Equals(PermissionStatus.Granted))
        {
            await Permissions.RequestAsync<Permissions.StorageWrite>();
        }

        var students = await load(); // Load existing student data

        var studentToDelete = students.Find(student => student.StudentID == studentId);
        if (studentToDelete != null)
        {
            students.Remove(studentToDelete);

            await Save(students);
        }
    }

    private Boolean StorageFileExists()
    {
        return File.Exists(Path.Combine(FileSystem.AppDataDirectory, STORAGE_FILE_NAME));
    }

    async public Task<List<Student>> load()
    {
        var permission = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

        if (!permission.Equals(PermissionStatus.Granted))
        {
            await Permissions.RequestAsync<Permissions.StorageRead>();
        }
        if (!StorageFileExists())
        {
            CreateStorageDirectory();
            return new List<Student>();
        }

        string storagePath = Path.Combine(FileSystem.AppDataDirectory, STORAGE_FILE_NAME);

        try
        {
            return JsonSerializer.Deserialize<List<Student>>(File.ReadAllText(storagePath));
        }
        catch (Exception e)
        {

            Console.WriteLine(e.Message);
            return new List<Student>();
        }


    }

    async public Task<object> Save(List<Student> students)
    {

        try
        {
            var permission = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

            if (!permission.Equals(PermissionStatus.Granted))
            {
                await Permissions.RequestAsync<Permissions.StorageWrite>();
            }

            var json = JsonSerializer.Serialize(students);


            var path = Path.Combine(FileSystem.AppDataDirectory, STORAGE_FILE_NAME);

            if (!File.Exists(path))
            {
                CreateStorageDirectory();
            }
            File.WriteAllText(path, json);
            return json;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public List<Student> GetDummyStudents()
    {

        var students = new List<Student>();

        for (var i = 0; i < 100; i++)
        {
            students.Add(GetDummyStudent());
        }

        return students;
    }


    public Student GetDummyStudent()
    {
        var random = new Random();
        var student = new Student();
        student.StudentID = random.Next(100000, 999999);
        student.StudentName = GenerateRandomString(random.Next(3, 10)) + " " + GenerateRandomString(random.Next(3, 10));

        var subjects = new List<SubjectResult>();
        var subjectLength = random.Next(2, 10);
        for (var i = 0; i < subjectLength; i++)
        {
            subjects.Add(GetDummySubjectData());
        }


        student.Results = subjects;

        return student;
    }

    public SubjectResult GetDummySubjectData()
    {

        var name = GenerateRandomString(10);
        var random = new Random();

        float written = random.Next(1, 20);
        float oral = random.Next(1, 10);
        float attendance = random.Next(1, 10);
        float project = random.Next(1, 10);

        return new SubjectResult(name, written, oral, attendance, project);
    }

    public static string GenerateRandomString(int length)
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        char[] result = new char[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = chars[random.Next(chars.Length)];
        }

        return new string(result);
    }
}


