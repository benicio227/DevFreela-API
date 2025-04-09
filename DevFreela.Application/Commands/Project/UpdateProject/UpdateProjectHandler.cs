using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.Project.UpdateProject;
public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ResultViewModel>
{
    private readonly DevFreelaDbContext _context;
    public UpdateProjectHandler(DevFreelaDbContext context)
    {
        _context = context;
    }
    public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var projects = await _context.Projects.SingleOrDefaultAsync(p => p.Id == request.IdProject);

        if (projects is null)
        {
            return ResultViewModel.Error("Projeto não existe");
        }

        projects.Update(request.Title, request.Description, request.TotalCost);

        _context.Projects.Update(projects);
        await _context.SaveChangesAsync();

        return ResultViewModel.Success();
    }
}
