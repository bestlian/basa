namespace BasaProject.Controllers;

using Microsoft.AspNetCore.Mvc;
using BasaProject.Models;
using BasaProject.Outputs;
using BasaProject.Helpers;
using BasaProject.Services;

[ApiController]
[Route("[controller]")]
[Authorize]
public class TranslateController : ControllerBase
{
    private readonly DataContext _db;

    public TranslateController(DataContext dataContext)
    {
        _db = dataContext;
    }

    // TRANSLATE WORD
    [HttpGet("{words}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(TranslateResponse), StatusCodes.Status200OK)]
    public IActionResult Create(string words)
    {
        var res = TranslateHelper.SundaLemes(words, _db);

        return Ok(res);
    }
}