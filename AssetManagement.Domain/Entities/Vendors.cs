using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Domain.Entities
{
    public class Vendors
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string GSTNumber { get; set; } = null!;
        public string PANNumber { get; set; } = null!;
        public bool IsActive { get; set; }
        public string ContactPerson { get; set; } = null!;
        public bool IsVerified { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
