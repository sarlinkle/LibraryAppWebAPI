namespace LibraryAppWebAPI.Models
{
    public class LibraryUserDTO
    {
        public required string LibraryCardNumber { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public List<Book>? BorrowedBooks { get; set; }
    }
}
