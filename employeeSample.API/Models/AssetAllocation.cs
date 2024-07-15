using System;
using System.Collections.Generic;

namespace employeeSample.API.Models;

public partial class AssetAllocation
{
    public int AllocationId { get; set; }

    public int AssetId { get; set; }

    public int EmployeeId { get; set; }

    public DateTime AllocatedOn { get; set; }

    public bool isActive { get; set; }

    public int AllocatedBy { get; set; }

    public virtual Employeemaster AllocatedByNavigation { get; set; } = null!;

    public virtual Asset Asset { get; set; } = null!;

    public virtual Employeemaster Employee { get; set; } = null!;
}
