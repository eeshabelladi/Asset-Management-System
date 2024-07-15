using System.ComponentModel.DataAnnotations;

namespace employeeSample.API.Models
{
    public class AssetDto
    {
        public int AssetId { get; set; }

        public string SerialNumber { get; set; }

        public int InventoryId { get; set; }

        public DateTime WarrantyStartDate { get; set; }

        public DateTime WarrantyEndDate { get; set; }

        public bool isAvailable { get; set; }

        public int AssetCreatedBy { get; set; }

        public DateTime AssetCreatedOn { get; set; }
    }
}
