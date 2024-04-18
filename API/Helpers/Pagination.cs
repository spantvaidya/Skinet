namespace API.Helpers
{
    public class Pagination<T> where T : class
    {
        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            PageCount = count;
            Data = data;
        }
        public int PageIndex { get; set; }
        public int PageSize { get; set;}
        public int PageCount { get; set;}
        public IReadOnlyList<T> Data { get; set; }
    }
}