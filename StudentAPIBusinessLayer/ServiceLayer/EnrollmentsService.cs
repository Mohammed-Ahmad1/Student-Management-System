using Microsoft.EntityFrameworkCore;
using StudentAPIDataAccessLayer.DataAccess;
using StudentAPIDataAccessLayer.DTOs;
using StudentAPIDataAccessLayer.Entities;

namespace StudentAPIBusinessLayer.ServiceLayer
{
    public class EnrollmentsService
    {
        private readonly EnrollmentsDataAccess _dataAccess;

        public EnrollmentsService(EnrollmentsDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<Enrollment> GetEnrollmentsPerStudent(int StudentID)
        {
            return _dataAccess.GetEnrollmentsPerStudent(StudentID);
        }

        public int GetTotalCompleteCreditPerStudent(int StudentID)
        {
            return _dataAccess.GetTotalCompleteCreditPerStudent(StudentID);
        }

        public double CalculateStudentGPA(int studentId)
        {
            return _dataAccess.CalculateStudentGPA(studentId);  
        }

        public List<CourseEnrollmentReportDto> GetNumberOfEnrolledStudents()
        {
            return _dataAccess.GetTotalNumberOfEnrolledStudentsInCourses();
        }


        public List<StudentTranscriptDto> GetStudentTranscript(int studentId)
        {
            if (studentId <= 0)
                throw new ArgumentException("Invalid student ID");

            return _dataAccess.GetStudentTranscript(studentId);
        }


    
    }

}
