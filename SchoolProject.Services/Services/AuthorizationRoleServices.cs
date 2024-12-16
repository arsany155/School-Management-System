using Microsoft.AspNetCore.Identity;
using SchoolProject.Services.Interfaces;

namespace SchoolProject.Services.Services
{
    public class AuthorizationRoleServices : IAuthorizationRoleServices
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthorizationRoleServices(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<string> AddRole(string RoleName)
        {
            var identityRole = new IdentityRole();
            identityRole.Name = RoleName;

            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
            {
                return "Success";
            }
            return "Failed";
        }

        public async Task<bool> IsRoleExist(string RoleName)
        {
            var role = await _roleManager.FindByNameAsync(RoleName);
            if (role == null)
            {
                return true;
            }
            return false;
        }
    }
}
