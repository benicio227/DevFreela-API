using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetUserById;
public class GetUserByIdQuery : IRequest<ResultViewModel<UserViewModel>>
{
    public GetUserByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}

public class GetProjectByIdQuery : IRequestHandler<GetUserByIdQuery, ResultViewModel<UserViewModel>>
{
    private readonly DevFreelaDbContext _context;
    public GetProjectByIdQuery(DevFreelaDbContext context)
    {
        _context = context;
    }
    public async Task<ResultViewModel<UserViewModel>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Skills) // Carrega o objeto(que é uma lista de UserSkill)
            .ThenInclude(u => u.Skill) // Inclui o objeto Skill dentro de cada UserSkill
            .SingleOrDefaultAsync(u => u.Id == request.Id);

        if (user is null)
        {
            return ResultViewModel<UserViewModel>.Error("Usuário não existe");
        }

        var model = UserViewModel.FromEntity(user);

        return ResultViewModel<UserViewModel>.Success(model);
    }
}
