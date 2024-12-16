using Microsoft.EntityFrameworkCore;

namespace SchoolProject.Core.Wrapper
{
    public static class QueryableExtensions
    {
        public static async Task<PaginationResult<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize) where T : class
        {
            if (source == null) throw new ArgumentNullException("Empty");
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 10 : pageSize;
            int count = await source.AsNoTracking().CountAsync();
            if (count == 0) return PaginationResult<T>.Success(new List<T>(), count, pageNumber, pageSize);
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            var items = await source.Skip(pageNumber - 1).Take(pageSize).ToListAsync();
            return PaginationResult<T>.Success(items, count, pageNumber, pageSize);
        }
    }
}
