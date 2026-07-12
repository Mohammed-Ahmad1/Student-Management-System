using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAPIDataAccessLayer.DTOs
{
    public class StudentGpaDto
    {
        public string StudentName { get; set; } = null!;
        public string Semester { get; set; } = null!;
        public double GPA { get; set; }
    }
}
