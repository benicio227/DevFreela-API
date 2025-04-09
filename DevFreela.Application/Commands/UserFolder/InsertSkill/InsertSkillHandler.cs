using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.UserFolder.InsertSkill;
public class InsertSkillHandler : IRequestHandler<InsertSkillCommand, ResultViewModel<UserSkillsInputModel>>
{
    private readonly DevFreelaDbContext _context;
    public InsertSkillHandler(DevFreelaDbContext context)
    {
        _context = context;
    }
    public async Task<ResultViewModel<UserSkillsInputModel>> Handle(InsertSkillCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == request.Id);

        if (user is null)
        {
            return (ResultViewModel<UserSkillsInputModel>)ResultViewModel.Error("Usuário não encontrado");
        }

        foreach (var skillId in request.SkillsIds)
        {
            var skill = await _context.Skills.SingleOrDefaultAsync(s => s.Id == skillId);

            if (skill is null)
            {
                return (ResultViewModel<UserSkillsInputModel>)ResultViewModel.Error($"Skill com Id {skillId} não encontrada");
            }

            var userSkill = new UserSkill(request.Id, skillId);

            await _context.UserSkills.AddAsync(userSkill);
        }

        await _context.SaveChangesAsync();

        return (ResultViewModel<UserSkillsInputModel>)ResultViewModel.Success();
    }
}
