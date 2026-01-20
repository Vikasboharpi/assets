using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Application.DTOs
{
    public class CreateVendorDto
    {
        public string VendorName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string GSTNumber { get; set; } = null!;
        public string PANNumber { get; set; } = null!;
        public string ContactPerson { get; set; } = null!;
    }

    public class UpdateVendorDto : CreateVendorDto
    {
        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }
    }

    public class VendorResponseDto
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }
    }
}
