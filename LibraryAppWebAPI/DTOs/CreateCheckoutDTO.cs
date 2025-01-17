namespace LibraryAppWebAPI.DTOs
{
    public class CreateCheckoutDTO
    {
        public int LibraryUserId { get; set; }
        public required List<int> BookIds { get; set; } = new();
        public DateTime DateBorrowed { get; set; }
        public DateTime DateDue { get; set; }
    }
}
