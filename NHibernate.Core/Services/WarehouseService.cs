using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Domain.Interfaces;

namespace NHibernatePoc.Core.Services
{
    public class WarehouseService : Service<Warehouse>, IWarehouseService
    {
        public WarehouseService(IWarehouseRepository warehouseRepository): base(warehouseRepository)
        {
        }
    }
}
