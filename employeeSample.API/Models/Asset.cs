using System;
using System.Collections.Generic;

namespace employeeSample.API.Models;

public partial class Asset
{
    public int AssetId { get; set; }

    public string SerialNumber { get; set; } = null!;

    public int InventoryId { get; set; }

    public DateTime WarrantyStartDate { get; set; }

    public DateTime WarrantyEndDate { get; set; }

    public bool isAvailable { get; set; } 

    public int AssetCreatedBy { get; set; }

    public DateTime AssetCreatedOn { get; set; }

    public virtual ICollection<AssetAllocation> AssetAllocations { get; set; } = new List<AssetAllocation>();

    public virtual Employeemaster AssetCreatedByNavigation { get; set; } = null!;

    public virtual Inventory Inventory { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
