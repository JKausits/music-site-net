using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicSite.API.Persistence;
using MusicSite.API.Persistence.Extensions;
using System.Net;

namespace MusicSite.API.Features
{
    public partial class VenuesController
    {
        [ProducesResponseType(typeof(VenueDto), (int)HttpStatusCode.OK)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVenue(Guid id, [FromBody] VenueRequestDto request, CancellationToken token)
        {
            var command = new UpdateVenueCommand(id, request);
            var result = await Mediator.Send(command, token);

            return Ok(result);
        }
    }

    public class UpdateVenueCommand : IRequest<VenueDto>
    {
        public UpdateVenueCommand(Guid id, VenueRequestDto request)
        {
            Request = request;
            Id = id;
        }

        public Guid Id { get; set; }
        public VenueRequestDto Request { get; set; }
    }

    public class UpdateVenueCommandHandler : BaseRequestHandler<UpdateVenueCommand, VenueDto>
    {
        public UpdateVenueCommandHandler(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<VenueDto> Handle(UpdateVenueCommand request, CancellationToken cancellationToken)
        {
            var venue = await Context.Venues.FindByIdAsync(request.Id, cancellationToken, "Venue");

            Mapper.Map(request.Request, venue);
            await Context.SaveChangesAsync(cancellationToken);

            return Mapper.Map<VenueDto>(venue);
        }
    }
}
