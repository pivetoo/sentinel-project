using Sentinel.API.Controllers.Base;
using Sentinel.Domain.Entities;
using Sentinel.Domain.Services.Base;

namespace Sentinel.API.Controllers
{
    public class EmpresaController : CrudApi<Empresa>
    {
        public EmpresaController(CrudService<Empresa> service) : base(service)
        {
        }
    }
}