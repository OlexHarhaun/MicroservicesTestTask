using UserService.Interfaces;
using UserService.Models;
using UserService.Repositories;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<User> GetUserById(int id)
        {
            try
            {
                return await _userRepository.GetUserById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by id");
                throw;
            }
        }

        public async Task CreateUser(User user)
        {
            try
            {
                await _userRepository.CreateUser(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                throw;
            }
        }

        public async Task<bool> UpdateUser(int id, User user)
        {
            try
            {
                return await _userRepository.UpdateUser(id, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user");
                throw;
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                return await _userRepository.DeleteUser(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user");
                throw;
            }
        }

        public async Task<List<int>> GetUserIdsBySubscriptionTypeAsync(string subscriptionType)
        {
            return await _userRepository.GetUserIdsBySubscriptionTypeAsync(subscriptionType);
        }
    }
}
