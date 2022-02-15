using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SchoolDemo.Models
{
    public class StudentContext: IdentityDbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>().HasKey(e=>e.Id);
            modelBuilder.Entity<Department>().HasKey(e => e.DepartmentId);
            modelBuilder.Entity<Sem>().HasKey(e => e.SemId);    
            modelBuilder.Entity<Student>()
                .HasOne<Department>(s => s.Department)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.DepartmentId);
            modelBuilder.Entity<Student>()
                .HasOne<Sem>(s => s.sem)
                .WithMany(x => x.Students)
                .HasForeignKey(s => s.SemId);
            modelBuilder.Entity<Student>()
                .Property(s => s.Fname)
                .IsRequired();
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Sem> sems { get; set; }
        
        public DbSet<UserModel> User { get; set; }    
    }
}
