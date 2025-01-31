using Microsoft.AspNetCore.Mvc;
using Sentinel.API.Controllers.Base;
using Sentinel.Domain.DTO;
using Sentinel.Domain.Entities;
using Sentinel.Domain.Services;
using Sentinel.Domain.Services.Base;

namespace Sentinel.API.Controllers
{
    public class PapelController : CrudApi<Papel>
    {
        private readonly PapelService _papelService;

        public PapelController(CrudService<Papel> service, PapelService papelService) : base(service)
        {
            _papelService = papelService;
        }

        [HttpGet]
        public override IActionResult GetAll()
        {
            var papeis = _papelService.GetAll();
            var dtos = papeis.Select(p => new PapelDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Descricao = p.Descricao,
                PermissaoIds = p.Permissoes.Select(perm => perm.Id).ToList()
            });
            return Ok(dtos);
        }

        [HttpGet("{id:long}")]
        public override IActionResult GetById(long id)
        {
            try
            {
                var papel = _papelService.GetById(id);
                var dto = new PapelDTO
                {
                    Id = papel.Id,
                    Nome = papel.Nome,
                    Descricao = papel.Descricao,
                    PermissaoIds = papel.Permissoes.Select(p => p.Id).ToList()
                };
                return Ok(dto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public override IActionResult Add([FromBody] Papel papel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _papelService.Add(papel);

                var responseDto = new PapelDTO
                {
                    Id = papel.Id,
                    Nome = papel.Nome,
                    Descricao = papel.Descricao,
                    PermissaoIds = papel.Permissoes.Select(p => p.Id).ToList()
                };

                return CreatedAtAction(nameof(GetById), new { id = papel.Id }, responseDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id:long}")]
        public override IActionResult Update(long id, [FromBody] Papel papel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != papel.Id)
                return BadRequest(new { Message = "ID mismatch." });

            try
            {
                _papelService.Update(papel);
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

        [HttpPost("{id:long}/permissoes/{permissaoId:long}")]
        public IActionResult AtribuirPermissao(long id, long permissaoId)
        {
            try
            {
                _papelService.AtribuirPermissao(id, permissaoId);
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

        [HttpDelete("{id:long}/permissoes/{permissaoId:long}")]
        public IActionResult RemoverPermissao(long id, long permissaoId)
        {
            try
            {
                _papelService.RemoverPermissao(id, permissaoId);
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
