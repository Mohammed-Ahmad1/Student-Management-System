using Microsoft.AspNetCore.Mvc;
using StudentAPIBusinessLayer.ServiceLayer;
using StudentAPIDataAccessLayer.DTOs;
using StudentAPIDataAccessLayer.Entities;
using System.Collections;

namespace StudentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentsService _service;
        public StudentsController(StudentsService service)
        {
            _service = service;
        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetAllStudents")]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            var students = _service.GetAllStudents();

            if (students.Count == 0)
            {
                return NotFound("No Any Student");
            }

            return Ok(students);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetPassedStudents")]
        public ActionResult<Student> GetPassedStudent()
        {
            var PassedStudent = _service.GetPassedStudents();

            if (PassedStudent.Count == 0)
            {
                return NotFound("No Passed Students");
            }

            return Ok(PassedStudent);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetAverageGrades")]
        public ActionResult<double> GetAverageGrades()
        {
            var AverageGrades = _service.GetAverageGrade();

            return Ok(AverageGrades);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}", Name = "GetStudentByID")]
        public ActionResult<Student> GetStudentByID(int ID)
        {
            if (ID <= 0)
                return BadRequest("Enter a Valid ID Number");

            var FindStudent = _service.GetStudentById(ID);

            if (FindStudent == null)
                return NotFound($"Student With ID :{ID} Not Found");


            return Ok(FindStudent);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{ID}")]
        public ActionResult DeleteStudent(int ID)
        {
            // you can't to delete a student if the student have enrollment(s). 

            if (ID <= 0)
                return BadRequest("Enter a Valid ID Number");

            var Student = _service.DeleteStudent(ID);

            if (!Student)
                return NotFound($"Student With ID: {ID} Not Found");


            return Ok("Student Deleted Sucsessfully");

        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("AddNewStudent")]
        public ActionResult<Student> AddNewStudent(CreateStudentDto NewStudent)
        {
            if (string.IsNullOrEmpty(NewStudent.Name) || NewStudent.Age <= 0)
                return BadRequest("Enter Valid Data");


            var createdStudent = _service.AddStudent(NewStudent);

            if (createdStudent == null)
                return BadRequest("Failed to add student.");


            return CreatedAtAction(
                nameof(GetStudentByID),
                new { id = createdStudent.Id },
                createdStudent);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("UpdateStudent")]
        public ActionResult UpdateStudent(int id, UpdatedStudentDTO UpdatedStudent)
        {
            if (string.IsNullOrEmpty(UpdatedStudent.Name) || UpdatedStudent.Age <= 0)
                return BadRequest("Enter a Valid Data");

            var UpdateStudent = _service.UpdateStudent(id, UpdatedStudent);

            if (UpdateStudent == null)
                return NotFound("Student Not Found");

            return Ok(UpdateStudent);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetActiveStudent")]
        public ActionResult<IEnumerable<Student>> GetActiveStudents()
        {
            var ActiveStudents = _service.GetActiveStudents();

            if (ActiveStudents.Count == 0)
                return NotFound("No Active Students");


            return Ok(ActiveStudents);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetInActiveStudent")]
        public ActionResult<IEnumerable<Student>> GetInActiveStudents()
        {
            var InActiveStudents = _service.GetInActiveStudents();

            if (InActiveStudents.Count == 0)
                return NotFound("No InActive Students");


            return Ok(InActiveStudents);
        }


        [HttpGet("{id}/GPA_BySemester")]
        public IActionResult GetGpa(int id, string semester)
        {
            if (id <= 0)
                return BadRequest("Invalid student ID");

            var gpa = _service.GetStudentGpaBySemester(id, semester);

            return Ok(new
            {
                StudentId = id,
                Semester = semester,
                GPA = gpa
            });
        }



        [HttpGet("{CourseID}/Course", Name = "GetStudentWithCourse")]
        public ActionResult<List<Student>> GetStudentsInCourse(int CourseID)
        {
            var Students = _service.GetStudentsInCourse(CourseID);
            if (Students.Count == 0)
                return NotFound($"No Students Inside Course With ID:{CourseID}");

            return Ok(Students);
        }

    } 
}