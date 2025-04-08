using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services;
public class UserService : IUserService
{
    private readonly DevFreelaDbContext _context;
    public UserService(DevFreelaDbContext context)
    {
        _context = context;
    }
    public ResultViewModel<UserViewModel> GetById(int id)
    {
        var user = _context.Users
            .Include(u => u.Skills) // Carrega o objeto(que é uma lista de UserSkill)
                .ThenInclude(u => u.Skill) // Inclui o objeto Skill dentro de cada UserSkill
            .SingleOrDefault(u => u.Id == id);

        if(user is null)
        {
            return ResultViewModel<UserViewModel>.Error("Usuário não existe");
        }

        var model = UserViewModel.FromEntity(user);

        return ResultViewModel<UserViewModel>.Success(model);
    }

    public ResultViewModel<int> Insert(CreateUserInputModel model)
    {
        var user = model.ToEntity();

        _context.Users.Add(user);
        _context.SaveChanges();

        return ResultViewModel<int>.Success(user.Id);
    }

    public ResultViewModel InsertSkill(int id, UserSkillsInputModel model)
    {
        var user = _context.Skills.SingleOrDefault(u => u.Id == id);

        if (user is null)
        {
            return ResultViewModel.Error("Usuário não encontrado");
        }

        foreach(var skillId in model.SkillsIds)
        {
            var skill = _context.Skills.SingleOrDefault(s => s.Id == skillId);

            if (skill is null)
            {
                return ResultViewModel.Error($"Skill com Id {skillId} não encontrada");
            }

            var userSkill = new UserSkill(model.Id, skillId);

            _context.UserSkills.Add(userSkill);
        }

        _context.SaveChanges();

        return ResultViewModel.Success();
    }
}
