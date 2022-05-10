using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicSite.API.Persistence;
using MusicSite.API.Persistence.Entities;
using System.Net;

namespace MusicSite.API.Features
{
    public partial class VenuesController
    {
        [ProducesResponseType(typeof(VenueDto), (int)HttpStatusCode.Created)]
        [HttpPost]
        public async Task<IActionResult> CreateVenue([FromBody] VenueRequestDto request, CancellationToken token)
        {
            var command = new CreateVenueCommand(request);
            var result = await Mediator.Send(command, token);

            return StatusCode((int)HttpStatusCode.Created, result);
        }
    }

    public class CreateVenueCommand : IRequest<VenueDto>
    {
        public CreateVenueCommand(VenueRequestDto request)
        {
            Request = request;
        }

        public VenueRequestDto Request { get; set; }
    }

    public class CreateVenueCommandHandler : BaseRequestHandler<CreateVenueCommand, VenueDto>
    {
        public CreateVenueCommandHandler(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<VenueDto> Handle(CreateVenueCommand request, CancellationToken cancellationToken)
        {
            var venue = Mapper.Map<Venue>(request.Request);
            Context.Add(venue);
            await Context.SaveChangesAsync(cancellationToken);

            return Mapper.Map<VenueDto>(venue);
        }
    }
}
