using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudentAPIDataAccessLayer.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string? Email { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool IsActive { get; set; }
        public int DepartmentId { get; set; }



        // Navigation Propeties

        [JsonIgnore]
        public virtual ICollection<Enrollment> Enrollments { get; set; } 
            = new List<Enrollment>();


        [JsonIgnore]
        public virtual Department Department { get; set; } = null!;
    }
}
