using System;
using System.Collections.Generic;

namespace employeeSample.API.Models;

public partial class EmployeeRole
{
    public int EmpRoleId { get; set; }

    public int EmployeeId { get; set; }

    public int RoleId { get; set; }

    public virtual Employeemaster Employee { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
