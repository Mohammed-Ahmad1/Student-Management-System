using StudentAPIDataAccessLayer.DataAccess;
using StudentAPIDataAccessLayer.DTOs;
using StudentAPIDataAccessLayer.Entities;
namespace StudentAPIBusinessLayer.ServiceLayer
{
    public class CourseService
    {
        private readonly CoursesDataAccess _dataAccess;

        public CourseService(CoursesDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<Course> GetAllCourses()
        {
            return _dataAccess.GetAllCourses();
        }

        public Course GetCourseById(int id)
        {
            var Course = _dataAccess.GetCourseByID(id);

            if (Course == null)
                throw new KeyNotFoundException("Course not found");

            return Course;
        }


        public List<Course>GetCoursesWithCredits(int credits)
        {
            if (credits <= 0)
                throw new Exception("Credits Must to be grater than 0");

            return _dataAccess.GetCoursesWithCredits(credits);
        }

        public Course AddCourse(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            if (course.Credits != 1 && course.Credits != 3 && course.Credits != 6)
                throw new ArgumentException("Credits must be 1, 3, or 6");

            if (string.IsNullOrWhiteSpace(course.Code))
                throw new ArgumentException("Course code is required");

            if (string.IsNullOrWhiteSpace(course.Title))
                throw new ArgumentException("Course title is required");

            return _dataAccess.AddCourse(course);
        }

        public Course UpdateCourse(int id, Course course)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            if (course == null)
                throw new ArgumentNullException(nameof(course));

            if (course.Credits != 1 && course.Credits != 3 && course.Credits != 6)
                throw new ArgumentException("Credits must be 1, 3, or 6");

            if (string.IsNullOrWhiteSpace(course.Code))
                throw new ArgumentException("Course code is required");

            if (string.IsNullOrWhiteSpace(course.Title))
                throw new ArgumentException("Course title is required");

            var updated = _dataAccess.UpdateCourse(id, course);
            if (updated == null)
                throw new KeyNotFoundException($"Course with ID {id} not found");

            return updated;
        }

        public bool DeleteCourse(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            return _dataAccess.DeleteCourse(id);
        }
    }
}
