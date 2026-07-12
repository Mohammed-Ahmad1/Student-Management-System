using Microsoft.AspNetCore.Mvc;
using StudentAPIBusinessLayer.ServiceLayer;
using StudentAPIDataAccessLayer.Entities;

namespace StudentManagementsAPISystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly DepartmentService _service;

        public DepartmentsController(DepartmentService service)
        {
            _service = service;
        }

        // GET: api/departments
        [HttpGet]
        public ActionResult<List<Department>> GetAllDepartments()
        {
            return Ok(_service.GetAllDepartments());
        }

        // GET: api/departments/5
        [HttpGet("{id}")]
        public ActionResult<Department> GetDepartmentById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            var dept = _service.GetDepartmentById(id);

            if (dept == null)
                return NotFound("Department not found");

            return Ok(dept);
        }

        // POST: api/departments
        [HttpPost]
        public ActionResult<Department> AddDepartment(Department department)
        {
            if (string.IsNullOrWhiteSpace(department.Name))
                return BadRequest("Department name is required");

            var created = _service.AddDepartment(department);

            return CreatedAtAction(
                nameof(GetDepartmentById),
                new { id = created.Id },
                created
            );
        }

        // PUT: api/departments/5
        [HttpPut("{id}")]
        public ActionResult<Department> UpdateDepartment(int id, Department department)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            try
            {
                var updated = _service.UpdateDepartment(id, department);
                return Ok(updated);
            }
            catch (System.Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/departments/5
        [HttpDelete("{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            var result = _service.DeleteDepartment(id);

            if (!result)
                return NotFound("Department not found");

            return NoContent();
        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("GetDepartmentsByCourseID")]
        public ActionResult<IEnumerable<Department>> GetCoursesByDepartmentId(int id)
        {
            var Depts = _service.GetDepartmentsByCourseID(id);


            if (id <= 0) 
                return BadRequest("Enter a Valid Course ID");


            if(Depts.Count==0)
                return NotFound("This Course Not Assigned Yet To Any Department");

            return Ok(Depts);
        }

    }
}