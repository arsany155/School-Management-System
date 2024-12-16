using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Helper;
using SchoolProject.Infrastructure.Interfaces;
using SchoolProject.Services.IServices;

namespace SchoolProject.Services.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly IStudentRepository _studentRepository;

        public StudentServices(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<string> AddStudent(Student student)
        {
            var studentResult = _studentRepository.GetTableNoTracking().Where(x => x.NameAr.Equals(student.NameAr)).FirstOrDefault();
            if (studentResult != null) return "Exist";
            await _studentRepository.AddAsync(student);
            return "Success";
        }

        public async Task<string> DeleteStudent(Student student)
        {
            var trans = _studentRepository.BeginTransaction();
            try
            {
                await _studentRepository.DeleteAsync(student);
                await trans.CommitAsync();
                return "Success";
            }
            catch
            {
                trans.Rollback();
                return "Falied";
            }
        }

        public IQueryable<Student> FilterStudentPaginatedQuery(StudentOrderingEnum order, string search)
        {
            var querable = _studentRepository.GetTableNoTracking().Include(d => d.Department).AsQueryable();
            if (search != null)
            {
                querable = querable.Where(x => x.NameAr.Contains(search) || x.Address.Contains(search));
            }
            switch (order)
            {
                case StudentOrderingEnum.StudID:
                    querable = querable.OrderBy(x => x.StudID);
                    break;

                case StudentOrderingEnum.Name:
                    querable = querable.OrderBy(x => x.NameAr);
                    break;

                case StudentOrderingEnum.Address:
                    querable = querable.OrderBy(x => x.Address);
                    break;

                case StudentOrderingEnum.DepartmentName:
                    querable = querable.OrderBy(x => x.Department.DNameAr);
                    break;


            }
            return querable;
        }

        public IQueryable<Student> GetAllStudents()
        {
            return _studentRepository.GetTableNoTracking().Include(d => d.Department).AsQueryable();
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _studentRepository.GetStudentsAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            // return await _studentRepository.GetByIdAsync(id); ===> This to all info about student 

            var studentinfo = _studentRepository.GetTableNoTracking()
                                                .Include(x => x.Department)
                                                .Where(x => x.StudID == id)
                                                .FirstOrDefault();

            return studentinfo;
        }

        public async Task<Student> GetStudentByIdWithoutDepartmentAsync(int id)
        {
            var studentinfo = await _studentRepository.GetByIdAsync(id);

            return studentinfo;
        }

        public async Task<bool> IsNameArExist(string name)
        {
            var studentResult = _studentRepository.GetTableNoTracking().Where(x => x.NameAr.Equals(name)).FirstOrDefault();
            if (studentResult == null) return false;
            return true;
        }
        public async Task<bool> IsNameEnExist(string name)
        {
            var studentResult = _studentRepository.GetTableNoTracking().Where(x => x.NameEn.Equals(name)).FirstOrDefault();
            if (studentResult == null) return false;
            return true;
        }

        public async Task<bool> IsNameEnExistExcludeSelf(string name, int id)
        {
            var studentResult = await _studentRepository.GetTableNoTracking().Where(x => x.NameEn.Equals(name) & !x.StudID.Equals(id)).FirstOrDefaultAsync();
            if (studentResult == null) return false;
            return true;
        }
        public async Task<bool> IsNameArExistExcludeSelf(string name, int id)
        {
            var studentResult = await _studentRepository.GetTableNoTracking().Where(x => x.NameAr.Equals(name) & !x.StudID.Equals(id)).FirstOrDefaultAsync();
            if (studentResult == null) return false;
            return true;
        }

        public async Task<string> UpdateStudent(Student student)
        {
            await _studentRepository.UpdateAsync(student);
            return "Success";
        }

        public IQueryable<Student> GetAllStudentsByDepartmentId(int DID)
        {
            return _studentRepository.GetTableNoTracking().Where(x => x.DID.Equals(DID)).AsQueryable();
        }
    }
}
