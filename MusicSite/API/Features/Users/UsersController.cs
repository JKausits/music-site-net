using MediatR;
using MusicSite.API.Common;

namespace MusicSite.API.Features
{
    public partial class UsersController : ApiControllerBase
    {
        protected readonly IJwtTokenGenerator JwtTokenGenerator;

        public UsersController(IMediator mediatR, IJwtTokenGenerator jwtTokenGenerator) : base(mediatR)
        {
            JwtTokenGenerator = jwtTokenGenerator;
        }
    }
}
