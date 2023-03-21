namespace BasaProject.Controllers;

using Microsoft.AspNetCore.Mvc;
using BasaProject.Models;
using BasaProject.Services;
using BasaProject.Outputs;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Authorize]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    // LOGIN WITH PASSWORD
    [AllowAnonymous]
    [HttpPost("login")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model, IpAddress());

        if (response == null)
            return BadRequest(new { message = "Email or password is incorrect" });

        return Ok(response);
    }


    [HttpGet("{id}")]
    [Produces("application/json")]
    [Authorize(Roles = "1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IList<UserResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }

    private string? IpAddress()
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