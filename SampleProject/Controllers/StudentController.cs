using ASP.NETWebAPI.Models;
using ASP.NETWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ASP.NETWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;
        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpGet("AllStudents")]
        [HttpGet("students")]
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<StudentInfo>> GetAllStudents()
        {
            return Ok(_studentService.GetAllStudents());
        }

        [HttpGet("student/{studentId}")]
        public ActionResult<StudentInfo> GetStudentById(int studentId)
        {
            try
            {
                var student = _studentService.GetStudentById(studentId);
                return Ok(student);

            }
            catch (ArgumentException ex)
            {
                return BadRequest("Invalid student ID.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Student with ID {studentId} not found.");
            }
        }

        [HttpGet("[action]")]
        public ActionResult<IEnumerable<StudentInfo>> GetStudentsWithMarksAbove(double mark)
        {
            try
            {
                var students = _studentService.StudentWithAboveSpecifiedMarks(mark);
                return Ok(students);
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Invalid marks , marks should be between 0 and 100");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(" there is no student who has marks above the specified value");
            }


        }
        [HttpPost("addstudent")]
        public ActionResult AddStudent([FromBody] StudentInfo student)
        {
            if (student == null)
            {
                return BadRequest("Invalid student data.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _studentService.AddStudent(student);
            return Ok($"Added Sucessfully");
        }

        [HttpPut("Update")]
        public ActionResult UpdatetheStudentInfo([FromBody] StudentInfo updatingstudentinfo)
        {
            try
            {
                _studentService.UpdateStudentInfo(updatingstudentinfo);
                return Ok($" the student {updatingstudentinfo.name} updated sucessfully");

            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"the studnet  {updatingstudentinfo.name} not found");
            }

        }

        [HttpPatch("updatemarks/{id}")]
        public ActionResult UpdateStudentPartially([FromRoute] int id, [FromBody] double newMark)
        {
            try
            {
                var student = _studentService.UpdateMarks(id, newMark);
                return Ok($"Student mark updated to {newMark} sucessfully");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound("the student with {id} is not found");
            }

        }

        [HttpDelete("deletebyId")]
        public ActionResult<IEnumerable<StudentInfo>> DeleteStudentInfoById([FromQuery] int id)
        {
            try
            {
                _studentService.Delete(id);
                return Ok("deleted successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Student with ID {id} not found.");
            }
        }
    }
}
