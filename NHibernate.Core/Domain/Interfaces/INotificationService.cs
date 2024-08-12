namespace NHibernatePoc.Core.Domain.Interfaces
{
    public interface INotificationService
    {
        void NotifyInventoryLow(int partId, int availableQuantity);
        void NotifyOrderStatusChanged(int orderId, string newStatus);
    }
}
