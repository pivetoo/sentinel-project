using Microsoft.AspNetCore.Mvc;
using Sentinel.API.Controllers.Base;
using Sentinel.Domain.Entities;
using Sentinel.Domain.Services;

namespace Sentinel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioEmpresaController : CrudApi<UsuarioEmpresa>
    {
        private readonly UsuarioEmpresaService _usuarioEmpresaService;

        public UsuarioEmpresaController(UsuarioEmpresaService usuarioEmpresaService) : base(usuarioEmpresaService)
        {
            _usuarioEmpresaService = usuarioEmpresaService;
        }

        [HttpGet("empresa/{empresaId:long}")]
        public IActionResult ObterPorEmpresa(long empresaId)
        {
            var usuarios = _usuarioEmpresaService.ObterPorEmpresa(empresaId);
            return Ok(usuarios);
        }

        [HttpPost("{id:long}/papeis/{papelId:long}")]
        public IActionResult AtribuirPapel(long id, long papelId)
        {
            try
            {
                _usuarioEmpresaService.AtribuirPapel(id, papelId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id:long}/papeis/{papelId:long}")]
        public IActionResult RemoverPapel(long id, long papelId)
        {
            try
            {
                _usuarioEmpresaService.RemoverPapel(id, papelId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("{id:long}/ativar")]
        public IActionResult AtivarUsuario(long id)
        {
            try
            {
                _usuarioEmpresaService.AtivarUsuarioEmpresa(id);
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
                _usuarioEmpresaService.InativarUsuarioEmpresa(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}
