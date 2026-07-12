using Microsoft.EntityFrameworkCore;
using StudentAPIDataAccessLayer.DTOs;
using StudentAPIDataAccessLayer.Entities;
using Students_Management_System.Data_Access_Layer.Data;


namespace StudentAPIDataAccessLayer.DataAccess
{
    public class InstructorsDataAccess
    {

        private readonly AppDbContext _context;

        public InstructorsDataAccess(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        public List<Instructor> GetAllInstructors()
        {
            return _context.Instructors
                .AsNoTracking()
                .ToList();
        }

        // GET BY ID
        public Instructor? GetInstructorById(int id)
        {
            if (id <= 0)
                return null;

            return _context.Instructors
                .FirstOrDefault(i => i.Id == id);
        }

        // ADD
        public Instructor AddInstructor(Instructor instructor)
        {
            _context.Instructors.Add(instructor);
            _context.SaveChanges();

            return instructor;
        }

        // UPDATE
        public Instructor UpdateInstructor(int id, Instructor updated)
        {
            var instructor = _context.Instructors.FirstOrDefault(i => i.Id == id);

            instructor.FirstName = updated.FirstName;
            instructor.LastName = updated.LastName;
            instructor.Email = updated.Email;
            instructor.Phone = updated.Phone;
            instructor.HireDate = updated.HireDate;
            instructor.Office = updated.Office;
            instructor.Salary = updated.Salary;
            instructor.IsActive = updated.IsActive;
            instructor.DepartmentId = updated.DepartmentId;

            _context.SaveChanges();

            return instructor;
        }

        // DELETE
        public bool DeleteInstructor(int id)
        {
            var instructor = _context.Instructors.FirstOrDefault(i => i.Id == id);

            if (instructor == null)
                return false;

            _context.Instructors.Remove(instructor);

            return _context.SaveChanges() > 0;
        }

        
        public bool InstructorExists(int id)
        {
            return _context.Instructors.Any(i => i.Id == id);
        }

        public List<Course> GetInstructorCourses(int id)
        {
            return (
                from c in _context.Courses
                join ci in _context.CourseInstructors
                    on c.Id equals ci.CourseId
                where ci.InstructorId == id
                select c
            ).ToList();
        }

    }

}