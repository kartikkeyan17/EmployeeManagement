using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmployeeInfrastructure.Models;

public partial class EmployeeContext : DbContext
{
    public EmployeeContext()
    {
    }

    public EmployeeContext(DbContextOptions<EmployeeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DepartmentMaster> DepartmentMasters { get; set; }

    public virtual DbSet<DesignationMaster> DesignationMasters { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=EmployeeManagement;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DepartmentMaster>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("Pk_DepartmentId");

            entity.ToTable("DepartmentMaster");

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Department).HasMaxLength(50);
        });

        modelBuilder.Entity<DesignationMaster>(entity =>
        {
            entity.HasKey(e => e.DesignationId).HasName("Pk_DesignationId");

            entity.ToTable("DesignationMaster");

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Designation).HasMaxLength(50);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("Pk_Employee_EmployeeId");

            entity.ToTable("Employee");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.EmployeeName).HasMaxLength(50);
            entity.Property(e => e.Salary).HasColumnType("decimal(10, 0)");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_Employee_DepartmentId_DepartmentMaster_DepartmentId");

            entity.HasOne(d => d.Designation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DesignationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_Employee_DesignationId_DesignationMaster_DesignationId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
