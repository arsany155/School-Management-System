namespace SchoolProject.Core.Wrapper
{
    public class PaginationResult<T>
    {
        public List<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public object Meta { get; set; }
        public bool HasPreviousPage => CurrentPage < TotalPages;
        public bool HasNextPage => CurrentPage > 1;
        public bool Successed { get; set; }
        public List<string> Messages { get; set; } = new();
        public int PageCount { get; set; }
        public int PageSize { get; set; }

        public PaginationResult()
        {

        }

        public PaginationResult(List<T> data)
        {
            Data = data;
        }

        internal PaginationResult(bool successed, List<T> data = default, List<string> messages = null, int count = 0, int page = 1, int pageSize = 10)
        {
            Data = data;
            CurrentPage = page;
            Successed = successed;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
        }

        public static PaginationResult<T> Success(List<T> data, int count, int page, int pageSize)
        {
            return new(true, data, null, count, page, pageSize);
        }


    }
}
