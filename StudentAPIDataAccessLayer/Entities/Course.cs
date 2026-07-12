using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudentAPIDataAccessLayer.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Title { get; set; } = null!;
        public int Credits { get; set; }
        public string? Description { get; set; }



        // Navigation Propeties

        [JsonIgnore]
        public virtual ICollection<Enrollment> Enrollments { get; set; } 
            = new List<Enrollment>();


        [JsonIgnore]
        public virtual ICollection<CourseInstructor> CourseInstructors { get; set; }
    = new List<CourseInstructor>();


        [JsonIgnore]
        public virtual ICollection<CourseDepartment> CourseDepartments { get; set; }
    = new List<CourseDepartment>();
    }
}
