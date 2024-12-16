using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Services.Interfaces;
using SchoolProject.Services.IServices;
using SchoolProject.Services.Services;

namespace SchoolProject.Services
{
    public static class ServicesDependencies
    {
        public static IServiceCollection AddServicesDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentServices, StudentServices>();
            services.AddTransient<IDepartmentServices, DepartmentServices>();
            services.AddTransient<IAuthenticationServices, Authentication>();
            services.AddTransient<IAuthorizationRoleServices, AuthorizationRoleServices>();

            return services;
        }
    }
}
