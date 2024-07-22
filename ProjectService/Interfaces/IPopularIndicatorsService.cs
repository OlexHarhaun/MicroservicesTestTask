using ProjectService.Responses;

namespace ProjectService.Interfaces
{
    public interface IPopularIndicatorsService
    {
        public Task<PopularIndicatorsResponse> GetPopularIndicatorsAsync(string subscriptionType);
    }
}
