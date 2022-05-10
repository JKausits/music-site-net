using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicSite.API.Persistence;
using MusicSite.API.Persistence.Extensions;

namespace MusicSite.API.Features
{
    public partial class VenuesController
    {
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenue(Guid id, CancellationToken token)
        {
            var command = new DeleteVenueCommand(id);
            var result = await Mediator.Send(command, token);

            return Ok(result);
        }
    }

    public class DeleteVenueCommand : IRequest<bool>
    {
        public DeleteVenueCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    public class DeleteVenueCommandHandler : BaseRequestHandler<DeleteVenueCommand, bool>
    {
        public DeleteVenueCommandHandler(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<bool> Handle(DeleteVenueCommand request, CancellationToken cancellationToken)
        {
            var venue = await Context.Venues.FindByIdAsync(request.Id, cancellationToken, "Venue");
            Context.Remove(venue);

            return await Context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
