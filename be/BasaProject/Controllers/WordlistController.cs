namespace BasaProject.Controllers;

using Microsoft.AspNetCore.Mvc;
using BasaProject.Models;
using BasaProject.Outputs;
using BasaProject.Helpers;
using BasaProject.Services;

[ApiController]
[Route("[controller]")]
[Authorize]
public class WordlistController : ControllerBase
{
    private readonly DataContext _db;

    public WordlistController(DataContext dataContext)
    {
        _db = dataContext;
    }

    // SAVE WORD
    [HttpPost("insert")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public IActionResult Create(WordlistRequest f)
    {
        var alert = new Message();
        var userid = new Guid(HttpContext.Items["UserID"]?.ToString());

        var check = _db.MsWordLists.FirstOrDefault(a => a.Word.ToLower() == f.Word.Trim().ToLower() && a.IsDeleted == false);
        if (check != null)
        {
            alert.Msg = "Word has already exist!";
            alert.Statuscode = 400;

            return BadRequest(alert);
        }

        try
        {
            var word = new MsWordList()
            {
                Word = f.Word.Trim(),
                Desc = f.Description,
                Type = f.Type,
                Indonesian = f.Indonesian.Trim(),
                English = f.English.Trim(),
                UserIn = userid
            };

            _db.Add(word);
            _db.SaveChanges();

            alert.Msg = "New word saved";
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

    // set role
    [HttpGet("wordtype")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IList<DropdownResponse>), StatusCodes.Status200OK)]
    public IActionResult GetWordType()
    {
        var res = new List<DropdownResponse>()
        {
            new DropdownResponse { Name = "Basa Lemes", Value = "lemes" },
            new DropdownResponse { Name = "Basa Loma", Value = "loma" },
            new DropdownResponse { Name = "Basa Kasar", Value = "kasar" },
        };

        return Ok(res);
    }
}