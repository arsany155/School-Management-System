using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.Interfaces;

namespace SchoolProject.Infrastructure.Repositories
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        private readonly ApplicationDBContext _context;
        public SubjectRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
