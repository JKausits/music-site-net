namespace MusicSite.API.Interaces
{
    public interface IPaginationQueryParameters
    {
        int Page { get; set; }
        int PageSize { get; set; }
    }
}
