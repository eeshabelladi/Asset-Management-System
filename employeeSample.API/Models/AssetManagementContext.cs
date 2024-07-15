using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace employeeSample.API.Models;

public partial class AssetManagementContext : DbContext
{
    public AssetManagementContext()
    {
    }

    public AssetManagementContext(DbContextOptions<AssetManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asset> Assets { get; set; }

    public virtual DbSet<AssetAllocation> AssetAllocations { get; set; }

    public virtual DbSet<EmployeeRole> EmployeeRoles { get; set; }

    public virtual DbSet<Employeemaster> Employeemasters { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=DESKTOP-RF8P68D\\MSSQLSERVER010;database=Asset-Management;trusted_connection=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>(entity =>
        {
            entity.HasKey(e => e.AssetId).HasName("PK__ASSETS__43492372F02EB0B6");

            entity.ToTable("ASSETS");

            entity.HasIndex(e => e.InventoryId, "IX_Assets_InventoryID");

            entity.Property(e => e.AssetId).HasColumnName("AssetID");
            entity.Property(e => e.AssetCreatedOn).HasPrecision(0);
            entity.Property(e => e.isAvailable).HasColumnType("bit");
            entity.Property(e => e.InventoryId).HasColumnName("InventoryID");
            entity.Property(e => e.SerialNumber).HasMaxLength(255);
            entity.Property(e => e.WarrantyEndDate).HasColumnType("date");
            entity.Property(e => e.WarrantyStartDate).HasColumnType("date");

            entity.Property(e => e.isAvailable).HasColumnName("isAvailable");

            entity.HasOne(d => d.AssetCreatedByNavigation).WithMany(p => p.Assets)
                .HasForeignKey(d => d.AssetCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ASSETS__AssetCre__57DD0BE4");

            entity.HasOne(d => d.Inventory).WithMany(p => p.Assets)
                .HasForeignKey(d => d.InventoryId)
                .HasConstraintName("FK__ASSETS__Inventor__56E8E7AB");
        });

        modelBuilder.Entity<AssetAllocation>(entity =>
        {
            entity.HasKey(e => e.AllocationId).HasName("PK__ASSET_AL__B3C6D6AB3ABEDFF2");

            entity.ToTable("ASSET_ALLOCATION");

            entity.Property(e => e.AllocationId).HasColumnName("AllocationID");
            entity.Property(e => e.AllocatedOn).HasPrecision(0);
            entity.Property(e => e.AssetId).HasColumnName("AssetID");
            entity.Property(e => e.isActive).HasColumnType("bit");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

            entity.Property(e => e.isActive).HasColumnName("isActive");

            entity.HasOne(d => d.AllocatedByNavigation).WithMany(p => p.AssetAllocationAllocatedByNavigations)
                .HasForeignKey(d => d.AllocatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ASSET_ALL__Alloc__6166761E");

            entity.HasOne(d => d.Asset).WithMany(p => p.AssetAllocations)
                .HasForeignKey(d => d.AssetId)
                .HasConstraintName("FK__ASSET_ALL__Asset__5F7E2DAC");

            entity.HasOne(d => d.Employee).WithMany(p => p.AssetAllocationEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__ASSET_ALL__Emplo__607251E5");
        });

        modelBuilder.Entity<EmployeeRole>(entity =>
        {
            entity.HasKey(e => e.EmpRoleId).HasName("PK__EMPLOYEE__6C99F20F655ABA4E");

            entity.ToTable("EMPLOYEE_ROLE");

            entity.Property(e => e.EmpRoleId).HasColumnName("EmpRoleID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeRoles)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__EMPLOYEE___Emplo__662B2B3B");

            entity.HasOne(d => d.Role).WithMany(p => p.EmployeeRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__EMPLOYEE___RoleI__671F4F74");
        });

        modelBuilder.Entity<Employeemaster>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__EMPLOYEE__7AD04FF14AA32A8B");

            entity.ToTable("EMPLOYEEMASTER");

            entity.HasIndex(e => e.ManagerId, "IX_EmployeeMaster_ManagerID");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.EmpCreatedOn).HasPrecision(0);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Gid).HasColumnType("nvarchar(255)");
            entity.Property(e => e.ManagerId).HasColumnName("ManagerID");
            entity.Property(e => e.Password).HasMaxLength(64);

            entity.HasOne(d => d.EmpCreatedByNavigation).WithMany(p => p.InverseEmpCreatedByNavigation)
                .HasForeignKey(d => d.EmpCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EMPLOYEEM__EmpCr__51300E55");

            entity.HasOne(d => d.Manager).WithMany(p => p.InverseManager)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__EMPLOYEEM__Manag__503BEA1C");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PK__INVENTOR__F5FDE6D3E6CB5E7C");

            entity.ToTable("INVENTORIES");

            entity.Property(e => e.InventoryId).HasColumnName("InventoryID");
            entity.Property(e => e.AssetType).HasMaxLength(255);
            entity.Property(e => e.Brand).HasMaxLength(255);
            entity.Property(e => e.InvCreatedOn).HasPrecision(0);
            entity.Property(e => e.Model).HasMaxLength(255);

            entity.HasOne(d => d.InvCreatedByNavigation).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.InvCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__INVENTORI__InvCr__540C7B00");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__REQUESTS__33A8519A45FE9623");

            entity.ToTable("REQUESTS");

            entity.HasIndex(e => e.AssetId, "IX_Requests_AssetID");

            entity.HasIndex(e => e.ReqCreatedBy, "IX_Requests_ReqCreatedBy");

            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.ApprovedOn).HasPrecision(0);
            entity.Property(e => e.AssetId).HasColumnName("AssetID");
            entity.Property(e => e.Reason).HasMaxLength(255);
            entity.Property(e => e.ReqCreatedOn).HasPrecision(0);
            entity.Property(e => e.ReqStatus).HasColumnType("nvarchar(255)");
            entity.Property(e => e.RequestType).HasMaxLength(255);
            entity.Property(e => e.ReqAssetType).HasColumnType("nvarchar(255)");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.RequestApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK__REQUESTS__Approv__5CA1C101");

            entity.HasOne(d => d.Asset).WithMany(p => p.Requests)
                .HasForeignKey(d => d.AssetId)
                .HasConstraintName("FK__REQUESTS__AssetI__5BAD9CC8");

            entity.HasOne(d => d.ReqCreatedByNavigation).WithMany(p => p.RequestReqCreatedByNavigations)
                .HasForeignKey(d => d.ReqCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__REQUESTS__ReqCre__5AB9788F");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__ROLES__8AFACE3A508A0382");

            entity.ToTable("ROLES");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
