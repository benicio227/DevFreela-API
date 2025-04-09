using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.SkillFolder.InsertSkill;
public class InsertSkillHandler : IRequestHandler<CreateSkillInputModel, ResultViewModel<int>>
{
    private readonly DevFreelaDbContext _context;

    public InsertSkillHandler(DevFreelaDbContext context)
    {
        _context = context;
    }
    public async Task<ResultViewModel<int>> Handle(CreateSkillInputModel request, CancellationToken cancellationToken)
    {
        var skill = request.ToEntity();

        await _context.Skills.AddAsync(skill);
        await _context.SaveChangesAsync();

        return ResultViewModel<int>.Success(skill.Id);
    }
}
