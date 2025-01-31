using Microsoft.AspNetCore.Mvc;
using Sentinel.API.Controllers.Base;
using Sentinel.Domain.Entities;
using Sentinel.Domain.Services;

namespace Sentinel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioSentinelController : CrudApi<UsuarioSentinel>
    {
        private readonly UsuarioSentinelService _usuarioSentinelService;

        public UsuarioSentinelController(UsuarioSentinelService usuarioSentinelService) : base(usuarioSentinelService)
        {
            _usuarioSentinelService = usuarioSentinelService;
        }

        [HttpPost("{id:long}/ativar")]
        public IActionResult AtivarUsuario(long id)
        {
            try
            {
                _usuarioSentinelService.AtivarUsuario(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpPost("{id:long}/inativar")]
        public IActionResult InativarUsuario(long id)
        {
            try
            {
                _usuarioSentinelService.InativarUsuario(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}
