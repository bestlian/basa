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
[Route("[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private IUserService _userService;
    private readonly DataContext _db;

    public UsersController(IUserService userService, DataContext dataContext)
    {
        _userService = userService;
        _db = dataContext;
    }

    // LOGIN WITH PASSWORD
    [AllowAnonymous]
    [HttpPost("login")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var alert = new Message();
        var response = _userService.Authenticate(model, IpAddress());

        if (response == null)
        {
            alert.Statuscode = 400;
            alert.Msg = "Email or password is incorrect";
            return BadRequest(alert);

        }

        return Ok(response);
    }

    // GET ALL USER
    [HttpGet("all")]
    [Produces("application/json")]
    [Authorize(Roles = 1)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IList<UserResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }

    // GET ALL USER
    [HttpGet("{id}")]
    [Produces("application/json")]
    [Authorize(Roles = 1)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IList<UserResponse>), StatusCodes.Status200OK)]
    public IActionResult GetByID(Guid id)
    {
        var user = _userService.GetById(id);
        return Ok(user);
    }

    // CREATE USER
    [HttpPost("create")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public IActionResult Create(UserRequest f)
    {
        var alert = new Message();
        if (ModelState.IsValid)
        {
            var check = _db.MsUsers.FirstOrDefault(a => a.Email == f.Email && a.IsDeleted == false);
            if (check != null)
            {
                alert.Statuscode = 400;
                alert.Msg = "Email is already exist!";

                return StatusCode(400, alert);
            }

            try
            {
                var user = new MsUser()
                {
                    Email = f.Email,
                    Password = BCHash.HashPassword(f.Password),
                    RoleID = f.RoleID,
                    Name = f.Name
                };

                _db.Add(user);
                _db.SaveChanges();

                alert.Msg = "Create new user successful";
                alert.Statuscode = 200;
                return Ok(alert);
            }
            catch (Exception ex)
            {
                alert.Msg = "Error: " + ex.Message;
                alert.Statuscode = 500;
                return StatusCode(500, alert);
            }

        }

        return BadRequest();
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