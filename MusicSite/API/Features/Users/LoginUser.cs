using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicSite.API.Exceptions;
using MusicSite.API.Persistence;
using MusicSite.API.Persistence.Extensions;
using System.Net;

namespace MusicSite.API.Features
{
    public partial class UsersController
    {

        [ProducesResponseType(typeof(LoginResponseDto), (int)HttpStatusCode.OK)]
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDto request, CancellationToken token)
        {
            var command = new LoginUserCommand(request);
            var result = await Mediator.Send(command, token);
            var jwtToken = JwtTokenGenerator.GenerateToken(result);

            return Ok(new LoginResponseDto(jwtToken));
        }
    }

    public class LoginUserCommand : IRequest<UserTokenDto>
    {
        public LoginUserCommand(UserLoginDto request)
        {
            Request = request;
        }

        public UserLoginDto Request { get; set; }
    }


    public class LoginUserCommandHandler : BaseRequestHandler<LoginUserCommand, UserTokenDto>
    {
        public LoginUserCommandHandler(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<UserTokenDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await Context.Users.FindByEmailAsync(request.Request.Email, cancellationToken);

            if (user == null || BCrypt.Net.BCrypt.Verify(request.Request.Password, user.Password) == false)
                throw new UnauthorizedException("Invalid Email/Password Combination.");

            return Mapper.Map<UserTokenDto>(user);
        }
    }

    public class LoginResponseDto
    {
        public LoginResponseDto(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}
