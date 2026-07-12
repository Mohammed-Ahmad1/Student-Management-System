using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAPIDataAccessLayer.DTOs
{
    public class PassedStudentsDTO
    {
        public int StudentID {  get; set; }
        public string StudentName { get; set; } = null!;

        public int Grade {  get; set; }

        public string Semester { get; set; } = null!;
    }
}
