using StudentAPIDataAccessLayer.Entities;
using Students_Management_System.Data_Access_Layer.Data;
using Microsoft.EntityFrameworkCore;

namespace StudentAPIDataAccessLayer.DataAccess
{
    public class DepartmentDataAccess
    {
        private readonly AppDbContext _context;

        public DepartmentDataAccess(AppDbContext context)
        {
            _context = context;
        }

        public List<Department> GetAllDepartments()
        {
            return _context.Departments
                .AsNoTracking()
                .ToList();
        }

        public Department? GetDepartmentById(int id)
        {
            if (id <= 0)
                return null;

            return _context.Departments
                .FirstOrDefault(d => d.Id == id);
        }

        public bool DepartmentExists(int id)
        {
            return _context.Departments
                .Any(d => d.Id == id);
        }

        public Department AddDepartment(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
            return department;
        }

        public Department UpdateDepartment(int id, Department updated)
        {
            var dept = _context.Departments.FirstOrDefault(d => d.Id == id);

            dept.Name = updated.Name;
            dept.Description = updated.Description;

            _context.SaveChanges();

            return dept;
        }

        public bool DeleteDepartment(int id)
        {
            var dept = _context.Departments.FirstOrDefault(d => d.Id == id);

            if (dept == null)
                return false;

            _context.Departments.Remove(dept);

            return _context.SaveChanges() > 0;
        }
        public List<Department> GetDepartmentsByCourseID(int CourseID)
        {
            return _context.Departments
                .Where(d => d.CourseDepartments.Any(cd => cd.CourseId == CourseID))
                .Select(d => new Department
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description
                })
                .ToList();
        }
    }
}