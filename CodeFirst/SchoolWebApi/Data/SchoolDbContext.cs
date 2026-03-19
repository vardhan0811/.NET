using Microsoft.EntityFrameworkCore;
using SchoolWebApi.Models;

namespace SchoolWebApi.Data
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Department> Departments { get; set; }
    }
}
