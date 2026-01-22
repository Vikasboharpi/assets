using AssetManagement.Domain.Entities;
using AssetManagement.Domain.Interfaces;
using AssetManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Infrastructure.Repositories
{
    public class AssetManagementRepository : IAssetManagementRepository
    {
        private readonly AppDbContext _context;

        public AssetManagementRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsBySerialAsync(string serialNumber)
            => await _context.Assetmanagement.AnyAsync(x => x.SerialNumber == serialNumber);

        public async Task AddAsync(Assetmanagement asset)
        {
            _context.Assetmanagement.Add(asset);
            await _context.SaveChangesAsync();
        }

        public async Task<Assetmanagement?> GetByIdAsync(int id)
            => await _context.Assetmanagement.FindAsync(id);

        public async Task<List<Assetmanagement>> GetAllAsync()
            => await _context.Assetmanagement.AsNoTracking().ToListAsync();

        public async Task UpdateAsync(Assetmanagement asset)
        {
            _context.Assetmanagement.Update(asset);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Assetmanagement asset)
        {
            _context.Assetmanagement.Remove(asset);
            await _context.SaveChangesAsync();
        }


    }
}