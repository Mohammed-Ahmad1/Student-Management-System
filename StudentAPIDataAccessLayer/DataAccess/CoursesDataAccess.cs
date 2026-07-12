using Microsoft.EntityFrameworkCore;
using StudentAPIDataAccessLayer.DTOs;
using StudentAPIDataAccessLayer.Entities;
using Students_Management_System.Data_Access_Layer.Data;


namespace StudentAPIDataAccessLayer.DataAccess
{
    public class CoursesDataAccess
    {
        private readonly AppDbContext _context;
        public CoursesDataAccess(AppDbContext context)
        {
            _context = context;
        }

        public List<Course> GetAllCourses()
        {
            return _context.Courses
                .AsNoTracking()
                .ToList();
        }


        public Course? GetCourseByID(int id)
        {
            return _context.Courses
                .FirstOrDefault(s => s.Id == id);
        }


        public List<Course> GetCoursesWithCredits(int credits)
        {
            return _context.Courses.Where(c => c.Credits == credits).ToList();
        }

        public Course AddCourse(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
            return course;
        }

        public Course? UpdateCourse(int id, Course updated)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == id);
            if (course == null)
                return null;

            course.Code = updated.Code;
            course.Title = updated.Title;
            course.Credits = updated.Credits;
            course.Description = updated.Description;

            _context.SaveChanges();
            return course;
        }

        public bool DeleteCourse(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == id);
            if (course == null)
                return false;

            _context.Courses.Remove(course);
            return _context.SaveChanges() > 0;
        }

        public bool CourseExists(int id)
        {
            return _context.Courses.Any(c => c.Id == id);
        }
    }
}


