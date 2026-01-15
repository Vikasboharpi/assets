using AssetManagement.Application.DTOs;
using AssetManagement.Application.Interfaces;
using AssetManagement.API.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AssetManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly IPurchaseOrderService _purchaseOrderService;

        public PurchaseOrdersController(IPurchaseOrderService purchaseOrderService)
        {
            _purchaseOrderService = purchaseOrderService;
        }

        /// <summary>
        /// Get all purchase orders
        /// </summary>
        [HttpGet]
        [RoleAuthorization("Admin", "Manager", "Employee", "IT Support")]
        public async Task<ActionResult<IEnumerable<PurchaseOrderDto>>> GetAllPurchaseOrders()
        {
            var purchaseOrders = await _purchaseOrderService.GetAllPurchaseOrdersAsync();
            return Ok(purchaseOrders);
        }

        /// <summary>
        /// Get purchase order by ID
        /// </summary>
        [HttpGet("/GetById/{id}")]
        [RoleAuthorization("Admin", "Manager", "Employee", "IT Support")]
        public async Task<ActionResult<PurchaseOrderDto>> GetPurchaseOrder(int id)
        {
            var purchaseOrder = await _purchaseOrderService.GetPurchaseOrderByIdAsync(id);
            if (purchaseOrder == null)
            {
                return NotFound($"Purchase Order with ID {id} not found");
            }
            return Ok(purchaseOrder);
        }



        [HttpGet("{id}")]
        [RoleAuthorization("Admin", "Manager", "Employee", "IT Support")]
        public async Task<ActionResult<PurchaseOrderDto>> UpdatePoOrder(int id)
        {
            var purchaseOrder = await _purchaseOrderService.GetPurchaseOrderByIdAsync(id);
            if (purchaseOrder == null)
            {
                return NotFound($"Purchase Order with ID {id} not found");
            }
            return Ok(purchaseOrder);
        }

        /// <summary>
        /// Get purchase order by PR_ID
        /// </summary>
        //[HttpGet("pr/{prId}")]
        //[RoleAuthorization("Admin", "Manager", "Employee", "IT Support")]
        //public async Task<ActionResult<PurchaseOrderDto>> GetPurchaseOrderByPRId(string prId)
        //{
        //    var purchaseOrder = await _purchaseOrderService.GetPurchaseOrderByPRIdAsync(prId);
        //    if (purchaseOrder == null)
        //    {
        //        return NotFound($"Purchase Order with PR_ID {prId} not found");
        //    }
        //    return Ok(purchaseOrder);
        //}

        /// <summary>
        /// Get purchase orders by status
        /// </summary>
        //[HttpGet("status/{status}")]
        //[RoleAuthorization("Admin", "Manager", "Employee", "IT Support")]
        //public async Task<ActionResult<IEnumerable<PurchaseOrderDto>>> GetPurchaseOrdersByStatus(string status)
        //{
        //    var purchaseOrders = await _purchaseOrderService.GetPurchaseOrdersByStatusAsync(status);
        //    return Ok(purchaseOrders);
        //}

        /// <summary>
        /// Get purchase orders by requester name
        /// </summary>
        //[HttpGet("requester/{requesterName}")]
        //[RoleAuthorization("Admin", "Manager", "Employee", "IT Support")]
        //public async Task<ActionResult<IEnumerable<PurchaseOrderDto>>> GetPurchaseOrdersByRequester(string requesterName)
        //{
        //    var purchaseOrders = await _purchaseOrderService.GetPurchaseOrdersByRequesterAsync(requesterName);
        //    return Ok(purchaseOrders);
        //}

        /// <summary>
        /// Get purchase orders by category
        /// </summary>
        //[HttpGet("category/{category}")]
        //[RoleAuthorization("Admin", "Manager", "Employee", "IT Support")]
        //public async Task<ActionResult<IEnumerable<PurchaseOrderDto>>> GetPurchaseOrdersByCategory(string category)
        //{
        //    var purchaseOrders = await _purchaseOrderService.GetPurchaseOrdersByCategoryAsync(category);
        //    return Ok(purchaseOrders);
        //}

        /// <summary>
        /// Get purchase orders by location
        /// </summary>
        //[HttpGet("location/{location}")]
        //[RoleAuthorization("Admin", "Manager", "Employee", "IT Support")]
        //public async Task<ActionResult<IEnumerable<PurchaseOrderDto>>> GetPurchaseOrdersByLocation(string location)
        //{
        //    var purchaseOrders = await _purchaseOrderService.GetPurchaseOrdersByLocationAsync(location);
        //    return Ok(purchaseOrders);
        //}

        /// <summary>
        /// Get purchase orders by date range
        /// </summary>
        //[HttpGet("daterange")]
        //[RoleAuthorization("Admin", "Manager", "Employee", "IT Support")]
        //public async Task<ActionResult<IEnumerable<PurchaseOrderDto>>> GetPurchaseOrdersByDateRange(
        //    [FromQuery] DateTime startDate, 
        //    [FromQuery] DateTime endDate)
        //{
        //    if (startDate > endDate)
        //    {
        //        return BadRequest("Start date cannot be greater than end date");
        //    }

        //    var purchaseOrders = await _purchaseOrderService.GetPurchaseOrdersByDateRangeAsync(startDate, endDate);
        //    return Ok(purchaseOrders);
        //}

        //    var purchaseOrders = await _purchaseOrderService.GetPurchaseOrdersByDateRangeAsync(startDate, endDate);
        //    return Ok(purchaseOrders);
        //}

        /// <summary>
        /// Debug endpoint to check authentication and authorization
        /// </summary>
        [HttpGet("debug/auth")]
        [Authorize]
        public ActionResult<object> DebugAuth()
        {
            var debugInfo = new
            {
                IsAuthenticated = User.Identity?.IsAuthenticated ?? false,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                UserName = User.FindFirst(ClaimTypes.Name)?.Value,
                UserEmail = User.FindFirst(ClaimTypes.Email)?.Value,
                UserRole = User.FindFirst(ClaimTypes.Role)?.Value,
                AllClaims = User.Claims.Select(c => new { c.Type, c.Value }).ToList(),
                AllowedRoles = new[] { "Admin", "Manager", "Employee", "IT Support" },
                RoleMatch = User.FindFirst(ClaimTypes.Role)?.Value != null && 
                           new[] { "Admin", "Manager", "Employee", "IT Support" }.Contains(User.FindFirst(ClaimTypes.Role)?.Value)
            };

            return Ok(debugInfo);
        }

        /// <summary>
        /// Get available statuses
        /// </summary>
        [HttpGet("statuses")]
        [RoleAuthorization("Admin", "Manager", "Employee", "IT Support")]
        public async Task<ActionResult<IEnumerable<string>>> GetAvailableStatuses()
        {
            var statuses = await _purchaseOrderService.GetAvailableStatusesAsync();
            return Ok(statuses);
        }

        /// <summary>
        /// Create a new purchase order
        /// </summary>
        [HttpPost]
        [RoleAuthorization("Admin", "Manager", "Employee", "IT Support")]
        public async Task<ActionResult<PurchaseOrderDto>> CreatePurchaseOrder(CreatePurchaseOrderDto createDto)
        {
            try
            {
                var userId = GetCurrentUserId();
                var purchaseOrder = await _purchaseOrderService.CreatePurchaseOrderAsync(createDto, userId);
                return CreatedAtAction(nameof(GetPurchaseOrder), new { id = purchaseOrder.Id }, purchaseOrder);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update a purchase order
        /// </summary>
        [HttpPut("{id}")]
        [RoleAuthorization("Admin", "Manager", "IT Support")]
        public async Task<ActionResult<PurchaseOrderDto>> UpdatePurchaseOrder(int id, UpdatePurchaseOrderDto updateDto)
        {
            try
            {
                var userId = GetCurrentUserId();
                var purchaseOrder = await _purchaseOrderService.UpdatePurchaseOrderAsync(id, updateDto, userId);
                return Ok(purchaseOrder);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update purchase order status
        /// </summary>
        //[HttpPatch("{id}/status")]
        //[RoleAuthorization("Admin", "Manager", "IT Support")]
        //public async Task<ActionResult> UpdatePurchaseOrderStatus(int id, PurchaseOrderStatusDto statusDto)
        //{
        //    try
        //    {
        //        var userId = GetCurrentUserId();
        //        var result = await _purchaseOrderService.UpdatePurchaseOrderStatusAsync(id, statusDto.Status, userId);
        //        if (!result)
        //        {
        //            return NotFound($"Purchase Order with ID {id} not found");
        //        }
        //        return NoContent();
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        /// <summary>
        /// Delete a purchase order (soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        [RoleAuthorization("Admin", "Manager")]
        public async Task<ActionResult> DeletePurchaseOrder(int id)
        {
            var result = await _purchaseOrderService.DeletePurchaseOrderAsync(id);
            if (!result)
            {
                return NotFound($"Purchase Order with ID {id} not found");
            }
            return NoContent();
        }

        /// <summary>
        /// Check if purchase order exists
        /// </summary>
        //[HttpHead("{id}")]
        //[RoleAuthorization("Admin", "Manager", "Employee", "IT Support")]
        //public async Task<ActionResult> PurchaseOrderExists(int id)
        //{
        //    var exists = await _purchaseOrderService.PurchaseOrderExistsAsync(id);
        //    return exists ? Ok() : NotFound();
        //}

        /// <summary>
        /// Check if PR_ID exists
        /// </summary>
        //[HttpHead("pr/{prId}")]
        //[RoleAuthorization("Admin", "Manager", "Employee", "IT Support")]
        //public async Task<ActionResult> PRIdExists(string prId)
        //{
        //    var exists = await _purchaseOrderService.PRIdExistsAsync(prId);
        //    return exists ? Ok() : NotFound();
        //}

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                throw new UnauthorizedAccessException("User ID not found in token");
            }
            return userId;
        }
    }
}