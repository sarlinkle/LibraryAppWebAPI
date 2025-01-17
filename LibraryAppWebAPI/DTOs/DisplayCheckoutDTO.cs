using LibraryAppWebAPI.Models;

namespace LibraryAppWebAPI.DTOs
{
    public class DisplayCheckoutDTO
    {
        public required int LibraryCardNumber { get; set; }
        public required List<string> BookTitles { get; set; } = new();
        public required DateTime DateBorrowed { get; set; }
        public required DateTime DateDue { get; set; }
    }
}
