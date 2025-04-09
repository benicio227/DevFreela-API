using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.SkillFolder.InsertSkill;
public class InsertSkillCommand : IRequest<ResultViewModel<int>>
{
    public InsertSkillCommand(string description)
    {
        Description = description;
    }

    public string Description { get; set; }

    public static CreateSkillInputModel FromEntity(Skill skill)
    => new(skill.Description);

    public Skill ToEntity()
    => new(Description);
}
