namespace TodoApp.Responses
{
    public class PagingMetaData
    {
        public PagingMetaData(int PageNumber, int PageSize, int TotalCount)
        {
            pageNumber = PageNumber;
            pageSize = PageSize;
            totalCount = TotalCount;
            numberOfPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
        }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int totalCount { get; set; }
        public int numberOfPages { get; set; }
    }
}