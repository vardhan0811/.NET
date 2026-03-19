using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StudentApiTraining.Models;
using StudentApiTraining.Services;

namespace StudentApiTraining.Controllers
{

    [ApiController]
    // - Automatic model validation (If request body is invalid, ASP.NET automatically returns 400 BadRequest)
    // - Automatic parameter binding (FromBody, FromQuery, FromRoute) ASP.NET automatically understands where data comes from
    // - Better error responses (Instead of plain text errors, responses become structured JSON)

    [Route("api/[controller]")]
    // - This defines the URL path for this controller.
    // - [controller] is a placeholder that ASP.NET replaces with the controller's name (without "Controller" suffix). So, for StudentsController, the route becomes "api/student".

    public class StudentsController : ControllerBase
    // - ControllerBase provides (Ok(), BadRequest(), NotFound(), Created(), NoContent())
    {
        // Now ASP.NET injects the service automatically.
        private readonly IStudentService _service;
        public StudentsController(IStudentService service)
        {
            _service = service;
        }

        // - Handle GET requests to "api/students" and return a list of students.
        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _service.GetStudents();
            return Ok(students);
        }

        [HttpGet("{id}")]
        // - Route parameter : "{id}" - handle requests like "api/students/1", where "1" is the value of the "id" parameter to get a student by id.
        public IActionResult GetStudentById(int id)
        {
            var student = _service.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpDelete("{id}")]
        // - Route parameter : "{id}" - handle requests like "api/students/1", where "1" is the value of the "id" parameter to delete a student by id.
        public IActionResult DeleteStudentById(int id)
        {
            var student = _service.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            _service.DeleteStudent(id);
            return NoContent(); // 204 No Content indicates that the request was successful but there is no content to return.
        }

        [HttpPut("{id}")]
        // - Route parameter : "{id}" - handle requests like "api/students/1", where "1" is the value of the "id" parameter to update a student by id.
        public IActionResult UpdateStudentById(int id, [FromBody] Student updatedStudent)
        {
            var student = _service.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            student.Name = updatedStudent.Name;
            student.Age = updatedStudent.Age;
            student.Course = updatedStudent.Course;

            return Ok(student);
        }

        [HttpPatch("{id}")]
        // - Route parameter : "{id}" - handle requests like "api/students/1", where "1" is the value of the "id" parameter to partially update a student by id.
        public IActionResult patchUpdateStudentById(int id, [FromBody] Student patchStudent)
        {
            var student = _service.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrWhiteSpace(patchStudent.Name))
            {
                student.Name = patchStudent.Name;
            }
            if (patchStudent.Age != 0)
            {
                student.Age = patchStudent.Age;
            }
            if (!string.IsNullOrWhiteSpace(patchStudent.Course))
            {
                student.Course = patchStudent.Course;
            }
            return Ok(student);
        }

        [HttpHead]
        // - Handle HEAD requests to "api/students/count". This endpoint can be used to check if the resource exists and get metadata without returning the actual content.
        public IActionResult GetStudentsCount()
        {
           Response.Headers.Append("Students Count: ", _service.GetStudents().Count.ToString());
            return Ok();
        }
            
        [HttpOptions]
        // - How many HTTP methods are allowed for this endpoint.
        public IActionResult GetStudentsOptions()
        {
            Response.Headers.Append("Allow", "GET, POST, PUT, PATCH, DELETE, HEAD, OPTIONS");
            return Ok();
        }

        [HttpPost]
        // - Create a new student. The student data is expected to be in the request body, and ASP.NET will automatically bind it to the "newStudent" parameter.
        public IActionResult CreateStudentById([FromBody] Student newStudent)
        {
            newStudent.Id = _service.GetStudents().Any() ? _service.GetStudents().Max(s => s.Id) + 1 : 1;
            _service.CreateStudent(newStudent);
            return CreatedAtAction(nameof(GetStudentById), new { id = newStudent.Id }, newStudent);
        }
    }
}
