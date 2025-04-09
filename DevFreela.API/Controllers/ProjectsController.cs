using DevFreela.Application.Commands.CompleteProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.InserComment;
using DevFreela.Application.Commands.InsertProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace DevFreela.API.Controllers;
[Route("api/projects")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _service;
    private readonly IMediator _mediator;
    public ProjectsController(IProjectService service, IMediator mediator)
    {
        _service = service;
        _mediator = mediator;
    }

    // GET api/projects?search=crm
    [HttpGet]
    public async Task<IActionResult> Get(string search = "")
    {
        //var result = _service.GetAll();

        var query = new GetAllProjectsQuery();

        var result = _mediator.Send(query);

        return Ok(result);
    }

    // GET api/projects/1234
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        //var result = _service.GetById(id);

        var result = await _mediator.Send(new GetProjectByIdQuery(id));

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }

    // POST api/projects
    [HttpPost]
    public async Task<IActionResult> Post(InsertProjectCommand command)
    {
        //var result = _service.Insert(model);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Data}, command);
    }

    // PUT api/projects/1234

    // OBS: Veja que não reutilizamos o model CreateProjectInputModel no método Put.
    // OBS: Classes diferentes requerem Modelos(model) diferentes. Isso é uma boa prática.
    // OBS: Digamos que queremos mudar a propriedade TotalCost depois de algum tempo, então
    // precisaríamos usar um if, e isso não é bom
    // "Classes diferentes tem que serem alteradas por motivos específicos diferentes. Não podemos ficar
    // alterando a mesma classe por razões diferentes"

    [HttpPut("{id}")] // O id que passamos aqui na URL é por conta da convenção do padrão REST
    public async Task<IActionResult> Put(int id, UpdateProjectCommand command)
    {
        //var result = _service.Update(command);

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    // DELETE api/projects/1234
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        //var result = _service.Delete(id);

        var result = await _mediator.Send(new DeleteProjectCommand(id));

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    // PUT api/projects/1234/start
    [HttpPut("{id}/start")]
    public async Task<IActionResult> Start(int id)
    {
        //var result = _service.Start(id);

        var result = await _mediator.Send(new StartProjectCommand(id));

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    // PUT api/projects/1234/complete
    [HttpPut("{id}/complete")]
    public async Task<IActionResult> Complete(int id)
    {
        //var result = _service.Complete(id);

        var result = await _mediator.Send(new CompleteProjectCommand(id));

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    // POST api/projects/1234/comments
    [HttpPost("{id}/comments")]
    public async Task<IActionResult> PostComment(int id, InsertCommentCommand command)
    {
        // var result = _service.InsertComment(id, model);

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok();
    }
}
