using Microsoft.AspNetCore.Mvc;
using MediatR;
using Framework.Filters;
using Framework.Resources;

namespace Travel.Framework.Base
{
    [ApiController]
    [ApiVersion("1.0")]
   // [ResultApiFormatter]
    [Route("api/v{v:apiVersion}/[controller]/[action]")]
    //   [ApiReturnAction]
    public class BaseController : ControllerBase
    {
        private readonly IMediator mediator;

        public BaseController(IMediator mediator)
        {
            this.mediator = mediator;
        }
    }
}
