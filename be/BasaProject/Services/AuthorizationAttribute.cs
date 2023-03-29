namespace BasaProject.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public int Roles { get; set; }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userID = context.HttpContext.Items["UserID"]?.ToString();
        var roleID = context.HttpContext.Items["RoleID"]?.ToString();
        var endpoint = context.HttpContext.GetEndpoint();

        // skip authorization if action is decorated with [AllowAnonymous] attribute
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        // AUTH START
        var role = endpoint?.Metadata?.GetMetadata<AuthorizeAttribute>()?.Roles;
        if (userID == null)
        {
            // NOT LOGIN
            context.Result = new JsonResult(new { status = 401, msg = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
        else if (role > 0 && role.ToString() != roleID)
        {
            if (roleID != "1")
            {
                // NOT AUTHORIZED ROLE
                context.Result = new JsonResult(new { status = 403, msg = "Forbidden! authorized roles only" }) { StatusCode = StatusCodes.Status403Forbidden };
            }
        }
    }
}

[AttributeUsage(AttributeTargets.Method)]
public class AllowAnonymousAttribute : Attribute
{ }