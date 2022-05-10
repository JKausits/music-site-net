using MusicSite.API.Interaces;

namespace MusicSite.API.Features
{
    public class VenueDto : IIdentifiable
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
