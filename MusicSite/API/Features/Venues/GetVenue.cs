using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicSite.API.Persistence;
using MusicSite.API.Persistence.Extensions;
using System.Net;

namespace MusicSite.API.Features
{
    public partial class VenuesController
    {

        [ProducesResponseType(typeof(VenueDetailDto), (int)HttpStatusCode.OK)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVenue(Guid id, CancellationToken token)
        {
            var query = new GetVenueQuery(id);
            var response = await Mediator.Send(query, token);
            return Ok(response);
        }
    }

    public class GetVenueQuery : IRequest<VenueDetailDto>
    {
        public GetVenueQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    public class GetVenueQueryHandler : BaseRequestHandler<GetVenueQuery, VenueDetailDto>
    {
        public GetVenueQueryHandler(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<VenueDetailDto> Handle(GetVenueQuery request, CancellationToken cancellationToken)
        {
            var venue = await Context.Venues
                .ProjectTo<VenueDetailDto>(Mapper.ConfigurationProvider)
                .FindByIdAsync(request.Id, cancellationToken, "Venue");

            return venue;
        }
    }
}
