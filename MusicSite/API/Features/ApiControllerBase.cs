using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MusicSite.API.Features
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected readonly IMediator Mediator;

        public ApiControllerBase(IMediator mediatR)
        {
            Mediator = mediatR;
        }
    }
}
