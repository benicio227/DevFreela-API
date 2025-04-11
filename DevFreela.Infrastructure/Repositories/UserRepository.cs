using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly DevFreelaDbContext _context;

    public UserRepository(DevFreelaDbContext context)
    {
        _context = context;
    }

    public async Task<int> Add(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user.Id;
    }

    public async Task<Skill?> AddPostSkill(int id)
    {
        var skill = await _context.Skills.SingleOrDefaultAsync(u => u.Id == id);

        return skill;
    }

    public async Task<UserSkill> AddUserSkill(UserSkill userSkill)
    {
        var userSkillExist = await _context.AddAsync(userSkill);

        _context.SaveChanges();

        return userSkill;
    }

    public async Task<User?> GetById(int id)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);

        return user;
    }
}
