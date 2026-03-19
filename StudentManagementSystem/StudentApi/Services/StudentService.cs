namespace StudentApi.Services;

using StudentApi.Models;
using StudentApi.Repositories;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repository;

    public StudentService(IStudentRepository repository)
    {
        _repository = repository;
    }

    public List<Student> GetStudents()
    {
        return _repository.GetAll();
    }

    public Student GetStudentById(int id)
    {
        return _repository.GetById(id);
    }

    public Student CreateStudent(Student student)
    {
        return _repository.Add(student);
    }

    public Student UpdateStudent(int id, Student student)
    {
        return _repository.Update(id, student);
    }

    public bool DeleteStudent(int id)
    {
        return _repository.Delete(id);
    }
}