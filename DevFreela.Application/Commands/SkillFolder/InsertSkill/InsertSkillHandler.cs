using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.SkillFolder.InsertSkill;
public class InsertSkillHandler : IRequestHandler<CreateSkillInputModel, ResultViewModel<int>>
{
    private readonly ISkillRepository _repository;

    public InsertSkillHandler(ISkillRepository repository)
    {
        _repository = repository;
    }
    public async Task<ResultViewModel<int>> Handle(CreateSkillInputModel request, CancellationToken cancellationToken)
    {
        var skill = request.ToEntity();

        await _repository.Add(skill);

        return ResultViewModel<int>.Success(skill.Id);
    }
}
