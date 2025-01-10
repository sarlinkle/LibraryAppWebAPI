using LibraryAppWebAPI.Models;

namespace LibraryAppWebAPI.DTOs
{
    public class CreateNewLibraryUserDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }        
    }
}
