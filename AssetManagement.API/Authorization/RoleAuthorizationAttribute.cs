using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace AssetManagement.API.Authorization
{
    public class RoleAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _allowedRoles;

        public RoleAuthorizationAttribute(params string[] allowedRoles)
        {
            _allowedRoles = allowedRoles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Check if user is authenticated
            if (!context.HttpContext.User.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new UnauthorizedObjectResult(new
                {
                    error = "User is not authenticated",
                    message = "Please provide a valid JWT token in the Authorization header"
                });
                return;
            }

            // Get user role from claims
            var userRole = context.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userRole))
            {
                context.Result = new UnauthorizedObjectResult(new
                {
                    error = "Role claim not found",
                    message = "JWT token does not contain a valid role claim",
                    availableClaims = context.HttpContext.User.Claims.Select(c => new { c.Type, c.Value }).ToList()
                });
                return;
            }

            if (!_allowedRoles.Contains(userRole))
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }

    // Predefined role authorization attributes
    public class AdminOnlyAttribute : RoleAuthorizationAttribute
    {
        public AdminOnlyAttribute() : base("Admin") { }
    }

    public class AdminAndITAttribute : RoleAuthorizationAttribute
    {
        public AdminAndITAttribute() : base("Admin", "IT Support") { }
    }

    public class AdminManagerITAttribute : RoleAuthorizationAttribute
    {
        public AdminManagerITAttribute() : base("Admin", "Manager", "IT Support") { }
    }
}