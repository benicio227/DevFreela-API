using DevFreela.Core.Entities;
using System.Net.Sockets;

namespace DevFreela.Application.Models;

public class CreateSkillInputModel
{

    public CreateSkillInputModel(string description)
    {
        Description = description;
    }
    public string Description {  get; set; }

    public static CreateSkillInputModel FromEntity(Skill skill)
        => new(skill.Description);

    public Skill ToEntity()
    => new(Description);
}
