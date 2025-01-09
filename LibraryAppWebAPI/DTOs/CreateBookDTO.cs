using LibraryAppWebAPI.Models;

namespace LibraryAppWebAPI.DTOs
{
    public class CreateBookDTO
    {
        public required string Title { get; set; }
        public required string ISBN { get; set; }
        public required int ReleaseYear { get; set; }
        public required List<Author> Authors { get; set; }
        public string? Rating { get; set; }
    }
}
