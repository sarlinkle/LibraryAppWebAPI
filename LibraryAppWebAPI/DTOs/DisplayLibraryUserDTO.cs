using LibraryAppWebAPI.Models;

namespace LibraryAppWebAPI.DTOs
{
    public class DisplayLibraryUserDTO
    {
        public required int LibraryCardNumber { get; set; }
        public required string FullName { get; set; }
    }
}
