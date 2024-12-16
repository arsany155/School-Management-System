using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.Interfaces;

namespace SchoolProject.Infrastructure.Repositories
{
    public class InstructorRepository : GenericRepository<Instructor>, IInstructorRepository
    {
        private readonly ApplicationDBContext _context;
        public InstructorRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
