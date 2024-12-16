using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SchoolProject.Core;
using SchoolProject.Core.Middlewares;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Helper;
using SchoolProject.Infrastructure;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Services;
using System.Globalization;

namespace SchoolProject.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<ApplicationDBContext>(op =>
            {
                op.UseSqlServer(builder.Configuration.GetConnectionString("dbcontext"));
            });


            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("jwtSettings"));
            builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<JwtSettings>>().Value);



            // Localization
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            builder.Services.AddInfrastructureDependencies(builder.Configuration)
                            .AddServicesDependencies()
                            .AddCoresDependencies();
            // Configure Identity
            builder.Services.AddIdentity<User, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
                config.Password.RequiredUniqueChars = 1;
                config.Password.RequireDigit = true;
                config.Password.RequireLowercase = true;
                config.Password.RequireUppercase = true;
                config.Password.RequireNonAlphanumeric = true;
                config.Password.RequiredLength = 6;
                config.SignIn.RequireConfirmedEmail = false;
                config.User.RequireUniqueEmail = true;
                config.Lockout.AllowedForNewUsers = true;
                config.Lockout.MaxFailedAccessAttempts = 3;
                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);
            })
            .AddEntityFrameworkStores<ApplicationDBContext>()
            .AddDefaultTokenProviders();

            //builder.Services.AddAutoMapper(typeof(Program));




            #region Localization
            builder.Services.AddControllersWithViews();
            builder.Services.AddLocalization(op =>
            {
                op.ResourcesPath = "";
            });
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                List<CultureInfo> supportedCultures = new List<CultureInfo>
                {
                        new CultureInfo("en-US"),
                        new CultureInfo("ar-EG")
                };

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            #endregion

            var app = builder.Build();



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //Localization
            var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);


            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
