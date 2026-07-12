using StudentAPIDataAccessLayer.DataAccess;
using StudentAPIDataAccessLayer.Entities;

namespace StudentAPIBusinessLayer.ServiceLayer
{
    public class DepartmentService
    {
        private readonly DepartmentDataAccess _dataAccess;

        public DepartmentService(DepartmentDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<Department> GetAllDepartments()
        {
            return _dataAccess.GetAllDepartments();
        }

        public Department? GetDepartmentById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            return _dataAccess.GetDepartmentById(id);
        }

        public Department AddDepartment(Department department)
        {
            if (string.IsNullOrWhiteSpace(department.Name))
                throw new ArgumentException("Department name is required");

            return _dataAccess.AddDepartment(department);
        }

        public Department UpdateDepartment(int id, Department department)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            var existing = _dataAccess.GetDepartmentById(id);

            if (existing == null)
                throw new Exception("Department not found");

            return _dataAccess.UpdateDepartment(id, department);
        }

        public bool DeleteDepartment(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            return _dataAccess.DeleteDepartment(id);
        }


        public List<Department> GetDepartmentsByCourseID(int id)
        {
            return _dataAccess.GetDepartmentsByCourseID(id);
        }

        public bool IsDepartmentExist(int id)
        {
            return _dataAccess.DepartmentExists(id);
        }
    }
}