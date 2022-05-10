using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicSite.API.Persistence;
using MusicSite.API.Persistence.Extensions;
using System.Net;

namespace MusicSite.API.Features
{
    public partial class ShowsController
    {
        [ProducesResponseType(typeof(ShowSummaryDto), (int)HttpStatusCode.OK)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShow(Guid id, CancellationToken token)
        {
            var query = new GetShowQuery(id);
            var result = await Mediator.Send(query, token);

            return StatusCode((int)HttpStatusCode.Created, result);
        }
    }


    public class GetShowQuery : IRequest<ShowSummaryDto>
    {
        public GetShowQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    public class GetShowQueryHandler : BaseRequestHandler<GetShowQuery, ShowSummaryDto>
    {
        public GetShowQueryHandler(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<ShowSummaryDto> Handle(GetShowQuery request, CancellationToken cancellationToken)
        {
            return await Context.Shows
                .ProjectTo<ShowSummaryDto>(Mapper.ConfigurationProvider)
                .FindByIdAsync(request.Id, cancellationToken, "Show");
        }
    }
}
