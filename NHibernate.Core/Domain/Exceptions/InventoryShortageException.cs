namespace NHibernatePoc.Core.Domain.Exceptions
{
    public class InventoryShortageException : Exception
    {
        public InventoryShortageException(int partId, int requestedQuantity, int availableQuantity)
            : base($"Insufficient inventory for part ID {partId}. Requested: {requestedQuantity}, Available: {availableQuantity}.")
        {
        }
    }
}
