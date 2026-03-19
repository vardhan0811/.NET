namespace SchoolWebApi.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public List<Student> Students { get; set; }
    }
}
