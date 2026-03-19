using StudentPortal.Repositories;
using StudentPortal.Services;
using StudentPortalDb.Models;

namespace StudentPortalDb.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;

        public StudentService(IStudentRepository repo)
        {
            _repo = repo;
        }

        public Task<List<Student>> SearchAsync(string? q = null)
        {
            return _repo.GetAllAsync(q);
        }

        public Task<Student?> GetAsync(int id)
        {
            return _repo.GetByIdAsync(id);
        }

        public async Task<(bool ok, string message)> CreateAsync(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.FullName))
                return (false, "Full name is required.");

            if (string.IsNullOrWhiteSpace(student.Email))
                return (false, "Email is required.");

            if (await _repo.EmailExistsAsync(student.Email))
                return (false, "Email already exists.");

            student.CreatedAt = DateTime.UtcNow;

            await _repo.AddAsync(student);

            return (true, "Student created successfully.");
        }

        public async Task<(bool ok, string message)> UpdateAsync(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.FullName))
                return (false, "Full name is required.");

            if (string.IsNullOrWhiteSpace(student.Email))
                return (false, "Email is required.");

            if (await _repo.EmailExistsAsync(student.Email, student.StudentId))
                return (false, "Email already exists.");

            await _repo.UpdateAsync(student);

            return (true, "Student updated successfully.");
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public Task<List<Student>> GetStudentsPagedAsync(int page, int pageSize)
        {
            return _repo.GetStudentsPagedAsync(page, pageSize);
        }
    }
}