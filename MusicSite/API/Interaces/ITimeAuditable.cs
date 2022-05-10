namespace MusicSite.API.Interaces
{
    public interface ITimeAuditable
    {
        DateTime CreatedAt { get; set; }
        DateTime ModifiedAt { get; set; }
    }
}
