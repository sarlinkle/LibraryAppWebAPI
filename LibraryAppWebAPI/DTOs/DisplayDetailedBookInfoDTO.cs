using LibraryAppWebAPI.Models;

namespace LibraryAppWebAPI.DTOs
{
    public class DisplayDetailedBookInfoDTO
    {
        public required string Title { get; set; }
        public required string ISBN { get; set; }
        public required DateOnly ReleaseDate { get; set; }     
        public required List<string> AuthorNames { get; set; } = new();
        public string? Rating { get; set; }
    }
}
