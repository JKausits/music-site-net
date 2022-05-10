using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MusicSite.API.Features
{
    [Route("api/[controller]")]
    [Authorize]
    public partial class VenuesController : ApiControllerBase
    {
        public VenuesController(IMediator mediatR) : base(mediatR)
        {
        }
    }
}
