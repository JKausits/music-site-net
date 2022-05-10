namespace MusicSite.API.Settings
{
    public class JwtSettings
    {
        public string SecurityKey { get; set; } = string.Empty;
        public int ExpiryInMinutes { get; set; }
    }
}
