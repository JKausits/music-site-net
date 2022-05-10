using Microsoft.EntityFrameworkCore;
using MusicSite.API.Exceptions;
using MusicSite.API.Interaces;

namespace MusicSite.API.Persistence.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<T> FindByIdAsync<T>(this IQueryable<T> query, Guid id, CancellationToken token, string? name = null) where T : class, IIdentifiable
        {
            var entity = await query
                .FilterById(id)
                .FirstOrDefaultAsync(token);

            if (entity == null && !string.IsNullOrWhiteSpace(name))
                throw new NotFoundException(id, name);

            return entity!;
        }

        public static IQueryable<T> FilterById<T>(this IQueryable<T> query, Guid id) where T : class, IIdentifiable
        {
            return query.Where(x => x.Id.Equals(id));
        }

        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, IPaginationQueryParameters parameters) where T : class
        {
            return query.Skip(parameters.Page * parameters.PageSize).Take(parameters.PageSize);
        }
    }
}
