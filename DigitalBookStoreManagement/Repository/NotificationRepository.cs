using DigitalBookStoreManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace DigitalBookStoreManagement.Repository
{
    public class NotificationRepository:I_NotificationRepository
    {
        private readonly UserContext _context;
        public NotificationRepository(UserContext context)
        {
            _context = context;
        }

        public async Task AddNotification(string message)
        {
            var notification = new Notification { Message = message };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Notification>> GetAllNotifications()
        {
            return await _context.Notifications.ToListAsync();
        }
    }
}
