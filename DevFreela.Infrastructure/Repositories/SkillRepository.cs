using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<Skill>> GetAll()
    {
        var skills = await _context.Skills.ToListAsync();

        return skills;
    }

    public Task<Skill?> GetById(int id)
    {
        throw new NotImplementedException();
    }
}
