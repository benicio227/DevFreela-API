using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.SkillFolder.InsertSkill;
public class CreateSkillInputModel : IRequest<ResultViewModel<int>>
{
    public CreateSkillInputModel(string description)
    {
        Description = description;
    }
    public string Description { get; set; }

    //public static CreateSkillInputModel FromEntity(Skill skill)
     //   => new(skill.Description);

    public Skill ToEntity()
    => new(Description);
}
