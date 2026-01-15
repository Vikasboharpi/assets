using System.ComponentModel.DataAnnotations;
using AssetManagement.Domain.Entities;

namespace AssetManagement.Application.DTOs
{
    public class AssetRegistrationDto
    {
        [Required(ErrorMessage = "Asset name is required")]
        [StringLength(100, ErrorMessage = "Asset name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
        public string? Category { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Brand name cannot exceed 100 characters")]
        public string? Brand { get; set; } = string.Empty;

        [Required(ErrorMessage = "Serial number is required")]
        [StringLength(100, ErrorMessage = "Serial number cannot exceed 100 characters")]
        public string SerialNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Location is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid location")]
        public string? Location { get; set; }

        [StringLength(500, ErrorMessage = "Remarks cannot exceed 500 characters")]
        public string? Remarks { get; set; }

        [StringLength(500, ErrorMessage = "Ram cannot exceed 500 characters")]
        public string? Ram { get; set; }
        [StringLength(500, ErrorMessage = "Storage cannot exceed 500 characters")]
        public string? Storage { get; set; }

        [StringLength(500, ErrorMessage = "OperatingSystem cannot exceed 500 characters")]
        public string? OperatingSystem { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please fill a valid GB")]
        public int? SizeInGB { get; set; }
    }

    public class AssetResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string? Remarks { get; set; }
        public AssetStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public string? Ram { get; set; }
        public string? Brand { get; set; }
        public string? Location { get; set; }

        // Related entities
        public string? Category { get; set; } = null!;
    }

    public class AssetUpdateDto
    {
        [Required(ErrorMessage = "Asset name is required")]
        [StringLength(100, ErrorMessage = "Asset name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required")]
        [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
        public string? Category { get; set; }

        [Required(ErrorMessage = "Brand is required")]
        [StringLength(100, ErrorMessage = "Brand name cannot exceed 100 characters")]
        public string? Brand { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid location")]
        public string Location { get; set; }

        [StringLength(500, ErrorMessage = "Remarks cannot exceed 500 characters")]
        public string? Remarks { get; set; }

        public AssetStatus Status { get; set; }
    }

    public class AssetAssignmentDto
    {
        [Required(ErrorMessage = "User ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid user")]
        public int UserId { get; set; }

        [StringLength(500, ErrorMessage = "Assignment remarks cannot exceed 500 characters")]
        public string? AssignmentRemarks { get; set; }
    }
}