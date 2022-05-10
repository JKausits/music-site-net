using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicSite.API.Common;
using MusicSite.API.Interaces;
using MusicSite.API.Persistence;
using MusicSite.API.Persistence.Entities;
using System.Net;

namespace MusicSite.API.Features
{
    public partial class VenuesController
    {
        [ProducesResponseType(typeof(PaginatedListDto<VenueDto>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<IActionResult> GetVenues([FromQuery] VenueQueryParametersDto parameters, CancellationToken token)
        {
            var query = new GetVenuesQuery(parameters);
            var response = await Mediator.Send(query, token);
            return Ok(response);
        }
    }

    public class GetVenuesQuery : IRequest<PaginatedListDto<VenueDto>>
    {
        public GetVenuesQuery(VenueQueryParametersDto parameters)
        {
            Parameters = parameters;
        }

        public VenueQueryParametersDto Parameters { get; private set; }
    }

    public class GetVenuesQueryHandler : BaseRequestHandler<GetVenuesQuery, PaginatedListDto<VenueDto>>
    {
        private readonly IPaginator _paginator;
        public GetVenuesQueryHandler(
            ApplicationDbContext context,
            IMapper mapper,
            IPaginator paginator) : base(context, mapper)
        {
            _paginator = paginator;
        }

        public override async Task<PaginatedListDto<VenueDto>> Handle(GetVenuesQuery request, CancellationToken cancellationToken)
        {
            var query = Context.Venues.AsQueryable();
            return await _paginator.Paginate<Venue, VenueDto>(query, request.Parameters, cancellationToken);
        }
    }

    public class VenueQueryParametersDto : IPaginationQueryParameters
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
