using SchoolProject.Data.Entities;

namespace SchoolProject.Services.Interfaces
{
    public interface IDepartmentServices
    {
        public Task<Department> GetDepartmentByIdAsync(int id);
        public Task<bool> IsDepartmentIdExist(int departmentid);
    }
}
