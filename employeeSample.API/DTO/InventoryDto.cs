namespace employeeSample.API.DTO
{
    public class InventoryDto
    {
        public int InventoryId { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int Quantity { get; set; }

        public string AssetType { get; set; }

        public int InvCreatedBy { get; set; }

        public DateTime InvCreatedOn { get; set; }

    }
}
