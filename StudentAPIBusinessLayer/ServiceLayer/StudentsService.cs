using StudentAPIDataAccessLayer.DataAccess;
using StudentAPIDataAccessLayer.DTOs;
using StudentAPIDataAccessLayer.Entities;

namespace StudentAPIBusinessLayer.ServiceLayer
{
    public class StudentsService
    {
        private readonly StudentsDataAccess _dataAccess;
        private readonly DepartmentDataAccess _departmentDAL;


        public StudentsService(StudentsDataAccess dataAccess, DepartmentDataAccess departmentDAL)
        {
            _dataAccess = dataAccess;
            _departmentDAL = departmentDAL;
        }


        public List<Student> GetAllStudents()
        {
            return _dataAccess.GetAllStudents();
        }

        public List<PassedStudentsDTO> GetPassedStudents()
        {
            return _dataAccess.GetPassedStudents();
        }

        public double GetAverageGrade()
        {
            return _dataAccess.GetAverageGrade();
        }

        public Student? GetStudentById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            return _dataAccess.GetStudentById(id);

        }

        public Student? AddStudent(CreateStudentDto student)
        {
            if (!_departmentDAL.DepartmentExists(student.DepartmentID))
                return null;

            return _dataAccess.AddStudent(student);
        }

        public bool DeleteStudent(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            var student = _dataAccess.GetStudentById(id);

            return _dataAccess.DeleteStudent(id);
        }

        public Student UpdateStudent(int id, UpdatedStudentDTO updatedStudent)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            return _dataAccess.UpdateStudent(id, updatedStudent);
        }

        public List<Student>GetActiveStudents()
        {
            return _dataAccess.GetActiveStudents();
        }

        public List<Student> GetInActiveStudents()
        {
            return _dataAccess.GetInActiveStudents();
        }

        public double GetStudentGpaBySemester(int studentId, string semester)
        {
            if (studentId <= 0)
                throw new ArgumentException("Invalid student ID");

            if (string.IsNullOrEmpty(semester))
                throw new ArgumentException("Semester is required");

            var gpa = _dataAccess.GetStudentGpaBySemester(studentId, semester);

            return gpa;
        }

        public List<Student> GetStudentsInCourse(int courseId)
        {
            return _dataAccess.GetStudentsInCourse(courseId);
        }
    }
}