using LibraryAppWebAPI.Models;

namespace LibraryAppWebAPI.DTOs
{
    public class CheckoutDTO
    {
        public int LibraryUserId { get; set; }
        public int BookId { get; set; }
    }
}
