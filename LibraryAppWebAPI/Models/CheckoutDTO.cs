namespace LibraryAppWebAPI.Models
{
    public class CheckoutDTO
    {
        public required LibraryUser LibraryUser { get; set; }
        public required ICollection<Book> BookID { get; set; }
    }
}
