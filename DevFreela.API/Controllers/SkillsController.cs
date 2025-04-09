using DevFreela.Application.Commands.SkillFolder.InsertSkill;
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

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok(); // ou NotFound() só pra não dar erro
    }

    // post api/skills
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Application.Commands.SkillFolder.InsertSkill.CreateSkillInputModel command)
    {
        //var result = _service.Insert;


        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
    }
}
