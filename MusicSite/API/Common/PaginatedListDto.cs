using MusicSite.API.Interaces;

namespace MusicSite.API.Common
{
    public class PaginatedListDto<T> where T : class
    {
        public IEnumerable<T> Items { get; private set; }
        public int Page { get; private set; }
        public int PageSize { get; private set; }
        public int ItemCount { get; private set; }

        public PaginatedListDto(IEnumerable<T> items, int itemCount, IPaginationQueryParameters pagination)
        {
            ItemCount = itemCount;
            Items = items;
            Page = pagination.Page;
            PageSize = pagination.PageSize;
        }
    }
}
