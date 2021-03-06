using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MusicSite.API.Features
{
    [Route("api/[controller]")]
    [Authorize]
    public partial class ShowsController : ApiControllerBase
    {
        public ShowsController(IMediator mediatR) : base(mediatR)
        {
        }
    }
}
