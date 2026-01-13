using AssetManagement.Domain.Entities;
using AssetManagement.Domain.Interfaces;
using AssetManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Infrastructure.Repositories
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly AppDbContext _context;

        public PurchaseOrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PurchaseOrder>> GetAllAsync()
        {
            return await _context.PurchaseOrders
                .Include(po => po.CreatedByUser)
                .Include(po => po.UpdatedByUser)
                .Where(po => po.IsActive)
                .OrderByDescending(po => po.CreatedAt)
                .ToListAsync();
        }

        public async Task<PurchaseOrder?> GetByIdAsync(int id)
        {
            return await _context.PurchaseOrders
                .Include(po => po.CreatedByUser)
                .Include(po => po.UpdatedByUser)
                .FirstOrDefaultAsync(po => po.Id == id && po.IsActive);
        }

        public async Task<PurchaseOrder?> GetByPRIdAsync(string prId)
        {
            return await _context.PurchaseOrders
                .Include(po => po.CreatedByUser)
                .Include(po => po.UpdatedByUser)
                .FirstOrDefaultAsync(po => po.PR_ID == prId && po.IsActive);
        }

        public async Task<IEnumerable<PurchaseOrder>> GetByStatusAsync(string status)
        {
            return await _context.PurchaseOrders
                .Include(po => po.CreatedByUser)
                .Include(po => po.UpdatedByUser)
                .Where(po => po.Status == status && po.IsActive)
                .OrderByDescending(po => po.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<PurchaseOrder>> GetByRequesterAsync(string requesterName)
        {
            return await _context.PurchaseOrders
                .Include(po => po.CreatedByUser)
                .Include(po => po.UpdatedByUser)
                .Where(po => po.RequesterName.Contains(requesterName) && po.IsActive)
                .OrderByDescending(po => po.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<PurchaseOrder>> GetByCategoryAsync(string category)
        {
            return await _context.PurchaseOrders
                .Include(po => po.CreatedByUser)
                .Include(po => po.UpdatedByUser)
                .Where(po => po.Category.Contains(category) && po.IsActive)
                .OrderByDescending(po => po.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<PurchaseOrder>> GetByLocationAsync(string location)
        {
            return await _context.PurchaseOrders
                .Include(po => po.CreatedByUser)
                .Include(po => po.UpdatedByUser)
                .Where(po => po.Location.Contains(location) && po.IsActive)
                .OrderByDescending(po => po.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<PurchaseOrder>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.PurchaseOrders
                .Include(po => po.Category)
                .Include(po => po.Location)
                .Include(po => po.CreatedByUser)
                .Include(po => po.UpdatedByUser)
                .Where(po => po.OrderDateTime >= startDate && po.OrderDateTime <= endDate && po.IsActive)
                .OrderByDescending(po => po.CreatedAt)
                .ToListAsync();
        }

        public async Task<PurchaseOrder> CreateAsync(PurchaseOrder purchaseOrder)
        {
            purchaseOrder.CreatedAt = DateTime.UtcNow;
            purchaseOrder.UpdatedAt = DateTime.UtcNow;
            
            _context.PurchaseOrders.Add(purchaseOrder);
            await _context.SaveChangesAsync();
            
            return await GetByIdAsync(purchaseOrder.Id) ?? purchaseOrder;
        }

        public async Task<PurchaseOrder> UpdateAsync(PurchaseOrder purchaseOrder)
        {
            purchaseOrder.UpdatedAt = DateTime.UtcNow;
            
            _context.PurchaseOrders.Update(purchaseOrder);
            await _context.SaveChangesAsync();
            
            return await GetByIdAsync(purchaseOrder.Id) ?? purchaseOrder;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var purchaseOrder = await _context.PurchaseOrders.FindAsync(id);
            if (purchaseOrder == null) return false;

            // Soft delete
            purchaseOrder.IsActive = false;
            purchaseOrder.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.PurchaseOrders
                .AnyAsync(po => po.Id == id && po.IsActive);
        }

        public async Task<bool> PRIdExistsAsync(string prId)
        {
            return await _context.PurchaseOrders
                .AnyAsync(po => po.PR_ID == prId && po.IsActive);
        }
    }
}