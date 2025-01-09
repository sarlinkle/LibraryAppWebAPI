namespace LibraryAppWebAPI.Models
{
    public class LibraryUser
    {
        public int Id { get; set; }
        public required string LibraryCardNumber { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public List<Checkout>? Checkouts { get; set; } = new();
    }
}
