namespace SchoolProject.Services.Interfaces
{
    public interface IAuthorizationRoleServices
    {
        public Task<string> AddRole(string RoleName);
        public Task<bool> IsRoleExist(string RoleName);
    }
}
