using Microsoft.AspNetCore.Mvc;
using StudentAPIBusinessLayer.ServiceLayer;
using StudentAPIDataAccessLayer.Entities;

namespace StudentManagementsAPISystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private readonly InstructorsService _service;

        public InstructorsController(InstructorsService service)
        {
            _service = service;
        }

        // GET ALL
        [HttpGet]
        public ActionResult<List<Instructor>> GetAll()
        {
            return Ok(_service.GetAllInstructors());
        }

        // GET BY ID
        [HttpGet("{id}")]
        public ActionResult<Instructor> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            var instructor = _service.GetInstructorById(id);

            if (instructor == null)
                return NotFound();

            return Ok(instructor);
        }

        // POST
        [HttpPost]
        public ActionResult<Instructor> Add(Instructor instructor)
        {
            if (string.IsNullOrWhiteSpace(instructor.FirstName) ||
                string.IsNullOrWhiteSpace(instructor.LastName))
                return BadRequest("Invalid data");

            var created = _service.AddInstructor(instructor);

            if (created == null)
                return BadRequest("Invalid Department ID");

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT
        [HttpPut("{id}")]
        public ActionResult<Instructor> Update(int id, Instructor instructor)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            var updated = _service.UpdateInstructor(id, instructor);

            return Ok(updated);
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            var result = _service.DeleteInstructor(id);

            if (!result)
                return NotFound();

            return NoContent();
        }

        // GET Instructor Courses
        [HttpGet("GetInstructorCourses/{id}")]
        public ActionResult<IEnumerable<Course>> GetInstructorCourses(int id)
        {
            if (id <= 0)
                return BadRequest("Enter a valid Instructor ID");

            var courses = _service.GetInstructorCourses(id);

            if (courses == null || !courses.Any())
                return NotFound($"No courses found for instructor with ID: {id}");

            return Ok(courses);
        }
    }
}