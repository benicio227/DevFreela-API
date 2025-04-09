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
        var user = await _context.Skills.SingleOrDefaultAsync(u => u.Id == request.Id);

        if (user is null)
        {
            return (ResultViewModel<UserSkillsInputModel>)ResultViewModel.Error("Usuário não encontrado");
        }

        foreach (var skillId in request.SkillsIds)
        {
            var skill = _context.Skills.SingleOrDefault(s => s.Id == skillId);

            if (skill is null)
            {
                return (ResultViewModel<UserSkillsInputModel>)ResultViewModel.Error($"Skill com Id {skillId} não encontrada");
            }

            var userSkill = new UserSkill(request.Id, skillId);

            _context.UserSkills.Add(userSkill);
        }

        _context.SaveChanges();

        return (ResultViewModel<UserSkillsInputModel>)ResultViewModel.Success();
    }
}
