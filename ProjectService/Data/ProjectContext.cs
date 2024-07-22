using MongoDB.Driver;
using ProjectService.Models;

namespace ProjectService.Data
{
    public class ProjectContext
    {
        private readonly IMongoDatabase _database;

        public ProjectContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDB:ConnectionString"]);
            _database = client.GetDatabase(configuration["MongoDB:DatabaseName"]);
        }

        public IMongoCollection<Project> Projects => _database.GetCollection<Project>("projects");
    }
}
