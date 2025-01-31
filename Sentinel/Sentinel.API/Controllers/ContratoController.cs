using Microsoft.AspNetCore.Mvc;
using Sentinel.API.Controllers.Base;
using Sentinel.Domain.DTO;
using Sentinel.Domain.Entities;
using Sentinel.Domain.Services;
using Sentinel.Domain.Services.Base;

namespace Sentinel.API.Controllers
{
    public class ContratoController : CrudApi<Contrato>
    {
        private readonly ContratoService _contratoService;

        public ContratoController(CrudService<Contrato> service, ContratoService contratoService = null) : base(service)
        {
            _contratoService = contratoService;
        }

        [HttpPost("criar")]
        public IActionResult CriarContrato([FromBody] CriarContratoDto dto)
        {
            _contratoService.CriarContrato(dto);
            return Ok(new { message = "Contrato criado com sucesso." });
        }
    }
}
