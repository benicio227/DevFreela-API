using DevFreela.Application.Models;
using DevFreela.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;
    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var result = _service.GetById(id);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }

    // POST api/users
    [HttpPost]
    public IActionResult Post(CreateUserInputModel model)
    {
        var result = _service.Insert(model);

        return CreatedAtAction(nameof(GetById), new { Id = result.Data, model});
    }

    [HttpPost("{id}/skills")]
    public IActionResult PostSkills(int id, UserSkillsInputModel model)
    {
        var result = _service.InsertSkill(id, model);

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
