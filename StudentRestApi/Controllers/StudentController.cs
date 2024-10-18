using Microsoft.AspNetCore.Mvc;
using StudentRestApi.Model;
using System;


namespace StudentRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Student>>> Search(string name, Gender? gender)
        {
            try
            {
                var result = await _studentRepository.Search(name, gender);
                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error retrieving data from the database");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            try
            {
                return Ok(await _studentRepository.GetStudents());
            }
            catch (Exception)
            {
                return StatusCode(500, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            try
            {
                var result = await _studentRepository.GetStudent(id);

                if (result == null)
                {
                    return NotFound();
                }
                return result;

            }
            catch (Exception)
            {
                return StatusCode(500, "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            try
            {
                if (student == null)
                {
                    return BadRequest();
                }

                var stu = await _studentRepository.GetStudentByEmail(student.Email);
                if (student != null)
                {
                    ModelState.AddModelError("Email", "Student email already in use");
                    return BadRequest(ModelState);
                }
                var createdStudent = await _studentRepository.AddStudent(student);
                return CreatedAtAction(nameof(GetStudent), new { id = createdStudent.StudentId }, createdStudent);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error creating new student record");
            }
        }
        [HttpPost("Update, {id:int}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id, Student student)
        {
            try
            {
                if (id != student.StudentId)
                {
                    return BadRequest("Student ID mismatch");
                }
                var studentToUpdate = await _studentRepository.GetStudent(id);
                if (studentToUpdate == null)
                {
                    return NotFound($"Student with id {id} not found");
                }
                return await _studentRepository.UpdateStudent(student);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error updateing Student record.");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            try
            {
                var studentToDelete = await _studentRepository.GetStudent(id);
                if (studentToDelete == null)
                {
                    return NotFound($"Student with id {id} Not Found.");
                }
                await _studentRepository.DeleteStudent(id);
                return Ok($"Student with id {id} Deleted.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error deleting Student record.");
            }
        }
    }
}
