using System;
using System.Collections.Generic;

namespace employeeSample.API.Models;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int Quantity { get; set; }

    public string AssetType { get; set; } = null!;

    public int InvCreatedBy { get; set; }

    public DateTime InvCreatedOn { get; set; }

    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();

    public virtual Employeemaster InvCreatedByNavigation { get; set; } = null!;
}
