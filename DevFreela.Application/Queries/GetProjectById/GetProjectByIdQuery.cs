using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetProjectById;
public class GetProjectByIdQuery : IRequest<ResultViewModel<ProjectViewModel>>
{
    public GetProjectByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}

public class GetProjectByIdHanlder : IRequestHandler<GetProjectByIdQuery, ResultViewModel<ProjectViewModel>>
{
    private readonly DevFreelaDbContext _context;
    public GetProjectByIdHanlder(DevFreelaDbContext context)
    {
        _context = context;
    }
    public async Task<ResultViewModel<ProjectViewModel>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .Include(p => p.Comments)
            .ThenInclude(c => c.User)
            .SingleOrDefaultAsync(p => p.Id == request.Id);

        if (project is null)
        {
            return ResultViewModel<ProjectViewModel>.Error("Projeto não existe");
        }

        var model = ProjectViewModel.FromEntity(project);

        return ResultViewModel<ProjectViewModel>.Success(model);
    }
}
