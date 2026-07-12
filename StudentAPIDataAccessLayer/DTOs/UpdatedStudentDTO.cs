using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAPIDataAccessLayer.DTOs
{
    public class UpdatedStudentDTO
    {
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string? Email { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool IsActive { get; set; }

    }
}
