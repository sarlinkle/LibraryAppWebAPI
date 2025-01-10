using LibraryAppWebAPI.Models;

namespace LibraryAppWebAPI.DTOs
{
    public class DisplayBookDTO
    {
        public required string Title { get; set; }
        public required string ISBN { get; set; }
        public required DateOnly ReleaseDate { get; set; }
        public required List<string> AuthorIds { get; set; } = new();
        public string? Rating { get; set; }
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
