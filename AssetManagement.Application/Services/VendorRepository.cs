using AssetManagement.Application.Interfaces;
using AssetManagement.Domain.Entities;
using AssetManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Application.Services
{
    public class VendorRepository: IVendorRepository
    {
        private readonly AppDbContext _context;

        public VendorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vendors>> GetAllAsync()
      => await _context.Vendors.AsNoTracking().ToListAsync();

        public async Task<Vendors?> GetByIdAsync(int id)
        => await _context.Vendors.FindAsync(id);

        public async Task AddAsync(Vendors vendor)
        {
            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Vendors vendor)
        {
            _context.Vendors.Update(vendor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Vendors vendor)
        {
            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();
        }
    }
}
