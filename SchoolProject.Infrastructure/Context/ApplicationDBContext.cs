using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Context
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        public ApplicationDBContext()
        {

        }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentSubject> DepartmentSubjects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Ins_Subject> Ins_Subjects { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DepartmentSubject>()
                .HasKey(x => new { x.SubID, x.DID });

            modelBuilder.Entity<Ins_Subject>()
                .HasKey(x => new { x.SubjectId, x.InsId });

            modelBuilder.Entity<StudentSubject>()
                .HasKey(x => new { x.SubID, x.StudID });

            modelBuilder.Entity<Instructor>()
                .HasOne(x => x.Supervisors)
                .WithMany(x => x.Instructors)
                .HasForeignKey(x => x.SupervisorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Department>()
               .HasOne(d => d.Instructor)
               .WithOne(i => i.departmentManager)
               .HasForeignKey<Department>(d => d.InsManager)
               .OnDelete(DeleteBehavior.Restrict);



            base.OnModelCreating(modelBuilder);
        }

    }
}
