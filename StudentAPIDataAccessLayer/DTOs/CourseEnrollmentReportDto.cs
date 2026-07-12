using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAPIDataAccessLayer.DTOs
{
    public class CourseEnrollmentReportDto
    {
        public string CourseTitle { get; set; } = null!;
        public int StudentsCount { get; set; }
    }
}
