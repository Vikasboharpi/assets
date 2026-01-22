using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Domain.Entities
{
    public class Assetmanagement
    {

        public int Id { get; set; }

        public string Category { get; set; } = null!;
        public string AssetName { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string SerialNumber { get; set; } = null!;
        public string Location { get; set; } = null!;

        public string RAM { get; set; } = null!;
        public string Storage { get; set; } = null!;
        public string Processor { get; set; } = null!;
        public int StorageSize { get; set; }

        public string? Remarks { get; set; }

        public AssetManagementStatus Status { get; set; } = AssetManagementStatus.Available;
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string DepartmentType { get; set; } = null!;

    }


    public enum AssetManagementStatus
    {
        Available = 1,
        Assigned = 2,
        Maintenance = 3,
        Retired = 4
    }
}
