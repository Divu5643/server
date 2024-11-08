using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace perfomanceSystemServer.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<DepartmentMaster> DepartmentMasters { get; set; }

    public virtual DbSet<DesignationMaster> DesignationMasters { get; set; }

    public virtual DbSet<EmployeeDetail> EmployeeDetails { get; set; }

    public virtual DbSet<Goal> Goals { get; set; }

    public virtual DbSet<Performance> Performances { get; set; }

    public virtual DbSet<Reviewer> Reviewers { get; set; }

    public virtual DbSet<RoleMaster> RoleMasters { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<YearAndMonth> YearAndMonths { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=BFL-COMP-7473\\SQLEXPRESS;Database=PerformanceDB;Trusted_Connection=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__comment__C3B4DFCA76F758F7");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(CONVERT([datetime],getdate()))");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Comments).HasConstraintName("FK__comment__Created__03F0984C");

            entity.HasOne(d => d.Goal).WithMany(p => p.Comments).HasConstraintName("FK__comment__goalId__02FC7413");
        });

        modelBuilder.Entity<DepartmentMaster>(entity =>
        {
            entity.HasKey(e => e.DeptId).HasName("PK__departme__BE2D26B677ADAED9");
        });

        modelBuilder.Entity<DesignationMaster>(entity =>
        {
            entity.HasKey(e => e.DesignationId).HasName("PK__designat__197CE32A40D92C5B");
        });

        modelBuilder.Entity<EmployeeDetail>(entity =>
        {
            entity.HasKey(e => e.DetailId).HasName("PK__employee__830778596DF75773");

            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(d => d.User).WithMany(p => p.EmployeeDetails).HasConstraintName("FK__employeeD__useri__4E88ABD4");
        });

        modelBuilder.Entity<Goal>(entity =>
        {
            entity.HasKey(e => e.GoalId).HasName("PK__goal__7E225EB1C793FAF9");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(CONVERT([date],getdate()))");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.GoalCreatedByNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__goal__createdBy__3C69FB99");

            entity.HasOne(d => d.User).WithMany(p => p.GoalUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__goal__userId__3B75D760");
        });

        modelBuilder.Entity<Performance>(entity =>
        {
            entity.HasKey(e => e.PerformanceId).HasName("PK__Performa__AC2FDEE1E98520B8");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(CONVERT([date],getdate()))");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PerformanceCreatedByNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Performan__creat__48CFD27E");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.PerformanceModifiedByNavigations).HasConstraintName("FK__Performan__modif__49C3F6B7");

            entity.HasOne(d => d.User).WithMany(p => p.PerformanceUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Performan__userI__45F365D3");
        });

        modelBuilder.Entity<Reviewer>(entity =>
        {
            entity.HasKey(e => e.ReviewerId).HasName("PK__reviewer__DA03DE1EC8DFB23A");

            entity.HasOne(d => d.Employee).WithMany(p => p.ReviewerEmployees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reviewer__employ__4222D4EF");

            entity.HasOne(d => d.Manager).WithMany(p => p.ReviewerManagers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reviewer__manage__4316F928");
        });

        modelBuilder.Entity<RoleMaster>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__roleMast__CD98462ACB8B942A");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("PK__userMast__CBA1B2579BE44ECA");

            entity.Property(e => e.DeptId).HasDefaultValue(1);
            entity.Property(e => e.DesignationId).HasDefaultValue(1);
            entity.Property(e => e.RoleId).HasDefaultValue(3);

            entity.HasOne(d => d.Dept).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__deptId__2739D489");

            entity.HasOne(d => d.Designation).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__designati__29221CFB");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__userMaste__roleI__17036CC0");
        });

        modelBuilder.Entity<YearAndMonth>(entity =>
        {
            entity.HasKey(e => e.YearId).HasName("PK__YearAndM__14200DA5338F99B2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
