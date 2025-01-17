namespace LibraryAppWebAPI.DTOs
{
    public class DisplayReturnDTO
    {
        public required int CheckoutId { get; set; }
        public required List<string> BookTitles { get; set; } = new();
        public string? Rating { get; set; }
        public DateOnly? DateReturned { get; set; }
    }
}
