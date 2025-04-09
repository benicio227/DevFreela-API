using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetAllProjects;

public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, ResultViewModel<List<ProjectItemViewModel>>>
{
    private readonly DevFreelaDbContext _context;
    public GetAllProjectsHandler(DevFreelaDbContext context)
    {
        _context = context;
    }
    public async Task<ResultViewModel<List<ProjectItemViewModel>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _context.Projects
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .Where(p => !p.IsDeleted)
            .ToListAsync();

        var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();
        // O método Select pega cada objeto da lista de projects e passa para o método FromEntity(que está espe
        // rando no parâmetro um objeto do tipo Project)
        // E dentro do método é feita a conversão das propriedades da entidade Project para o model

        return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
    }
}

