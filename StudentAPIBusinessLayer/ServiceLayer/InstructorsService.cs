using StudentAPIDataAccessLayer.DataAccess;
using StudentAPIDataAccessLayer.Entities;

namespace StudentAPIBusinessLayer.ServiceLayer
{
    public class InstructorsService
    {
        private readonly InstructorsDataAccess _dataAccess;
        private readonly DepartmentDataAccess _departmentDAL;

        public InstructorsService(
            InstructorsDataAccess dataAccess,
            DepartmentDataAccess departmentDAL)
        {
            _dataAccess = dataAccess;
            _departmentDAL = departmentDAL;
        }

        public List<Instructor> GetAllInstructors()
        {
            return _dataAccess.GetAllInstructors();
        }

        public Instructor? GetInstructorById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            return _dataAccess.GetInstructorById(id);
        }

        public Instructor? AddInstructor(Instructor instructor)
        {
            if (!_departmentDAL.DepartmentExists(instructor.DepartmentId))
                return null;

            return _dataAccess.AddInstructor(instructor);
        }

        public Instructor UpdateInstructor(int id, Instructor instructor)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            return _dataAccess.UpdateInstructor(id, instructor);
        }

        public bool DeleteInstructor(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            return _dataAccess.DeleteInstructor(id);
        }

        public bool InstructorExists(int id)
        {
            return _dataAccess.InstructorExists(id);
        }

        public List<Course> GetInstructorCourses(int id)
        {
            return _dataAccess.GetInstructorCourses(id);
        }
    }
}