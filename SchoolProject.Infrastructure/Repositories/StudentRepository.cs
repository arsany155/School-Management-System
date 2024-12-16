using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Repositories
{
    public class StudentRepository : GenericRepository<Student> ,  IStudentRepository
    {
        private readonly ApplicationDBContext _context;

        public StudentRepository(ApplicationDBContext context):base(context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
           return await _context.Students.Include(d => d.Department).ToListAsync();
        }
    }
}
