using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        public Task<List<Student>> GetStudentsAsync();
    }
}
