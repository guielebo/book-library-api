namespace Infrastructure.Entities
{
    public class PaginatedResult<T>
    {
        public ICollection<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
