using UserService.Models;

namespace UserService.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserById(int id);
        Task CreateUser(User user);
        Task<bool> UpdateUser(int id, User user);
        Task<bool> DeleteUser(int id);
        Task<List<int>> GetUserIdsBySubscriptionTypeAsync(string subscriptionType);
    }
}
