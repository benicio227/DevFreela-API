using DevFreela.Application.Models;
using DevFreela.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;
[Route("api/skills")]
[ApiController]
public class SkillsController : ControllerBase
{
    private readonly ISkillService _service;
    public SkillsController(ISkillService service)
    {
        _service = service;
    }

    // GET api/skills
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _service.GetAll;

        return Ok(result);
    }

    // post api/skills
    [HttpPost]
    public IActionResult Post(CreateSkillInputModel model)
    {
        var result = _service.Insert;

        return CreatedAtAction(string.Empty, model);
    }
}
