namespace BasaProject.Controllers;

using Microsoft.AspNetCore.Mvc;
using BasaProject.Models;
using BasaProject.Outputs;
using BasaProject.Helpers;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("[controller]")]
[Authorize]
public class RoleController : ControllerBase
{
    private readonly DataContext _db;

    public RoleController(DataContext dataContext)
    {
        _db = dataContext;
    }

    // set role
    [AllowAnonymous]
    [HttpGet("all")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IList<RoleResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        IList<RoleResponse> response = _db.MsRoles.Where(a => a.IsDeleted == false)
            .Select(a => new RoleResponse
            {
                RoleID = a.RoleID,
                RoleName = a.RoleName
            }).ToList();

        return Ok(response);
    }

    // set role
    [HttpPost("insert/{rolename}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public IActionResult Create(string rolename)
    {
        var alert = new Message();
        if (string.IsNullOrEmpty(rolename)) return BadRequest();

        var check = _db.MsRoles.FirstOrDefault(a => a.RoleName.ToLower() == rolename.Trim().ToLower() && a.IsDeleted == false);
        if (check != null)
        {
            alert.Msg = "Role name has already exist!";
            alert.Statuscode = 400;

            return BadRequest(alert);
        }

        try
        {
            var role = new MsRole()
            {
                RoleName = rolename.Trim()
            };

            _db.Add(role);
            _db.SaveChanges();

            alert.Msg = "New role created";
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
}