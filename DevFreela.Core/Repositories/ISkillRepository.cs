using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories;
public interface ISkillRepository
{
    public Task<int> Add(Skill skill);
    public Task<List<Skill>> GetAll();
    public Task<Skill?> GetById(int id);
}
