using AssetManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Application.Interfaces
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
