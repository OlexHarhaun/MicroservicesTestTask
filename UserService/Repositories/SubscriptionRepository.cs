using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Interfaces;
using UserService.Models;

namespace UserService.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly UserContext _context;

        public SubscriptionRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<Subscription> GetSubscriptionById(int id)
        {
            return await _context.Subscriptions.FindAsync(id);
        }

        public async Task CreateSubscription(Subscription subscription)
        {
            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateSubscription(int id, Subscription subscription)
        {
            _context.Entry(subscription).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Subscriptions.AnyAsync(e => e.Id == id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteSubscription(int id)
        {
            var subscription = await _context.Subscriptions.FindAsync(id);
            if (subscription == null)
            {
                return false;
            }

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
