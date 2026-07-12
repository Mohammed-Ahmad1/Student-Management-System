using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using StudentAPIBusinessLayer.ServiceLayer;
using StudentAPIDataAccessLayer.DTOs;
using StudentAPIDataAccessLayer.Entities;

namespace StudentManagementsAPISystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly EnrollmentsService _service;
        public EnrollmentsController(EnrollmentsService service)
        {
            _service = service;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{StudentID}", Name ="GetStudentEnrollment")]
        public ActionResult<IEnumerable<Enrollment>> GetEnrollmentPerStudent(int StudentID)
        {
            var Enrollments = _service.GetEnrollmentsPerStudent(StudentID);

            if (Enrollments.Count==0)
            {
                return NotFound($"No Enrollment For a Student With ID: {StudentID}");
            }


            return Ok(Enrollments);
        }





        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{StudentID}/Get/", Name = "GetTotalCompleteCreditPerStudent")]
        public ActionResult<int> GetTotalCompleteCreditPerStudent(int StudentID)
        {
            if(StudentID<=0)
            {
                return BadRequest("Enter a Valid ID Number");
            }



            int SumOfCompletedCredits = _service.GetTotalCompleteCreditPerStudent(StudentID);

            return Ok(SumOfCompletedCredits);
        }

        [HttpGet("{id}/gpa")]
        public IActionResult GetStudentGPA(int id)
        {
            var gpa = _service.CalculateStudentGPA(id);

            return Ok(new
            {
                StudentId = id,
                GPA = gpa
            });
        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("GetNumberOfEnrolledStudetns")]
        public ActionResult<IEnumerable<CourseEnrollmentReportDto>>GetNumberOfEnrolledStudent()
        {
            var EnrolledStudents = _service.GetNumberOfEnrolledStudents();

            if (EnrolledStudents.Count == 0)
                return NotFound("No Enrolled Students");

            return Ok(EnrolledStudents);
        }


        [HttpGet("{id}/transcript")]
        public IActionResult GetTranscript(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid student ID");

            var result = _service.GetStudentTranscript(id);

            if (result == null || result.Count == 0)
                return NotFound("No transcript found for this student");

            return Ok(result);
        }

    }
}
