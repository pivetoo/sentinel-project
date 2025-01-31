using Sentinel.API.Controllers.Base;
using Sentinel.Domain.Entities;
using Sentinel.Domain.Services.Base;

namespace Sentinel.API.Controllers
{
    public class PlanoController : CrudApi<Plano>
    {
        public PlanoController(CrudService<Plano> service) : base(service)
        {
        }
    }
}
