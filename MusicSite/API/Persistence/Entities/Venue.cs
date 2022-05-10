using MusicSite.API.Interaces;

namespace MusicSite.API.Persistence.Entities
{
    public class Venue : IIdentifiable, ITimeAuditable
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<Show> Shows { get; } = new List<Show>();

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
