using MongoDB.Bson;
using ProjectService.Models;

namespace ProjectService.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project> GetProjectById(string id);
        Task CreateProject(Project project);
        Task<bool> UpdateProject(string id, Project project);
        Task<bool> DeleteProject(string id);
        Task<List<Project>> GetProjectsByUserId(int userId);
    }
}
