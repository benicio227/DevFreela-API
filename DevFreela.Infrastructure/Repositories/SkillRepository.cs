using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Infrastructure.Repositories;
public class SkillRepository : ISkillRepository
{
    private readonly DevFreelaDbContext _context;

    public SkillRepository(DevFreelaDbContext context)
    {
        _context = context;
    }
    public async Task<int> Add(Skill skill)
    {
        var skillExist = await _context.Skills.AddAsync(skill);

        return skill.Id;
    }

    public Task<List<Skill>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Skill?> GetById(int id)
    {
        throw new NotImplementedException();
    }
}
