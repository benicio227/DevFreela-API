using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetAllSkills;
public class GetAllSkillsHandler : IRequestHandler<GetAllSkillsQuery, ResultViewModel<List<CreateSkillInputModel>>>
{
    private readonly ISkillRepository _repository;

    public GetAllSkillsHandler(ISkillRepository repository)
    {
        _repository = repository;
    }
    public async Task<ResultViewModel<List<CreateSkillInputModel>>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
    {
        var skills = await _repository.GetAll();

        var model = skills.Select(CreateSkillInputModel.FromEntity).ToList();

        return ResultViewModel<List<CreateSkillInputModel>>.Success(model);
    }
}
