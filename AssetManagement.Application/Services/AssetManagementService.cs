using AssetManagement.Application.DTOs;
using AssetManagement.Application.Interfaces;
using AssetManagement.Domain.Entities;
using AssetManagement.Domain.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AssetManagement.Application.Services
{
    public class AssetManagementService:IAssetManagementService
    {
        private readonly IAssetManagementRepository _repo;
        private readonly ILogger<AssetManagementService> _logger;

        public AssetManagementService(IAssetManagementRepository repo, ILogger<AssetManagementService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<ApiResponseDto<AssetManagementDto>> CreateAsync(
            AssetManagementCreateDto dto, int userId)
        {
            try
            {
                if (await _repo.ExistsBySerialAsync(dto.SerialNumber))
                    return ApiResponseDto<AssetManagementDto>
                        .ErrorResponse("Serial number already exists");

                var asset = new Assetmanagement
                {
                    Category = dto.Category,
                    AssetName = dto.AssetName,
                    Brand = dto.Brand,
                    SerialNumber = dto.SerialNumber,
                    Location = dto.Location,
                    RAM = dto.RAM,
                    Storage = dto.Storage,
                    Processor = dto.Processor,
                    StorageSize = dto.StorageSize,
                    Remarks = dto.Remarks,
                    CreatedBy = userId,
                    DepartmentType = dto.DepartmentType
                };

                await _repo.AddAsync(asset);

                return ApiResponseDto<AssetManagementDto>.SuccessResponse(
                    new AssetManagementDto
                    {
                        Id = asset.Id,
                        AssetName = asset.AssetName,
                        Category = asset.Category,
                        Brand = asset.Brand,
                        SerialNumber = asset.SerialNumber,
                        Status = asset.Status,
                        DepartmentType = asset.DepartmentType
                    },
                    "Asset registered successfully"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Asset creation failed");
                throw; 
            }
        }

        public async Task<ApiResponseDto<List<AssetManagementDto>>> GetAllAsync()
        {
            try
            {
                var assets = await _repo.GetAllAsync();

                var result = assets.Select(a => new AssetManagementDto
                {
                    Id = a.Id,
                    Category = a.Category,
                    AssetName = a.AssetName,
                    Brand = a.Brand,
                    SerialNumber = a.SerialNumber,
                    Location = a.Location,
                    RAM = a.RAM,
                    Storage = a.Storage,
                    Processor = a.Processor,
                    StorageSize = a.StorageSize,
                    Remarks = a.Remarks,
                    CreatedAt = a.CreatedAt,
                    DepartmentType = a.DepartmentType
                }).ToList();

                return ApiResponseDto<List<AssetManagementDto>>
                    .SuccessResponse(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "failed to load assets");
                throw;
            }
        }

        // VIEW BY ID
        public async Task<ApiResponseDto<AssetManagementDto>> GetByIdAsync(int id)
        {
            try
            {
                var asset = await _repo.GetByIdAsync(id);
                if (asset == null)
                    return ApiResponseDto<AssetManagementDto>
                        .ErrorResponse("Asset not found");

                return ApiResponseDto<AssetManagementDto>.SuccessResponse(
                    new AssetManagementDto
                    {
                        Id = asset.Id,
                        Category = asset.Category,
                        AssetName = asset.AssetName,
                        Brand = asset.Brand,
                        SerialNumber = asset.SerialNumber,
                        Location = asset.Location,
                        RAM = asset.RAM,
                        Storage = asset.Storage,
                        Processor = asset.Processor,
                        StorageSize = asset.StorageSize,
                        Remarks = asset.Remarks,
                        CreatedAt = asset.CreatedAt,
                        DepartmentType = asset.DepartmentType
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "failed to load assets by id");
                throw;
            }
        }
        public async Task<ApiResponseDto<bool>> UpdateAsync(
    int id, AssetManagementUpdateDto dto)
        {
            try
            {
                var asset = await _repo.GetByIdAsync(id);
                if (asset == null)
                    return ApiResponseDto<bool>.ErrorResponse("Asset not found");

                asset.Category = dto.Category;
                asset.AssetName = dto.AssetName;
                asset.Brand = dto.Brand;
                asset.Location = dto.Location;
                asset.RAM = dto.RAM;
                asset.Storage = dto.Storage;
                asset.Processor = dto.Processor;
                asset.StorageSize = dto.StorageSize;
                asset.Remarks = dto.Remarks;
                asset.DepartmentType = dto.DepartmentType;

                await _repo.UpdateAsync(asset);

                return ApiResponseDto<bool>
                    .SuccessResponse(true, "Asset updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Asset updated unsuccessfully");
                throw;
            }
        }

        public async Task<ApiResponseDto<bool>> DeleteAsync(int id)
        {
            try
            {
                var asset = await _repo.GetByIdAsync(id);
                if (asset == null)
                    return ApiResponseDto<bool>.ErrorResponse("Asset not found");

                await _repo.DeleteAsync(asset);

                return ApiResponseDto<bool>
                    .SuccessResponse(true, "Asset deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Asset not deleted successfully");
                throw;
            }
        }
    }
}