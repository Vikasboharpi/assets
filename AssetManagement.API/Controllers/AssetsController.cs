using AssetManagement.Application.DTOs;
using AssetManagement.Application.Interfaces;
using AssetManagement.Domain.Entities;
using AssetManagement.API.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AssetManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetManagementService _assetService;
        private readonly ILogger<AssetsController> _logger;

        public AssetsController(IAssetManagementService assetService, ILogger<AssetsController> logger)
        {
            _assetService = assetService;
            _logger = logger;
        }



    }
}