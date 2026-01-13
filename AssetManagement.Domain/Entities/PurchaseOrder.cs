using System.ComponentModel.DataAnnotations;

namespace AssetManagement.Domain.Entities
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string PR_ID { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string RequesterName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string ProcessName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Category { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string? ITCategory { get; set; }
        
        [Required]
        [StringLength(100)]
        public string AssetName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Location { get; set; } = string.Empty;
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected, Completed
        
        public DateTime OrderDateTime { get; set; } = DateTime.UtcNow;
        
        // Audit fields
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public int CreatedByUserId { get; set; }
        public Users CreatedByUser { get; set; } = null!;
        
        public int? UpdatedByUserId { get; set; }
        public Users? UpdatedByUser { get; set; }
    }
}