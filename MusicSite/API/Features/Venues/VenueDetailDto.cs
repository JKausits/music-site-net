using MusicSite.API.Interaces;

namespace MusicSite.API.Features
{
    public class VenueDetailDto : IIdentifiable
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<ShowSummaryDto> Shows { get; set; }
    }
}
