using DigitalBookStoreManagement.Model;

namespace DigitalBookStoreManagement.Repository
{
    public interface I_NotificationRepository
    {
        public Task AddNotification(string message);
        public Task<IEnumerable<Notification>> GetAllNotifications();

    }
}
