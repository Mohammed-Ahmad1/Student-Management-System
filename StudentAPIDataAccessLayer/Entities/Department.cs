using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudentAPIDataAccessLayer.Entities
{
    public class Department
    {
            public int Id { get; set; }

            public string Name { get; set; } = null!;

            public string? Description { get; set; }

            // Navigation Properties

            [JsonIgnore]
            public virtual ICollection<Student> Students { get; set; }
                = new List<Student>();

            [JsonIgnore]
            public virtual ICollection<Instructor> Instructors { get; set; }
                = new List<Instructor>();

        [JsonIgnore]
        public virtual ICollection<CourseDepartment> CourseDepartments { get; set; }
    = new List<CourseDepartment>();
    }
}
