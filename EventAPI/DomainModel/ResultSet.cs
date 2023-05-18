namespace EventAPI.DomainModel
{
    public class ResultSet
    {
            public int TotalCount { get; set; }
            public int TotalPages { get; set; }
            public int Page { get; set; }
            public int PageSize { get; set; }
            public IEnumerable<Event> Results { get; set; }
    }
}
