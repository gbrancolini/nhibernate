using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Domain.Exceptions;
using NHibernatePoc.Core.Domain.Interfaces;
using NHibernatePoc.Core.Specifications.InventorySpecifications;

namespace NHibernatePoc.Core.Services
{
    public class InventoryService : Service<Inventory>, IInventoryService
    {
        private readonly INotificationService _notificationService;
        private readonly IValidationService _validationService;
        private readonly IPartRepository _partRepository;

        /// <summary>
        /// The service receives repository interfaces for both Inventory and Part,
        /// allowing it to decouple data access management from business logic.
        /// This dependency injection facilitates easier unit testing and maintenance.
        /// </summary>
        /// <param name="inventoryRepository"></param>
        /// <param name="partRepository"></param>
        public InventoryService(IInventoryRepository inventoryRepository, INotificationService notificationService, 
            IValidationService validationService, IPartRepository partRepository): base(inventoryRepository) 
        {
            _notificationService = notificationService;
            _validationService = validationService;
            _partRepository = partRepository;
        }

        /// <summary>
        /// This method adjusts the inventory quantity for a specific part based on deltaQuantity,
        /// which can be either positive (adding stock) or negative (reducing stock).
        /// Before making any updates, it checks if the inventory entry for the part exists and ensures that the 
        /// new quantity will not fall below zero.
        /// If either of these checks fails, an exception is thrown to prevent the inventory
        /// from entering an invalid state. This ensures data integrity and prevents business rule violations,
        /// such as selling more parts than are available in stock.
        /// </summary>
        /// <param name="partId"></param>
        /// <param name="deltaQuantity"></param>
        /// <exception cref="InventoryShortageException"></exception>
        public void UpdateInventoryForPart(int partId, int deltaQuantity)
        {
            var inventory = _repository.GetAll().FirstOrDefault(inv => inv.Part.Id == partId);
            if (inventory == null)
            {
                throw new InventoryShortageException(partId, deltaQuantity, 0);
            }

            int newQuantity = inventory.Quantity + deltaQuantity;
            if (newQuantity < 0)
            {
                throw new InventoryShortageException(partId, deltaQuantity, inventory.Quantity);
            }

            inventory.Quantity = newQuantity;
            _repository.Update(inventory);

            if (inventory.Quantity < 10)
            {
                _notificationService.NotifyInventoryLow(partId, inventory.Quantity);
            }
        }

        public override void Update(Inventory inventory)
        {
            _validationService.ValidateInventory(inventory);
            _repository.Update(inventory);
        }

        public IEnumerable<Inventory> GetLowInventories(int threshold)
        {
            var spec = new LowInventorySpecification(threshold);
            return _repository.FindBySpecification(spec.ToExpression());
        }
    }
}
