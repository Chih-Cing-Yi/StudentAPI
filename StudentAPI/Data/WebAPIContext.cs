using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Models;

namespace StudentAPI.Data;

public partial class WebAPIContext : DbContext
{
    public WebAPIContext()
    {
    }

    public WebAPIContext(DbContextOptions<WebAPIContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<CourseD> CourseDs { get; set; }

    public virtual DbSet<CourseM> CourseMs { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.ToTable("Class");

            entity.Property(e => e.ClassName).HasMaxLength(25);
        });

        modelBuilder.Entity<CourseD>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Course_D");

            entity.HasIndex(e => e.CourseId, "IX_Course_D");
        });

        modelBuilder.Entity<CourseM>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK_course");

            entity.ToTable("Course_M");

            entity.Property(e => e.CourseName).HasMaxLength(25);
            entity.Property(e => e.Instructor).HasMaxLength(25);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Student");

            entity.Property(e => e.StudentAddress).HasMaxLength(25);
            entity.Property(e => e.StudentName).HasMaxLength(25);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserName).HasMaxLength(25);
            entity.Property(e => e.UserPassword).HasMaxLength(25);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
