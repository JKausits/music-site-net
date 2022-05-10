using MusicSite.API.Interaces;

namespace MusicSite.API.Features
{
    public class UserTokenDto : IIdentifiable
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public int Exp { get; set; }
    }
}
