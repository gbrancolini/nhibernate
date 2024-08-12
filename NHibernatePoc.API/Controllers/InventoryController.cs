using Microsoft.AspNetCore.Mvc;
using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Domain.Interfaces;

namespace NHibernatePoc.API.Controllers
{
    public class InventoryController : BaseController<Inventory, IInventoryService>
    {
        public InventoryController(IInventoryService service) : base(service)
        {
        }

        // Endpoint específico para actualizar la cantidad de inventario de una parte
        [HttpPost("UpdateForPart/{partId}/{deltaQuantity}")]
        public IActionResult UpdateInventoryForPart(int partId, int deltaQuantity)
        {
            try
            {
                _service.UpdateInventoryForPart(partId, deltaQuantity);
                return Ok($"Inventory updated successfully for part ID {partId}.");
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Error updating inventory: {ex.Message}");
            }
        }

        // Endpoint para obtener inventarios con baja cantidad
        [HttpGet("Low/{threshold}")]
        public ActionResult<IEnumerable<Inventory>> GetLowInventories(int threshold)
        {
            try
            {
                var inventories = _service.GetLowInventories(threshold);
                return Ok(inventories);
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Error retrieving low inventories: {ex.Message}");
            }
        }
    }
}
