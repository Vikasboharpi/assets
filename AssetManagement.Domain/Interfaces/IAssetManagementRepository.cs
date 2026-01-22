using AssetManagement.Domain.Entities;

namespace AssetManagement.Domain.Interfaces
{
    public interface IAssetManagementRepository
    {
        Task<bool> ExistsBySerialAsync(string serialNumber);
        Task AddAsync(Assetmanagement asset);
        Task<Assetmanagement?> GetByIdAsync(int id);
        Task<List<Assetmanagement>> GetAllAsync();
        Task UpdateAsync(Assetmanagement asset);
        Task DeleteAsync(Assetmanagement asset);
 
    }
}