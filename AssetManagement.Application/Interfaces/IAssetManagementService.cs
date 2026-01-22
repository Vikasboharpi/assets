using AssetManagement.Application.DTOs;
using AssetManagement.Domain.Entities;

namespace AssetManagement.Application.Interfaces
{
    public interface IAssetManagementService
    {
        Task<ApiResponseDto<AssetManagementDto>> CreateAsync(AssetManagementCreateDto AssetManagement, int id);
        Task<ApiResponseDto<List<AssetManagementDto>>> GetAllAsync();
        Task<ApiResponseDto<AssetManagementDto>> GetByIdAsync(int id);
        Task<ApiResponseDto<bool>> UpdateAsync(int id, AssetManagementUpdateDto dto);
        Task<ApiResponseDto<bool>> DeleteAsync(int id);
    }
}