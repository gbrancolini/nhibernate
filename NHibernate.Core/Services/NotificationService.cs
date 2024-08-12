using NHibernatePoc.Core.Domain.Interfaces;

namespace NHibernatePoc.Core.Services
{
    public class NotificationService : INotificationService
    {
        public void NotifyInventoryLow(int partId, int availableQuantity)
        {
            Console.WriteLine($"Alert: Inventory low for part ID {partId}. Only {availableQuantity} items left.");
        }

        public void NotifyOrderStatusChanged(int orderId, string newStatus)
        {
            Console.WriteLine($"Notification: Order {orderId} status changed to {newStatus}.");
        }
    }
}
