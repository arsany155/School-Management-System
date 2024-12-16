using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Interfaces;
using SchoolProject.Services.Interfaces;

namespace SchoolProject.Services.Services
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentServices(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            var department = await _repository.GetTableNoTracking().Where(x => x.DID.Equals(id))
                                                    .Include(x => x.DepartmentSubjects).ThenInclude(x => x.Subject)
                                                    .Include(x => x.Instructors)
                                                    .Include(x => x.Instructor)
                                                    .FirstOrDefaultAsync();
            return department;
        }

        public async Task<bool> IsDepartmentIdExist(int departmentid)
        {
            return await _repository.GetTableNoTracking().AnyAsync(x => x.DID.Equals(departmentid));
        }
    }
}
