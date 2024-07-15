using System;
using System.Collections.Generic;

namespace employeeSample.API.Models;

public partial class Employeemaster
{
    public int EmployeeId { get; set; }

    public string Gid { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsActive { get; set; }

    public int? ManagerId { get; set; }

    public int EmpCreatedBy { get; set; }

    public DateTime EmpCreatedOn { get; set; }

    public virtual ICollection<AssetAllocation> AssetAllocationAllocatedByNavigations { get; set; } = new List<AssetAllocation>();

    public virtual ICollection<AssetAllocation> AssetAllocationEmployees { get; set; } = new List<AssetAllocation>();

    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();

    public virtual Employeemaster EmpCreatedByNavigation { get; set; } = null!;

    public virtual ICollection<EmployeeRole> EmployeeRoles { get; set; } = new List<EmployeeRole>();

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<Employeemaster> InverseEmpCreatedByNavigation { get; set; } = new List<Employeemaster>();

    public virtual ICollection<Employeemaster> InverseManager { get; set; } = new List<Employeemaster>();

    public virtual Employeemaster? Manager { get; set; }

    public virtual ICollection<Request> RequestApprovedByNavigations { get; set; } = new List<Request>();

    public virtual ICollection<Request> RequestReqCreatedByNavigations { get; set; } = new List<Request>();
}
