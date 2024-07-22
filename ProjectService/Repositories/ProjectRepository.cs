using MongoDB.Bson;
using MongoDB.Driver;
using ProjectService.Data;
using ProjectService.Interfaces;
using ProjectService.Models;

namespace ProjectService.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ProjectContext _context;

        public ProjectRepository(ProjectContext context)
        {
            _context = context;
        }

        public async Task<Project> GetProjectById(String id)
        {
            return await _context.Projects.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateProject(Project project)
        {
            await _context.Projects.InsertOneAsync(project);
        }

        public async Task<bool> UpdateProject(String id, Project project)
        {
            var result = await _context.Projects.ReplaceOneAsync(p => p.Id == id, project);
            return result.MatchedCount > 0;
        }

        public async Task<bool> DeleteProject(String id)
        {
            var result = await _context.Projects.DeleteOneAsync(p => p.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<List<Project>> GetProjectsByUserId(int userId)
        {
            return await _context.Projects.Find(p => p.UserId == userId).ToListAsync();
        }
    }
}
