using LibraryAppWebAPI.Models;

namespace LibraryAppWebAPI.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string ISBN { get; set; }
        public int ReleaseYear { get; set; }
        public required List<Author> Authors { get; set; } = new();
        public string? Rating { get; set; }
    }
}
