using Microsoft.AspNetCore.Mvc;
using Sentinel.API.Controllers.Base;
using Sentinel.Domain.DTO;
using Sentinel.Domain.Entities;
using Sentinel.Domain.Services;
using Sentinel.Domain.Services.Base;

namespace Sentinel.API.Controllers
{
    public class PermissaoController : CrudApi<Permissao>
    {
        private readonly PermissaoService _permissaoService;

        public PermissaoController(CrudService<Permissao> service, PermissaoService permissaoService) : base(service)
        {
            _permissaoService = permissaoService;
        }

        [HttpGet]
        public override IActionResult GetAll()
        {
            var permissoes = _permissaoService.GetAll();
            var dtos = permissoes.Select(p => new PermissaoDTO
            {
                Id = p.Id,
                Recurso = p.Recurso,
                Acao = p.Acao,
                Descricao = p.Descricao
            });
            return Ok(dtos);
        }

        [HttpGet("{id:long}")]
        public override IActionResult GetById(long id)
        {
            try
            {
                var permissao = _permissaoService.GetById(id);
                var dto = new PermissaoDTO
                {
                    Id = permissao.Id,
                    Recurso = permissao.Recurso,
                    Acao = permissao.Acao,
                    Descricao = permissao.Descricao
                };
                return Ok(dto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpGet("recurso/{recurso}")]
        public IActionResult GetByRecurso(string recurso)
        {
            var permissoes = _permissaoService.ObterPorRecurso(recurso);
            var dtos = permissoes.Select(p => new PermissaoDTO
            {
                Id = p.Id,
                Recurso = p.Recurso,
                Acao = p.Acao,
                Descricao = p.Descricao
            });
            return Ok(dtos);
        }

        [HttpPost]
        public override IActionResult Add([FromBody] Permissao permissao)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _permissaoService.Add(permissao);
                var dto = new PermissaoDTO
                {
                    Id = permissao.Id,
                    Recurso = permissao.Recurso,
                    Acao = permissao.Acao,
                    Descricao = permissao.Descricao
                };
                return CreatedAtAction(nameof(GetById), new { id = permissao.Id }, dto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id:long}")]
        public override IActionResult Update(long id, [FromBody] Permissao permissao)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != permissao.Id)
                return BadRequest(new { Message = "ID mismatch." });

            try
            {
                _permissaoService.Update(permissao);
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
    }
}
