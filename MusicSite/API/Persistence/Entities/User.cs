using MusicSite.API.Interaces;

namespace MusicSite.API.Persistence.Entities
{
    public class User : IIdentifiable, ITimeAuditable
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
