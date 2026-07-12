using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using StudentAPIBusinessLayer.ServiceLayer;
using StudentAPIDataAccessLayer.Entities;

namespace StudentManagementsAPISystem.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController:ControllerBase
    {
        private readonly CourseService _service;
        public CoursesController(CourseService service)
        {
            _service = service;
        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetAllCourses")]
        public ActionResult<IEnumerable<Course>> GetAllCourses()
        {
            var Courses = _service.GetAllCourses();

            if (Courses.Count == 0)
            {
                return NotFound("No Any Courses");
            }

            return Ok(Courses);
        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("course/{id}", Name = "GetCourseByID")]
        public ActionResult<Course> GetCourseByID(int id)
        {
            if (id <= 0)
                return BadRequest("Enter a Valid ID Number");

            var FindCourse = _service.GetCourseById(id);

            if (FindCourse == null)
                return NotFound($"Course With ID :{id} Not Found");


            return Ok(FindCourse);
        }




        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpGet("credits/{credits}",Name ="GetCoursesWithCredits")]
        public ActionResult<IEnumerable<Course>>GetCoursesWithCredit(int credits)
        {
            if (credits <= 0)
                return BadRequest("Credits Must to be grater then 0");


            var Courses = _service.GetCoursesWithCredits(credits);

            if(Courses.Count==0)
            {
                return NotFound($"No Courses With {credits} Credits");
            }

            return Ok(Courses);

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Course> AddCourse(Course course)
        {
            if (course == null)
                return BadRequest("Course data is required");

            try
            {
                var created = _service.AddCourse(course);
                return CreatedAtAction(nameof(GetCourseByID), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Course> UpdateCourse(int id, Course course)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            if (course == null)
                return BadRequest("Course data is required");

            try
            {
                var updated = _service.UpdateCourse(id, course);
                return Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteCourse(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            try
            {
                var deleted = _service.DeleteCourse(id);
                if (!deleted)
                    return NotFound($"Course with ID: {id} not found");

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
