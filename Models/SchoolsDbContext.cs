using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Models
{
    public class SchoolsDbContext : DbContext
    {
        public SchoolsDbContext()
        {
        }

        public SchoolsDbContext(DbContextOptions<SchoolsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<Enrollment> Enrollments { get; set; }

        public virtual DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=localhost;Database=CodeFirstDb;Trusted_Connection=True;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CourseId).HasName("PK_dbo.Course");

                entity.ToTable("Course");

                entity.Property(e => e.CourseId)
                    .ValueGeneratedNever()
                    .HasColumnName("CourseId");
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.EnrollmentId).HasName("PK_dbo.Enrollment");

                entity.ToTable("Enrollment");

                entity.HasIndex(e => e.CourseId, "IX_CourseId");

                entity.HasIndex(e => e.StudentId, "IX_StudentId");

                entity.Property(e => e.EnrollmentId).HasColumnName("EnrollmentId");
                entity.Property(e => e.CourseId).HasColumnName("CourseId");
                entity.Property(e => e.StudentId).HasColumnName("StudentId");

                entity.HasOne(d => d.Course).WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_dbo.Enrollment_dbo.Course_CourseId");

                entity.HasOne(d => d.Student).WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_dbo.Enrollment_dbo.Student_StudentId");
            });

  

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StudentId).HasName("PK_dbo.Student");

                entity.ToTable("Student");

                entity.Property(e => e.StudentId).HasColumnName("StudentId");
                entity.Property(e => e.EnrollmentDate).HasColumnType("datetime");
            });
        }

    }
}
