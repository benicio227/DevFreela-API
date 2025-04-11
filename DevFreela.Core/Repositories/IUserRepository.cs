using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IUserRepository
    {
        public Task<User?> GetById(int id);
        public Task<int> Add(User user);
        public Task<Skill?> AddPostSkill(int id);
        public Task<UserSkill> AddUserSkill(UserSkill user);
    }
}
