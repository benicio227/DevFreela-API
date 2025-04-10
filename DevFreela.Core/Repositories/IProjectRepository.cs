using DevFreela.Core.Entities;
using System.Runtime.InteropServices;

namespace DevFreela.Core.Repositories;
public interface IProjectRepository
{
    Task<List<Project>> GetAll();
    Task<Project?> GetDeatilsById(int id);
    Task<Project?> GetById(int id);
    Task<int> Add(Project project);
    Task Update(Project project);
    Task AddComment(ProjectComment comment);
    Task<bool> Exists(int id);
}
