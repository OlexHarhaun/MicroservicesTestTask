using MongoDB.Driver;
using ProjectService.Interfaces;
using ProjectService.Models;
using ProjectService.Responses;
using System.Net.Http;
using System.Text.Json;

namespace ProjectService.Services
{
    public class PopularIndicatorsService : IPopularIndicatorsService
    {
        private readonly ILogger<PopularIndicatorsService> _logger;
        private readonly HttpClient _httpClient;
        private readonly IMongoCollection<Project> _projectCollection;
        //prod move this to appsettings as long as localy i have issues with ips
        private readonly string getUserIdsBySubscriptionTypeEndpoint = "https://localhost:7141/api/User/subscriptionType/";

        public PopularIndicatorsService(ILogger<PopularIndicatorsService> logger, HttpClient httpClient,IMongoCollection<Project> projectCollection)
        {
            _logger = logger;
            _httpClient = httpClient;
            _projectCollection = projectCollection;
        }
        private async Task<List<int>> GetUserIdsBySubscriptionTypeAsync(string subscriptionType)
        {
            var userIds = new List<int>();

            try
            {
                var response = await _httpClient.GetAsync($"{getUserIdsBySubscriptionTypeEndpoint}{subscriptionType}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    userIds = JsonSerializer.Deserialize<List<int>>(content);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching user IDs by subscription type");
                throw;
            }

            return userIds;
        }

        public async Task<PopularIndicatorsResponse> GetPopularIndicatorsAsync(string subscriptionType)
        {
            try
            {
                var userIds = await GetUserIdsBySubscriptionTypeAsync(subscriptionType);

                if (userIds.Count == 0)
                {
                    _logger.LogWarning("No user IDs found for subscription type: {SubscriptionType}", subscriptionType);
                    return new PopularIndicatorsResponse { Indicators = new List<IndicatorCount>() };
                }

                var filter = Builders<Project>.Filter.In(p => p.UserId, userIds) &
                             Builders<Project>.Filter.ElemMatch(p => p.Charts, c => c.Indicators.Any());
                var projects = await _projectCollection.Find(filter).ToListAsync();

                var indicators = projects
                    .SelectMany(p => p.Charts)
                    .SelectMany(c => c.Indicators)
                    .GroupBy(i => i.Name)
                    .Select(g => new
                    {
                        Name = g.Key,
                        Count = g.Count()
                    })
                    .OrderByDescending(i => i.Count)
                    .Take(3)
                    .ToList();

                return new PopularIndicatorsResponse
                {
                    Indicators = indicators.Select(i => new IndicatorCount
                    {
                        Name = i.Name,
                        Used = i.Count,
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving popular indicators.");
                throw;
            }
        }
    }
}
