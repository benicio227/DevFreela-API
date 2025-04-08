﻿using DevFreela.Application.Models;
using DevFreela.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;
[Route("api/projects")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _service;
    public ProjectsController(IProjectService service)
    {
        _service = service;
    }

    // GET api/projects?search=crm
    [HttpGet]
    public IActionResult Get(string search = "")
    {
        var result = _service.GetAll();

        return Ok(result);
    }

    // GET api/projects/1234
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

    // POST api/projects
    [HttpPost]
    public IActionResult Post(CreateProjectInputModel model)
    {
        var result = _service.Insert(model);

        return CreatedAtAction(nameof(GetById), new { id = result.Data}, model);
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
        var result = _service.Update(model);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    // DELETE api/projects/1234
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var result = _service.Delete(id);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    // PUT api/projects/1234/start
    [HttpPut("{id}/start")]
    public IActionResult Start(int id)
    {
        var result = _service.Start(id);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    // PUT api/projects/1234/complete
    [HttpPut("{id}/complete")]
    public IActionResult Complete(int id)
    {
        var result = _service.Complete(id);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    // POST api/projects/1234/comments
    [HttpPost("{id}/comments")]
    public IActionResult PostComment(int id, CreateProjectCommentInputModel model)
    {
        var result = _service.InsertComment(id, model);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok();
    }
}
