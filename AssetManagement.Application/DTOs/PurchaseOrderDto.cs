namespace AssetManagement.Application.DTOs
{
    public class PurchaseOrderDto
    {
        public int Id { get; set; }
        public string PR_ID { get; set; } = string.Empty;
        public string RequesterName { get; set; } = string.Empty;
        public string ProcessName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string? ITCategory { get; set; }
        public string AssetName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime OrderDateTime { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; } = string.Empty;
        public int? UpdatedByUserId { get; set; }
        public string? UpdatedByUserName { get; set; }
    }

    public class CreatePurchaseOrderDto
    {
        public string PR_ID { get; set; } = string.Empty;
        public string RequesterName { get; set; } = string.Empty;
        public string ProcessName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string? ITCategory { get; set; }
        public string AssetName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime? OrderDateTime { get; set; }
    }

    public class UpdatePurchaseOrderDto
    {
        public string RequesterName { get; set; } = string.Empty;
        public string ProcessName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string? ITCategory { get; set; }
        public string AssetName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime? OrderDateTime { get; set; }
    }

    public class PurchaseOrderStatusDto
    {
        public string Status { get; set; } = string.Empty;
    }
}