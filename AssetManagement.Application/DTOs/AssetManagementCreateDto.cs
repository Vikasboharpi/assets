using AssetManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Application.DTOs
{
    public class AssetManagementCreateDto
    {
        public string Category { get; set; } = null!;
        public string AssetName { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string SerialNumber { get; set; } = null!;
        public string Location { get; set; } = null!;

        public string RAM { get; set; } = null!;
        public string Storage { get; set; } = null!;
        public string Processor { get; set; } = null!;
        public int StorageSize { get; set; }
        public string DepartmentType { get; set; } = null!;
        public string? Remarks { get; set; }
    }


    public class AssetManagementUpdateDto
    {
        public string Category { get; set; } = null!;
        public string AssetName { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string RAM { get; set; } = null!;
        public string Storage { get; set; } = null!;
        public string Processor { get; set; } = null!;
        public int StorageSize { get; set; }
        public string? Remarks { get; set; }
        public string DepartmentType { get; set; } = null!;
    }

    public class AssetManagementDto
    {
        public int Id { get; set; }
        public string Category { get; set; } = null!;
        public string AssetName { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string SerialNumber { get; set; } = null!;
        public string Location { get; set; } = null!;
        public AssetManagementStatus Status { get; set; }
        public string RAM { get; set; } = null!;
        public string Storage { get; set; } = null!;
        public string Processor { get; set; } = null!;
        public int StorageSize { get; set; }
        public string? Remarks { get; set; }
        public string DepartmentType { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
