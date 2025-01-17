using LibraryAppWebAPI.Models;

namespace LibraryAppWebAPI.DTOs
{
    public class DisplayAuthorDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required List<string> BookTitles { get; set; } = new();
    }
}
