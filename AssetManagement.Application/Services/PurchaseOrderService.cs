using AssetManagement.Application.DTOs;
using AssetManagement.Application.Interfaces;
using AssetManagement.Domain.Entities;
using AssetManagement.Domain.Interfaces;
using AutoMapper;

namespace AssetManagement.Application.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        private readonly List<string> _availableStatuses = new()
        {
            "Pending",
            "Approved",
            "Rejected",
            "In Progress",
            "Completed",
            "Cancelled"
        };

        public PurchaseOrderService(
            IPurchaseOrderRepository purchaseOrderRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PurchaseOrderDto>> GetAllPurchaseOrdersAsync()
        {
            var purchaseOrders = await _purchaseOrderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PurchaseOrderDto>>(purchaseOrders);
        }

        public async Task<PurchaseOrderDto?> GetPurchaseOrderByIdAsync(int id)
        {
            var purchaseOrder = await _purchaseOrderRepository.GetByIdAsync(id);
            return purchaseOrder == null ? null : _mapper.Map<PurchaseOrderDto>(purchaseOrder);
        }

        public async Task<PurchaseOrderDto?> GetPurchaseOrderByPRIdAsync(string prId)
        {
            var purchaseOrder = await _purchaseOrderRepository.GetByPRIdAsync(prId);
            return purchaseOrder == null ? null : _mapper.Map<PurchaseOrderDto>(purchaseOrder);
        }

        public async Task<IEnumerable<PurchaseOrderDto>> GetPurchaseOrdersByStatusAsync(string status)
        {
            var purchaseOrders = await _purchaseOrderRepository.GetByStatusAsync(status);
            return _mapper.Map<IEnumerable<PurchaseOrderDto>>(purchaseOrders);
        }

        public async Task<IEnumerable<PurchaseOrderDto>> GetPurchaseOrdersByRequesterAsync(string requesterName)
        {
            var purchaseOrders = await _purchaseOrderRepository.GetByRequesterAsync(requesterName);
            return _mapper.Map<IEnumerable<PurchaseOrderDto>>(purchaseOrders);
        }

        public async Task<IEnumerable<PurchaseOrderDto>> GetPurchaseOrdersByCategoryAsync(string category)
        {
            var purchaseOrders = await _purchaseOrderRepository.GetByCategoryAsync(category);
            return _mapper.Map<IEnumerable<PurchaseOrderDto>>(purchaseOrders);
        }

        public async Task<IEnumerable<PurchaseOrderDto>> GetPurchaseOrdersByLocationAsync(string location)
        {
            var purchaseOrders = await _purchaseOrderRepository.GetByLocationAsync(location);
            return _mapper.Map<IEnumerable<PurchaseOrderDto>>(purchaseOrders);
        }

        public async Task<IEnumerable<PurchaseOrderDto>> GetPurchaseOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var purchaseOrders = await _purchaseOrderRepository.GetByDateRangeAsync(startDate, endDate);
            return _mapper.Map<IEnumerable<PurchaseOrderDto>>(purchaseOrders);
        }

        public async Task<PurchaseOrderDto> CreatePurchaseOrderAsync(CreatePurchaseOrderDto createDto, int createdByUserId)
        {
            // Validate user exists
            if (!await _userRepository.ExistsAsync(createdByUserId))
            {
                throw new ArgumentException("User not found");
            }

            // Check if PR_ID already exists
            if (await _purchaseOrderRepository.PRIdExistsAsync(createDto.PR_ID))
            {
                throw new ArgumentException("PR_ID already exists");
            }

            var purchaseOrder = _mapper.Map<PurchaseOrder>(createDto);
            purchaseOrder.CreatedByUserId = createdByUserId;
            purchaseOrder.OrderDateTime = createDto.OrderDateTime ?? DateTime.UtcNow;

            var createdPurchaseOrder = await _purchaseOrderRepository.CreateAsync(purchaseOrder);
            return _mapper.Map<PurchaseOrderDto>(createdPurchaseOrder);
        }

        public async Task<PurchaseOrderDto> UpdatePurchaseOrderAsync(int id, UpdatePurchaseOrderDto updateDto, int updatedByUserId)
        {
            var existingPurchaseOrder = await _purchaseOrderRepository.GetByIdAsync(id);
            if (existingPurchaseOrder == null)
            {
                throw new ArgumentException("Purchase Order not found");
            }

            // Validate user exists
            if (!await _userRepository.ExistsAsync(updatedByUserId))
            {
                throw new ArgumentException("User not found");
            }

            // Update properties
            existingPurchaseOrder.RequesterName = updateDto.RequesterName;
            existingPurchaseOrder.ProcessName = updateDto.ProcessName;
            existingPurchaseOrder.Category = updateDto.Category;
            existingPurchaseOrder.ITCategory = updateDto.ITCategory;
            existingPurchaseOrder.AssetName = updateDto.AssetName;
            existingPurchaseOrder.Location = updateDto.Location;
            existingPurchaseOrder.Quantity = updateDto.Quantity;
            existingPurchaseOrder.Status = updateDto.Status;
            existingPurchaseOrder.UpdatedByUserId = updatedByUserId;
            
            if (updateDto.OrderDateTime.HasValue)
            {
                existingPurchaseOrder.OrderDateTime = updateDto.OrderDateTime.Value;
            }

            var updatedPurchaseOrder = await _purchaseOrderRepository.UpdateAsync(existingPurchaseOrder);
            return _mapper.Map<PurchaseOrderDto>(updatedPurchaseOrder);
        }

        public async Task<bool> UpdatePurchaseOrderStatusAsync(int id, string status, int updatedByUserId)
        {
            var existingPurchaseOrder = await _purchaseOrderRepository.GetByIdAsync(id);
            if (existingPurchaseOrder == null)
            {
                return false;
            }

            if (!_availableStatuses.Contains(status))
            {
                throw new ArgumentException("Invalid status");
            }

            existingPurchaseOrder.Status = status;
            existingPurchaseOrder.UpdatedByUserId = updatedByUserId;

            await _purchaseOrderRepository.UpdateAsync(existingPurchaseOrder);
            return true;
        }

        public async Task<bool> DeletePurchaseOrderAsync(int id)
        {
            return await _purchaseOrderRepository.DeleteAsync(id);
        }

        public async Task<bool> PurchaseOrderExistsAsync(int id)
        {
            return await _purchaseOrderRepository.ExistsAsync(id);
        }

        public async Task<bool> PRIdExistsAsync(string prId)
        {
            return await _purchaseOrderRepository.PRIdExistsAsync(prId);
        }

        public async Task<IEnumerable<string>> GetAvailableStatusesAsync()
        {
            return await Task.FromResult(_availableStatuses);
        }
    }
}