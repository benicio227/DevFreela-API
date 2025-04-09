using DevFreela.Application.Models;
using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;
[Route("api/skills")]
[ApiController]
public class SkillsController : ControllerBase
{
    private readonly ISkillService _service;
    private readonly IMediator _mediator;
    public SkillsController(ISkillService service, IMediator mediator)
    {
        _service = service;
        _mediator = mediator;
    }

    // GET api/skills
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // var result = _service.GetAll;

        var result = await _mediator.Send(new GetAllSkillsQuery());

        return Ok(result);
    }

    // post api/skills
    [HttpPost]
    public async Task<IActionResult> Post(CreateSkillInputModel model)
    {
        //var result = _service.Insert;

        var result = await _mediator.Send(model);

        return CreatedAtAction(string.Empty, model);
    }
}
