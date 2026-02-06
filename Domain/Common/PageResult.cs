namespace Domain.Common
{
    public class PagedResult<T>
    {
        public IReadOnlyList<T> Items { get; }
        public int TotalCount { get; }
        public int PageSize { get; }
        public int CurrentPage { get; }

        // ✅ ADD THIS
        public int TotalPages =>
            (int)Math.Ceiling(TotalCount / (double)PageSize);

        public PagedResult(
            IReadOnlyList<T> items,
            int totalCount,
            int currentPage,
            int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }
    }
}