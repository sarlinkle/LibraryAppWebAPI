using LibraryAppWebAPI.Models;

namespace LibraryAppWebAPI.DTOs
{
    public class AuthorDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required List<Book> Books { get; set; }
    }

    public class CreateAuthorDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required List<Book> Books { get; set; }
    }
}
