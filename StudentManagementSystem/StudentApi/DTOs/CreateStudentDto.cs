namespace StudentApi.DTOs;

public class CreateStudentDto
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Status { get; set; } = "Active";
}