using LibraryAppWebAPI.Models;

namespace LibraryAppWebAPI.DTOs
{
    public class DisplayCheckoutDTO
    {
        public required string LibraryCardNumber { get; set; }
        public required List<Book> Books { get; set; }
        public DateTime DateBorrowed { get; set; }
    }
}
