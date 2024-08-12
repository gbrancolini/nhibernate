using Microsoft.AspNetCore.Mvc;
using NHibernatePoc.Core.Domain.Interfaces;

namespace NHibernatePoc.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<TEntity, TService> : ControllerBase
        where TEntity : class, IEntity
        where TService : IService<TEntity>
    {
        protected readonly TService _service;

        public BaseController(TService service)
        {
            _service = service;
        }

        // GET: api/[controller]
        [HttpGet]
        public ActionResult<IEnumerable<TEntity>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        // GET: api/[controller]/5
        [HttpGet("{id}")]
        public ActionResult<TEntity> GetById(int id)
        {
            var entity = _service.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        // POST: api/[controller]
        [HttpPost]
        public ActionResult<TEntity> Add([FromBody] TEntity entity)
        {
            _service.Add(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        // PUT: api/[controller]/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TEntity entity)
        {
            if (entity.Id != id)
            {
                return BadRequest("ID mismatch");
            }

            var existingEntity = _service.GetById(id);
            if (existingEntity == null)
            {
                return NotFound();
            }

            _service.Update(entity);
            return NoContent();
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = _service.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }

            _service.Delete(id);
            return NoContent();
        }
    }
}
