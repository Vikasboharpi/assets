using AssetManagement.Application.DTOs;
using AssetManagement.Domain.Interfaces;
using AssetManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.API.Controllers
{
    [ApiController]
    [Route("api/vendors")]
    public class VendorsController : ControllerBase
    {
        private readonly IVendorRepository _repo;

        public VendorsController(IVendorRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vendors = await _repo.GetAllAsync();
            return Ok(vendors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vendor = await _repo.GetByIdAsync(id);
            if (vendor == null) return NotFound();
            return Ok(vendor);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVendorDto dto)
        {
            var vendor = new Vendors
            {
                VendorName = dto.VendorName,
                EmailAddress = dto.EmailAddress,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                GSTNumber = dto.GSTNumber,
                PANNumber = dto.PANNumber,
                ContactPerson = dto.ContactPerson,
                IsActive = true,
                IsVerified = false,
                Status = dto.Status
            };

            await _repo.AddAsync(vendor);
            return CreatedAtAction(nameof(GetById), new { id = vendor.VendorId }, vendor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateVendorDto dto)
        {
            var vendor = await _repo.GetByIdAsync(id);
            if (vendor == null) return NotFound();

            vendor.VendorName = dto.VendorName;
            vendor.IsActive = dto.IsActive;
            vendor.IsVerified = dto.IsVerified;

            await _repo.UpdateAsync(vendor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var vendor = await _repo.GetByIdAsync(id);
            if (vendor == null) return NotFound();

            await _repo.DeleteAsync(vendor);
            return NoContent();
        }
    }
}
