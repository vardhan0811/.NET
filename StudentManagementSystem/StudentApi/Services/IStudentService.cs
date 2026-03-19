using StudentApi.Models;

namespace StudentApi.Services;

public interface IStudentService
{
    List<Student> GetStudents();
    Student GetStudentById(int id);
    Student CreateStudent(Student student);
    Student UpdateStudent(int id, Student student);
    bool DeleteStudent(int id);
}