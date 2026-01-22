using AssetManagement.Domain.Entities;

namespace AssetManagement.Domain.Interfaces
{
    public interface IVendorRepository
    {
        Task<IEnumerable<Vendors>> GetAllAsync();
        Task<Vendors?> GetByIdAsync(int id);
        Task AddAsync(Vendors vendor);
        Task UpdateAsync(Vendors vendor);
        Task DeleteAsync(Vendors vendor);
    }
}