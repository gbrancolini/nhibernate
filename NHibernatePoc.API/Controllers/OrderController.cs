using Microsoft.AspNetCore.Mvc;
using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Domain.Exceptions;
using NHibernatePoc.Core.Domain.Interfaces;

namespace NHibernatePoc.API.Controllers
{
    public class OrderController : BaseController<Order, IOrderService>
    {
        public OrderController(IOrderService service) : base(service)
        {
        }

        // POST: api/Order/Create
        [HttpPost("Create")]
        public ActionResult<Order> CreateOrder([FromBody] IEnumerable<OrderDetail> orderDetails)
        {
            try
            {
                var order = _service.CreateOrder(orderDetails);
                return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Order/{id}/Process
        [HttpPost("{id}/Process")]
        public IActionResult ProcessOrder(int id)
        {
            try
            {
                _service.ProcessOrder(id);
                return Ok($"Order {id} processed successfully.");
            }
            catch (InvalidOrderOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Order/{id}/Cancel
        [HttpPost("{id}/Cancel")]
        public IActionResult CancelOrder(int id)
        {
            try
            {
                _service.CancelOrder(id);
                return Ok($"Order {id} cancelled successfully.");
            }
            catch (InvalidOrderOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
