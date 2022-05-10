using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicSite.API.Persistence;
using MusicSite.API.Persistence.Entities;
using System.Net;

namespace MusicSite.API.Features
{
    public partial class UsersController
    {

        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Created)]
        [HttpPost("register")]
        [Authorize]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterDto request, CancellationToken token)
        {
            var command = new RegisterUserCommand(request);
            var result = await Mediator.Send(command, token);
            var jwtToken = JwtTokenGenerator.GenerateToken(result);

            return StatusCode((int)HttpStatusCode.Created, jwtToken);
        }
    }

    public class RegisterUserCommand : IRequest<UserTokenDto>
    {
        public RegisterUserCommand(UserRegisterDto request)
        {
            Request = request;
        }

        public UserRegisterDto Request { get; set; }
    }


    public class RegisterUserCommandHandler : BaseRequestHandler<RegisterUserCommand, UserTokenDto>
    {
        public RegisterUserCommandHandler(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<UserTokenDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = Mapper.Map<User>(request.Request);
            Context.Users.Add(user);
            await Context.SaveChangesAsync(cancellationToken);

            return Mapper.Map<UserTokenDto>(user);
        }
    }
}
