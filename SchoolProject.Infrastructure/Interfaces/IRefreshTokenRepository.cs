using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Interfaces
{
    public interface IRefreshTokenRepository : IGenericRepository<UserRefreshToken>
    {
    }
}
