using MusicSite.API.Interaces;

namespace MusicSite.API.Persistence.Entities
{
    public class Show : IIdentifiable, ITimeAuditable
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Rate { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

        public Guid VenueId { get; set; }
        public virtual Venue? Venue { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
