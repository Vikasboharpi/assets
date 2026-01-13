using AssetManagement.Domain.Entities;

namespace AssetManagement.Domain.Interfaces
{
    public interface IPurchaseOrderRepository
    {
        Task<IEnumerable<PurchaseOrder>> GetAllAsync();
        Task<PurchaseOrder?> GetByIdAsync(int id);
        Task<PurchaseOrder?> GetByPRIdAsync(string prId);
        Task<IEnumerable<PurchaseOrder>> GetByStatusAsync(string status);
        Task<IEnumerable<PurchaseOrder>> GetByRequesterAsync(string requesterName);
        Task<IEnumerable<PurchaseOrder>> GetByCategoryAsync(string category);
        Task<IEnumerable<PurchaseOrder>> GetByLocationAsync(string location);
        Task<IEnumerable<PurchaseOrder>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<PurchaseOrder> CreateAsync(PurchaseOrder purchaseOrder);
        Task<PurchaseOrder> UpdateAsync(PurchaseOrder purchaseOrder);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> PRIdExistsAsync(string prId);
    }
}