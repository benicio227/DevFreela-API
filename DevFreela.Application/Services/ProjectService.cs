using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services;
public class ProjectService : IProjectService
{
    private readonly DevFreelaDbContext _context;
    public ProjectService(DevFreelaDbContext context)
    {
        _context = context;
    }

    public ResultViewModel Complete(int id)
    {
        var project = _context.Projects.SingleOrDefault(p => p.Id == id);

        if (project is null)
        {
            return ResultViewModel.Error("Projeto não existe");
        }

        project.Complete();
        _context.Projects.Update(project);
        _context.SaveChanges();

        return ResultViewModel.Success();
    }

    public ResultViewModel Delete(int id)
    {
        var project = _context.Projects.SingleOrDefault(p => p.Id == id);

        if (project is null)
        {
            return ResultViewModel.Error("Projeto não existe");
        }

        project.SetAsDeleted();
        _context.Projects.Update(project);
        _context.SaveChanges();

        return ResultViewModel.Success();
    }

    public ResultViewModel<List<ProjectItemViewModel>> GetAll(string search = "")
    {
        var projects = _context.Projects
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .Where(p => !p.IsDeleted && search == "" || p.Title.Contains(search) || p.Description.Contains(search)).ToList()
            .ToList();

        var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();
        // O método Select pega cada objeto da lista de projects e passa para o método FromEntity(que está espe
        // rando no parâmetro um objeto do tipo Project)
        // E dentro do método é feita a conversão das propriedades da entidade Project para o model

        return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
    }

    public ResultViewModel<ProjectViewModel> GetById(int id)
    {
        var project = _context.Projects
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .Include(p => p.Comments)
                .ThenInclude(c => c.User)
            .SingleOrDefault(p => p.Id == id);

        if (project is null)
        {
            return ResultViewModel<ProjectViewModel>.Error("Projeto não existe");
        }

        var model = ProjectViewModel.FromEntity(project);

        return ResultViewModel<ProjectViewModel>.Success(model);
    }

    public ResultViewModel<int> Insert(CreateProjectInputModel model)
    {
        var project = model.ToEntity();

        _context.Projects.Add(project);
        _context.SaveChanges();

        return ResultViewModel<int>.Success(project.Id);
    }

    public ResultViewModel InsertComment(int id, CreateProjectCommentInputModel model)
    {
        var projects = _context.Projects.SingleOrDefault(p => p.Id == id);

        if (projects is null)
        {
            return ResultViewModel.Error("Projeto não encontrado");
        }

        var comment = new ProjectComment(model.Content, model.IdProject, model.IdUser);

        _context.ProjectComments.Add(comment);
        _context.SaveChanges();

        return ResultViewModel.Success();
    }

    public ResultViewModel Start(int id)
    {
        var project = _context.Projects.SingleOrDefault(p => p.Id == id);

        if (project is null)
        {
            return ResultViewModel.Error("Projeto não existe");
        }

        project.Start();
        _context.Projects.Update(project);
        _context.SaveChanges();

        return ResultViewModel.Success();
    }

    public ResultViewModel Update(UpdateProjectInputModel model)
    {
        var projects = _context.Projects.SingleOrDefault(p => p.Id == model.IdProject);

        if (projects is null)
        {
            return ResultViewModel.Error("Projeto não existe");
        }

        projects.Update(model.Title, model.Description, model.TotalCost);

        _context.Projects.Update(projects);
        _context.SaveChanges();

        return ResultViewModel.Success();
    }
}
