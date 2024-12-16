using SchoolProject.Data.Entities;
using SchoolProject.Data.Helper;

namespace SchoolProject.Services.Interfaces
{
    public interface IAuthenticationServices
    {
        public Task<JwtAuthResult> GetJWTToken(User user);
        public Task<JwtAuthResult> GetRefreshToken(string accessToken, string RefreshToken);
    }
}
