using Microsoft.AspNetCore.Mvc;
using StudentApi.Models;
using StudentApi.Services;

namespace StudentApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _service;
    public StudentsController(IStudentService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetStudents()
    {
        var students = _service.GetStudents();
        return Ok(students);
    }

    [HttpGet("{id}")]
    public IActionResult GetStudentById(int id)
    {
        var student = _service.GetStudentById(id);
        if (student == null)
        {
            return NotFound();
        }
        return Ok(student);
    }

    [HttpPost]
    public IActionResult CreateStudent([FromBody] Student student)
    {
        var created = _service.CreateStudent(student);
        return CreatedAtAction(nameof(GetStudentById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateStudent(int id, [FromBody] Student student)
    {
        var updated = _service.UpdateStudent(id, student);
        if (updated == null)
        {
            return NotFound();
        }
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteStudent(int id)
    {
        var deleted = _service.DeleteStudent(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}