using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MusicSite.API.Interaces;
using MusicSite.API.Persistence.Extensions;

namespace MusicSite.API.Common
{
    public interface IPaginator
    {
        Task<PaginatedListDto<R>> Paginate<T, R>(
            IQueryable<T> query,
            IPaginationQueryParameters pagination,
            CancellationToken token)
            where T : class
            where R : class;
    }

    public class Paginator : IPaginator
    {
        private readonly IMapper _mapper;

        public Paginator(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<PaginatedListDto<R>> Paginate<T, R>(
            IQueryable<T> query, 
            IPaginationQueryParameters pagination,
            CancellationToken token)
            where T : class
            where R : class
        {
            var itemCount = await query.CountAsync(token);
            var items = await query
                .Paginate(pagination)
                .ProjectTo<R>(_mapper.ConfigurationProvider)
                .ToListAsync(token);
            return new PaginatedListDto<R>(items, itemCount, pagination);
        }
    }
}
