using UserService.Interfaces;
using UserService.Models;

namespace UserService.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ILogger<SubscriptionService> _logger;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository, ILogger<SubscriptionService> logger)
        {
            _subscriptionRepository = subscriptionRepository;
            _logger = logger;
        }

        public async Task<Subscription> GetSubscriptionById(int id)
        {
            try
            {
                return await _subscriptionRepository.GetSubscriptionById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting subscription by id");
                throw;
            }
        }

        public async Task CreateSubscription(Subscription subscription)
        {
            try
            {
                await _subscriptionRepository.CreateSubscription(subscription);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating subscription");
                throw;
            }
        }

        public async Task<bool> UpdateSubscription(int id, Subscription subscription)
        {
            try
            {
                return await _subscriptionRepository.UpdateSubscription(id, subscription);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating subscription");
                throw;
            }
        }

        public async Task<bool> DeleteSubscription(int id)
        {
            try
            {
                return await _subscriptionRepository.DeleteSubscription(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting subscription");
                throw;
            }
        }
    }
}
