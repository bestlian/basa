namespace BasaProject.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
//using Microsoft.AspNetCore.Authorization;
using BasaProject.Models;
using BasaProject.Services;
using BasaProject.Outputs;
using BasaProject.Helpers;
using BCHash = BCrypt.Net.BCrypt;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ClientAppController : ControllerBase
{
    private IUserService _userService;
    private readonly DataContext _db;

    public ClientAppController(IUserService userService, DataContext dataContext)
    {
        _userService = userService;
        _db = dataContext;
    }

    // GET BEARER TOKEN
    [AllowAnonymous]
    [HttpPost("getToken")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public IActionResult Login(ClientRequest model)
    {
        var alert = new Message();
        var response = _userService.ClientAuthenticate(model, IpAddress());

        if (response == null)
        {
            alert.Statuscode = 400;
            alert.Msg = "Email or password is incorrect";
            return BadRequest(alert);

        }

        return Ok(response);
    }

    private string IpAddress()
    {
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"];
        else
            return HttpContext.Connection?.RemoteIpAddress?.MapToIPv4().ToString();
    }

    private void DeleteTokenCookie()
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(7)
        };
        Response.Cookies.Delete("refreshToken");
    }
}