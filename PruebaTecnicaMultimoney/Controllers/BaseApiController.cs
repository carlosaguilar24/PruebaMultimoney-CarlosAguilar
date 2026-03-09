using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PruebaTecnicaMultimoney.Controllers
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        private ISender _sender = null!;
        protected ISender sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
