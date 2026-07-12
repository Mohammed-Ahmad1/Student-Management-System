using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAPIDataAccessLayer.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentID { get; set; }
        public int CourseID { get; set; }
        public int Grade {  get; set; }
        public Decimal AttendanceRate {  get; set; }
        public string? Semester { get; set; } 




        // Navigation Propeties
        public Student Student { get; set; }=null!;
        public Course Course { get; set; } = null!;

    }
}
