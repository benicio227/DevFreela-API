using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.UserFolder.InsertSkill;
public class InsertSkillHandler : IRequestHandler<InsertSkillCommand, ResultViewModel<UserSkillsInputModel>>
{
    private readonly IUserRepository _repository;
    public InsertSkillHandler(IUserRepository repository)
    {
        _repository = repository;
    }
    public async Task<ResultViewModel<UserSkillsInputModel>> Handle(InsertSkillCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetById(request.Id);

        if (user is null)
        {
            return (ResultViewModel<UserSkillsInputModel>)ResultViewModel.Error("Usuário não encontrado");
        }

        foreach (var skillId in request.SkillsIds)
        {
            var skill = await _repository.AddPostSkill(skillId);

            if (skill is null)
            {
                return (ResultViewModel<UserSkillsInputModel>)ResultViewModel.Error($"Skill com Id {skillId} não encontrada");
            }

            var userSkill = new UserSkill(request.Id, skillId);

            await _repository.AddUserSkill(userSkill);
        }

        return (ResultViewModel<UserSkillsInputModel>)ResultViewModel.Success();
    }
}
