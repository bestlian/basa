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

        var check = WordlistHelper.Find(f.Word.Trim().ToLower(), _db);
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

    // PAIR BASA LEMES
    [HttpPost("pairing")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IList<Message>), StatusCodes.Status200OK)]
    public IActionResult Pairing(PairingRequest f)
    {
        var alert = new Message();
        if (ModelState.IsValid)
        {
            var res = WordlistHelper.PairingBasaLemes(f, _db);

            return StatusCode(res.Statuscode, res);
        }

        var errorList = ModelState.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
        );

        alert.DetailError = errorList;
        alert.Statuscode = 400;
        alert.Msg = "Validation error";
        return BadRequest(alert);
    }

    // GET WORD TYPE
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

    // SEARCH WORD BY KEYWORD
    [HttpGet("search/{word}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IList<WordlistResponse>), StatusCodes.Status200OK)]
    public IActionResult Search(string word)
    {
        var res = WordlistHelper.Search(word, _db);

        return Ok(res);
    }

    // GET WORD BY TYPE
    [HttpGet("type/{type}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IList<WordlistResponse>), StatusCodes.Status200OK)]
    public IActionResult GetByType(string type, int page, int size)
    {
        if (string.IsNullOrEmpty(type) || page <= 0 || size <= 0)
        {
            return null;
        }

        var res = WordlistHelper.GetByType(type, page, size, _db);

        return Ok(res);
    }

}