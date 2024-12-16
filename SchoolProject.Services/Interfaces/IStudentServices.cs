using SchoolProject.Data.Entities;
using SchoolProject.Data.Helper;

namespace SchoolProject.Services.IServices
{
    public interface IStudentServices
    {
        public Task<List<Student>> GetAllStudentsAsync();
        public Task<Student> GetStudentByIdAsync(int id);
        public Task<Student> GetStudentByIdWithoutDepartmentAsync(int id);

        public Task<string> AddStudent(Student student);

        public Task<bool> IsNameArExist(string name);
        public Task<bool> IsNameEnExist(string name);
        public Task<bool> IsNameEnExistExcludeSelf(string name, int id);
        public Task<bool> IsNameArExistExcludeSelf(string name, int id);

        public Task<string> UpdateStudent(Student student);

        public Task<string> DeleteStudent(Student student);

        public IQueryable<Student> GetAllStudents();
        public IQueryable<Student> GetAllStudentsByDepartmentId(int DID);
        public IQueryable<Student> FilterStudentPaginatedQuery(StudentOrderingEnum order, string search);

    }
}
