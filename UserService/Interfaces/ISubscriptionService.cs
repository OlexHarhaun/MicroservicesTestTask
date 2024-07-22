using UserService.Models;

namespace UserService.Interfaces
{
    public interface ISubscriptionService
    {
        Task<Subscription> GetSubscriptionById(int id);
        Task CreateSubscription(Subscription subscription);
        Task<bool> UpdateSubscription(int id, Subscription subscription);
        Task<bool> DeleteSubscription(int id);
    }
}
