using DevFreela.Application.Commands.UserFolder.InsertSkill;
using DevFreela.Application.Commands.UserFolder.InsertUser;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.GetUserById;
using DevFreela.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;
    private readonly IMediator _mediator;
    public UsersController(IUserService service, IMediator mediator)
    {
        _service = service;
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        //var result = _service.GetById(id);

        var result = await _mediator.Send(new GetUserByIdQuery(id));

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }

    // POST api/users
    [HttpPost]
    public async Task<IActionResult> Post(InsertUserCommand command)
    {
        //var result = _service.Insert(model);
        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
    }

    [HttpPost("{id}/skills")]
    public async Task<IActionResult> PostSkills(int id, InsertSkillCommand command)
    {
        //var result = _service.InsertSkill(id, model);

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok();
    }

    [HttpPut("{id}/profile-picture")]
    public IActionResult PostProfilePicture(int id, IFormFile file)
    {
        var description = $"File: {file.FileName}, Size: {file.Length}";

        return Ok(description);
    }
}
