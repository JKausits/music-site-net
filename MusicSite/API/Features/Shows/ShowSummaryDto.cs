using MusicSite.API.Interaces;

namespace MusicSite.API.Features
{
    public class ShowSummaryDto : IIdentifiable
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Rate { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

        public Guid VenueId { get; set; }
    }
}
