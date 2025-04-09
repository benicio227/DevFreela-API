using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services;
public class SkillService : ISkillService
{
    private readonly DevFreelaDbContext _context;

    public SkillService(DevFreelaDbContext context)
    {
        _context = context;
    }
    public ResultViewModel<List<CreateSkillInputModel>> GetAll()
    {
        var skills = _context.Skills.ToList();

        var model = skills.Select(CreateSkillInputModel.FromEntity).ToList();

        return ResultViewModel<List<CreateSkillInputModel>>.Success(model);
    }

    public ResultViewModel<int> Insert(CreateSkillInputModel model)
    {
 
        var skill = model.ToEntity();

        _context.Skills.Add(skill);
        _context.SaveChanges();

        return ResultViewModel<int>.Success(skill.Id);
    }
}
