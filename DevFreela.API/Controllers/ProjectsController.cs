﻿using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;
[Route("api/projects")]
[ApiController]
public class ProjectsController : ControllerBase
{
    public ProjectsController()
    {

    }

    // GET api/projects?search=crm
    [HttpGet]
    public IActionResult Get(string search = "")
    {
        return Ok();
    }

    // GET api/projects/1234
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok();
    }

    // POST api/projects
    [HttpPost]
    public IActionResult Post(CreateProjectInputModel model)
    {
        return CreatedAtAction(nameof(GetById), new { id = 1}, model);
    }

    // PUT api/projects/1234

    // OBS: Veja que não reutilizamos o model CreateProjectInputModel no método Put.
    // OBS: Classes diferentes requerem Modelos(model) diferentes. Isso é uma boa prática.
    // OBS: Digamos que queremos mudar a propriedade TotalCost depois de algum tempo, então
    // precisaríamos usar um if, e isso não é bom
    // "Classes diferentes tem que serem alteradas por motivos específicos diferentes. Não podemos ficar
    // alterando a mesma classe por razões diferentes"

    [HttpPut("{id}")] // O id que passamos aqui na URL é por conta da convenção do padrão REST
    public IActionResult Put(int id, UpdateProjectInputModel model)
    {
        return NoContent();
    }

    // DELETE api/projects/1234
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return NoContent();
    }

    // PUT api/projects/1234/start
    [HttpPut("{id}/start")]
    public IActionResult Start(int id)
    {
        return NoContent();
    }

    // PUT api/projects/1234/complete
    [HttpPut("{id}/complete")]
    public IActionResult Complete(int id)
    {
        return NoContent();
    }

    // POST api/projects/1234/comments
    [HttpPost("{id}/comments")]
    public IActionResult PostComment(int id, CreateProjectCommentInputModel model)
    {
        return Ok();
    }
}
