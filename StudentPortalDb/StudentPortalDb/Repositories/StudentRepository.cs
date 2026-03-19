using Microsoft.EntityFrameworkCore;
using StudentPortalDb.Models;

namespace StudentPortal.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentPortalDbContext _db;

        public StudentRepository(StudentPortalDbContext db)
        {
            _db = db;
        }

        public async Task<List<Student>> GetAllAsync(string? q = null)
        {
            var query = _db.Students.AsQueryable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                q = q.Trim().ToLower();
                query = query.Where(s =>
                    s.FullName.ToLower().Contains(q) ||
                    s.Email.ToLower().Contains(q));
            }

            return await query
                .AsNoTracking()
                .OrderBy(s => s.StudentId)
                .ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _db.Students.FindAsync(id);
        }

        public async Task AddAsync(Student student)
        {
            await _db.Students.AddAsync(student);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            _db.Students.Update(student);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _db.Students.FindAsync(id);
            if (student != null)
            {
                _db.Students.Remove(student);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<bool> EmailExistsAsync(string email, int? ignoreStudentId = null)
        {
            return await _db.Students
                .AnyAsync(s => s.Email == email &&
                               (!ignoreStudentId.HasValue || s.StudentId != ignoreStudentId));
        }

        public async Task<List<Student>> GetStudentsPagedAsync(int pageNumber, int pageSize)
        {
            return await _db.Students
                .FromSqlRaw("EXEC GetStudentsPaged @PageNumber = {0}, @PageSize = {1}",
                             pageNumber, pageSize)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}