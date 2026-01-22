using AssetManagement.Application.DTOs;
using AssetManagement.Application.Interfaces;
using AssetManagement.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AssetManagement.API.Controllers
{
    [ApiController]
    [Route("api/assets")]
    [Authorize(Roles = "Admin,Manager,IT")]
    public class AssetManagementController : ControllerBase
    {
        private readonly IAssetManagementService _service;

        public AssetManagementController(IAssetManagementService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager,IT")]
        public async Task<IActionResult> Create(AssetManagementCreateDto dto)
        {
            var userId = GetUserId(User);
            var result = await _service.CreateAsync(dto, userId);
            return result.Success ? Ok(result) : BadRequest(result);
        }


        public static int GetUserId(ClaimsPrincipal user)
        {
            var claim = user.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null || !int.TryParse(claim.Value, out var userId))
                throw new UnauthorizedAccessException("Invalid user identity");

            return userId;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result.Success ? Ok(result) : NotFound(result);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
      int id, AssetManagementUpdateDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return result.Success ? Ok(result) : NotFound(result);
        }

    
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result.Success ? Ok(result) : NotFound(result);
        }


    }
}
