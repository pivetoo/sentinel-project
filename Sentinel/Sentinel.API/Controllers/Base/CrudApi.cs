using Microsoft.AspNetCore.Mvc;
using Sentinel.Domain.Entities.Base;
using Sentinel.Domain.Services.Base;

namespace Sentinel.API.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class CrudApi<T> : ControllerBase where T : EntidadeBase
    {
        protected readonly CrudService<T> _service;

        public CrudApi(CrudService<T> service)
        {
            _service = service;
        }

        [HttpGet]
        public virtual IActionResult GetAll()
        {
            var entities = _service.GetAll();
            return Ok(entities);
        }

        [HttpGet("{id:long}")]
        public virtual IActionResult GetById(long id)
        {
            try
            {
                var entity = _service.GetById(id);
                return Ok(entity);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public virtual IActionResult Add([FromBody] T entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _service.Add(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        [HttpPut("{id:long}")]
        public virtual IActionResult Update(long id, [FromBody] T entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != entity.Id)
                return BadRequest(new { Message = "ID mismatch." });

            try
            {
                _service.Update(entity);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id:long}")]
        public virtual IActionResult Delete(long id)
        {
            try
            {
                _service.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}
