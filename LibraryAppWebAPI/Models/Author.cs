namespace LibraryAppWebAPI.Models
{
    public class Author
    {
        public required int AuthorID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required List<Book> Books { get; set; }
    }
}
