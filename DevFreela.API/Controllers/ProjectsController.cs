using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers;
[Route("api/projects")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly DevFreelaDbContext _context;
    public ProjectsController(DevFreelaDbContext context)
    {
        _context = context;
    }

    // GET api/projects?search=crm
    [HttpGet]
    public IActionResult Get(string search = "", int page = 0, int size = 3)
    {
        var projects = _context.Projects
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .Where(p => !p.IsDeleted && search == "" || p.Title.Contains(search) || p.Description.Contains(search)).ToList()
            .Skip(page * size)
            .Take(size)
            .ToList();

        var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();

        return Ok(model);
    }

    // GET api/projects/1234
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var project = _context.Projects
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .Include(p => p.Comments)
            .SingleOrDefault(p => p.Id == id);

        var model = ProjectViewModel.FromEntity(project);

        return Ok(model);
    }

    // POST api/projects
    [HttpPost]
    public IActionResult Post(CreateProjectInputModel model)
    {
        var project = model.ToEntity();

        _context.Projects.Add(project);
        _context.SaveChanges();

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
        var projects = _context.Projects.SingleOrDefault(p => p.Id == id);

        if (projects is null)
        {
            return NotFound();
        }

        projects.Update(model.Title, model.Description, model.TotalCost);

        _context.Projects.Update(projects);
        _context.SaveChanges();

        return NoContent();
    }

    // DELETE api/projects/1234
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var project = _context.Projects.SingleOrDefault(p => p.Id == id);

        if (project is null)
        {
            return NotFound();
        }

        project.SetAsDeleted();
        _context.Projects.Update(project);
        _context.SaveChanges();

        return NoContent();
    }

    // PUT api/projects/1234/start
    [HttpPut("{id}/start")]
    public IActionResult Start(int id)
    {
        var projects = _context.Projects.SingleOrDefault(p => p.Id == id);

        if (projects is null)
        {
            return NotFound();
        }

        return NoContent();
    }

    // PUT api/projects/1234/complete
    [HttpPut("{id}/complete")]
    public IActionResult Complete(int id)
    {
        var project = _context.Projects.SingleOrDefault(p => p.Id == id);

        if (project is null)
        {
            return NotFound();
        }

        project.Complete();
        _context.Projects.Update(project);
        _context.SaveChanges();

        return NoContent();
    }

    // POST api/projects/1234/comments
    [HttpPost("{id}/comments")]
    public IActionResult PostComment(int id, CreateProjectCommentInputModel model)
    {
        var projects = _context.Projects.SingleOrDefault(p => p.Id == id);

        if (projects is null)
        {
            return NotFound();
        }

        var comment = new ProjectComment(model.Content, model.IdProject, model.IdUser);

        _context.ProjectComments.Add(comment);
        _context.SaveChanges();

        return Ok();
    }
}
