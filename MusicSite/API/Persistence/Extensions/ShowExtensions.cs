using MusicSite.API.Persistence.Entities;

namespace MusicSite.API.Persistence.Extensions
{
    public static class ShowExtensions
    {
        public static IQueryable<Show> FilterDateRange(this IQueryable<Show> query, DateTime? start, DateTime? end)
        {
            if (start.HasValue)
                return query.Where(x => x.StartAt.Date >= start.Value.Date);

            if(end.HasValue)
                return query.Where(x => x.EndAt.Date <= end.Value.Date);

            return query;
        }

    }
}
