namespace employeeSample.API.DTO
{
    public class AssetAllocationDto
    {
        public int AllocationId { get; set; }

        public int AssetId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime AllocatedOn { get; set; }

        public bool isActive { get; set; }

        public int AllocatedBy { get; set; }
    }
}
