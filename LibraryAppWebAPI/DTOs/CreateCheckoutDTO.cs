namespace LibraryAppWebAPI.DTOs
{
    public class CreateCheckoutDTO
    {
        public int LibraryUserId { get; set; }
        public required List<int> BookIds { get; set; } = new();
    }
}
