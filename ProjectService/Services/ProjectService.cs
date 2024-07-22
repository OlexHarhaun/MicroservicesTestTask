using MongoDB.Bson;
using ProjectService.Interfaces;
using ProjectService.Models;

namespace ProjectService.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ILogger<ProjectService> _logger;

        public ProjectService(IProjectRepository projectRepository, ILogger<ProjectService> logger)
        {
            _projectRepository = projectRepository;
            _logger = logger;
        }

        public async Task<Project> GetProjectById(string id)
        {
            try
            {
                return await _projectRepository.GetProjectById(new String(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting project by id");
                throw;
            }
        }

        public async Task CreateProject(Project project)
        {
            try
            {
                await _projectRepository.CreateProject(project);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating project");
                throw;
            }
        }

        public async Task<bool> UpdateProject(string id, Project project)
        {
            try
            {
                return await _projectRepository.UpdateProject(new String(id), project);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating project");
                throw;
            }
        }

        public async Task<bool> DeleteProject(string id)
        {
            try
            {
                return await _projectRepository.DeleteProject(new String(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting project");
                throw;
            }
        }

        public async Task<List<Project>> GetProjectsByUserId(int userId)
        {
            try
            {
                return await _projectRepository.GetProjectsByUserId(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting projects by user id");
                throw;
            }
        }
    }
}
