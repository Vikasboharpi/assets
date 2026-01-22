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

        [HttpGet]
        [RoleAuthorization("Admin", "Manager", "Employee", "IT Support")]
        public async Task<ActionResult<IEnumerable<PurchaseOrderDto>>> GetAllPurchaseOrders()
        {
            var purchaseOrders = await _purchaseOrderService.GetAllPurchaseOrdersAsync();
            return Ok(purchaseOrders);
        }

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




        [HttpPost("/UpdateForApproval/{id}")]
        [RoleAuthorization("Admin", "Manager", "Employee", "IT Support")]
        public async Task<ActionResult<UpdatePurchaseOrderDto>> UpdatePurchaseOrderForApproved(int id)
        {
            var purchaseOrder = await _purchaseOrderService.UpdatePurchaseOrderForApproved(id);
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