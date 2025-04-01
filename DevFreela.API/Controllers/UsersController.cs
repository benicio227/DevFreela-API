using DevFreela.API.Entities;
using DevFreela.API.Models;
using DevFreela.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly DevFreelaDbContext _context;
    public UsersController(DevFreelaDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var user = _context.Users
            .Include(u => u.Skills) // Carrega o objeto(que é uma lista de UserSkill)
                .ThenInclude(u => u.Skill) // Inclui o objeto Skill dentro de cada UserSkill
            .SingleOrDefault(u => u.Id == id);

        //Objeto montado:
        // var user = new User
        // {
        //    Id = 1,
        //    FullName = "João",
        //    Skills = new List<UserSkill>
        //    {
        //      new UserSkill {IdSkill = 1, Skill = new Skill { Id = 1, Description = "C#"}}
        //      new UserSkill {IdSkill = 2, Skill = new Skill { Id = 2, Description = "Javascript"}}
        //    }
        // }


        if (user is null)
        {
            return NotFound();
        }

        var model = UserViewModel.FromEntity(user);

        return Ok(model);
    }

    // POST api/users
    [HttpPost]
    public IActionResult Post(CreateUserInputModel model)
    {
        var user = new User(model.FullName, model.Email, model.BirthDate);

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok();
    }

    [HttpPost("{id}/skills")]
    public IActionResult PostSkills(int id, UserSkillsInputModel model)
    {
        var userSkills = model.SkillsIds.Select(s => new UserSkill(id, s)).ToList();

        _context.UserSkills.AddRange(userSkills);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPut("{id}/profile-picture")]
    public IActionResult PostProfilePicture(int id, IFormFile file)
    {
        var description = $"File: {file.FileName}, Size: {file.Length}";

        return Ok(description);
    }
}
