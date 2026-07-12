using Microsoft.EntityFrameworkCore;
using StudentAPIDataAccessLayer.DTOs;
using StudentAPIDataAccessLayer.Entities;
using Students_Management_System.Data_Access_Layer.Data;

namespace StudentAPIDataAccessLayer.DataAccess
{
    public class StudentsDataAccess
    {
        private readonly AppDbContext _context;
        public StudentsDataAccess(AppDbContext context)
        {
            _context = context;
        }

        public List<Student> GetAllStudents()
        {
            return _context.Students
                .AsNoTracking()
                .ToList();
        }

        public Student? GetStudentById(int id)
        {
            return _context.Students
                .FirstOrDefault(s => s.Id == id);
        }

        public Student? AddStudent(CreateStudentDto NewStudent)
        {
            var student = new Student
            {
                Name = NewStudent.Name,
                Age = NewStudent.Age,
                Email = NewStudent.Email,
                EnrollmentDate = NewStudent.EnrollmentDate,
                IsActive = NewStudent.IsActive,
                DepartmentId = NewStudent.DepartmentID
            };

            _context.Students.Add(student);
            _context.SaveChanges();

            return student;
        }


        public Student UpdateStudent(int id, UpdatedStudentDTO updatedStudent)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == id);

            student.Name = updatedStudent.Name;
            student.Age = updatedStudent.Age;
            student.Email = updatedStudent.Email;
            student.EnrollmentDate = updatedStudent.EnrollmentDate;
            student.IsActive = updatedStudent.IsActive;

            _context.SaveChanges();

            return student;
        }

        public bool DeleteStudent(int id)
        {
            int RowsEffected = -1;

            var student = _context.Students.FirstOrDefault(s => s.Id == id);

            if (student == null)
                return false;

            _context.Students.Remove(student);

            RowsEffected = _context.SaveChanges();

            return RowsEffected > 0;
        }

        public List<PassedStudentsDTO> GetPassedStudents()
        {
            return (
                from s in _context.Students
                join e in _context.Enrollments
                on s.Id equals e.StudentID
                where e.Grade >= 50
                orderby e.Grade descending
                select new PassedStudentsDTO
                {
                    StudentID = s.Id,
                    StudentName = s.Name,
                    Grade = e.Grade,
                    Semester = e.Semester
                }
            ).ToList();
        }

        public double GetAverageGrade()
        {
            return _context.Enrollments.Any()
                ? _context.Enrollments.Average(e => e.Grade)
                : 0;
        }

        public List<Student> GetActiveStudents()
        {
            var ActiveStudents =
                _context.Students.Where(s => s.IsActive == true).ToList();

            return ActiveStudents;
        }

        public List<Student> GetInActiveStudents()
        {
            var ActiveStudents =
                _context.Students.Where(s => s.IsActive == false).ToList();

            return ActiveStudents;
        }

        public double GetStudentGpaBySemester(int studentId, string semester)
        {
            var gpa = _context.Enrollments
                .Where(e => e.StudentID == studentId && e.Semester == semester)
                .Average(e => (double)e.Grade);

            return gpa;
        }

        public List<Student> GetStudentsInCourse(int CourseID)
        {
            var students =
                (from s in _context.Students
                 join e in _context.Enrollments
                     on s.Id equals e.StudentID
                 join c in _context.Courses
                     on e.CourseID equals c.Id
                 where c.Id == CourseID
                 select s)
                 .Distinct()
                 .ToList();

            return students;
        }

    }
}