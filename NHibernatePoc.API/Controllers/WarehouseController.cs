using Microsoft.AspNetCore.Mvc;
using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Domain.Interfaces;

namespace NHibernatePoc.API.Controllers
{
    public class WarehouseController : BaseController<Warehouse, IWarehouseService>
    {
        public WarehouseController(IWarehouseService service) : base(service)
        {
        }
    }
}
