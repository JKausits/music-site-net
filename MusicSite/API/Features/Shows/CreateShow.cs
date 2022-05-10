using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicSite.API.Features.Shows.Validations;
using MusicSite.API.Persistence;
using MusicSite.API.Persistence.Entities;
using MusicSite.API.Persistence.Extensions;
using System.Net;

namespace MusicSite.API.Features
{
    public partial class VenuesController
    {
        [ProducesResponseType(typeof(ShowSummaryDto), (int)HttpStatusCode.Created)]
        [HttpPost("{id}/shows")]
        public async Task<IActionResult> CreateShow(Guid id, [FromBody] ShowRequestDto request, CancellationToken token)
        {
            var command = new CreateShowCommand(request, id);
            var result = await Mediator.Send(command, token);

            return StatusCode((int)HttpStatusCode.Created, result);
        }
    }

    public class CreateShowCommand : IRequest<ShowSummaryDto>
    {
        public CreateShowCommand(ShowRequestDto request, Guid venueId)
        {
            Request = request;
            VenueId = venueId;
        }

        public ShowRequestDto Request { get; set; }
        public Guid VenueId { get; set; }
    }


    public class CreateShowCommandHandler : BaseRequestHandler<CreateShowCommand, ShowSummaryDto>
    {
        private readonly IShowValidator _validator;
        public CreateShowCommandHandler(ApplicationDbContext context, IMapper mapper, IShowValidator validator) : base(context, mapper)
        {
            _validator = validator;
        }

        public override async Task<ShowSummaryDto> Handle(CreateShowCommand request, CancellationToken cancellationToken)
        {
            var venue = await Context.Venues.FindByIdAsync(request.VenueId, cancellationToken, "Venue");

            var show = Mapper.Map<Show>(request.Request);
            await _validator.ValidateShowCreatedAsync(show, cancellationToken);
            venue.Shows.Add(show);
            await Context.SaveChangesAsync(cancellationToken);
            return Mapper.Map<ShowSummaryDto>(show);
        }
    }
}
