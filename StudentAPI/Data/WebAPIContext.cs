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

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=2023WebAPI;Persist Security Info=True;User ID=sa;pwd=1qaz@wsx;MultipleActiveResultSets=true ;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.ToTable("Class");

            entity.Property(e => e.ClassName).HasMaxLength(25);
        });

        modelBuilder.Entity<CourseD>(entity =>
        {
            entity.HasKey(e => e.RowId).HasName("PK_Course_D_1");

            entity.ToTable("Course_D");

            entity.HasIndex(e => e.CourseId, "IX_Course_D");
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
