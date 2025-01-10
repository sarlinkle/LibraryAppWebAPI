namespace LibraryAppWebAPI.DTOs
{
    public class DisplayReturnDTO
    {
        public required int CheckoutId { get; set; }
        public string? Rating { get; set; }
        public DateTime? DateReturned { get; set; }
    }
}
