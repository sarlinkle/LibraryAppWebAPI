namespace LibraryAppWebAPI.Models
{
    public class Checkout
    {
        public required int CheckoutID { get; set; }
        public required ICollection<Book> BookID { get; set; }
        public required LibraryUser LibraryUser { get; set; }
        public required bool Available { get; set; }
        public DateOnly? BorrowedDate { get; set; }
        public DateOnly? ReturnDate { get; set; }
    }
}
