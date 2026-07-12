using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudentAPIDataAccessLayer.Entities
{
    public class Instructor
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public DateTime HireDate { get; set; }

        public string? Office { get; set; }

        public decimal? Salary { get; set; }

        public bool IsActive { get; set; }

        public int DepartmentId { get; set; }



        [JsonIgnore]
        public virtual Department Department { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<CourseInstructor> CourseInstructors { get; set; }
            = new List<CourseInstructor>();



    }
}
