namespace LibraryAppWebAPI.DTOs
{
    public class SearchDTO
    {
    }

    public class SearchBookDTO
    {
        public string? Title { get; set; }
        public string? ISBN { get; set; }
    }
    public class SearchAuthorDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
    public class SearchAuthorBookDTO
    {
        public string? Title { get; set; }
        public string? ISBN { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
