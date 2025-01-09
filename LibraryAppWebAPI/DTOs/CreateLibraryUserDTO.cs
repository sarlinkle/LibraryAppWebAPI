using LibraryAppWebAPI.Models;

namespace LibraryAppWebAPI.DTOs
{
    public class CreateLibraryUserDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }        
    }
}
