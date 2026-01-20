using AssetManagement.Application.DTOs;

namespace AssetManagement.Application.Interfaces
{
    public interface IPurchaseOrderService
    {
        Task<IEnumerable<PurchaseOrderDto>> GetAllPurchaseOrdersAsync();
        Task<PurchaseOrderDto?> GetPurchaseOrderByIdAsync(int id);
        Task<CreatePurchaseOrderDto?> UpdatePurchaseOrderForApproved(int id);
        Task<PurchaseOrderDto?> GetPurchaseOrderByPRIdAsync(string prId);
        Task<IEnumerable<PurchaseOrderDto>> GetPurchaseOrdersByStatusAsync(string status);
        Task<IEnumerable<PurchaseOrderDto>> GetPurchaseOrdersByRequesterAsync(string requesterName);
        Task<IEnumerable<PurchaseOrderDto>> GetPurchaseOrdersByCategoryAsync(string category);
        Task<IEnumerable<PurchaseOrderDto>> GetPurchaseOrdersByLocationAsync(string location);
        Task<IEnumerable<PurchaseOrderDto>> GetPurchaseOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<PurchaseOrderDto> CreatePurchaseOrderAsync(CreatePurchaseOrderDto createDto, int createdByUserId);
        Task<PurchaseOrderDto> UpdatePurchaseOrderAsync(int id, UpdatePurchaseOrderDto updateDto, int updatedByUserId);
        Task<bool> UpdatePurchaseOrderStatusAsync(int id, string status, int updatedByUserId);
        Task<bool> DeletePurchaseOrderAsync(int id);
        Task<bool> PurchaseOrderExistsAsync(int id);
        Task<bool> PRIdExistsAsync(string prId);
        Task<IEnumerable<string>> GetAvailableStatusesAsync();
    }
}