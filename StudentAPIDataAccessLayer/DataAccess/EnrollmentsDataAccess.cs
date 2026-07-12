using Microsoft.EntityFrameworkCore;
using StudentAPIDataAccessLayer.DTOs;
using StudentAPIDataAccessLayer.Entities;
using Students_Management_System.Data_Access_Layer.Data;

namespace StudentAPIDataAccessLayer.DataAccess
{
    public class EnrollmentsDataAccess
    {
        private readonly AppDbContext _context;
        public EnrollmentsDataAccess(AppDbContext context)
        {
            _context = context;
        }


        public List<Enrollment> GetEnrollmentsPerStudent(int StudentID)
        {
            return _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Where(e=>e.StudentID== StudentID)
                .ToList();
        }

        public int GetTotalCompleteCreditPerStudent(int StudentID)
        {
            var totalCredits =
                (
                    from s in _context.Students
                    join e in _context.Enrollments
                    on s.Id equals e.StudentID
                    join c in _context.Courses
                    on e.CourseID equals c.Id
                    where s.Id == StudentID
                    select c.Credits
                 )
                .Sum();



            return totalCredits;
        }


        public double CalculateStudentGPA(int studentID)
        {
            var gpa = _context.Students
                .Where(s => s.Id == studentID)
                .Select(s =>
                    s.Enrollments.Sum(e => e.Grade * e.Course.Credits)
                    / s.Enrollments.Sum(e => e.Course.Credits)
                )
                .FirstOrDefault();

            return gpa;
        }


        public List<CourseEnrollmentReportDto> GetTotalNumberOfEnrolledStudentsInCourses()
        {
            var result =
                (
                from c in _context.Courses
                join e in _context.Enrollments
                    on c.Id equals e.CourseID
                join s in _context.Students
                    on e.StudentID equals s.Id
                group e by c.Title into g

                select new CourseEnrollmentReportDto
                {
                    CourseTitle = g.Key,
                    StudentsCount = g.Count()
                }
                ).ToList();


            return result;

        }



        public List<StudentTranscriptDto> GetStudentTranscript(int studentId)
        {
            var result =
                (from s in _context.Students
                 join e in _context.Enrollments
                     on s.Id equals e.StudentID
                 join c in _context.Courses
                    on e.CourseID equals  c.Id 
                 where s.Id == studentId
                 select new StudentTranscriptDto
                 {
                     StudentName = s.Name,
                     CourseTitle = c.Title,
                     Grade = e.Grade
                 })
                .ToList();

            return result;
        }

    
    }
}
