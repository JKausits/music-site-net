using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSite.API.Persistence;
using MusicSite.API.Persistence.Extensions;
using System.Net;

namespace MusicSite.API.Features
{
    public partial class ShowsController
    {
        [ProducesResponseType(typeof(IEnumerable<ShowListDto>), (int)HttpStatusCode.OK)]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetShows([FromQuery] GetShowsQueryParameters queryParameters, CancellationToken token)
        {
            var query = new GetShowsQuery(queryParameters);
            var result = await Mediator.Send(query, token);

            return StatusCode((int)HttpStatusCode.Created, result);
        }
    }

    public class GetShowsQueryParameters
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }


    public class GetShowsQuery : IRequest<IEnumerable<ShowListDto>>
    {
        public GetShowsQuery(GetShowsQueryParameters queryParameters)
        {
            QueryParameters = queryParameters;
        }

        public GetShowsQueryParameters QueryParameters { get; set; }
    }

    public class GetShowsQueryHandler : BaseRequestHandler<GetShowsQuery, IEnumerable<ShowListDto>>
    {
        public GetShowsQueryHandler(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<IEnumerable<ShowListDto>> Handle(GetShowsQuery request, CancellationToken cancellationToken)
        {
            return await Context.Shows
                .FilterDateRange(request.QueryParameters.StartDate, request.QueryParameters.EndDate)
                .ProjectTo<ShowListDto>(Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
