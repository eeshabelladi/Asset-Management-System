using System;
using System.Collections.Generic;

namespace employeeSample.API.Models;

public partial class Request
{
    public int RequestId { get; set; }

    public int ReqCreatedBy { get; set; }

    public int? AssetId { get; set; }

    public DateTime ReqCreatedOn { get; set; }

    public string ReqStatus { get; set; }

    public DateTime? ApprovedOn { get; set; }

    public string RequestType { get; set; } = null!;

    public int? ApprovedBy { get; set; }

    public string? Reason { get; set; }
    public string? ReqAssetType { get; set; }

    public virtual Employeemaster? ApprovedByNavigation { get; set; }

    public virtual Asset? Asset { get; set; } = null!;

    public virtual Employeemaster ReqCreatedByNavigation { get; set; } = null!;
}
