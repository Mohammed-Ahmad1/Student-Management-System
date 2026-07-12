using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAPIDataAccessLayer.DTOs
{
    public class StudentTranscriptDto
    {
        public string StudentName { get; set; } = null!;
        public string CourseTitle { get; set; } = null!;
        public int Grade { get; set; }
    }
}