namespace employeeSample.API.DTO
{
    public class AssetPropertiesDto
    {
        public int AllocationId { get; set; }

        public int AssetId { get; set; }

        public int EmployeeId { get; set; }
        public int InventoryId { get; set; }
        public string SerialNumber { get; set; }
        public string Brand { get; set; }

        public string Model { get; set; }

        public string AssetType { get; set; }
    }
}
