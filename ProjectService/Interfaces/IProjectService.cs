using ProjectService.Models;

namespace ProjectService.Interfaces
{
    public interface IProjectService
    {
        Task<Project> GetProjectById(string id);
        Task CreateProject(Project project);
        Task<bool> UpdateProject(string id, Project project);
        Task<bool> DeleteProject(string id);
        Task<List<Project>> GetProjectsByUserId(int userId);
    }
}
