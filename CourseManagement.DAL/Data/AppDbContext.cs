using CourseManagement.DAL.Entities;
using CourseManagement.DAL.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
        base(options)
        { }
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Session> Sessions => Set<Session>();
        public DbSet<AppUser> Users => Set<AppUser>();
        public DbSet<Grade> Grades => Set<Grade>();

        public object Instructors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Course>()
            .HasIndex(c => c.Name)
            .IsUnique();
            modelBuilder.Entity<AppUser>()
            .HasIndex(u => u.Email)
            .IsUnique();
            modelBuilder.Entity<Course>()
            .HasOne(c => c.Instructor)
            .WithMany(u => u.CoursesTaught)
            .HasForeignKey(c => c.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Session>()
                .HasOne(s => s.Course)
.WithMany(c => c.Sessions)
.HasForeignKey(s => s.CourseId)
.OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Grade>()
            .HasOne(g => g.Session)
            .WithMany(s => s.Grades)
            .HasForeignKey(g => g.SessionId)
            .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Grade>()
            .HasOne(g => g.Trainee)
            .WithMany(u => u.Grades)
            .HasForeignKey(g => g.TraineeId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}