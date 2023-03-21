using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Microsoft.AspNetCore.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public int Roles { get; set; }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userID = context.HttpContext.Items["UserID"]?.ToString();
        var roleID = context.HttpContext.Items["RoleID"]?.ToString();
        var endpoint = context.HttpContext.GetEndpoint();
        //var role = this.Roles;
        if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() == null)
        {
            var role = endpoint?.Metadata?.GetMetadata<AuthorizeAttribute>()?.Roles;
            if (userID == null)
            {
                // not logged in
                context.Result = new JsonResult(new { status = 401, msg = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else if (role > 0 && role.ToString() != roleID)
            {
                if (roleID != "1")
                {
                    // not logged in
                    context.Result = new JsonResult(new { status = 403, msg = "Forbidden! Only authorized roles" }) { StatusCode = StatusCodes.Status403Forbidden };
                }
            }
        }
    }
}