using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicSite.API.Features.Shows.Validations;
using MusicSite.API.Persistence;
using MusicSite.API.Persistence.Extensions;
using System.Net;

namespace MusicSite.API.Features
{
    public partial class ShowsController
    {
        [ProducesResponseType(typeof(ShowSummaryDto), (int)HttpStatusCode.OK)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShow(Guid id, [FromBody] ShowRequestDto request, CancellationToken token)
        {
            var command = new UpdateShowCommand(request, id);
            var result = await Mediator.Send(command, token);

            return StatusCode((int)HttpStatusCode.Created, result);
        }
    }

    public class UpdateShowCommand : IRequest<ShowSummaryDto>
    {
        public UpdateShowCommand(ShowRequestDto request, Guid id)
        {
            Request = request;
            Id = id;
        }

        public ShowRequestDto Request { get; set; }
        public Guid Id { get; set; }
    }

    public class UpdateShowCommandHandler : BaseRequestHandler<UpdateShowCommand, ShowSummaryDto>
    {
        private readonly IShowValidator _validator;

        public UpdateShowCommandHandler(ApplicationDbContext context, IMapper mapper, IShowValidator validator) : base(context, mapper)
        {
            _validator = validator;
        }

        public override async Task<ShowSummaryDto> Handle(UpdateShowCommand request, CancellationToken cancellationToken)
        {
            var show = await Context.Shows.FindByIdAsync(request.Id, cancellationToken, "Show");

            Mapper.Map(request.Request, show);
            await _validator.ValidateShowUpdatedAsync(show, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
            return Mapper.Map<ShowSummaryDto>(show);
        }
    }
}
