using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.Interfaces;

namespace SchoolProject.Infrastructure.Repositories
{
    public class RefreshTokenRepository : GenericRepository<UserRefreshToken>, IRefreshTokenRepository
    {
        private readonly ApplicationDBContext _context;
        public RefreshTokenRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
