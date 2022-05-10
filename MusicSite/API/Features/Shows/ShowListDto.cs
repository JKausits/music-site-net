namespace MusicSite.API.Features
{
    public class ShowListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public VenueDto Venue { get; set; }
    }
}
