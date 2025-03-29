using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;
[Route("api/skills")]
[ApiController]
public class SkillsController : ControllerBase
{
    // GET api/skills
    [HttpGet]
    public IActionResult GetAll()
    {

        return Ok();
    }

    // post api/skills
    [HttpPost]
    public IActionResult Post()
    {
        return Ok();
    }
}
