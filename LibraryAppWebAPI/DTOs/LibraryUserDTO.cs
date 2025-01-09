using LibraryAppWebAPI.Models;

namespace LibraryAppWebAPI.DTOs
{
    public class LibraryUserDTO
    {
        public int Id { get; set; }
        public required string LibraryCardNumber { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }        
    }
}
