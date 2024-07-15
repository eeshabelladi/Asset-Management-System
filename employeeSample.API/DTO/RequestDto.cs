namespace employeeSample.API.DTO
{
    public class RequestDto
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

    }
}
